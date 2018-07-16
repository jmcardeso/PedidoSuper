Option Strict On
Option Explicit Off

Imports System.Drawing.Printing
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmPedidoAntiguo
    Dim PedidoSeleccionado As IEnumerable(Of XElement) 'Declaramos la variable que contendrá el pedido buscado
    Dim TotalAntiguo As Decimal

    Private Sub frmPedidoAntiguo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Asignamos el ancho de las columnas por código (en modo diseño no lo hace bien)
        lsvPedidoAntiguo.Columns.Item(0).Width = 38
        lsvPedidoAntiguo.Columns.Item(1).Width = 230
        lsvPedidoAntiguo.Columns.Item(2).Width = 45
        lsvPedidoAntiguo.Columns.Item(3).Width = 52

        tbxTotal.Text = frmListaPedidos.lsvBusquedaPedidos.SelectedItems(0).SubItems(1).Text 'Escribimos el total del pedido...
        Me.Text = "Pedido: " & frmListaPedidos.lsvBusquedaPedidos.SelectedItems(0).SubItems(0).Text 'Y la fecha en la barra de título
        TotalAntiguo = CDec(tbxTotal.Text.TrimEnd("€"c)) 'Almacenamos el total antiguo para poder trabajar con él

        ' Realizamos una consulta LINQ para obtener un pedido cuya fecha y total sean los seleccionados
        PedidoSeleccionado = From elemento In frmPedidoFroiz.docXMLPedidos.Descendants("Pedido") Where CDate(elemento.Attribute("Fecha").Value) = CDate(frmListaPedidos.lsvBusquedaPedidos.SelectedItems(0).SubItems(0).Text) And elemento.Attribute("Total").Value = TotalAntiguo.ToString Select elemento
        If PedidoSeleccionado.Count <> 1 Then 'Si no hay ningún pedido o más de uno, advertimos al usuario de un error y salimos
            MessageBox.Show("Se ha producido un error inesperado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
        DibujarProductos(PedidoSeleccionado.Descendants("Producto"), True) 'En caso contrario, dibujamos el pedido en el ListView
    End Sub

    Private Sub btnEditarLinea_Click(sender As Object, e As EventArgs) Handles btnEditarLinea.Click
        Dim TotalNuevo As Decimal = 0
        Dim NombreAntiguo As String
        Dim ProductoBorrar, PedidoBorrar As XElement
        Dim ListaProductos As IEnumerable(Of XElement)

        If lsvPedidoAntiguo.SelectedItems.Count > 0 Then 'Si hay un elemento seleccionado...
            dlgEditarLinea.tbxCant.Text = lsvPedidoAntiguo.SelectedItems.Item(0).SubItems(0).Text 'Asignamos sus datos a los TextBox del diálogo
            dlgEditarLinea.tbxDescripcion.Text = lsvPedidoAntiguo.SelectedItems.Item(0).SubItems(1).Text
            dlgEditarLinea.tbxPVP.Text = lsvPedidoAntiguo.SelectedItems.Item(0).SubItems(2).Text.TrimEnd("€"c)
            dlgEditarLinea.tbxImporte.Text = lsvPedidoAntiguo.SelectedItems.Item(0).SubItems(3).Text.TrimEnd("€"c)
            NombreAntiguo = dlgEditarLinea.tbxDescripcion.Text

            If dlgEditarLinea.ShowDialog = DialogResult.OK Then 'Si el usuario confirma los cambios...

                If CDec(dlgEditarLinea.tbxCant.Text) = 0 Then
                    ListaProductos = From elemento2 As XElement In (From elemento As XElement In PedidoSeleccionado.Descendants("Producto") Where elemento.Element("Nombre").Value = NombreAntiguo Select elemento)
                    ProductoBorrar = ListaProductos.First
                    TotalNuevo = TotalAntiguo - CDec(ProductoBorrar.Element("Precio").Value)
                    ProductoBorrar.Remove()
                    lsvPedidoAntiguo.Items.Remove(lsvPedidoAntiguo.SelectedItems.Item(0))
                Else
                    For Each Producto As XElement In (From elemento As XElement In PedidoSeleccionado.Descendants("Producto") Where elemento.Element("Nombre").Value = NombreAntiguo Select elemento)
                        Producto.Element("Unidades").Value = dlgEditarLinea.tbxCant.Text
                        Producto.Element("Nombre").Value = dlgEditarLinea.tbxDescripcion.Text
                        Producto.Element("PrecioUd").Value = dlgEditarLinea.tbxPVP.Text
                        TotalNuevo = TotalAntiguo - CDec(Producto.Element("Precio").Value)
                        Producto.Element("Precio").Value = dlgEditarLinea.tbxImporte.Text
                        TotalNuevo += CDec(dlgEditarLinea.tbxImporte.Text)
                        lsvPedidoAntiguo.Items.Item(lsvPedidoAntiguo.SelectedIndices(0)).SubItems(0).Text = dlgEditarLinea.tbxCant.Text
                        lsvPedidoAntiguo.Items.Item(lsvPedidoAntiguo.SelectedIndices(0)).SubItems(1).Text = dlgEditarLinea.tbxDescripcion.Text
                        lsvPedidoAntiguo.Items.Item(lsvPedidoAntiguo.SelectedIndices(0)).SubItems(2).Text = dlgEditarLinea.tbxPVP.Text & "€"c
                        lsvPedidoAntiguo.Items.Item(lsvPedidoAntiguo.SelectedIndices(0)).SubItems(3).Text = dlgEditarLinea.tbxImporte.Text & "€"c
                    Next
                End If

                If TotalNuevo <= 0 Then
                    PedidoBorrar = PedidoSeleccionado.First
                    PedidoBorrar.Remove()
                    Me.Close()
                Else
                    DibujarProductos(PedidoSeleccionado.Descendants("Producto"), True)
                    PedidoSeleccionado.First.Attribute("Total").Value = TotalNuevo.ToString
                    tbxTotal.Text = TotalNuevo.ToString & "€"c
                    TotalAntiguo = TotalNuevo
                End If

            End If

        Else 'Si no hay ningún elemento seleccionado, informamos al usuario y salimos
            MessageBox.Show("No hay ningún producto seleccionado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnImprimirPedido_Click(sender As Object, e As EventArgs) Handles btnImprimirPedido.Click
        Dim DS As New DSPedido 'Creamos un DataSet del tipo DSPedido, previamente creado en diseño. En él pasaremos los datos a Crystal Reports
        Dim Tabla As New DataTable("DSPedido_Tabla") 'Creamos una tabla con el mismo nombre que la que creamos en el DataSet para poder pasarle los datos
        Dim Columna As DataColumn 'Declaramos una variable del tipo datos de columna de la tabla...
        Dim Fila As DataRow 'Y otra del tipo datos de la fila
        Dim Encabezado, Pie As TextObject 'Por último, declaramos dos variables para el encabezado y el pie de página del informe (TextObject es un tipo de CR)

        'Asignamos los nombres y tipos de datos de las columnas. Han de coincidir con los creados en fase de diseño
        Columna = New DataColumn With {
            .DataType = System.Type.GetType("System.String"),
            .ColumnName = "Cantidad"
        }
        Tabla.Columns.Add(Columna) 'Añadimos la columna la colección de columnas de la tabla

        Columna = New DataColumn With {
            .DataType = System.Type.GetType("System.String"),
            .ColumnName = "Descripción"
        }
        Tabla.Columns.Add(Columna)

        Columna = New DataColumn With {
            .DataType = System.Type.GetType("System.String"),
            .ColumnName = "PVP"
        }
        Tabla.Columns.Add(Columna)

        Columna = New DataColumn With {
            .DataType = System.Type.GetType("System.String"),
            .ColumnName = "Importe"
        }
        Tabla.Columns.Add(Columna)

        For Each elemento As ListViewItem In lsvPedidoAntiguo.Items 'Para cada elemento de la lista de productos del pedido seleccionado...
            Fila = Tabla.NewRow() 'Creamos una nueva fila...
            Fila("Cantidad") = elemento.SubItems(0).Text 'Y añadimos los datos en ella
            Fila("Descripción") = elemento.SubItems(1).Text
            Fila("PVP") = elemento.SubItems(2).Text
            Fila("Importe") = elemento.SubItems(3).Text
            Tabla.Rows.Add(Fila) 'Añadimos la fila a la colección de filas de la tabla
        Next

        Encabezado = CType(plantillaPedido1.ReportDefinition.ReportObjects.Item("Encabezado"), TextObject) 'Asignamos el encabezado del informe previamente diseñado a la variable, forzando su tipo a TextObject para poder usar su propiedad Text
        Encabezado.Text = Me.Text 'Ponemos como texto del encabezado el texto "Pedido: fecha del pedido"
        Pie = CType(plantillaPedido1.ReportDefinition.ReportObjects.Item("Total"), TextObject) 'Hacemos lo mismo con el pie del informe, que contendrá el total
        Pie.Text = tbxTotal.Text

        plantillaPedido1.Database.Tables("DSPedido_Tabla").SetDataSource(Tabla) 'Asignamos la tabla "DSPedido_Tabla" que creamos por código con los datos del pedido a la tabla del informe creada en la fase de diseño

        ElegirImpresora.Document = DocImpresora 'Asignamos el PrintDocument al PrintDialog (necesario para guardar las opciones elegidas en el diálogo)
        ElegirImpresora.ShowDialog() 'Mostramos el cuadro de diálogo

        plantillaPedido1.SummaryInfo.ReportTitle = Me.Text 'Ponemos la fecha como título del documento (se ve en las propiedades de un pdf, por ejemplo)
        plantillaPedido1.PrintOptions.PrinterName = DocImpresora.PrinterSettings.PrinterName 'Asignamos la impresora elegida al CRDocument (el objeto que controla el informe)
        plantillaPedido1.PrintToPrinter(DocImpresora.PrinterSettings.Copies, False, DocImpresora.PrinterSettings.FromPage, DocImpresora.PrinterSettings.ToPage) 'Imprimimos el informe con las opciones elegidas por el usuario

        Me.Close() 'Salimos del formulario
    End Sub

#Region "Funciones auxiliares"
    ''' <summary>
    ''' Rellena el ListView con los productos del pedido buscado
    ''' </summary>
    ''' <param name="ListaProductos">El xml con los productos</param>
    ''' <param name="BorrarListView">(Opcional) Verdadero para borrar el ListView</param>
    Public Sub DibujarProductos(ByVal ListaProductos As IEnumerable(Of XElement), Optional ByVal BorrarListView As Boolean = False)
        Dim itemsProductos, item As ListViewItem 'Definimos una variable del tipo elemento del control ListView y su iterador
        Dim n As Integer = 0

        If BorrarListView Then lsvPedidoAntiguo.Items.Clear() 'Si el argumento opcional es True, borramos la lista

        For Each elemento In ListaProductos 'Recorremos la lista y le añadimos los productos
            itemsProductos = New ListViewItem 'Reinicializamos la clase con cada iteración para borrar sus datos
            itemsProductos.SubItems(0).Text = elemento.Element("Unidades").Value 'Asignamos las unidades compradas...
            itemsProductos.SubItems.Add(elemento.Element("Nombre").Value) 'El nombre...
            itemsProductos.SubItems.Add(elemento.Element("PrecioUd").Value & "€"c) 'El precio de cada unidad...
            itemsProductos.SubItems.Add(elemento.Element("Precio").Value & "€"c) 'Y el importe de la línea
            lsvPedidoAntiguo.Items.Add(itemsProductos) 'Y los añadimos al ListView
        Next

        For Each item In lsvPedidoAntiguo.Items
            If n Mod 2 <> 0 Then 'Si es par dibujamos en un color, si es impar en el otro
                item.BackColor = Color.Honeydew
            Else
                item.BackColor = Color.White
            End If
            n += 1
        Next
    End Sub
#End Region
End Class
Option Strict On
Option Explicit Off

Public Class frmPedidoAntiguo
    Private Sub frmPedidoAntiguo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim PedidoSeleccionado As IEnumerable(Of XElement)

        'CRInformePedido.FileName = Application.StartupPath & "\Pedido.rpt"

        lsvPedidoAntiguo.Columns.Item(0).Width = 38
        lsvPedidoAntiguo.Columns.Item(1).Width = 230
        lsvPedidoAntiguo.Columns.Item(2).Width = 45
        lsvPedidoAntiguo.Columns.Item(3).Width = 52

        tbxTotal.Text = frmListaPedidos.lsvBusquedaPedidos.SelectedItems(0).SubItems(1).Text 'Escribimos el total del pedido...
        Me.Text = "Pedido: " & frmListaPedidos.lsvBusquedaPedidos.SelectedItems(0).SubItems(0).Text 'Y la fecha en la barra de título
        ' Realizamos una consulta LINQ para obtener un pedido cuya fecha y total sean los seleccionados
        PedidoSeleccionado = From elemento In frmPedidoFroiz.docXMLPedidos.Descendants("Pedido") Where CDate(elemento.Attribute("Fecha").Value) = CDate(frmListaPedidos.lsvBusquedaPedidos.SelectedItems(0).SubItems(0).Text) And elemento.Attribute("Total").Value = frmListaPedidos.lsvBusquedaPedidos.SelectedItems(0).SubItems(1).Text.TrimEnd("€"c) Select elemento
        If PedidoSeleccionado.Count <> 1 Then 'Si no hay ningún pedido o más de uno, advertimos al usuario de un error y salimos
            MessageBox.Show("Se ha producido un error inesperado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End If
        DibujarProductos(PedidoSeleccionado.Descendants("Producto"), True) 'En caso contrario, dibujamos el pedido en el ListView
    End Sub

    Private Sub lsvPedidoAntiguo_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lsvPedidoAntiguo.ItemSelectionChanged
        If e.IsSelected Then e.Item.Selected = False 'Impedimos la selección de elementos del ListView
    End Sub

    Private Sub btnImprimirPedido_Click(sender As Object, e As EventArgs) Handles btnImprimirPedido.Click
        Dim DS As New DSPedido
        Dim Tabla As New DataTable("DSPedido_Tabla")
        Dim Columna As DataColumn
        Dim Fila As DataRow

        Columna = New DataColumn
        Columna.DataType = System.Type.GetType("System.String")
        Columna.ColumnName = "Cantidad"
        Tabla.Columns.Add(Columna)

        Columna = New DataColumn
        Columna.DataType = System.Type.GetType("System.String")
        Columna.ColumnName = "Descripción"
        Tabla.Columns.Add(Columna)

        Columna = New DataColumn
        Columna.DataType = System.Type.GetType("System.String")
        Columna.ColumnName = "PVP"
        Tabla.Columns.Add(Columna)

        Columna = New DataColumn
        Columna.DataType = System.Type.GetType("System.String")
        Columna.ColumnName = "Importe"
        Tabla.Columns.Add(Columna)

        For Each elemento As ListViewItem In lsvPedidoAntiguo.Items
            Fila = Tabla.NewRow()
            Fila("Cantidad") = elemento.SubItems(0).Text
            Fila("Descripción") = elemento.SubItems(1).Text
            Fila("PVP") = elemento.SubItems(2).Text
            Fila("Importe") = elemento.SubItems(3).Text
            Tabla.Rows.Add(Fila)
        Next

        plantillaPedido1.Database.Tables("DSPedido_Tabla").SetDataSource(Tabla)
        plantillaPedido1.PrintToPrinter(1, False, 0, 0)
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
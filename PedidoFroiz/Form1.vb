Imports System.IO
Imports System.Xml

Public Class frmPedidoFroiz
#Region "Constantes"
    Public Const IDENTIFICADOR_PRECIO_INICIO = "data-price="
    Public Const IDENTIFICADOR_PRECIO_FINAL = Chr(34)
    Public Const IDENTIFICADOR_NOMBRE_INICIO = "dproducto " & Chr(34) & ">"
    Public Const IDENTIFICADOR_NOMBRE_FINAL = "  "
    Public Const URL_BUSQUEDA = "https://www.froiz.com/shop/search.php?q="
#End Region

    Public docXMLProductos, docXMLPedidos As XElement
    Public itemsHistoricoProductos, itemsPedido As ListViewItem
    Public RutaMisPedidos = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\Mis Pedidos"

    Private Sub frmPedidoFroiz_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Comprobamos si existe el directorio Mis Pedidos en Mis Documentos
        If Not Directory.Exists(RutaMisPedidos) Then
            Directory.CreateDirectory(RutaMisPedidos)
        End If

        'Y los ficheros xml necesarios para el programa
        If Not File.Exists(RutaMisPedidos & "\Productos.xml") Then
            Dim Elemento As New XElement("Productos")
            Elemento.Save(RutaMisPedidos & "\Productos.xml")
        End If
        docXMLProductos = XElement.Load(RutaMisPedidos & "\Productos.xml")
        DibujarHistoricoProductos(docXMLProductos, True)

        If Not File.Exists(RutaMisPedidos & "\Pedidos.xml") Then
            Dim Elemento As New XElement("Pedidos")
            Elemento.Save(RutaMisPedidos & "\Pedidos.xml")
        End If
        docXMLPedidos = XElement.Load(RutaMisPedidos & "\Pedidos.xml")

    End Sub

    Private Sub frmPedidoFroiz_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Grabamos los ficheros en el disco antes de salir del programa
        docXMLProductos.Save(RutaMisPedidos & "\Productos.xml")
        docXMLPedidos.Save(RutaMisPedidos & "\Pedidos.xml")
    End Sub

    Private Sub btnNuevoProducto_Click(sender As Object, e As EventArgs) Handles btnNuevoProducto.Click
        frmBuscarProductos.ShowDialog()
    End Sub

    Private Sub btnEliminarDelHistorico_Click(sender As Object, e As EventArgs) Handles btnEliminarDelHistorico.Click
        Dim itemsSeleccionados As ListView.SelectedListViewItemCollection = lsvHistoricoProductos.SelectedItems 'Creamos una colección con los elementos seleccionados en el ListView
        Dim item As ListViewItem 'Esta variable la necesitamos para el bucle For Each
        Dim ListaNombresProductos As IEnumerable(Of XElement) = docXMLProductos.Descendants("Producto") '

        If itemsSeleccionados.Count > 0 Then 'Si hay algún elemento seleccionado...
            For Each item In itemsSeleccionados 'Recorremos la colección...
                lsvHistoricoProductos.Items.Remove(item) 'Lo borramos del ListView...

                Dim ResultadoLINQ = From elemento In ListaNombresProductos Where elemento.Element("Nombre").Value = item.SubItems(0).Text And elemento.Element("Precio").Value = item.SubItems(1).Text.TrimEnd("€").Replace(",", ".") Select elemento
                ResultadoLINQ.Remove() 'Y lo borramos del xml haciendo una consulta LINQ que nos dé como resultado el producto que queremos borrar
            Next
            DibujarHistoricoProductos(docXMLProductos, True) 'Redibujamos la lista de productos
        End If
    End Sub

    Private Sub btnAnhadirAPedido_Click(sender As Object, e As EventArgs) Handles btnAnhadirAPedido.Click
        Dim itemsSeleccionados As ListView.SelectedListViewItemCollection = lsvHistoricoProductos.SelectedItems 'Creamos una colección con los elementos seleccionados en el ListView
        Dim nuevoItem, itemRepetido As ListViewItem 'Declaramos una variable de tipo ListViewItem para poder volcar la información de la colección de seleccionados sin que dé errores y otra para buscar productos repetidos
        Dim itemProductosPedido = New ListViewItem 'Esta variable la necesitamos para el bucle For Each. Reutilizamos la que empleamos en el procedimento de búsqueda
        Dim n As Integer
        Dim Total As Decimal = 0

        If itemsSeleccionados.Count > 0 Then 'Si hay algún elemento seleccionado...
            If tbxTotal.Text.Length > 0 Then Total = CDec(tbxTotal.Text.TrimEnd("€")) 'Si hay total, ponlo en la variable
            n = lsvPedido.Items.Count 'Asignamos al índice la posición del último elemento del histórico de productos para continuar a partir de ahí
            For Each itemProductosPedido In itemsSeleccionados
                nuevoItem = New ListViewItem 'Reinicializamos la variable llamando a su constructor
                If n Mod 2 Then
                    nuevoItem.BackColor = Color.Honeydew 'Si es impar, este color de fondo...
                Else
                    nuevoItem.BackColor = Color.White 'Si es par, blanco
                End If
                nuevoItem.SubItems(0).Text = "1" 'Ponemos una unidad del producto por defecto...
                nuevoItem.SubItems.Add(itemProductosPedido.SubItems(0).Text) 'Ponemos el nombre del producto...
                nuevoItem.SubItems.Add(itemProductosPedido.SubItems(1).Text) 'Su precio, que ya incluye el símbolo del euro...
                nuevoItem.SubItems.Add(nuevoItem.SubItems(2).Text) 'Y el precio*unidades, también con el euro (por ahora es el mismo que el precio de 1 ud.)
                itemRepetido = lsvPedido.FindItemWithText(nuevoItem.SubItems(1).Text) 'Buscamos si el producto ya está en la lista
                If itemRepetido IsNot Nothing Then 'Si el producto ya está en el pedido...
                    If MessageBox.Show("El producto " & Chr(34) & nuevoItem.SubItems(1).Text & Chr(34) & " ya está en el pedido, ¿Desea añadir una unidad?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then 'Preguntamos si queremos añadir una unidad
                        itemRepetido.SubItems(0).Text = CDec(itemRepetido.SubItems(0).Text) + 1 'Le sumamos 1 a las unidades
                        Total -= CDec(itemRepetido.SubItems(3).Text.TrimEnd("€")) 'Restamos del total el precio de la línea
                        itemRepetido.SubItems(3).Text = (CDec(itemRepetido.SubItems(3).Text.TrimEnd("€")) + CDec(nuevoItem.SubItems(2).Text.TrimEnd("€"))).ToString & "€" 'Sumamos al precio de la línea el precio de 1 unidad del producto
                        Total += CDec(itemRepetido.SubItems(3).Text.TrimEnd("€")) 'Y recalculamos el total
                    End If
                Else 'Si el producto no está ya en el pedido...
                    lsvPedido.Items.Add(nuevoItem) 'Lo añadimos a la lista...
                    Total += CDec(nuevoItem.SubItems(3).Text.TrimEnd("€")) 'Y calculamos el nuevo total
                    n += 1 'Sumamos 1 al índice de posición
                End If
            Next
            tbxTotal.Text = Total.ToString & "€" 'Ponemos el total en su TextBox...
            lsvHistoricoProductos.SelectedItems.Clear() 'Y desmarcamos la selección
        End If
    End Sub

    Private Sub lsvPedido_AfterLabelEdit(sender As Object, e As LabelEditEventArgs) Handles lsvPedido.AfterLabelEdit
        Dim Total As Decimal = 0
        Dim Unidad As Integer = 0
        Dim Etiqueta As String

        If e.Label IsNot Nothing Then 'Comprobamos que no sea nulo porque nos puede generar una excepción si cambiamos de campo mientras están seleccionadas las unidades
            Etiqueta = e.Label.Replace(".", ",") 'Reemplazamos el punto por la coma decimal

            If tbxTotal.Text.Length > 0 Then Total = CDec(tbxTotal.Text.TrimEnd("€")) 'Si hay total, ponlo en la variable
            If IsNumeric(Etiqueta) Then 'Si introdujimos un número...
                Unidad = CInt(Etiqueta) 'Guardamos la unidad en una variable entera para eliminar los decimales
                If Unidad > 0 And CDec(Etiqueta) = Unidad Then 'Si las unidades son al menos 1 y, además, no tienen decimales...
                    lsvPedido.Items(e.Item).SubItems(0).Text = Unidad.ToString 'Reasignamos las unidades a su campo de texto en el ListView sin decimales
                    Total -= CDec(lsvPedido.Items(e.Item).SubItems(3).Text.TrimEnd("€")) 'Restamos del total lo que había en esa línea
                    lsvPedido.Items(e.Item).SubItems(3).Text = (Unidad * CDec(lsvPedido.Items(e.Item).SubItems(2).Text.TrimEnd("€"))).ToString & "€" 'Y multiplicamos el precio/unidad por las nuevas unidades introducidas guardándolo en su campo de texto
                    Total += CDec(lsvPedido.Items(e.Item).SubItems(3).Text.TrimEnd("€")) 'Recalculamos el total...
                    tbxTotal.Text = Total.ToString & "€" 'Y lo ponemos en el TextBox correspondiente
                Else
                    MessageBox.Show("El número de unidades es menor que 1 o contiene decimales", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    e.CancelEdit = True 'Si las unidades son menos que 1 o tienen decimales, cancelamos
                End If
            Else
                MessageBox.Show("Las unidades sólo pueden contener números", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                e.CancelEdit = True 'Si las unidades no son un número, cancelamos
            End If
        End If
    End Sub

    Private Sub btnEliminarDePedido_Click(sender As Object, e As EventArgs) Handles btnEliminarDePedido.Click
        Dim Total As Decimal = 0
        Dim item As ListViewItem

        If tbxTotal.Text.Length > 0 Then Total = CDec(tbxTotal.Text.TrimEnd("€")) 'Si hay total, lo asignamos a su variable
        If lsvPedido.SelectedItems.Count > 0 Then 'Si hemos seleccionado algún producto para borrar...
            For Each item In lsvPedido.SelectedItems 'Recorremos la colección de elementos seleccionados...
                Total -= CDec(item.SubItems(3).Text.TrimEnd("€")) 'Descontamos su precio total del general...
                lsvPedido.Items.Remove(item) 'Y borramos el elemento del ListView de pedidos
            Next
            If Total > 0 Then 'Si el total es mayor que cero, lo asignamos al texto del botón y le añadimos el símbolo del euro
                tbxTotal.Text = Total.ToString & "€"
            Else
                tbxTotal.Text = "" 'Si es cero significa que no hay productos en el pedido y, por tanto, borramos el texto del total
            End If
        End If
    End Sub

    Private Sub btnConfirmarPedido_Click(sender As Object, e As EventArgs) Handles btnConfirmarPedido.Click
        If lsvPedido.Items.Count > 0 Then 'Si hay productos en el pedido...
            If MessageBox.Show("¿Deseas añadir este pedido?", "CONFIRMACIÓN DEL PEDIDO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then 'Preguntamos al usuario si desea guardar el pedido
                AnhadirXMLPedidos(lsvPedido.Items) 'Llamamos al procedimiento que añade el pedido al xml...
                lsvPedido.Items.Clear() 'Borramos la lista...
                tbxTotal.Text = "" 'Y el total
            End If
        End If
    End Sub

    Private Sub btnBorrarPedido_Click(sender As Object, e As EventArgs) Handles btnBorrarPedido.Click

    End Sub

    Private Sub btnVerPedidos_Click(sender As Object, e As EventArgs) Handles btnVerPedidos.Click
        frmListaPedidos.ShowDialog()
        If frmListaPedidos.DialogResult = DialogResult.OK Then
            frmPedidoAntiguo.ShowDialog()
        End If
    End Sub

#Region "Funciones auxiliares"
    ''' <summary>
    ''' Rellena el ListView del Histórico de Productos
    ''' </summary>
    ''' <param name="docXML">El xml con los productos</param>
    ''' <param name="BorrarListView">(Opcional) Verdadero para borrar el ListView</param>
    Public Sub DibujarHistoricoProductos(ByVal docXML As XElement, Optional ByVal BorrarListView As Boolean = False)
        Dim Productos As New List(Of Producto) 'Creamos una lista de objetos de la clase Producto para poder 
        Dim itemsHistoricoProductos As ListViewItem
        Dim n As Integer = 0
        Dim ListaProductos As IEnumerable(Of XElement) = docXMLProductos.Descendants("Producto") 'Llenamos una lista con todos los productos del xml

        If BorrarListView Then lsvHistoricoProductos.Items.Clear() 'Si el argumento opcional es True, borramos la lista

        For Each elemento In ListaProductos 'Recorremos la lista y los añadimos a la lista de objetos Producto
            Productos.Add(New Producto With {.Nombre = elemento.Element("Nombre").Value, .Precio = CDec(elemento.Element("Precio").Value.Replace(".", ","))})
        Next

        For Each Producto In Productos
            itemsHistoricoProductos = New ListViewItem
            If n Mod 2 Then
                itemsHistoricoProductos.BackColor = Color.Honeydew
            Else
                itemsHistoricoProductos.BackColor = Color.White
            End If
            itemsHistoricoProductos.SubItems(0).Text = Producto.Nombre
            itemsHistoricoProductos.SubItems.Add(Producto.Precio.ToString & "€")
            lsvHistoricoProductos.Items.Add(itemsHistoricoProductos)
            n += 1
        Next
    End Sub

    ''' <summary>
    ''' Devuelve una cadena con el contenido de una página web
    ''' </summary>
    ''' <param name="Url">La dirección de la página web (incluyendo http://)</param>
    ''' <returns>La cadena con el volcado de la página web</returns>
    Public Function RetHTML(ByVal Url As String) As String
        Dim WR As System.Net.WebRequest
        Dim Rsp As System.Net.WebResponse
        WR = System.Net.WebRequest.Create(Url)
        Rsp = WR.GetResponse()
        Return New StreamReader(Rsp.GetResponseStream()).ReadToEnd()
    End Function

    ''' <summary>
    ''' Devuelve una lista de productos contenidos en una cadena volcada de una página web
    ''' </summary>
    ''' <param name="HTML">La cadena con la página web volcada</param>
    ''' <returns>La lista de productos, con su nombre y su precio</returns>
    Public Function RetProductos(ByVal HTML As String) As List(Of Producto)
        Dim listaProductos As New List(Of Producto)
        Dim IndexINI As Integer = 0
        Dim IndexFIN As Integer = 0
        Dim sPrecio As String
        Dim Precio As Decimal
        Dim sNombre As String

        Do
            IndexINI = HTML.IndexOf(IDENTIFICADOR_PRECIO_INICIO, IndexINI) 'Miramos la posición de la primera aparición del precio
            If IndexINI < 0 Then 'Si el índice es -1 significa que no se encontró nada y salimos
                Exit Do
            Else
                IndexINI += IDENTIFICADOR_PRECIO_INICIO.Length + 1 'Si encuentra un precio suma la longitud del identificador
                'a la posición para quedar en el sitio justo
            End If

            IndexFIN = HTML.IndexOf(IDENTIFICADOR_PRECIO_FINAL, IndexINI) 'Buscamos la posición final del precio
            sPrecio = HTML.Substring(IndexINI, IndexFIN - IndexINI) 'Generamos la subcadena con las posiciones correctas
            Precio = CDec(sPrecio.Replace(".", ",")) 'Convertimos en decimal la cadena y sustituímos el punto por la coma decimal

            IndexINI = HTML.IndexOf(IDENTIFICADOR_NOMBRE_INICIO, IndexFIN) + IDENTIFICADOR_NOMBRE_INICIO.Length 'Lo mismo con el nombre del producto
            IndexFIN = HTML.IndexOf(IDENTIFICADOR_NOMBRE_FINAL, IndexINI)
            sNombre = HTML.Substring(IndexINI, IndexFIN - IndexINI)

            listaProductos.Add(New Producto() With {.Nombre = sNombre, .Precio = Precio}) 'Añadimos el producto a la lista
        Loop

        Return listaProductos

    End Function

    ''' <summary>
    ''' Añade un pedido a Pedidos.xml, con la fecha y el total como atributos
    ''' </summary>
    ''' <param name="listaProductos">La lista de productos a añadir</param>
    Public Sub AnhadirXMLPedidos(ByVal listaProductos As ListView.ListViewItemCollection)
        Dim Pedido As XElement
        Dim Fecha, Total As XAttribute
        Dim Producto As ListViewItem
        Dim ProductoPedido As XElement

        Pedido = New XElement("Pedido")
        Fecha = New XAttribute("Fecha", Date.Today)
        Total = New XAttribute("Total", tbxTotal.Text.TrimEnd("€"))
        Pedido.Add(Fecha)
        Pedido.Add(Total)
        For Each Producto In listaProductos
            ProductoPedido = New XElement("Producto", New XElement("Unidades", Producto.SubItems(0).Text), New XElement("Nombre", Producto.SubItems(1).Text), New XElement("PrecioUd", Producto.SubItems(2).Text.TrimEnd("€")), New XElement("Precio", Producto.SubItems(3).Text.TrimEnd("€")))
            Pedido.Add(ProductoPedido)
        Next
        docXMLPedidos.Add(Pedido)
    End Sub

    ''' <summary>
    ''' Añade una lista de productos a Productos.xml
    ''' </summary>
    ''' <param name="listaProductos">La lista de productos que se van a añadir</param>
    ''' <returns>Verdadero si los productos no existen en el xml y se pueden añadir, falso en caso contrario</returns>
    Public Overloads Function AnhadirXMLProductos(ByVal listaProductos As List(Of Producto)) As Boolean
        Dim ListaNombresProductos As IEnumerable(Of XElement) = docXMLProductos.Descendants("Producto").Elements("Nombre")
        Dim resultado As Boolean = True

        For Each Producto In listaProductos
            Dim ResultadoLINQ = From elemento In ListaNombresProductos Where elemento.Value = Producto.Nombre Select elemento
            If ResultadoLINQ.Count = 0 Then
                docXMLProductos.Add(New XElement("Producto", New XElement("Nombre", Producto.Nombre), New XElement("Precio", Producto.Precio)))
            Else
                MessageBox.Show("El producto " & Chr(34) & Producto.Nombre & Chr(34) & " ya se encuentra en la lista", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                resultado = False
            End If
        Next

        Return resultado
    End Function

    ''' <summary>
    ''' Añade un único producto a Productos.xml
    ''' </summary>
    ''' <param name="NuevoProducto">El producto que se va a añadir</param>
    ''' <returns>Verdadero si el producto no existe en el xml y se puede añadir, falso en caso contrario</returns>
    Public Overloads Function AnhadirXMLProductos(ByVal NuevoProducto As Producto) As Boolean
        Dim ListaNombresProductos As IEnumerable(Of XElement) = docXMLProductos.Descendants("Producto").Elements("Nombre")
        Dim resultado As Boolean = True

        Dim ResultadoLINQ = From elemento In ListaNombresProductos Where elemento.Value = NuevoProducto.Nombre Select elemento
        If ResultadoLINQ.Count = 0 Then
            docXMLProductos.Add(New XElement("Producto", New XElement("Nombre", NuevoProducto.Nombre), New XElement("Precio", NuevoProducto.Precio)))
        Else
            MessageBox.Show("El producto " & Chr(34) & NuevoProducto.Nombre & Chr(34) & " ya se encuentra en la lista", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            resultado = False
        End If

        Return resultado
    End Function

#End Region
End Class

''' <summary>
''' La clase Producto contiene el nombre y el precio de los productos
''' </summary>
Public Class Producto
    Public Property Nombre As String
    Public Property Precio As Decimal
End Class

Option Strict On
Option Explicit Off

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
    Public RutaMisPedidos As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\Mis Pedidos"

    Private Sub frmPedidoFroiz_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Comprobamos si existe el directorio Mis Pedidos en Mis Documentos...
        If Not Directory.Exists(RutaMisPedidos) Then
            Directory.CreateDirectory(RutaMisPedidos)
        End If

        'Y los ficheros xml necesarios para el programa
        If Not File.Exists(RutaMisPedidos & "\Productos.xml") Then
            Dim Elemento As New XElement("Productos")
            Elemento.Save(RutaMisPedidos & "\Productos.xml")
        End If
        docXMLProductos = XElement.Load(RutaMisPedidos & "\Productos.xml")
        DibujarHistoricoProductos(docXMLProductos.Descendants("Producto"), True)

        If Not File.Exists(RutaMisPedidos & "\Pedidos.xml") Then
            Dim Elemento As New XElement("Pedidos")
            Elemento.Save(RutaMisPedidos & "\Pedidos.xml")
        End If
        docXMLPedidos = XElement.Load(RutaMisPedidos & "\Pedidos.xml")

        'Asignamos el ancho de las columnas de los ListView para que no aparezca una barra de scroll horizontal (queda mal estéticamente)
        lsvHistoricoProductos.Columns.Item(0).Width = 260
        lsvHistoricoProductos.Columns.Item(1).Width = 44

        lsvPedido.Columns.Item(0).Width = 38
        lsvPedido.Columns.Item(1).Width = 230
        lsvPedido.Columns.Item(2).Width = 45
        lsvPedido.Columns.Item(3).Width = 52
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
        Dim ListaNombresProductos As IEnumerable(Of XElement) = docXMLProductos.Descendants("Producto") 'Rellenamos una lista con los nodos "Producto" del xml

        If lsvHistoricoProductos.Items.Count = 0 Then 'Si no hay productos, avisamos al usuario y salimos
            MessageBox.Show("La lista de productos está vacía", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If itemsSeleccionados.Count > 0 Then 'Si hay algún elemento seleccionado...
            For Each item In itemsSeleccionados 'Recorremos la colección...
                lsvHistoricoProductos.Items.Remove(item) 'Lo borramos del ListView...

                Dim ResultadoLINQ = From elemento In ListaNombresProductos Where elemento.Element("Nombre").Value = item.SubItems(0).Text And elemento.Element("Precio").Value = item.SubItems(1).Text.TrimEnd("€"c) Select elemento
                ResultadoLINQ.Remove() 'Y lo borramos del xml haciendo una consulta LINQ que nos dé como resultado el producto que queremos borrar
            Next
            DibujarHistoricoProductos(docXMLProductos.Descendants("Producto"), True) 'Redibujamos la lista de productos
        Else
            MessageBox.Show("No se ha seleccionado ningún producto", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning) 'Si hay productos, pero no hemos seleccionado ninguno, avisamos al usuario y nos vamos
        End If
    End Sub

    Private Sub btnActualizarPrecios_Click(sender As Object, e As EventArgs) Handles btnActualizarPrecios.Click
        Dim Productos As List(Of ListViewItem) 'Declaramos una lista de ListViewItems para almacenar los productos de la web...
        Dim Producto, elemento As ListViewItem 'Y dos variables ListViewItem; una para recorrer el control ListView y otra para recorrer la lista de productos
        Dim sResultadoHTML As String = "" 'Declaramos una cadena para guardar la página web
        Dim Encontrado As Boolean = False 'Encontrado es el flag que nos indicará si el producto se encuentra en los resultados de la búsqueda web

        If lsvHistoricoProductos.Items.Count > 0 Then 'Primer control: ¿Hay elementos en la lista de productos? Si los hay, entonces...
            If lsvHistoricoProductos.SelectedItems.Count > 0 Then 'Segundo control: ¿Hay alguno seleccionado? (en ese caso, sólo se actualizarán los precios de los seleccionados)
                For Each elemento In lsvHistoricoProductos.SelectedItems 'Recorremos la lista de seleccionados...
                    sResultadoHTML = RetHTML(URL_BUSQUEDA & elemento.SubItems(0).Text) 'Buscamos cada producto en la web...
                    Productos = RetProductos(sResultadoHTML) 'Y los guardamos en la lista Productos (puede haber más de un producto debido al funcionamiento de la web de Froiz)
                    For Each Producto In Productos 'Recorremos la lista Productos...
                        If Producto.SubItems(0).Text = elemento.SubItems(0).Text Then 'Y en caso de que el nombre sea igual al que buscamos...
                            elemento.SubItems(1).Text = Producto.SubItems(1).Text 'Asignamos el precio que aparece en la web al campo precio del producto...
                            Encontrado = True 'Y activamos el flag Encontrado
                        Else 'Si el nombre es distinto...
                            If Not Encontrado Then 'Y el producto no se ha encontrado aún, avisamos al usuario
                                MessageBox.Show("El producto " & Chr(34) & elemento.SubItems(0).Text & Chr(34) & " no se encuentra y no se puede actualizar su precio", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End If
                        End If
                    Next
                Next
                AnhadirXMLProductos(lsvHistoricoProductos.SelectedItems, True) 'Sobreescribimos el producto con su precio actualizado en el xml...
                lsvHistoricoProductos.SelectedItems.Clear() 'Y borramos la lista de elementos seleccionados del control ListView
            Else '[Segundo control] Si no hay ningún elemento seleccionado quiere decir que actualizamos el precio de toda la lista
                For Each elemento In lsvHistoricoProductos.Items 'Recorremos toda la lista...
                    sResultadoHTML = RetHTML(URL_BUSQUEDA & elemento.SubItems(0).Text) 'Buscamos cada producto en la web...
                    Productos = RetProductos(sResultadoHTML) 'Etc, etc...
                    For Each Producto In Productos
                        If Producto.SubItems(0).Text = elemento.SubItems(0).Text Then
                            elemento.SubItems(1).Text = Producto.SubItems(1).Text
                            Encontrado = True
                        Else
                            If Not Encontrado Then
                                MessageBox.Show("El producto " & Chr(34) & elemento.SubItems(0).Text & Chr(34) & " no se encuentra y no se puede actualizar su precio", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End If
                        End If
                    Next
                Next
                AnhadirXMLProductos(lsvHistoricoProductos.Items, True)
            End If
        Else '[Primer control] Si no hay elementos en la lista de productos, avisamos al usuario y salimos
            MessageBox.Show("La lista de productos está vacía", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnAnhadirAPedido_Click(sender As Object, e As EventArgs) Handles btnAnhadirAPedido.Click
        Dim itemsSeleccionados As ListView.SelectedListViewItemCollection = lsvHistoricoProductos.SelectedItems 'Creamos una colección con los elementos seleccionados en el ListView
        Dim nuevoItem, itemRepetido As ListViewItem 'Declaramos dos variables de tipo ListViewItem. Una para poder volcar la información de la colección de seleccionados sin que dé errores y otra para buscar productos repetidos
        Dim itemProductosPedido = New ListViewItem 'Esta variable la necesitamos para el bucle For Each. Reutilizamos la que empleamos en el procedimento de búsqueda
        Dim n As Integer
        Dim Total As Decimal = 0

        If itemsSeleccionados.Count > 0 Then 'Si hay algún elemento seleccionado...
            If tbxTotal.Text.Length > 0 Then Total = CDec(tbxTotal.Text.TrimEnd("€"c)) 'Si hay total, ponlo en la variable
            n = lsvPedido.Items.Count 'Asignamos al índice la posición del último elemento del histórico de productos para continuar a partir de ahí
            For Each itemProductosPedido In itemsSeleccionados
                nuevoItem = New ListViewItem 'Reinicializamos la variable llamando a su constructor
                If n Mod 2 <> 0 Then
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
                    If itemRepetido.SubItems(2).Text <> nuevoItem.SubItems(2).Text Then
                        If MessageBox.Show("El producto " & Chr(34) & nuevoItem.SubItems(1).Text & Chr(34) & " ya está en el pedido y su precio ha cambiado, ¿Desea añadir una unidad y actualizar el precio?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                            itemRepetido.SubItems(0).Text = (CDec(itemRepetido.SubItems(0).Text) + 1).ToString 'Le sumamos 1 a las unidades
                            Total -= CDec(itemRepetido.SubItems(3).Text.TrimEnd("€"c)) 'Restamos del total el precio de la línea
                            itemRepetido.SubItems(2).Text = nuevoItem.SubItems(2).Text 'Actualizamos el PVP...
                            itemRepetido.SubItems(3).Text = (CDec(itemRepetido.SubItems(0).Text.TrimEnd("€"c)) * CDec(nuevoItem.SubItems(2).Text.TrimEnd("€"c))).ToString & "€" 'Multiplicamos las unidades por su PVP...
                            Total += CDec(itemRepetido.SubItems(3).Text.TrimEnd("€"c)) 'Recalculamos el total...
                        End If
                        Continue For 'Y pasamos a la siguiente iteración del bucle
                    End If
                    If MessageBox.Show("El producto " & Chr(34) & nuevoItem.SubItems(1).Text & Chr(34) & " ya está en el pedido, ¿Desea añadir una unidad?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then 'Preguntamos si queremos añadir una unidad
                        itemRepetido.SubItems(0).Text = (CDec(itemRepetido.SubItems(0).Text) + 1).ToString 'Le sumamos 1 a las unidades
                        Total -= CDec(itemRepetido.SubItems(3).Text.TrimEnd("€"c)) 'Restamos del total el precio de la línea
                        itemRepetido.SubItems(3).Text = (CDec(itemRepetido.SubItems(3).Text.TrimEnd("€"c)) + CDec(nuevoItem.SubItems(2).Text.TrimEnd("€"c))).ToString & "€" 'Sumamos al precio de la línea el precio de 1 unidad del producto
                        Total += CDec(itemRepetido.SubItems(3).Text.TrimEnd("€"c)) 'Y recalculamos el total
                    End If
                Else 'Si el producto no está en el pedido...
                    lsvPedido.Items.Add(nuevoItem) 'Lo añadimos a la lista...
                    Total += CDec(nuevoItem.SubItems(3).Text.TrimEnd("€"c)) 'Y calculamos el nuevo total
                    n += 1 'Sumamos 1 al índice de posición
                End If
            Next
            tbxTotal.Text = Total.ToString & "€" 'Ponemos el total en su TextBox...
            lsvHistoricoProductos.SelectedItems.Clear() 'Y desmarcamos la selección
        Else
            MessageBox.Show("No se ha seleccionado ningún producto", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub lsvPedido_AfterLabelEdit(sender As Object, e As LabelEditEventArgs) Handles lsvPedido.AfterLabelEdit
        Dim Total As Decimal = 0
        Dim Unidad As Integer = 0
        Dim Etiqueta As String

        If e.Label IsNot Nothing Then 'Comprobamos que no sea nulo porque nos puede generar una excepción si cambiamos de campo mientras están seleccionadas las unidades
            Etiqueta = e.Label.Replace(".", ",") 'Reemplazamos el punto por la coma decimal

            If tbxTotal.Text.Length > 0 Then Total = CDec(tbxTotal.Text.TrimEnd("€"c)) 'Si hay total, ponlo en la variable
            If IsNumeric(Etiqueta) Then 'Si introdujimos un número...
                Unidad = CInt(Etiqueta) 'Guardamos la unidad en una variable entera para eliminar los decimales
                If Unidad > 0 And CDec(Etiqueta) = Unidad Then 'Si las unidades son al menos 1 y, además, no tienen decimales...
                    lsvPedido.Items(e.Item).SubItems(0).Text = Unidad.ToString 'Reasignamos las unidades a su campo de texto en el ListView sin decimales
                    Total -= CDec(lsvPedido.Items(e.Item).SubItems(3).Text.TrimEnd("€"c)) 'Restamos del total lo que había en esa línea
                    lsvPedido.Items(e.Item).SubItems(3).Text = (Unidad * CDec(lsvPedido.Items(e.Item).SubItems(2).Text.TrimEnd("€"c))).ToString & "€" 'Y multiplicamos el precio/unidad por las nuevas unidades introducidas guardándolo en su campo de texto
                    Total += CDec(lsvPedido.Items(e.Item).SubItems(3).Text.TrimEnd("€"c)) 'Recalculamos el total...
                    tbxTotal.Text = Total.ToString & "€" 'Y lo ponemos en el TextBox correspondiente
                Else
                    MessageBox.Show("La cantidad es menor que 1 o contiene decimales", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    e.CancelEdit = True 'Si las unidades son menos que 1 o tienen decimales, cancelamos
                End If
            Else
                MessageBox.Show("La cantidad sólo puede contener caracteres numéricos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                e.CancelEdit = True 'Si las unidades no son un número, cancelamos
            End If
        End If
    End Sub

    Private Sub btnEliminarDePedido_Click(sender As Object, e As EventArgs) Handles btnEliminarDePedido.Click
        Dim Total As Decimal = 0
        Dim item As ListViewItem

        If tbxTotal.Text.Length > 0 Then Total = CDec(tbxTotal.Text.TrimEnd("€"c)) 'Si hay total, lo asignamos a su variable
        If lsvPedido.SelectedItems.Count > 0 Then 'Si hemos seleccionado algún producto para borrar...
            For Each item In lsvPedido.SelectedItems 'Recorremos la colección de elementos seleccionados...
                Total -= CDec(item.SubItems(3).Text.TrimEnd("€"c)) 'Descontamos su precio total del general...
                lsvPedido.Items.Remove(item) 'Y borramos el elemento del ListView de pedidos
            Next
            If Total > 0 Then 'Si el total es mayor que cero, lo asignamos al texto del botón y le añadimos el símbolo del euro
                tbxTotal.Text = Total.ToString & "€"
            Else
                tbxTotal.Text = "" 'Si es cero significa que no hay productos en el pedido y, por tanto, borramos el texto del total
            End If
        Else
            MessageBox.Show("No se ha seleccionado ningún producto", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnConfirmarPedido_Click(sender As Object, e As EventArgs) Handles btnConfirmarPedido.Click
        If lsvPedido.Items.Count > 0 Then 'Si hay productos en el pedido...
            If MessageBox.Show("¿Desea añadir este pedido?", "CONFIRMACIÓN DEL PEDIDO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then 'Preguntamos al usuario si desea guardar el pedido
                AnhadirXMLPedidos(lsvPedido.Items) 'Llamamos al procedimiento que añade el pedido al xml...
                lsvPedido.Items.Clear() 'Borramos la lista...
                tbxTotal.Text = "" 'Y el total
            End If
        End If
    End Sub

    Private Sub btnBorrarPedido_Click(sender As Object, e As EventArgs) Handles btnBorrarPedido.Click
        If lsvPedido.Items.Count > 0 Then
            If MessageBox.Show("¿Desea borrar el pedido?", "BORRADO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                lsvPedido.Items.Clear()
                tbxTotal.Text = ""
            End If
        End If
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
    ''' <param name="ListaProductos">El xml con los productos</param>
    ''' <param name="BorrarListView">(Opcional) Verdadero para borrar el ListView</param>
    Public Sub DibujarHistoricoProductos(ByVal ListaProductos As IEnumerable(Of XElement), Optional ByVal BorrarListView As Boolean = False)
        Dim itemsHistoricoProductos, item As ListViewItem 'Definimos una variable del tipo elemento del control ListView y su iterador
        Dim n As Integer = 0

        If BorrarListView Then lsvHistoricoProductos.Items.Clear() 'Si el argumento opcional es True, borramos la lista

        For Each elemento In ListaProductos 'Recorremos la lista y le añadimos los productos
            itemsHistoricoProductos = New ListViewItem 'Reinicializamos la clase con cada iteración para borrar sus datos
            itemsHistoricoProductos.SubItems(0).Text = elemento.Element("Nombre").Value 'Asignamos el nombre...
            itemsHistoricoProductos.SubItems.Add(elemento.Element("Precio").Value & "€") 'Y el precio...
            lsvHistoricoProductos.Items.Add(itemsHistoricoProductos) 'Y los añadimos al ListView
        Next
        lsvHistoricoProductos.Sorting = SortOrder.Ascending 'Forzamos la ordenación de la lista antes de colorear las línas para que queden bien

        For Each item In lsvHistoricoProductos.Items
            If n Mod 2 <> 0 Then 'Si es par dibujamos en un color, si es impar en el otro
                item.BackColor = Color.Honeydew
            Else
                item.BackColor = Color.White
            End If
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
    Public Function RetProductos(ByVal HTML As String) As List(Of ListViewItem)
        Dim listaProductos As New List(Of ListViewItem) 'Asignamos una lista de elementos ListViewItem para poder guardar los productos
        Dim IndexINI As Integer = 0
        Dim IndexFIN As Integer = 0
        Dim sPrecio, sNombre As String
        Dim Producto As ListViewItem 'Declaramos una variable de tipo ListViewItem para poder iterar con ella en la lista de productos

        Do
            IndexINI = HTML.IndexOf(IDENTIFICADOR_PRECIO_INICIO, IndexINI) 'Miramos la posición de la primera aparición del precio
            If IndexINI < 0 Then 'Si el índice es -1 significa que no se encontró nada y salimos
                Exit Do
            Else
                IndexINI += IDENTIFICADOR_PRECIO_INICIO.Length + 1 'Si encuentra un precio suma la longitud del identificador a la posición para quedar en el sitio justo
            End If

            IndexFIN = HTML.IndexOf(IDENTIFICADOR_PRECIO_FINAL, IndexINI) 'Buscamos la posición final del precio
            sPrecio = HTML.Substring(IndexINI, IndexFIN - IndexINI).Replace(".", ",") 'Generamos la subcadena con las posiciones correctas y sustituímos el punto por la coma decimal

            IndexINI = HTML.IndexOf(IDENTIFICADOR_NOMBRE_INICIO, IndexFIN) + IDENTIFICADOR_NOMBRE_INICIO.Length 'Lo mismo con el nombre del producto
            IndexFIN = HTML.IndexOf(IDENTIFICADOR_NOMBRE_FINAL, IndexINI)
            sNombre = HTML.Substring(IndexINI, IndexFIN - IndexINI)

            Producto = New ListViewItem
            Producto.SubItems(0).Text = sNombre 'Guardamos el nombre...
            Producto.SubItems.Add(sPrecio & "€") 'Y el precio con el símbolo del euro
            listaProductos.Add(Producto) 'Añadimos el producto a la lista
        Loop

        Return listaProductos

    End Function

    ''' <summary>
    ''' Añade un pedido a Pedidos.xml, con la fecha y el total como atributos
    ''' </summary>
    ''' <param name="listaProductos">La lista de productos a añadir</param>
    Public Sub AnhadirXMLPedidos(ByVal listaProductos As ListView.ListViewItemCollection)
        Dim Pedido As XElement 'El nodo "Pedido"
        Dim Fecha, Total As XAttribute 'Los atributos de "Pedido"
        Dim Producto As ListViewItem 'El iterador de la lista de productos
        Dim ProductoPedido As XElement 'El contenido del nodo "Pedido", es decir, Producto (Unidades, Nombre, PrecioUd, Precio)

        Pedido = New XElement("Pedido") 'Creamos un nodo "Pedido"...
        Fecha = New XAttribute("Fecha", Date.Today.ToShortDateString) 'Le asignamos el atributo "Fecha"...
        Total = New XAttribute("Total", tbxTotal.Text.TrimEnd("€"c)) 'Y el atributo "Total" (para facilitar futuras búsquedas)
        Pedido.Add(Fecha) 'Añadimos los dos atributos al nodo
        Pedido.Add(Total)
        For Each Producto In listaProductos 'Recorremos la lista de productos...
            ProductoPedido = New XElement("Producto", New XElement("Unidades", Producto.SubItems(0).Text), New XElement("Nombre", Producto.SubItems(1).Text), New XElement("PrecioUd", Producto.SubItems(2).Text.TrimEnd("€"c)), New XElement("Precio", Producto.SubItems(3).Text.TrimEnd("€"c))) 'Y creamos sus nodos hijos para cada elemento
            Pedido.Add(ProductoPedido) 'Los añadimos al nodo "Pedido"...
        Next
        docXMLPedidos.Add(Pedido) 'Y este, a su vez, al documento xml
    End Sub

    ''' <summary>
    ''' Añade una lista de productos a Productos.xml
    ''' </summary>
    ''' <param name="listaProductos">La lista de productos que se van a añadir</param>
    ''' <param name="Sobreescribir">(Opcional) Si es True y el producto ya existe, lo sobreescribe</param>
    ''' <returns>Verdadero si los productos no existen en el xml y se pueden añadir, falso en caso contrario</returns>
    Public Function AnhadirXMLProductos(ByVal listaProductos As ListView.SelectedListViewItemCollection, Optional ByVal Sobreescribir As Boolean = False) As Boolean
        Dim ListaNombresProductos As IEnumerable(Of XElement) = docXMLProductos.Descendants("Producto") 'Creamos una lista con todos los productos del xml
        Dim Producto As ListViewItem 'Declaramos el iterador que recorrerá la lista
        Dim resultado As Boolean = True
        Dim ResultadoLINQ As IEnumerable(Of XElement)

        For Each Producto In listaProductos
            ResultadoLINQ = From elemento In ListaNombresProductos Where elemento.Element("Nombre").Value = Producto.SubItems(0).Text Select elemento 'Consultamos si el producto está en la lista
            If ResultadoLINQ.Count = 0 Then 'Si no está, lo añadimos al xml
                docXMLProductos.Add(New XElement("Producto", New XElement("Nombre", Producto.SubItems(0).Text), New XElement("Precio", Producto.SubItems(1).Text.TrimEnd("€"c))))
            Else 'Si está...
                If Sobreescribir And ResultadoLINQ.Count = 1 Then 'Si el flag de sobreescritura está activo y sólo hay un producto que coincida, sobreescribimos el precio...
                    ResultadoLINQ.Single.Element("Precio").Value = Producto.SubItems(1).Text.TrimEnd("€"c)
                Else
                    'En caso contrario, se lo advertimos al usuario y asignamos False al resultado para indincárselo también al programa
                    MessageBox.Show("El producto " & Chr(34) & Producto.SubItems(0).Text & Chr(34) & " ya se encuentra en la lista", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    resultado = False
                End If
            End If
        Next

        Return resultado
    End Function

    ''' <summary>
    ''' Añade una lista de productos a Productos.xml
    ''' </summary>
    ''' <param name="listaProductos">La lista de productos que se van a añadir</param>
    ''' <param name="Sobreescribir">(Opcional) Si es True y el producto ya existe, lo sobreescribe</param>
    ''' <returns>Verdadero si los productos no existen en el xml y se pueden añadir, falso en caso contrario</returns>
    Public Function AnhadirXMLProductos(ByVal listaProductos As ListView.ListViewItemCollection, Optional ByVal Sobreescribir As Boolean = False) As Boolean
        Dim ListaNombresProductos As IEnumerable(Of XElement) = docXMLProductos.Descendants("Producto") 'Creamos una lista con todos los nombres de productos del xml
        Dim Producto As ListViewItem 'Declaramos el iterador que recorrerá la lista
        Dim resultado As Boolean = True
        Dim ResultadoLINQ As IEnumerable(Of XElement)

        For Each Producto In listaProductos
            ResultadoLINQ = From elemento In ListaNombresProductos Where elemento.Element("Nombre").Value = Producto.SubItems(0).Text Select elemento 'Consultamos si el producto está en la lista
            If ResultadoLINQ.Count = 0 Then 'Si no está, lo añadimos al xml
                docXMLProductos.Add(New XElement("Producto", New XElement("Nombre", Producto.SubItems(0).Text), New XElement("Precio", Producto.SubItems(1).Text.TrimEnd("€"c))))
            Else 'Si está...
                If Sobreescribir And ResultadoLINQ.Count = 1 Then 'Si el flag de sobreescritura está activo y sólo hay un producto que coincida, sobreescribimos el precio...
                    ResultadoLINQ.Single.Element("Precio").Value = Producto.SubItems(1).Text.TrimEnd("€"c)
                Else
                    'En caso contrario, se lo advertimos al usuario y asignamos False al resultado para indincárselo también al programa
                    MessageBox.Show("El producto " & Chr(34) & Producto.SubItems(0).Text & Chr(34) & " ya se encuentra en la lista", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    resultado = False
                End If
            End If
        Next

        Return resultado
    End Function
#End Region
End Class
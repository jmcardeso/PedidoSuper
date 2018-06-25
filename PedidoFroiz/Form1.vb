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

    Public itemsHistoricoProductos As ListViewItem
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
        If Not File.Exists(RutaMisPedidos & "\Pedidos.xml") Then
            Dim Elemento As New XElement("Pedidos")
            Elemento.Save(RutaMisPedidos & "\Pedidos.xml")
        End If
    End Sub

    Private Sub btnNuevoProducto_Click(sender As Object, e As EventArgs) Handles btnNuevoProducto.Click
        frmBuscarProductos.ShowDialog()
    End Sub

    Private Sub btnVerPedidos_Click(sender As Object, e As EventArgs) Handles btnVerPedidos.Click
        frmListaPedidos.ShowDialog()
        If frmListaPedidos.DialogResult = DialogResult.OK Then
            frmPedidoAntiguo.ShowDialog()
        End If
    End Sub

#Region "Funciones auxiliares"
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
    ''' Añade una lista de productos a Productos.xml
    ''' </summary>
    ''' <param name="listaProductos">La lista de productos que se van a añadir</param>
    ''' <returns>Verdadero si los productos no existen en el xml y se pueden añadir, falso en caso contrario</returns>
    Public Overloads Function AnhadirXMLProductos(ByVal listaProductos As List(Of Producto)) As Boolean
        Dim Documento As XElement = XElement.Load(RutaMisPedidos & "\Productos.xml")
        Dim ListaNombresProductos As IEnumerable(Of XElement) = Documento.Descendants("Producto").Elements("Nombre")
        Dim resultado As Boolean = True

        For Each Producto In listaProductos
            Dim ResultadoLINQ = From elemento In ListaNombresProductos Where elemento.Value = Producto.Nombre Select elemento
            If ResultadoLINQ.Count = 0 Then
                Documento.Add(New XElement("Producto", New XElement("Nombre", Producto.Nombre), New XElement("Precio", Producto.Precio)))
            Else
                MessageBox.Show("El producto " & Chr(34) & Producto.Nombre & Chr(34) & " ya se encuentra en la lista", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                resultado = False
            End If
        Next

        Documento.Save(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\Mis Pedidos\Productos.xml")
        Return resultado
    End Function

    ''' <summary>
    ''' Añade un único producto a Productos.xml
    ''' </summary>
    ''' <param name="NuevoProducto">El producto que se va a añadir</param>
    ''' <returns>Verdadero si el producto no existe en el xml y se puede añadir, falso en caso contrario</returns>
    Public Overloads Function AnhadirXMLProductos(ByVal NuevoProducto As Producto) As Boolean
        Dim Documento As XElement = XElement.Load(RutaMisPedidos & "\Productos.xml")
        Dim ListaNombresProductos As IEnumerable(Of XElement) = Documento.Descendants("Producto").Elements("Nombre")
        Dim resultado As Boolean = True

        Dim ResultadoLINQ = From elemento In ListaNombresProductos Where elemento.Value = NuevoProducto.Nombre Select elemento
        If ResultadoLINQ.Count = 0 Then
            Documento.Add(New XElement("Producto", New XElement("Nombre", NuevoProducto.Nombre), New XElement("Precio", NuevoProducto.Precio)))
        Else
            MessageBox.Show("El producto " & Chr(34) & NuevoProducto.Nombre & Chr(34) & " ya se encuentra en la lista", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            resultado = False
        End If

        Documento.Save(My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\Mis Pedidos\Productos.xml")
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

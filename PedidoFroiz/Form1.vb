Imports System.IO
Imports System.Xml

Public Class frmPedidoFroiz
#Region "Constantes"
    Const IDENTIFICADOR_PRECIO_INICIO = "data-price="
    Const IDENTIFICADOR_PRECIO_FINAL = Chr(34)
    Const IDENTIFICADOR_NOMBRE_INICIO = "dproducto " & Chr(34) & ">"
    Const IDENTIFICADOR_NOMBRE_FINAL = "  "
    Const URL_BUSQUEDA = "https://www.froiz.com/shop/search.php?q="
#End Region

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim sResultadoHTML As String = ""
        Dim Productos As New List(Of Producto)
        tbxResultado.Text = ""

        If tbxProducto.Text <> "" Then
            sResultadoHTML = RetHTML(URL_BUSQUEDA & tbxProducto.Text)
            Productos = RetProductos(sResultadoHTML)
            CrearXMLProductos(Productos)
            For Each Producto In Productos
                tbxResultado.Text &= Producto.Nombre & " " & Producto.Precio.ToString & "€" & vbCrLf
            Next
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
    ''' Crea un fichero xml con la lista de productos
    ''' </summary>
    ''' <param name="listaProductos">La lista de productos de la que se va a crear el fichero</param>
    Public Sub CrearXMLProductos(ByVal listaProductos As List(Of Producto))
        Dim Documento As New XElement("Productos")

        For Each Producto In listaProductos
            Documento.Add(New XElement("Producto", New XElement("Nombre", Producto.Nombre), New XElement("Precio", Producto.Precio)))
        Next

        Documento.Save(Application.StartupPath & "\Productos.xml")
    End Sub

#End Region
End Class

''' <summary>
''' La clase Producto contiene el nombre y el precio de los productos
''' </summary>
Public Class Producto
    Public Property Nombre As String
    Public Property Precio As Decimal
End Class

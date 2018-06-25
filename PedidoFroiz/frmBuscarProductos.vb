Public Class frmBuscarProductos

    Public itemsBusquedaProductos As ListViewItem

    Private Sub btnBuscarProducto_Click(sender As Object, e As EventArgs) Handles btnBuscarProducto.Click
        Dim sResultadoHTML As String = ""
        Dim Productos As New List(Of Producto)
        Dim itemsBusquedaProductos As ListViewItem
        Dim n As Integer = 0

        lsvBusquedaProductos.Items.Clear()

        sResultadoHTML = frmPedidoFroiz.RetHTML(frmPedidoFroiz.URL_BUSQUEDA & tbxNombreProducto.Text)
        Productos = frmPedidoFroiz.RetProductos(sResultadoHTML)

        For Each Producto In Productos
            itemsBusquedaProductos = New ListViewItem
            If n Mod 2 Then
                itemsBusquedaProductos.BackColor = Color.Honeydew
            Else
                itemsBusquedaProductos.BackColor = Color.White
            End If
            itemsBusquedaProductos.SubItems(0).Text = Producto.Nombre
            itemsBusquedaProductos.SubItems.Add(Producto.Precio.ToString & "€")
            lsvBusquedaProductos.Items.Add(itemsBusquedaProductos)
            n += 1
        Next
    End Sub

    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        Dim itemsSeleccionados As ListView.SelectedListViewItemCollection = lsvBusquedaProductos.SelectedItems 'Creamos una colección con los elementos seleccionados en el ListView
        Dim nuevoItem As ListViewItem 'Declaramos una variable de tipo ListViewItem para poder volcar la información de la colección de seleccionados sin que dé errores
        itemsBusquedaProductos = New ListViewItem 'Esta variable la necesitamos para el bucle For Each. Reutilizamos la que empleamos en el procedimento de búsqueda
        Dim NuevoProducto As Producto
        Dim n As Integer

        If lsvBusquedaProductos.SelectedItems.Count > 0 Then 'Si hay algún elemento seleccionado...
            n = frmPedidoFroiz.lsvHistoricoProductos.Items.Count 'Asignamos al índice la posición del último elemento del histórico de productos para continuar a partir de ahí
            For Each itemsBusquedaProductos In itemsSeleccionados
                nuevoItem = New ListViewItem 'Reinicializamos la variable llamando a su constructor
                If n Mod 2 Then
                    nuevoItem.BackColor = Color.Honeydew 'Si es impar, este color de fondo...
                Else
                    nuevoItem.BackColor = Color.White 'Si es par, blanco
                End If
                nuevoItem.SubItems(0).Text = itemsBusquedaProductos.SubItems(0).Text 'Ponemos el nombre del producto...
                nuevoItem.SubItems.Add(itemsBusquedaProductos.SubItems(1).Text) 'Y su precio, que ya incluye el símbolo del euro

                'Comprobamos si el producto no existe ya en la lista. Para ello, creamos un nuevo objeto de la clase Producto y lo intentamos añadir; si el resultado es True, el producto no existía previamente y se añade correctamente
                NuevoProducto = New Producto With {
                    .Nombre = nuevoItem.SubItems(0).Text,
                    .Precio = CDec(nuevoItem.SubItems(1).Text.TrimEnd("€"))
                }
                If frmPedidoFroiz.AnhadirXMLProductos(NuevoProducto) Then 'Si el producto no está repetido, lo añadimos al xml y...
                    frmPedidoFroiz.lsvHistoricoProductos.Items.Add(nuevoItem) 'Lo añadimos a la lista del formulario principal
                    n += 1 'Sumamos 1 al índice de posición
                End If
            Next
        End If

        Me.Close() 'Cerramos el formulario modal
    End Sub

    Private Sub frmBuscarProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
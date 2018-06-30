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
        itemsBusquedaProductos = New ListViewItem 'Esta variable la necesitamos para el bucle For Each. Reutilizamos la que empleamos en el procedimento de búsqueda
        Dim NuevosProductos As New List(Of Producto)

        If itemsSeleccionados.Count > 0 Then 'Si hay algún elemento seleccionado...
            For Each itemsBusquedaProductos In itemsSeleccionados
                NuevosProductos.Add(New Producto With {
                    .Nombre = itemsBusquedaProductos.SubItems(0).Text,
                    .Precio = CDec(itemsBusquedaProductos.SubItems(1).Text.TrimEnd("€"))
                })
            Next
            frmPedidoFroiz.AnhadirXMLProductos(NuevosProductos) 'Si el producto no está repetido, lo añadimos al xml y..
            frmPedidoFroiz.DibujarHistoricoProductos(frmPedidoFroiz.docXMLProductos, True) 'Lo añadimos a la lista del formulario principal
        End If

        Me.Close() 'Cerramos el formulario modal
    End Sub

    Private Sub frmBuscarProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
Option Strict On
Option Explicit Off

Public Class frmBuscarProductos

    Public itemsBusquedaProductos As ListViewItem

    Private Sub frmBuscarProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lsvBusquedaProductos.Items.Clear() 'Borramos el ListView y el TextBox al cargar el formulario
        tbxNombreProducto.Text = ""
    End Sub

    Private Sub btnBuscarProducto_Click(sender As Object, e As EventArgs) Handles btnBuscarProducto.Click
        Dim sResultadoHTML As String = ""
        Dim Productos As List(Of ListViewItem)
        Dim Producto As ListViewItem
        Dim n As Integer = 0

        lsvBusquedaProductos.Items.Clear() 'Antes de empezar la búsqueda, borramos la lista
        If tbxNombreProducto.Text.Length = 0 Then Exit Sub 'Si no hay ningún producto que buscar, salimos del procedimiento

        sResultadoHTML = frmPedidoFroiz.RetHTML(frmPedidoFroiz.URL_BUSQUEDA & tbxNombreProducto.Text) 'Buscamos el producto en la web..
        Productos = frmPedidoFroiz.RetProductos(sResultadoHTML) 'Y asignamos el resultado a la lista de productos

        For Each Producto In Productos 'Recorremos la lista y asignamos colores alternos a los elementos del ListView
            If n Mod 2 <> 0 Then
                Producto.BackColor = Color.Honeydew
            Else
                Producto.BackColor = Color.White
            End If
            lsvBusquedaProductos.Items.Add(Producto) 'Añadimos cada producto al ListView
            n += 1
        Next
    End Sub

    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        If lsvBusquedaProductos.SelectedItems.Count > 0 Then 'Si hay algún elemento seleccionado...
            frmPedidoFroiz.AnhadirXMLProductos(lsvBusquedaProductos.SelectedItems) 'Si el producto no está repetido, lo añadimos al xml y..
            frmPedidoFroiz.DibujarHistoricoProductos(frmPedidoFroiz.docXMLProductos.Descendants("Producto"), True) 'Lo añadimos a la lista del formulario principal
            Me.Close() 'Cerramos el formulario modal
        Else
            MessageBox.Show("No se ha seleccionado ningún producto", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub
End Class
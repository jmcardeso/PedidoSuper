Option Strict On
Option Explicit Off

Public Class frmBuscarProductos

    Public itemsBusquedaProductos As ListViewItem

    Private Sub frmBuscarProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lsvBusquedaProductos.Items.Clear()
        tbxNombreProducto.Text = ""
    End Sub

    Private Sub btnBuscarProducto_Click(sender As Object, e As EventArgs) Handles btnBuscarProducto.Click
        Dim sResultadoHTML As String = ""
        Dim Productos As List(Of ListViewItem)
        Dim Producto As ListViewItem
        Dim n As Integer = 0

        lsvBusquedaProductos.Items.Clear()
        If tbxNombreProducto.Text.Length = 0 Then Exit Sub

        sResultadoHTML = frmPedidoFroiz.RetHTML(frmPedidoFroiz.URL_BUSQUEDA & tbxNombreProducto.Text)
        Productos = frmPedidoFroiz.RetProductos(sResultadoHTML)

        For Each Producto In Productos
            If n Mod 2 <> 0 Then
                Producto.BackColor = Color.Honeydew
            Else
                Producto.BackColor = Color.White
            End If
            lsvBusquedaProductos.Items.Add(Producto)
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
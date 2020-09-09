Option Strict On
Option Explicit Off

Imports System.Windows.Forms

Public Class dlgEditarLinea

    Dim DescripcionAntigua, CantidadAntigua, PVPAntiguo, ImporteAntiguo As String 'Declaramos las variables para almacenar el resultado previo a la edición

    Private Sub dlgEditarLinea_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbxDescripcion.ReadOnly = True 'Por defecto, la descripción es de sólo lectura
        tbxDescripcion.BackColor = SystemColors.Window 'Por estética ponemos su color de fondo como los demás
        CantidadAntigua = tbxCant.Text 'Asignamos los valores anteriores a la edición a las variables para poder revertir los cambios
        DescripcionAntigua = tbxDescripcion.Text
        PVPAntiguo = tbxPVP.Text
        ImporteAntiguo = tbxImporte.Text
        cbxEditarDescripcion.Checked = False
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim CadenaAviso As String = ""

        If Not IsNumeric(tbxCant.Text) Then 'Si la cantidad no es un número, avisamos al usuario, revertimos los cambios y salimos del procedimiento
            MessageBox.Show("La cantidad sólo puede contener caracteres numéricos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbxCant.Text = CantidadAntigua
            Exit Sub
        End If

        If Not IsNumeric(tbxPVP.Text) Then 'Hacemos lo mismo con el PVP...
            MessageBox.Show("El P.V.P. sólo puede contener caracteres numéricos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbxPVP.Text = PVPAntiguo
            Exit Sub
        End If

        If CDec(tbxCant.Text) = 0 Then 'Primer control: Si ponemos un 0, es decir, queremos borrar el producto...
            If frmPedidoAntiguo.lsvPedidoAntiguo.Items.Count = 1 Then 'Segundo control: Si sólo queda este producto en el pedido...
                CadenaAviso = "Este es el único producto del pedido, si lo borra se borrará también el pedido, ¿Está seguro?" 'Asignamos la advertencia correspondiente a CadenaAviso
            Else '[Segundo control] En caso contrario, sólo advertimos del borrado del producto, no del pedido
                CadenaAviso = "El producto se borrará del pedido, ¿Está seguro?"
            End If

            'Si el usuario cancela la operación de borrado, volvemos a poner la cantidad antigua en el TextBox y salimos del procedimiento
            If MessageBox.Show(CadenaAviso, "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
                tbxCant.Text = CantidadAntigua
                Exit Sub
            End If
        End If

        'Reemplazamos los puntos por comas en los números y calculamos el importe según los nuevos datos
        tbxCant.Text = tbxCant.Text.Replace(".", ",")
        tbxPVP.Text = tbxPVP.Text.Replace(".", ",")
        tbxImporte.Text = Math.Round(CDec(tbxCant.Text) * CDec(tbxPVP.Text), 2).ToString

        Me.DialogResult = System.Windows.Forms.DialogResult.OK 'Si todo fue bien, salimos del cuadro de diálogo con el resultado OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel 'Si el usuario cancela la operación, salimos con el resultado Cancel
        Me.Close()
    End Sub

    Private Sub cbxEditarDescripcion_CheckedChanged(sender As Object, e As EventArgs) Handles cbxEditarDescripcion.CheckedChanged
        If cbxEditarDescripcion.Checked = True Then 'Activamos o desactivamos la edición de la descripción según el estado del botón
            tbxDescripcion.ReadOnly = False
        Else
            tbxDescripcion.ReadOnly = True
            tbxDescripcion.Text = DescripcionAntigua 'Cuando lo volvemos a poner de sólo lectura, el contenido de la descripción vuelve a ser el original
        End If
    End Sub

End Class

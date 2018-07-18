<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class dlgEditarLinea
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbxCant = New System.Windows.Forms.TextBox()
        Me.tbxDescripcion = New System.Windows.Forms.TextBox()
        Me.tbxPVP = New System.Windows.Forms.TextBox()
        Me.tbxImporte = New System.Windows.Forms.TextBox()
        Me.cbxEditarDescripcion = New System.Windows.Forms.CheckBox()
        Me.ttpFormulario = New System.Windows.Forms.ToolTip(Me.components)
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Cant.*"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(51, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Descripción"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(296, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "P.V.P."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(369, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Importe"
        '
        'tbxCant
        '
        Me.tbxCant.Location = New System.Drawing.Point(12, 27)
        Me.tbxCant.Name = "tbxCant"
        Me.tbxCant.Size = New System.Drawing.Size(36, 20)
        Me.tbxCant.TabIndex = 5
        '
        'tbxDescripcion
        '
        Me.tbxDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.tbxDescripcion.Location = New System.Drawing.Point(54, 27)
        Me.tbxDescripcion.Name = "tbxDescripcion"
        Me.tbxDescripcion.ReadOnly = True
        Me.tbxDescripcion.Size = New System.Drawing.Size(239, 20)
        Me.tbxDescripcion.TabIndex = 6
        '
        'tbxPVP
        '
        Me.tbxPVP.Location = New System.Drawing.Point(299, 27)
        Me.tbxPVP.Name = "tbxPVP"
        Me.tbxPVP.Size = New System.Drawing.Size(64, 20)
        Me.tbxPVP.TabIndex = 7
        '
        'tbxImporte
        '
        Me.tbxImporte.BackColor = System.Drawing.SystemColors.Window
        Me.tbxImporte.Location = New System.Drawing.Point(370, 27)
        Me.tbxImporte.Name = "tbxImporte"
        Me.tbxImporte.ReadOnly = True
        Me.tbxImporte.Size = New System.Drawing.Size(64, 20)
        Me.tbxImporte.TabIndex = 8
        '
        'cbxEditarDescripcion
        '
        Me.cbxEditarDescripcion.AutoSize = True
        Me.cbxEditarDescripcion.Location = New System.Drawing.Point(54, 77)
        Me.cbxEditarDescripcion.Name = "cbxEditarDescripcion"
        Me.cbxEditarDescripcion.Size = New System.Drawing.Size(210, 17)
        Me.cbxEditarDescripcion.TabIndex = 9
        Me.cbxEditarDescripcion.Text = "Editar la descripción (no recomendado)"
        Me.cbxEditarDescripcion.UseVisualStyleBackColor = True
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Image = Global.PedidoFroiz.My.Resources.Resources.if_Delete_131713
        Me.Cancel_Button.Location = New System.Drawing.Point(391, 62)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(43, 44)
        Me.Cancel_Button.TabIndex = 1
        Me.ttpFormulario.SetToolTip(Me.Cancel_Button, "Cancelar")
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Image = Global.PedidoFroiz.My.Resources.Resources.if_check_13491
        Me.OK_Button.Location = New System.Drawing.Point(333, 62)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(43, 44)
        Me.OK_Button.TabIndex = 0
        Me.ttpFormulario.SetToolTip(Me.OK_Button, "Aceptar")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(161, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "(*) 0 borra el producto del pedido"
        '
        'dlgEditarLinea
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(447, 120)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.cbxEditarDescripcion)
        Me.Controls.Add(Me.tbxImporte)
        Me.Controls.Add(Me.tbxPVP)
        Me.Controls.Add(Me.tbxDescripcion)
        Me.Controls.Add(Me.tbxCant)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgEditarLinea"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edición"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents tbxCant As TextBox
    Friend WithEvents tbxDescripcion As TextBox
    Friend WithEvents tbxPVP As TextBox
    Friend WithEvents tbxImporte As TextBox
    Friend WithEvents cbxEditarDescripcion As CheckBox
    Friend WithEvents ttpFormulario As ToolTip
    Friend WithEvents Cancel_Button As Button
    Friend WithEvents OK_Button As Button
    Friend WithEvents Label5 As Label
End Class

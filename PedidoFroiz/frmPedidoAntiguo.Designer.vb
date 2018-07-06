<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPedidoAntiguo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPedidoAntiguo))
        Me.ttpFormulario = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnImprimirPedido = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnVolver = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbxTotal = New System.Windows.Forms.TextBox()
        Me.lblPedidoFecha = New System.Windows.Forms.Label()
        Me.lsvPedidoAntiguo = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'btnImprimirPedido
        '
        Me.btnImprimirPedido.ImageIndex = 8
        Me.btnImprimirPedido.ImageList = Me.ImageList1
        Me.btnImprimirPedido.Location = New System.Drawing.Point(12, 272)
        Me.btnImprimirPedido.Name = "btnImprimirPedido"
        Me.btnImprimirPedido.Size = New System.Drawing.Size(43, 44)
        Me.btnImprimirPedido.TabIndex = 1
        Me.ttpFormulario.SetToolTip(Me.btnImprimirPedido, "Imprimir el pedido")
        Me.btnImprimirPedido.UseVisualStyleBackColor = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "actualizar.png")
        Me.ImageList1.Images.SetKeyName(1, "Anhadir.png")
        Me.ImageList1.Images.SetKeyName(2, "borrar.png")
        Me.ImageList1.Images.SetKeyName(3, "confirmar.png")
        Me.ImageList1.Images.SetKeyName(4, "derecha.png")
        Me.ImageList1.Images.SetKeyName(5, "izquierda.png")
        Me.ImageList1.Images.SetKeyName(6, "lista.png")
        Me.ImageList1.Images.SetKeyName(7, "buscar.ico")
        Me.ImageList1.Images.SetKeyName(8, "imprimir.ico")
        Me.ImageList1.Images.SetKeyName(9, "volver.ico")
        '
        'btnVolver
        '
        Me.btnVolver.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnVolver.ImageIndex = 9
        Me.btnVolver.ImageList = Me.ImageList1
        Me.btnVolver.Location = New System.Drawing.Point(71, 272)
        Me.btnVolver.Name = "btnVolver"
        Me.btnVolver.Size = New System.Drawing.Size(43, 44)
        Me.btnVolver.TabIndex = 3
        Me.btnVolver.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.ttpFormulario.SetToolTip(Me.btnVolver, "Volver al formulario principal")
        Me.btnVolver.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(219, 288)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "TOTAL:"
        '
        'tbxTotal
        '
        Me.tbxTotal.BackColor = System.Drawing.SystemColors.Window
        Me.tbxTotal.Location = New System.Drawing.Point(270, 285)
        Me.tbxTotal.Name = "tbxTotal"
        Me.tbxTotal.ReadOnly = True
        Me.tbxTotal.Size = New System.Drawing.Size(69, 20)
        Me.tbxTotal.TabIndex = 4
        Me.tbxTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPedidoFecha
        '
        Me.lblPedidoFecha.AutoSize = True
        Me.lblPedidoFecha.Location = New System.Drawing.Point(9, 9)
        Me.lblPedidoFecha.Name = "lblPedidoFecha"
        Me.lblPedidoFecha.Size = New System.Drawing.Size(43, 13)
        Me.lblPedidoFecha.TabIndex = 16
        Me.lblPedidoFecha.Text = "Pedido:"
        '
        'lsvPedidoAntiguo
        '
        Me.lsvPedidoAntiguo.BackColor = System.Drawing.SystemColors.Window
        Me.lsvPedidoAntiguo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lsvPedidoAntiguo.FullRowSelect = True
        Me.lsvPedidoAntiguo.GridLines = True
        Me.lsvPedidoAntiguo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lsvPedidoAntiguo.Location = New System.Drawing.Point(12, 25)
        Me.lsvPedidoAntiguo.MultiSelect = False
        Me.lsvPedidoAntiguo.Name = "lsvPedidoAntiguo"
        Me.lsvPedidoAntiguo.Size = New System.Drawing.Size(328, 240)
        Me.lsvPedidoAntiguo.TabIndex = 0
        Me.lsvPedidoAntiguo.UseCompatibleStateImageBehavior = False
        Me.lsvPedidoAntiguo.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Descripción"
        Me.ColumnHeader1.Width = 261
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "P.V.P."
        '
        'frmPedidoAntiguo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnVolver
        Me.ClientSize = New System.Drawing.Size(355, 331)
        Me.Controls.Add(Me.btnImprimirPedido)
        Me.Controls.Add(Me.btnVolver)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbxTotal)
        Me.Controls.Add(Me.lblPedidoFecha)
        Me.Controls.Add(Me.lsvPedidoAntiguo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmPedidoAntiguo"
        Me.Text = "Pedido:"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ttpFormulario As ToolTip
    Friend WithEvents btnImprimirPedido As Button
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents btnVolver As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents tbxTotal As TextBox
    Friend WithEvents lblPedidoFecha As Label
    Friend WithEvents lsvPedidoAntiguo As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEsperando
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pbxEsperando = New System.Windows.Forms.PictureBox()
        CType(Me.pbxEsperando, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbxEsperando
        '
        Me.pbxEsperando.Image = Global.PedidoFroiz.My.Resources.Resources.Esperando
        Me.pbxEsperando.Location = New System.Drawing.Point(0, 0)
        Me.pbxEsperando.Margin = New System.Windows.Forms.Padding(0)
        Me.pbxEsperando.MaximumSize = New System.Drawing.Size(73, 73)
        Me.pbxEsperando.MinimumSize = New System.Drawing.Size(73, 73)
        Me.pbxEsperando.Name = "pbxEsperando"
        Me.pbxEsperando.Size = New System.Drawing.Size(73, 73)
        Me.pbxEsperando.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pbxEsperando.TabIndex = 0
        Me.pbxEsperando.TabStop = False
        Me.pbxEsperando.UseWaitCursor = True
        '
        'frmEsperando
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(73, 73)
        Me.ControlBox = False
        Me.Controls.Add(Me.pbxEsperando)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(73, 73)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(73, 73)
        Me.Name = "frmEsperando"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        CType(Me.pbxEsperando, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pbxEsperando As PictureBox
End Class

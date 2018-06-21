<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPedidoFroiz
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
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.tbxProducto = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lsvHistoricoProductos = New System.Windows.Forms.ListView()
        Me.Nombre = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Precio = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'btnBuscar
        '
        Me.btnBuscar.Location = New System.Drawing.Point(145, 102)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(101, 48)
        Me.btnBuscar.TabIndex = 0
        Me.btnBuscar.Text = "Buscar"
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'tbxProducto
        '
        Me.tbxProducto.Location = New System.Drawing.Point(10, 55)
        Me.tbxProducto.Name = "tbxProducto"
        Me.tbxProducto.Size = New System.Drawing.Size(375, 20)
        Me.tbxProducto.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Producto:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 182)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(116, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Histótico de productos:"
        '
        'lsvHistoricoProductos
        '
        Me.lsvHistoricoProductos.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Nombre, Me.Precio})
        Me.lsvHistoricoProductos.FullRowSelect = True
        Me.lsvHistoricoProductos.GridLines = True
        Me.lsvHistoricoProductos.Location = New System.Drawing.Point(10, 198)
        Me.lsvHistoricoProductos.Name = "lsvHistoricoProductos"
        Me.lsvHistoricoProductos.Size = New System.Drawing.Size(375, 240)
        Me.lsvHistoricoProductos.TabIndex = 6
        Me.lsvHistoricoProductos.UseCompatibleStateImageBehavior = False
        Me.lsvHistoricoProductos.View = System.Windows.Forms.View.Details
        '
        'Nombre
        '
        Me.Nombre.Text = "Nombre"
        Me.Nombre.Width = 261
        '
        'Precio
        '
        Me.Precio.Text = "Precio"
        '
        'frmPedidoFroiz
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(806, 450)
        Me.Controls.Add(Me.lsvHistoricoProductos)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbxProducto)
        Me.Controls.Add(Me.btnBuscar)
        Me.Name = "frmPedidoFroiz"
        Me.Text = "PedidoFroiz"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnBuscar As Button
    Friend WithEvents tbxProducto As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lsvHistoricoProductos As ListView
    Friend WithEvents Nombre As ColumnHeader
    Friend WithEvents Precio As ColumnHeader
End Class

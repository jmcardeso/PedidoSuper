<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBuscarProductos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBuscarProductos))
        Me.tbxNombreProducto = New System.Windows.Forms.TextBox()
        Me.btnBuscarProducto = New System.Windows.Forms.Button()
        Me.lsvBusquedaProductos = New System.Windows.Forms.ListView()
        Me.Nombre = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Precio = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnConfirmar = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'tbxNombreProducto
        '
        Me.tbxNombreProducto.Location = New System.Drawing.Point(21, 27)
        Me.tbxNombreProducto.Name = "tbxNombreProducto"
        Me.tbxNombreProducto.Size = New System.Drawing.Size(265, 20)
        Me.tbxNombreProducto.TabIndex = 0
        '
        'btnBuscarProducto
        '
        Me.btnBuscarProducto.Image = Global.PedidoSúper.My.Resources.Resources.buscar
        Me.btnBuscarProducto.Location = New System.Drawing.Point(306, 14)
        Me.btnBuscarProducto.Name = "btnBuscarProducto"
        Me.btnBuscarProducto.Size = New System.Drawing.Size(43, 44)
        Me.btnBuscarProducto.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnBuscarProducto, "Buscar el producto")
        Me.btnBuscarProducto.UseVisualStyleBackColor = True
        '
        'lsvBusquedaProductos
        '
        Me.lsvBusquedaProductos.BackColor = System.Drawing.SystemColors.Window
        Me.lsvBusquedaProductos.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Nombre, Me.Precio})
        Me.lsvBusquedaProductos.FullRowSelect = True
        Me.lsvBusquedaProductos.GridLines = True
        Me.lsvBusquedaProductos.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lsvBusquedaProductos.HideSelection = False
        Me.lsvBusquedaProductos.Location = New System.Drawing.Point(21, 66)
        Me.lsvBusquedaProductos.Name = "lsvBusquedaProductos"
        Me.lsvBusquedaProductos.Size = New System.Drawing.Size(328, 240)
        Me.lsvBusquedaProductos.TabIndex = 2
        Me.lsvBusquedaProductos.UseCompatibleStateImageBehavior = False
        Me.lsvBusquedaProductos.View = System.Windows.Forms.View.Details
        '
        'Nombre
        '
        Me.Nombre.Text = "Descripción"
        Me.Nombre.Width = 261
        '
        'Precio
        '
        Me.Precio.Text = "P.V.P."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Nombre del producto:"
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Image = Global.PedidoSúper.My.Resources.Resources.if_Delete_131713
        Me.btnCancelar.Location = New System.Drawing.Point(306, 323)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(43, 44)
        Me.btnCancelar.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.btnCancelar, "Cancelar")
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnConfirmar
        '
        Me.btnConfirmar.Image = Global.PedidoSúper.My.Resources.Resources.if_check_13491
        Me.btnConfirmar.Location = New System.Drawing.Point(243, 323)
        Me.btnConfirmar.Name = "btnConfirmar"
        Me.btnConfirmar.Size = New System.Drawing.Size(43, 44)
        Me.btnConfirmar.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.btnConfirmar, "Confirmar")
        Me.btnConfirmar.UseVisualStyleBackColor = True
        '
        'frmBuscarProductos
        '
        Me.AcceptButton = Me.btnConfirmar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(371, 384)
        Me.Controls.Add(Me.btnConfirmar)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lsvBusquedaProductos)
        Me.Controls.Add(Me.btnBuscarProducto)
        Me.Controls.Add(Me.tbxNombreProducto)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmBuscarProductos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Buscar un producto"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbxNombreProducto As TextBox
    Friend WithEvents btnBuscarProducto As Button
    Friend WithEvents lsvBusquedaProductos As ListView
    Friend WithEvents Nombre As ColumnHeader
    Friend WithEvents Precio As ColumnHeader
    Friend WithEvents Label1 As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents btnCancelar As Button
    Friend WithEvents btnConfirmar As Button
End Class

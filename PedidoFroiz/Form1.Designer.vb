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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPedidoFroiz))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lsvHistoricoProductos = New System.Windows.Forms.ListView()
        Me.Nombre = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Precio = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lsvPedido = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAnhadirAPedido = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnEliminarDePedido = New System.Windows.Forms.Button()
        Me.tbxTotal = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnEliminarDelHistorico = New System.Windows.Forms.Button()
        Me.btnNuevoProducto = New System.Windows.Forms.Button()
        Me.btnActualizarPrecios = New System.Windows.Forms.Button()
        Me.btnVerPedidos = New System.Windows.Forms.Button()
        Me.btnConfirmarPedido = New System.Windows.Forms.Button()
        Me.ttpFormulario = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnBorrarPedido = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Productos:"
        '
        'lsvHistoricoProductos
        '
        Me.lsvHistoricoProductos.BackColor = System.Drawing.SystemColors.Window
        Me.lsvHistoricoProductos.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Nombre, Me.Precio})
        Me.lsvHistoricoProductos.FullRowSelect = True
        Me.lsvHistoricoProductos.GridLines = True
        Me.lsvHistoricoProductos.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lsvHistoricoProductos.Location = New System.Drawing.Point(12, 23)
        Me.lsvHistoricoProductos.Name = "lsvHistoricoProductos"
        Me.lsvHistoricoProductos.Size = New System.Drawing.Size(328, 240)
        Me.lsvHistoricoProductos.TabIndex = 0
        Me.lsvHistoricoProductos.UseCompatibleStateImageBehavior = False
        Me.lsvHistoricoProductos.View = System.Windows.Forms.View.Details
        '
        'Nombre
        '
        Me.Nombre.Text = "Descripción"
        Me.Nombre.Width = 260
        '
        'Precio
        '
        Me.Precio.Text = "P.V.P."
        '
        'lsvPedido
        '
        Me.lsvPedido.BackColor = System.Drawing.SystemColors.Window
        Me.lsvPedido.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader4})
        Me.lsvPedido.FullRowSelect = True
        Me.lsvPedido.GridLines = True
        Me.lsvPedido.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lsvPedido.LabelEdit = True
        Me.lsvPedido.Location = New System.Drawing.Point(431, 23)
        Me.lsvPedido.Name = "lsvPedido"
        Me.lsvPedido.Size = New System.Drawing.Size(391, 240)
        Me.lsvPedido.TabIndex = 3
        Me.lsvPedido.UseCompatibleStateImageBehavior = False
        Me.lsvPedido.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Cant."
        Me.ColumnHeader3.Width = 38
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Descripción"
        Me.ColumnHeader1.Width = 248
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "P.V.P."
        Me.ColumnHeader2.Width = 45
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Importe"
        Me.ColumnHeader4.Width = 52
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(428, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Pedido:"
        '
        'btnAnhadirAPedido
        '
        Me.btnAnhadirAPedido.ImageIndex = 4
        Me.btnAnhadirAPedido.ImageList = Me.ImageList1
        Me.btnAnhadirAPedido.Location = New System.Drawing.Point(364, 80)
        Me.btnAnhadirAPedido.Name = "btnAnhadirAPedido"
        Me.btnAnhadirAPedido.Size = New System.Drawing.Size(43, 40)
        Me.btnAnhadirAPedido.TabIndex = 1
        Me.ttpFormulario.SetToolTip(Me.btnAnhadirAPedido, "Añadir el producto al pedido")
        Me.btnAnhadirAPedido.UseVisualStyleBackColor = True
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
        'btnEliminarDePedido
        '
        Me.btnEliminarDePedido.ImageIndex = 1
        Me.btnEliminarDePedido.ImageList = Me.ImageList1
        Me.btnEliminarDePedido.Location = New System.Drawing.Point(364, 159)
        Me.btnEliminarDePedido.Name = "btnEliminarDePedido"
        Me.btnEliminarDePedido.Size = New System.Drawing.Size(43, 40)
        Me.btnEliminarDePedido.TabIndex = 2
        Me.ttpFormulario.SetToolTip(Me.btnEliminarDePedido, "Eliminar el producto del pedido")
        Me.btnEliminarDePedido.UseVisualStyleBackColor = True
        '
        'tbxTotal
        '
        Me.tbxTotal.BackColor = System.Drawing.SystemColors.Window
        Me.tbxTotal.Location = New System.Drawing.Point(753, 283)
        Me.tbxTotal.Name = "tbxTotal"
        Me.tbxTotal.ReadOnly = True
        Me.tbxTotal.Size = New System.Drawing.Size(69, 20)
        Me.tbxTotal.TabIndex = 10
        Me.tbxTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(702, 286)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "TOTAL:"
        '
        'btnEliminarDelHistorico
        '
        Me.btnEliminarDelHistorico.ImageIndex = 3
        Me.btnEliminarDelHistorico.ImageList = Me.ImageList1
        Me.btnEliminarDelHistorico.Location = New System.Drawing.Point(71, 270)
        Me.btnEliminarDelHistorico.Name = "btnEliminarDelHistorico"
        Me.btnEliminarDelHistorico.Size = New System.Drawing.Size(43, 44)
        Me.btnEliminarDelHistorico.TabIndex = 4
        Me.ttpFormulario.SetToolTip(Me.btnEliminarDelHistorico, "Eliminar el producto")
        Me.btnEliminarDelHistorico.UseVisualStyleBackColor = True
        '
        'btnNuevoProducto
        '
        Me.btnNuevoProducto.ImageIndex = 0
        Me.btnNuevoProducto.ImageList = Me.ImageList1
        Me.btnNuevoProducto.Location = New System.Drawing.Point(12, 270)
        Me.btnNuevoProducto.Name = "btnNuevoProducto"
        Me.btnNuevoProducto.Size = New System.Drawing.Size(43, 44)
        Me.btnNuevoProducto.TabIndex = 8
        Me.btnNuevoProducto.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.ttpFormulario.SetToolTip(Me.btnNuevoProducto, "Añadir un nuevo producto")
        Me.btnNuevoProducto.UseVisualStyleBackColor = True
        '
        'btnActualizarPrecios
        '
        Me.btnActualizarPrecios.ImageIndex = 5
        Me.btnActualizarPrecios.ImageList = Me.ImageList1
        Me.btnActualizarPrecios.Location = New System.Drawing.Point(129, 270)
        Me.btnActualizarPrecios.Name = "btnActualizarPrecios"
        Me.btnActualizarPrecios.Size = New System.Drawing.Size(43, 44)
        Me.btnActualizarPrecios.TabIndex = 5
        Me.ttpFormulario.SetToolTip(Me.btnActualizarPrecios, "Actualizar los precios")
        Me.btnActualizarPrecios.UseVisualStyleBackColor = True
        '
        'btnVerPedidos
        '
        Me.btnVerPedidos.ImageIndex = 6
        Me.btnVerPedidos.ImageList = Me.ImageList1
        Me.btnVerPedidos.Location = New System.Drawing.Point(550, 270)
        Me.btnVerPedidos.Name = "btnVerPedidos"
        Me.btnVerPedidos.Size = New System.Drawing.Size(43, 44)
        Me.btnVerPedidos.TabIndex = 6
        Me.ttpFormulario.SetToolTip(Me.btnVerPedidos, "Ver los anteriores pedidos")
        Me.btnVerPedidos.UseVisualStyleBackColor = True
        '
        'btnConfirmarPedido
        '
        Me.btnConfirmarPedido.ImageIndex = 2
        Me.btnConfirmarPedido.ImageList = Me.ImageList1
        Me.btnConfirmarPedido.Location = New System.Drawing.Point(431, 270)
        Me.btnConfirmarPedido.Name = "btnConfirmarPedido"
        Me.btnConfirmarPedido.Size = New System.Drawing.Size(43, 44)
        Me.btnConfirmarPedido.TabIndex = 9
        Me.ttpFormulario.SetToolTip(Me.btnConfirmarPedido, "Confirmar el pedido")
        Me.btnConfirmarPedido.UseVisualStyleBackColor = True
        '
        'btnBorrarPedido
        '
        Me.btnBorrarPedido.ImageIndex = 3
        Me.btnBorrarPedido.ImageList = Me.ImageList1
        Me.btnBorrarPedido.Location = New System.Drawing.Point(491, 270)
        Me.btnBorrarPedido.Name = "btnBorrarPedido"
        Me.btnBorrarPedido.Size = New System.Drawing.Size(43, 44)
        Me.btnBorrarPedido.TabIndex = 7
        Me.ttpFormulario.SetToolTip(Me.btnBorrarPedido, "Borrar el pedido")
        Me.btnBorrarPedido.UseVisualStyleBackColor = True
        '
        'frmPedidoFroiz
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(833, 323)
        Me.Controls.Add(Me.btnBorrarPedido)
        Me.Controls.Add(Me.btnConfirmarPedido)
        Me.Controls.Add(Me.btnVerPedidos)
        Me.Controls.Add(Me.btnActualizarPrecios)
        Me.Controls.Add(Me.btnNuevoProducto)
        Me.Controls.Add(Me.btnEliminarDelHistorico)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbxTotal)
        Me.Controls.Add(Me.btnEliminarDePedido)
        Me.Controls.Add(Me.btnAnhadirAPedido)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lsvPedido)
        Me.Controls.Add(Me.lsvHistoricoProductos)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmPedidoFroiz"
        Me.Text = "PedidoFroiz"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As Label
    Friend WithEvents lsvHistoricoProductos As ListView
    Friend WithEvents Nombre As ColumnHeader
    Friend WithEvents Precio As ColumnHeader
    Friend WithEvents lsvPedido As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents Label1 As Label
    Friend WithEvents btnAnhadirAPedido As Button
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents btnEliminarDePedido As Button
    Friend WithEvents tbxTotal As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnEliminarDelHistorico As Button
    Friend WithEvents btnNuevoProducto As Button
    Friend WithEvents btnActualizarPrecios As Button
    Friend WithEvents btnVerPedidos As Button
    Friend WithEvents btnConfirmarPedido As Button
    Friend WithEvents ttpFormulario As ToolTip
    Friend WithEvents btnBorrarPedido As Button
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListaPedidos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmListaPedidos))
        Me.dtpDesdeFecha = New System.Windows.Forms.DateTimePicker()
        Me.dtpHastaFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.btnVerPedido = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.lsvBusquedaPedidos = New System.Windows.Forms.ListView()
        Me.Pedido = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Total = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'dtpDesdeFecha
        '
        Me.dtpDesdeFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDesdeFecha.Location = New System.Drawing.Point(59, 22)
        Me.dtpDesdeFecha.Name = "dtpDesdeFecha"
        Me.dtpDesdeFecha.Size = New System.Drawing.Size(81, 20)
        Me.dtpDesdeFecha.TabIndex = 0
        '
        'dtpHastaFecha
        '
        Me.dtpHastaFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpHastaFecha.Location = New System.Drawing.Point(204, 22)
        Me.dtpHastaFecha.Name = "dtpHastaFecha"
        Me.dtpHastaFecha.Size = New System.Drawing.Size(81, 20)
        Me.dtpHastaFecha.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Desde:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(157, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Hasta:"
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
        'btnBuscar
        '
        Me.btnBuscar.ImageIndex = 7
        Me.btnBuscar.ImageList = Me.ImageList1
        Me.btnBuscar.Location = New System.Drawing.Point(300, 12)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(43, 44)
        Me.btnBuscar.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnBuscar, "Buscar pedidos en el intervalo de fechas")
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'btnVerPedido
        '
        Me.btnVerPedido.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnVerPedido.ImageIndex = 2
        Me.btnVerPedido.ImageList = Me.ImageList1
        Me.btnVerPedido.Location = New System.Drawing.Point(243, 394)
        Me.btnVerPedido.Name = "btnVerPedido"
        Me.btnVerPedido.Size = New System.Drawing.Size(43, 44)
        Me.btnVerPedido.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.btnVerPedido, "Ver el pedido")
        Me.btnVerPedido.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.ImageIndex = 3
        Me.btnCancelar.ImageList = Me.ImageList1
        Me.btnCancelar.Location = New System.Drawing.Point(300, 394)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(43, 44)
        Me.btnCancelar.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.btnCancelar, "Cancelar")
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'lsvBusquedaPedidos
        '
        Me.lsvBusquedaPedidos.BackColor = System.Drawing.SystemColors.Window
        Me.lsvBusquedaPedidos.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Pedido, Me.Total})
        Me.lsvBusquedaPedidos.FullRowSelect = True
        Me.lsvBusquedaPedidos.GridLines = True
        Me.lsvBusquedaPedidos.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lsvBusquedaPedidos.Location = New System.Drawing.Point(15, 73)
        Me.lsvBusquedaPedidos.Name = "lsvBusquedaPedidos"
        Me.lsvBusquedaPedidos.Size = New System.Drawing.Size(328, 305)
        Me.lsvBusquedaPedidos.TabIndex = 3
        Me.lsvBusquedaPedidos.UseCompatibleStateImageBehavior = False
        Me.lsvBusquedaPedidos.View = System.Windows.Forms.View.Details
        '
        'Pedido
        '
        Me.Pedido.Text = "Pedido"
        Me.Pedido.Width = 261
        '
        'Total
        '
        Me.Total.Text = "Total"
        '
        'frmListaPedidos
        '
        Me.AcceptButton = Me.btnVerPedido
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(358, 450)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnVerPedido)
        Me.Controls.Add(Me.lsvBusquedaPedidos)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpHastaFecha)
        Me.Controls.Add(Me.dtpDesdeFecha)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmListaPedidos"
        Me.Text = "Lista de pedidos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dtpDesdeFecha As DateTimePicker
    Friend WithEvents dtpHastaFecha As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents btnBuscar As Button
    Friend WithEvents lsvBusquedaPedidos As ListView
    Friend WithEvents Pedido As ColumnHeader
    Friend WithEvents Total As ColumnHeader
    Friend WithEvents btnVerPedido As Button
    Friend WithEvents btnCancelar As Button
End Class

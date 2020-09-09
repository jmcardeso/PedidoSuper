Option Strict On
Option Explicit Off

Imports System.Threading

Public Class frmBuscarProductos
    Public itemsBusquedaProductos As ListViewItem

    Private Sub frmBuscarProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lsvBusquedaProductos.Items.Clear() 'Borramos el ListView y el TextBox al cargar el formulario
        tbxNombreProducto.Text = ""
    End Sub

    Private Sub tbxNombreProducto_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles tbxNombreProducto.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then 'Si pulsamos enter en el TextBox...
            Me.Controls("btnBuscarProducto").Focus() 'Obligamos a poner el foco en el botón de buscar producto y, como resultado, al pulsar enter también se pulsa el botón
        End If
    End Sub

    Private Sub btnBuscarProducto_Click(sender As Object, e As EventArgs) Handles btnBuscarProducto.Click
        Dim sResultadoHTML As String = ""
        Dim Productos As List(Of ListViewItem)
        Dim Producto As ListViewItem
        Dim n As Integer = 0
        Dim ProcesoEsperando As ThreadStart 'Declaramos las variables necesarias para trabajar con otro hilo
        Dim hilo As Thread

        lsvBusquedaProductos.Items.Clear() 'Antes de empezar la búsqueda, borramos la lista
        If tbxNombreProducto.Text.Length = 0 Then
            MessageBox.Show("No hay ningún producto que buscar", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub 'Si no hay ningún producto que buscar, salimos del procedimiento
        End If

        'Iniciamos un nuevo hilo para mostrar una rueda girando como indicador de espera. Si no se hace en un nuevo hilo, la animación del gif no se ve
        ProcesoEsperando = New ThreadStart(AddressOf MostrarFrmEsperando)
        hilo = New Thread(ProcesoEsperando)
        hilo.Start()

        sResultadoHTML = Form1.RetHTML(Form1.URL_BUSQUEDA & tbxNombreProducto.Text) 'Buscamos el producto en la web..
        Productos = Form1.RetProductos(sResultadoHTML) 'Y asignamos el resultado a la lista de productos

        For Each Producto In Productos 'Recorremos la lista y asignamos colores alternos a los elementos del ListView
            If n Mod 2 <> 0 Then
                Producto.BackColor = Color.Honeydew
            Else
                Producto.BackColor = Color.White
            End If
            lsvBusquedaProductos.Items.Add(Producto) 'Añadimos cada producto al ListView
            n += 1
        Next

        hilo.Abort()

    End Sub

    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click, lsvBusquedaProductos.DoubleClick
        If lsvBusquedaProductos.SelectedItems.Count > 0 Then 'Si hay algún elemento seleccionado...
            Form1.AnhadirXMLProductos(lsvBusquedaProductos.SelectedItems) 'Si el producto no está repetido, lo añadimos al xml y..
            Form1.DibujarHistoricoProductos(Form1.docXMLProductos.Descendants("Producto"), True) 'Lo añadimos a la lista del formulario principal
            Me.Close() 'Cerramos el formulario modal
        Else
            MessageBox.Show("No se ha seleccionado ningún producto", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub

#Region "Funciones auxiliares"

    ''' <summary>
    ''' Muestra en otro hilo de ejecución una rueda girando como señal de espera
    ''' </summary>
    Public Sub MostrarFrmEsperando()
        Dim frmX As Integer = frmBuscarProductos.ActiveForm.Location.X + 155 'Las coordenadas X e Y del formulario principal, más los píxeles necesarios para centrar el cuadro en el ListView
        Dim frmY As Integer = frmBuscarProductos.ActiveForm.Location.Y + 170

        frmEsperando.Location = New Point(frmX, frmY) 'Asignamos las coordenadas de inicio al formulario...
        frmEsperando.ShowDialog() 'Y lo llamamos como formulario modal
    End Sub

#End Region
End Class
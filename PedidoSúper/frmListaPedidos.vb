Option Strict On
Option Explicit Off

Public Class frmListaPedidos
    Private Sub frmListaPedidos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set the ListViewItemSorter property to a new ListViewItemComparer object.
        Me.lsvBusquedaPedidos.ListViewItemSorter = New ListViewItemComparer()
        BorrarPantallaBusqueda()
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim ListaPedidos As IEnumerable(Of XElement) 'Asignamos una variable para llenar con los pedidos que cumplan los criterios de la búsqueda

        If CDate(dtpHastaFecha.Value.ToShortDateString) < CDate(dtpDesdeFecha.Value.ToShortDateString) Then
            MessageBox.Show("La fecha final no puede ser menor que la inicial", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            dtpHastaFecha.Value = Date.Now
            Exit Sub
        End If
        ListaPedidos = From elemento In Form1.docXMLPedidos.Descendants("Pedido") Where CDate(elemento.Attribute("Fecha").Value) >= CDate(dtpDesdeFecha.Value.ToShortDateString) And CDate(elemento.Attribute("Fecha").Value) <= CDate(dtpHastaFecha.Value.ToShortDateString) Select elemento
        If ListaPedidos.Count = 0 Then
            MessageBox.Show("No se ha encontrado ningún pedido entre esas fechas", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information)
            BorrarPantallaBusqueda()
        Else
            DibujarPedidos(ListaPedidos, True)
        End If
    End Sub

    Private Sub btnVerPedido_Click(sender As Object, e As EventArgs) Handles btnVerPedido.Click
        If lsvBusquedaPedidos.SelectedItems.Count <> 1 Then
            MessageBox.Show("No hay ningún pedido seleccionado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.DialogResult = DialogResult.None 'Indica que el formulario aún se está ejecutando (para no salir al principal)
        End If
    End Sub

    Private Sub lsvBusquedaPedidos_DoubleClick(sender As Object, e As EventArgs) Handles lsvBusquedaPedidos.DoubleClick
        Me.DialogResult = DialogResult.OK 'Esto equivale a pulsar el botón btnVerPedido (no hay que hacer comprobaciones, puesto que si se hace doble click es porque hay un elemento seleccionado)
    End Sub

#Region "Funciones auxiliares"
    ''' <summary>
    ''' Rellena el ListView de los pedidos
    ''' </summary>
    ''' <param name="ListaPedidos">El xml con los pedidos</param>
    ''' <param name="BorrarListView">(Opcional) Verdadero para borrar el ListView</param>
    Public Sub DibujarPedidos(ByVal ListaPedidos As IEnumerable(Of XElement), Optional ByVal BorrarListView As Boolean = False)
        Dim itemsPedidos, item As ListViewItem 'Definimos una variable del tipo elemento del control ListView y su iterador
        Dim Pedido As XElement
        Dim n As Integer = 0

        If BorrarListView Then lsvBusquedaPedidos.Items.Clear() 'Si el argumento opcional es True, borramos la lista

        For Each Pedido In ListaPedidos 'Recorremos la lista y le añadimos los pedidos
            itemsPedidos = New ListViewItem 'Reinicializamos la clase con cada iteración para borrar sus datos
            itemsPedidos.SubItems(0).Text = CDate(Pedido.Attribute("Fecha").Value).ToLongDateString 'Asignamos el nombre...
            itemsPedidos.SubItems.Add(Pedido.Attribute("Total").Value & "€") 'Y el precio...
            lsvBusquedaPedidos.Items.Add(itemsPedidos) 'Y los añadimos al ListView
        Next

        ' Call the sort method to manually sort.
        lsvBusquedaPedidos.Sort()

        For Each item In lsvBusquedaPedidos.Items
            If n Mod 2 <> 0 Then 'Si es par dibujamos en un color, si es impar en el otro
                item.BackColor = Color.Honeydew
            Else
                item.BackColor = Color.White
            End If
            n += 1
        Next
    End Sub

    ''' <summary>
    ''' Borra la pantalla de búsqueda y pone las fechas a día de hoy
    ''' </summary>
    Public Sub BorrarPantallaBusqueda()
        lsvBusquedaPedidos.Items.Clear()
        dtpDesdeFecha.Value = Date.Now
        dtpHastaFecha.Value = dtpDesdeFecha.Value
    End Sub
#End Region
End Class

' Implements the manual sorting of items by columns.
Class ListViewItemComparer
    Implements IComparer
    Private col As Integer
    Private order As SortOrder

    Public Sub New()
        col = 0
        order = SortOrder.Ascending
    End Sub

    Public Sub New(column As Integer, order As SortOrder)
        col = column
        Me.order = order
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim returnVal As Integer
        ' Determine whether the type being compared is a date type.
        Try
            ' Parse the two objects passed as a parameter as a DateTime.
            Dim firstDate As System.DateTime = DateTime.Parse(CType(x, ListViewItem).SubItems(col).Text)
            Dim secondDate As System.DateTime = DateTime.Parse(CType(y, ListViewItem).SubItems(col).Text)
            ' Compare the two dates.
            returnVal = DateTime.Compare(firstDate, secondDate)
            ' If neither compared object has a valid date format, compare as a string.
        Catch
            ' Compare the two items as a string.
            returnVal = [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
        End Try

        ' Determine whether the sort order is descending.
        If order = SortOrder.Descending Then
            ' Invert the value returned by String.Compare.
            returnVal *= -1
        End If
        Return returnVal
    End Function
End Class
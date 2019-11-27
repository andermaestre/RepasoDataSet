Public Class Form1
    Private Sub NuevoPedidoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevoPedidoToolStripMenuItem.Click
        Dim resp As MsgBoxResult = MsgBox("Kierres hunho nuébo?", vbYesNo)

        If resp = MsgBoxResult.Yes Then
            Dim tabla As DataTable = ds.Tables("Pedidos")
            Dim nuevoPedido As DataRow
            Dim nuevoId = BuscarMax()
            nuevoPedido = tabla.NewRow
            nuevoPedido.BeginEdit()
            nuevoPedido("IdPedido") = nuevoId
            nuevoPedido("Fecha") = Date.Today
            nuevoPedido("Cerrado") = 0
            nuevoPedido("TotalNeto") = 0
            nuevoPedido("TotalIva") = 0
            nuevoPedido("TotalPagar") = 0
            nuevoPedido.EndEdit()
            tabla.Rows.Add(nuevoPedido)
            pedidoActual = nuevoId
            Dim f As Form2 = New Form2()
            f.Show()

        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InicializarDataAdapter()

    End Sub

    Private Function BuscarMax() As Integer
        Dim tabla As DataTable = ds.Tables("Pedidos")
        Dim rows() As DataRow = tabla.Select("MAX(IdPedido)")
        If rows.Length = 0 Then
            Return 1
        Else
            Dim numero As Object = rows(0).Item("IdPedido")
            Return numero + 1
        End If
    End Function


End Class

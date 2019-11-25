Public Class Form1
    Private Sub NuevoPedidoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevoPedidoToolStripMenuItem.Click
        Dim resp As MsgBoxResult = MsgBox("Kierres hunho nuébo?", vbYesNo)

        If resp = MsgBoxResult.Yes Then

        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        daPedidos.Fill(ds, "Pedidos")
        daProductos.Fill(ds, "Productos")
        daLineasDetalle.Fill(ds, "LineasDetalle")

        Dim tab As DataTable = ds.Tables("Pedidos")
        Dim key(0) As DataColumn

        key(0) = tab.Columns("IdPedido")
        tab.PrimaryKey = key

        tab = ds.Tables("Productos")
        key(0) = tab.Columns("IdProducto")
        tab.PrimaryKey = key

        tab = ds.Tables("LineasDetalle")
        key(0) = tab.Columns("IdDetalle")
        tab.PrimaryKey = key



    End Sub
End Class

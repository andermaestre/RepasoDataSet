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

        Dim colx, coly As DataColumn

        colx = ds.Tables("Pedidos").Columns("IdPedido")
        coly = ds.Tables("LineasDetalle").Columns("IdPedido")

        Dim reldetped As DataRelation = New DataRelation("DetallesPedidos", colx, coly)

        ds.Relations.Add(reldetped)

        colx = ds.Tables("Productos").Columns("IdProducto")
        coly = ds.Tables("LineasDetalle").Columns("IdProducto")

        Dim reldetprod As DataRelation = New DataRelation("DetallesProductos", colx, coly)

        ds.Relations.Add(reldetprod)

        Dim consFK As ForeignKeyConstraint = ds.Tables("LineasDetalle").Constraints("DetallesPedidos")
        consFK.DeleteRule = Rule.None
        consFK.UpdateRule = Rule.None
        ds.EnforceConstraints = True

        consFK = ds.Tables("LineasDetalle").Constraints("DetallesProductos")
        consFK.DeleteRule = Rule.None
        consFK.UpdateRule = Rule.None
        ds.EnforceConstraints = True

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

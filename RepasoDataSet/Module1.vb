Imports System.Data.SqlClient

Module Module1

    Public pedidoActual As Integer

    Public cadena As String = "Data Source=SEGUNDO150;Initial Catalog=DAM_Andermaestre_DEV;Integrated Security=True"

    Public daPedidos As New SqlDataAdapter("Select * From RepasoDataSet.Pedidos", cadena)
    Public daProductos As New SqlDataAdapter("Select IdProducto,  From RepasoDataSet.Productos", cadena)
    Public daLineasDetalle As New SqlDataAdapter("Select * From RepasoDataSet.LineasDetalle", cadena)

    Public BuilderPedidos As New SqlCommandBuilder(daPedidos)
    Public BuilderProductos As New SqlCommandBuilder(daProductos)
    Public BuilderLineasDetalle As New SqlCommandBuilder(daLineasDetalle)

    Public ds As New DataSet

    Private Sub InicializaDA(da As SqlDataAdapter)
        Dim cb As SqlCommandBuilder
        da.InsertCommand = cb.GetInsertCommand
        da.DeleteCommand = cb.GetDeleteCommand
        da.UpdateCommand = cb.GetUpdateCommand
    End Sub

    Public Sub InicializarDataAdapter()

        InicializaDA(daPedidos)
        InicializaDA(daProductos)
        InicializaDA(daLineasDetalle)

        'daPedidos.UpdateCommand = BuilderPedidos.GetUpdateCommand()
        'daPedidos.InsertCommand = BuilderPedidos.GetUpdateCommand()
        'daPedidos.DeleteCommand = BuilderPedidos.GetUpdateCommand()

        'daProductos.UpdateCommand = BuilderProductos.GetUpdateCommand()
        'daProductos.InsertCommand = BuilderProductos.GetUpdateCommand()
        'daProductos.DeleteCommand = BuilderProductos.GetUpdateCommand()

        'daLineasDetalle.UpdateCommand = BuilderLineasDetalle.GetUpdateCommand()
        'daLineasDetalle.InsertCommand = BuilderLineasDetalle.GetUpdateCommand()
        'daLineasDetalle.DeleteCommand = BuilderLineasDetalle.GetUpdateCommand()

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

        'Relacion Pedidos.LineasDetalle

        Dim colx, coly As DataColumn

        colx = ds.Tables("Pedidos").Columns("IdPedido")
        coly = ds.Tables("LineasDetalle").Columns("IdPedido")

        Dim reldetped As DataRelation = New DataRelation("DetallesPedidos", colx, coly)

        ds.Relations.Add(reldetped)

        colx = ds.Tables("Productos").Columns("IdProducto")
        coly = ds.Tables("LineasDetalle").Columns("IdProducto")

        Dim reldetprod As DataRelation = New DataRelation("DetallesProductos", colx, coly)

        ds.Relations.Add(reldetprod)

        'Dim consFK As ForeignKeyConstraint = ds.Tables("LineasDetalle").Constraints("DetallesPedidos")
        'consFK.DeleteRule = Rule.None
        'consFK.UpdateRule = Rule.None
        'ds.EnforceConstraints = True

        'consFK = ds.Tables("LineasDetalle").Constraints("DetallesProductos")
        'consFK.DeleteRule = Rule.None
        'consFK.UpdateRule = Rule.None
        'ds.EnforceConstraints = True

    End Sub

End Module

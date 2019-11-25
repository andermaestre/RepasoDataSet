Imports System.Data.SqlClient

Module Module1
    Public cadena As String = "Data Source=SEGUNDO150;Initial Catalog=DAM_Andermaestre_DEV;Integrated Security=True"

    Public daPedidos As New SqlDataAdapter("Select * From RepasoDataSet.Pedidos", cadena)
    Public daProductos As New SqlDataAdapter("Select * From RepasoDataSet.Productos", cadena)
    Public daLineasDetalle As New SqlDataAdapter("Select * From RepasoDataSet.LineasDetalle", cadena)

    Public BuilderPedidos As New SqlCommandBuilder(daPedidos)
    Public BuilderProductos As New SqlCommandBuilder(daProductos)
    Public BuilderLineasDetalle As New SqlCommandBuilder(daLineasDetalle)

    Public ds As New DataSet

    Public Sub InicializarDataAdapter()
        daPedidos.UpdateCommand = BuilderPedidos.GetUpdateCommand()
        daPedidos.InsertCommand = BuilderPedidos.GetUpdateCommand()
        daPedidos.DeleteCommand = BuilderPedidos.GetUpdateCommand()

        daProductos.UpdateCommand = BuilderProductos.GetUpdateCommand()
        daProductos.InsertCommand = BuilderProductos.GetUpdateCommand()
        daProductos.DeleteCommand = BuilderProductos.GetUpdateCommand()

        daLineasDetalle.UpdateCommand = BuilderLineasDetalle.GetUpdateCommand()
        daLineasDetalle.InsertCommand = BuilderLineasDetalle.GetUpdateCommand()
        daLineasDetalle.DeleteCommand = BuilderLineasDetalle.GetUpdateCommand()

    End Sub

End Module

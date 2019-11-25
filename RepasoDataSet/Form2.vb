Public Class Form2
    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Dispose()

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtIdPedido.Text = pedidoActual
        txtFecha.Text = Date.Today

        cargarListView()

    End Sub

    Private Sub cargarListView()
        Dim tabla As DataTable = ds.Tables("Productos")
        For Each r As DataRow In tabla.Rows
            ComboBox1.Items.Add(r("Descripcion").ToString())
        Next
    End Sub
End Class
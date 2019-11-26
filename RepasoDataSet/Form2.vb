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

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim tabla As DataTable = ds.Tables("Productos")
        Dim linea As DataRow

        linea = devolverPrecio()

        txtPrecio.Text = linea.Item("PVP").ToString()
        txtUnidades.Text = ""

    End Sub

    Private Function devolverPrecio() As DataRow
        Dim tabla As DataTable = ds.Tables("Productos")
        For Each elem As DataRow In tabla.Rows

            If elem.Item("Descripcion") = ComboBox1.Text Then
                Return elem
            End If
        Next
    End Function

    Private Sub TxtUnidades_TextChanged(sender As Object, e As EventArgs) Handles txtUnidades.TextChanged

        If txtUnidades.Text <> "" Then
            txtTotalLinea.Text = Integer.Parse(txtPrecio.Text) * Integer.Parse(txtUnidades.Text)
        Else
            txtTotalLinea.Text = ""
        End If
    End Sub

    Private Sub TxtUnidades_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUnidades.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.keychar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
End Class
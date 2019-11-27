Public Class Form2
    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click

        Dim r As DataRow = ds.Tables("Pedidos").Rows.Find(CInt(txtIdPedido.Text))
        Dim unidades As Integer
        Dim total As Double
        Dim precio As Double

        For Each fila As DataRow In r.GetChildRows("LineasPedido")

        Next


        Me.Dispose()

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtIdPedido.Text = pedidoActual
        txtFecha.Text = Date.Today

        cargarCombo()
        Dim fila As DataRow = ds.Tables("Pedidos").NewRow
        fila("IdPedido") = pedidoActual
        fila("Fecha") = Date.Today


    End Sub

    Private Sub cargarCombo()
        Dim tabla As DataTable = ds.Tables("Productos")
        For Each r As DataRow In tabla.Rows
            ComboBox1.Items.Add(r("IdProducto").ToString())
        Next
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim tabla As DataTable = ds.Tables("Productos")
        Dim linea As DataRow = ds.Tables("Productos").Rows.Find(ComboBox1.SelectedText)



        txtPrecio.Text = linea("PVP").ToString()
        txtUnidades.Text = ""

    End Sub



    Private Sub TxtUnidades_TextChanged(sender As Object, e As EventArgs) Handles txtUnidades.TextChanged

        If txtUnidades.Text <> "" Then
            txtTotalLinea.Text = Double.Parse(txtPrecio.Text) * Double.Parse(txtUnidades.Text)
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

    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim tabla As DataTable = ds.Tables("LineasDetalle")
        Dim linea As DataRow
        linea = tabla.NewRow
        linea.BeginEdit()
        linea("IdDetalle") = BuscarMax2()
        linea("IdPedido") = pedidoActual
        linea("IdProducto") = ComboBox1.SelectedText
        linea("unidades") = Integer.Parse(txtUnidades.Text)
        linea("TotalLinea") = Double.Parse(txtTotalLinea.Text)
        linea.EndEdit()
        AñadirLinea()

        ds.Tables("LineasDetalle").Rows.Add(linea)
        daLineasDetalle.Update(ds, "LineasDetalle")

    End Sub

    Private Sub AñadirLinea()
        Dim it As ListViewItem = ListView1.Items.Add(linea("IdDetalle"))
        For i As Integer = 1 To ds.Tables("LineasDetalle").Columns.Count - 1
            it.SubItems.Add(linea(i))
        Next
    End Sub

    Private Function BuscarMax2() As Integer
        Dim tabla As DataTable = ds.Tables("LineasDetalle")
        Dim rows() As DataRow = tabla.Select("MAX(IdDetalle)")
        If rows.Length = 0 Then
            Return 1
        Else
            Dim numero As Object = rows(0).Item("IdDetalle")
            Return numero + 1
        End If
    End Function

End Class


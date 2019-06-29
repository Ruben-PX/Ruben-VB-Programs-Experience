Public Class SumaForm
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Text = "El resultado es " & NumericUpDown1.Value + NumericUpDown2.Value
    End Sub

    Private Sub Suma_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
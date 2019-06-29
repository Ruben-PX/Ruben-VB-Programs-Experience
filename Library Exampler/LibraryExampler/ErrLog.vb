Module ErrLog
    Dim ErrLog As New ArrayList
    Public Sub Err(s As String)
        ErrLog.Add(s)
        If ErrLog.Count = 1 Then
            Main.ErrBtn.Text = "Hay 1 Error"
            Main.ErrBtn.Enabled = True
            Main.ErrBtn.ForeColor = Color.Red
        Else
            Main.ErrBtn.Text = "Hay " & ErrLog.Count & " Errores"
        End If
    End Sub

    Public Sub Show()
        Dim txt As String = ""
        For Each err As String In ErrLog
            txt &= err & vbCrLf
        Next
        MessageBox.Show(txt, "Lista de errores")
    End Sub

End Module

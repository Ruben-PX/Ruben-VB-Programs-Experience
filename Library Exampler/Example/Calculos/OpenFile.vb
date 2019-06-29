Imports System.Windows.Forms

Public Class OpenFile
    Public Sub Exec()
        Dim op As New OpenFileDialog
        If op.ShowDialog = DialogResult.OK Then
            MsgBox("Has seleccionado el siguiente archivo: " & vbCrLf & op.FileName)
        End If
    End Sub
End Class

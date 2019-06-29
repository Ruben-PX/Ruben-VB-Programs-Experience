Public Class Main
    Dim exercises As New Dictionary(Of String, Object)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ErrBtn.Text = "No hay errores"
        ErrBtn.Enabled = False
        Ejercicios.Load()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If Not ListView1.SelectedItems.Count = 0 Then
            TextBox1.Text = ListView1.SelectedItems.Item(0).Text
            TextBox2.Text = ListView1.SelectedItems.Item(0).SubItems.Item(1).Text
        Else
            TextBox1.Text = ""
            TextBox2.Text = "GitHub Project: https://github.com/Ruben-PX/Ruben-VB-Programs-Experience"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListView1.SelectedItems.Count = 1 Then
            If ListView1.SelectedItems.Item(0).ImageIndex = 0 Then
                Me.Hide()
                exercises(ListView1.SelectedItems.Item(0).Name).ShowDialog()
                Me.Show()
            ElseIf ListView1.SelectedItems.Item(0).ImageIndex = 1 Then
                Me.Hide()
                exercises(ListView1.SelectedItems.Item(0).Name).Exec()
                Me.Show()
            Else
                ErrLog.Err("Error, se ha intentado ejecutar una propiedad que no existe")
            End If
        End If
    End Sub

    Private Sub ErrBtn_Click_1(sender As Object, e As EventArgs) Handles ErrBtn.Click
        ErrLog.Show()
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.SelectedItems.Count = 1 Then
            If ListView1.SelectedItems.Item(0).ImageIndex = 0 Then
                Me.Hide()
                exercises(ListView1.SelectedItems.Item(0).Name).ShowDialog()
                Me.Show()
            ElseIf ListView1.SelectedItems.Item(0).ImageIndex = 1 Then
                Me.Hide()
                exercises(ListView1.SelectedItems.Item(0).Name).Exec()
                Me.Show()
            Else
                ErrLog.Err("Error, se ha intentado ejecutar una propiedad que no existe")
            End If
        End If
    End Sub

    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles TextBox2.Click
        If TextBox2.Text = "GitHub Project: https://github.com/Ruben-PX/Ruben-VB-Programs-Experience" Then
            Clipboard.SetData(DataFormats.StringFormat, "https://github.com/Ruben-PX/Ruben-VB-Programs-Experience")
            MsgBox("Link copiado")
        End If
    End Sub

    ' Modifiers

    Public Sub AddGroup(Text As String) ' Añade un grupo de ejercicios
        If Not ListView1.Groups.Contains(New ListViewGroup(Text, Text)) Then
            ListView1.Groups.Add(Text, Text)
        End If
    End Sub

    Public Sub AddForm(Group As String, Value As String, Frm As Form, Descript As String) ' Añade un ejercicio nuevo
        Try
            Dim itm As ListViewItem
            itm = New ListViewItem
            itm.Group = ListView1.Groups.Item(Group)
            itm.Text = Value
            itm.Name = Group & "-" & Value
            itm.SubItems.Add(Descript)
            itm.ImageIndex = 0
            itm.ToolTipText = Descript
            exercises.Add(itm.Name, Frm)
            ListView1.Items.Add(itm)
        Catch ex As Exception
            Err(ex.Message)
        End Try
    End Sub

    Public Sub AddClass(Group As String, Value As String, CLS As Object, Descript As String)
        Try
            Dim itm As ListViewItem
            itm = New ListViewItem
            itm.Group = ListView1.Groups.Item(Group)
            itm.Text = Value
            itm.Name = Group & "-" & Value
            itm.SubItems.Add(Descript)
            itm.ImageIndex = 1
            itm.ToolTipText = Descript
            exercises.Add(itm.Name, CLS)
            ListView1.Items.Add(itm)
        Catch ex As Exception
            Err(ex.Message)
        End Try
    End Sub
End Class



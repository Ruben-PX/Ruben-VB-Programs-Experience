Imports System.ComponentModel
Imports System.IO
Public Class Form1
    Dim FileLoaded As Boolean = False
    Dim FileContents As String()
    Dim FileTreeNode As New TreeNode
    Dim FS As String

    Dim dic As New Dictionary(Of String, TreeNode)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        dic = New Dictionary(Of String, TreeNode)
        If FileLoaded Then
            FileTreeNode = New TreeNode
            Label1.Text = "Procesando"
            FS = TextBox2.Text
            Button2.Enabled = False
            BackgroundWorker1.RunWorkerAsync()
        Else
            dic = New Dictionary(Of String, TreeNode)
            ProgressBar1.Maximum = 1
            Scan(TextBox1.Text, TextBox2.Text)
            ProgressBar1.Value = 1
        End If

        'Idea principal
        'dic.Add("Ruben", TreeView1.Nodes.Add("Ruben"))
        'dic.Add("Ruben/Dexo", dic("Ruben").Nodes.Add("Dexo"))
        'dic.Add("Ruben/Dexo/Bass", dic("Ruben/Dexo").Nodes.Add("Bass"))
        'txt.Replace("/" & txt.Split("/").Last, "")
        'If dic.Keys.Contains("Dexo") Then
    End Sub
    Dim SearchTXT As TreeNode
    Private Sub Scan(s As String, splitter As String)
        SearchTXT = New TreeNode
        Dim txt2 As String = ""
        Dim LB1 As New ArrayList
        For i = 1 To s.Split(splitter).Count
            txt2 &= splitter & s.Split(splitter)(i - 1)
            LB1.Add(txt2)
        Next
        LB1(0) = LB1(0).ToString.Replace(splitter, "")

        For i = 1 To LB1.Count
            If Not dic.ContainsKey(LB1(i - 1)) Then
                If i - 1 = 0 Then
                    dic.Add(LB1(i - 1), SearchTXT.Nodes.Add(LB1(i - 1)))
                Else
                    dic.Add(LB1(i - 1), dic(LB1(i - 2)).Nodes.Add(LB1(i - 1).ToString.Split(splitter).Last))
                End If
            End If
        Next
        SearchTXT.Text = "Search " & TreeView1.Nodes.Count
        TreeView1.Nodes.Add(SearchTXT)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If FileLoaded Then
            TextBox1.Enabled = True
            TextBox1.Text = ""
            FileLoaded = Not FileLoaded
            Button2.Text = "Load TXT"
            ProgressBar1.Value = 0
            ProgressBar1.Maximum = 1
            If TextBox2.Text = "\" Then
                TextBox2.Text = "/"
            End If
        Else
            If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                TextBox1.Enabled = False
                FileContents = File.ReadAllLines(OpenFileDialog1.FileName)
                TextBox1.Text = OpenFileDialog1.FileName
                Button2.Text = "Unload TXT"
                FileLoaded = Not FileLoaded
                ProgressBar1.Maximum = FileContents.Count - 1
                Label1.Text = "Listo para procesar"
                TextBox2.Text = "\"
            End If
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim n As Long = 0
        For Each s As String In FileContents
            Dim txt2 As String = ""
            Dim LB1 As New ArrayList
            For i = 1 To s.Split(FS).Count
                txt2 &= FS & s.Split(FS)(i - 1)
                LB1.Add(txt2)
            Next
            LB1(0) = LB1(0).ToString.Replace(FS, "")

            For i = 1 To LB1.Count
                If Not dic.ContainsKey(LB1(i - 1)) Then
                    If i - 1 = 0 Then
                        dic.Add(LB1(i - 1), FileTreeNode.Nodes.Add(LB1(i - 1)))
                    Else
                        dic.Add(LB1(i - 1), dic(LB1(i - 2)).Nodes.Add(LB1(i - 1).ToString.Split(FS).Last))
                    End If
                End If
            Next
            BackgroundWorker1.ReportProgress(n)
            n += 1
        Next
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        ' Corregir file list (Repeat)
        FileTreeNode.Text = "File " & TreeView1.Nodes.Count
        TreeView1.Nodes.Add(FileTreeNode)
        ProgressBar1.Value = 0
        Button2.Enabled = True
    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Clipboard.SetData(DataFormats.StringFormat, "dir /S /B > text.txt")
        Label1.Text = "Command copied, Run into windows terminal"
    End Sub
End Class

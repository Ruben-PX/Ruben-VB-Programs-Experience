Imports System.IO
Public Class Form1
    Dim _Plugins As New Dictionary(Of String, Object)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NumericUpDown1.Maximum = Decimal.MaxValue
        NumericUpDown2.Maximum = Decimal.MaxValue
        NumericUpDown1.Minimum = Decimal.MinValue
        NumericUpDown2.Minimum = Decimal.MinValue

        LoadPlugins()
        If Not _Plugins.Keys.Count = 0 Then
            For Each plugin As String In _Plugins.Keys
                ComboBox1.Items.Add(plugin)
            Next
            ComboBox1.Text = ComboBox1.Items.Item(0)
            ToolStripStatusLabel1.Text = "Total de plugins cargados: " & _Plugins.Keys.Count
        Else
            MsgBox("No se ha cargado ningun plugin", MsgBoxStyle.Critical)
            Me.Close()
        End If
    End Sub

    Private Sub LoadPlugins()
        If Not Directory.Exists(Environment.CurrentDirectory & "\Plugins\") Then
            Directory.CreateDirectory(Environment.CurrentDirectory & "\Plugins\")
        End If

        If Directory.GetFiles(Environment.CurrentDirectory & "\Plugins\", "*.dll").Count = 0 Then
            MsgBox("No hay plugins existentes")
            Me.Close()
        End If

        Dim Plg As Object
        For Each file As String In Directory.GetFiles(Environment.CurrentDirectory & "\Plugins\", "*.dll")
            Plg = PluginLoad.Load(file)
            If IsNothing(Plg) Then
                MsgBox("Error al cargar plugin: " & file & vbCrLf & vbCrLf & "El plugin no ha devuelto un nombre")
            Else
                If TypeOf Plg.Name Is String Then
                    _Plugins.Add(Plg.Name, Plg)
                Else
                    MsgBox("No se ha cargado el plugin del directorio: " & file & vbCrLf & vbCrLf & "Porque no devuelve 'Name' como string")
                End If
            End If

        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Label1.Text = _Plugins.Item(ComboBox1.Text).Make(NumericUpDown1.Value, NumericUpDown2.Value)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If _Plugins.Item(ComboBox1.Text).Type = 0 Then
            NumericUpDown1.Enabled = False
            NumericUpDown2.Enabled = False
        ElseIf _Plugins.Item(ComboBox1.Text).Type = 1 Then
            NumericUpDown1.Enabled = True
            NumericUpDown2.Enabled = False
        ElseIf _Plugins.Item(ComboBox1.Text).Type = 2 Then
            NumericUpDown1.Enabled = True
            NumericUpDown2.Enabled = True
        End If
    End Sub
End Class

Imports System.Windows.Forms

Public Class Main
    Public ReadOnly Name As String = "Example Library"
    Public lst As New List(Of Ex)
    Public Function GetList() As List(Of Ex)
        add(0, "Calculos", "Suma", New SumaForm, "Una simple calculadora que suma dos numeros")
        add(1, "Operaciones con archivos", "OpenFileDialog", New OpenFile, "Abre un dialogo, para seleccionar un archivo")
        Return lst

    End Function
    Public Sub add(Type As Boolean, group As String, name As String, link As Object, Desc As String)
        ' Type:
        '   0 --> Link as Form
        '   1 --> Link as class
        Dim t As New Ex
        t.Type = Type
        t.group = group
        t.name = name
        t.link = link
        t.Desc = Desc
        lst.Add(t)
    End Sub
    Structure Ex
        Public Type As Boolean
        Public group As String
        Public name As String
        Public link As Object
        Public Desc As String
    End Structure

End Class

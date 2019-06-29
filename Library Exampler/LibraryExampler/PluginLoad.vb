Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Text

Module PluginLoad

    Public Function Load(DLLFile As String) As System.Object

        If File.Exists(DLLFile) Then
            Dim oType As System.Type
            Dim oAssembly As System.Reflection.Assembly
            Dim obj As Object

            ' Carga el archivo
            oAssembly = Assembly.LoadFrom(DLLFile)

            oType = oAssembly.GetType(RemoveDiacritics(oAssembly.GetName.Name).Replace(" ", "_") & ".Main")
            If IsNothing(oType) Then
                Return Nothing
            End If
            obj = Activator.CreateInstance(oType)
            If Not IsNothing(obj) Then
                Return obj
            Else
                Return Nothing
            End If

        End If
    End Function

    'https://social.msdn.microsoft.com/Forums/es-ES/vbes/thread/1d2f6d6b-762f-4d24-b5ac-abad3c7e4ae3
    Private Function RemoveDiacritics(stIn As String) As String

        Dim stFormD As String = stIn.Normalize(NormalizationForm.FormD)
        Dim sb As New StringBuilder()

        For ich As Integer = 0 To stFormD.Length - 1
            Dim uc As UnicodeCategory = CharUnicodeInfo.GetUnicodeCategory(stFormD(ich))
            If uc <> UnicodeCategory.NonSpacingMark Then
                sb.Append(stFormD(ich))
            End If
        Next

        Return (sb.ToString().Normalize(NormalizationForm.FormC))

    End Function

End Module

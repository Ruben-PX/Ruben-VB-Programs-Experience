Imports System.IO
Imports System.Reflection
Module PluginLoad

    Public Function Load(DLLFile As String) As System.Object

        If File.Exists(DLLFile) Then
            Dim oType As System.Type
            Dim oAssembly As System.Reflection.Assembly
            Dim obj As Object

            ' Carga el archivo
            oAssembly = Assembly.LoadFrom(DLLFile)

            oType = oAssembly.GetType(oAssembly.GetName.Name.Replace(" ", "_") & ".Main")
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

End Module

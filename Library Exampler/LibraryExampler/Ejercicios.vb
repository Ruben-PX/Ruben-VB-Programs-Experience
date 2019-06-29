Imports System.IO

Module Ejercicios
    Dim DLX As New Dictionary(Of String, Object)

    Public Sub Load()
        LoadDLL()
        ' Main.AddGroup(0, "Botones")
        ' Main.AddForm(0, "Ejercio 1", B_Ejercicio1, "Un botón que cambia el titulo de la ventana")
        ' Main.addClass(0, Ejercicio 2", ---, "Descript")
    End Sub

    Private Sub LoadDLL()
        ' Save DLLs in Dictionary
        If Directory.Exists(Environment.CurrentDirectory & "\Plugins\") And Directory.GetFiles(Environment.CurrentDirectory & "\Plugins\", "*.dll").Count > 0 Then
            For Each fn As String In Directory.GetFiles(Environment.CurrentDirectory & "\Plugins\", "*.dll")

                Dim plg As Object = PluginLoad.Load(fn)
                If Not IsNothing(plg) Then
                    DLX.Add(fn.Replace(Environment.CurrentDirectory & "\Plugins\", ""), plg)
                Else
                    ErrLog.Err("Plugin no compatible con el programa: " & fn)
                End If
            Next
        End If

        ' Load all exercises
        For Each list As String In DLX.Keys
            Dim l = DLX(list).GetList()
            For Each x In l
                Main.AddGroup(x.group)
                If x.Type Then
                    Main.AddClass(x.group, x.name, x.link, x.Desc)
                Else
                    Main.AddForm(x.group, x.name, x.link, x.Desc)
                End If
            Next
        Next
    End Sub
End Module

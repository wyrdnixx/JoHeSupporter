Imports System.Text
Imports SKGL

<TestClass()>
Public Class UnitTest1

    <TestMethod()>
    Public Sub CalculateMachineCode()
        Dim skgl As New SKGL.Generate
        Dim code As Integer = skgl.MachineCode

        Debug.WriteLine("Machine ID: " + code.ToString)

    End Sub

End Class

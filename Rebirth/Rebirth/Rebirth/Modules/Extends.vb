Imports System.Runtime.CompilerServices

Public Module Extends
    <Extension()> Public Function GetBytes(ByVal str As String) As Byte()
        Return New System.Text.ASCIIEncoding().GetBytes(str)
    End Function

    <Extension()> Public Function GetString(ByVal byts() As Byte) As String
        Return New System.Text.ASCIIEncoding().GetString(byts)
    End Function
End Module

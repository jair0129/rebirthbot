Public Class BnetIcon

    Public Width As Long
    Public Height As Long
    Public Data() As Byte
    Public Flags As Long
    Public Products() As Long
    Public SortIndex As Long
    Public p_Image As Image
    Public ImageIndex As Integer

    Public Function UseFor(ByVal usrFlags As Long, ByVal Product As Long) As Boolean
        If usrFlags <> 0 Then
            If (usrFlags And Me.Flags) = usrFlags Then Return True
        End If

        If Me.Products Is Nothing Then Return False

        If Me.Products.Length() = 0 Then Return False

        For Each element As Long In Me.Products
            If element = Product Then Return True
        Next

        Return False
    End Function

    Public Overrides Function ToString() As String
        Dim out As String = "Icon[flags=0x" & Hex(Me.Flags) & ", Width=" & Me.Width & ", Height=" & Me.Height
        If Products IsNot Nothing Then
            out &= ", products=["
            For Each element As Long In Products
                out &= Hex(element) + ","
            Next element
            out &= "]"
        End If
        out &= "]"
        Return out
    End Function

End Class

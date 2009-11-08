'RebirthBot
'Copyright (C) 2009 by Spencer Ragen
'
'Redistribution and use in source and binary forms, with or without modification, 
'are permitted provided that the following conditions are met: 
'
'1.) Redistributions of source code must retain the above copyright notice, 
'this list of conditions and the following disclaimer. 
'2.) Redistributions in binary form must reproduce the above copyright notice, 
'this list of conditions and the following disclaimer in the documentation 
'and/or other materials provided with the distribution. 
'3.) The name of the author may not be used to endorse or promote products derived 
'from this software without specific prior written permission. 
'
'See LICENSE.TXT that should have accompanied this software for full terms and 
'conditions.

''' <summary>
''' Icon descriptor class
''' </summary>
''' <remarks>Stores information about a single icon, including what games to 
''' and flags are to determine when to display the icon.  This is a fairly
''' direct port from Scott's (BNU-Camel's) BnetIcon class from BNU Bot 2.</remarks>
Public Class BnetIcon

    Public Width As Long
    Public Height As Long
    Public Data() As Byte
    Public Flags As Long
    Public Products() As Long
    Public SortIndex As Long
    Public p_Image As Image
    Public ImageIndex As Integer

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="usrFlags">Flags of the user</param>
    ''' <param name="Product">Product (long form) of the user</param>
    ''' <returns>True if the icon is to be used for that user, False otherwise</returns>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Get a string representation of the icon data
    ''' </summary>
    ''' <returns>String containing the width and height of the icon, as well as the flags and
    ''' any associated products.</returns>
    ''' <remarks></remarks>
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

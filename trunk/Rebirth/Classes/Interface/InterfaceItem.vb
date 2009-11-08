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
''' A descriptor for a customizable interface object.  Contains Font, foreground,
''' and background colors.
''' </summary>
Public Class InterfaceItem
    Private ci_Fore As ColorItem
    Private ci_Back As ColorItem
    Private ii_ElementName As String
    Private ii_ElementFont As System.Drawing.Font

    Public Sub New(ByVal ElementName As String, ByVal ElementFont As System.Drawing.Font, ByVal ForeColor As ColorItem, ByVal BackColor As ColorItem)
        ii_ElementName = ElementName
        ii_ElementFont = ElementFont
        ci_Fore = ForeColor
        ci_Back = BackColor
    End Sub

    Public Property NAME() As String
        Get
            Return ii_ElementName
        End Get
        Set(ByVal value As String)
            ii_ElementName = value
        End Set
    End Property

    Public Property FONT() As System.Drawing.Font
        Get
            Return ii_ElementFont
        End Get
        Set(ByVal value As System.Drawing.Font)
            ii_ElementFont = value
        End Set
    End Property

    Public Property FORECOLOR() As ColorItem
        Get
            Return ci_Fore
        End Get
        Set(ByVal value As ColorItem)
            ci_Fore = value
        End Set
    End Property

    Public Property BACKCOLOR() As ColorItem
        Get
            Return ci_Back
        End Get
        Set(ByVal value As ColorItem)
            ci_Back = value
        End Set
    End Property

End Class

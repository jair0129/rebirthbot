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
''' Class of a color item consisting of a System.Drawing.Color, color code,
''' color name, and a tag.
''' </summary>
Public Class ColorItem
    Private ci_Val As Color
    Private ci_Text As String
    Private ci_Param As String
    Private ci_Tag As String

    Public Sub New(ByVal Value As Color, ByVal Text As String, ByVal Param As String)
        Me.ci_Val = Value
        Me.ci_Text = Text
        Me.ci_Param = Param
        Me.ci_Tag = ""
    End Sub

    Public Property VALUE() As Color
        Get
            Return ci_Val
        End Get
        Set(ByVal value As Color)
            ci_Val = value
        End Set
    End Property

    Public Property TEXT() As String
        Get
            Return ci_Text
        End Get
        Set(ByVal value As String)
            ci_Text = value
        End Set
    End Property

    Public Property PARAM() As String
        Get
            Return ci_Param
        End Get
        Set(ByVal value As String)
            ci_Param = value
        End Set
    End Property

    Public Property TAG() As String
        Get
            Return ci_Tag
        End Get
        Set(ByVal value As String)
            ci_Tag = value
        End Set
    End Property
End Class

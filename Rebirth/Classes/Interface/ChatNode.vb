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
''' A single element of a ChatItem.  ChatNode consists of 1 color and 1 string
''' </summary>
''' <remarks>It takes many ChatNodes to form the complicated ChatItems.
''' This class is also so remarkably simple that I'm not going to bother with
''' commenting it properly.</remarks>
Public Class ChatNode
    Private m_Color As System.Drawing.Color
    Private m_Text As String

    Public Sub New(ByVal cColor As System.Drawing.Color, ByVal Text As String)
        m_Color = cColor
        m_Text = Text
    End Sub

    Public Sub New(ByVal cColor As ColorItem, ByVal Text As String)
        m_Color = cColor.VALUE
        m_Text = Text
    End Sub

    Public ReadOnly Property TEXTCOLOR()
        Get
            Return m_Color
        End Get
    End Property

    Public ReadOnly Property TEXTSTRING()
        Get
            Return m_Text
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return m_Text
    End Function
End Class

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
''' A descriptor of a language entry.  Contains the language code and language
''' name in English, and the local language name.
''' </summary>
''' <remarks>The local language name is the name of the language in that
''' language. For instance: Русский for Russian</remarks>
Public Class LanguageItem
    Private m_langName As String
    Private m_localName As String
    Private m_langCode As String

    Public Sub New(ByVal LanguageName As String, ByVal LanguageCode As String)
        Me.m_langName = LanguageName
        Me.m_langCode = LanguageCode
    End Sub

    Public ReadOnly Property NAME() As String
        Get
            Return Me.m_langName
        End Get
    End Property

    Public ReadOnly Property CODE() As String
        Get
            Return Me.m_langCode
        End Get
    End Property

    Public Property LOCALNAME() As String
        Get
            Return m_localName
        End Get
        Set(ByVal value As String)
            m_localName = value
        End Set
    End Property
End Class

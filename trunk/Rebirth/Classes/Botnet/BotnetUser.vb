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


'' This is not yet implemented, and as such, is not commented properly yet.
Public Class BotnetUser

    Private bu_ID As Long
    Private bu_Access As Long
    Private bu_Admin As Long
    Private bu_IP As String
    Private bu_bnetName As String
    Private bu_bnetChannel As String
    Private bu_bnetServer As String
    Private bu_botnetName As String
    Private bu_botnetDB As String

    Public Property BOTID() As Long
        Get
            Return bu_ID
        End Get
        Set(ByVal Value As Long)
            bu_ID = Value
        End Set
    End Property

    Public Property ACCESS() As Long
        Get
            Return bu_Access
        End Get
        Set(ByVal Value As Long)
            bu_Access = Value
        End Set
    End Property

    Public Property ADMIN() As Long
        Get
            Return bu_Admin
        End Get
        Set(ByVal Value As Long)
            bu_Admin = Value
        End Set
    End Property

    Public Property IP() As String
        Get
            Return bu_IP
        End Get
        Set(ByVal Value As String)
            bu_IP = Value
        End Set
    End Property

    Public Property BNETNAME() As String
        Get
            Return bu_bnetName
        End Get
        Set(ByVal Value As String)
            bu_bnetName = Value
        End Set
    End Property

    Public Property BNETCHANNEL() As String
        Get
            Return bu_bnetChannel
        End Get
        Set(ByVal Value As String)
            bu_bnetChannel = Value
        End Set
    End Property

    Public Property BNETSERVER() As String
        Get
            Return bu_bnetServer
        End Get
        Set(ByVal Value As String)
            bu_bnetServer = Value
        End Set
    End Property

    Public Property BOTNETNAME() As String
        Get
            Return bu_botnetName
        End Get
        Set(ByVal Value As String)
            bu_botnetName = Value
        End Set
    End Property

    Public Property BOTNETDATABASE() As String
        Get
            Return bu_botnetDB
        End Get
        Set(ByVal Value As String)
            bu_botnetDB = Value
        End Set
    End Property
End Class

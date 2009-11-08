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

Public Class SentBotnetData
    Private msg_Text As String
    Private msg_Chat As Boolean
    Private pkt_ID As Byte
    Private pkt_Name As String

    Public Sub New(ByVal pID As Byte)
        ID = pID
        msg_Chat = False
    End Sub

    Public Sub New(ByVal msg As String)
        msg_Text = msg
        msg_Chat = True
    End Sub

    Public Property TEXT() As String
        Get
            Return msg_Text
        End Get
        Set(ByVal value As String)
            msg_Text = value
        End Set
    End Property

    Public Property CHAT() As Boolean
        Get
            Return msg_Chat
        End Get
        Set(ByVal value As Boolean)
            msg_Chat = value
        End Set
    End Property

    Public ReadOnly Property NAME() As String
        Get
            Return pkt_Name
        End Get
    End Property

    Public Property ID() As Byte
        Get
            Return pkt_ID
        End Get
        Set(ByVal value As Byte)
            pkt_ID = value
            Select Case value
                Case &H0 : pkt_Name = "PACKET_IDLE"
                Case &H1 : pkt_Name = "PACKET_LOGON"
                Case &H2 : pkt_Name = "PACKET_STATSUPDATE"
                Case &H3 : pkt_Name = "PACKET_DATABASE"
                Case &H4 : pkt_Name = "PACKET_MESSAGE"
                Case &H5 : pkt_Name = "PACKET_CYCLE" ' defunct
                Case &H6 : pkt_Name = "PACKET_USERINFO"
                Case &H7 : pkt_Name = "PACKET_BROADCASTMESSAGE" ' this will never be PACKET_USERLOGGINGOFF because that is only received
                Case &H8 : pkt_Name = "PACKET_COMMAND"
                Case &H9 : pkt_Name = "PACKET_CHANGEDBPASSWORD" ' similar to above, PACKET_BOTNETVERSIONACK is never sent
                Case &HA : pkt_Name = "PACKET_BOTNETVERSION"
                Case &HB : pkt_Name = "PACKET_BOTNETCHAT"
                Case &HC : pkt_Name = "PACKET_UNKNOWN_C"
                Case &HD : pkt_Name = "PACKET_ACCOUNT"
                Case &H10 : pkt_Name = "PACKET_CHATDROPOPTIONS"
            End Select
        End Set
    End Property
End Class

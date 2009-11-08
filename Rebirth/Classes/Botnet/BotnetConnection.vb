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

Imports MBNCSUtil
Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Net.Dns
Imports System.Environment
Imports System.Net.Sockets
Imports System.Runtime.InteropServices

Public Class BotnetConnection

#Region "packet enum"
    Private Enum BotnetPackets
        PACKET_IDLE = &H0
        PACKET_LOGON = &H1
        PACKET_STATSUPDATE = &H2
        PACKET_DATABASE = &H3
        PACKET_MESSAGE = &H4
        PACKET_CYCLE = &H5
        PACKET_USERINFO = &H6
        PACKET_USERLOGGINGOFF = &H7
        PACKET_BROADCASTMESSAGE = &H7
        PACKET_COMMAND = &H8
        PACKET_CHANGEDBPASSWORD = &H9
        PACKET_BOTNETVERSIONACK = &H9
        PACKET_BOTNETVERSION = &HA
        PACKET_BOTNETCHAT = &HB
        PACKET_UNKNOWN_C = &HC
        PACKET_ACCOUNT = &HD
        PACKET_CHATDROPOPTIONS = &H10
    End Enum
#End Region

#Region "events"
    Public Event BOTNET_SocketError(ByVal errCode As String, ByVal errMessage As String)
    Public Event BOTNET_Error_ServerClosedConnection()
    Public Event BOTNET_Connected()
    Public Event BOTNET_Disconnected()
    Public Event BOTNET_Timeout()
    'Public Event BOTNET_UnhandledPacket(ByVal Data() As Byte)
    Public Event BOTNET_UnhandledPacket(ByVal Data As String)
    Public Event BOTNET_ServerLogon_Passed()
    Public Event BOTNET_ServerLogon_Failed()
    Public Event BOTNET_AccountLogon_Passed()
    Public Event BOTNET_AccountLogon_Failed()
    Public Event BOTNET_AccountCreate_Passed()
    Public Event BOTNET_AccountCreate_Failed()
    Public Event BOTNET_DatabaseLogon_Passed()
    Public Event BOTNET_DatabaseLogon_Failed()
    Public Event BOTNET_StatsUpdate_Okay()
    Public Event BOTNET_StatsUpdate_Failed()
    Public Event BOTNET_CreatingAccount(ByVal aName As String)
    Public Event BOTNET_ProtocolVersion(ByVal Version As Long)
    Public Event BOTNET_ServerVersion(ByVal Version As Long, ByVal UseVer As Long)
    Public Event BOTNET_UserInfo(ByVal bnUser As BotnetUser)
    Public Event BOTNET_UserChangedChannel(ByVal bnUser As String, ByVal bnID As Long, ByVal NewChannel As String)
    Public Event BOTNET_UserChat(ByVal bnName As String, ByVal bnID As Long, ByVal bnMessage As String)
    Public Event BOTNET_UserEmote(ByVal bnName As String, ByVal bnID As Long, ByVal bnMessage As String)
    Public Event BOTNET_UserWhisperChat(ByVal bnName As String, ByVal bnID As Long, ByVal bnMessage As String)
    Public Event BOTNET_UserWhisperEmote(ByVal bnName As String, ByVal bnID As Long, ByVal bnMessage As String)
    Public Event BOTNET_UserLoggingOff(ByVal bnName As String, ByVal bnID As Long)
    Public Event BOTNET_MyID(ByVal bnName As String, ByVal bnID As Long)
#End Region

#Region "variable declares"
    Private sck As Socket
    Private USERS As BotnetUserlist

    Private _Version As Long
    Private _ServerVersion As Long

    Private AttemptCreate As Boolean

    Private nullTimer As Threading.Timer

    Private MyID As Long

    Private ipHost As IPHostEntry
    Private ipEnd As IPEndPoint
    Private async As New AsyncCallback(AddressOf BOTNET_ConnectionCompleted)

    Private cfg As BotnetConfig

    Public Stats_Username As String
    Public Stats_Channel As String
    Public Stats_ServerIP As Long

    Private Messages As List(Of SentBotnetData)
#End Region

#Region "constructors"
    Public Sub New(ByVal ncfg As BotnetConfig)
        Messages = New List(Of SentBotnetData)
        Me.cfg = ncfg
    End Sub
#End Region

#Region "public members"
    Public Sub Connect()
        nullTimer = New Threading.Timer(New Threading.TimerCallback(AddressOf BOTNET_PACKET_IDLE), Nothing, 1800000, 1800000)
        Me._Version = 1
        Me._ServerVersion = 1
        USERS = New BotnetUserlist
        sck = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        ipHost = GetHostEntry(cfg.SERVER)
        ipEnd = New IPEndPoint(ipHost.AddressList(0), cfg.PORT)
        sck.BeginConnect(ipEnd, async, sck)
    End Sub

    Public Sub Disconnect()
        If sck Is Nothing Then Exit Sub
        If sck.Connected Then
            sck.Shutdown(SocketShutdown.Both)
            sck.Close()
            sck = Nothing
        End If
    End Sub

    Public Function IsConnected() As Boolean
        If sck Is Nothing Then
            Return False
        Else
            Return sck.Connected
        End If
    End Function

    Public Sub StatsUpdate()
        Call SEND_PACKET_STATSUPDATE()
    End Sub

    Public Sub BOTNET_PACKET_IDLE(ByVal state As Object)
        SendPacket(New BotnetPacket(BotnetPackets.PACKET_IDLE).GetData())
    End Sub
#End Region

#Region "connected/parse dispatch"
    Private Sub BOTNET_ConnectionCompleted(ByVal async As IAsyncResult)
        Dim buff(1023) As Byte
        Dim asyncnew As New AsyncCallback(AddressOf BOTNET_DataArrival)

        If sck.Connected = True Then
            sck.EndConnect(async)
            sck.BeginReceive(buff, 0, sck.Available, SocketFlags.None, asyncnew, sck)
            Call SEND_PACKET_LOGON_SERVER()
            RaiseEvent BOTNET_Connected()
        End If
    End Sub

    Private Sub BOTNET_DataArrival(ByVal async As IAsyncResult)
        Dim i As Integer
        Dim array As New List(Of Byte)
        Dim pLen As Integer
        Try
            If sck Is Nothing Then Exit Sub
            If sck.Connected = True Then
                Try
                    sck.EndReceive(async)
                Catch ex As Exception

                End Try

                Dim buff(sck.Available - 1) As Byte
                Dim asyncnew As New AsyncCallback(AddressOf BOTNET_DataArrival)

                sck.BeginReceive(buff, 0, sck.Available, SocketFlags.None, asyncnew, sck)
                If buff.Length > 0 Then
                    For i = 0 To buff.Length - 1
                        array.Add(buff(i))
                    Next
                    pLen = GetWORD(buff(2), buff(3))
                    Do Until (array.Count = 0)
                        Dim newbuff(1024) As Byte
                        pLen = GetWORD(array(2), array(3))
                        For i = 0 To pLen - 1
                            newbuff(i) = array(i)
                        Next
                        array.RemoveRange(0, pLen)
                        ParseBOTNET(newbuff)
                    Loop
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ParseBOTNET(ByVal Data() As Byte)
        Dim pkt As New BotnetReader(Data)
        'Debug.Print(pkt.ToString())
        If Data(0) <> &H0 Then
            Select Case pkt.PacketID
                Case BotnetPackets.PACKET_IDLE ' do nothing
                Case BotnetPackets.PACKET_LOGON : PARSE_PACKET_LOGON(Data)
                Case BotnetPackets.PACKET_STATSUPDATE : PARSE_PACKET_STATSUPDATE(Data)
                    'Case BotnetPackets.PACKET_DATABASE : PARSE_UNKNOWN(Data)
                    'Case BotnetPackets.PACKET_MESSAGE : PARSE_UNKNOWN(Data)
                    'Case BotnetPackets.PACKET_CYCLE : PARSE_UNKNOWN(Data)
                Case BotnetPackets.PACKET_USERINFO : PARSE_PACKET_USERINFO(Data)
                Case BotnetPackets.PACKET_USERLOGGINGOFF : PARSE_PACKET_USERLOGGINGOFF(Data)
                    'Case &H8 'PARSE_UNKNOWN(Data)
                Case BotnetPackets.PACKET_BOTNETVERSIONACK : PARSE_PACKET_BOTNETVERSIONACK(Data)
                Case BotnetPackets.PACKET_BOTNETVERSION : PARSE_PACKET_BOTNETVERSION(Data)
                    'Case &HB : PARSE_UNKNOWN(Data)
                    'Case BotnetPackets.PACKET_UNKNOWN_C ' unknown
                Case BotnetPackets.PACKET_ACCOUNT : PARSE_PACKET_ACCOUNT(Data)
                Case BotnetPackets.PACKET_BOTNETCHAT : PARSE_PACKET_BOTNETCHAT(Data)
                    'Case &HE : PARSE_UNKNOWN(Data)
                    'Case &HF : PARSE_UNKNOWN(Data)
                    'Case &H10 : PARSE_UNKNOWN(Data)
                Case Else
                    'PARSE_UNKNOWN(Data)
                    RaiseEvent BOTNET_UnhandledPacket(pkt.ToString())
            End Select
        End If
    End Sub
#End Region

#Region "packet parsers"

    ' packet 0x01
    ' sequence: 2, received
    Private Sub PARSE_PACKET_LOGON(ByVal Data() As Byte)
        '(send to client) id 0x01: log on status
        'Contents:
        '	(BOOL) status : 0=FAIL, 1=OK.  In version 4, this message always
        '		indicates success.
        '	(4) (DWORD) client's IP address : clients inside a NAT device
        '		may find this useful for learning their external address.
        'Response: On success, the client should proceed to logon and acquire its
        'database, as appropriate.

        Dim pkt As New BotnetReader(Data)
        Dim status As Boolean = pkt.ReadBoolean()
        ' i will grab the ip later

        If status Then
            RaiseEvent BOTNET_ServerLogon_Passed()
        Else
            RaiseEvent BOTNET_ServerLogon_Failed()
        End If
    End Sub

    ' packet 0x0a
    ' sequence: 4, received
    Private Sub PARSE_PACKET_BOTNETVERSION(ByVal Data() As Byte)
        '(send to client) id 0x0a: botnet server version
        'Contents:
        '	(DWORD) version
        '       Version(Information)
        'Version 1 supports all packets 0x00 through 0x0b.
        'Version 2 supports messages 0x0c and 0x0d.
        'Version 4 supports message 0xa to server.

        Dim pkt As New BotnetReader(Data)
        Dim ver As Long = pkt.ReadUInt32()

        Me._ServerVersion = ver
        RaiseEvent BOTNET_ServerVersion(Me._ServerVersion, Me._Version)

        If Me.AttemptCreate Then
            Call SEND_PACKET_ACCOUNT_CREATE()
        Else
            Call SEND_PACKET_ACCOUNT_LOGON()
        End If
    End Sub

    ' packet 0x0d
    ' sequence: 5~, received
    ' notes: this packet will be 5th during the initial connection sequence,
    '        but it will appear any time after PACKET_ACCOUNT is sent.  it
    '        will appear 7th if the result of 5th is failure, as it will
    '        send PACKET_ACCOUNT again as a creation request
    Private Sub PARSE_PACKET_ACCOUNT(ByVal Data() As Byte)
        '(send to client) id 0x0d: account management reply
        'Contents:
        '	(DWORD)	subcommand --- This is the same subcommand as the message being replied to.
        '	(BOOL)	: Success/Failure for subcommand
        'Response: None.

        Dim pkt As New BotnetReader(Data)
        Dim cmd As Long = pkt.ReadUInt32()
        Dim res As Boolean = pkt.ReadBoolean()

        Select Case cmd
            Case 0 ' account logon
                If res Then
                    RaiseEvent BOTNET_AccountLogon_Passed()
                    Call SEND_PACKET_USERINFO()
                Else
                    RaiseEvent BOTNET_AccountLogon_Failed()
                    Me.AttemptCreate = True
                    RaiseEvent BOTNET_CreatingAccount(Me.cfg.ACCOUNT)
                    Call Me.Disconnect()
                    Call Me.Connect()
                End If
            Case 1 ' account change password
                ' i dont send this yet, so i should never get it
            Case 2 ' account create
                If res Then
                    RaiseEvent BOTNET_AccountCreate_Passed()
                    Call SEND_PACKET_ACCOUNT_LOGON()
                Else
                    RaiseEvent BOTNET_AccountCreate_Failed()
                End If
        End Select
    End Sub

    ' packet 0x02
    ' sequence: 7*, received
    ' notes: received 7th during normal initial connection, 11th if initial
    '        account login failed and subsequent create/login passed
    Private Sub PARSE_PACKET_STATSUPDATE(ByVal Data() As Byte)
        '(send to client) id 0x02: update bot status
        '(DWORD) status : 0=FAIL, 1=OK.  In version 4, this message is always success.
        '(DWORD) Optional dword which specifies administrative access, if it has
        '	changed from its previous value.  If this dword is not present,
        '	the client should assume that the access has not changed; the
        '	default access is 0.  As noted in the documentation to message 6,
        '	administrative access is expressed as a 32-bit mask of features
        '	available.  Flags C and L enable insertion of client IP address
        '	in message 6, if the client has also negotiated to use revision
        '	1 packets (that is, fields marked 4.1 are in use).
        '
        Dim pkt As New BotnetReader(Data)
        Dim status As Long = pkt.ReadUInt32()
        If status = 1 Then
            RaiseEvent BOTNET_StatsUpdate_Okay()
        Else
            RaiseEvent BOTNET_StatsUpdate_Failed()
        End If
    End Sub

    Private Sub PARSE_PACKET_BOTNETVERSIONACK(ByVal Data() As Byte)
        '(send to client) id 0x09: acknowledge communication version
        'Contents:
        '	(DWORD) communication version.  This message is sent to confirm
        '	acceptance of msg 0x0a.  All subsequent messages will be formed in
        '	this style.  That is, clients should not change parsing methods until
        '	the server confirms the new style.
        'Response: None()

        Dim pkt As New BotnetReader(Data)
        Me._Version = pkt.ReadUInt32()
        RaiseEvent BOTNET_ProtocolVersion(Me._Version)
    End Sub

    Private Sub PARSE_PACKET_USERINFO(ByVal Data() As Byte)
        '(send to client) id 0x06: botnet bot information
        'Contents:
        '	(DWORD) bot id
        '	(4.1) (DWORD) database access flags
        '		1 = read
        '		2 = write
        '		4 = restricted access
        '	(4.1) (DWORD) administrative capabilities
        '		Specified in Zerobot Traditional Flags Format (ZTFF):
        '		A = superuser, can perform any administrative action
        '		B = broadcast, may use talk-to-all
        '		C = connection, may administer botnet connectivity
        '		D = database, may create and maintain databases
        '		I = ID control, may create and modify hub IDs
        '		S = botnet service
        '	(4.1) (Admin only) (DWORD) IP address of the bot being described
        '	(STRING:20) bot name
        '	(STRING:*) bot channel
        '	(DWORD) bot server
        '	(2) (STRING:16) unique account name
        '	(3) (STRING:*) database

        Dim pkt As New BotnetReader(Data)
        Dim bu As New BotnetUser

        If pkt.Length() < 5 Then Exit Sub

        bu.BOTID = pkt.ReadUInt32()
        bu.ACCESS = pkt.ReadUInt32()
        bu.ADMIN = pkt.ReadUInt32()
        bu.IP = "0.0.0.0"
        bu.BNETNAME = pkt.ReadCString()
        bu.BNETCHANNEL = pkt.ReadCString()
        bu.BNETSERVER = New System.Net.IPEndPoint(pkt.ReadUInt32(), 6112).Address.ToString()
        bu.BOTNETNAME = pkt.ReadCString()
        bu.BOTNETDATABASE = pkt.ReadCString()

        If USERS.IsUser(bu.BOTID) Then
            Debug.Print("found user " & bu.BOTID & " - " & bu.BOTNETNAME)
            USERS.UpdateUser(bu)
            RaiseEvent BOTNET_UserChangedChannel(bu.BOTNETNAME, bu.BOTID, bu.BNETCHANNEL)
        Else
            USERS.AddUser(bu)
            RaiseEvent BOTNET_UserInfo(bu)
            If bu.BOTNETNAME = Me.cfg.ACCOUNT Then
                Me.MyID = bu.BOTID
                RaiseEvent BOTNET_MyID(bu.BOTNETNAME, bu.BOTID)
            End If
        End If
        bu = Nothing
    End Sub

    Private Sub PARSE_PACKET_BOTNETCHAT(ByVal Data() As Byte)
        '(send to server) id 0x0b: botnet chat
        'Contents:
        '	(DWORD) command
        '		0	: message to all bots
        '		1	: message to bots on the same database
        '		2	: message to bot specified by id.
        '	(DWORD) action	: 0x00=talk, 0x01=emote, any other is dropped
        '	(DWORD) id	: for command 0x02, id of bot to send to, otherwise ignored.
        '	(STRING:496) message: blank messages are dropped
        'Response: the server generates 0xb to the specified other clients.  No
        'response is sent to the sending client.
        '
        '(send to client) id 0x0b: botnet chat
        'Contents:
        '	(DWORD) command	: same as 0xb to server
        '	(DWORD) action	: same as 0xb to server
        '	(DWORD) id		: id of source bot (for all commands)
        '	(STRING) message	: chat message text
        'Response: None.

        Dim pkt As New BotnetReader(Data)
        Dim cmd As Long = pkt.ReadUInt32()
        Dim act As Long = pkt.ReadUInt32()
        Dim id As Long = pkt.ReadUInt32()
        Dim msg As String = pkt.ReadCString()
        Dim username As String = USERS.GetBotnetNameByID(id)

        Select Case cmd
            Case 0, 1
                If act = 0 Then
                    RaiseEvent BOTNET_UserChat(username, id, msg)
                Else
                    RaiseEvent BOTNET_UserEmote(username, id, msg)
                End If
            Case 2 ' essentially a whisper
                If act = 0 Then
                    RaiseEvent BOTNET_UserWhisperChat(username, id, msg)
                Else
                    RaiseEvent BOTNET_UserWhisperEmote(username, id, msg)
                End If
        End Select
    End Sub

    Public Sub PARSE_PACKET_USERLOGGINGOFF(ByVal Data() As Byte)
        '(send to client) id 0x07: bot disconnecting from botnet
        '(DWORD) bot id

        Dim pkt As New BotnetReader(Data)
        Dim id As Long = pkt.ReadUInt32()
        Dim uname As String = USERS.GetBotnetNameByID(id)
        If USERS.RemoveUser(id) Then
            RaiseEvent BOTNET_UserLoggingOff(uname, id)
        End If
    End Sub
#End Region

#Region "send routines"
    Private Sub SendPacket(ByVal Data() As Byte)
        If sck Is Nothing Then
            RaiseEvent BOTNET_Error_ServerClosedConnection()
            Messages.Clear()
            nullTimer = Nothing
        End If

        Messages.Add(New SentBotnetData(Data(1)))

        Try
            sck.BeginSend(Data, 0, Data.Length(), SocketFlags.None, New AsyncCallback(AddressOf BOTNET_SendComplete), sck)
        Catch ex As SocketException
            RaiseEvent BOTNET_SocketError(ex.ErrorCode.ToString(), ex.Message)
            RaiseEvent BOTNET_Error_ServerClosedConnection()
        End Try
    End Sub

    Private Sub BOTNET_SendComplete(ByVal async As IAsyncResult)
        'Dim item As SentBnetData = Messages(0)
        'Messages.Remove(item)
        'If item.CHAT Then
        'If item.TEXT <> "" Then
        ' RaiseEvent BotNET_SendChatMessage(TrueUsername, item.TEXT)
        ' End If
        ' Else
        ' RaiseEvent BotNET_PacketSent(item)
        ' End If
    End Sub

    ' packet 0x01
    ' sequence: 1, sent
    Private Sub SEND_PACKET_LOGON_SERVER()
        '(send to server) id 0x01: log on to botnet
        'Contents:
        '	(STRING:32) bot id
        '	(STRING:64) hub password

        Dim pck As New BotnetPacket(BotnetPackets.PACKET_LOGON)
        pck.InsertCString("RebirthBot" & BOT_VERSION)
        pck.InsertCString(Me.cfg.SERVERPWD)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ' packet 0x0a
    ' sequence: 3, sent
    Private Sub SEND_PACKET_BOTNETVERSION()
        '(send to server) id 0x0a: specify communication version and client
        '        capabilities()
        'Contents:
        '	(DWORD) communication version.  Valid values are 0 (the default), or 1.
        '	Messages which have conditionally added fields (as identified by the
        '	4.X syntax) will contain fields for which X is not above this value.
        '	(DWORD) client capabilities.  Currently only bit 0 is defined.  If set,
        '	the client awaits server confirmation of database changes.  If clear,
        '	the client updates the local ACL immediately and expects the server to
        '	countermand prohibited changes.
        'Response: The server updates the communication version and sends 0x9 to the
        '	client.  Message 0xa may be sent at any time, and may be resent if the
        '	client desires to change the negotiation version.

        Dim pck As New BotnetPacket(BotnetPackets.PACKET_BOTNETVERSION)
        pck.InsertUInt32(Me._Version)
        pck.InsertUInt32(1)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ' packet 0x0d
    ' sequence 4~, sent
    ' notes: this packet is used for logon, password change, and create account.
    '        this specific sub sends logon, and is sent 4th, though it can be
    '        sent anytime after server logon
    Private Sub SEND_PACKET_ACCOUNT_LOGON()
        '(send to server) id 0x0d: account management
        'Contents:
        '	(DWORD) subcommand
        '	(STRING:16) account name	: name to acquire
        '	* Subcommand 0x00: Login:
        '		(STRING:96) account password
        '	* Subcommand 0x01: Change password:
        '		(STRING:96) old password
        '		(STRING:96) new password
        '	* Subcommand 0x02: Account create:
        '		(STRING:96) account password
        'Response: The server returns the result code in 0xd.
        'Other subcommand values are reserved for future use.
        'The server ensures that there is never more than one user online using a
        'given account name.  Account names are restricted to alphanumeric
        'characters, brackets, underscores, and dashes.  Invalid characters
        'result in failure to create/logon the account.

        Dim pck As New BotnetPacket(BotnetPackets.PACKET_ACCOUNT)
        pck.InsertUInt32(0) ' login command
        pck.InsertCString(Me.cfg.ACCOUNT)
        pck.InsertCString(Me.cfg.ACCOUNTPWD)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ' packet 0x0d
    ' sequence: (6)~, sent
    ' notes: this is sent during the initial connection sequence if the result 
    '        of login is false, otherwise it is sent upon request (NOT IMPLEMENTED)
    Private Sub SEND_PACKET_ACCOUNT_CREATE()
        Me.AttemptCreate = False
        Dim pck As New BotnetPacket(BotnetPackets.PACKET_ACCOUNT)
        pck.InsertUInt32(2) ' create command
        pck.InsertCString(Me.cfg.ACCOUNT)
        pck.InsertCString(Me.cfg.ACCOUNTPWD)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ' packet 0x02
    ' sequence: 6*, sent
    ' notes: this is sent 6th if login passes first time.  if login fails, it is sent
    '        10th (after create relogin attempt) if the second login passes
    Private Sub SEND_PACKET_STATSUPDATE()
        '(send to server) id 0x02: update bot stats
        'Contents:
        '	(STRING:20) unique username on battle.net : This string provides room
        '		to specify a name mangle ("Someone#2"), but not to specify
        '		gateway.
        '	(STRING:36) current channel on battle.net : By convention,
        '		"<Not logged on>" is used to indicate clients which are not
        '		presently connected.
        '	(DWORD) battle.net server ip
        '	(STRING:96) database id (which database to use) : includes database
        '		password.  Use the following format: "name password\0".
        '	(DWORD) cycle status : 0=NotCycling, 1=Cycling
        'Response: client receives packet id 0x02.  Client is also assigned a unique
        '	botnet identification number.  Prior to receiving this number, clients
        '	are prohibited from performing actions which cause interaction with
        '	other users.

        Dim pck As New BotnetPacket(BotnetPackets.PACKET_STATSUPDATE)
        If Me.cfg.HIDEUSERNAME Then
            pck.InsertCString("<Hidden>")
        Else
            pck.InsertCString(Me.Stats_Username)
        End If

        If Me.cfg.HIDECHANNEL Then
            pck.InsertCString("<Hidden>")
        Else
            pck.InsertCString(Me.Stats_Channel)
        End If

        If Me.cfg.HIDESERVER Then
            pck.InsertUInt32(0)
        Else
            pck.InsertUInt32(Me.Stats_ServerIP)
        End If
        pck.InsertCString(Me.cfg.DATABASE & " " & Me.cfg.DATABASEPWD)
        pck.InsertUInt32(0) ' cycling is defunct, so always send disable
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    Private Sub SEND_PACKET_USERINFO()
        ' empty packet
        SendPacket(New BotnetPacket(BotnetPackets.PACKET_USERINFO).GetData())
    End Sub

    Public Sub SendChat(ByVal message As String)
        '(send to server) id 0x0b: botnet chat
        'Contents:
        '	(DWORD) command
        '		0	: message to all bots
        '		1	: message to bots on the same database
        '		2	: message to bot specified by id.
        '	(DWORD) action	: 0x00=talk, 0x01=emote, any other is dropped
        '	(DWORD) id	: for command 0x02, id of bot to send to, otherwise ignored.
        '	(STRING:496) message: blank messages are dropped
        'Response: the server generates 0xb to the specified other clients.  No
        'response is sent to the sending client.
        Dim pck As New BotnetPacket(BotnetPackets.PACKET_BOTNETCHAT)
        pck.InsertUInt32(1)
        pck.InsertUInt32(0)
        pck.InsertUInt32(0)
        pck.InsertCString(message)
        SendPacket(pck.GetData())
        pck = Nothing

        Dim k As New BotnetUser
        k = USERS.GetEntryNameByID(Me.MyID)
        RaiseEvent BOTNET_UserChat(k.BOTNETNAME, k.BOTID, message)
    End Sub
#End Region
End Class

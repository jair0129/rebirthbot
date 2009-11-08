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

Imports MBNCSUtil
Imports MBNCSUtil.Net
Imports MBNCSUtil.Data
Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Net.Dns
Imports System.Environment
Imports System.Net.Sockets
Imports System.Runtime.InteropServices

''' <summary>
''' BnetConnection handles all BNCS traffic in its own thread.  Sockets are handled asynchronously.
''' </summary>
''' <remarks>The general structure of this class is a replication of the BnetConnection class
''' from Hyperion, a project by Lead[x86] (see http://forum.x86labs.org/ if you want to talk to
''' Lead about Hyperion).  Most of the code here, however, was typed out by me.</remarks>
Public Class BnetConnection

#Region "packet enums"
    Private Enum BNETPackets
        SID_NULL = &H0
        SID_STOPADV = &H2
        SID_SERVERLIST = &H4
        SID_CLIENTID = &H5
        SID_STARTVERSIONING = &H6
        SID_REPORTVERSION = &H7
        SID_STARTADVEX = &H8
        SID_GETADVLISTEX = &H9
        SID_ENTERCHAT = &HA
        SID_GETCHANNELLIST = &HB
        SID_JOINCHANNEL = &HC
        SID_CHATCOMMAND = &HE
        SID_CHATEVENT = &HF
        SID_LEAVECHAT = &H10
        SID_LOCALEINFO = &H12
        SID_FLOODDETECTED = &H13
        SID_UDPPINGRESPONSE = &H14
        SID_CHECKAD = &H15
        SID_CLICKAD = &H16
        SID_REGISTRY = &H18
        SID_MESSAGEBOX = &H19
        SID_STARTADVEX2 = &H1A
        SID_GAMEDATAADDRESS = &H1B
        SID_STARTADVEX3 = &H1C
        SID_LOGONCHALLENGEEX = &H1D
        SID_CLIENTID2 = &H1E
        SID_LEAVEGAME = &H1F
        SID_DISPLAYAD = &H21
        SID_NOTIFYJOIN = &H22
        SID_PING = &H25
        SID_READUSERDATA = &H26
        SID_WRITEUSERDATA = &H27
        SID_LOGONCHALLENGE = &H28
        SID_LOGONRESPONSE = &H29
        SID_CREATEACCOUNT = &H2A
        SID_SYSTEMINFO = &H2B
        SID_GAMERESULT = &H2C
        SID_GETICONDATA = &H2D
        SID_GETLADDERDATA = &H2E
        SID_FINDLADDERUSER = &H2F
        SID_CDKEY = &H30
        SID_CHANGEPASSWORD = &H31
        SID_CHECKDATAFILE = &H32
        SID_GETFILETIME = &H33
        SID_QUERYREALMS = &H34
        SID_PROFILE = &H35
        SID_CDKEY2 = &H36
        SID_LOGONRESPONSE2 = &H3A
        SID_CHECKDATAFILE2 = &H3C
        SID_CREATEACCOUNT2 = &H3D
        SID_LOGONREALMEX = &H3E
        SID_STARTVERSIONING2 = &H3F
        SID_QUERYREALMS2 = &H40
        SID_QUERYADURL = &H41
        SID_WARCRAFTGENERAL = &H44
        SID_NETGAMEPORT = &H45
        SID_NEWS_INFO = &H46
        SID_OPTIONALWORK = &H4A
        SID_EXTRAWORK = &H4B
        SID_REQUIREDWORK = &H4C
        SID_TOURNAMENT = &H4E
        SID_AUTH_INFO = &H50
        SID_AUTH_CHECK = &H51
        SID_AUTH_ACCOUNTCREATE = &H52
        SID_AUTH_ACCOUNTLOGON = &H53
        SID_AUTH_ACCOUNTLOGONPROOF = &H54
        SID_AUTH_ACCOUNTCHANGE = &H55
        SID_AUTH_ACCOUNTCHANGEPROOF = &H56
        SID_AUTH_ACCOUNTUPGRADE = &H57
        SID_AUTH_ACCOUNTUPGRADEPROOF = &H58
        SID_SETEMAIL = &H59
        SID_RESETPASSWORD = &H5A
        SID_CHANGEEMAIL = &H5B
        SID_SWITCHPRODUCT = &H5C
        SID_REPORTCRASH = &H5D
        SID_WARDEN = &H5E
        SID_GAMEPLAYERSEARCH = &H60
        SID_FRIENDSLIST = &H65
        SID_FRIENDSUPDATE = &H66
        SID_FRIENDSADD = &H67
        SID_FRIENDSREMOVE = &H68
        SID_FRIENDSPOSITION = &H69
        SID_CLANFINDCANDIDATES = &H70
        SID_CLANINVITEMULTIPLE = &H71
        SID_CLANCREATIONINVITATION = &H72
        SID_CLANDISBAND = &H73
        SID_CLANMAKECHIEFTAIN = &H74
        SID_CLANINFO = &H75
        SID_CLANQUITNOTIFY = &H76
        SID_CLANINVITATION = &H77
        SID_CLANREMOVEMEMBER = &H78
        SID_CLANINVITATIONRESPONSE = &H79
        SID_CLANRANKCHANGE = &H7A
        SID_CLANSETMOTD = &H7B
        SID_CLANMOTD = &H7C
        SID_CLANMEMBERLIST = &H7D
        SID_CLANMEMBERREMOVED = &H7E
        SID_CLANMEMBERSTATUSCHANGE = &H7F
        SID_CLANMEMBERRANKCHANGE = &H81
        SID_CLANMEMBERINFORMATION = &H82
    End Enum

    Private Enum ChatEvents
        EID_SHOWUSER = &H1
        EID_JOIN = &H2
        EID_LEAVE = &H3
        EID_WHISPER = &H4
        EID_TALK = &H5
        EID_BROADCAST = &H6
        EID_CHANNEL = &H7
        EID_USERFLAGS = &H9
        EID_WHISPERSENT = &HA
        EID_CHANNELFULL = &HD
        EID_CHANNELDOESNOTEXIST = &HE
        EID_CHANNELRESTRICTED = &HF
        EID_INFO = &H12
        EID_ERROR = &H13
        EID_EMOTE = &H17
    End Enum
#End Region

#Region "variable declares"
    Public cfg As BnetConfig
    Private sck As Socket

    Private nullTimer As Threading.Timer
    Private queueTimer As Threading.Timer

    Private ipHost As IPHostEntry
    Private ipEnd As IPEndPoint
    Private async As New AsyncCallback(AddressOf BNET_ConnectionCompleted)

    Private ServerToken As Integer                      ' server token for password hashing
    Private ClientToken As Integer                      ' client token for password hashing
    Private ValueStringLckdwn() As Byte                 ' lockdown value string
    Private ValueString As String                       ' non lockdown value string
    Private UsingLockdown As Boolean                    ' obvious
    Private Filename As String                          ' mpq filename to be used for hashing
    Private mpqFiletime As Long                         ' filetime of the mpq archive
    Private CheckSum As Integer                         ' hash checksum
    Private W3Srv() As Byte                             ' warcraft 3 server version
    Private CRResult As Integer                         ' checkrevision result
    Private key1hash() As Byte                          ' hash of primary key
    Private GameFiles(2) As String                      ' hash file paths
    Private TrueUsername As String                      ' the username with number hack assigned by server

    Private war3Logon As NLS
    Private war3Salt() As Byte
    Private war3ServerKey() As Byte

    Private _AllowEnterChat As Boolean
    Private _SID_ENTERCHATready As Boolean
    Private _UDPCode As Long

    Private Messages As List(Of SentBnetData)
#End Region

#Region "events"
    Public Event BNET_AccountLogonAccepted()
    Public Event BNET_AccountLogonProof()
    Public Event BNET_AccountLogonSuccess()
    Public Event BNET_AccountLogonSuccessSalt()
    Public Event BNET_ConnectionStarted()
    Public Event BNET_Connected()
    Public Event BNET_Disconnected()
    Public Event BNET_Exception(ByVal ex As Exception)
    Public Event BNET_EnteredChat(ByVal UniqueName As String)
    Public Event BNET_Error_AccountDoesNotExist()
    Public Event BNET_Error_AccountIsBanned()
    Public Event BNET_Error_AccountUnknownError()
    Public Event BNET_Error_AccountUpgrade()
    Public Event BNET_Error_CdkeyBanned()
    Public Event BNET_Error_ChannelFull(ByVal Text As String)
    Public Event BNET_Error_ChannelDoesNotExist(ByVal Test As String)
    Public Event BNET_Error_ChannelRestricted(ByVal Text As String)
    Public Event BNET_Error_ErrorMessage(ByVal Text As String)
    Public Event BNET_Error_GameVersionDowngrade()
    Public Event BNET_Error_InvalidCdkey()
    Public Event BNET_Error_InvalidGameVersion()
    Public Event BNET_Error_InvalidPassword()
    Public Event BNET_Error_KeyInUse(ByVal UserName As String)
    Public Event BNET_Error_OldGameVersion()
    Public Event BNET_Error_RegisterEmail()
    Public Event BNET_Error_ServerClosedConnection()
    Public Event BNET_Error_VersionCheckFailed()
    Public Event BNET_Error_WrongProductInfo()
    Public Event BNET_FriendsAdd(ByVal account As String, ByVal status As Byte, ByVal locStatus As Byte, ByVal ProductID As Long, ByVal Location As String)
    Public Event BNET_FriendsList(ByVal account As String, ByVal status As Byte, ByVal locStatus As Byte, ByVal ProductID As Long, ByVal Location As String)
    Public Event BNET_FriendsRemove(ByVal index As Byte)
    Public Event BNET_FriendsPosition(ByVal oldIndex As Byte, ByVal newIndex As Byte)
    Public Event BNET_FriendsUpdate(ByVal index As Byte, ByVal status As Byte, ByVal locStatus As Byte, ByVal ProductID As Long, ByVal Location As String)
    Public Event BNET_GetIconData(ByVal filetime As ULong, ByVal filename As String, ByVal client As String, ByVal cdkey As String, ByVal server As String)
    Public Event BNET_Info_Broadcast(ByVal Text As String)
    Public Event BNET_Info_Channel(ByVal Text As String, ByVal Flags As Long)
    Public Event BNET_Info_Information(ByVal Text As String)
    Public Event BNET_PacketReceived(ByVal pID As Byte)
    Public Event BNET_PacketSent(ByVal Packet As SentBnetData)
    Public Event BNET_ProfileInfo(ByVal ID As String, ByVal PInfo As ProfileInfo)
    Public Event BNET_SendChatMessage(ByVal TrueName As String, ByVal Message As String)
    Public Event BNET_SendingLoginRequest()
    Public Event BNET_SocketError(ByVal errCode As String, ByVal errMessage As String)
    Public Event BNET_ShowUser(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Public Event BNET_UnhandledPacket(ByVal pID As Byte, ByVal Data As String)
    Public Event BNET_UsingLockdown()
    Public Event BNET_UsingHashFile(ByVal HashPath As String)
    Public Event BNET_UserJoined(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Public Event BNET_UserLeft(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Public Event BNET_UserWhisperRecieved(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Public Event BNET_UserTalk(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Public Event BNET_UserFlagsUpdate(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Public Event BNET_UserWhisperSent(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Public Event BNET_UserEmote(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Public Event BNET_VerifyingGameVersion()
    Public Event BNET_VersionCheckPassed()
    Public Event BNET_WAR3ServerInvalid()
#End Region

#Region "constructors"
    ''' <summary>
    ''' Creates a new BnetConnection class which handles all BNCS traffic.
    ''' </summary>
    ''' <param name="ncfg">The BnetConfig to use.</param>
    ''' <remarks>Blank BnetConfig will cause problems, as no checks are made</remarks>
    Public Sub New(ByVal ncfg As BnetConfig)
        cfg = ncfg
        Messages = New List(Of SentBnetData)
        Me._UDPCode = UDPCode
    End Sub
#End Region

#Region "public methods"
    ''' <summary>
    ''' Initialize a new connection to a Battle.net server
    ''' </summary>
    ''' <remarks>BNET_ConnectionStarted is raised at the beginning.  The socket is created
    ''' and the connection started.</remarks>
    Public Sub Connect()
        RaiseEvent BNET_ConnectionStarted()
        _AllowEnterChat = False
        _SID_ENTERCHATready = False
        sck = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        ipHost = GetHostEntry(cfg.SERVER)
        ipEnd = New IPEndPoint(ipHost.AddressList(0), 6112)
        sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 7000)
        sck.BeginConnect(ipEnd, async, sck)
    End Sub

    ''' <summary>
    ''' Terminate any existing connection to a Battle.net server.
    ''' </summary>
    ''' <param name="Silent">If True is passed, BNET_Disconnect is not raised.</param>
    ''' <remarks>Passing True for Silent can be handy for calling disconnect after an error,
    ''' when the user has been notified by other means that the connection has been
    ''' terminated, or in the Connect call from the menu, where it is expected that the
    ''' selected connection will be terminated</remarks>
    Public Sub Disconnect(Optional ByVal Silent As Boolean = False)
        nullTimer = Nothing
        If sck Is Nothing Then Exit Sub
        sck.Shutdown(SocketShutdown.Both)
        sck.Close()
        sck = Nothing
        If Not Silent Then RaiseEvent BNET_Disconnected()
    End Sub

    ''' <summary>
    ''' Get the current state of the BNCS connection
    ''' </summary>
    ''' <returns>True if the connection is active, False if not</returns>
    Public Function IsConnected() As Boolean
        Return sck.Connected
    End Function

    ''' <summary>
    ''' Send a chat message or command to the Battle.net server.
    ''' </summary>
    ''' <param name="Data">String of text to send</param>
    Public Sub SendChat(ByVal Data As String)
        If Not Data = Nothing And sck.Connected Then
            Messages.Add(New SentBnetData(Data))
            Dim pck As New BncsPacket(BNETPackets.SID_CHATCOMMAND)
            pck.InsertCString(Data)
            sck.BeginSend(pck.GetData(), 0, pck.GetData().Length(), SocketFlags.None, New AsyncCallback(AddressOf BNET_SendComplete), sck)
            pck = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Should be called after icon processing has finished
    ''' </summary>
    ''' <remarks>_SID_ENTERCHATready and _AllowEnterChat are set to false by the
    ''' <see cref="Connect">Connect</see> method.  If these are false the class will not send SID_ENTERCHAT
    ''' or SID_JOINCHANNEL.  This is a safety measure to allow certain things to happen first, as sending
    ''' some packets after SID_ENTERCHAT or SID_JOINCHANNEL can cause the server to terminate the connection.</remarks>
    Public Sub AllowEnterChat()
        If _SID_ENTERCHATready Then
            SEND_SID_ENTERCHAT()
            SEND_SID_JOINCHANNEL()
        End If
        _AllowEnterChat = True
    End Sub

    ''' <summary>
    ''' Request the regular and ladder records, ladder rating, location, and description
    ''' </summary>
    ''' <param name="Username">Username to request the profile field of</param>
    ''' <returns>The ID of the request is returned.</returns>
    ''' <remarks>The ID is a simple hash consisting of the sum of the ASCII value of each
    ''' character in the username.</remarks>
    Public Function RequestProfile(ByVal Username As String) As String
        If sck Is Nothing Then Return "-1"
        If Not sck.Connected Then Return "-1"

        Dim val As Long = 0

        For i As Integer = 0 To Username.Length() - 1
            val += Asc(Username.Chars(i))
        Next i

        Dim pck As New BncsPacket(BNETPackets.SID_READUSERDATA)

        pck.InsertInt32(&H1)
        pck.InsertInt32(9)
        pck.InsertInt32(val)
        pck.InsertCString(Username)

        pck.InsertCString("Record\" & cfg.CLIENT & "\0\wins")
        pck.InsertCString("Record\" & cfg.CLIENT & "\0\losses")
        pck.InsertCString("Record\" & cfg.CLIENT & "\0\disconnects")
        pck.InsertCString("Record\" & cfg.CLIENT & "\1\wins")
        pck.InsertCString("Record\" & cfg.CLIENT & "\1\losses")
        pck.InsertCString("Record\" & cfg.CLIENT & "\1\disconnects")
        pck.InsertCString("Record\" & cfg.CLIENT & "\1\rating")
        pck.InsertCString("profile\location")
        pck.InsertCString("profile\description")
        SendPacket(pck.GetData())
        pck = Nothing

        Return val.ToString()
    End Function

    ''' <summary>
    ''' Update the profile of the account currently in use.
    ''' </summary>
    ''' <param name="pi_Loc">The new location</param>
    ''' <param name="pi_Desc">The new description</param>
    Public Sub UpdateProfile(ByVal pi_Loc As String, ByVal pi_Desc As String)
        '(DWORD) Number of accounts
        '(DWORD) Number of keys
        '(STRING) [] Accounts to update
        '(STRING) [] Keys to update
        '(STRING) [] New values
        Dim pck As New BncsPacket(BNETPackets.SID_WRITEUSERDATA)
        pck.InsertInt32(1)
        pck.InsertInt32(2)
        pck.InsertCString(cfg.USERNAME)
        pck.InsertCString("profile\location")
        pck.InsertCString("profile\description")
        pck.InsertCString(pi_Loc)
        pck.InsertCString(pi_Desc)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub
#End Region

#Region "Socket callbacks"
    ''' <summary>
    ''' Asyncronously called when the connection attempt has finished and a connection to the server
    ''' has been established.
    ''' </summary>
    ''' <param name="async">The status of the current operation</param>
    ''' <remarks>The connection sequence currently only handles keyed, full version products.  The 
    ''' method also starts the proper connection sequence.  At the moment, the only difference in 
    ''' connection sequences is W2BN, which uses OLS.  WAR3 and W3XP use NLS, while the rest use
    ''' what I call LSv2, which is roughly half of NLS followed by some different packets.</remarks>
    Private Sub BNET_ConnectionCompleted(ByVal async As IAsyncResult)
        nullTimer = New Threading.Timer(New Threading.TimerCallback(AddressOf BNET_SID_NULL), Nothing, 180000, 180000)

        Dim buff(4096) As Byte
        Dim asyncnew As New AsyncCallback(AddressOf BNET_DataArrival)

        buff(0) = &H1

        If sck.Connected = True Then
            sck.EndConnect(async)
            sck.BeginReceive(buff, 0, sck.Available, SocketFlags.None, asyncnew, sck)
            sck.Send(buff, 1, SocketFlags.None)
            RaiseEvent BNET_Connected()
            'Select Case cfg.CLIENT
            'Case "W2BN"
            '    SEND_SID_CLIENTID2()
            '    SEND_SID_LOCALEINFO()
            '    SEND_SID_STARTVERSIONING()
            'Case "WAR3", "W3XP", "STAR", "SEXP", "D2DV", "D2XP"
            SEND_SID_AUTH_INFO()
            'End Select
        End If
    End Sub

    ''' <summary>
    ''' Called any time the socket receives data from the server.
    ''' </summary>
    ''' <param name="async">Status of the current operation</param>
    ''' <remarks>This is almost line-for-line copied from Hyperion.  I have added a few checks,
    ''' and the BNET_Exception event.</remarks>
    Private Sub BNET_DataArrival(ByVal async As IAsyncResult)
        If sck Is Nothing Then
            Me.Disconnect()
            Exit Sub
        End If

        If Not sck.Connected() Then
            Me.Disconnect()
            Exit Sub
        End If

        Dim i As Integer
        Dim array As New List(Of Byte)
        Dim pLen As Integer

        Try
            sck.EndReceive(async)

            Dim buff(sck.Available - 1) As Byte
            Dim asyncnew As New AsyncCallback(AddressOf BNET_DataArrival)

            sck.BeginReceive(buff, 0, sck.Available, SocketFlags.None, asyncnew, sck)
            If buff.Length > 0 Then
                For i = 0 To buff.Length - 1
                    array.Add(buff(i))
                Next
                pLen = GetWORD(buff(2), buff(3))
                Do Until (array.Count = 0)
                    Dim newbuff(4096) As Byte
                    pLen = GetWORD(array(2), array(3))
                    For i = 0 To pLen - 1
                        If i >= array.Count() Then
                            pLen = i
                            Exit For
                        End If
                        newbuff(i) = array(i)
                    Next
                    array.RemoveRange(0, pLen)
                    ParsePacket(newbuff)
                Loop
            End If
        Catch ex As Exception
            RaiseEvent BNET_Exception(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Called when data has finished sending.
    ''' </summary>
    ''' <param name="async">Status of the current operation</param>
    ''' <remarks>This is used to let the client know when a packet has finished sending.  If the 
    ''' packet was a chat message (SID_CHATCOMMAND), then the text is stored instead of the raw
    ''' packet data.  This is so that sent messages can be added to the chat window when they have
    ''' been sent, instead of when they have started to be sent, which is more accurate of when
    ''' other users actually see the message.  Handy for fans of trivia (who should be smacked about
    ''' with a large slimey thing, such as a fish, in any place).</remarks>
    Private Sub BNET_SendComplete(ByVal async As IAsyncResult)
        Dim item As SentBnetData = Messages(0)
        Messages.Remove(item)
        If item.CHAT Then
            If item.TEXT <> "" Then
                RaiseEvent BNET_SendChatMessage(TrueUsername, item.TEXT)
            End If
        Else
            RaiseEvent BNET_PacketSent(item)
        End If
    End Sub

    ''' <summary>
    ''' Send SID_NULL, the keep-alive packet
    ''' </summary>
    ''' <remarks>Should be sent every 5-8 minutes.</remarks>
    Private Sub BNET_SID_NULL()
        SendPacket(New BncsPacket(BNETPackets.SID_NULL).GetData())
    End Sub
#End Region

#Region "dispatch"
    ''' <summary>
    ''' Called from BNET_DataArrival.  Handles dispatching the packet to the appropriate parsing
    ''' routine.
    ''' </summary>
    ''' <param name="Data">A Byte array containing the packet data.</param>
    ''' <remarks>Not all BNCS packets are currently handled.</remarks>
    Private Sub ParsePacket(ByVal Data() As Byte)
        If Data(0) <> &H0 Then
            Dim pck As New BncsReader(Data)
            'Debug.Print(pck.ToString())
            RaiseEvent BNET_PacketReceived(pck.PacketID)
            Select Case pck.PacketID
                Case BNETPackets.SID_NULL ' [0x00]
                Case BNETPackets.SID_CLIENTID ' do nothing
                Case BNETPackets.SID_STARTVERSIONING : PARSE_SID_STARTVERSIONING(pck)
                Case BNETPackets.SID_LOGONCHALLENGEEX : PARSE_SID_LOGONCHALLENGEEX(pck)
                Case BNETPackets.SID_REPORTVERSION : PARSE_SID_REPORTVERSION(pck)
                Case BNETPackets.SID_CDKEY2 : PARSE_SID_CDKEY2(pck)
                Case BNETPackets.SID_AUTH_INFO : PARSE_SID_AUTH_INFO(pck) ' [0x50]
                Case BNETPackets.SID_AUTH_CHECK : PARSE_SID_AUTH_CHECK(pck) ' [0x51]
                Case BNETPackets.SID_GETICONDATA : PARSE_SID_GETICONDATA(pck)
                Case BNETPackets.SID_LOGONRESPONSE : PARSE_SID_LOGONRESPONSE(pck)
                Case BNETPackets.SID_LOGONRESPONSE2 : PARSE_SID_LOGONRESPONSE2(pck)
                Case BNETPackets.SID_PING : PARSE_SID_PING(pck)
                Case BNETPackets.SID_READUSERDATA : PARSE_SID_READUSERDATA(pck)
                Case BNETPackets.SID_ENTERCHAT : PARSE_SID_ENTERCHAT(pck)
                Case BNETPackets.SID_CHATEVENT : PARSE_SID_CHATEVENT(pck)
                Case BNETPackets.SID_FRIENDSLIST : PARSE_SID_FRIENDSLIST(pck)
                Case BNETPackets.SID_FRIENDSADD : PARSE_SID_FRIENDSADD(pck)
                Case BNETPackets.SID_FRIENDSREMOVE : PARSE_SID_FRIENDSREMOVE(pck)
                Case BNETPackets.SID_FRIENDSPOSITION : PARSE_SID_FRIENDSPOSITION(pck)
                Case BNETPackets.SID_FRIENDSUPDATE : PARSE_SID_FRIENDSUPDATE(pck)
                Case Else : RaiseEvent BNET_UnhandledPacket(pck.PacketID, pck.ToString())
            End Select
            pck = Nothing
        End If
    End Sub
#End Region

#Region "send routines"
    ''' <summary>
    ''' Attempt to send a packet to the server.
    ''' </summary>
    ''' <param name="Data">A Byte array of the packet data.</param>
    ''' <remarks>If this failed, it typically means that the connection to the server does
    ''' not exist.  In that case, the bot clears the packet queue and disables the nullTimer,
    ''' as well as raising several error and disconnect notices.  The socket is also shut down
    ''' properly if possible.</remarks>
    Private Sub SendPacket(ByVal Data() As Byte)
        Try
            Messages.Add(New SentBnetData(Data(1)))
            sck.BeginSend(Data, 0, Data.Length(), SocketFlags.None, New AsyncCallback(AddressOf BNET_SendComplete), sck)
        Catch ex As SocketException
            RaiseEvent BNET_SocketError(ex.ErrorCode.ToString(), ex.Message)
            RaiseEvent BNET_Error_ServerClosedConnection()
            RaiseEvent BNET_Disconnected()
            Messages.Clear()
            nullTimer = Nothing
            If sck IsNot Nothing Then
                sck.Shutdown(SocketShutdown.Both)
                sck.Close()
                sck = Nothing
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Send packet SID_CLIENTID2 (W2BN logon sequence)
    ''' </summary>
    ''' <remarks>Similar to SID_AUTH_INFO, though not as much information is sent.  The client
    ''' token is also set here.</remarks>
    Private Sub SEND_SID_CLIENTID2()
        Dim pck As New BncsPacket(BNETPackets.SID_CLIENTID2)
        Dim ipAddr As Long

        ipAddr = BitConverter.ToInt32(GetHostEntry(cfg.SERVER).AddressList(0).GetAddressBytes, 0)
        ClientToken = TickCount

        pck.InsertInt32(1)
        pck.InsertInt32(1)
        'pck.InsertInt32(GetHostEntry(cfg.SERVER).AddressList(0).Address)
        pck.InsertInt32(0)
        pck.InsertInt32(0)
        pck.InsertInt32(ClientToken)
        pck.InsertCString("")
        pck.InsertCString("")
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Send packet SID_LOCALEINFO (W2BN logon sequence)
    ''' </summary>
    ''' <remarks>Similar to SID_AUTH_INFO.  SID_LOCALEINFO combined with SID_CLIENTID2 pretty much
    ''' is SID_AUTH_INFO, actually.</remarks>
    Private Sub SEND_SID_LOCALEINFO()
        '(FILETIME) System time
        '(FILETIME) Local time
        '(DWORD) Timezone bias
        '(DWORD) SystemDefaultLCID
        '(DWORD) UserDefaultLCID
        '(DWORD) UserDefaultLangID
        '(STRING) Abbreviated language name
        '(STRING) Country name
        '(STRING) Abbreviated country name
        '(STRING) Country (English)
        Dim pck As New BncsPacket(BNETPackets.SID_LOCALEINFO)
        pck.InsertInt64(Now.ToFileTimeUtc)
        pck.InsertInt64(Now.ToFileTimeUtc)
        pck.InsertInt32(0)
        pck.InsertInt32(0)
        pck.InsertInt32(0)
        pck.InsertInt32(0)
        pck.InsertCString("en-US") ' hardcoded to USA, will change later
        pck.InsertCString("United States")
        pck.InsertCString("USA") ' hardcoded to USA, will change later
        pck.InsertCString("United States")
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Send packet SID_STARTVERSIONING (W2BN logon sequence)
    ''' </summary>
    ''' <remarks>Informs the server that we are using W2BN and what version we are using.  This 
    ''' sequence used to be (and still can be) used by StarCraft and Brood War clients, though
    ''' the official clients never do.  As such, the client ID is hard-coded to W2BN</remarks>
    Private Sub SEND_SID_STARTVERSIONING()
        '(DWORD) Platform ID
        '(DWORD) Product ID
        '(DWORD) Version Byte
        '(DWORD) Unknown (0) 

        RaiseEvent BNET_VerifyingGameVersion()
        Dim pck As New BncsPacket(BNETPackets.SID_STARTVERSIONING)
        pck.InsertInt32(PLATID_IX86)
        pck.InsertInt32(CLIENT_W2BN) ' because this is only used for W2BN connections...
        pck.InsertInt32(&H4F)
        pck.InsertInt32(0)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Sends packet SID_AUTH_INFO to the server
    ''' </summary>
    ''' <remarks>Tells the server what game we are using, what version, what language, 
    ''' and from what country.  Other details include the local IP address and some
    ''' other stuff I don't bother with.  The country, country code, and language are
    ''' all hard-coded to USA and English, at the moment.  I will change this later.</remarks>
    Private Sub SEND_SID_AUTH_INFO()
        Dim VerByte As Long
        Dim client_val As Long
        Select Case cfg.CLIENT
            Case "STAR"
                VerByte = &HD3
                client_val = CLIENT_STAR
            Case "SEXP"
                VerByte = &HD3
                client_val = CLIENT_SEXP
            Case "W2BN"
                VerByte = &H4F
                client_val = CLIENT_W2BN
            Case "D2DV"
                VerByte = &HC
                client_val = CLIENT_D2DV
            Case "D2XP"
                VerByte = &HC
                client_val = CLIENT_D2XP
            Case "WAR3"
                VerByte = &H18
                client_val = CLIENT_WAR3
            Case "W3XP"
                VerByte = &H18
                client_val = CLIENT_W3XP
        End Select

        Dim pck As New BncsPacket(BNETPackets.SID_AUTH_INFO)

        pck.InsertInt32(0)
        pck.InsertInt32(PLATID_IX86)
        pck.InsertInt32(client_val) ' for now, will change later
        pck.InsertInt32(VerByte)
        pck.InsertInt32(PROD_LANGUE) ' english, will change later
        pck.InsertInt32(&H0)
        pck.InsertInt32(&H0)
        pck.InsertInt32(&H0)
        pck.InsertInt32(&H0)
        pck.InsertCString("USA") ' hardcoded to USA, will change later
        pck.InsertCString("United States")
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Send packet SID_REPORTVERSION (W2BN logon sequence) to the server
    ''' </summary>
    ''' <remarks>Similar to SID_AUTH_CHECK, but without the CD-Key hash</remarks>
    Private Sub SEND_SID_REPORTVERSION()
        Dim ImageFile As String = Nothing
        Dim exeInfo As String = Nothing
        Dim exeVer As Integer
        Dim KeyHash(1024) As Byte
        Dim ldPath As String = Nothing
        Dim ldDigest() As Byte = Nothing

        GameFiles(0) = Application.StartupPath & "\Hashes\W2BN\Warcraft II BNE.exe"
        GameFiles(1) = Application.StartupPath & "\Hashes\W2BN\Storm.dll"
        GameFiles(2) = Application.StartupPath & "\Hashes\W2BN\Battle.snp"

        RaiseEvent BNET_UsingHashFile(GameFiles(0))
        RaiseEvent BNET_UsingHashFile(GameFiles(1))
        RaiseEvent BNET_UsingHashFile(GameFiles(2))

        If Filename.StartsWith("LOCKDOWN", StringComparison.OrdinalIgnoreCase) Then
            Dim dllName As String = Filename.Replace(".mpq", ".dll")
            Dim req As New BnFtpVersion1Request(cfg.CLIENT, Filename, Nothing)
            req.Server = cfg.SERVER
            req.LocalFileName = Path.Combine(Path.GetTempPath, Filename)
            req.ExecuteRequest()

            ImageFile = Application.StartupPath & "\Hashes\W2BN\W2BN.bin"

            If ImageFile <> "" Then RaiseEvent BNET_UsingHashFile(ImageFile)

            Using arch As MpqArchive = MpqServices.OpenArchive(req.LocalFileName)
                If arch.ContainsFile(dllName) Then
                    ldPath = Path.Combine(Path.GetTempPath(), dllName)
                    arch.SaveToPath(dllName, Path.GetTempPath(), False)
                End If
            End Using

            ldDigest = CheckRevision.DoLockdownCheckRevision(ValueStringLckdwn, GameFiles, ldPath, ImageFile, exeVer, CRResult)
        Else
            Dim mpqNum As Integer = CheckRevision.ExtractMPQNumber(Filename)
            CRResult = CheckRevision.DoCheckRevision(ValueString, GameFiles, mpqNum)
            exeVer = CheckRevision.GetExeInfo(GameFiles(0), exeInfo)
        End If

        '(DWORD) Platform ID
        '(DWORD) Product ID
        '(DWORD) Version Byte
        '(DWORD) EXE Version
        '(DWORD) EXE Hash
        '(STRING) EXE Information 
        Dim pck As New BncsPacket(BNETPackets.SID_REPORTVERSION)
        pck.InsertInt32(PLATID_IX86)
        pck.InsertInt32(CLIENT_W2BN) ' because this is only used for W2BN connections...
        pck.InsertInt32(&H4F)
        pck.InsertInt32(exeVer)
        pck.InsertInt32(CRResult)
        If UsingLockdown Then
            Try
                pck.InsertByteArray(ldDigest)
            Catch ex As Exception
                'Recalculate Digest if theres a problem
                ldDigest = CheckRevision.DoLockdownCheckRevision(ValueStringLckdwn, GameFiles, ldPath, ImageFile, exeVer, CRResult)
                pck.InsertByteArray(ldDigest)
            End Try
            pck.InsertByte(0)
        Else
            pck.InsertCString(exeInfo)
        End If
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Send packet SID_CDKEY2 (W2BN logon sequence) to the server
    ''' </summary>
    ''' <remarks>This is the bit of SID_AUTH_CHECK missing from SEND_SID_REPORTVERSION</remarks>
    Private Sub SEND_SID_CDKEY2()
        Dim Cdkey1 As CdKey = New CdKey(cfg.CDKEY)
        ClientToken = CUInt(New Random().Next())
        key1hash = Cdkey1.GetHash(ClientToken, ServerToken)

        '(DWORD) Spawn (0/1)
        '(DWORD) Key Length
        '(DWORD) CDKey Product
        '(DWORD) CDKey Value1
        '(DWORD) Server Token
        '(DWORD) Client Token
        '(DWORD) [5] Hashed Data
        '(STRING) Key owner
        Dim pck As New BncsPacket(BNETPackets.SID_CDKEY2)
        pck.InsertInt32(0) ' never spawned
        pck.InsertInt32(Cdkey1.Key.Length())
        pck.InsertInt32(Cdkey1.Product)
        pck.InsertInt32(Cdkey1.Value1)
        pck.InsertInt32(ServerToken)
        pck.InsertInt32(ClientToken)
        pck.Insert(key1hash)
        pck.InsertCString(cfg.OWNER)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Sends the CD-Key and executable hash data to the server for verification.
    ''' </summary>
    ''' <remarks>I have nothing to declare.</remarks>
    Private Sub SEND_SID_AUTH_CHECK()
        RaiseEvent BNET_VerifyingGameVersion()
        ClientToken = TickCount
        Dim ImageFile As String = Nothing
        Dim exeInfo As String = Nothing
        Dim exeVer As Integer
        Dim KeyHash(1024) As Byte
        Dim ldPath As String = Nothing
        Dim ldDigest() As Byte = Nothing

        Select Case cfg.CLIENT
            Case "STAR", "SEXP"
                GameFiles(0) = Application.StartupPath & "\Hashes\STAR\Starcraft.exe"
                GameFiles(1) = Application.StartupPath & "\Hashes\STAR\Storm.dll"
                GameFiles(2) = Application.StartupPath & "\Hashes\STAR\Battle.snp"
                ImageFile = Application.StartupPath & "\Hashes\STAR\STAR.bin"
            Case "W2BN"
                GameFiles(0) = Application.StartupPath & "\Hashes\W2BN\Warcraft II BNE.exe"
                GameFiles(1) = Application.StartupPath & "\Hashes\W2BN\Storm.dll"
                GameFiles(2) = Application.StartupPath & "\Hashes\W2BN\Battle.snp"
                ImageFile = Application.StartupPath & "\Hashes\W2BN\W2BN.bin"
            Case "D2DV"
                GameFiles(0) = Application.StartupPath & "\Hashes\D2DV\Game.exe"
                GameFiles(1) = Application.StartupPath & "\Hashes\D2DV\BNClient.dll"
                GameFiles(2) = Application.StartupPath & "\Hashes\D2DV\D2Client.dll"
            Case "D2XP"
                GameFiles(0) = Application.StartupPath & "\Hashes\D2XP\Game.exe"
                GameFiles(1) = Application.StartupPath & "\Hashes\D2XP\BNClient.dll"
                GameFiles(2) = Application.StartupPath & "\Hashes\D2XP\D2Client.dll"
            Case "WAR3"
                GameFiles(0) = Application.StartupPath & "\Hashes\WAR3\War3.exe"
                GameFiles(1) = Application.StartupPath & "\Hashes\WAR3\Storm.dll"
                GameFiles(2) = Application.StartupPath & "\Hashes\WAR3\Game.dll"
            Case "W3XP"
                GameFiles(0) = Application.StartupPath & "\Hashes\W3XP\War3.exe"
                GameFiles(1) = Application.StartupPath & "\Hashes\W3XP\Storm.dll"
                GameFiles(2) = Application.StartupPath & "\Hashes\W3XP\Game.dll"
        End Select

        RaiseEvent BNET_UsingHashFile(GameFiles(0))
        RaiseEvent BNET_UsingHashFile(GameFiles(1))
        RaiseEvent BNET_UsingHashFile(GameFiles(2))

        If UsingLockdown Then
            Dim dllName As String = Filename.Replace(".mpq", ".dll")
            Dim req As New BnFtpVersion1Request(cfg.CLIENT, Filename, Nothing)
            req.Server = cfg.SERVER
            req.LocalFileName = Path.Combine(Path.GetTempPath, Filename)
            req.ExecuteRequest()

            If ImageFile <> "" Then RaiseEvent BNET_UsingHashFile(ImageFile)

            Using arch As MpqArchive = MpqServices.OpenArchive(req.LocalFileName)
                If arch.ContainsFile(dllName) Then
                    ldPath = Path.Combine(Path.GetTempPath(), dllName)
                    arch.SaveToPath(dllName, Path.GetTempPath(), False)
                End If
            End Using

            ldDigest = CheckRevision.DoLockdownCheckRevision(ValueStringLckdwn, GameFiles, ldPath, ImageFile, exeVer, CRResult)
        Else
            Dim mpqNum As Integer = CheckRevision.ExtractMPQNumber(Filename)
            CRResult = CheckRevision.DoCheckRevision(ValueString, GameFiles, mpqNum)
            exeVer = CheckRevision.GetExeInfo(GameFiles(0), exeInfo)
        End If

        If cfg.CLIENT = "WAR3" Or cfg.CLIENT = "W3XP" Then
            If Not NLS.ValidateServerSignature(W3Srv, ipEnd.Address.GetAddressBytes) Then
                RaiseEvent BNET_WAR3ServerInvalid()
            End If
        End If

        Dim Cdkey1 As CdKey = Nothing
        Dim Cdkey2 As CdKey = Nothing

        Cdkey1 = New CdKey(cfg.CDKEY)

        If cfg.CLIENT = "D2XP" Or cfg.CLIENT = "W3XP" Then
            Cdkey2 = New CdKey(cfg.EXPKEY)
        End If

        ClientToken = CUInt(New Random().Next())
        key1hash = Cdkey1.GetHash(ClientToken, ServerToken)

        Dim pck As New BncsPacket(BNETPackets.SID_AUTH_CHECK)
        pck.Insert(ClientToken)
        pck.Insert(exeVer)
        pck.Insert(CRResult)

        If cfg.CLIENT = "D2XP" Or cfg.CLIENT = "W3XP" Then
            pck.Insert(2)
        Else
            pck.Insert(1)
        End If

        pck.Insert(False)
        pck.Insert(Cdkey1.Key.Length)
        pck.Insert(Cdkey1.Product)
        pck.Insert(Cdkey1.Value1)
        pck.Insert(0)
        pck.Insert(key1hash)

        If Cdkey2 IsNot Nothing Then
            pck.Insert(Cdkey2.Key.Length)
            pck.Insert(Cdkey2.Product)
            pck.Insert(Cdkey2.Value1)
            pck.Insert(0)
            pck.Insert(Cdkey2.GetHash(ClientToken, ServerToken))
        End If

        If UsingLockdown Then
            Try
                pck.InsertByteArray(ldDigest)
            Catch ex As Exception
                'Recalculate Digest if theres a problem
                ldDigest = CheckRevision.DoLockdownCheckRevision(ValueStringLckdwn, GameFiles, ldPath, ImageFile, exeVer, CRResult)
                pck.InsertByteArray(ldDigest)
            End Try
            pck.InsertByte(0)
        Else
            pck.InsertCString(exeInfo)
        End If

        pck.InsertCString(cfg.OWNER)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Request the details on the icon archive to be used.
    ''' </summary>
    ''' <remarks>If sent after SID_ENTERCHAT, the server terminates the connection.</remarks>
    Private Sub SEND_SID_GETICONDATA()
        Dim pck As New BncsPacket(BNETPackets.SID_GETICONDATA)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Identical in every way (except for the packet id) to SID_LOGONRESPONSE2.
    ''' </summary>
    ''' <remarks>I don't know why they didn't just add more handling for SID_LOGONRESPONSE in
    ''' the clients instead of adding a whole new packet to the protocol.</remarks>
    Private Sub SEND_SID_LOGONRESPONSE()
        ' identical to SEND_SID_LOGONRESPONSE2 except for the packet id...go figure
        RaiseEvent BNET_SendingLoginRequest()
        Dim pck As New BncsPacket(BNETPackets.SID_LOGONRESPONSE)
        Dim passHash() As Byte

        pck.InsertInt32(ClientToken)
        pck.InsertInt32(ServerToken)

        passHash = OldAuth.DoubleHashPassword(cfg.PASSWORD, ClientToken, ServerToken)

        pck.InsertByteArray(passHash)
        pck.InsertCString(cfg.USERNAME)
        Debug.Print(pck.ToString())
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Sends the hash of the password for non-WAR3 connections.
    ''' </summary>
    ''' <remarks>Pretty straitforward, no?</remarks>
    Private Sub SEND_SID_LOGONRESPONSE2()
        RaiseEvent BNET_SendingLoginRequest()
        Dim pck As New BncsPacket(BNETPackets.SID_LOGONRESPONSE2)
        Dim passHash() As Byte

        pck.InsertInt32(ClientToken)
        pck.InsertInt32(ServerToken)

        passHash = OldAuth.DoubleHashPassword(cfg.PASSWORD, ClientToken, ServerToken)

        pck.InsertByteArray(passHash)
        pck.InsertCString(cfg.USERNAME)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Sends the logon details for WAR3 based connections.
    ''' </summary>
    ''' <remarks>Simpler than SID_LOGONRESPONSE2.</remarks>
    Private Sub SEND_SID_AUTH_ACCOUNTLOGON()
        RaiseEvent BNET_SendingLoginRequest()
        Dim pck As New BncsPacket(BNETPackets.SID_AUTH_ACCOUNTLOGON)
        war3Logon = New NLS(cfg.USERNAME, cfg.PASSWORD)
        war3Logon.LoginAccount(pck)

        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Proves that we do, in fact, want to log in to the account we want to log in
    ''' to, and have the authority to do so.
    ''' </summary>
    ''' <remarks>Tedious.</remarks>
    Private Sub SEND_SID_AUTH_ACCOUNTLOGONPROOF()
        RaiseEvent BNET_AccountLogonProof()
        Dim pck As New BncsPacket(BNETPackets.SID_AUTH_ACCOUNTLOGONPROOF)
        war3Logon.LoginProof(pck, war3Salt, war3ServerKey)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Sends a request to the server to enter chat.
    ''' </summary>  
    ''' <remarks>This is sent after the logon process, as well as after a game ends and
    ''' the client is returning to chat.  Rebirth does not currently support games, and
    ''' as such, only sends this after login</remarks>
    Private Sub SEND_SID_ENTERCHAT()
        Dim pck As New BncsPacket(BNETPackets.SID_ENTERCHAT)
        If cfg.CLIENT <> "WAR3" And cfg.CLIENT <> "W3XP" Then
            pck.InsertCString(cfg.USERNAME)
        Else
            pck.InsertCString(String.Empty)
        End If
        pck.InsertCString(String.Empty)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Packet version of the /join command.  This is sent by clients after sending SID_ENTERCHAT
    ''' or when the user requests to join a channel from the CHANNELS page in clients.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SEND_SID_JOINCHANNEL()
        Dim pck As New BncsPacket(BNETPackets.SID_JOINCHANNEL)
        pck.InsertUInt32(&H1)
        pck.InsertCString(cfg.HOME)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Sends ping response.
    ''' </summary>
    ''' <param name="val">val should be extracted from received SID_PING messages</param>
    ''' <remarks>If sent before receiving the first SID_PING, the connection will receive
    ''' 0 as a value for ping (what other clients see).  If not sent at all, -1.  If sent
    ''' late, the you can artificially inflate your ping.</remarks>
    Private Sub SEND_SID_PING(ByVal val As Long)
        Dim pck As New BncsPacket(BNETPackets.SID_PING)
        pck.InsertUInt32(val)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Send response to indicate the client has UDP support
    ''' </summary>
    ''' <remarks>Currently, this is clearly a fake.  The bot does not support UDP at this time.</remarks>
    Private Sub SEND_SID_UDPPINGRESPONSE()
        Dim pck As New BncsPacket(BNETPackets.SID_UDPPINGRESPONSE)
        pck.InsertUInt32(Me._UDPCode)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Requests the friends list.
    ''' </summary>
    ''' <remarks>Simple.</remarks>
    Private Sub SEND_SID_FRIENDSLIST()
        Dim pck As New BncsPacket(BNETPackets.SID_FRIENDSLIST)
        SendPacket(pck.GetData())
        pck = Nothing
    End Sub
#End Region

#Region "parsers"
    ''' <summary>
    ''' Get the MPQ archive and its filetime to be used for hashing.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>Lockdown status is also determined here.  This is a W2BN logon sequence packet.</remarks>
    Private Sub PARSE_SID_STARTVERSIONING(ByVal pck As BncsReader)
        'Debug.Print(pck.ToString())
        '(FILETIME) MPQ Filetime
        '(STRING) MPQ Filename
        '(STRING) ValueString 
        mpqFiletime = pck.ReadInt64()
        Filename = pck.ReadCString()

        UsingLockdown = Filename.StartsWith("LOCKDOWN", StringComparison.OrdinalIgnoreCase)

        If UsingLockdown = True Then
            RaiseEvent BNET_UsingLockdown()
            ValueStringLckdwn = pck.ReadNullTerminatedByteArray
        Else
            ValueString = pck.ReadCString()
        End If

        SEND_SID_REPORTVERSION()
    End Sub

    ''' <summary>
    ''' Get the UDP token to be sent in
    ''' <seealso cref="SEND_SID_UDPPINGRESPONSE">SID_UDPPINGRESPONSE</seealso> and server token
    ''' to be used in hashing.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>The UDP token is almost always "netb".  This is a W2BN logon sequence packet.</remarks>
    Private Sub PARSE_SID_LOGONCHALLENGEEX(ByVal pck As BncsReader)
        '(DWORD) UDP Token
        '(DWORD) Server Token 
        Me._UDPCode = pck.ReadInt32()
        Me.ServerToken = pck.ReadInt32()
    End Sub

    ''' <summary>
    ''' Result of <seealso cref="SEND_SID_STARTVERSIONING">SID_STARTVERSIONING</seealso>.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>Patch path is ignored by Rebirth.  This is a W2BN logon sequence packet.</remarks>
    Private Sub PARSE_SID_REPORTVERSION(ByVal pck As BncsReader)
        '(DWORD) Result
        '(STRING) Patch path 

        ' 0: Failed version check
        ' 1: Old game version
        ' 2: Success
        ' 3: Reinstall required
        Select Case pck.ReadInt32()
            Case 0 : RaiseEvent BNET_Error_VersionCheckFailed()
            Case 1 : RaiseEvent BNET_Error_OldGameVersion()
            Case 2
                RaiseEvent BNET_VersionCheckPassed()
                SEND_SID_UDPPINGRESPONSE()
                SEND_SID_CDKEY2()
            Case 3 : RaiseEvent BNET_Error_GameVersionDowngrade()
        End Select
    End Sub

    ''' <summary>
    ''' Result of <seealso cref="SEND_SID_CDKEY2">SID_CDKEY2</seealso>.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>This is a W2BN logon sequence packet.</remarks>
    Private Sub PARSE_SID_CDKEY2(ByVal pck As BncsReader)
        Select Case pck.ReadInt32()
            Case &H1
                SEND_SID_GETICONDATA()
                SEND_SID_LOGONRESPONSE()
            Case &H2 : RaiseEvent BNET_Error_InvalidCdkey()
            Case &H3 : RaiseEvent BNET_Error_WrongProductInfo()
            Case &H4 : RaiseEvent BNET_Error_CdkeyBanned()
            Case &H5 : RaiseEvent BNET_Error_KeyInUse(pck.ReadCString)
        End Select
    End Sub

    ''' <summary>
    ''' Result of <seealso cref="SEND_SID_AUTH_INFO">SID_AUTH_INFO</seealso>.  Extracts 
    ''' the logon type (ignored), server token, and hashing MPQ information
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>LogonType here is ignored, although it should be checked.</remarks>
    Private Sub PARSE_SID_AUTH_INFO(ByVal pck As BncsReader)
        Dim LogonType As Integer

        LogonType = pck.ReadUInt32()
        ServerToken = pck.ReadInt32()
        Me._UDPCode = pck.ReadUInt32()
        mpqFiletime = pck.ReadInt64()
        Filename = pck.ReadCString()
        UsingLockdown = Filename.StartsWith("LOCKDOWN", StringComparison.OrdinalIgnoreCase)

        If UsingLockdown = True Then
            RaiseEvent BNET_UsingLockdown()
            ValueStringLckdwn = pck.ReadNullTerminatedByteArray
        Else
            ValueString = pck.ReadCString
        End If

        'If Client = "WAR3" Or Client = "W3XP" Then W3Srv = pck.ReadByteArray(128)

        SEND_SID_UDPPINGRESPONSE()
        SEND_SID_AUTH_CHECK()
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Response to <seealso cref="SEND_SID_AUTH_CHECK">SID_AUTH_CHECK</seealso>.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>The connection sequence forks here, depending on the client.  WAR3 clients will
    ''' continue on with NLS, while StarCraft and Diablo II clients will continue on with LSv2.
    ''' The icon archive to use is also requested.</remarks>
    Private Sub PARSE_SID_AUTH_CHECK(ByVal pck As BncsReader)
        Select Case pck.ReadInt32
            Case &H0
                RaiseEvent BNET_VersionCheckPassed()
                SEND_SID_GETICONDATA()
                If cfg.CLIENT = "WAR3" Or cfg.CLIENT = "W3XP" Then
                    SEND_SID_AUTH_ACCOUNTLOGON()
                Else
                    SEND_SID_LOGONRESPONSE2()
                End If
            Case &H100 : RaiseEvent BNET_Error_OldGameVersion()
            Case &H101 : RaiseEvent BNET_Error_InvalidGameVersion()
            Case &H102 : RaiseEvent BNET_Error_GameVersionDowngrade()
            Case &H200 : RaiseEvent BNET_Error_InvalidCdkey()
            Case &H201 : RaiseEvent BNET_Error_KeyInUse(pck.ReadCString)
            Case &H202 : RaiseEvent BNET_Error_CdkeyBanned()
            Case &H203 : RaiseEvent BNET_Error_WrongProductInfo()
            Case Else
        End Select
        pck = Nothing
    End Sub

    ''' <summary>
    ''' Response to <seealso cref="SEND_SID_GETICONDATA">SID_GETICONDATA</seealso>.  Contains
    ''' the icon file to use and it's last modified time.  
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>If the filetime extracted from SID_GETICONDATA is different than the filetime
    ''' of the local copy, the bot should download the remote version and replace the local copy.
    ''' </remarks>
    Private Sub PARSE_SID_GETICONDATA(ByVal pck As BncsReader)
        Dim filetime As ULong = pck.ReadUInt64()
        Dim filename As String = pck.ReadCString()
        RaiseEvent BNET_GetIconData(filetime, filename, cfg.CLIENT, cfg.CDKEY, cfg.SERVER)
    End Sub

    ''' <summary>
    ''' Response to <seealso cref="SEND_SID_LOGONRESPONSE">SID_LOGONRESPONSE</seealso>.
    ''' If passed, the bot is ready to send SID_ENTERCHAT and SID_JOINCHANNEL.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks></remarks>
    Private Sub PARSE_SID_LOGONRESPONSE(ByVal pck As BncsReader)
        Select Case pck.ReadInt32
            Case &H0 : RaiseEvent BNET_Error_InvalidPassword()
            Case &H1
                RaiseEvent BNET_AccountLogonSuccess()
                _SID_ENTERCHATready = True
                If _AllowEnterChat Then
                    SEND_SID_ENTERCHAT()
                    SEND_SID_JOINCHANNEL()
                End If
        End Select
    End Sub

    ''' <summary>
    ''' Response to <seealso cref="SEND_SID_LOGONRESPONSE2">SID_LOGONRESPONSE2</seealso>.
    ''' If passed, the bot is ready to send SID_ENTERCHAT and SID_JOINCHANNEL.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks><seealso cref="SEND_SID_LOGONRESPONSE2">SID_LOGONRESPONSE2</seealso> has more
    ''' error codes than <seealso cref="SEND_SID_LOGONRESPONSE">SID_LOGONRESPONSE</seealso>.
    ''' If the account does not exist, the bot should attempt to create the account (not yet
    ''' implemented)</remarks>
    Private Sub PARSE_SID_LOGONRESPONSE2(ByVal pck As BncsReader)
        Select Case pck.ReadInt32
            Case &H0
                RaiseEvent BNET_AccountLogonSuccess()
                _SID_ENTERCHATready = True
                SEND_SID_UDPPINGRESPONSE()
                If _AllowEnterChat Then
                    SEND_SID_ENTERCHAT()
                    SEND_SID_JOINCHANNEL()
                End If
            Case &H1 : RaiseEvent BNET_Error_AccountDoesNotExist()
                'SEND_SID_CREATEACCOUNT2()
            Case &H2 : RaiseEvent BNET_Error_InvalidPassword()
            Case &H6 : RaiseEvent BNET_Error_AccountIsBanned()
        End Select
    End Sub

    ''' <summary>
    ''' Similar to <seealso cref="SEND_SID_LOGONRESPONSE2">SID_LOGONRESPONSE2</seealso>, but 
    ''' specific to WAR3 connections.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>As with <seealso cref="SEND_SID_LOGONRESPONSE2">SID_LOGONRESPONSE2</seealso>,
    ''' if the account does not exist, the bot should attempt to create it (not yet implemented)</remarks>
    Private Sub PARSE_SID_AUTH_ACCOUNTLOGON(ByVal pck As BncsReader)
        '(DWORD) Status
        '(BYTE) [32] Salt (s)
        '(BYTE) [32] Server Key (B)

        '0x00: Logon accepted, requires proof.
        '0x01: Account doesn't exist.
        '0x05: Account requires upgrade.
        'Other: Unknown (failure).
        Select Case pck.ReadInt32()
            Case 0
                RaiseEvent BNET_AccountLogonAccepted()
                war3Salt = pck.ReadByteArray(32)
                war3ServerKey = pck.ReadByteArray(32)
                SEND_SID_AUTH_ACCOUNTLOGONPROOF()
            Case 1 : RaiseEvent BNET_Error_AccountDoesNotExist()
            Case 5 : RaiseEvent BNET_Error_AccountUpgrade()
            Case Else : RaiseEvent BNET_Error_AccountUnknownError()
        End Select
    End Sub

    ''' <summary>
    ''' Response to <seealso cref="SEND_SID_AUTH_ACCOUNTLOGONPROOF">SID_AUTH_ACCOUNTLOGONPROOF</seealso>.
    ''' Lets the client know the result of the logon attempt.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>Case 0x48 is logon accepted, but also noting that the password as no salt.
    ''' I have decided to allow the client to continue with login.  May revert after testing.</remarks>
    Private Sub PARSE_SID_AUTH_ACCOUNTLOGONPROOF(ByVal pck As BncsReader)
        '(DWORD) Status
        '(BYTE) [20] Server Password Proof (M2)
        '(STRING) Additional information

        '0x00: Logon successful.
        '0x02: Incorrect password.
        '0x0E: An email address should be registered for this account.
        '0x0F: Custom error. A string at the end of this message contains the error.

        Select Case pck.ReadInt32()
            Case &H0
                RaiseEvent BNET_AccountLogonSuccess()
                _SID_ENTERCHATready = True
                If _AllowEnterChat Then
                    SEND_SID_ENTERCHAT()
                    SEND_SID_JOINCHANNEL()
                End If
            Case &H2 : RaiseEvent BNET_Error_InvalidPassword()
            Case &HE : RaiseEvent BNET_Error_RegisterEmail()
            Case &HF
                pck.ReadByteArray(20)
                RaiseEvent BNET_Error_ErrorMessage(pck.ReadCString())
            Case &H48
                RaiseEvent BNET_AccountLogonSuccessSalt()
                _SID_ENTERCHATready = True
                If _AllowEnterChat Then
                    SEND_SID_ENTERCHAT()
                    SEND_SID_JOINCHANNEL()
                End If
            Case Else : RaiseEvent BNET_Error_AccountUnknownError()
        End Select
    End Sub

    ''' <summary>
    ''' Response to <seealso cref="SEND_SID_ENTERCHAT">SID_ENTERCHAT</seealso>.
    ''' Contains the unique name assigned to the current login.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>This will be mangled on official servers (Account##).  PvPGN servers
    ''' disconnect whoever was using the account and assigns the new connection the name, and
    ''' no mangling takes place.</remarks>
    Private Sub PARSE_SID_ENTERCHAT(ByVal pck As BncsReader)
        TrueUsername = pck.ReadCString
        RaiseEvent BNET_EnteredChat(TrueUsername)
        pck = Nothing
        SEND_SID_FRIENDSLIST()
    End Sub

    ''' <summary>
    ''' Response to <seealso cref="SEND_SID_PING">SID_PING</seealso>.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>Simple client emulation.  Currently has no practical importance.</remarks>
    Private Sub PARSE_SID_PING(ByVal pck As BncsReader)
        SEND_SID_PING(pck.ReadUInt32)
    End Sub

    ''' <summary>
    ''' Notification of a chat event.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>Chat events are the bulk of Battle.net interactions. The 12 bytes that are 
    ''' skipped by Seek() are the IP address, Account number, and Registration Authority.
    ''' All of these are defunct.</remarks>
    Private Sub PARSE_SID_CHATEVENT(ByVal pck As BncsReader)
        Dim EventID As Integer
        Dim UserFlags As Integer
        Dim Ping As Integer
        Dim Username As String
        Dim Text As String

        EventID = pck.ReadInt32
        UserFlags = pck.ReadInt32
        Ping = pck.ReadInt32
        pck.Seek(12)
        Username = pck.ReadCString
        Text = pck.ReadCString

        Select Case EventID
            Case ChatEvents.EID_SHOWUSER : RaiseEvent BNET_ShowUser(UserFlags, Ping, Username, Text)
            Case ChatEvents.EID_JOIN : RaiseEvent BNET_UserJoined(UserFlags, Ping, Username, Text)
            Case ChatEvents.EID_LEAVE : RaiseEvent BNET_UserLeft(UserFlags, Ping, Username, Text)
            Case ChatEvents.EID_WHISPER : RaiseEvent BNET_UserWhisperRecieved(UserFlags, Ping, Username, Text)
            Case ChatEvents.EID_TALK : RaiseEvent BNET_UserTalk(UserFlags, Ping, Username, Text)
            Case ChatEvents.EID_BROADCAST : RaiseEvent BNET_Info_Broadcast(Text)
            Case ChatEvents.EID_CHANNEL : RaiseEvent BNET_Info_Channel(Text, UserFlags)
            Case ChatEvents.EID_USERFLAGS : RaiseEvent BNET_UserFlagsUpdate(UserFlags, Ping, Username, Text)
            Case ChatEvents.EID_WHISPERSENT : RaiseEvent BNET_UserWhisperSent(UserFlags, Ping, Username, Text)
            Case ChatEvents.EID_CHANNELFULL : RaiseEvent BNET_Error_ChannelFull(Text)
            Case ChatEvents.EID_CHANNELDOESNOTEXIST : RaiseEvent BNET_Error_ChannelDoesNotExist(Text)
            Case ChatEvents.EID_CHANNELRESTRICTED : RaiseEvent BNET_Error_ChannelRestricted(Text)
            Case ChatEvents.EID_INFO : RaiseEvent BNET_Info_Information(Text)
            Case ChatEvents.EID_ERROR : RaiseEvent BNET_Error_ErrorMessage(Text)
            Case ChatEvents.EID_EMOTE : RaiseEvent BNET_UserEmote(UserFlags, Ping, Username, Text)
        End Select
    End Sub

    ''' <summary>
    ''' Handles parsing of the binary friends list.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks></remarks>
    Private Sub PARSE_SID_FRIENDSLIST(ByVal pck As BncsReader)
        '(BYTE) Number of Entries
        'For each entry:
        '(STRING) Account
        '(BYTE) Status
        '(BYTE) Location
        '(DWORD) ProductID
        '(STRING) Location name
        Dim num As Integer = pck.ReadByte()
        Dim i As Integer

        For i = 0 To num - 1
            Dim account As String = pck.ReadCString()
            Dim status As Byte = pck.ReadByte()
            Dim loc As Byte = pck.ReadByte()
            Dim prodID As Long = pck.ReadInt32()
            Dim location As String = pck.ReadCString()
            RaiseEvent BNET_FriendsList(account, status, loc, prodID, location)
        Next i
    End Sub

    ''' <summary>
    ''' Handles parsing of the binary friends add.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks></remarks>
    Private Sub PARSE_SID_FRIENDSADD(ByVal pck As BncsReader)
        Dim account As String = pck.ReadCString()
        Dim status As Byte = pck.ReadByte()
        Dim loc As Byte = pck.ReadByte()
        Dim prodID As Long = pck.ReadInt32()
        Dim location As String = pck.ReadCString()
        RaiseEvent BNET_FriendsAdd(account, status, loc, prodID, location)
    End Sub

    ''' <summary>
    ''' Handles parsing of the binary friends remove.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>The friend's index in the list is given, not the actual account.  This makes
    ''' order rather important in displaying the friends list.</remarks>
    Private Sub PARSE_SID_FRIENDSREMOVE(ByVal pck As BncsReader)
        RaiseEvent BNET_FriendsRemove(pck.ReadByte())
    End Sub

    ''' <summary>
    ''' Handles parsing of the binary friends position change.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>The friend's index in the list is given, not the actual account.  This makes
    ''' order rather important in displaying the friends list.</remarks>
    Private Sub PARSE_SID_FRIENDSPOSITION(ByVal pck As BncsReader)
        RaiseEvent BNET_FriendsPosition(pck.ReadByte(), pck.ReadByte())
    End Sub

    ''' <summary>
    ''' Handles parsing of the binary friends update.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>The friend's index in the list is given, not the actual account.  This makes
    ''' order rather important in displaying the friends list.</remarks>
    Private Sub PARSE_SID_FRIENDSUPDATE(ByVal pck As BncsReader)
        '(BYTE) Entry number
        '(BYTE) Friend Location
        '(BYTE) Friend Status
        '(DWORD) ProductID
        '(STRING) Location
        Dim pos As Byte = pck.ReadByte()
        Dim loc As Byte = pck.ReadByte()
        Dim status As Byte = pck.ReadByte()
        Dim prod As Long = pck.ReadInt32()
        Dim location As String = pck.ReadCString()
        RaiseEvent BNET_FriendsUpdate(pos, status, loc, prod, location)
    End Sub

    ''' <summary>
    ''' Parse of <seealso cref="RequestProfile">SID_READUSERDATA</seealso>.
    ''' </summary>
    ''' <param name="pck">BncsReader passed from <see cref="ParsePacket">Dispatch</see></param>
    ''' <remarks>The packet contains the ID passed for that request in
    ''' <seealso cref="RequestProfile">SID_READUSERDATA</seealso>, and is passed to the event
    ''' for easy display processing.</remarks>
    Private Sub PARSE_SID_READUSERDATA(ByVal pck As BncsReader)
        '(DWORD) Number of accounts
        '(DWORD) Number of keys
        '(DWORD) Request ID
        '(STRING) [] Requested Key Values 

        Dim tmp As New ProfileInfo
        Dim val As Long
        pck.ReadInt32()
        pck.ReadInt32()
        val = pck.ReadInt32()

        tmp.WINS = pck.ReadCString()
        tmp.LOSSES = pck.ReadCString()
        tmp.DISCONNECTS = pck.ReadCString()
        tmp.WINSLADDER = pck.ReadCString()
        tmp.LOSSESLADDER = pck.ReadCString()
        tmp.DISCONNECTSLADDER = pck.ReadCString()
        tmp.RATINGLADDER = pck.ReadCString()
        tmp.LOCATION = pck.ReadCString()
        tmp.DESCRIPTION = pck.ReadCString()

        RaiseEvent BNET_ProfileInfo(val.ToString(), tmp)

        tmp = Nothing
    End Sub

#End Region
End Class

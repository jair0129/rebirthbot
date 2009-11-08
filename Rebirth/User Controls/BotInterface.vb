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

Imports System.IO
Imports System.Threading

''' <summary>
''' Main interface of the program.
''' </summary>
''' <remarks>There's way too much to comment right now.  I'll get around to it
''' later some time.</remarks>

Public Class BotInterface

    Private WithEvents localText As Localization

    Private TextEvents As Collection ' ChatItem
    Private IconList As List(Of BnetIcon)

    Private ProfilePanes As Collection

    Private WithEvents BOT As BnetConnection
    Private WithEvents BNFTP As BnetFtp
    Private WithEvents BniIcons As IconsProcessor
    Private WithEvents MColor As MasterColor

    Private _channel As String
    Private _name As String
    Private _admin As Color
    Private _sysop As Color
    Private _chanop As Color
    Private _moderator As Color
    Private _me As Color
    Private _lastTab As String
    Private _triedTabs As List(Of String)

    Public langCode As String
    Public pConf As ProfileConfig

    Public EnableConnect As Boolean

#Region "delegate event declares"
    Private Delegate Sub BNET_AccountLogonAcceptedDelegate()
    Private Delegate Sub BNET_AccountLogonProofDelegate()
    Private Delegate Sub BNET_AccountLogonSuccessSaltDelegate()
    Private Delegate Sub BNET_AccountLogonSuccessDelegate()
    Private Delegate Sub BNET_ConnectionStartedDelegate()
    Private Delegate Sub BNET_ConnectedDelegate()
    Private Delegate Sub BNET_DisconnectedDelegate()
    Private Delegate Sub BNET_EnteredChatDelegate(ByVal UniqueName As String)
    Private Delegate Sub BNET_Error_AccountDoesNotExistDelegate()
    Private Delegate Sub BNET_Error_AccountIsBannedDelegate()
    Private Delegate Sub BNET_Error_AccountUnknownErrorDelegate()
    Private Delegate Sub BNET_Error_AccountUpgradeDelegate()
    Private Delegate Sub BNET_Error_CdkeyBannedDelegate()
    Private Delegate Sub BNET_Error_ChannelDoesNotExistDelegate(ByVal Test As String)
    Private Delegate Sub BNET_Error_ChannelFullDelegate(ByVal Text As String)
    Private Delegate Sub BNET_Error_ChannelRestrictedDelegate(ByVal Text As String)
    Private Delegate Sub BNET_Error_ErrorMessageDelegate(ByVal Text As String)
    Private Delegate Sub BNET_Error_GameVersionDowngradeDelegate()
    Private Delegate Sub BNET_Error_InvalidCdkeyDelegate()
    Private Delegate Sub BNET_Error_InvalidGameVersionDelegate()
    Private Delegate Sub BNET_Error_InvalidPasswordDelegate()
    Private Delegate Sub BNET_Error_KeyInUseDelegate(ByVal UserName As String)
    Private Delegate Sub BNET_Error_OldGameVersionDelegate()
    Private Delegate Sub BNET_Error_RegisterEmailDelegate()
    Private Delegate Sub BNET_Error_ServerClosedConnectionDelegate()
    Private Delegate Sub BNET_Error_VersionCheckFailedDelegate()
    Private Delegate Sub BNET_Error_WrongProductInfoDelegate()
    Private Delegate Sub BNET_ExceptionDelegate(ByVal ex As Exception)
    Private Delegate Sub BNET_FriendsAddDelegate(ByVal account As String, ByVal status As Byte, ByVal locStatus As Byte, ByVal ProductID As Long, ByVal Location As String)
    Private Delegate Sub BNET_FriendsListDelegate(ByVal account As String, ByVal status As Byte, ByVal locStatus As Byte, ByVal ProductID As Long, ByVal Location As String)
    Private Delegate Sub BNET_FriendsPositionDelegate(ByVal oldIndex As Byte, ByVal newIndex As Byte)
    Private Delegate Sub BNET_FriendsRemoveDelegate(ByVal index As Byte)
    Private Delegate Sub BNET_FriendsUpdateDelegate(ByVal index As Byte, ByVal status As Byte, ByVal locStatus As Byte, ByVal ProductID As Long, ByVal Location As String)
    Private Delegate Sub BNET_GetIconDataDelegate(ByVal filetime As ULong, ByVal filename As String, ByVal client As String, ByVal cdkey As String, ByVal server As String)
    Private Delegate Sub BNET_Info_BroadcastDelegate(ByVal Text As String)
    Private Delegate Sub BNET_Info_ChannelDelegate(ByVal Text As String, ByVal Flags As Long)
    Private Delegate Sub BNET_Info_InformationDelegate(ByVal Text As String)
    Private Delegate Sub BNET_PacketReceivedDelegate(ByVal pID As Byte)
    Private Delegate Sub BNET_PacketSentDelegate(ByVal Packet As SentBnetData)
    Private Delegate Sub BNET_SendChatMessageDelegate(ByVal TrueName As String, ByVal Message As String)
    Private Delegate Sub BNET_SendingLoginRequestDelegate()
    Private Delegate Sub BNET_ShowUserDelegate(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Private Delegate Sub BNET_SocketErrorDelegate(ByVal errCode As String, ByVal errMesssage As String)
    Private Delegate Sub BNET_UnhandledPacketDelegate(ByVal pID As Byte, ByVal Data As String)
    Private Delegate Sub BNET_VerifyingGameVersionDelegate()
    Private Delegate Sub BNET_UserEmoteDelegate(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Private Delegate Sub BNET_UserFlagsUpdateDelegate(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Private Delegate Sub BNET_UserJoinedDelegate(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Private Delegate Sub BNET_UserLeftDelegate(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Private Delegate Sub BNET_UserWhisperRecievedDelegate(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Private Delegate Sub BNET_UserWhisperSentDelegate(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Private Delegate Sub BNET_UserTalkDelegate(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
    Private Delegate Sub BNET_UsingHashFileDelegate(ByVal HashPath As String)
    Private Delegate Sub BNET_UsingLockdownDelegate()
    Private Delegate Sub BNET_WAR3ServerInvalidDelegate()
    Private Delegate Sub BNET_VersionCheckPassedDelegate()

    Private Delegate Sub ConstructingIconsDelegate()
    Private Delegate Sub ExtractingIconsDelegate()
    Private Delegate Sub IconCountDelegate(ByVal num As Integer)
#End Region

    Public Sub ResizeInstance()
        ' width/left resizes
        Me.rtbChat.Width = Me.Width - Me.tabLists.Width
        Me.cmbTextEntry.Width = Me.Width - Me.tabLists.Width
        Me.tabLists.Left = Me.rtbChat.Width + 3

        ' height/top resizes
        Me.rtbChat.Height = Me.Height - Me.cmbTextEntry.Height
        Me.cmbTextEntry.Top = Me.Height - Me.cmbTextEntry.Height
        Me.tabLists.Height = Me.Height
        Me.lvChannel.Height = Me.tabLists.TabPages.Item(0).Height - Me.txtChannel.Height
        Me.lvFriends.Height = Me.tabLists.TabPages.Item(1).Height
        Me.lvBotnetUsers.Height = Me.tabLists.TabPages.Item(2).Height - Me.txtBotnet.Height
        Me.lvClan.Height = Me.tabLists.TabPages.Item(0).Height - Me.txtClan.Height
    End Sub

    Private Sub BotInterface_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim k As New Config
        k.LoadConfig(Me.Tag)

        k.ConfirmColorsFile()

        MColor = New MasterColor
        MColor.LoadColors(Me.Tag)

        Me._admin = MColor.GetColorBlizzard.VALUE
        Me._sysop = MColor.GetColorSysop.VALUE
        Me._chanop = MColor.GetColorChanOp.VALUE
        Me._moderator = MColor.GetColorModerator.VALUE
        Me._me = MColor.GetColorMe.VALUE

        Dim i As InterfaceItem
        i = MColor.GetChatWindow
        Me.rtbChat.Font = i.FONT
        Me.rtbChat.BackColor = i.BACKCOLOR.VALUE
        Me.rtbChat.ForeColor = i.FORECOLOR.VALUE

        i = MColor.GetChannelLabel
        Me.txtChannel.Font = i.FONT
        Me.txtChannel.BackColor = i.BACKCOLOR.VALUE
        Me.txtChannel.ForeColor = i.FORECOLOR.VALUE

        i = MColor.GetChannelUserList
        Me.lvChannel.Font = i.FONT
        Me.lvChannel.BackColor = i.BACKCOLOR.VALUE
        Me.lvChannel.ForeColor = i.FORECOLOR.VALUE

        i = MColor.GetSendChatBox
        Me.cmbTextEntry.Font = i.FONT
        Me.cmbTextEntry.BackColor = i.BACKCOLOR.VALUE
        Me.cmbTextEntry.ForeColor = i.FORECOLOR.VALUE

        i = MColor.GetFriendsList
        Me.lvFriends.Font = i.FONT
        Me.lvFriends.BackColor = i.BACKCOLOR.VALUE
        Me.lvFriends.ForeColor = i.FORECOLOR.VALUE

        i = MColor.GetBotnetLabel
        Me.txtBotnet.Font = i.FONT
        Me.txtBotnet.BackColor = i.BACKCOLOR.VALUE
        Me.txtBotnet.ForeColor = i.FORECOLOR.VALUE

        i = MColor.GetBotnetUserList
        Me.lvBotnetUsers.Font = i.FONT
        Me.lvBotnetUsers.BackColor = i.BACKCOLOR.VALUE
        Me.lvBotnetUsers.ForeColor = i.FORECOLOR.VALUE

        i = MColor.GetclanList
        Me.lvClan.Font = i.FONT
        Me.lvClan.BackColor = i.BACKCOLOR.VALUE
        Me.lvClan.ForeColor = i.FORECOLOR.VALUE

        k = Nothing
    End Sub

    Private Sub BotInterface_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Call Me.ResizeInstance()
    End Sub

    Public Sub Disconnect()
        If BOT Is Nothing Then Exit Sub
        BOT.Disconnect()
        lvChannel.Items.Clear()
        txtChannel.Text = ""
    End Sub

    Private Sub ReprocessChannelList()
        Dim order As Integer = 4
        Dim flags As Long
        Dim imageIndex As Integer
        Dim itemKey As String

        For Each item As ListViewItem In Me.lvChannel.Items
            If item.ImageIndex = -1 Then
                itemKey = item.SubItems(1).Text
                Me.lvChannel.Items.RemoveByKey(itemKey)
                flags = Val("&H" & item.SubItems(3).Text) ' fugly, i know
                If (flags And &H1) = &H1 Then order = 0
                If (flags And &H8) = &H8 Then order = 1
                If (flags And &H2) = &H2 Then order = 2
                If (flags And &H4) = &H4 Then order = 3
                imageIndex = GetChannelListImageIndex(IconList, flags, GetProductAsLong(item.SubItems(5).Text))

                Me.lvChannel.Items.Add(itemKey, order.ToString(), imageIndex)
                Me.lvChannel.Items(itemKey).SubItems.Add(itemKey)
                Me.lvChannel.Items(itemKey).SubItems.Add(item.SubItems(2).Text)
                Me.lvChannel.Items(itemKey).SubItems.Add(item.SubItems(3).Text)
                Me.lvChannel.Items(itemKey).SubItems.Add(imageIndex)
                Me.lvChannel.Items(itemKey).SubItems.Add(item.SubItems(5).Text)
            End If
        Next
    End Sub

    Public Sub UpdateChannelInfo()
        If BOT Is Nothing Then
            Me.txtChannel.Text = ""
            Exit Sub
        End If

        If BOT.IsConnected() Then
            Me.txtChannel.Text = _channel & " [" & Me.lvChannel.Items.Count() & "] "
        Else
            Me.txtChannel.Text = "Disconnected"
        End If
    End Sub

    Public Sub Connect()
        If EnableConnect Then
            ProfilePanes = New Collection
            Dim k As New Config
            k.LoadConfig(Me.Tag)
            Debug.Print("Username: " & k.BNET.USERNAME)
            Debug.Print("Password: " & k.BNET.PASSWORD)
            Debug.Print("Server: " & k.BNET.SERVER)
            Debug.Print("CDKey: " & k.BNET.CDKEY)
            Debug.Print("Client: " & k.BNET.CLIENT)
            Debug.Print("Home: " & k.BNET.HOME)
            Debug.Print("Owner: " & k.BNET.OWNER)
            BOT = New BnetConnection(k.BNET)
            k = Nothing
            BOT.Connect()
        Else
            Me.AddC("Connection disabled!")
        End If
    End Sub

    Public Sub AddC(ByVal ParamArray saElements() As Object)
        Dim i As Integer

        With Me.rtbChat
            .SuspendLayout()
            .SelectionStart = .TextLength
            .SelectionLength = 0
            .SelectionColor = Color.DarkGray
            .SelectedText = Format(Now, " [hh:mm:ss] ")

            For i = LBound(saElements) To UBound(saElements) Step 2
                'Application.DoEvents()
                .SelectionStart = .TextLength
                .SelectionLength = 0
                .SelectionColor = saElements(i)
                .SelectedText = saElements(i + 1) & Strings.Left(vbCrLf, -2 * CLng((i + 1) = UBound(saElements)))
                .SelectionStart = .TextLength
                .ScrollToCaret()
            Next i

            If Len(.Text) > 10000 Then
                Dim tmp As String = .Rtf

                Dim s As Integer = tmp.IndexOf("\par", 0)
                Dim en As Integer = tmp.IndexOf("\par", s + 5)
                Dim a As String = tmp.Substring(0, s + 5) & "\cf1\f0\fs17"
                Dim b As String = tmp.Substring(en + 5)

                tmp = a & b

                .Rtf = tmp
                .SelectionStart = Len(.Text)
            End If
            .ResumeLayout()
        End With
    End Sub

    Public Sub RemoveProfile(ByVal ID As String)
        If Me.ProfilePanes Is Nothing Then Exit Sub
        If Not Me.ProfilePanes.Contains(ID) Then Exit Sub

        Me.ProfilePanes.Remove(ID)
    End Sub

#Region "invoked bnet events"
    Public Sub BNET_AccountLogonSuccess()
        TextEvents.Item("bnetaccountlogonsuccess").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_ConnectionStarted()
        TextEvents.Item("bnetconnecting").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_Connected()
        TextEvents.Item("bnetconnected").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_Disconnected()
        TextEvents.Item("bnetdisconnected").AddText(Me.rtbChat, Nothing)
        BOT.Disconnect()
        lvChannel.Items.Clear()
        txtChannel.Text = ""
    End Sub

    Public Sub BNET_SocketError(ByVal errCode As String, ByVal errMessage As String)

    End Sub

    Public Sub BNET_Exception(ByVal ex As Exception)
        TextEvents.Item("bnetexception").AddText(Me.rtbChat, ex.Message)
    End Sub

    Public Sub BNET_PacketReceived(ByVal pID As Byte)
        'Debug.Print("BNET RECV > " & Hex(pID))
    End Sub

    Public Sub BNET_PacketSent(ByVal Packet As SentBnetData)
        'Debug.Print("BNET SEND > " & Packet.NAME & " [" & Hex(Packet.ID) & "]")
    End Sub

    Public Sub BNET_UnhandledPacket(ByVal pID As Byte, ByVal Data As String)
        TextEvents.Item("bnetunhandledpacket").AddText(Me.rtbChat, pID, Data)
    End Sub

    Public Sub BNET_VerifyingGameVersion()
        TextEvents.Item("bnetverifyinggameversion").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_GetIconData(ByVal filetime As ULong, ByVal filename As String, ByVal client As String, ByVal cdkey As String, ByVal Server As String)
        Dim fp As String = Application.StartupPath & "\icons\" & Server & "_" & filename
        If Not File.Exists(fp) Then GoTo getFile
        Dim check As New FileInfo(fp)
        Dim localtime As Date = check.LastWriteTimeUtc()

        If filetime <> localtime.Ticks Then
            Call Me.BNFTP_FileDownloadComplete(Server & "_" & filename, Application.StartupPath & "\icons", True)
            Exit Sub
        End If

getFile:
        If client = "WAR3" Or client = "W3XP" Then
            BNFTP = New BnetFtp(filename, filetime, Server, client, cdkey)
            BNFTP.Execute()
        Else
            BNFTP = New BnetFtp(filename, filetime, Server, client)
            BNFTP.Execute()
        End If
    End Sub

    Public Sub BNET_UsingLockdown()
        TextEvents.Item("bnetusinglockdown").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_UsingHashFile(ByVal HashPath As String)
        Dim localPath As String = HashPath.Replace(Application.StartupPath, "")
        TextEvents.Item("bnetusinghashfile").AddText(Me.rtbChat, HashPath, localPath)
    End Sub

    Public Sub BNET_WAR3ServerInvalid()
        TextEvents.Item("bnetwar3serverinvalid").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_SendChatMessage(ByVal TrueName As String, ByVal Message As String)
        If Message.Chars(0) <> "/" Then
            TextEvents.Item("bnetsendchatmessage").AddText(Me.rtbChat, TrueName, Message)
        End If
    End Sub

    Public Sub BNET_VersionCheckFailed()
        TextEvents.Item("bnetversioncheckfailed").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_VersionCheckPassed()
        TextEvents.Item("bnetversioncheckpassed").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_Error_OldGameVersion()
        TextEvents.Item("bnetoldgameversion").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_Error_InvalidGameVersion()
        TextEvents.Item("bnetinvalidgameversion").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_Error_GameVersionDowngrade()
        TextEvents.Item("bnetgameversiondowngrade").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_Error_InvalidCdkey()
        AddC(Color.Red, "BNET > Invalid CD-Key")
    End Sub

    Public Sub BNET_Error_KeyInUse(ByVal UserName As String)
        AddC(Color.Red, "BNET > CD-Key in us by ", Color.LightBlue, UserName)
    End Sub

    Public Sub BNET_Error_CdkeyBanned()
        TextEvents.Item("bnetcdkeybanned").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_Error_WrongProductInfo()
        TextEvents.Item("bnetwrongproductinfo").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_SendingLoginRequest()
        TextEvents.Item("bnetsendingloginrequest").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_Error_AccountDoesNotExist()
        TextEvents.Item("bnetaccountdoesnotexist").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_Error_InvalidPassword()
        TextEvents.Item("bnetinvalidpassword").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_Error_AccountIsBanned()
        TextEvents.Item("bnetaccountisbanned").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_EnteredChat(ByVal UniqueName As String)
        TextEvents.Item("bnetenteredchat").AddText(Me.rtbChat, UniqueName)
        _name = UniqueName
    End Sub

    Public Sub BNET_ShowUser(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
        'AddC(Color.DarkGray, "User in channel: ", Color.LightBlue, Username, Color.DarkGray, " [", Color.LightBlue, Hex(Flags), Color.DarkGray, "]")

        Dim order As Integer = 4
        Dim col As Color = Color.White

        If (Flags And &H1) = &H1 Then
            order = 0
            col = Me._admin
        End If

        If (Flags And &H8) = &H8 Then
            order = 1
            col = Me._sysop
        End If

        If (Flags And &H2) = &H2 Then
            order = 2
            col = Me._chanop
        End If

        If (Flags And &H4) = &H4 Then
            order = 3
            col = Me._moderator
        End If

        If Username = _name Then col = Me._me
        '(flags 0x01), Battle.net (0x08), Operator (0x02), and Moderator (0x04)

        Dim imageIndex As Integer = GetChannelListImageIndex(IconList, Flags, GetProductAsLong(Text))
        lvChannel.Items.Add(Username, order.ToString(), imageIndex)
        lvChannel.Items(Username).SubItems.Add(Username)
        lvChannel.Items(Username).SubItems.Add(Ping.ToString())
        lvChannel.Items(Username).SubItems.Add(Hex(Flags).ToString())
        lvChannel.Items(Username).SubItems.Add(imageIndex)
        lvChannel.Items(Username).SubItems.Add(Text)
        lvChannel.Items(Username).ForeColor = col

        'Debug.Print("SHOW > Username: " & Username & ", Flags=" & Flags & ", ImageIndex=" & imageIndex)
        Call UpdateChannelInfo()
        TextEvents.Item("bnetshowuser").AddText(Me.rtbChat, Username, Hex(Flags).ToString(), Ping, Text)

    End Sub

    Public Sub BNET_UserJoined(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
        Dim order As Integer = 4
        Dim col As Color = Color.White

        If (Flags And &H1) = &H1 Then
            order = 0
            col = Me._admin
        End If

        If (Flags And &H8) = &H8 Then
            order = 1
            col = Me._sysop
        End If

        If (Flags And &H2) = &H2 Then
            order = 2
            col = Me._chanop
        End If

        If (Flags And &H4) = &H4 Then
            order = 3
            col = Me._moderator
        End If

        If Username = _name Then col = Me._me
        '(flags 0x01), Battle.net (0x08), Operator (0x02), and Moderator (0x04)

        Dim imageIndex As Integer = GetChannelListImageIndex(IconList, Flags, GetProductAsLong(Text))
        lvChannel.Items.Add(Username, order.ToString(), imageIndex)
        lvChannel.Items(Username).SubItems.Add(Username)
        lvChannel.Items(Username).SubItems.Add(Ping.ToString())
        lvChannel.Items(Username).SubItems.Add(Hex(Flags).ToString())
        lvChannel.Items(Username).SubItems.Add(imageIndex)
        lvChannel.Items(Username).SubItems.Add(Text)
        lvChannel.Items(Username).ForeColor = col

        'Debug.Print("JOIN > Username: " & Username & ", Flags=" & Flags & ", ImageIndex=" & imageIndex)
        Call UpdateChannelInfo()
        TextEvents.Item("bnetuserjoined").AddText(Me.rtbChat, Username, Hex(Flags).ToString(), Ping, Text)

    End Sub

    Public Sub BNET_UserLeft(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
        TextEvents.Item("bnetuserleft").AddText(Me.rtbChat, Username, Hex(Flags).ToString(), Ping, Text)
        lvChannel.Items.RemoveByKey(Username)
        Call UpdateChannelInfo()
    End Sub

    Public Sub BNET_UserWhisperRecieved(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
        TextEvents.Item("bnetwhisperrecieved").AddText(Me.rtbChat, Username, Text, Hex(Flags).ToString(), Ping)
    End Sub

    Public Sub BNET_UserTalk(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
        If (Flags And &H1) = &H1 Then
            TextEvents.Item("bnetblizzardtalk").AddText(Me.rtbChat, Username, Text, Hex(Flags).ToString(), Ping)
        ElseIf (Flags And &H8) = &H8 Then
            TextEvents.Item("bnetsysoptalk").AddText(Me.rtbChat, Username, Text, Hex(Flags).ToString(), Ping)
        ElseIf (Flags And &H2) = &H2 Then
            TextEvents.Item("bnetchanoptalk").AddText(Me.rtbChat, Username, Text, Hex(Flags).ToString(), Ping)
        ElseIf (Flags And &H4) = &H4 Then
            TextEvents.Item("bnetmoderatortalk").AddText(Me.rtbChat, Username, Text, Hex(Flags).ToString(), Ping)
        Else
            TextEvents.Item("bnetusertalk").AddText(Me.rtbChat, Username, Text, Hex(Flags).ToString(), Ping)
        End If
    End Sub

    Public Sub BNET_Info_Broadcast(ByVal Text As String)
        TextEvents.Item("bnetbroadcast").AddText(Me.rtbChat, Text)
    End Sub

    Public Sub BNET_Info_Channel(ByVal Text As String, ByVal Flags As Long)
        Dim k As String = TextEvents.Item("channelpublic").ToString()

        If (Flags And CHAN_PUBLIC) = CHAN_PUBLIC Then k = TextEvents.Item("channelpublic").ToString()
        If (Flags And CHAN_MODERATED) = CHAN_MODERATED Then k = TextEvents.Item("channelmoderated").ToString()
        If (Flags And CHAN_RESTRICTED) = CHAN_RESTRICTED Then k = TextEvents.Item("channelrestricted").ToString()
        If (Flags And CHAN_SILENT) = CHAN_SILENT Then k = TextEvents.Item("channelsilent").ToString()
        If (Flags And CHAN_SYSTEM) = CHAN_SYSTEM Then k = TextEvents.Item("channelsystem").ToString()
        If (Flags And CHAN_PRODSPEC) = CHAN_PRODSPEC Then k = TextEvents.Item("channelprodspec").ToString()
        If (Flags And CHAN_GLOBAL) = CHAN_GLOBAL Then k = TextEvents.Item("channelglobal").ToString()

        TextEvents.Item("bnetchanneljoined").AddText(Me.rtbChat, Text, k)
        _channel = Text
        Me.lvChannel.Items.Clear()
        Call UpdateChannelInfo()
    End Sub

    Public Sub BNET_UserFlagsUpdate(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)

        If Not lvChannel.Items.ContainsKey(Username) Then Exit Sub

        Dim order As Integer = 4
        Dim col As Color = Color.White

        If (Flags And &H1) = &H1 Then
            order = 0
            col = Me._admin
        End If

        If (Flags And &H8) = &H8 Then
            order = 1
            col = Me._sysop
        End If

        If (Flags And &H2) = &H2 Then
            order = 2
            col = Me._chanop
        End If

        If (Flags And &H4) = &H4 Then
            order = 3
            col = Me._moderator
        End If

        If Username = _name Then col = Me._me

        Dim imageIndex As Integer = GetChannelListImageIndex(IconList, Flags, GetProductAsLong(Text))

        If lvChannel.Items(Username).Text = order.ToString() Then
            lvChannel.Items(Username).ImageIndex = imageIndex
            lvChannel.Items(Username).SubItems(2).Text = Ping.ToString()
            lvChannel.Items(Username).SubItems(3).Text = Hex(Flags).ToString()
            lvChannel.Items(Username).SubItems(4).Text = imageIndex
            lvChannel.Items(Username).ForeColor = col
            Exit Sub
        End If

        lvChannel.Items.RemoveByKey(Username)

        lvChannel.Items.Add(Username, order.ToString(), imageIndex)
        lvChannel.Items(Username).SubItems.Add(Username)
        lvChannel.Items(Username).SubItems.Add(Ping.ToString())
        lvChannel.Items(Username).SubItems.Add(Hex(Flags).ToString())
        lvChannel.Items(Username).SubItems.Add(imageIndex)
        lvChannel.Items(Username).ForeColor = col

        TextEvents.Item("bnetflagsupdate").AddText(Me.rtbChat, Username, Hex(Flags).ToString(), lvChannel.Items(Username).SubItems(3).Text, Ping, Text)

    End Sub

    Public Sub BNET_UserWhisperSent(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
        TextEvents.Item("bnetwhispersent").AddText(Me.rtbChat, Username, Text, Hex(Flags).ToString(), Ping)
    End Sub

    Public Sub BNET_Error_ChannelFull(ByVal Text As String)
        TextEvents.Item("bnetchannelfull").AddText(Me.rtbChat, Text)
    End Sub

    Public Sub BNET_Error_ChannelDoesNotExist(ByVal Test As String)
        TextEvents.Item("bnetchanneldoesnotexist").AddText(Me.rtbChat, Text)
    End Sub

    Public Sub BNET_Error_ChannelRestricted(ByVal Text As String)
        TextEvents.Item("bnetrestricted").AddText(Me.rtbChat, Text)
    End Sub

    Public Sub BNET_Info_Information(ByVal Text As String)
        TextEvents.Item("bnetinformation").AddText(Me.rtbChat, Text)
    End Sub

    Public Sub BNET_Error_ErrorMessage(ByVal Text As String)
        TextEvents.Item("bneterror").AddText(Me.rtbChat, Text)
    End Sub

    Public Sub BNET_Error_ServerClosedConnection()
        TextEvents.Item("bnetserverclosedconnection").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_UserEmote(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String)
        If (Flags And &H1) = &H1 Then
            TextEvents.Item("bnetblizzardemote").AddText(Me.rtbChat, Username, Text, Hex(Flags).ToString(), Ping)
        ElseIf (Flags And &H8) = &H8 Then
            TextEvents.Item("bnetsysopemote").AddText(Me.rtbChat, Username, Text, Hex(Flags).ToString(), Ping)
        ElseIf (Flags And &H2) = &H2 Then
            TextEvents.Item("bnetchanopemote").AddText(Me.rtbChat, Username, Text, Hex(Flags).ToString(), Ping)
        ElseIf (Flags And &H4) = &H4 Then
            TextEvents.Item("bnetmoderatoremote").AddText(Me.rtbChat, Username, Text, Hex(Flags).ToString(), Ping)
        Else
            TextEvents.Item("bnetuseremote").AddText(Me.rtbChat, Username, Text, Hex(Flags).ToString(), Ping)
        End If

    End Sub

    Public Sub BNET_FriendsList(ByVal account As String, ByVal status As Byte, ByVal locStatus As Byte, ByVal ProductID As Long, ByVal Location As String)
        Dim mutual As Boolean = ((status And &H1) = &H1)
        Dim dnd As Boolean = ((status And &H2) = &H2)
        Dim away As Boolean = ((status And &H4) = &H4)
        Dim _prod As String = "<Unknown>"
        Dim _sts As String = ""
        Dim _locS As String = ""
        Dim _locC As String = ""

        If dnd And away Then _sts = "DND/Away"
        If dnd And _sts = "" Then _sts = "DND"
        If away And _sts = "" Then _sts = "Away"
        If _sts = "" Then _sts = "Available"

        lvFriends.Items.Add(account, account, GetChannelListImageIndex(IconList, 0, ProductID))
        lvFriends.Items(account).SubItems.Add(mutual.ToString())

        Select Case locStatus
            Case &H0
                _locS = TextEvents.Item("friendslocationoffline").ToString()
                _locC = TextEvents.Item("friendslocationoffline").ToString()
                _sts = TextEvents.Item("friendslocationoffline").ToString()
            Case &H1
                _locS = TextEvents.Item("friendslocationnotinchat").ToString()
                _locC = TextEvents.Item("friendslocationnotinchat").ToString()
            Case &H2
                _locS = TextEvents.Item("friendslocationinchat").ToString()
                _locC = Location
            Case &H3
                _locS = TextEvents.Item("friendslocationinpublicgame").ToString()
                _locC = Location
            Case &H4
                _locS = TextEvents.Item("friendslocationinprivategame").ToString()
                _locC = TextEvents.Item("friendslocationnonmutual").ToString()
            Case &H5
                _locS = TextEvents.Item("friendslocationinprivategame").ToString()
                _locC = Location
            Case Else
                _locS = TextEvents.Item("friendslocationunknown").ToString()
                _locC = Location
        End Select

        Select Case ProductID
            Case CLIENT_STAR : _prod = "StarCraft"
            Case CLIENT_SEXP : _prod = "StarCraft: Brood War"
            Case CLIENT_D2DV : _prod = "Diablo II"
            Case CLIENT_D2XP : _prod = "Diablo II: Lord of Destruction"
            Case CLIENT_W2BN : _prod = "Warcraft II BNE"
            Case CLIENT_WAR3 : _prod = "Warcraft 3"
            Case CLIENT_W3XP : _prod = "Warcraft 3: The Frozen Throne"
        End Select

        If mutual Then
            TextEvents.Item("bnetmutualfriendslist").AddText(Me.rtbChat, account, _prod, _sts, _locS, _locC)
        Else
            TextEvents.Item("bnetfriendslist").AddText(Me.rtbChat, account, _prod, _sts, _locS, _locC)
        End If
    End Sub

    Public Sub BNET_FriendsAdd(ByVal account As String, ByVal status As Byte, ByVal locStatus As Byte, ByVal ProductID As Long, ByVal Location As String)
        Dim mutual As Boolean = ((status And &H1) = &H1)
        Dim dnd As Boolean = ((status And &H2) = &H2)
        Dim away As Boolean = ((status And &H4) = &H4)
        Dim _prod As String = "<Unknown>"
        Dim _sts As String = ""
        Dim _locS As String = ""
        Dim _locC As String = ""

        If dnd And away Then _sts = "DND/Away"
        If dnd And _sts = "" Then _sts = "DND"
        If away And _sts = "" Then _sts = "Away"
        If _sts = "" Then _sts = "Available"

        lvFriends.Items.Add(account, account, GetChannelListImageIndex(IconList, 0, ProductID))
        lvFriends.Items(account).SubItems.Add(mutual.ToString())

        Select Case locStatus
            Case &H0
                _locS = TextEvents.Item("friendslocationoffline").ToString()
                _locC = TextEvents.Item("friendslocationoffline").ToString()
                _sts = TextEvents.Item("friendslocationoffline").ToString()
            Case &H1
                _locS = TextEvents.Item("friendslocationnotinchat").ToString()
                _locC = TextEvents.Item("friendslocationnotinchat").ToString()
            Case &H2
                _locS = TextEvents.Item("friendslocationinchat").ToString()
                _locC = Location
            Case &H3
                _locS = TextEvents.Item("friendslocationinpublicgame").ToString()
                _locC = Location
            Case &H4
                _locS = TextEvents.Item("friendslocationinprivategame").ToString()
                _locC = TextEvents.Item("friendslocationnonmutual").ToString()
            Case &H5
                _locS = TextEvents.Item("friendslocationinprivategame").ToString()
                _locC = Location
            Case Else
                _locS = TextEvents.Item("friendslocationunknown").ToString()
                _locC = Location
        End Select

        Select Case ProductID
            Case CLIENT_STAR : _prod = "StarCraft"
            Case CLIENT_SEXP : _prod = "StarCraft: Brood War"
            Case CLIENT_D2DV : _prod = "Diablo II"
            Case CLIENT_D2XP : _prod = "Diablo II: Lord of Destruction"
            Case CLIENT_W2BN : _prod = "Warcraft II BNE"
            Case CLIENT_WAR3 : _prod = "Warcraft 3"
            Case CLIENT_W3XP : _prod = "Warcraft 3: The Frozen Throne"
        End Select

        If mutual Then
            TextEvents.Item("bnetmutualfriendsadd").AddText(Me.rtbChat, account, _prod, _sts, _locS, _locC)
        Else
            TextEvents.Item("bnetfriendsadd").AddText(Me.rtbChat, account, _prod, _sts, _locS, _locC)
        End If
    End Sub

    Public Sub BNET_FriendsUpdate(ByVal index As Byte, ByVal status As Byte, ByVal locStatus As Byte, ByVal ProductID As Long, ByVal Location As String)
        If index > lvFriends.Items.Count() - 1 Then Exit Sub

        Dim mutual As Boolean = ((status And &H1) = &H1)
        Dim dnd As Boolean = ((status And &H2) = &H2)
        Dim away As Boolean = ((status And &H4) = &H4)
        Dim _prod As String = "<Unknown>"
        Dim _sts As String = ""
        Dim _locS As String = ""
        Dim _locC As String = ""

        If dnd And away Then _sts = "DND/Away"
        If dnd And _sts = "" Then _sts = "DND"
        If away And _sts = "" Then _sts = "Away"
        If _sts = "" Then _sts = "Available"
        Dim account As String = lvFriends.Items(index).Text

        lvFriends.Items(index).ImageIndex = GetChannelListImageIndex(IconList, 0, ProductID)
        lvFriends.Items(index).SubItems(1).Text = mutual

        Select Case locStatus
            Case &H0
                _locS = TextEvents.Item("friendslocationoffline").ToString()
                _locC = TextEvents.Item("friendslocationoffline").ToString()
                _sts = TextEvents.Item("friendslocationoffline").ToString()
            Case &H1
                _locS = TextEvents.Item("friendslocationnotinchat").ToString()
                _locC = TextEvents.Item("friendslocationnotinchat").ToString()
            Case &H2
                _locS = TextEvents.Item("friendslocationinchat").ToString()
                _locC = Location
            Case &H3
                _locS = TextEvents.Item("friendslocationinpublicgame").ToString()
                _locC = Location
            Case &H4
                _locS = TextEvents.Item("friendslocationinprivategame").ToString()
                _locC = TextEvents.Item("friendslocationnonmutual").ToString()
            Case &H5
                _locS = TextEvents.Item("friendslocationinprivategame").ToString()
                _locC = Location
            Case Else
                _locS = TextEvents.Item("friendslocationunknown").ToString()
                _locC = Location
        End Select

        Select Case ProductID
            Case CLIENT_STAR : _prod = "StarCraft"
            Case CLIENT_SEXP : _prod = "StarCraft: Brood War"
            Case CLIENT_D2DV : _prod = "Diablo II"
            Case CLIENT_D2XP : _prod = "Diablo II: Lord of Destruction"
            Case CLIENT_W2BN : _prod = "Warcraft II BNE"
            Case CLIENT_WAR3 : _prod = "Warcraft 3"
            Case CLIENT_W3XP : _prod = "Warcraft 3: The Frozen Throne"
        End Select

        If mutual Then
            TextEvents.Item("bnetmutualfriendsupdate").AddTextParseAfterArgs(Me.rtbChat, account, _prod, _sts, _locS, _locC)
        Else
            TextEvents.Item("bnetfriendsupdate").AddTextParseAfterArgs(Me.rtbChat, account, _prod, _sts, _locS, _locC)
        End If
    End Sub

    Public Sub BNET_FriendsRemove(ByVal index As Byte)
        If index > lvFriends.Items.Count() - 1 Then Exit Sub

        Dim account As String = lvFriends.Items(index).Text

        If lvFriends.Items(index).SubItems(1).Text = "True" Then
            TextEvents.Item("bnetmutualfriendsremove").AddText(Me.rtbChat, account)
        Else
            TextEvents.Item("bnetfriendsremove").AddText(Me.rtbChat, account)
        End If
    End Sub

    Public Sub BNET_FriendsPosition(ByVal oldIndex As Byte, ByVal newIndex As Byte)
        ' insert puts the item at the index given and moves everything else down
        Dim item As ListViewItem = lvFriends.Items(oldIndex)
        If oldIndex < newIndex Then
            lvFriends.Items.Insert(newIndex, item)
            lvFriends.Items.RemoveAt(oldIndex)
        Else
            lvFriends.Items.RemoveAt(oldIndex)
            lvFriends.Items.Insert(newIndex, item)
        End If

        If item.SubItems(1).Text = "True" Then
            TextEvents.Item("bnetmutualfriendsposition").AddText(Me.rtbChat, item.SubItems(0).Text, newIndex)
        Else
            TextEvents.Item("bnetfriendsposition").AddText(Me.rtbChat, item.SubItems(0).Text, newIndex)
        End If

        item = Nothing
    End Sub

    Public Sub BNET_AccountLogonAccepted()
        TextEvents.Item("bnetaccountlogonaccepted").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_AccountLogonProof()
        TextEvents.Item("bnetaccountlogonproof").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_AccountLogonSuccessSalt()
        TextEvents.Item("bnetaccountlogonsuccesssalt").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_Error_AccountUnknownError()
        TextEvents.Item("bnetaccountunknownerror").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_Error_AccountUpgrade()
        TextEvents.Item("bnetaccountupgrade").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub BNET_Error_RegisterEmail()
        TextEvents.Item("bnetregisteremail").AddText(Me.rtbChat, Nothing)
    End Sub
#End Region

    Public Sub ConstructingIcons()
        TextEvents.Item("bniconstructing").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub ExtractingIcons()
        TextEvents.Item("bniextracting").AddText(Me.rtbChat, Nothing)
    End Sub

    Public Sub IconCount(ByVal num As Integer)
        TextEvents.Item("bnicount").AddText(Me.rtbChat, num)
    End Sub

#Region "BnetConnection delegates"
    Private Sub BOT_BNET_AccountLogonAccepted() Handles BOT.BNET_AccountLogonAccepted

        Me.Invoke(New BNET_AccountLogonAcceptedDelegate(AddressOf BNET_AccountLogonAccepted))
    End Sub

    Private Sub BOT_BNET_AccountLogonProof() Handles BOT.BNET_AccountLogonProof
        Me.Invoke(New BNET_AccountLogonProofDelegate(AddressOf BNET_AccountLogonProof))
    End Sub

    Private Sub BOT_BNET_AccountLogonSuccess() Handles BOT.BNET_AccountLogonSuccess
        Me.Invoke(New BNET_AccountLogonSuccessDelegate(AddressOf BNET_AccountLogonSuccess))
    End Sub

    Private Sub BOT_BNET_AccountLogonSuccessSalt() Handles BOT.BNET_AccountLogonSuccessSalt
        Me.Invoke(New BNET_AccountLogonSuccessSaltDelegate(AddressOf BNET_AccountLogonSuccessSalt))
    End Sub

    Private Sub BOT_BNET_Connected() Handles BOT.BNET_Connected
        Me.Invoke(New BNET_ConnectedDelegate(AddressOf BNET_Connected))
    End Sub

    Private Sub BOT_BNET_ConnectionStarted() Handles BOT.BNET_ConnectionStarted
        Me.Invoke(New BNET_ConnectionStartedDelegate(AddressOf BNET_ConnectionStarted))
    End Sub

    Private Sub BOT_BNET_Disconnected() Handles BOT.BNET_Disconnected
        Me.Invoke(New BNET_DisconnectedDelegate(AddressOf BNET_Disconnected))
    End Sub

    Private Sub BOT_BNET_EnteredChat(ByVal UniqueName As String) Handles BOT.BNET_EnteredChat
        Me.Invoke(New BNET_EnteredChatDelegate(AddressOf BNET_EnteredChat), UniqueName)
    End Sub

    Private Sub BOT_BNET_Error_AccountDoesNotExist() Handles BOT.BNET_Error_AccountDoesNotExist
        Me.Invoke(New BNET_Error_AccountDoesNotExistDelegate(AddressOf BNET_Disconnected))
    End Sub

    Private Sub BOT_BNET_Error_AccountIsBanned() Handles BOT.BNET_Error_AccountIsBanned
        Me.Invoke(New BNET_Error_AccountIsBannedDelegate(AddressOf BNET_Error_AccountIsBanned))
    End Sub

    Private Sub BOT_BNET_Error_AccountUnknownError() Handles BOT.BNET_Error_AccountUnknownError
        Me.Invoke(New BNET_Error_AccountUnknownErrorDelegate(AddressOf BNET_Error_AccountUnknownError))
    End Sub

    Private Sub BOT_BNET_Error_AccountUpgrade() Handles BOT.BNET_Error_AccountUpgrade
        Me.Invoke(New BNET_Error_AccountUpgradeDelegate(AddressOf BNET_Error_AccountUpgrade))
    End Sub

    Private Sub BOT_BNET_Error_CdkeyBanned() Handles BOT.BNET_Error_CdkeyBanned
        Me.Invoke(New BNET_Error_CdkeyBannedDelegate(AddressOf BNET_Error_CdkeyBanned))
    End Sub

    Private Sub BOT_BNET_Error_ChannelDoesNotExist(ByVal Text As String) Handles BOT.BNET_Error_ChannelDoesNotExist
        Me.Invoke(New BNET_Error_ChannelDoesNotExistDelegate(AddressOf BNET_Error_ChannelDoesNotExist), Text)
    End Sub

    Private Sub BOT_BNET_Error_ChannelFull(ByVal Text As String) Handles BOT.BNET_Error_ChannelFull
        Me.Invoke(New BNET_Error_ChannelFullDelegate(AddressOf BNET_Error_ChannelFull), Text)
    End Sub

    Private Sub BOT_BNET_Error_ChannelRestricted(ByVal Text As String) Handles BOT.BNET_Error_ChannelRestricted
        Me.Invoke(New BNET_Error_ChannelRestrictedDelegate(AddressOf BNET_Error_ChannelRestricted), Text)
    End Sub

    Private Sub BOT_BNET_Error_ErrorMessage(ByVal Text As String) Handles BOT.BNET_Error_ErrorMessage
        Me.Invoke(New BNET_Error_ErrorMessageDelegate(AddressOf BNET_Error_ErrorMessage), Text)
    End Sub

    Private Sub BOT_BNET_Error_GameVersionDowngrade() Handles BOT.BNET_Error_GameVersionDowngrade
        Me.Invoke(New BNET_Error_GameVersionDowngradeDelegate(AddressOf BNET_Error_GameVersionDowngrade))
    End Sub

    Private Sub BOT_BNET_Error_InvalidCdkey() Handles BOT.BNET_Error_InvalidCdkey
        Me.Invoke(New BNET_Error_InvalidCdkeyDelegate(AddressOf BNET_Error_InvalidCdkey))
    End Sub

    Private Sub BOT_BNET_Error_InvalidGameVersion() Handles BOT.BNET_Error_InvalidGameVersion
        Me.Invoke(New BNET_Error_InvalidGameVersionDelegate(AddressOf BNET_Error_InvalidGameVersion))
    End Sub

    Private Sub BOT_BNET_Error_InvalidPassword() Handles BOT.BNET_Error_InvalidPassword
        Me.Invoke(New BNET_Error_InvalidPasswordDelegate(AddressOf BNET_Error_InvalidPassword))
    End Sub

    Private Sub BOT_BNET_Error_KeyInUse(ByVal UserName As String) Handles BOT.BNET_Error_KeyInUse
        Me.Invoke(New BNET_Error_KeyInUseDelegate(AddressOf BNET_Error_KeyInUse), UserName)
    End Sub

    Private Sub BOT_BNET_Error_OldGameVersion() Handles BOT.BNET_Error_OldGameVersion
        Me.Invoke(New BNET_Error_OldGameVersionDelegate(AddressOf BNET_Error_OldGameVersion))
    End Sub

    Private Sub BOT_BNET_Error_RegisterEmail() Handles BOT.BNET_Error_RegisterEmail
        Me.Invoke(New BNET_Error_RegisterEmailDelegate(AddressOf BNET_Error_RegisterEmail))
    End Sub

    Private Sub BOT_BNET_Error_ServerClosedConnection() Handles BOT.BNET_Error_ServerClosedConnection
        Me.Invoke(New BNET_Error_ServerClosedConnectionDelegate(AddressOf BNET_Error_ServerClosedConnection))
    End Sub

    Private Sub BOT_BNET_Error_VersionCheckFailed() Handles BOT.BNET_Error_VersionCheckFailed
        Me.Invoke(New BNET_Error_VersionCheckFailedDelegate(AddressOf BOT_BNET_Error_VersionCheckFailed))
    End Sub

    Private Sub BOT_BNET_Error_WrongProductInfo() Handles BOT.BNET_Error_WrongProductInfo
        Me.Invoke(New BNET_Error_WrongProductInfoDelegate(AddressOf BNET_Error_WrongProductInfo))
    End Sub

    Private Sub BOT_BNET_Exception(ByVal ex As System.Exception) Handles BOT.BNET_Exception
        Me.Invoke(New BNET_ExceptionDelegate(AddressOf BNET_Exception), ex)
    End Sub

    Private Sub BOT_BNET_FriendsAdd(ByVal account As String, ByVal status As Byte, ByVal locStatus As Byte, ByVal ProductID As Long, ByVal Location As String) Handles BOT.BNET_FriendsAdd
        Me.Invoke(New BNET_FriendsAddDelegate(AddressOf BNET_FriendsAdd), account, status, locStatus, ProductID, Location)
    End Sub

    Private Sub BOT_BNET_FriendsList(ByVal account As String, ByVal status As Byte, ByVal locStatus As Byte, ByVal ProductID As Long, ByVal Location As String) Handles BOT.BNET_FriendsList
        Me.Invoke(New BNET_FriendsListDelegate(AddressOf BNET_FriendsList), account, status, locStatus, ProductID, Location)
    End Sub

    Private Sub BOT_BNET_FriendsPosition(ByVal oldIndex As Byte, ByVal newIndex As Byte) Handles BOT.BNET_FriendsPosition
        Me.Invoke(New BNET_FriendsPositionDelegate(AddressOf BNET_FriendsPosition), oldIndex, newIndex)
    End Sub

    Private Sub BOT_BNET_FriendsRemove(ByVal index As Byte) Handles BOT.BNET_FriendsRemove
        Me.Invoke(New BNET_FriendsRemoveDelegate(AddressOf BNET_FriendsRemove), index)
    End Sub

    Private Sub BOT_BNET_FriendsUpdate(ByVal index As Byte, ByVal status As Byte, ByVal locStatus As Byte, ByVal ProductID As Long, ByVal Location As String) Handles BOT.BNET_FriendsUpdate
        Me.Invoke(New BNET_FriendsUpdateDelegate(AddressOf BNET_FriendsUpdate), index, status, locStatus, ProductID, Location)
    End Sub

    Private Sub BOT_BNET_GetIconData(ByVal filetime As ULong, ByVal filename As String, ByVal client As String, ByVal cdkey As String, ByVal server As String) Handles BOT.BNET_GetIconData
        Me.Invoke(New BNET_GetIconDataDelegate(AddressOf BNET_GetIconData), filetime, filename, client, cdkey, server)
    End Sub

    Private Sub BOT_BNET_Info_Broadcast(ByVal Text As String) Handles BOT.BNET_Info_Broadcast
        Me.Invoke(New BNET_Info_BroadcastDelegate(AddressOf BNET_Info_Broadcast), Text)
    End Sub

    Private Sub BOT_BNET_Info_Channel(ByVal Text As String, ByVal Flags As Long) Handles BOT.BNET_Info_Channel
        Me.Invoke(New BNET_Info_ChannelDelegate(AddressOf BNET_Info_Channel), Text, Flags)
    End Sub

    Private Sub BOT_BNET_Info_Information(ByVal Text As String) Handles BOT.BNET_Info_Information
        Me.Invoke(New BNET_Info_InformationDelegate(AddressOf BNET_Info_Information), Text)
    End Sub

    Private Sub BOT_BNET_PacketReceived(ByVal pID As Byte) Handles BOT.BNET_PacketReceived
        Me.Invoke(New BNET_PacketReceivedDelegate(AddressOf BNET_PacketReceived), pID)
    End Sub

    Private Sub BOT_BNET_PacketSent(ByVal Packet As SentBnetData) Handles BOT.BNET_PacketSent
        Me.Invoke(New BNET_PacketSentDelegate(AddressOf BNET_PacketSent), Packet)
    End Sub

    Private Sub BOT_BNET_ProfileInfo(ByVal ID As String, ByVal PInfo As ProfileInfo) Handles BOT.BNET_ProfileInfo
        If Me.ProfilePanes.Contains(ID) Then
            Dim name As String = Me.ProfilePanes.Item(ID)
            Me.ProfilePanes.Remove(ID)

            WindowsFormsSynchronizationContext.AutoInstall = False

            Dim pcfg As ProfileConfig = Me.pConf
            pcfg.p_Title = pcfg.p_Title.Replace("($arg0)", name)

            If name = Me._name Then
                Dim prof As New MyProfile
                prof.txtLoc.Text = PInfo.LOCATION
                prof.txtDescr.Text = PInfo.DESCRIPTION
                prof.txtW.Text = PInfo.WINS
                prof.txtL.Text = PInfo.LOSSES
                prof.txtD.Text = PInfo.DISCONNECTS
                prof.txtWL.Text = PInfo.WINSLADDER
                prof.txtLL.Text = PInfo.LOSSESLADDER
                prof.txtDL.Text = PInfo.DISCONNECTSLADDER
                prof.txtR.Text = PInfo.RATINGLADDER
                prof.BOT = Me.bot
                prof.SetText(pcfg)
                Application.Run(prof)
            Else
                Dim prof As New UserProfile
                prof.txtLoc.Text = PInfo.LOCATION
                prof.txtDescr.Text = PInfo.DESCRIPTION
                prof.txtW.Text = PInfo.WINS
                prof.txtL.Text = PInfo.LOSSES
                prof.txtD.Text = PInfo.DISCONNECTS
                prof.txtWL.Text = PInfo.WINSLADDER
                prof.txtLL.Text = PInfo.LOSSESLADDER
                prof.txtDL.Text = PInfo.DISCONNECTSLADDER
                prof.txtR.Text = PInfo.RATINGLADDER
                prof.SetText(pcfg)
                Application.Run(prof)
            End If
        End If
    End Sub

    Private Sub BOT_BNET_SendChatMessage(ByVal TrueName As String, ByVal Message As String) Handles BOT.BNET_SendChatMessage
        Me.Invoke(New BNET_SendChatMessageDelegate(AddressOf BNET_SendChatMessage), TrueName, Message)
    End Sub

    Private Sub BOT_BNET_SendingLoginRequest() Handles BOT.BNET_SendingLoginRequest
        Me.Invoke(New BNET_SendingLoginRequestDelegate(AddressOf BNET_SendingLoginRequest))
    End Sub

    Private Sub BOT_BNET_ShowUser(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String) Handles BOT.BNET_ShowUser
        Me.Invoke(New BNET_ShowUserDelegate(AddressOf BNET_ShowUser), Flags, Ping, Username, Text)
    End Sub

    Private Sub BOT_BNET_SocketError(ByVal errCode As String, ByVal errMessage As String) Handles BOT.BNET_SocketError
        Me.Invoke(New BNET_SocketErrorDelegate(AddressOf BNET_SocketError), errCode, errMessage)
    End Sub

    Private Sub BOT_BNET_UnhandledPacket(ByVal pID As Byte, ByVal Data As String) Handles BOT.BNET_UnhandledPacket
        Me.Invoke(New BNET_UnhandledPacketDelegate(AddressOf BNET_UnhandledPacket), pID, Data)
    End Sub

    Private Sub BOT_BNET_UserEmote(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String) Handles BOT.BNET_UserEmote
        Me.Invoke(New BNET_UserEmoteDelegate(AddressOf BNET_UserEmote), Flags, Ping, Username, Text)
    End Sub

    Private Sub BOT_BNET_UserFlagsUpdate(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String) Handles BOT.BNET_UserFlagsUpdate
        Me.Invoke(New BNET_UserFlagsUpdateDelegate(AddressOf BNET_UserFlagsUpdate), Flags, Ping, Username, Text)
    End Sub

    Private Sub BOT_BNET_UserJoined(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String) Handles BOT.BNET_UserJoined
        Me.Invoke(New BNET_UserJoinedDelegate(AddressOf BNET_UserJoined), Flags, Ping, Username, Text)
    End Sub

    Private Sub BOT_BNET_UserLeft(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String) Handles BOT.BNET_UserLeft
        Me.Invoke(New BNET_UserLeftDelegate(AddressOf BNET_UserLeft), Flags, Ping, Username, Text)
    End Sub

    Private Sub BOT_BNET_UserTalk(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String) Handles BOT.BNET_UserTalk
        Me.Invoke(New BNET_UserTalkDelegate(AddressOf BNET_UserTalk), Flags, Ping, Username, Text)
    End Sub

    Private Sub BOT_BNET_UserWhisperRecieved(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String) Handles BOT.BNET_UserWhisperRecieved
        Me.Invoke(New BNET_UserWhisperRecievedDelegate(AddressOf BNET_UserWhisperRecieved), Flags, Ping, Username, Text)
    End Sub

    Private Sub BOT_BNET_UserWhisperSent(ByVal Flags As Integer, ByVal Ping As Integer, ByVal Username As String, ByVal Text As String) Handles BOT.BNET_UserWhisperSent
        Me.Invoke(New BNET_UserWhisperSentDelegate(AddressOf BNET_UserWhisperSent), Flags, Ping, Username, Text)
    End Sub

    Private Sub BOT_BNET_UsingHashFile(ByVal HashPath As String) Handles BOT.BNET_UsingHashFile
        Me.Invoke(New BNET_UsingHashFileDelegate(AddressOf BNET_UsingHashFile), HashPath)
    End Sub

    Private Sub BOT_BNET_UsingLockdown() Handles BOT.BNET_UsingLockdown
        Me.Invoke(New BNET_UsingLockdownDelegate(AddressOf BNET_UsingLockdown))
    End Sub

    Private Sub BOT_BNET_VerifyingGameVersion() Handles BOT.BNET_VerifyingGameVersion
        Me.Invoke(New BNET_VerifyingGameVersionDelegate(AddressOf BNET_VerifyingGameVersion))
    End Sub

    Private Sub BOT_BNET_VersionCheckPassed() Handles BOT.BNET_VersionCheckPassed
        Me.Invoke(New BNET_VersionCheckPassedDelegate(AddressOf BNET_VersionCheckPassed))
    End Sub

    Private Sub BOT_BNET_WAR3ServerInvalid() Handles BOT.BNET_WAR3ServerInvalid
        Me.Invoke(New BNET_WAR3ServerInvalidDelegate(AddressOf BNET_WAR3ServerInvalid))
    End Sub
#End Region

    Private Sub BNFTP_DownloadingFile(ByVal filename As String) Handles BNFTP.DownloadingFile
        TextEvents.Item("bnftpdownloadstarted").AddText(Me.rtbChat, filename)
    End Sub

    Private Sub BNFTP_FileDownloadComplete(ByVal filename As String, ByVal filePath As String, Optional ByVal Suppress As Boolean = False) Handles BNFTP.FileDownloadComplete
        BNFTP = Nothing

        If Not Suppress Then TextEvents.Item("bnftpdownloadfinished").AddText(Me.rtbChat, filename, filePath)
        Dim fullPath As String = filePath & "\" & filename
        IconList = New List(Of BnetIcon)
        BniIcons = New IconsProcessor(fullPath)
        Try
            IconList = BniIcons.ReadIcons()
            BniIcons = Nothing

            Me.ilIcons.Images.Clear()
            Dim i As Integer

            For Each bni As BnetIcon In IconList
                Me.ilIcons.Images.Add(bni.p_Image)
                bni.ImageIndex = i
                i += 1
            Next
            Call Me.ReprocessChannelList()
            TextEvents.Item("bnicomplete").AddText(Me.rtbChat, Nothing)
            BOT.AllowEnterChat()
        Catch ex As FormatException
            Me.Invoke(New BNET_Error_ErrorMessageDelegate(AddressOf BNET_Error_ErrorMessage), ex.Message)
        End Try
    End Sub

    Private Sub cmbTextEntry_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbTextEntry.KeyUp
        If e.KeyCode = Keys.Enter Then
            If BOT Is Nothing Then Exit Sub
            If Not BOT.IsConnected Then Exit Sub

            cmbTextEntry.Items.Insert(0, cmbTextEntry.Text)
            BOT.SendChat(cmbTextEntry.Text)
            cmbTextEntry.Text = ""
            Me._lastTab = ""
        ElseIf e.KeyCode = Keys.Tab Then
            If _triedTabs Is Nothing Then _triedTabs = New List(Of String)

            With cmbTextEntry
                ' select the current word being typed by going from the end of the text
                ' to either the first space or the start of the text
                Dim i As Integer
                For i = .Text.Length() - 1 To 0 Step -1
                    If .Text.Chars(i) = " " Then
                        i += 1
                        Exit For
                    End If
                Next i
                If i < 0 Then i = 0
                .Select(i, .Text.Length())

                ' if we dont already have a _lastTab then set it
                ' last tab is the control value to check against
                If _lastTab = "" Then _lastTab = .SelectedText

                ' go through the user list and check items
                ' TODO: make it go through whichever list is currently focused (channel, friends, botnet, clan)
                ' TODO: add clan list
                For i = 0 To lvChannel.Items.Count() - 1
                    Dim check As String = lvChannel.Items(i).SubItems(1).Text

                    ' if it IS the selected test, we dont care since they already have it typed out
                    ' if what they are typing is longer than the name being checked, it obviously can't
                    ' complete to the shorter value
                    If check = .SelectedText Then Continue For
                    If _lastTab.Length() > check.Length() Then Continue For

                    ' case insensitive check
                    If check.Substring(0, _lastTab.Length()).ToLower() = _lastTab.ToLower() Then
                        ' if we've already seen it, don't bother
                        ' if we haven't, set the selected text and add the new value to the "we've already seen it so don't bother" list
                        If Not _triedTabs.Contains(check) Then
                            .SelectedText = check
                            _triedTabs.Add(check)
                            Exit Sub
                        End If
                    End If
                Next i

                ' if we go through the entire list and dont find a match, then screw it
                _triedTabs.Clear()
                .SelectedText = _lastTab
            End With
        Else
            ' clears the last tab because user began typing again
            _lastTab = ""
        End If
    End Sub

    Private Sub BniIcons_ConstructingIcons() Handles BniIcons.ConstructingIcons
        Me.Invoke(New ConstructingIconsDelegate(AddressOf ConstructingIcons))
    End Sub

    Private Sub BniIcons_ExtractingIcons() Handles BniIcons.ExtractingIcons
        Me.Invoke(New ExtractingIconsDelegate(AddressOf ExtractingIcons))
    End Sub

    Private Sub BniIcons_IconCount(ByVal num As Integer) Handles BniIcons.IconCount
        Me.Invoke(New IconCountDelegate(AddressOf IconCount), num)
    End Sub

    Private Sub MColor_LoadTextEventsFailed() Handles MColor.LoadTextEventsFailed
        AddC(Color.Red, "ERROR: Could not load text events")
        EnableConnect = False
    End Sub

    Private Sub MColor_LoadTextEventsSucceeded(ByVal Events As Microsoft.VisualBasic.Collection) Handles MColor.LoadTextEventsSucceeded
        Me.TextEvents = Events
        EnableConnect = True
    End Sub

    Private Sub tabLists_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabLists.SelectedIndexChanged
        Call Me.ResizeInstance()
    End Sub

    Private Sub lvChannel_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvChannel.MouseDoubleClick
        If lvChannel.SelectedItems.Count = 0 Then Exit Sub

        Dim str As String = lvChannel.SelectedItems(0).SubItems(1).Text() & ": "
        str &= lvChannel.SelectedItems(0).SubItems(0).Text() & ", "
        str &= lvChannel.SelectedItems(0).SubItems(2).Text() & ", "
        str &= lvChannel.SelectedItems(0).SubItems(3).Text() & ", "
        str &= lvChannel.SelectedItems(0).SubItems(4).Text()
        Debug.Print(str)
    End Sub

    Private Sub IgnoreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IgnoreToolStripMenuItem.Click
        If BOT Is Nothing Then Exit Sub
        If BOT.IsConnected = False Then Exit Sub
        If lvChannel.Items.Count() = 0 Then Exit Sub
        If lvChannel.SelectedItems.Count() = 0 Then Exit Sub

        BOT.SendChat("/ignore " & lvChannel.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub UnignoreToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnignoreToolStripMenuItem.Click
        If BOT Is Nothing Then Exit Sub
        If BOT.IsConnected = False Then Exit Sub
        If lvChannel.Items.Count() = 0 Then Exit Sub
        If lvChannel.SelectedItems.Count() = 0 Then Exit Sub

        BOT.SendChat("/unignore " & lvChannel.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub KickToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KickToolStripMenuItem.Click
        If BOT Is Nothing Then Exit Sub
        If BOT.IsConnected = False Then Exit Sub
        If lvChannel.Items.Count() = 0 Then Exit Sub
        If lvChannel.SelectedItems.Count() = 0 Then Exit Sub

        BOT.SendChat("/kick " & lvChannel.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub BanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BanToolStripMenuItem.Click
        If BOT Is Nothing Then Exit Sub
        If BOT.IsConnected = False Then Exit Sub
        If lvChannel.Items.Count() = 0 Then Exit Sub
        If lvChannel.SelectedItems.Count() = 0 Then Exit Sub

        BOT.SendChat("/ban " & lvChannel.SelectedItems(0).SubItems(1).Text)
    End Sub

    Private Sub ProfileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProfileToolStripMenuItem.Click
        If BOT Is Nothing Then Exit Sub
        If BOT.IsConnected = False Then Exit Sub
        If lvChannel.Items.Count() = 0 Then Exit Sub
        If lvChannel.SelectedItems.Count() = 0 Then Exit Sub

        Dim val As String = BOT.RequestProfile(lvChannel.SelectedItems(0).SubItems(1).Text)
        If val = "-1" Then Exit Sub

        Me.ProfilePanes.Add(lvChannel.SelectedItems(0).SubItems(1).Text, val)
    End Sub

    Private Sub cmbTextEntry_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTextEntry.SelectedIndexChanged

    End Sub
End Class

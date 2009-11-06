Imports System.Xml

Public Class MasterColor
    Private mc_ChatWindow As InterfaceItem
    Private mc_ChannelLabel As InterfaceItem
    Private mc_ChannelUserList As InterfaceItem
    Private mc_SendChatBox As InterfaceItem
    Private mc_BotnetLabel As InterfaceItem
    Private mc_BotnetUserList As InterfaceItem
    Private mc_FriendsList As InterfaceItem

    Private mc_Blizzard As ColorItem
    Private mc_SysOp As ColorItem
    Private mc_ChanOp As ColorItem
    Private mc_Moderator As ColorItem
    Private mc_Me As ColorItem
    Private m_Events As Collection

    Public Event LoadTextEventsFailed()
    Public Event LoadTextEventsSucceeded(ByVal Events As Collection)

    Public Sub LoadColors(ByVal Filename As String)
        Dim m_xmld As XmlDocument
        Dim Events As New Collection

        Filename = Application.StartupPath & "\Profiles\" & Filename & "\colors.xml"

        m_xmld = New XmlDocument()
        m_xmld.Load(Filename)

        mc_ChatWindow = Me.SetInterfaceItem(m_xmld, "/colors/interface/chatwindow", "chatwindow")
        mc_ChannelLabel = Me.SetInterfaceItem(m_xmld, "/colors/interface/channellabel", "channellabel")
        mc_ChannelUserList = Me.SetInterfaceItem(m_xmld, "/colors/interface/channeluserlist", "channeluserlist")
        mc_SendChatBox = Me.SetInterfaceItem(m_xmld, "/colors/interface/sendchatbox", "sendchatbox")
        mc_BotnetLabel = Me.SetInterfaceItem(m_xmld, "/colors/interface/botnetlabel", "botnetlabel")
        mc_BotnetUserList = Me.SetInterfaceItem(m_xmld, "/colors/interface/botnetuserlist", "botnetuserlist")
        mc_FriendsList = Me.SetInterfaceItem(m_xmld, "/colors/interface/friendslist", "friendslist")

        mc_Blizzard = GetUserFlagColor(m_xmld, "/colors/interface/userblizzard")
        mc_SysOp = GetUserFlagColor(m_xmld, "/colors/interface/usersysop")
        mc_ChanOp = GetUserFlagColor(m_xmld, "/colors/interface/userchanop")
        mc_Moderator = GetUserFlagColor(m_xmld, "/colors/interface/usermoderator")
        mc_Me = GetUserFlagColor(m_xmld, "/colors/interface/userme")

        Try
            For Each str As String In BnetTextEventItems
                Dim ci As ChatItem = GetTextItem(m_xmld, "bnet/" & str)
                Events.Add(ci, "bnet" & str)
                If ci Is Nothing Then Debug.Print(str & " > nothing")
            Next

            For Each str As String In BniTextEventItems
                Dim ci As ChatItem = GetTextItem(m_xmld, "bni/" & str)
                Events.Add(ci, "bni" & str)
                If ci Is Nothing Then Debug.Print(str & " > nothing")
            Next

            For Each str As String In BnftpTextEventItems
                Dim ci As ChatItem = GetTextItem(m_xmld, "bnftp/" & str)
                Events.Add(ci, "bnftp" & str)
                If ci Is Nothing Then Debug.Print(str & " > nothing")
            Next

            For Each str As String In FriendsTextEventParts
                Dim ci As ChatItem = GetTextItem(m_xmld, "parts/friends/" & str)
                Events.Add(ci, "friends" & str)
                If ci Is Nothing Then Debug.Print(str & " > nothing")
            Next
            m_Events = Events

            RaiseEvent LoadTextEventsSucceeded(Events)
        Catch ex As Exception
            RaiseEvent LoadTextEventsFailed()
        End Try
    End Sub

    Private Function SetInterfaceItem(ByRef xmlDoc As XmlDocument, ByVal path As String, ByVal name As String) As InterfaceItem
        Dim ii As InterfaceItem = Nothing
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        Dim fname As String
        Dim fsize As Double

        m_nodelist = xmlDoc.SelectNodes(path)
        For Each m_node In m_nodelist
            fname = m_node.Attributes.GetNamedItem("font").Value
            fsize = Double.Parse(m_node.Attributes.GetNamedItem("size").Value)
            ii = New InterfaceItem(name, New Font(fname, fsize), Nothing, Nothing)
            ii.FORECOLOR = ParseColorParam(m_node.Attributes.GetNamedItem("forecolor").Value)
            ii.BACKCOLOR = ParseColorParam(m_node.Attributes.GetNamedItem("backcolor").Value)
            ii.NAME = name
        Next

        Return ii
    End Function

    Private Function GetUserFlagColor(ByRef xmlDoc As XmlDocument, ByVal path As String) As ColorItem
        Dim ret As ColorItem = ColorList(135)
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        m_nodelist = xmlDoc.SelectNodes(path)
        For Each m_node In m_nodelist
            ret = ParseColorParam(m_node.Attributes.GetNamedItem("color").Value)
        Next

        Return ret
    End Function

    Private Function GetTextItem(ByRef xmlDoc As XmlDocument, ByVal name As String) As ChatItem
        Dim ret As ChatItem = Nothing
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        m_nodelist = xmlDoc.SelectNodes("/colors/text/" & name)
        For Each m_node In m_nodelist
            ret = New ChatItem(m_node.InnerText, name)
        Next

        Return ret
    End Function

    Public Sub SaveColors(ByVal Filename As String, ByVal tEvents As Collection)
        Dim settings As New XmlWriterSettings()
        settings.Indent = True
        settings.NewLineOnAttributes = True

        Filename = Application.StartupPath & "\Profiles\" & Filename & "\colors.xml"

        Using writer As XmlWriter = XmlWriter.Create(Filename, settings)
            writer.WriteStartDocument()
            writer.WriteStartElement("colors")

            writer.WriteStartElement("interface")

            writer.WriteStartElement("chatwindow")
            writer.WriteAttributeString("font", mc_ChatWindow.FONT.Name)
            writer.WriteAttributeString("size", mc_ChatWindow.FONT.Size)
            writer.WriteAttributeString("forecolor", mc_ChatWindow.FORECOLOR.PARAM)
            writer.WriteAttributeString("backcolor", mc_ChatWindow.BACKCOLOR.PARAM)
            writer.WriteEndElement()

            writer.WriteStartElement("sendchatbox")
            writer.WriteAttributeString("font", mc_SendChatBox.FONT.Name)
            writer.WriteAttributeString("size", mc_SendChatBox.FONT.Size)
            writer.WriteAttributeString("forecolor", mc_SendChatBox.FORECOLOR.PARAM)
            writer.WriteAttributeString("backcolor", mc_SendChatBox.BACKCOLOR.PARAM)
            writer.WriteEndElement()

            writer.WriteStartElement("channellabel")
            writer.WriteAttributeString("font", mc_ChannelLabel.FONT.Name)
            writer.WriteAttributeString("size", mc_ChannelLabel.FONT.Size)
            writer.WriteAttributeString("forecolor", mc_ChannelLabel.FORECOLOR.PARAM)
            writer.WriteAttributeString("backcolor", mc_ChannelLabel.BACKCOLOR.PARAM)
            writer.WriteEndElement()

            writer.WriteStartElement("channeluserlist")
            writer.WriteAttributeString("font", mc_ChannelUserList.FONT.Name)
            writer.WriteAttributeString("size", mc_ChannelUserList.FONT.Size)
            writer.WriteAttributeString("forecolor", mc_ChannelUserList.FORECOLOR.PARAM)
            writer.WriteAttributeString("backcolor", mc_ChannelUserList.BACKCOLOR.PARAM)
            writer.WriteEndElement()

            writer.WriteStartElement("userblizzard")
            writer.WriteAttributeString("color", mc_Blizzard.TEXT)
            writer.WriteEndElement()

            writer.WriteStartElement("usersysop")
            writer.WriteAttributeString("color", mc_Blizzard.TEXT)
            writer.WriteEndElement()

            writer.WriteStartElement("userchanop")
            writer.WriteAttributeString("color", mc_Blizzard.TEXT)
            writer.WriteEndElement()

            writer.WriteStartElement("usermoderator")
            writer.WriteAttributeString("color", mc_Blizzard.TEXT)
            writer.WriteEndElement()

            writer.WriteStartElement("userme")
            writer.WriteAttributeString("color", mc_Blizzard.TEXT)
            writer.WriteEndElement()

            writer.WriteEndElement()

            writer.WriteStartElement("text")

            writer.WriteStartElement("bnet")
            For Each item As ChatItem In tEvents
                Dim name As String = item.NAME
                If name.StartsWith("bnet") Then
                    name = item.NAME.Substring(4)
                    writer.WriteStartElement(name)
                    writer.WriteString(item.ToString())
                    writer.WriteEndElement()
                End If
            Next item
            writer.WriteEndElement()

            writer.WriteStartElement("botnet")
            For Each item As ChatItem In tEvents
                Dim name As String = item.NAME
                If name.StartsWith("botnet") Then
                    name = item.NAME.Substring(6)
                    writer.WriteStartElement(name)
                    writer.WriteString(item.ToString())
                    writer.WriteEndElement()
                End If
            Next item
            writer.WriteEndElement()

            writer.WriteStartElement("bni")
            For Each item As ChatItem In tEvents
                Dim name As String = item.NAME
                If name.StartsWith("bni") Then
                    name = item.NAME.Substring(3)
                    writer.WriteStartElement(name)
                    writer.WriteString(item.ToString())
                    writer.WriteEndElement()
                End If
            Next item
            writer.WriteEndElement()

            writer.WriteStartElement("bnftp")
            For Each item As ChatItem In tEvents
                Dim name As String = item.NAME
                If name.StartsWith("bnftp") Then
                    name = item.NAME.Substring(5)
                    writer.WriteStartElement(name)
                    writer.WriteString(item.ToString())
                    writer.WriteEndElement()
                End If
            Next item
            writer.WriteEndElement()

            writer.WriteStartElement("irc")
            For Each item As ChatItem In tEvents
                Dim name As String = item.NAME
                If name.StartsWith("irc") Then
                    name = item.NAME.Substring(3)
                    writer.WriteStartElement(name)
                    writer.WriteString(item.ToString())
                    writer.WriteEndElement()
                End If
            Next item
            writer.WriteEndElement()

            writer.WriteStartElement("parts")
            writer.WriteStartElement("friends")
            For Each item As ChatItem In tEvents
                Dim name As String = item.NAME
                If name.StartsWith("friends") Then
                    name = item.NAME.Substring(7)
                    writer.WriteStartElement(name)
                    writer.WriteString(item.ToString())
                    writer.WriteEndElement()
                End If
            Next item
            writer.WriteEndElement()
            writer.WriteEndElement()

            writer.WriteEndElement()


            writer.WriteEndElement()
            writer.Flush()
        End Using
    End Sub

    Public Sub SetChatWindow(ByVal iFont As System.Drawing.Font, ByVal cForeColor As ColorItem, ByVal cBackColor As ColorItem)
        mc_ChatWindow = New InterfaceItem("chatwindow", iFont, cForeColor, cBackColor)
    End Sub

    Public Sub SetChannelLabel(ByVal iFont As System.Drawing.Font, ByVal cForeColor As ColorItem, ByVal cBackColor As ColorItem)
        mc_ChannelLabel = New InterfaceItem("channellabel", iFont, cForeColor, cBackColor)
    End Sub

    Public Sub SetChannelUserList(ByVal iFont As System.Drawing.Font, ByVal cForeColor As ColorItem, ByVal cBackColor As ColorItem)
        mc_ChannelUserList = New InterfaceItem("channeluserlist", iFont, cForeColor, cBackColor)
    End Sub

    Public Sub SetSendChatBox(ByVal iFont As System.Drawing.Font, ByVal cForeColor As ColorItem, ByVal cBackColor As ColorItem)
        mc_SendChatBox = New InterfaceItem("sendchatbox", iFont, cForeColor, cBackColor)
    End Sub

    Public ReadOnly Property GetChatWindow() As InterfaceItem
        Get
            Return mc_ChatWindow
        End Get
    End Property

    Public ReadOnly Property GetChannelLabel() As InterfaceItem
        Get
            Return mc_ChannelLabel
        End Get
    End Property

    Public ReadOnly Property GetChannelUserList() As InterfaceItem
        Get
            Return mc_ChannelUserList
        End Get
    End Property

    Public ReadOnly Property GetFriendsList() As InterfaceItem
        Get
            Return mc_FriendsList
        End Get
    End Property

    Public ReadOnly Property GetBotnetLabel() As InterfaceItem
        Get
            Return mc_BotnetLabel
        End Get
    End Property

    Public ReadOnly Property GetBotnetUserList() As InterfaceItem
        Get
            Return mc_BotnetUserList
        End Get
    End Property

    Public ReadOnly Property GetSendChatBox() As InterfaceItem
        Get
            Return mc_SendChatBox
        End Get
    End Property

    Public ReadOnly Property GetColorBlizzard() As ColorItem
        Get
            Return mc_Blizzard
        End Get
    End Property

    Public ReadOnly Property GetColorSysop() As ColorItem
        Get
            Return mc_SysOp
        End Get
    End Property

    Public ReadOnly Property GetColorChanOp() As ColorItem
        Get
            Return mc_ChanOp
        End Get
    End Property

    Public ReadOnly Property GetColorModerator() As ColorItem
        Get
            Return mc_Moderator
        End Get
    End Property

    Public ReadOnly Property GetColorMe() As ColorItem
        Get
            Return mc_Me
        End Get
    End Property
End Class

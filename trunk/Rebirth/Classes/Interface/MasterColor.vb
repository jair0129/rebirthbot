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

Imports System.Xml

''' <summary>
''' Master interface and textevent color settings
''' </summary>
''' <remarks>Contains separate routines for text events and interface settings</remarks>
Public Class MasterColor
    Private mc_ChatWindow As InterfaceItem
    Private mc_ChannelLabel As InterfaceItem
    Private mc_ChannelUserList As InterfaceItem
    Private mc_SendChatBox As InterfaceItem
    Private mc_BotnetLabel As InterfaceItem
    Private mc_BotnetUserList As InterfaceItem
    Private mc_FriendsList As InterfaceItem
    Private mc_ClanList As InterfaceItem

    Private mc_Blizzard As ColorItem
    Private mc_SysOp As ColorItem
    Private mc_ChanOp As ColorItem
    Private mc_Moderator As ColorItem
    Private mc_Me As ColorItem
    Private m_Events As Collection

    Public Event LoadTextEventsFailed()
    Public Event LoadTextEventsSucceeded(ByVal Events As Collection)

    ''' <summary>
    ''' Load colors for interface objects
    ''' </summary>
    ''' <param name="Filename">Actually the Profilename to load</param>
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
        mc_ClanList = Me.SetInterfaceItem(m_xmld, "/colors/interface/clanlist", "clanlist")

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

            For Each str As String In ChannelTypeParts
                Dim ci As ChatItem = GetTextItem(m_xmld, "parts/channel/" & str)
                Events.Add(ci, "channel" & str)
                If ci Is Nothing Then Debug.Print(str & " > nothing")
            Next

            m_Events = Events

            RaiseEvent LoadTextEventsSucceeded(Events)
        Catch ex As Exception
            RaiseEvent LoadTextEventsFailed()
        End Try
    End Sub

    ''' <summary>
    ''' Retrieve a value from the XmlDocument, transcribe it to an InterfaceItem, and return it
    ''' </summary>
    ''' <param name="xmlDoc">Document to get value from</param>
    ''' <param name="path">Path to the value</param>
    ''' <param name="name">Name of the InterfaceItem</param>
    ''' <returns>InterfaceItem created from the value obtained from the XmlDocument</returns>
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

    ''' <summary>
    ''' Obtain the color associated with a given server flags type
    ''' </summary>
    ''' <param name="xmlDoc"></param>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
            If name.IndexOf("parts/") >= 0 Then
                ret = New ChatItem(m_node.InnerText, name, True)
            Else
                ret = New ChatItem(m_node.InnerText, name)
            End If
        Next

        Return ret
    End Function

    ''' <summary>
    ''' Save all the fancy colors
    ''' </summary>
    ''' <param name="Filename">Actually the Profilename</param>
    ''' <param name="tEvents">The TextEvents object</param>
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

    '' Below is quite possibly the crappiest way I could have done what I did

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

    Public Sub SetFriendsList(ByVal iFont As System.Drawing.Font, ByVal cForeColor As ColorItem, ByVal cBackColor As ColorItem)
        mc_FriendsList = New InterfaceItem("friendslist", iFont, cForeColor, cBackColor)
    End Sub

    Public Sub SetBotnetLabel(ByVal iFont As System.Drawing.Font, ByVal cForeColor As ColorItem, ByVal cBackColor As ColorItem)
        mc_BotnetLabel = New InterfaceItem("botnetlabel", iFont, cForeColor, cBackColor)
    End Sub

    Public Sub SetBotnetUserList(ByVal iFont As System.Drawing.Font, ByVal cForeColor As ColorItem, ByVal cBackColor As ColorItem)
        mc_BotnetUserList = New InterfaceItem("botnetuserlist", iFont, cForeColor, cBackColor)
    End Sub

    Public Sub SetClanUserList(ByVal iFont As System.Drawing.Font, ByVal cForeColor As ColorItem, ByVal cBackColor As ColorItem)
        mc_ClanList = New InterfaceItem("clanlist", iFont, cForeColor, cBackColor)
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

    Public ReadOnly Property GetClanList() As InterfaceItem
        Get
            Return mc_ClanList
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

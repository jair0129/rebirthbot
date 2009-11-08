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
Imports System.IO

''' <summary>
''' Localization master control.
''' </summary>
''' <remarks>The bulk of this class is the loading routine.  There might
''' also be some accessors or something.  There are no mutators or save
''' at this point because I have ambitious plans for expansion later on.</remarks>
Public Class Localization

    Private m_menu_LoadBot As String
    Private m_menu_UnloadBot As String
    Private m_menu_Connection As String
    Private m_menu_BnetReconnect As String
    Private m_menu_BnetDisconnect As String
    Private m_menu_BotnetReconnect As String
    Private m_menu_BotnetDisconnect As String

    Private m_tab_Channel As String
    Private m_tab_Friends As String
    Private m_tab_Botnet As String
    Private m_tab_Clan As String

    Private m_loadMenu As Boolean
    Private m_loadText As Boolean

    Public m_ProfileConfig As ProfileConfig
    Public m_ConfigConfig As ConfigConfig

    Private m_events As Collection

    Private WithEvents fileGrab As DownloadFile

    Public Event LoadMenuLanguageFailed()
    Public Event LoadMenuLanguageSucceeded()

    ''' <summary>
    ''' Load all of the various text to be displayed
    ''' </summary>
    ''' <param name="Lang">Lang is the language code to be loaded.</param>
    Public Sub LoadMenuLanguage(ByVal Lang As String)
        Dim m_xmld As XmlDocument
        m_loadMenu = False

        Dim filePath As String = Application.StartupPath & "\localization\interface_" & Lang & ".xml"

        If Not File.Exists(filePath) Then
            m_loadMenu = True
            fileGrab = New DownloadFile
            fileGrab.Download("http://rabbitx86.net/rebirth/" & BOT_VERSION & "/localization/interface_" & Lang & ".xml", filePath)
            Exit Sub
        End If

        m_xmld = New XmlDocument()
        m_xmld.Load(filePath)

        m_menu_LoadBot = Me.GetLangItem(m_xmld, "/data/menus/loadbot")
        m_menu_UnloadBot = Me.GetLangItem(m_xmld, "/data/menus/unloadbot")
        m_menu_Connection = Me.GetLangItem(m_xmld, "/data/menus/connection")
        m_menu_BnetReconnect = Me.GetLangItem(m_xmld, "/data/menus/bnetreconnect")
        m_menu_BnetDisconnect = Me.GetLangItem(m_xmld, "/data/menus/bnetdisconnect")
        m_menu_BotnetReconnect = Me.GetLangItem(m_xmld, "/data/menus/botnetreconnect")
        m_menu_BotnetDisconnect = Me.GetLangItem(m_xmld, "/data/menus/botnetdisconnect")

        m_tab_Channel = Me.GetLangItem(m_xmld, "/data/tabs/channel")
        m_tab_Friends = Me.GetLangItem(m_xmld, "/data/tabs/friends")
        m_tab_Botnet = Me.GetLangItem(m_xmld, "/data/tabs/botnet")
        m_tab_Clan = Me.GetLangItem(m_xmld, "/data/tabs/clan")

        Me.m_ConfigConfig = New ConfigConfig
        Me.m_ProfileConfig = New ProfileConfig

        With Me.m_ConfigConfig
            .c_Title = Me.GetLangItem(m_xmld, "/data/config/title")
            .t_Global = Me.GetLangItem(m_xmld, "/data/config/tabs/global")
            .t_Profiles = Me.GetLangItem(m_xmld, "/data/config/tabs/profiles")

            .p_Username = Me.GetLangItem(m_xmld, "/data/config/profile/username")
            .p_Password = Me.GetLangItem(m_xmld, "/data/config/profile/password")
            .p_Server = Me.GetLangItem(m_xmld, "/data/config/profile/server")
            .p_CDKey = Me.GetLangItem(m_xmld, "/data/config/profile/cdkey")
            .p_EXPKey = Me.GetLangItem(m_xmld, "/data/config/profile/expkey")
            .p_Product = Me.GetLangItem(m_xmld, "/data/config/profile/product")
            .p_Home = Me.GetLangItem(m_xmld, "/data/config/profile/home")

            .p_Save = Me.GetLangItem(m_xmld, "/data/config/profile/save")
            .p_Lang = Me.GetLangItem(m_xmld, "/data/config/profile/language")
            .p_Autoload = Me.GetLangItem(m_xmld, "/data/config/profile/autoload")
            .p_Autoconnect = Me.GetLangItem(m_xmld, "/data/config/profile/autoconnect")
        End With

        With Me.m_ProfileConfig
            .p_Title = Me.GetLangItem(m_xmld, "/data/profile/title")
            .p_Wins = Me.GetLangItem(m_xmld, "/data/profile/wins")
            .p_Losses = Me.GetLangItem(m_xmld, "/data/profile/losses")
            .p_Disconnects = Me.GetLangItem(m_xmld, "/data/profile/disconnects")
            .p_Location = Me.GetLangItem(m_xmld, "/data/profile/location")
            .p_Description = Me.GetLangItem(m_xmld, "/data/profile/description")
            .p_Rating = Me.GetLangItem(m_xmld, "/data/profile/rating")
            .p_Save = Me.GetLangItem(m_xmld, "/data/profile/save")
        End With

        RaiseEvent LoadMenuLanguageSucceeded()
    End Sub

    ''' <summary>
    ''' Retrieve a LanguageItem object from a path in an XML Document.
    ''' </summary>
    ''' <param name="xmlDoc">XmlDocument which contains the path</param>
    ''' <param name="path">Path to the item to be loaded</param>
    ''' <returns>LanguageItem corresponding to the path</returns>
    ''' <remarks>Just a way to cut space in the load routine.</remarks>
    Private Function GetLangItem(ByRef xmlDoc As XmlDocument, ByVal path As String) As String
        Dim ret As String = ""
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        m_nodelist = xmlDoc.SelectNodes(path)
        For Each m_node In m_nodelist
            ret = m_node.InnerText
        Next
        Return ret
    End Function

    Public ReadOnly Property TEXT_EVENTS() As Collection
        Get
            Return Me.m_events
        End Get
    End Property

    Public ReadOnly Property MENU_LOADBOT() As String
        Get
            Return m_menu_LoadBot
        End Get
    End Property

    Public ReadOnly Property MENU_UNLOADBOT() As String
        Get
            Return m_menu_UnloadBot
        End Get
    End Property

    Public ReadOnly Property MENU_CONNECTION() As String
        Get
            Return m_menu_Connection
        End Get
    End Property

    Public ReadOnly Property MENU_BNETRECONNECT() As String
        Get
            Return m_menu_BnetReconnect
        End Get
    End Property

    Public ReadOnly Property MENU_BNETDISCONNECT() As String
        Get
            Return m_menu_BnetDisconnect
        End Get
    End Property

    Public ReadOnly Property MENU_BOTNETRECONNECT() As String
        Get
            Return m_menu_BotnetReconnect
        End Get
    End Property

    Public ReadOnly Property MENU_BOTNETDISCONNECT() As String
        Get
            Return m_menu_BotnetDisconnect
        End Get
    End Property

    Public ReadOnly Property TAB_CHANNEL() As String
        Get
            Return m_tab_Channel
        End Get
    End Property

    Public ReadOnly Property TAB_FRIENDS() As String
        Get
            Return m_tab_Friends
        End Get
    End Property

    Public ReadOnly Property TAB_BOTNET() As String
        Get
            Return m_tab_Botnet
        End Get
    End Property

    Public ReadOnly Property TAB_CLAN() As String
        Get
            Return m_tab_Clan
        End Get
    End Property
End Class

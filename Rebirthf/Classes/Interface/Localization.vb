Imports System.Xml
Imports System.IO

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

    Private m_events As Collection

    Private WithEvents fileGrab As DownloadFile

    Public Event LoadMenuLanguageFailed()
    Public Event LoadMenuLanguageSucceeded()

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
        Debug.Print(m_tab_Channel)
        Debug.Print(Me.GetLangItem(m_xmld, "/data/tabs/channel"))

        m_tab_Friends = Me.GetLangItem(m_xmld, "/data/tabs/friends")
        m_tab_Botnet = Me.GetLangItem(m_xmld, "/data/tabs/botnet")
        m_tab_Clan = Me.GetLangItem(m_xmld, "/data/tabs/clan")

        RaiseEvent LoadMenuLanguageSucceeded()
    End Sub

    Private Function GetLangItem(ByRef xmlDoc As XmlDocument, ByVal path As String) As String
        Dim ret As String = ""
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode

        m_nodelist = xmlDoc.SelectNodes(path)
        For Each m_node In m_nodelist
            ret = m_node.InnerText
            'Debug.Print(m_node.InnerText)
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

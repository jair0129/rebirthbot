Imports System.Xml
Imports System.IO

Public Class Config
    Private bnetCfg As BnetConfig
    Private botnetCfg As BotnetConfig

    Public Sub LoadConfig(ByVal Filename As String)
        bnetCfg = New BnetConfig
        botnetCfg = New BotnetConfig

        Dim m_xmld As XmlDocument

        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        Dim type As String = ""
        Dim langCheck As String = ""
        Dim cfgPath As String = Application.StartupPath & "\Profiles\" & Filename & "\config.xml"

        m_xmld = New XmlDocument()
        m_xmld.Load(cfgPath)

        m_nodelist = m_xmld.SelectNodes("/connection")
        For Each m_node In m_nodelist
            type = m_node.Attributes.GetNamedItem("type").Value
            langCheck = m_node.Attributes.GetNamedItem("lang").Value
        Next

        If type = "" Then Exit Sub
        If langCheck = "" Then langCheck = "en-US"

        If Not File.Exists(Application.StartupPath & "\Profiles\" & Filename & "\colors.xml") Then
            If Not File.Exists(Application.StartupPath & "\localization\colors_" & langCheck & ".xml") Then
                Throw New FileNotFoundException
            End If

            File.Copy(Application.StartupPath & "\localization\colors_" & langCheck & ".xml", _
                      Application.StartupPath & "\Profiles\" & Filename & "\colors.xml", True)
        End If

        ' section to check that the colors file matches the required version
        Dim m_xmldColors As XmlDocument = New XmlDocument()

        Dim m_nodelistColors As XmlNodeList
        Dim m_nodeColors As XmlNode
        Dim lang As String = ""

        m_xmldColors.Load(Application.StartupPath & "\Profiles\" & Filename & "\colors.xml")
        m_nodelistColors = m_xmldColors.SelectNodes("/connection")
        For Each m_nodeColors In m_nodelistColors
            lang = m_nodeColors.Attributes.GetNamedItem("lang").Value
        Next

        If lang = "" Or lang <> langCheck Then
            If Not File.Exists(Application.StartupPath & "\localization\colors_" & langCheck & ".xml") Then
                Throw New FileNotFoundException
            End If
            File.Delete(Application.StartupPath & "\Profiles\" & Filename & "\colors.xml")

            File.Copy(Application.StartupPath & "\localization\colors_" & langCheck & ".xml", _
                      Application.StartupPath & "\Profiles\" & Filename & "\colors.xml", True)
        End If

        If type = "bnet" Then
            Me.bnetCfg.USERNAME = Me.GetConfigItem(m_xmld, "/connection/bnet/username")
            Me.bnetCfg.PASSWORD = Me.GetConfigItem(m_xmld, "/connection/bnet/password")
            Me.bnetCfg.SERVER = Me.GetConfigItem(m_xmld, "/connection/bnet/server")
            Me.bnetCfg.CLIENT = Me.GetConfigItem(m_xmld, "/connection/bnet/client")
            Me.bnetCfg.CDKEY = Me.GetConfigItem(m_xmld, "/connection/bnet/cdkey")
            Me.bnetCfg.EXPKEY = Me.GetConfigItem(m_xmld, "/connection/bnet/expkey")
            Me.bnetCfg.HOME = Me.GetConfigItem(m_xmld, "/connection/bnet/home")
            Me.bnetCfg.OWNER = Me.GetConfigItem(m_xmld, "/connection/bnet/owner")

            Me.botnetCfg.SERVER = Me.GetConfigItem(m_xmld, "/connection/botnet/server")
            Me.botnetCfg.SERVERPWD = Me.GetConfigItem(m_xmld, "/connection/botnet/serverpwd")
            Me.botnetCfg.PORT = Long.Parse(Me.GetConfigItem(m_xmld, "/connection/botnet/port"))
            Me.botnetCfg.DATABASE = Me.GetConfigItem(m_xmld, "/connection/botnet/database")
            Me.botnetCfg.DATABASEPWD = Me.GetConfigItem(m_xmld, "/connection/botnet/databasepwd")
            Me.botnetCfg.ACCOUNT = Me.GetConfigItem(m_xmld, "/connection/botnet/account")
            Me.botnetCfg.ACCOUNTPWD = Me.GetConfigItem(m_xmld, "/connection/botnet/accountpwd")
            Me.botnetCfg.DBMASK = Me.GetConfigItem(m_xmld, "/connection/botnet/databasemask")
        End If
    End Sub

    Private Function GetConfigItem(ByRef xmlDoc As XmlDocument, ByVal path As String) As String
        Dim ret As String = ""
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        m_nodelist = xmlDoc.SelectNodes(path)
        For Each m_node In m_nodelist
            ret = m_node.InnerText
        Next

        Return ret
    End Function

    Public Sub SaveConfig(ByVal Filename As String)
        Dim settings As New XmlWriterSettings()

        settings.Indent = True
        settings.NewLineOnAttributes = True

        Using writer As XmlWriter = XmlWriter.Create(Application.StartupPath & "\Profiles\" & Filename, settings)
            writer.WriteStartDocument()
            writer.WriteStartElement("Connection")

            writer.WriteStartElement("bnet")

            writer.WriteStartElement("username")
            writer.WriteString(Me.bnetCfg.USERNAME)
            writer.WriteEndElement()

            writer.WriteStartElement("password")
            writer.WriteString(Me.bnetCfg.PASSWORD)
            writer.WriteEndElement()

            writer.WriteStartElement("server")
            writer.WriteString(Me.bnetCfg.SERVER)
            writer.WriteEndElement()

            writer.WriteStartElement("client")
            writer.WriteString(Me.bnetCfg.CLIENT)
            writer.WriteEndElement()

            writer.WriteStartElement("cdkey")
            writer.WriteString(Me.bnetCfg.CDKEY)
            writer.WriteEndElement()

            writer.WriteStartElement("expkey")
            writer.WriteString(Me.bnetCfg.EXPKEY)
            writer.WriteEndElement()

            writer.WriteStartElement("home")
            writer.WriteString(Me.bnetCfg.HOME)
            writer.WriteEndElement()

            writer.WriteStartElement("owner")
            writer.WriteString(Me.bnetCfg.OWNER)
            writer.WriteEndElement()

            writer.WriteEndElement()

            writer.WriteStartElement("botnet")

            writer.WriteStartElement("enablebotnet")
            writer.WriteString(Me.botnetCfg.ENABLE)
            writer.WriteEndElement()

            writer.WriteStartElement("hideserver")
            writer.WriteString(Me.botnetCfg.HIDESERVER)
            writer.WriteEndElement()

            writer.WriteStartElement("hidechannel")
            writer.WriteString(Me.botnetCfg.HIDECHANNEL)
            writer.WriteEndElement()

            writer.WriteStartElement("hidebnetaccount")
            writer.WriteString(Me.botnetCfg.HIDEUSERNAME)
            writer.WriteEndElement()

            writer.WriteStartElement("server")
            writer.WriteString(Me.botnetCfg.SERVER)
            writer.WriteEndElement()

            writer.WriteStartElement("serverpwd")
            writer.WriteString(Me.botnetCfg.SERVERPWD)
            writer.WriteEndElement()

            writer.WriteStartElement("port")
            writer.WriteString(Me.botnetCfg.PORT)
            writer.WriteEndElement()

            writer.WriteStartElement("database")
            writer.WriteString(Me.botnetCfg.DATABASE)
            writer.WriteEndElement()

            writer.WriteStartElement("databasepwd")
            writer.WriteString(Me.botnetCfg.DATABASEPWD)
            writer.WriteEndElement()

            writer.WriteStartElement("account")
            writer.WriteString(Me.botnetCfg.ACCOUNT)
            writer.WriteEndElement()

            writer.WriteStartElement("accountpwd")
            writer.WriteString(Me.botnetCfg.ACCOUNTPWD)
            writer.WriteEndElement()

            writer.WriteStartElement("dbmask")
            writer.WriteString(Me.botnetCfg.DBMASK)
            writer.WriteEndElement()


            writer.WriteEndElement()

            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Flush()
        End Using
    End Sub

    Public Property BNET() As BnetConfig
        Get
            Return bnetCfg
        End Get
        Set(ByVal value As BnetConfig)
            bnetCfg = value
        End Set
    End Property

    Public Property BOTNET() As BotnetConfig
        Get
            Return botnetCfg
        End Get
        Set(ByVal value As BotnetConfig)
            botnetCfg = value
        End Set
    End Property
End Class

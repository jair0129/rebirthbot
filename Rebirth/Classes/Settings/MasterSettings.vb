Imports System.Xml
Imports System.IO

Public Class MasterSettings

    Private autoLoad As List(Of String)
    Private autoConnect As List(Of String)
    Private m_langAvailable As List(Of LanguageItem)
    Public m_interfaceLang As LanguageItem

    Public Sub LoadSettings(ByVal Filename As String)

        autoLoad = New List(Of String)
        autoConnect = New List(Of String)
        m_langAvailable = New List(Of LanguageItem)

        m_interfaceLang = New LanguageItem("English (US)", "en-US")

        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        Dim filePath As String = Application.StartupPath & "\" & Filename & ".xml"

        If Not File.Exists(filePath) Then
            Me.SaveSettings(Filename)
            Exit Sub
        End If

        m_xmld = New XmlDocument()
        m_xmld.Load(filePath)


        Try
            m_nodelist = m_xmld.SelectNodes("/settings/interfacelang/use")
            For Each m_node In m_nodelist
                If m_node.InnerText <> "" Then
                    m_interfaceLang = New LanguageItem(m_node.Attributes.GetNamedItem("long").Value, m_node.InnerText)
                End If
            Next
        Catch ex As Exception
            m_interfaceLang = New LanguageItem("English (US)", "en-US")
        End Try

        m_nodelist = m_xmld.SelectNodes("/settings/interfacelang/available")
        For Each m_node In m_nodelist
            If m_node.InnerText <> "" Then
                Dim tmp As New LanguageItem(m_node.Attributes.GetNamedItem("long").Value, m_node.InnerText)
                m_langAvailable.Add(tmp)
            End If
        Next

        If m_interfaceLang Is Nothing Then
            m_interfaceLang = New LanguageItem("English (US)", "en-US")
        End If

        frmMain.langCode = m_interfaceLang.CODE()

        Try
            m_nodelist = m_xmld.SelectNodes("/settings/autoload/profile")

            For Each m_node In m_nodelist
                If m_node.InnerText <> "" Then
                    Dim name As String = m_node.InnerText
                    autoLoad.Add(name)
                    Try
                        Dim t As String
                        t = m_node.Attributes.GetNamedItem("autoconnect").Value
                        If Boolean.Parse(t) Then
                            frmMain.LoadBot(name, True)
                            autoConnect.Add(name)
                        Else
                            frmMain.LoadBot(name)
                        End If
                    Catch ex As Exception
                        frmMain.LoadBot(name)
                    End Try
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub

    Public Sub LoadSettingsNoAction(ByVal Filename As String)

        autoLoad = New List(Of String)
        autoConnect = New List(Of String)
        m_langAvailable = New List(Of LanguageItem)
        m_interfaceLang = New LanguageItem("English (US)", "en-US")

        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        Dim filePath As String = Application.StartupPath & "\" & Filename & ".xml"

        If Not File.Exists(filePath) Then
            Me.SaveSettings(Filename)
            Exit Sub
        End If

        m_xmld = New XmlDocument()
        m_xmld.Load(filePath)

        Try
            m_nodelist = m_xmld.SelectNodes("/settings/interfacelang/use")
            For Each m_node In m_nodelist
                If m_node.InnerText <> "" Then
                    m_interfaceLang = New LanguageItem(m_node.Attributes.GetNamedItem("long").Value, m_node.InnerText)
                End If
            Next
        Catch ex As Exception
            m_interfaceLang = New LanguageItem("English (US)", "en-US")
        End Try

        Try
            m_nodelist = m_xmld.SelectNodes("/settings/interfacelang/available")
            For Each m_node In m_nodelist
                If m_node.InnerText <> "" Then
                    Dim tmp As New LanguageItem(m_node.Attributes.GetNamedItem("long").Value, m_node.InnerText)
                    m_langAvailable.Add(tmp)
                End If
            Next
        Catch ex As Exception
            Dim tmp As New LanguageItem("English (US)", "en-US")
            m_langAvailable.Add(tmp)
        End Try

        If m_interfaceLang Is Nothing Then m_interfaceLang = New LanguageItem("English (US)", "en-US")

        Try
            m_nodelist = m_xmld.SelectNodes("/settings/autoload/profile")

            For Each m_node In m_nodelist
                If m_node.InnerText <> "" Then
                    Dim name As String = m_node.InnerText
                    autoLoad.Add(name)
                    Try
                        Dim t As String
                        t = m_node.Attributes.GetNamedItem("autoconnect").Value
                        If Boolean.Parse(t) Then
                            autoConnect.Add(name)
                        End If
                    Catch ex As Exception
                    End Try
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub


    Public Sub SaveSettings(ByVal Filename As String)
        Dim settings As New XmlWriterSettings()
        settings.Indent = True
        settings.NewLineOnAttributes = True

        Using writer As XmlWriter = XmlWriter.Create(Filename, settings)
            writer.WriteStartDocument()
            writer.WriteStartElement("settings")
            writer.WriteStartElement("autoload")

            For Each item As String In autoLoad
                If Not item Is Nothing Then
                    If item = "" Then Continue For

                    writer.WriteStartElement("profile")
                    If autoConnect.Contains(item) Then
                        writer.WriteAttributeString("autoconnect", "True")
                    Else
                        writer.WriteAttributeString("autoconnect", "False")
                    End If
                    writer.WriteString(item)
                    writer.WriteEndElement()
                End If
            Next item

            writer.WriteEndElement()

            writer.WriteStartElement("interfacelang")
            writer.WriteStartElement("use")
            writer.WriteAttributeString("long", "English")
            writer.WriteString("en-US")
            For Each str As LanguageItem In m_langAvailable
                If str IsNot Nothing Then
                    writer.WriteAttributeString("long", str.NAME())
                    writer.WriteElementString("available", str.CODE())
                End If
            Next
            writer.WriteEndElement()

            writer.WriteEndElement()

            writer.WriteEndDocument()
            writer.Flush()
        End Using
    End Sub

    Public Property InterfaceLanguage() As LanguageItem
        Get
            Return Me.m_interfaceLang
        End Get
        Set(ByVal value As LanguageItem)
            m_interfaceLang = value
        End Set
    End Property
End Class

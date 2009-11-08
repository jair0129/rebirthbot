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
''' Handles the global settings for the bot
''' </summary>
Public Class MasterSettings

    Private autoLoad As List(Of String)
    Private autoConnect As List(Of String)
    Private m_langAvailable As List(Of LanguageItem)
    Public m_interfaceLang As LanguageItem

    Public Sub New()
        m_langAvailable = New List(Of LanguageItem)
        m_interfaceLang = New LanguageItem("English (US)", "en-US")
    End Sub

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

        Try
            m_nodelist = m_xmld.SelectNodes("/settings/interfacelang/available")
            For Each m_node In m_nodelist
                If m_node.InnerText <> "" Then
                    Dim tmp As New LanguageItem(m_node.Attributes.GetNamedItem("long").Value, m_node.InnerText)
                    tmp.LOCALNAME = m_node.Attributes.GetNamedItem("local").Value
                    If Not Me.HasLang(tmp) Then m_langAvailable.Add(tmp)
                End If
            Next
        Catch ex As Exception
            Dim tmp As New LanguageItem("English (US)", "en-US")
            m_langAvailable.Add(tmp)
        End Try

        If m_interfaceLang Is Nothing Then m_interfaceLang = New LanguageItem("English (US)", "en-US")

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
                    tmp.LOCALNAME = m_node.Attributes.GetNamedItem("local").Value
                    If Not Me.HasLang(tmp) Then m_langAvailable.Add(tmp)
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

        Dim filePath As String = Application.StartupPath & "\" & Filename & ".xml"
        Using writer As XmlWriter = XmlWriter.Create(filePath, settings)
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
            writer.WriteAttributeString("long", Me.m_interfaceLang.NAME)
            writer.WriteAttributeString("local", Me.m_interfaceLang.LOCALNAME)
            writer.WriteString(Me.m_interfaceLang.CODE)
            writer.WriteEndElement()

            For Each str As LanguageItem In m_langAvailable
                If str IsNot Nothing Then
                    writer.WriteStartElement("available")
                    writer.WriteAttributeString("long", str.NAME())
                    writer.WriteAttributeString("local", str.LOCALNAME())
                    writer.WriteString(str.CODE())
                    writer.WriteEndElement()
                End If
            Next

            writer.WriteEndElement()

            writer.WriteEndDocument()
            writer.Flush()
            writer.Close()
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

    Public Sub AddAvailableLang(ByVal lItem As LanguageItem)
        If lItem IsNot Nothing Then Me.m_langAvailable.Add(lItem)
    End Sub

    Public ReadOnly Property AvailableLang() As List(Of LanguageItem)
        Get
            Return m_langAvailable
        End Get
    End Property

    Public Function HasLang(ByVal li As LanguageItem) As Boolean
        For Each k As LanguageItem In Me.m_langAvailable
            If k.CODE.ToLower = li.CODE.ToLower Then Return True
        Next k

        Return False
    End Function

    Public Sub SetAutoLoad(ByVal profile As String, ByVal val As Boolean)
        If val Then
            If Me.autoLoad.Contains(profile) Then Exit Sub
            Me.autoLoad.Add(profile)
        Else
            If Not Me.autoLoad.Contains(profile) Then Exit Sub
            Me.autoLoad.Remove(profile)
        End If
    End Sub

    Public Sub SetAutoConnect(ByVal profile As String, ByVal val As Boolean)
        If val Then
            If Me.autoConnect.Contains(profile) Then Exit Sub
            Me.autoConnect.Add(profile)
        Else
            If Not Me.autoConnect.Contains(profile) Then Exit Sub
            Me.autoConnect.Remove(profile)
        End If
    End Sub

    Public Function DoesAutoLoad(ByVal profile As String) As Boolean
        If Me.autoLoad Is Nothing Then Return False
        If profile Is Nothing Then Return False
        Return Me.autoLoad.Contains(profile)
    End Function

    Public Function DoesAutoConnect(ByVal profile As String) As Boolean
        If Me.autoConnect Is Nothing Then Return False
        If profile Is Nothing Then Return False
        Return Me.autoConnect.Contains(profile)
    End Function
End Class

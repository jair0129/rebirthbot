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

Public Class frmMain

    Private WithEvents SiteCheck As WebsiteCheck
    Public langCode As String

    Public v_Channel As String
    Public v_Friends As String
    Public v_Botnet As String
    Public v_Clan As String
    Public pConf As ProfileConfig
    Public cConf As ConfigConfig


    Public Sub LoadBot(ByVal Text As String, Optional ByVal AutoConnect As Boolean = False)
        If Directory.Exists(Application.StartupPath & "\Profiles\" & Text) Then

            Dim index As Integer = tabBots.TabPages.Count()
            tabBots.TabPages.Add(Text)
            tabBots.SelectedIndex = index

            ReDim Preserve uiBotInstance(index)
            uiBotInstance(index) = New BotInterface

            uiBotInstance(index).langCode = Me.langCode
            uiBotInstance(index).TabIndex = index
            uiBotInstance(index).Tag = Text
            uiBotInstance(index).Dock = DockStyle.Fill
            uiBotInstance(index).pConf = Me.pConf
            uiBotInstance(index).Refresh()

            uiBotInstance(index).tabLists.TabPages(0).Text = Me.v_Channel
            uiBotInstance(index).tabLists.TabPages(1).Text = Me.v_Friends
            uiBotInstance(index).tabLists.TabPages(2).Text = Me.v_Botnet
            uiBotInstance(index).tabLists.TabPages(3).Text = Me.v_Clan

            tabBots.TabPages(index).Controls.Add(uiBotInstance(index))
            tabBots.TabPages(index).Refresh()

            uiBotInstance(index).ResizeInstance()

            If AutoConnect Then uiBotInstance(index).Connect()
        Else
            ' show create profile
        End If
    End Sub

    Public Sub UnloadBot(ByVal Text As String)
        Dim i As Integer

        For i = 0 To tabBots.TabPages.Count() - 1
            If tabBots.TabPages.Item(i).Text = Text Then
                tabBots.TabPages.RemoveAt(i)
                Call uiBotInstance(i).Disconnect()
                uiBotInstance(i) = Nothing
                Exit Sub
            End If
        Next i
    End Sub

    Private Sub mnuLoadBot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLoadBot.Click
        LoadProfile.Visible = True
    End Sub

    Private Sub UnloadBotToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUnload.Click
        If tabBots.SelectedIndex < 0 Then Exit Sub
        If uiBotInstance(tabBots.SelectedIndex) IsNot Nothing Then Call Me.UnloadBot(uiBotInstance(tabBots.SelectedIndex).Tag)
    End Sub

    Private Sub mnuReconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReconnect.Click
        Call uiBotInstance(tabBots.SelectedIndex).Disconnect()
        Call uiBotInstance(tabBots.SelectedIndex).Connect()
    End Sub

    Private Sub mnuDisconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDisconnect.Click
        Call uiBotInstance(tabBots.SelectedIndex).Disconnect()
    End Sub

    Private Sub TestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestToolStripMenuItem.Click
        ColorSwatch.Visible = True
    End Sub

    Private Sub tabBots_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabBots.SelectedIndexChanged
        Dim i As Integer
        For i = 0 To uiBotInstance.Length() - 1
            If uiBotInstance(i) IsNot Nothing Then
                If uiBotInstance(i).lvChannel IsNot Nothing Then uiBotInstance(i).lvChannel.Refresh()
            End If
        Next
    End Sub

    Private Sub SiteCheck_FileDownloaded(ByVal URL As String, ByVal LocalPath As String) Handles SiteCheck.FileDownloaded
        Debug.Print("saved to " & LocalPath)
    End Sub

    Private Sub SiteCheck_NoUpdate() Handles SiteCheck.NoUpdate
        Debug.Print("no update")
    End Sub

    Private Sub SiteCheck_UpdateAvailable(ByVal URL As String) Handles SiteCheck.UpdateAvailable
        Debug.Print("getting " & URL)
        SiteCheck.GrabFile(URL)
    End Sub

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadingScreen.Visible = True
        LoadingScreen.Show()
    End Sub

    Private Sub ConfigurationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfigurationToolStripMenuItem.Click
        Configuration.SetText(Me.cConf)
        Configuration.Show()
    End Sub
End Class

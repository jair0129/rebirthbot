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

''' <summary>
''' Preload interface language, check for updates, check for directory structures,
''' check for proper configurations, check for hashes.
''' </summary>
''' <remarks>
''' The user will not see this form unless they have an incredibly slow computer,
''' internet connection, or both.</remarks>

Public NotInheritable Class LoadingScreen

    Private WithEvents SiteCheck As WebsiteCheck
    Private WithEvents interfacetext As Localization
    Private WithEvents fileDownload As DownloadFile

    Private canClose As Boolean

    Private Sub AddText(ByVal ParamArray items() As Object)
        Dim k As String = ""
        With Me.rtbStatus
            For Each item As String In items
                If item Is Nothing Then GoTo skipItem
                .SelectionStart = .TextLength
                .SelectionLength = 0
                .SelectionColor = Color.Black
                .SelectedText = item
                .SelectionStart = .TextLength
                k &= item
            Next item
            .SelectionStart = .TextLength
            .SelectionLength = 0
            .SelectionColor = Color.Black
            .SelectedText = vbCrLf

skipItem:
            .ScrollToCaret()
        End With
        'Debug.Print(k)
    End Sub

    Private Sub LoadingScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        canClose = False

        Version.Text = BOT_TITLE

        'Copyright info
        Copyright.Text = My.Application.Info.Copyright
    End Sub

    Private Sub SiteCheck_FileDownloaded(ByVal URL As String, ByVal LocalPath As String) Handles SiteCheck.FileDownloaded
        If LocalPath.Substring(LocalPath.LastIndexOf(".") + 1) <> "rar" Then Exit Sub

        Dim str As String = "Update downloaded to: " & vbCrLf & LocalPath & vbCrLf
        str &= vbCrLf & "To use the new version, please unpack and run."

        MsgBox(str, MsgBoxStyle.OkOnly, "Update")
    End Sub

    Private Sub SiteCheck_NoUpdate() Handles SiteCheck.NoUpdate
        Me.AddText("No update found")
        Me.ContinueLoading()
    End Sub

    Private Sub SiteCheck_UpdateAvailable(ByVal URL As String) Handles SiteCheck.UpdateAvailable
        Me.AddText("Update available!")
        Dim f As MsgBoxResult = MsgBox("New version available.  Download?", MsgBoxStyle.YesNo, "Rebirth Update")
        If f = MsgBoxResult.Yes Then
            Me.AddText("Getting update")
            SiteCheck.GrabFile(URL)
        Else
            Me.AddText("Skipping update")
            Me.ContinueLoading()
        End If
    End Sub

    Private Sub ContinueLoading()
        Dim k As New MasterSettings

        interfacetext = New Localization
        fileDownload = New DownloadFile

        Me.AddText("Checking local file structure...")
        Dim rf As New List(Of RebirthFile)
        rf = SiteCheck.GrabFileList()

        For Each rfile As RebirthFile In rf
            If rfile.IsRequired Then
                If rfile.IsDirectory And Not Directory.Exists(Application.StartupPath & "\" & rfile.FilePath) Then
                    Directory.CreateDirectory(Application.StartupPath & "\" & rfile.FilePath)
                End If
                If Not rfile.IsDirectory Then
                    If Not File.Exists(Application.StartupPath & "\" & rfile.FilePath & "\" & rfile.FileName) Then
                        SiteCheck.GrabFile(BOT_SITE & rfile.FilePath.Replace("\", "/") & "/" & rfile.FileName, _
                                           Application.StartupPath & "\" & rfile.FilePath & "\" & rfile.FileName)
                    End If
                End If
            End If
        Next

        k.LoadSettingsNoAction("settings")
        Me.AddText("Loading localization settings...")
        Me.AddText("Loading language ", k.m_interfaceLang.NAME)

        interfacetext.LoadMenuLanguage(k.m_interfaceLang.CODE)

        frmMain.mnuLoadBot.Text = interfacetext.MENU_LOADBOT
        frmMain.mnuUnload.Text = interfacetext.MENU_UNLOADBOT
        frmMain.mnuConnection.Text = interfacetext.MENU_CONNECTION
        frmMain.mnuReconnect.Text = interfacetext.MENU_BNETRECONNECT
        frmMain.mnuDisconnect.Text = interfacetext.MENU_BNETDISCONNECT

        frmMain.v_Channel = interfacetext.TAB_CHANNEL
        frmMain.v_Friends = interfacetext.TAB_FRIENDS
        frmMain.v_Botnet = interfacetext.TAB_BOTNET
        frmMain.v_Clan = interfacetext.TAB_CLAN

        frmMain.pConf = interfacetext.m_ProfileConfig
        frmMain.cConf = interfacetext.m_ConfigConfig

        ReDim uiBotInstance(0)

        CreateColorList()


        Dim hashRaw As String = Application.StartupPath & "\Hashes\"
        Dim urlRaw As String = "http://rabbitx86.net/rebirth/" & BOT_VERSION & "/Hashes/"

        Dim localHashes As New List(Of String)
        Dim remoteHashes As New List(Of String)

        Me.AddText("Checking hash files...")

        Dim dirs() As String = Directory.GetDirectories(Application.StartupPath & "\Profiles")

        Dim requireSTAR As Boolean = False
        Dim requireW2BN As Boolean = False
        Dim requireD2DV As Boolean = False
        Dim requireD2XP As Boolean = False
        Dim requireWAR3 As Boolean = False

        For Each dir As String In dirs
            'Application.DoEvents()
            Dim profile As String
            Dim check As New Config
            profile = dir.Substring(dir.LastIndexOf("\") + 1)
            check.LoadConfig(profile)

            If check.BNET.CLIENT = "STAR" Or check.BNET.CLIENT = "SEXP" Then requireSTAR = True
            If check.BNET.CLIENT = "W2BN" Then requireW2BN = True
            If check.BNET.CLIENT = "D2DV" Then requireD2DV = True
            If check.BNET.CLIENT = "D2XP" Then requireD2XP = True
            If check.BNET.CLIENT = "WAR3" Then requireWAR3 = True
        Next dir

        If requireSTAR Then
            Me.AddText("StarCraft hashes required")
            localHashes.Add(hashRaw & "STAR\starcraft.exe")
            localHashes.Add(hashRaw & "STAR\storm.dll")
            localHashes.Add(hashRaw & "STAR\battle.snp")
            localHashes.Add(hashRaw & "STAR\star.bin")

            remoteHashes.Add(urlRaw & "STAR/Starcraft.exe")
            remoteHashes.Add(urlRaw & "STAR/Storm.dll")
            remoteHashes.Add(urlRaw & "STAR/Battle.snp")
            remoteHashes.Add(urlRaw & "STAR/STAR.bin")
        End If

        If requireW2BN Then
            Me.AddText("Warcraft II BNE hashes required")
            localHashes.Add(hashRaw & "W2BN\Warcraft II BNE.exe")
            localHashes.Add(hashRaw & "W2BN\storm.dll")
            localHashes.Add(hashRaw & "W2BN\battle.snp")
            localHashes.Add(hashRaw & "W2BN\w2bn.bin")

            remoteHashes.Add(urlRaw & "W2BN/Warcraft II BNE.exe")
            remoteHashes.Add(urlRaw & "W2BN/storm.dll")
            remoteHashes.Add(urlRaw & "W2BN/battle.snp")
            remoteHashes.Add(urlRaw & "W2BN/W2BN.bin")
        End If

        If requireD2DV Then
            Me.AddText("Diablo II hashes required")
            localHashes.Add(hashRaw & "D2DV\Game.exe")
            localHashes.Add(hashRaw & "D2DV\Bnclient.dll")
            localHashes.Add(hashRaw & "D2DV\D2Client.dll")

            remoteHashes.Add(urlRaw & "D2DV/Game.exe")
            remoteHashes.Add(urlRaw & "D2DV/Bnclient.dll")
            remoteHashes.Add(urlRaw & "D2DV/D2Client.dll")
        End If

        If requireD2XP Then
            Me.AddText("Lord of Destruction hashes required")
            localHashes.Add(hashRaw & "D2XP\Game.exe")
            localHashes.Add(hashRaw & "D2XP\Bnclient.dll")
            localHashes.Add(hashRaw & "D2XP\D2Client.dll")

            remoteHashes.Add(urlRaw & "D2XP/Game.exe")
            remoteHashes.Add(urlRaw & "D2XP/Bnclient.dll")
            remoteHashes.Add(urlRaw & "D2XP/D2Client.dll")
        End If

        If requireWAR3 Then
            Me.AddText("Warcraft III hashes required")
            localHashes.Add(hashRaw & "WAR3\war3.exe")
            localHashes.Add(hashRaw & "WAR3\game.dll")
            localHashes.Add(hashRaw & "WAR3\Storm.dll")

            remoteHashes.Add(urlRaw & "WAR3/war3.exe")
            remoteHashes.Add(urlRaw & "WAR3/game.dll")
            remoteHashes.Add(urlRaw & "WAR3/Storm.dll")
        End If

        ' check for hashes
        For i = 0 To localHashes.Count() - 1
            'Application.DoEvents()
            If Not File.Exists(localHashes(i)) Then
                Dim localName As String = localHashes(i).Substring(localHashes(i).IndexOf("Hashes"))
                localName = localName.Substring(localName.IndexOf("\"))
                Me.AddText("Downloading hash file: ", localName)
                fileDownload.Download(remoteHashes(i), localHashes(i))
            End If
        Next


        k.LoadSettings("settings")

        Dim f As List(Of RebirthLocalization) = SiteCheck.GetLocalizationList()
        For Each thing In f
            If Not k.HasLang(thing.LangItem) Then k.AddAvailableLang(thing.LangItem)
        Next

        k.SaveSettings("settings")

        k = Nothing
        interfacetext = Nothing

        canClose = True
    End Sub

    Private Sub interfacetext_LoadMenuLanguageFailed() Handles interfacetext.LoadMenuLanguageFailed
        Dim k As MsgBoxResult = MsgBox("Failed to load menu localization.  Please visit iccup.com forums for support.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Critical Error")

        Do Until k = MsgBoxResult.Ok
            k = MsgBox("Failed to load menu localization.  Please visit iccup.com forums for support.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Critical Error")
        Loop

        Application.Exit()
    End Sub

    Private Sub interfacetext_LoadMenuLanguageSucceeded() Handles interfacetext.LoadMenuLanguageSucceeded
        Me.AddText("Loaded interface language")
    End Sub

    Private Sub fileDownload_FileDownloaded(ByVal URL As String, ByVal filePath As String) Handles fileDownload.FileDownloaded
        Dim localName As String = filePath.Substring(filePath.IndexOf("Hashes"))
        localName = localName.Substring(localName.IndexOf("\"))
        Me.AddText("Downloaded file: ", localName)
    End Sub

    Private Sub fileDownload_FileDownloadFailed(ByVal URL As String) Handles fileDownload.FileDownloadFailed
        Dim localName As String = URL.Substring(URL.IndexOf("Hashes"))
        localName = localName.Substring(localName.IndexOf("/"))
        Me.AddText("Failed to download file: ", localName)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If canClose Then
            While frmMain.Visible = False
                Application.DoEvents()
                frmMain.Visible = True
            End While

            Timer1.Enabled = False

            Me.Visible = False
        End If
    End Sub

    Private Sub LoadingScreen_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then
            Me.AddText("Rebirth build " & BOT_VERSION & " loading...")
            Me.AddText("Checking for updates...")
            SiteCheck = New WebsiteCheck
            SiteCheck.CheckUpdate()
        End If
    End Sub
End Class

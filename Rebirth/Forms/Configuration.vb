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

Public Class Configuration

    Private mSettings As MasterSettings
    Private mCurrentConfig As Config

    Private Sub Configuration_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mSettings = New MasterSettings
        mSettings.LoadSettingsNoAction("settings")

        Dim dirs() As String = Directory.GetDirectories(Application.StartupPath & "\Profiles")

        For Each dir As String In dirs
            'Application.DoEvents()
            Dim profile As String
            profile = dir.Substring(dir.LastIndexOf("\") + 1)
            ComboBox1.Items.Add(profile)
            ComboBox1.Text = profile
        Next dir

        cmbGame.Items.Add("StarCraft")
        cmbGame.Items.Add("Brood War")
        cmbGame.Items.Add("Warcraft II")
        cmbGame.Items.Add("Diablo II")
        cmbGame.Items.Add("Lord of Destruction")
        cmbGame.Items.Add("Warcraft 3")
        cmbGame.Items.Add("The Frozen Throne")

        Dim langs As List(Of LanguageItem) = mSettings.AvailableLang()
        For Each item As LanguageItem In langs
            If item IsNot Nothing Then
                cmbLangs.Items.Add(item.LOCALNAME)
                ComboBox2.Items.Add(item.LOCALNAME)
            End If
        Next item

        For Each item As LanguageItem In langs
            If item IsNot Nothing Then
                If item.CODE = mSettings.m_interfaceLang.CODE Then
                    ComboBox2.SelectedItem = item.LOCALNAME
                    Exit For
                End If
            End If
        Next item

    End Sub

    Public Sub SetText(ByVal ccfg As ConfigConfig)
        Me.Text = ccfg.c_Title
        Me.tabGlobal.Text = ccfg.t_Global
        Me.tabProfile.Text = ccfg.t_Profiles

        Label1.Text = ccfg.p_Username
        Label2.Text = ccfg.p_Password
        Label3.Text = ccfg.p_Server
        Label4.Text = ccfg.p_CDKey
        Label5.Text = ccfg.p_EXPKey
        Label6.Text = ccfg.p_Home
        Label7.Text = ccfg.p_Lang
        Label8.Text = ccfg.p_Product
        Label9.Text = ccfg.p_Lang

        chkAutoconnect.Text = ccfg.p_Autoconnect
        chkAutoload.Text = ccfg.p_Autoload

        Button1.Text = ccfg.p_Save
        Button2.Text = ccfg.p_Save
    End Sub

    Private Sub SetProfile()
        If mCurrentConfig Is Nothing Then Exit Sub
        If mCurrentConfig.BNET Is Nothing Then
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""
            cmbLangs.SelectedItem = Nothing
            chkAutoload.Checked = False
            chkAutoconnect.Checked = False
            Exit Sub
        End If

        TextBox1.Text = mCurrentConfig.BNET.USERNAME
        TextBox2.Text = mCurrentConfig.BNET.PASSWORD
        TextBox3.Text = mCurrentConfig.BNET.SERVER
        TextBox4.Text = mCurrentConfig.BNET.CDKEY
        TextBox5.Text = mCurrentConfig.BNET.EXPKEY
        TextBox6.Text = mCurrentConfig.BNET.HOME

        Select Case mCurrentConfig.BNET.CLIENT
            Case "SEXP" : cmbGame.SelectedItem = "Brood War"
            Case "W2BN" : cmbGame.SelectedItem = "Warcraft II"
            Case "D2DV" : cmbGame.SelectedItem = "Diablo II"
            Case "D2XP" : cmbGame.SelectedItem = "Lord of Destruction"
            Case "WAR3" : cmbGame.SelectedItem = "Warcraft 3"
            Case "W3XP" : cmbGame.SelectedItem = "The Frozen Throne"
            Case Else : cmbGame.SelectedItem = "StarCraft"
        End Select

        chkAutoload.Checked = mSettings.DoesAutoLoad(mCurrentConfig.BNET.USERNAME)
        chkAutoconnect.Checked = mSettings.DoesAutoConnect(mCurrentConfig.BNET.USERNAME)

        If chkAutoconnect.Checked Then chkAutoload.Checked = True

        Dim f As String = mCurrentConfig.LANG

        Dim langs As List(Of LanguageItem) = mSettings.AvailableLang()
        For Each item As LanguageItem In langs
            If item IsNot Nothing Then
                If item.CODE = mCurrentConfig.LANG Then
                    cmbLangs.SelectedItem = item.LOCALNAME
                    Exit For
                End If
            End If
        Next item
    End Sub

    Private Sub chkAutoconnect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoconnect.CheckedChanged
        If chkAutoconnect.Checked Then
            chkAutoload.Checked = True
        End If
    End Sub

    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If ComboBox1.Text <> "" Then
            mCurrentConfig = New Config()
            If File.Exists(Application.StartupPath & "\Profiles\" & ComboBox1.Text & "\config.xml") Then
                mCurrentConfig.LoadConfig(ComboBox1.Text)
            End If
        End If
        Me.SetProfile()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text <> "" Then
            If File.Exists(Application.StartupPath & "\Profiles\" & ComboBox1.Text & "\config.xml") Then
                mCurrentConfig = New Config()
                mCurrentConfig.LoadConfig(ComboBox1.Text)
            End If
        End If
        Me.SetProfile()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Not File.Exists(Application.StartupPath & "\Profiles\" & ComboBox1.Text & "\config.xml") Then
            Directory.CreateDirectory(Application.StartupPath & "\Profiles\" & ComboBox1.Text)
            mCurrentConfig = New Config()
        End If

        mCurrentConfig.BNET.USERNAME = TextBox1.Text
        mCurrentConfig.BNET.PASSWORD = TextBox2.Text
        mCurrentConfig.BNET.SERVER = TextBox3.Text
        mCurrentConfig.BNET.CDKEY = TextBox4.Text
        mCurrentConfig.BNET.EXPKEY = TextBox5.Text
        mCurrentConfig.BNET.HOME = TextBox6.Text
        mCurrentConfig.BNET.OWNER = TextBox1.Text
        mCurrentConfig.TYPE = "bnet"

        Select Case cmbGame.SelectedItem
            Case "StarCraft" : mCurrentConfig.BNET.CLIENT = "STAR"
            Case "Brood War" : mCurrentConfig.BNET.CLIENT = "SEXP"
            Case "Warcraft II" : mCurrentConfig.BNET.CLIENT = "W2BN"
            Case "Diablo II" : mCurrentConfig.BNET.CLIENT = "D2DV"
            Case "Lord of Destruction" : mCurrentConfig.BNET.CLIENT = "D2XP"
            Case "Warcraft 3" : mCurrentConfig.BNET.CLIENT = "WAR3"
            Case "The Frozen Throne" : mCurrentConfig.BNET.CLIENT = "W3XP"
            Case Else : mCurrentConfig.BNET.CLIENT = "SEXP"
        End Select

        mSettings.SetAutoLoad(ComboBox1.Text, chkAutoload.Checked)
        mSettings.SetAutoConnect(ComboBox1.Text, chkAutoconnect.Checked)

        Dim langs As List(Of LanguageItem) = mSettings.AvailableLang()
        For Each item As LanguageItem In langs
            If item IsNot Nothing Then
                If item.LOCALNAME = cmbLangs.SelectedItem Then
                    mCurrentConfig.LANG = item.CODE
                    Exit For
                End If
            End If
        Next item

        mCurrentConfig.SaveConfig(ComboBox1.Text)
        mSettings.SaveSettings("settings")
        MsgBox("Profile " & ComboBox1.Text & " saved", MsgBoxStyle.OkOnly, "Profile saved")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim langs As List(Of LanguageItem) = mSettings.AvailableLang()
        For Each item As LanguageItem In langs
            If item IsNot Nothing Then
                If item.LOCALNAME = ComboBox2.SelectedItem Then
                    mSettings.m_interfaceLang = item
                    Exit For
                End If
            End If
        Next item

        mSettings.SaveSettings("settings")
        MsgBox("Settings saved", MsgBoxStyle.OkOnly, "Settings saved")
    End Sub
End Class
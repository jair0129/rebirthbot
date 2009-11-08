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

Public Class LoadProfile

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        If cmbProfile.Text <> "" Then
            Call frmMain.LoadBot(cmbProfile.Text)
            Me.Dispose()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub LoadProfile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim dirs() As String = Directory.GetDirectories(Application.StartupPath & "\Profiles")

        For Each dir As String In dirs
            'Application.DoEvents()
            Dim profile As String
            profile = dir.Substring(dir.LastIndexOf("\") + 1)
            cmbProfile.Items.Add(profile)
            cmbProfile.Text = profile
        Next dir
    End Sub
End Class
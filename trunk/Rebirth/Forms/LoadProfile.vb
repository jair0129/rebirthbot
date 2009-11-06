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
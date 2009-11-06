Public Class Configuration

    Private mSettings As MasterSettings

    Private Sub Configuration_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mSettings = New MasterSettings
        mSettings.LoadSettingsNoAction("settings.xml")
    End Sub
End Class
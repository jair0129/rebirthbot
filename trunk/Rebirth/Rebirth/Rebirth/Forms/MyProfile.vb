Public Class MyProfile
    Public BOT As BnetConnection

    Private Sub btnOkay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOkay.Click
        Me.BOT.UpdateProfile(Me.txtLoc.Text, Me.txtDescr.Text)
        Me.Dispose()
    End Sub
End Class
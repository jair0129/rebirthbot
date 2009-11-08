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

Public Class MyProfile
    Public MyConfig As ProfileConfig
    Public BOT As BnetConnection

    Private Sub btnOkay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOkay.Click
        Me.BOT.UpdateProfile(Me.txtLoc.Text, Me.txtDescr.Text)
        Me.Dispose()
    End Sub

    Public Sub SetText(ByVal pconf As ProfileConfig)
        Me.Label2.Text = pconf.p_Location
        Me.Label3.Text = pconf.p_Description
        Me.Label5.Text = pconf.p_Wins
        Me.Label6.Text = pconf.p_Losses
        Me.Label7.Text = pconf.p_Disconnects
        Me.Label8.Text = pconf.p_Rating
        Me.btnOkay.Text = pconf.p_Save
    End Sub
End Class
Public Class ColorSwatch

    Private ButtonList() As Button

    Private Sub ColorSwatch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CreateButtons()
    End Sub

    Private Sub CreateButtons()
        ReDim ButtonList(0)
        For i = 0 To 139
            ReDim ButtonList(i)
            ButtonList(i) = New Button()
            ButtonList(i).Size = New Size(16, 16)
            ButtonList(i).Left = 16 * (i Mod 9) + ListBox1.Width + 2
            ButtonList(i).Top = 16 * CInt(i \ 9)
            ButtonList(i).BackColor = ColorList(i).VALUE
            ButtonList(i).Tag = i & " " & ColorList(i).TEXT
            'Debug.Print(i & " " & ColorList(i).TEXT)
            Me.Controls.Add(ButtonList(i))
            AddHandler ButtonList(i).Click, AddressOf Me.btnTestNumButtons_Click
        Next i
    End Sub

    Private Sub btnTestNumButtons_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Click
        If sender.Tag = "" Then Exit Sub
        Dim valPrim As String = sender.Tag.Substring(0, sender.Tag.IndexOf(" "))
        Dim valSec As String = "000" & valPrim
        valSec = valSec.Substring(valSec.Length - 3)
        txtEdit.Text = txtEdit.Text & "{%" & valSec & "}"
    End Sub
End Class
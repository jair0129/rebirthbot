
Imports System.IO
Imports System.Net

Public Class WebsiteCheck

    Public Event NewsReceived(ByVal msg As List(Of String))
    Public Event ChangelogReceived(ByVal msg As List(Of String))
    Public Event NoUpdate()
    Public Event UpdateAvailable(ByVal URL As String)
    Public Event FileDownloaded(ByVal URL As String, ByVal LocalPath As String)

    Public Sub GrabNews(ByVal URL As String)
        Dim request As WebRequest = WebRequest.Create(URL)
        Dim response As WebResponse = request.GetResponse()
        Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
        Dim str As String = reader.ReadToEnd()

        Dim ret As New List(Of String)

        Do While str.Length > 0
            Application.DoEvents()
            Dim start As Integer = str.IndexOf("***")
            If start >= 0 Then
                str = str.Replace(vbCrLf, "")
                str = str.Substring(start + 3)
                Dim tmp() As String  = (str & "&&").Split("&&")
                For Each k As String In tmp
                    If k IsNot Nothing Then
                        If k <> "" Then ret.Add(k)
                    End If
                Next k
            End If
            str = reader.ReadToEnd()
        Loop
        RaiseEvent NewsReceived(ret)
    End Sub

    Public Sub GrabChangelog(ByVal URL As String)
        Dim request As WebRequest = WebRequest.Create(URL)
        Dim response As WebResponse = request.GetResponse()
        Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
        Dim str As String = reader.ReadToEnd()

        Dim ret As New List(Of String)

        Do While str.Length > 0
            Application.DoEvents()
            Dim start As Integer = str.IndexOf("***")
            If start >= 0 Then
                str = str.Replace(vbCrLf, "")
                str = str.Substring(start + 3)
                Dim tmp() As String = (str & "&&").Split("&&")

                For Each k As String In tmp
                    If k IsNot Nothing Then
                        Dim item As String = k
                        If k.Substring(0, 2) = "%d" Then
                            Dim year As Integer = Integer.Parse(k.Substring(2, 4))
                            Dim month As Integer = Integer.Parse(k.Substring(6, 2))
                            Dim day As Integer = Integer.Parse(k.Substring(8, 2))

                            item = New Date(year, month, day)
                        End If

                        ret.Add(item)
                    End If
                Next k

            End If
            str = reader.ReadToEnd()
        Loop
        RaiseEvent ChangelogReceived(ret)
    End Sub

    Public Sub CheckUpdate()
        Dim request As WebRequest = WebRequest.Create("http://rabbitx86.net/rebirth/versioncheck.php?ver=" & BOT_VERSION)
        Dim response As WebResponse = request.GetResponse()
        Dim reader As StreamReader = New StreamReader(response.GetResponseStream())

        Dim str As String = reader.ReadToEnd()
        Dim ret As String = ""

        Do While str.Length > 0
            Application.DoEvents()
            If str = "0" Then
                RaiseEvent NoUpdate()
                Exit Sub
            Else
                ret &= str
            End If
            str = reader.ReadToEnd()
        Loop

        RaiseEvent UpdateAvailable(ret)
    End Sub

    Public Sub GrabFile(ByVal URL As String)
        Dim filePath As String
        filePath = Application.StartupPath & "\" & URL.Substring(URL.LastIndexOf("/") + 1)
        Dim objResponse As WebResponse = WebRequest.Create(URL).GetResponse()
        Dim ByteBucket(32768) As Byte
        Dim readStream As New BinaryReader(objResponse.GetResponseStream())
        Dim fileToWrite As FileStream = New FileStream(filePath, FileMode.Create, FileAccess.Write)
        Dim currentBytesRead, totalBytesRead As Integer
        Dim done As Boolean = False

        While done = False
            currentBytesRead = readStream.Read(ByteBucket, 0, 32768)
            fileToWrite.Write(ByteBucket, 0, currentBytesRead)
            totalBytesRead += currentBytesRead
            If totalBytesRead = objResponse.ContentLength() Then done = True
        End While
        fileToWrite.Close()
        RaiseEvent FileDownloaded(URL, filePath)
    End Sub

End Class

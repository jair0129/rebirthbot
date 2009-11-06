
Imports System.IO
Imports System.Net

Public Class DownloadFile

    Public Event FileDownloaded(ByVal URL As String, ByVal filePath As String)
    Public Event FileDownloadFailed(ByVal URL As String)

    Public Sub Download(ByVal URL As String, ByVal toPath As String)
        Try
            Dim objResponse As WebResponse = WebRequest.Create(URL).GetResponse()
            Dim ByteBucket(32768) As Byte
            Dim readStream As New BinaryReader(objResponse.GetResponseStream())
            Dim fileToWrite As FileStream = New FileStream(toPath, FileMode.Create, FileAccess.Write)
            Dim currentBytesRead, totalBytesRead As Integer
            Dim done As Boolean = False

            While done = False
                Application.DoEvents()
                currentBytesRead = readStream.Read(ByteBucket, 0, 32768)
                fileToWrite.Write(ByteBucket, 0, currentBytesRead)
                totalBytesRead += currentBytesRead
                If totalBytesRead = objResponse.ContentLength() Then done = True
            End While
            fileToWrite.Close()

            RaiseEvent FileDownloaded(URL, toPath)
        Catch ex As WebException
            RaiseEvent FileDownloadFailed(URL)
        End Try
    End Sub
End Class

Imports MBNCSUtil
Imports MBNCSUtil.Net
Imports MBNCSUtil.Data
Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Net.Dns
Imports System.Environment
Imports System.Net.Sockets
Imports System.Runtime.InteropServices

Public Class BnetFtp

    Private WithEvents fileRequest As BnFtpRequestBase

    Public Event FileDownloadComplete(ByVal filename As String, ByVal filepath As String)
    Public Event DownloadingFile(ByVal filename As String)

    Private m_Server As String
    Private m_Filename As String

    ' for star/sexp/w2bn/d2dv/d2xp transfers
    Public Sub New(ByVal filename As String, ByVal filetime As ULong, ByVal Server As String, ByVal Client As String)
        Dim localPath As String = Application.StartupPath & "\icons\" & Server & "_" & filename
        If File.Exists(localPath) Then
            fileRequest = New BnFtpVersion1Request(Client, filename, File.GetCreationTime(localPath))
        Else
            fileRequest = New BnFtpVersion1Request(Client, filename, Nothing)
        End If
        fileRequest.LocalFileName = localPath
        Me.m_Server = Server
        Me.m_Filename = filename
    End Sub

    ' for war3/w3xp transfers
    Public Sub New(ByVal filename As String, ByVal filetime As ULong, ByVal Server As String, ByVal Client As String, ByVal CDKey As String)
        Dim localPath As String = Application.StartupPath & "\icons\" & Server & "_" & filename
        If File.Exists(localPath) Then
            fileRequest = New BnFtpVersion2Request(Client, filename, File.GetCreationTime(localPath), CDKey)
        Else
            fileRequest = New BnFtpVersion2Request(Client, filename, Nothing, CDKey)
        End If
        fileRequest.LocalFileName = localPath
        fileRequest.Server = Server
        Me.m_Filename = filename
    End Sub

    Public Sub Execute()
        If fileRequest Is Nothing Then Exit Sub
        fileRequest.Server = Me.m_Server
        RaiseEvent DownloadingFile(Me.m_Filename)
        fileRequest.ExecuteRequest()
    End Sub

    Private Sub fileRequest_FileDownloadComplete(ByVal sender As Object, ByVal e As MBNCSUtil.Net.DownloadStatusEventArgs) Handles fileRequest.FileDownloadComplete
        If Not File.Exists(fileRequest.LocalFileName) Then Exit Sub
        Dim check As Long = New FileInfo(fileRequest.LocalFileName).Length

        If e.DownloadStatus() = check Then
            Dim fPath As String
            Dim fName As String

            fPath = fileRequest.LocalFileName.Substring(0, fileRequest.LocalFileName.LastIndexOf("\"))
            fName = fileRequest.LocalFileName.Substring(fileRequest.LocalFileName.LastIndexOf("\") + 1)

            RaiseEvent FileDownloadComplete(fName, fPath)
        End If
    End Sub

    Private Sub fileRequest_FilePartDownloaded(ByVal sender As Object, ByVal e As MBNCSUtil.Net.DownloadStatusEventArgs) Handles fileRequest.FilePartDownloaded
        ' nothing
    End Sub
End Class

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

''' <summary>
''' Handles processing of BNFTP connections.
''' </summary>
''' <remarks>This was rather tricky to get working.  Please don't futz with it.</remarks>
Public Class BnetFtp

    Private WithEvents fileRequest As BnFtpRequestBase

    Public Event FileDownloadComplete(ByVal filename As String, ByVal filepath As String)
    Public Event DownloadingFile(ByVal filename As String)

    Private m_Server As String
    Private m_Filename As String

    ''' <summary>
    ''' Create a new Version 1 request object
    ''' </summary>
    ''' <param name="filename">Name of the filetime to download.</param>
    ''' <param name="filetime">Local filetime of the file</param>
    ''' <param name="Server">Server to download from</param>
    ''' <param name="Client">The client requesting the download</param>
    ''' <remarks>As far as I can tell, client is pretty irrelevant.  The server is important
    ''' since Rebirth is designed specially to work with PvPGN servers, which often have their
    ''' own icons files.</remarks>
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

    ''' <summary>
    ''' Create a new Version 2 request object
    ''' </summary>
    ''' <param name="filename">Name of the filetime to download.</param>
    ''' <param name="filetime">Local filetime of the file</param>
    ''' <param name="Server">Server to download from</param>
    ''' <param name="Client">The client requesting the download</param>
    ''' <param name="CDKey">CD-Key being used in the current connection</param>
    ''' <remarks>The server is important since Rebirth is designed specially to work with
    ''' PvPGN servers, which often have their own icons files.  Version 2 requires that the 
    ''' server verify the CD-Key before a download may begin.  Version 2 is currently only
    ''' seen on WAR3 connections</remarks>
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

    ''' <summary>
    ''' Request the download to commence.
    ''' </summary>
    ''' <remarks>The BnFtpVersionXRequest objects will create the file if it needs to be
    ''' downloaded.</remarks>
    Public Sub Execute()
        If fileRequest Is Nothing Then Exit Sub
        fileRequest.Server = Me.m_Server
        RaiseEvent DownloadingFile(Me.m_Filename)
        fileRequest.ExecuteRequest()
    End Sub

    ''' <summary>
    ''' Notifies the client when the file has finished downloading.
    ''' </summary>
    ''' <param name="sender">A reference to the BnFtpVersionXRequest object passed from MBNCSUtil </param>
    ''' <param name="e">DownloadStatusEventArgs passed from MBNCSUtil</param>
    ''' <remarks>I added this event to MBNCSUtil to make my life easier.  As such, it may not work properly
    ''' and may actually be making my life more difficult.  So far, that does not seem the case.</remarks>
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
End Class

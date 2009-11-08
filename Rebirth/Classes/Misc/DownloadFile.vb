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
Imports System.Net

''' <summary>
''' Small class for downloading binary files.
''' </summary>
Public Class DownloadFile

    Public Event FileDownloaded(ByVal URL As String, ByVal filePath As String)
    Public Event FileDownloadFailed(ByVal URL As String)

    ''' <summary>
    ''' Download a file in binary mode
    ''' </summary>
    ''' <param name="URL">URL of the file to grab</param>
    ''' <param name="toPath">Local path to save the file</param>
    ''' <remarks>This can technically be used to grab webpages as well</remarks>
    Public Sub Download(ByVal URL As String, ByVal toPath As String)
        Try
            Dim objResponse As WebResponse = WebRequest.Create(URL).GetResponse()
            Dim buffer(1048576) As Byte
            Dim readStream As New BinaryReader(objResponse.GetResponseStream())
            Dim fileToWrite As FileStream = New FileStream(toPath, FileMode.Create, FileAccess.Write)
            Dim currentBytesRead, totalBytesRead As Integer
            Dim done As Boolean = False

            While Not done
                Application.DoEvents()
                currentBytesRead = readStream.Read(buffer, 0, 1048576)
                fileToWrite.Write(buffer, 0, currentBytesRead)
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

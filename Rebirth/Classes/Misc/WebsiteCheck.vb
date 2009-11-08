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
''' Class used to check for various things on Rebirth's home site
''' </summary>
''' <remarks>
''' <para>This class is hardcoded to my website.  The files pulled are lists of
''' required files and directories, lists of localization files, updates, and hash files.</para>
''' </remarks>
Public Class WebsiteCheck

    ''' <summary>
    ''' Class used to parse (specifically, at the moment) the localization list file.
    ''' </summary>
    Protected Class WebsiteFilesysParse
        Private pos As Integer
        Private buff As String

        ''' <summary>
        ''' Creates a new parser
        ''' </summary>
        ''' <param name="str">The ÿ (character 0xff) delimited string to parse</param>
        Public Sub New(ByVal str As String)
            buff = str
            pos = 0
        End Sub

        ''' <summary>
        ''' Creates a new parser
        ''' </summary>
        ''' <returns>The next string in the sequence</returns>
        Public Function NextItem() As String
            Dim ret As String = ""

            If pos >= buff.Length() Then Return Nothing

            Dim tmp As String = buff.Substring(pos)
            If tmp = "" Then Return Nothing

            If tmp.IndexOf(Chr(&HFF)) >= tmp.Length() Or tmp.IndexOf(Chr(&HFF)) < 0 Then
                ret = tmp
            Else
                ret = tmp.Substring(0, tmp.IndexOf(Chr(&HFF)))
            End If

            pos += ret.Length() + 1

            Return ret
        End Function
    End Class

    Public Event NewsReceived(ByVal msg As List(Of String))
    Public Event ChangelogReceived(ByVal msg As List(Of String))
    Public Event NoUpdate()
    Public Event UpdateAvailable(ByVal URL As String)
    Public Event FileDownloaded(ByVal URL As String, ByVal LocalPath As String)

    ''' <summary>
    ''' Retrieves the news file from the specified URL.
    ''' </summary>
    ''' <param name="URL">The URL of the news file</param>
    ''' <remarks>
    ''' <para>The URL will be deprecated later.  I will hard code it in as I have done with other
    ''' URL paths.  Also, the text format is pretty shoddy.  I will update this later.</para>
    ''' </remarks>
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
                Dim tmp() As String = (str & "&&").Split("&&")
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

    ''' <summary>
    ''' Retrieves the changelog file from the specified URL.
    ''' </summary>
    ''' <param name="URL">The URL of the news file</param>
    ''' <remarks>
    ''' <para>The URL will be deprecated later.  I will hard code it in as I have done with other
    ''' URL paths.  I will update this later.  This is almost the same as the GrabNews() method,
    ''' although it parses the text for the date item.  May update this to, in fact, use ChateItems.</para>
    ''' </remarks>
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

    ''' <summary>
    ''' Checks the website for updates.
    ''' </summary>
    ''' <remarks>
    ''' <para>The URL is hardcoded here to a specific file, with the current version passed as a parameter.
    ''' The simple structure of the routine could be converted to a function.  I will decide later.</para>
    ''' </remarks>
    Public Sub CheckUpdate()
        Try
            Dim request As WebRequest = WebRequest.Create("http://rabbitx86.net/rebirth/versioncheck.php?ver=" & BOT_VERSION)
            request.Timeout = "5000"
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
        Catch ex As WebException
            Debug.Print("fail")
        End Try
    End Sub

    ''' <summary>
    ''' Download a file from the website and store it, locally, to the same relative path.
    ''' </summary>
    ''' <param name="URL">URL of the file to download</param>
    Public Sub GrabFile(ByVal URL As String, Optional ByVal filePath As String = "")
        If filePath = "" Then filePath = Application.StartupPath & "\" & URL.Substring(URL.LastIndexOf("/") + 1)
        Dim objResponse As WebResponse = WebRequest.Create(URL).GetResponse()
        Dim ByteBucket(32768) As Byte
        Dim readStream As New BinaryReader(objResponse.GetResponseStream())
        Dim fileToWrite As FileStream = New FileStream(filePath, FileMode.Create, FileAccess.Write)
        Dim currentBytesRead, totalBytesRead As Integer
        Dim done As Boolean = False

        While Not done
            currentBytesRead = readStream.Read(ByteBucket, 0, 32768)
            fileToWrite.Write(ByteBucket, 0, currentBytesRead)
            totalBytesRead += currentBytesRead
            If totalBytesRead = objResponse.ContentLength() Then done = True
        End While

        fileToWrite.Close()
        RaiseEvent FileDownloaded(URL, filePath)
    End Sub

    ''' <summary>
    ''' Downloads a list of files and directories used by the current version of Rebirth
    ''' </summary>
    ''' <returns>A list of files and directories, whether they are required, and where they are to be stored</returns>
    ''' <remarks>Friend function so it can be used within the project</remarks>
    Friend Function GrabFileList() As List(Of RebirthFile)
        Dim URL As String = "http://rabbitx86.net/rebirth/" & BOT_VERSION & "/filelist.txt"
        Dim ret As New List(Of RebirthFile)
        Dim request As WebRequest = WebRequest.Create(URL)
        Dim response As WebResponse = request.GetResponse()
        Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
        Dim str As String = reader.ReadToEnd()

        Do While str.Length > 0
            Application.DoEvents()
            Dim tmp() As String = str.Split(vbCrLf)

            For Each k As String In tmp
                If k IsNot Nothing Then
                    k = k.Replace(vbCr, "")
                    k = k.Replace(vbLf, "")
                    If k = "" Then Continue For

                    Dim blarg As New RebirthFile

                    If k.Substring(0, 1) = "T" Then blarg.IsRequired = True
                    If k.Substring(1, 1) = "T" Then blarg.IsDirectory = True

                    k = k.Substring(2)
                    blarg.FilePath = k.Substring(0, k.LastIndexOf("\"))
                    If Not blarg.IsDirectory Then
                        blarg.FileName = k.Substring(k.LastIndexOf("\") + 1)
                    End If

                    ret.Add(blarg)
                End If
            Next k

            str = reader.ReadToEnd()
        Loop

        Return ret
    End Function

    ''' <summary>
    ''' Downloads a list of available localizations
    ''' </summary>
    ''' <returns>A list of languages as: the language code, language name in English, and
    ''' the language name in that language, as well as the name of the colors and localization files.</returns>
    ''' <remarks>Friend function so it can be used within the project.  The means of returns should
    ''' lend itself well later to theme files (if someone wants to make alternate English (US)
    ''' localizations, for instance</remarks>
    Friend Function GetLocalizationList() As List(Of RebirthLocalization)
        Debug.Print("GetLocalizationList() called")
        Dim URL As String = "http://rabbitx86.net/rebirth/" & BOT_VERSION & "/localizations.txt"
        Dim ret As New List(Of RebirthLocalization)
        Dim request As WebRequest = WebRequest.Create(URL)
        Dim response As WebResponse = request.GetResponse()
        Dim reader As StreamReader = New StreamReader(response.GetResponseStream())
        Dim str As String = reader.ReadToEnd()

        Do While str.Length > 0
            Application.DoEvents()
            Dim tmp() As String = str.Split(vbCrLf)

            For Each k As String In tmp
                If k IsNot Nothing Then
                    k = k.Replace(vbCr, "")
                    k = k.Replace(vbLf, "")
                    If k = "" Then Continue For

                    Dim blarg As New RebirthLocalization
                    Dim buff As New WebsiteFilesysParse(k)

                    Dim lcode As String = buff.NextItem()
                    Dim lname As String = buff.NextItem()
                    Dim locname As String = buff.NextItem()
                    Dim colors As String = buff.NextItem()
                    Dim localize As String = buff.NextItem()

                    Debug.Print("Language: [" & lcode & "] " & lname & " (" & locname & ") - " & colors & ";" & localize)

                    blarg.LangItem = New LanguageItem(lname, lcode)
                    blarg.LangItem.LOCALNAME = locname
                    blarg.ColorsFile = colors
                    blarg.LocalizationFile = localize

                    ret.Add(blarg)
                End If
            Next k

            str = reader.ReadToEnd()
        Loop

        Return ret
    End Function

End Class

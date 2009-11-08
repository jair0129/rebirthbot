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

''' <summary>
''' Customized text loaded into memory.
''' </summary>
''' <remarks>ChatItem is a class that stores a list of ChatNodes and handles adding
''' text to RichTextBoxes.</remarks>
Public Class ChatItem
    Private m_Items As List(Of ChatNode)
    Private m_Name As String

    ''' <summary>
    ''' Create a new chat item from a raw string and store its name.
    ''' </summary>
    ''' <param name="raw">raw text string to be parsed</param>
    ''' <param name="name">Name of the event item</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal raw As String, ByVal name As String)
        m_Items = New List(Of ChatNode)
        Dim k() As String
        Dim colorcode As String = ""
        Dim text As String = ""
        m_Name = name

        k = raw.Split("{%")
        For Each item As String In k
            If item = "" Then Continue For

            colorcode = item.Substring(0, 5)
            text = item.Substring(5)

            Dim node As ChatNode
            Dim citem As ColorItem = ParseColorParam(colorcode)
            node = New ChatNode(citem, text)
            m_Items.Add(node)
        Next
    End Sub

    ''' <summary>
    ''' Used for text parts.  No color parsing is done.
    ''' </summary>
    ''' <param name="raw">Raw string.</param>
    ''' <param name="name">Name of the item</param>
    ''' <param name="NoParse">Unused.  It is there simply to differentiate.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal raw As String, ByVal name As String, ByVal NoParse As Boolean)
        m_Items = New List(Of ChatNode)
        m_Name = name

        Dim node As New ChatNode(Color.White, raw)
        m_Items.Add(node)
    End Sub

    ''' <summary>
    ''' Parses each chat node for arguments and replacements and adds the text to the
    ''' supplied RichTextBox
    ''' </summary>
    ''' <param name="RTB">Reference to a RichTextBox object which will have text added to</param>
    ''' <param name="args">A list of arguments to be passed for parsing.  Can be Nothing.</param>
    Public Sub AddText(ByRef RTB As RichTextBox, ByVal ParamArray args() As Object)
        If Me.ToString() = "" Then Exit Sub

        Debug.Print(Me.NAME & " > " & Me.ToString())

        Dim i As Integer

        With RTB
            For Each node In m_Items
                If node.ToString() = "" Then Continue For
                'Application.DoEvents()

                Dim thetext As String = node.TEXTSTRING

                thetext = thetext.Replace("($ts)", Format(Now, "hh:mm:ss"))
                thetext = thetext.Replace("($newline)", vbCrLf)
                thetext = thetext.Replace("($gt)", ">")
                thetext = thetext.Replace("($lt)", "<")

                If args IsNot Nothing Then
                    For i = 0 To args.Length() - 1
                        thetext = thetext.Replace("($arg" & i & ")", args(i).ToString())
                    Next
                End If

                .SelectionStart = .TextLength
                .SelectionLength = 0
                .SelectionColor = node.TEXTCOLOR
                .SelectedText = thetext
                .SelectionStart = .TextLength
                .ScrollToCaret()
            Next node

            .SelectionStart = .TextLength
            .SelectionLength = 0
            .SelectionColor = Color.White
            .SelectedText = vbCrLf
            .SelectionStart = .TextLength
            .ScrollToCaret()

            If Len(.Text) > 10000 Then
                Dim tmp As String = .Rtf

                Dim s As Integer = tmp.IndexOf("\par", 0)
                Dim en As Integer = tmp.IndexOf("\par", s + 5)
                Dim a As String = tmp.Substring(0, s + 5) & "\cf1\f0\fs17"
                Dim b As String = tmp.Substring(en + 5)

                tmp = a & b

                .Rtf = tmp
                .SelectionStart = Len(.Text)
            End If
        End With
    End Sub

    ''' <summary>
    ''' Parses each chat node for arguments and replacements and adds the text to the
    ''' supplied RichTextBox.
    ''' </summary>
    ''' <param name="RTB">Reference to a RichTextBox object which will have text added to</param>
    ''' <param name="args">A list of arguments to be passed for parsing.  Can be Nothing.</param>
    ''' <remarks>Parsing for static replacements happens after arg replacement.  This method
    ''' should never be used for adding user controlled messages to chat, as they could, for
    ''' instance, say "($newline)" repeatedly to mess with the bot's chat window.</remarks>
    Public Sub AddTextParseAfterArgs(ByRef RTB As RichTextBox, ByVal ParamArray args() As Object)
        If Me.ToString() = "" Then Exit Sub

        Debug.Print(Me.NAME & " > " & Me.ToString())

        Dim i As Integer

        With RTB
            For Each node In m_Items
                If node.ToString() = "" Then Continue For
                'Application.DoEvents()

                Dim thetext As String = node.TEXTSTRING

                If args IsNot Nothing Then
                    For i = 0 To args.Length() - 1
                        thetext = thetext.Replace("($arg" & i & ")", args(i).ToString())
                    Next
                End If

                thetext = thetext.Replace("($ts)", Format(Now, "hh:mm:ss"))
                thetext = thetext.Replace("($newline)", vbCrLf)
                thetext = thetext.Replace("($gt)", ">")
                thetext = thetext.Replace("($lt)", "<")

                .SelectionStart = .TextLength
                .SelectionLength = 0
                .SelectionColor = node.TEXTCOLOR
                .SelectedText = thetext
                .SelectionStart = .TextLength
                .ScrollToCaret()
            Next node

            .SelectionStart = .TextLength
            .SelectionLength = 0
            .SelectionColor = Color.White
            .SelectedText = vbCrLf
            .SelectionStart = .TextLength
            .ScrollToCaret()

            If Len(.Text) > 10000 Then
                Dim tmp As String = .Rtf

                Dim s As Integer = tmp.IndexOf("\par", 0)
                Dim en As Integer = tmp.IndexOf("\par", s + 5)
                Dim a As String = tmp.Substring(0, s + 5) & "\cf1\f0\fs17"
                Dim b As String = tmp.Substring(en + 5)

                tmp = a & b

                .Rtf = tmp
                .SelectionStart = Len(.Text)
            End If
        End With
    End Sub

    ''' <summary>
    ''' Retrieve the name of the ChatItem
    ''' </summary>
    Public ReadOnly Property NAME() As String
        Get
            Return m_Name
        End Get
    End Property

    ''' <summary>
    ''' Convert the ChatItem to a string.
    ''' </summary>
    ''' <returns>The string containing all raw ChatNode text.  No colorcodes are present.</returns>
    Public Overrides Function ToString() As String
        Dim ret As String = ""

        For Each item As ChatNode In m_Items
            ret &= item.TEXTSTRING()
        Next item

        Return ret
    End Function
End Class

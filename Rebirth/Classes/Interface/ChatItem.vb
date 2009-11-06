Public Class ChatItem
    Private m_Items As List(Of ChatNode)
    Private m_Name As String

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

    Public Sub AddText(ByRef RTB As RichTextBox, ByVal ParamArray args() As Object)
        If Me.ToString() = "" Then Exit sub

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

    Public ReadOnly Property NAME() As String
        Get
            Return m_Name
        End Get
    End Property

    Public Overrides Function ToString() As String
        Dim ret As String = ""

        For Each item As ChatNode In m_Items
            ret &= item.TEXTSTRING()
        Next item

        Return ret
    End Function
End Class

Public Class BotnetUserlist
    Private Users() As BotnetUser
    Private pos As Long

    Public Sub New()
        ReDim Users(0)
        pos = 0
    End Sub

    Public Function AddUser(ByVal usr As BotnetUser) As Boolean
        If IsUser(usr.BOTID) Then
            Return UpdateUser(usr)
        Else
            Dim i As Integer
            For i = 0 To pos
                If Users(pos) Is Nothing Then
                    Users(pos) = usr
                    Return True
                End If
            Next

            If Users(pos) Is Nothing Then
                ReDim Preserve Users(0 To pos + 1)
                pos += 1
            Else
                If Users(pos).BOTNETNAME <> "" Then
                    ReDim Preserve Users(0 To pos + 1)
                    pos += 1
                End If
            End If

            Users(pos) = usr
            Return True
        End If
        Return False
    End Function

    Public Function UpdateUser(ByVal usr As BotnetUser) As Boolean
        Dim i As Integer
        For i = 0 To pos
            If Not Users(i) Is Nothing Then
                If Users(i).BOTID = usr.BOTID Then
                    Users(i) = usr
                    Return True
                End If
            End If
        Next i
        Return False
    End Function

    Public Function GetBotnetNameByID(ByVal id As Long) As String
        For Each k As BotnetUser In Users
            If Not k Is Nothing Then
                If k.BOTID = id Then
                    Return k.BOTNETNAME
                End If
            End If
        Next k
        Return "<unknown name>"
    End Function

    Public Function GetEntryNameByID(ByVal id As Long) As BotnetUser
        For Each k As BotnetUser In Users
            If Not k Is Nothing Then
                If k.BOTID = id Then
                    Return k
                End If
            End If
        Next k
        Return Nothing
    End Function

    Public Function IsUser(ByVal id As Long) As Boolean
        For Each k As BotnetUser In Users
            If Not k Is Nothing Then
                If k.BOTID = id Then
                    Return True
                End If
            End If
        Next k
        Return False
    End Function

    Public Function RemoveUser(ByVal id As Long) As Boolean
        If Not IsUser(id) Then Return False

        Dim i As Integer
        For i = 0 To pos
            If Not Users(i) Is Nothing Then
                If Users(i).BOTID = id Then
                    Users(i) = Nothing
                    Return True
                End If
            End If
        Next i
        Return False
    End Function
End Class

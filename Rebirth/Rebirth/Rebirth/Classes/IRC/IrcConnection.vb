Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Net.Dns
Imports System.Environment
Imports System.Net.Sockets
Imports System.Runtime.InteropServices

Public Class IrcConnection

    Private sck As Socket

    Private nullTimer As Threading.Timer
    Private queueTimer As Threading.Timer

    Private ipHost As IPHostEntry
    Private ipEnd As IPEndPoint
    Private async As New AsyncCallback(AddressOf IRC_ConnectionCompleted)

#Region "public methods"
    Public Sub Connect()
        'RaiseEvent BNET_ConnectionStarted()
        Debug.Print("irc > connection started")

        sck = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        ipHost = GetHostEntry("irc.dark-alex.org")
        ipEnd = New IPEndPoint(ipHost.AddressList(0), 6667)
        sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 7000)
        sck.BeginConnect(ipEnd, async, sck)
    End Sub

    Public Sub Disconnect(Optional ByVal Silent As Boolean = False)
        nullTimer = Nothing
        If sck Is Nothing Then Exit Sub
        sck.Shutdown(SocketShutdown.Both)
        sck.Close()
        sck = Nothing
        Debug.Print("irc > disconnected")
        'If Not Silent Then RaiseEvent BNET_Disconnected()
    End Sub

    Public Function IsConnected() As Boolean
        Return sck.Connected
    End Function
#End Region

    Private Sub IRC_ConnectionCompleted(ByVal async As IAsyncResult)
        'nullTimer = New Threading.Timer(New Threading.TimerCallback(AddressOf BNET_SID_NULL), Nothing, 180000, 180000)

        Dim asyncnew As New AsyncCallback(AddressOf BNET_DataArrival)

        Dim buff(4096) As Byte
        Dim pck As String
        pck = "NICK rabbit_testa"

        If sck.Connected = True Then
            sck.EndConnect(async)
            sck.BeginReceive(buff, 0, sck.Available, SocketFlags.None, asyncnew, sck)
            sck.Send(pck.GetBytes(), pck.Length(), SocketFlags.None)
        End If
    End Sub

    Private Sub BNET_DataArrival(ByVal async As IAsyncResult)
        If sck Is Nothing Then
            Me.Disconnect()
            Exit Sub
        End If

        If Not sck.Connected() Then
            Me.Disconnect()
            Exit Sub
        End If

        Dim i As Integer
        Dim array As New List(Of Byte)
        Dim pLen As Integer

        Try
            sck.EndReceive(async)

            Dim buff(sck.Available - 1) As Byte
            Dim asyncnew As New AsyncCallback(AddressOf BNET_DataArrival)

            sck.BeginReceive(buff, 0, sck.Available, SocketFlags.None, asyncnew, sck)
            If buff.Length > 0 Then
                For i = 0 To buff.Length - 1
                    array.Add(buff(i))
                Next

                pLen = buff.GetString.IndexOf(vbCrLf) + 1
                Do Until (array.Count = 0)
                    Dim newbuff(4096) As Byte
                    pLen = buff.GetString.IndexOf(vbCrLf) + 1
                    For i = 0 To pLen - 1
                        If i >= array.Count() Then
                            pLen = i
                            Exit For
                        End If
                        newbuff(i) = array(i)
                    Next
                    array.RemoveRange(0, pLen)
                    Debug.Print(newbuff.GetString())
                    'ParsePacket(newbuff)
                Loop
            End If
        Catch ex As Exception
            'RaiseEvent BNET_Exception(ex)
        End Try
    End Sub
End Class

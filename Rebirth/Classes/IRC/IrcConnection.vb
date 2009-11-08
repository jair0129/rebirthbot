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

Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Net.Dns
Imports System.Environment
Imports System.Net.Sockets
Imports System.Runtime.InteropServices

''' <summary>
''' An incomplete implementation of the IRC RFC-1459
''' </summary>
''' <remarks>So in complete I don't care</remarks>
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

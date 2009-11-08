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
''' Descriptor class for the profile of a user on the server
''' </summary>
Public Class ProfileInfo
    Private m_wins As String
    Private m_losses As String
    Private m_disconnects As String
    Private m_winsLadder As String
    Private m_lossesLadder As String
    Private m_disconnectsLadder As String
    Private m_ratingLadder As String
    Private m_location As String
    Private m_description As String

    Public Property WINS() As String
        Get
            Return Me.m_wins
        End Get
        Set(ByVal value As String)
            Me.m_wins = value
        End Set
    End Property

    Public Property LOSSES() As String
        Get
            Return Me.m_losses
        End Get
        Set(ByVal value As String)
            Me.m_losses = value
        End Set
    End Property

    Public Property DISCONNECTS() As String
        Get
            Return Me.m_disconnects
        End Get
        Set(ByVal value As String)
            Me.m_disconnects = value
        End Set
    End Property

    Public Property WINSLADDER() As String
        Get
            Return Me.m_winsLadder
        End Get
        Set(ByVal value As String)
            Me.m_winsLadder = value
        End Set
    End Property

    Public Property LOSSESLADDER() As String
        Get
            Return Me.m_lossesLadder
        End Get
        Set(ByVal value As String)
            Me.m_lossesLadder = value
        End Set
    End Property

    Public Property DISCONNECTSLADDER() As String
        Get
            Return Me.m_disconnectsLadder
        End Get
        Set(ByVal value As String)
            Me.m_disconnectsLadder = value
        End Set
    End Property

    Public Property RATINGLADDER() As String
        Get
            Return Me.m_ratingLadder
        End Get
        Set(ByVal value As String)
            Me.m_ratingLadder = value
        End Set
    End Property

    Public Property LOCATION() As String
        Get
            Return Me.m_location
        End Get
        Set(ByVal value As String)
            Me.m_location = value
        End Set
    End Property

    Public Property DESCRIPTION() As String
        Get
            Return Me.m_description
        End Get
        Set(ByVal value As String)
            Me.m_description = value
        End Set
    End Property

End Class

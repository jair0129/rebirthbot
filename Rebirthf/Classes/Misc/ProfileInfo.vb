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

Public Class BnetConfig
    Private cfg_Username As String
    Private cfg_Password As String
    Private cfg_Server As String
    Private cfg_Client As String
    Private cfg_CDKey As String
    Private cfg_EXPKey As String
    Private cfg_Home As String
    Private cfg_Owner As String

    Public Property USERNAME() As String
        Get
            Return cfg_Username
        End Get
        Set(ByVal Value As String)
            cfg_Username = Value
        End Set
    End Property

    Public Property PASSWORD() As String
        Get
            Return cfg_Password
        End Get
        Set(ByVal Value As String)
            cfg_Password = Value
        End Set
    End Property

    Public Property SERVER() As String
        Get
            Return cfg_Server
        End Get
        Set(ByVal Value As String)
            cfg_Server = Value
        End Set
    End Property

    Public Property CLIENT() As String
        Get
            Return cfg_Client
        End Get
        Set(ByVal Value As String)
            cfg_Client = Value
        End Set
    End Property

    Public Property CDKEY() As String
        Get
            Return cfg_CDKey
        End Get
        Set(ByVal Value As String)
            cfg_CDKey = Value
        End Set
    End Property

    Public Property EXPKEY() As String
        Get
            Return cfg_EXPKey
        End Get
        Set(ByVal Value As String)
            cfg_EXPKey = Value
        End Set
    End Property

    Public Property HOME() As String
        Get
            Return cfg_Home
        End Get
        Set(ByVal Value As String)
            cfg_Home = Value
        End Set
    End Property

    Public Property OWNER() As String
        Get
            Return cfg_Owner
        End Get
        Set(ByVal Value As String)
            cfg_Owner = Value
        End Set
    End Property
End Class

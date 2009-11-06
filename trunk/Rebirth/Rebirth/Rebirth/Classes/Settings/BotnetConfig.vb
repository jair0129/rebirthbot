Public Class BotnetConfig
    Private cfg_Enable As Boolean
    Private cfg_hideServer As Boolean
    Private cfg_hideChannel As Boolean
    Private cfg_hideBnetAccount As Boolean

    Private cfg_Server As String
    Private cfg_Port As Long
    Private cfg_SPassword As String
    Private cfg_DB As String
    Private cfg_DBPassword As String
    Private cfg_Account As String
    Private cfg_APassword As String
    Private cfg_Mask As String

    Public Property ENABLE() As Boolean
        Get
            Return cfg_Enable
        End Get
        Set(ByVal Value As Boolean)
            cfg_Enable = Value
        End Set
    End Property

    Public Property HIDESERVER() As Boolean
        Get
            Return cfg_hideServer
        End Get
        Set(ByVal Value As Boolean)
            cfg_hideServer = Value
        End Set
    End Property

    Public Property HIDECHANNEL() As Boolean
        Get
            Return cfg_hideChannel
        End Get
        Set(ByVal Value As Boolean)
            cfg_hideChannel = Value
        End Set
    End Property

    Public Property HIDEUSERNAME() As Boolean
        Get
            Return cfg_hideBnetAccount
        End Get
        Set(ByVal Value As Boolean)
            cfg_hideBnetAccount = Value
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

    Public Property SERVERPWD() As String
        Get
            Return cfg_SPassword
        End Get
        Set(ByVal Value As String)
            cfg_SPassword = Value
        End Set
    End Property

    Public Property PORT() As Long
        Get
            Return cfg_Port
        End Get
        Set(ByVal Value As Long)
            cfg_Port = Value
        End Set
    End Property

    Public Property DATABASE() As String
        Get
            Return cfg_DB
        End Get
        Set(ByVal Value As String)
            cfg_DB = Value
        End Set
    End Property

    Public Property DATABASEPWD() As String
        Get
            Return cfg_DBPassword
        End Get
        Set(ByVal Value As String)
            cfg_DBPassword = Value
        End Set
    End Property

    Public Property ACCOUNT() As String
        Get
            Return cfg_Account
        End Get
        Set(ByVal Value As String)
            cfg_Account = Value
        End Set
    End Property

    Public Property ACCOUNTPWD() As String
        Get
            Return cfg_APassword
        End Get
        Set(ByVal Value As String)
            cfg_APassword = Value
        End Set
    End Property

    Public Property DBMASK() As String
        Get
            Return cfg_Mask
        End Get
        Set(ByVal Value As String)
            cfg_Mask = Value
        End Set
    End Property
End Class

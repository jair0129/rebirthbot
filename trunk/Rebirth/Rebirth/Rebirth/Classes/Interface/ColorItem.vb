Public Class ColorItem
    Private ci_Val As Color
    Private ci_Text As String
    Private ci_Param As String
    Private ci_Tag As String

    Public Sub New(ByVal Value As Color, ByVal Text As String, ByVal Param As String)
        Me.ci_Val = Value
        Me.ci_Text = Text
        Me.ci_Param = Param
        Me.ci_Tag = ""
    End Sub

    Public Property VALUE() As Color
        Get
            Return ci_Val
        End Get
        Set(ByVal value As Color)
            ci_Val = value
        End Set
    End Property

    Public Property TEXT() As String
        Get
            Return ci_Text
        End Get
        Set(ByVal value As String)
            ci_Text = value
        End Set
    End Property

    Public Property PARAM() As String
        Get
            Return ci_Param
        End Get
        Set(ByVal value As String)
            ci_Param = value
        End Set
    End Property

    Public Property TAG() As String
        Get
            Return ci_Tag
        End Get
        Set(ByVal value As String)
            ci_Tag = value
        End Set
    End Property
End Class

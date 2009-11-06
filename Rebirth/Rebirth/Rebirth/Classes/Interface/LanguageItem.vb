Public Class LanguageItem
    Private m_langName As String
    Private m_langCode As String

    Public Sub New(ByVal LanguageName As String, ByVal LanguageCode As String)
        Me.m_langName = LanguageName
        Me.m_langCode = LanguageCode
    End Sub

    Public ReadOnly Property NAME() As String
        Get
            Return Me.m_langName
        End Get
    End Property

    Public ReadOnly Property CODE() As String
        Get
            Return Me.m_langCode
        End Get
    End Property
End Class

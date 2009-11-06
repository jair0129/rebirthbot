Public Class ChatNode
    Private m_Color As System.Drawing.Color
    Private m_Text As String

    Public Sub New(ByVal cColor As System.Drawing.Color, ByVal Text As String)
        m_Color = cColor
        m_Text = Text
    End Sub

    Public Sub New(ByVal cColor As ColorItem, ByVal Text As String)
        m_Color = cColor.VALUE
        m_Text = Text
    End Sub

    Public ReadOnly Property TEXTCOLOR()
        Get
            Return m_Color
        End Get
    End Property

    Public ReadOnly Property TEXTSTRING()
        Get
            Return m_Text
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return m_Text
    End Function
End Class

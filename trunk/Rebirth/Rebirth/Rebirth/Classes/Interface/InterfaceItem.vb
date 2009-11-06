Public Class InterfaceItem
    Private ci_Fore As ColorItem
    Private ci_Back As ColorItem
    Private ii_ElementName As String
    Private ii_ElementFont As System.Drawing.Font

    Public Sub New(ByVal ElementName As String, ByVal ElementFont As System.Drawing.Font, ByVal ForeColor As ColorItem, ByVal BackColor As ColorItem)
        ii_ElementName = ElementName
        ii_ElementFont = ElementFont
        ci_Fore = ForeColor
        ci_Back = BackColor
    End Sub

    Public Property NAME() As String
        Get
            Return ii_ElementName
        End Get
        Set(ByVal value As String)
            ii_ElementName = value
        End Set
    End Property

    Public Property FONT() As System.Drawing.Font
        Get
            Return ii_ElementFont
        End Get
        Set(ByVal value As System.Drawing.Font)
            ii_ElementFont = value
        End Set
    End Property

    Public Property FORECOLOR() As ColorItem
        Get
            Return ci_Fore
        End Get
        Set(ByVal value As ColorItem)
            ci_Fore = value
        End Set
    End Property

    Public Property BACKCOLOR() As ColorItem
        Get
            Return ci_Back
        End Get
        Set(ByVal value As ColorItem)
            ci_Back = value
        End Set
    End Property

End Class

Public Class COption
    Dim sDescription As String
    Dim cComboBox As ComboBox
    Dim sValues() As String
    Dim sDefaultValue As String
    Dim sCurrentValue As String

    Public Property Description As String
        Get
            Return sDescription
        End Get
        Set(value As String)
            sDescription = value
        End Set
    End Property

    Public Property ComboBox As ComboBox
        Get
            Return cComboBox
        End Get
        Set(value As ComboBox)
            cComboBox = value
        End Set
    End Property

    Public Property Values(idx) As String
        Get
            Return sValues(idx)
        End Get
        Set(value As String)
            sValues(idx) = value
        End Set
    End Property

    Public Property DefaultValue As String
        Get
            Return sDefaultValue
        End Get
        Set(value As String)
            sDefaultValue = value
        End Set
    End Property

    Public Property CurrentValue As String
        Get
            Return sCurrentValue
        End Get
        Set(value As String)
            sCurrentValue = value
        End Set
    End Property
End Class

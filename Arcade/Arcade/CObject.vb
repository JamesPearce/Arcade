Public Class CObject
    Dim pPosition As Point
    Dim sSide As String
    Dim _Button As Button
    Dim _Color As Color
    Dim _Facing As String

    Public Property Button As Button
        Get
            Return _Button
        End Get
        Set(value As Button)
            _Button = value
        End Set
    End Property

    Public Property Position As Point
        Get
            Return pPosition
        End Get
        Set(value As Point)
            pPosition = value
        End Set
    End Property

    Public Property Side As String
        Get
            Return sSide
        End Get
        Set(value As String)
            sSide = value
        End Set
    End Property

    Public Property Color As Color
        Get
            Return _Color
        End Get
        Set(value As Color)
            _Color = value
        End Set
    End Property

    Public Property Facing As String
        Get
            Return _Facing
        End Get
        Set(value As String)
            _Facing = value
        End Set
    End Property



    Shared Sub Object_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs)

    End Sub
End Class
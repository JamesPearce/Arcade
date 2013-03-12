Public Class CObjects
    Dim _List As New List(Of CObject)

    Public Property List As List(Of CObject)
        Get
            Return _List
        End Get
        Set(value As List(Of CObject))
            _List = value
        End Set
    End Property

    Public Sub NewObject(grd As Grid, point As Point, whichSide As String)
        Dim bButton As New Button
        Dim background As New SolidColorBrush
        Dim newObject As New CObject

        newObject.Button = bButton
        newObject.Position = point
        newObject.Side = whichSide

        Select Case newObject.Side
            Case "Blue"
                background.Color = Color.FromRgb(150, 150, 150)
            Case "Red"
                background.Color = Color.FromRgb(200, 200, 200)
        End Select
        newObject.Button.Background = background
        List.Add(newObject)

        AddHandler newObject.Button.MouseLeftButtonUp, AddressOf Object_MouseLeftButtonUp

        Grid.SetColumn(newObject.Button, newObject.Position.X)
        Grid.SetRow(newObject.Button, newObject.Position.Y)
        grd.Children.Add(newObject.Button)
    End Sub

    Public Sub NewObject(grd As Grid, point As Point, color As Color, Optional facing As String = Nothing)
        Dim bButton As New Button
        Dim background As New SolidColorBrush
        Dim newObject As New CObject

        newObject.Button = bButton
        newObject.Position = point
        newObject.Color = color
        newObject.Facing = facing

        background.Color = color
        newObject.Button.Background = background

        List.Add(newObject)

        AddHandler newObject.Button.MouseLeftButtonUp, AddressOf Object_MouseLeftButtonUp

        Grid.SetColumn(newObject.Button, newObject.Position.X)
        Grid.SetRow(newObject.Button, newObject.Position.Y)
        grd.Children.Add(newObject.Button)
    End Sub

    Public Shared Sub Object_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs)

    End Sub

End Class

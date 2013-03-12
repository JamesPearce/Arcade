Public Class WGameMenu

    Private Sub btnQuit_Click(sender As Object, e As RoutedEventArgs) Handles btnQuit.Click
        If MessageBox.Show("Do you really want to quit?", "Quit?", MessageBoxButton.YesNo) Then
            GameProperties = Nothing
            Options = Nothing
            Objects = Nothing
            Me.Close()
        End If
    End Sub

    Private Sub btnOptions_Click(sender As Object, e As RoutedEventArgs) Handles btnOptions.Click
        Dim wOptions As Window
        wOptions = New WOptions
        wOptions.Show()
    End Sub

    Private Sub btnInstuctions_Click(sender As Object, e As RoutedEventArgs) Handles btnInstuctions.Click
        Dim wInstructions As Window
        wInstructions = New WInstructions
        wInstructions.Show()
    End Sub

    Private Sub btnPlay_Click(sender As Object, e As RoutedEventArgs) Handles btnPlay.Click
        Dim wGame As Window

        Select Case GameProperties.NameWithSpaces
            Case "Button Stamp"
                wGame = New WGame
            Case "Snake"
                wGame = New wSnake
        End Select

        wGame.Show()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lblGameName.Content = GameProperties.NameWithSpaces
    End Sub
End Class

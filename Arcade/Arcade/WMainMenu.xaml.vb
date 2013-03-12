Class WMainMenu

    Private Sub StartGame(sender As Object, e As RoutedEventArgs)
        MGameInstance.NewGame(sender.Content)
        NewGame()
    End Sub

    Private Sub NewGame()
        Dim newGame As Window
        newGame = New WGameMenu
        newGame.Show()
    End Sub
End Class

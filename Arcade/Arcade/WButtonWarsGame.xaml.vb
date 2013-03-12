Public Class WButtonWarsGame

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Dim sConfigFilePath As String
        sConfigFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\James\Arcade\Config.txt"
        ' Add any initialization after the InitializeComponent() call.
        SetupGrid()
    End Sub

    Private Sub SetupGrid()
    End Sub

    Private Sub WButtonWarsGame_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If sender.Key.Escape = True Then
            If MessageBox.Show("Really quit?", "Quit?", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                Me.Close()
            End If
        End If
    End Sub
End Class

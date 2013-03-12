Public Class WInstructions

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lblName.Content = GameProperties.NameWithSpaces & " Instructions"
        txtInstructions.Text = GameProperties.Instructions_String

    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class

Public Class WOptions

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lblTitle.Content = GameProperties.NameWithSpaces & " Options"
        SetupAndPopulateGrid()

        ResizeWindow()
    End Sub

    Private Sub SetupAndPopulateGrid()

        For idx = 0 To Options.List.Count - 1
            Dim newRow As New RowDefinition
            Dim lbl As New Label
            grdMain.RowDefinitions.Add(newRow)
            lbl.Content = Options.List(idx).Description
            lbl.HorizontalAlignment = Windows.HorizontalAlignment.Right
            lbl.VerticalAlignment = Windows.VerticalAlignment.Center
            Grid.SetColumn(lbl, 0)
            Grid.SetRow(lbl, idx + 1)
            grdMain.Children.Add(lbl)
            Grid.SetColumn(Options.List.Item(idx).ComboBox, 1)
            Grid.SetRow(Options.List.Item(idx).ComboBox, idx + 1)
            grdMain.Children.Add(Options.List.Item(idx).ComboBox)
            AddHandler Options.List.Item(idx).ComboBox.SelectionChanged, AddressOf ComboBox_SelectionChanged
        Next

        Grid.SetRow(btnSaveAndClose, grdMain.RowDefinitions.Count - 1)
        Grid.SetRow(btnCloseWithoutSaving, grdMain.RowDefinitions.Count - 1)
    End Sub

    Private Sub ResizeWindow()
        Me.Height = (grdMain.RowDefinitions.Count * 45)
    End Sub

    Private Sub btnSaveAndClose_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveAndClose.Click
            If MessageBox.Show("Confirm you wish to save these settings", "Confirm Save", MessageBoxButton.OKCancel) = MessageBoxResult.OK Then
                MOptions.SaveSettings()
            CloseWIndow()
            End If
    End Sub

    Private Sub btnCloseWithoutSaving_Click(sender As Object, e As RoutedEventArgs) Handles btnCloseWithoutSaving.Click
        If MessageBox.Show("Confirm you wish to exit without saving", "Confirm Exit", MessageBoxButton.OKCancel) = MessageBoxResult.OK Then
            CloseWIndow()
        End If
    End Sub

    Private Sub CloseWIndow()
        RemoveOptionControlsFromContainer(grdMain)
        Me.Close()
    End Sub

    Private Sub ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        For idx As Integer = 0 To Options.List.Count - 1
            If Options.List(idx).ComboBox Is sender Then
                Select Case GameProperties.NameWithSpaces
                    Case "Button Stamp"
                        Select Case idx
                            Case 0, 1, 3, 4
                                Options.List(idx).CurrentValue = Options.List(idx).ComboBox.SelectedValue
                            Case 2
                                Options.List(idx).CurrentValue = Options.List(idx).ComboBox.SelectedValue
                        End Select
                    Case "Snake"
                        Select Case idx
                            Case 1, 2
                                Options.List(idx).CurrentValue = Options.List(idx).ComboBox.SelectedValue
                            Case 0
                                Options.List(idx).CurrentValue = Options.List(idx).ComboBox.SelectedValue
                        End Select
                End Select
            End If
        Next
    End Sub

End Class

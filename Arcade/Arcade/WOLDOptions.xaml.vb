Public Class WOLDOptions
    Dim ConfigFolder As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ConfigFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)

        PopulateOptions()
        'If GetCurrentSettings() = False Then
        DefaultSettings()
        'End If

    End Sub

    Private Sub PopulateOptions()


        Dim sdx As Single
        sdx = 0.5

        With cboRatio
            For idx As Integer = 1 To 10
                .Items.Add(idx)
            Next
        End With

        With cboBlueButtons
            For idx As Integer = 1 To 10
                .Items.Add(idx)
            Next
        End With

        With cboDelay
            While Math.Round(sdx, 1) <= 5
                .Items.Add(Math.Round(sdx, 1))
                sdx += 0.1
            End While
        End With

        With cboGridWidth
            For idx As Integer = 3 To 10
                .Items.Add(idx)
            Next
        End With

        With cboGridHeight
            For idx As Integer = 3 To 10
                .Items.Add(idx)
            Next
        End With
    End Sub

    Private Sub SaveAndClose_Click(sender As Object, e As RoutedEventArgs) Handles SaveAndClose.Click
        If MessageBox.Show("Do you want to save settings?", "Confirm Save", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then

            'Below is nested if's to ensure that no errors are produced if an unexpected results is returned (e.g. by Windows 
            If CheckConfigFileExists() = True Then
                If CheckSettingsExist() = False Then
                    CreateNewSettingsInConfigFile()
                Else
                    UpdateSettingsInConfigFile()
                End If
            End If
            Me.Close()
        End If
    End Sub

    Private Function CheckConfigFileExists() As Boolean

        CheckConfigFileExists = False
        'Create config file and folders if they don't exist
        If System.IO.Directory.Exists(ConfigFolder) = True Then
            If System.IO.Directory.Exists(ConfigFolder & "\James") = False Then
                System.IO.Directory.CreateDirectory(ConfigFolder & "\James")
                If System.IO.Directory.Exists(ConfigFolder & "\James\Arcade") = False Then System.IO.Directory.CreateDirectory(ConfigFolder & "\James\Arcade")
            End If
            If System.IO.File.Exists(ConfigFolder & "\James\Arcade\Config.txt") = False Then System.IO.File.Create(ConfigFolder & "\James\Arcade\Config.txt")
            If System.IO.File.Exists(ConfigFolder & "\James\Arcade\Config.txt") = True Then CheckConfigFileExists = True
        End If
    End Function

    Private Function CheckSettingsExist() As Boolean
        Dim sReader As System.IO.StreamReader
        Dim sLine As String
        CheckSettingsExist = False

        sReader = System.IO.File.OpenText(ConfigFolder & "\James\Arcade\Config.txt")
        While sReader.Peek <> -1
            sLine = sReader.ReadLine
            If sLine = "Button Wars" Then CheckSettingsExist = True
        End While
        sReader.Close()
    End Function

    Private Sub CreateNewSettingsInConfigFile()
        Dim sNewConfig(7) As String

        'Add default settings
        sNewConfig(0) = ""
        sNewConfig(1) = "--------------------"
        sNewConfig(2) = "Button Wars"
        sNewConfig(3) = "BWBlueB " & cboBlueButtons.SelectedItem
        sNewConfig(4) = "BWRatio " & cboRatio.SelectedItem
        sNewConfig(5) = "BWDelay " & cboDelay.SelectedItem
        sNewConfig(6) = "BWWidth " & cboGridWidth.SelectedItem
        sNewConfig(7) = "BWHeigh " & cboGridHeight.SelectedItem

        System.IO.File.AppendAllLines(ConfigFolder & "\James\Arcade\Config.txt", sNewConfig)
    End Sub

    Private Sub UpdateSettingsInConfigFile()
        Dim sReader As String()

        sReader = System.IO.File.ReadAllLines(ConfigFolder & "\James\Arcade\Config.txt")

        For idx = 0 To sReader.Count - 1
            Select Case Microsoft.VisualBasic.Strings.Left(sReader(idx), 8)
                Case "BWBlueB "
                    sReader(idx) = Microsoft.VisualBasic.Strings.Left(sReader(idx), 8) & cboBlueButtons.SelectedItem
                Case "BWBRatio "
                    sReader(idx) = Microsoft.VisualBasic.Strings.Left(sReader(idx), 8) & cboRatio.SelectedItem
                Case "BWDelay "
                    sReader(idx) = Microsoft.VisualBasic.Strings.Left(sReader(idx), 8) & cboDelay.SelectedItem
                Case "BWWidth "
                    sReader(idx) = Microsoft.VisualBasic.Strings.Left(sReader(idx), 8) & cboGridWidth.SelectedItem
                Case "BWHeigh "
                    sReader(idx) = Microsoft.VisualBasic.Strings.Left(sReader(idx), 8) & cboGridHeight.SelectedItem
            End Select
        Next
        System.IO.File.WriteAllLines(ConfigFolder & "\James\Arcade\Config.txt", sReader)
    End Sub

    Private Function GetCurrentSettings() As Boolean
        Dim sReader As String()
        Dim iSettingsFound As Integer

        GetCurrentSettings = False
        If System.IO.File.Exists(ConfigFolder & "\James\Arcade\Config.txt") Then
            sReader = System.IO.File.ReadAllLines(ConfigFolder & "\James\Arcade\Config.txt")

            For idx = 0 To sReader.Count - 1
                Select Case Microsoft.VisualBasic.Strings.Left(sReader(idx), 8)
                    Case "BWBlueB "
                        cboBlueButtons.SelectedItem = CType(Microsoft.VisualBasic.Strings.Right(sReader(idx), sReader(idx).Length - 8), Integer)
                        iSettingsFound += 1
                    Case "BWRatio "
                        cboRatio.SelectedItem = CType(Microsoft.VisualBasic.Strings.Right(sReader(idx), sReader(idx).Length - 8), Integer)
                        iSettingsFound += 1
                    Case "BWDelay "
                        cboDelay.SelectedItem = Math.Round(CType(Microsoft.VisualBasic.Strings.Right(sReader(idx), sReader(idx).Length - 8), Single), 1)
                        iSettingsFound += 1
                    Case "BWWidth "
                        cboGridWidth.SelectedItem = CType(Microsoft.VisualBasic.Strings.Right(sReader(idx), sReader(idx).Length - 8), Integer)
                        iSettingsFound += 1
                    Case "BWHeigh "
                        cboGridHeight.SelectedItem = CType(Microsoft.VisualBasic.Strings.Right(sReader(idx), sReader(idx).Length - 8), Integer)
                        iSettingsFound += 1
                End Select
            Next
        End If
        If iSettingsFound = 5 Then GetCurrentSettings = True
    End Function

    Private Sub DefaultSettings()
        cboBlueButtons.SelectedItem = 3
        cboRatio.SelectedItem = 3
        cboDelay.SelectedItem = 2.5
        cboGridWidth.SelectedItem = 6
        cboGridHeight.SelectedItem = 6
    End Sub

    Private Sub CloseWithoutSaving_Click(sender As Object, e As RoutedEventArgs) Handles CloseWithoutSaving.Click
        If MessageBox.Show("Do you want to close this screen without saving settings?", "Confirm Quit", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then Me.Close()
    End Sub
End Class
Module MOptions
    Public Sub ReadConfigFile()
        CheckConfigFileExists()
        If CheckSettingsExist() = False Then
            CreateNewSettingsInConfigFile()
        End If
        SetCurrentValuesFromConfigFile()
        UpdateSettingsToGameProperties()
    End Sub

    Public Sub SaveSettings()
        CheckConfigFileExists()
        If CheckSettingsExist() = False Then
            CreateNewSettingsInConfigFile()
        End If
        UpdateSettingsInConfigFile()
        UpdateSettingsToGameProperties()
    End Sub

    Public Function GetCurrentValue(sPropertyDescription As String) As String
        For idx As Integer = 0 To Options.List.Count - 1
            If Options.List(idx).Description = sPropertyDescription Then
                Return Options.List(idx).CurrentValue
                Exit Function
            End If
        Next
        Return "Default Value"
    End Function

    Private Function CheckConfigFileExists() As Boolean
        CheckConfigFileExists = False
        With GameProperties
            'Create config file and folders if they don't exist
            If System.IO.Directory.Exists(.ConfigLocation) = True Then
                If System.IO.Directory.Exists(.ConfigLocation & "\" & .ConfigFolder) = False Then
                    System.IO.Directory.CreateDirectory(.ConfigLocation & "\" & .ConfigFolder)
                End If
                If System.IO.File.Exists(.ConfigLocation & "\" & .ConfigFolder & "\" & .ConfigFile) = False Then
                    System.IO.File.Create(.ConfigLocation & "\" & .ConfigFolder & "\" & .ConfigFile).Close()
                End If
                If System.IO.File.Exists(.ConfigLocation & "\" & .ConfigFolder & "\" & .ConfigFile) = True Then CheckConfigFileExists = True
            End If
        End With
    End Function

    Private Function CheckSettingsExist() As Boolean
        Dim sReader As System.IO.StreamReader
        Dim sLine As String
        CheckSettingsExist = False

        With GameProperties
            sReader = System.IO.File.OpenText(.ConfigLocation & "\" & .ConfigFolder & "\" & .ConfigFile)
            While sReader.Peek <> -1
                sLine = sReader.ReadLine
                If sLine = GameProperties.NameWithSpaces Then CheckSettingsExist = True
            End While
            sReader.Close()
        End With
    End Function

    Private Sub CreateNewSettingsInConfigFile()
        Dim sNewConfig(7) As String

        'Add default settings
        With GameProperties
            Select .NameWithSpaces
                Case "Button Stamp"
                    sNewConfig(0) = ""
                    sNewConfig(1) = "--------------------"
                    sNewConfig(2) = GameProperties.NameWithSpaces
                    sNewConfig(3) = "BSBlueB = " & Options.List(0).DefaultValue
                    sNewConfig(4) = "BSRedBu = " & Options.List(1).DefaultValue
                    sNewConfig(5) = "BSDelay = " & Options.List(2).DefaultValue
                    sNewConfig(6) = "BSWidth = " & Options.List(3).DefaultValue
                    sNewConfig(7) = "BSHeigh = " & Options.List(4).DefaultValue

                Case "Snake"
                    sNewConfig(0) = ""
                    sNewConfig(1) = "--------------------"
                    sNewConfig(2) = GameProperties.NameWithSpaces
                    sNewConfig(3) = "SnDelay = " & Options.List(0).DefaultValue
                    sNewConfig(4) = "SnWidth = " & Options.List(1).DefaultValue
                    sNewConfig(5) = "SnHeigh = " & Options.List(2).DefaultValue
            End Select
            System.IO.File.AppendAllLines(.ConfigLocation & "\" & .ConfigFolder & "\" & .ConfigFile, sNewConfig)
        End With
    End Sub

    Private Sub UpdateSettingsInConfigFile()
        Dim sReader As String()

        With GameProperties
            sReader = System.IO.File.ReadAllLines(.ConfigLocation & "\" & .ConfigFolder & "\" & .ConfigFile)

            For idx = 0 To sReader.Count - 1
                Select Case .NameWithSpaces
                    Case "Button Stamp"
                        Select Case Microsoft.VisualBasic.Strings.Left(sReader(idx), 8)
                            Case "BSBlueB "
                                sReader(idx) = Microsoft.VisualBasic.Strings.Left(sReader(idx), 10) & Options.List(0).CurrentValue
                            Case "BSRedBu "
                                sReader(idx) = Microsoft.VisualBasic.Strings.Left(sReader(idx), 10) & Options.List(1).CurrentValue
                            Case "BSDelay "
                                sReader(idx) = Microsoft.VisualBasic.Strings.Left(sReader(idx), 10) & Options.List(2).CurrentValue
                            Case "BSWidth "
                                sReader(idx) = Microsoft.VisualBasic.Strings.Left(sReader(idx), 10) & Options.List(3).CurrentValue
                            Case "BSHeigh "
                                sReader(idx) = Microsoft.VisualBasic.Strings.Left(sReader(idx), 10) & Options.List(4).CurrentValue
                        End Select
                    Case "Snake"
                        Select Case Microsoft.VisualBasic.Strings.Left(sReader(idx), 8)
                            Case "SnDelay "
                                sReader(idx) = Microsoft.VisualBasic.Strings.Left(sReader(idx), 10) & Options.List(0).CurrentValue
                            Case "SnWidth "
                                sReader(idx) = Microsoft.VisualBasic.Strings.Left(sReader(idx), 10) & Options.List(1).CurrentValue
                            Case "SnHeigh "
                                sReader(idx) = Microsoft.VisualBasic.Strings.Left(sReader(idx), 10) & Options.List(2).CurrentValue
                        End Select
                End Select
            Next
            System.IO.File.WriteAllLines(.ConfigLocation & "\" & .ConfigFolder & "\" & .ConfigFile, sReader)
        End With
    End Sub

    Private Sub UpdateSettingsToGameProperties()
        With GameProperties
            For idx = 0 To Options.List.Count - 1
                Select Case .NameWithSpaces
                    Case "Button Stamp"
                        Select Case idx
                            Case Options.Dictionary("Number of Blue Buttons")
                                GameProperties.BlueButtons = CType(Options.List(idx).CurrentValue, Integer)
                            Case Options.Dictionary("Number of Red Buttons")
                                GameProperties.RedButtons = CType(Options.List(idx).CurrentValue, Integer)
                            Case Options.Dictionary("Delay between red button moves")
                                GameProperties.Delay = CType(Options.List(idx).CurrentValue, Single)
                            Case Options.Dictionary("Playing area width")
                                GameProperties.Columns = CType(Options.List(idx).CurrentValue, Integer)
                            Case Options.Dictionary("Playing area height")
                                GameProperties.Rows = CType(Options.List(idx).CurrentValue, Integer)
                        End Select
                    Case "Snake"
                        Select Case idx
                            Case Options.Dictionary("Delay between the snake's moves")
                                GameProperties.Delay = CType(Options.List(idx).CurrentValue, Single)
                            Case Options.Dictionary("Playing area width")
                                GameProperties.Columns = CType(Options.List(idx).CurrentValue, Integer)
                            Case Options.Dictionary("Playing area height")
                                GameProperties.Rows = CType(Options.List(idx).CurrentValue, Integer)
                        End Select
                End Select

            Next

        End With
    End Sub

    Private Function SetCurrentValuesFromConfigFile() As Boolean
        Dim sReader As String()
        Dim iSettingsFound As Integer

        With GameProperties
            SetCurrentValuesFromConfigFile = False
            If System.IO.File.Exists(.ConfigLocation & "\" & .ConfigFolder & "\" & .ConfigFile) Then
                sReader = System.IO.File.ReadAllLines(.ConfigLocation & "\" & .ConfigFolder & "\" & .ConfigFile)
                For idx = 0 To sReader.Count - 1
                    Select Case .NameWithSpaces
                        Case "Button Stamp"
                            Select Case Microsoft.VisualBasic.Strings.Left(sReader(idx), 8)
                                Case "BSBlueB "
                                    Options.List(0).CurrentValue = Microsoft.VisualBasic.Strings.Right(sReader(idx), sReader(idx).Length - 10)
                                    iSettingsFound += 1
                                Case "BSRedBu "
                                    Options.List(1).CurrentValue = Microsoft.VisualBasic.Strings.Right(sReader(idx), sReader(idx).Length - 10)
                                    iSettingsFound += 1
                                Case "BSDelay "
                                    Options.List(2).CurrentValue = Microsoft.VisualBasic.Strings.Right(sReader(idx), sReader(idx).Length - 10)
                                    iSettingsFound += 1
                                Case "BSWidth "
                                    Options.List(3).CurrentValue = Microsoft.VisualBasic.Strings.Right(sReader(idx), sReader(idx).Length - 10)
                                    iSettingsFound += 1
                                Case "BSHeigh "
                                    Options.List(4).CurrentValue = Microsoft.VisualBasic.Strings.Right(sReader(idx), sReader(idx).Length - 10)
                                    iSettingsFound += 1
                            End Select

                        Case "Snake"
                            Select Case Microsoft.VisualBasic.Strings.Left(sReader(idx), 8)
                                Case "SnDelay "
                                    Options.List(0).CurrentValue = Microsoft.VisualBasic.Strings.Right(sReader(idx), sReader(idx).Length - 10)
                                    iSettingsFound += 1
                                Case "SnWidth "
                                    Options.List(1).CurrentValue = Microsoft.VisualBasic.Strings.Right(sReader(idx), sReader(idx).Length - 10)
                                    iSettingsFound += 1
                                Case "SnHeigh "
                                    Options.List(2).CurrentValue = Microsoft.VisualBasic.Strings.Right(sReader(idx), sReader(idx).Length - 10)
                                    iSettingsFound += 1
                            End Select
                    End Select
                Next
            End If
            If iSettingsFound = 5 Then SetCurrentValuesFromConfigFile = True
        End With
    End Function

    Public Sub RemoveOptionControlsFromContainer(container As Grid)
        container.Children.Clear()
    End Sub

End Module

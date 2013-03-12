Public Class COptions
    Public _List As List(Of COption)
    Public OptionsEnum As Enumerable
    Public Dictionary As New Dictionary(Of String, Integer)

    Public Property List As List(Of COption)
        Get
            Return _List
        End Get
        Set(value As List(Of COption))
            _List = value
        End Set
    End Property

    Public Sub NewOptions(sGameName As String)
        _List = New List(Of COption)

        Select Case sGameName

            Case "Button Stamp"
                For idx As Integer = 0 To 4
                    Dim newOption As New COption

                    Select Case idx
                        Case 0
                            newOption.Description = "Number of Blue Buttons"
                            Dim BlueB As New ComboBox
                            newOption.ComboBox = BlueB
                            For optValues As Integer = 1 To 10
                                newOption.ComboBox.Items.Add(optValues)
                            Next
                            newOption.DefaultValue = "3"
                            List.Add(newOption)
                            Dictionary.Add(newOption.Description, List.Count - 1)
                        Case 1
                            newOption.Description = "Number of Red Buttons"
                            Dim RedBu As New ComboBox
                            newOption.ComboBox = RedBu
                            For optValues As Integer = 1 To 30
                                newOption.ComboBox.Items.Add(optValues)
                            Next
                            newOption.DefaultValue = "6"
                            List.Add(newOption)
                            Dictionary.Add(newOption.Description, List.Count - 1)
                        Case 2
                            newOption.Description = "Delay between red button moves"
                            Dim Delay As New ComboBox
                            newOption.ComboBox = Delay
                            Dim sdx As Single = 0.1
                            While Math.Round(sdx, 1) <= 5
                                Delay.Items.Add(Math.Round(sdx, 1))
                                sdx += 0.1
                            End While
                            newOption.DefaultValue = "1.5"
                            List.Add(newOption)
                            Dictionary.Add(newOption.Description, List.Count - 1)
                        Case 3
                            newOption.Description = "Playing area width"
                            Dim Width As New ComboBox
                            newOption.ComboBox = Width
                            For optValues As Integer = 2 To 10
                                newOption.ComboBox.Items.Add(optValues)
                            Next
                            newOption.DefaultValue = "5"
                            List.Add(newOption)
                            Dictionary.Add(newOption.Description, List.Count - 1)
                        Case 4
                            newOption.Description = "Playing area height"
                            Dim Heigh As New ComboBox
                            newOption.ComboBox = Heigh
                            For optValues As Integer = 2 To 10
                                newOption.ComboBox.Items.Add(optValues)
                            Next
                            newOption.DefaultValue = "5"
                            List.Add(newOption)
                            Dictionary.Add(newOption.Description, List.Count - 1)
                    End Select
                Next
                List(1).ComboBox.Items(0).GetType()

                ReadConfigFile()
                For idx As Integer = 0 To 4
                    Select Case idx
                        Case 0, 1, 3, 4
                            List(idx).ComboBox.SelectedValue = CType(List(idx).CurrentValue, Integer)
                        Case 2
                            List(idx).ComboBox.SelectedValue = Math.Round(CType(List(idx).CurrentValue, Single), 1)
                    End Select
                Next


            Case "Snake"
                For idx As Integer = 0 To 2
                    Dim newOption As New COption

                    Select Case idx
                        Case 0
                            newOption.Description = "Delay between the snake's moves"
                            Dim Delay As New ComboBox
                            newOption.ComboBox = Delay
                            Dim sdx As Single = 0.1
                            While Math.Round(sdx, 1) <= 5
                                Delay.Items.Add(Math.Round(sdx, 1))
                                sdx += 0.1
                            End While
                            newOption.DefaultValue = "0.5"
                            List.Add(newOption)
                            Dictionary.Add(newOption.Description, List.Count - 1)
                        Case 1
                            newOption.Description = "Playing area width"
                            Dim Width As New ComboBox
                            newOption.ComboBox = Width
                            For optValues As Integer = 2 To 10
                                newOption.ComboBox.Items.Add(optValues)
                            Next
                            newOption.DefaultValue = "5"
                            List.Add(newOption)
                            Dictionary.Add(newOption.Description, List.Count - 1)
                        Case 2
                            newOption.Description = "Playing area height"
                            Dim Heigh As New ComboBox
                            newOption.ComboBox = Heigh
                            For optValues As Integer = 2 To 10
                                newOption.ComboBox.Items.Add(optValues)
                            Next
                            newOption.DefaultValue = "5"
                            List.Add(newOption)
                            Dictionary.Add(newOption.Description, List.Count - 1)
                    End Select
                Next
                List(1).ComboBox.Items(0).GetType()

                ReadConfigFile()
                For idx As Integer = 0 To 2
                    Select Case idx
                        Case 1, 2
                            List(idx).ComboBox.SelectedValue = CType(List(idx).CurrentValue, Integer)
                        Case 0
                            List(idx).ComboBox.SelectedValue = Math.Round(CType(List(idx).CurrentValue, Single), 1)
                    End Select
                Next

        End Select
    End Sub

End Class

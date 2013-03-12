Imports System.Timers

Public Class WGame
    Dim lButtons As List(Of CObjects)
    WithEvents tTimer As Timer
    Dim iPlayerSelectedObject As Integer
    WithEvents bgWorkerTimerEvents As ComponentModel.BackgroundWorker
    Dim bAIMove As Boolean

    Private Sub WGame_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.Key = Key.Escape Then
            If MessageBox.Show("Do you really want to quit?", "Quit?", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then Me.Close()
        End If
    End Sub

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Setupgrid()
        PopulateGridWithWhiteSpace(grdMain)
        PopulateGrid()
        InitialiseTimer()
    End Sub

    Private Sub Setupgrid()
        For iRow As Integer = 0 To GameProperties.Rows - 1
            Dim newRow As New RowDefinition
            grdMain.RowDefinitions.Add(newRow)
        Next

        For iColumn As Integer = 0 To GameProperties.Columns - 1
            Dim newColumn As New ColumnDefinition
            grdMain.ColumnDefinitions.Add(newColumn)
        Next
    End Sub

    Private Sub PopulateGrid()
        'easiest to simply count the number of red buttons left to place, then the number of grid cells left to iterate through minus the number of blue
        ' buttons left to place
        Dim iRedButtonscreated As Integer = 0
        Dim cellsLeftToIterateThrough As Integer = (GameProperties.Columns * GameProperties.Rows)

        For iRow As Integer = 0 To GameProperties.Rows - 1
            For iColumn As Integer = 0 To GameProperties.Columns - 1
                'Red buttons
                If iRedButtonscreated < Options.List(Options.Dictionary("Number of Red Buttons")).CurrentValue Then
                    Dim point As New Point(iColumn, iRow)

                    Objects.NewObject(grdMain, point, "Red")
                    iRedButtonscreated += 1
                End If

                'Blue buttons
                If cellsLeftToIterateThrough <= CType(Options.List(Options.Dictionary("Number of Blue Buttons")).CurrentValue, Integer) Then
                    Dim point As New Point(iColumn, iRow)
                    Dim newObject As New CObjects
                    Objects.NewObject(grdMain, point, "Blue")
                End If

                cellsLeftToIterateThrough -= 1
            Next
        Next
    End Sub

    Private Sub InitialiseTimer()
        tTimer = New Timer
        tTimer.Interval = GameProperties.Delay * 1000
        tTimer.Start()
    End Sub

    Private Sub tTimer_Elapsed(sender As Object, e As ElapsedEventArgs) Handles tTimer.Elapsed
        CheckIfGameOver()
        DetermineRedButtonMoves()
        bAIMove = True
        ASyncEvents()
    End Sub

    Private Sub DetermineRedButtonMoves()
        Dim rand As New Random

        For idx As Integer = 0 To Objects.List.Count - 1
            Dim newPosition As Point
            With Objects.List(idx)
                If .Side = "Red" Then
                    If .Position.X = 0 Then
                        newPosition.X = .Position.X + 1
                    ElseIf .Position.X = GameProperties.Columns - 1 Then
                        newPosition.X = .Position.X - 1
                    Else
                        If rand.Next(2) = 1 Then
                            newPosition.X = .Position.X + 1
                        Else
                            newPosition.X = .Position.X - 1
                        End If
                    End If

                    If .Position.Y = 0 Then
                        newPosition.Y = .Position.Y + 1
                    ElseIf .Position.Y = GameProperties.Rows - 1 Then
                        newPosition.Y = .Position.Y - 1
                    Else
                        If rand.Next(2) = 1 Then
                            newPosition.Y = .Position.Y + 1
                        Else
                            newPosition.Y = .Position.Y - 1
                        End If
                    End If

                    .Position = newPosition
                End If
            End With
        Next
    End Sub

    Private Sub MoveRedButtons()
        For idx As Integer = 0 To Objects.List.Count - 1
            With Objects.List(idx)
                Grid.SetColumn(.Button, .Position.X)
                Grid.SetRow(.Button, .Position.Y)
            End With
        Next
    End Sub

    Private Sub CheckIfGameOver()
        Dim iBlueButtons As Integer
        Dim iRedButtons As Integer

        For idx As Integer = 0 To Objects.List.Count - 1
            If Objects.List(idx).Side = "Blue" Then
                iBlueButtons += 1
            ElseIf Objects.List(idx).Side = "Red" Then
                iRedButtons += 1
            End If
        Next

        If iBlueButtons = 0 Then
            GameOver("Lose")
        ElseIf iRedButtons = 0 Then
            GameOver("Win")
        End If
    End Sub

    Private Sub GameOver(WinOrLose As String)
        tTimer.Stop()
        MessageBox.Show("You " & WinOrLose, WinOrLose, MessageBoxButton.OK)
        Me.Close()
    End Sub

    Private Sub CollisionDetection(Aggressorside As String, VictimsSide As String)
        Dim iObjectsToRemove() As Integer
        Dim bAlreadyMarkedForRemoval As Boolean

        For iAggressor As Integer = 0 To Objects.List.Count - 1
            For iVictim As Integer = 0 To Objects.List.Count - 1
                If Objects.List(iAggressor).Side = Aggressorside Then
                    If Objects.List(iVictim).Side = VictimsSide Then
                        If Objects.List(iAggressor).Position = Objects.List(iVictim).Position Then
                            bAlreadyMarkedForRemoval = False

                            If iObjectsToRemove Is Nothing = False Then
                                For idx As Integer = 0 To iObjectsToRemove.Count - 1
                                    If iObjectsToRemove(idx) = iVictim Then bAlreadyMarkedForRemoval = True
                                Next
                            End If

                            If bAlreadyMarkedForRemoval = False Then
                                If iObjectsToRemove Is Nothing Then
                                    ReDim iObjectsToRemove(0)
                                Else
                                    ReDim Preserve iObjectsToRemove(iObjectsToRemove.Count)
                                End If
                                iObjectsToRemove(iObjectsToRemove.Count - 1) = iVictim
                            End If
                        End If
                    End If
                End If
            Next
        Next
        If iObjectsToRemove Is Nothing = False Then
            For idx As Integer = iObjectsToRemove.Count - 1 To 0 Step -1
                grdMain.Children.Remove(Objects.List(iObjectsToRemove(idx)).Button)
                Objects.List.RemoveAt(iObjectsToRemove(idx))
            Next
        End If
    End Sub

    Private Sub ASyncEvents()
        bgWorkerTimerEvents = New ComponentModel.BackgroundWorker
        bgWorkerTimerEvents.RunWorkerAsync()
    End Sub

    Private Sub bgWorkerTimerEvents_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles bgWorkerTimerEvents.DoWork
            Dispatcher.BeginInvoke(New Action(AddressOf UpdateUI))
    End Sub

    Private Sub UpdateUI()
        If bAIMove = True Then
            MoveRedButtons()
            CollisionDetection("Red", "Blue")
        Else
            MoveBlueButton()
            CollisionDetection("Blue", "Red")
        End If
    End Sub

    Private Sub PopulateGridWithWhiteSpace(grd As Grid)
        Dim scbBrush As New SolidColorBrush
        scbBrush.Color = Color.FromRgb(50, 50, 50)

        For iColumn As Integer = 0 To grd.ColumnDefinitions.Count - 1
            For iRow As Integer = 0 To grd.RowDefinitions.Count - 1
                Dim cnvWhiteSpace As New Canvas

                cnvWhiteSpace.Background = scbBrush
                AddHandler cnvWhiteSpace.MouseLeftButtonUp, AddressOf cnvWhiteSpace_MouseLeftButtonUp
                Grid.SetColumn(cnvWhiteSpace, iColumn)
                Grid.SetRow(cnvWhiteSpace, iRow)
                grd.Children.Add(cnvWhiteSpace)
            Next
        Next
    End Sub

    Private Sub cnvWhiteSpace_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs)
        Dim pClickedCell As Point
        If GameProperties.idxOfCurrentlySelectedObject <> vbNull Then
            If Math.Abs(Grid.GetColumn(sender) - Grid.GetColumn(Objects.List(GameProperties.idxOfCurrentlySelectedObject).Button)) <= 1 Then
                If Math.Abs(Grid.GetRow(sender) - Grid.GetRow(Objects.List(GameProperties.idxOfCurrentlySelectedObject).Button)) <= 1 Then

                    pClickedCell = New Point(Grid.GetColumn(sender), Grid.GetRow(sender))
                    Objects.List(GameProperties.idxOfCurrentlySelectedObject).Position = pClickedCell
                    bAIMove = False
                    ASyncEvents()
                End If
            End If
        End If
    End Sub

    Public Shared Sub Object_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs)
        For idx As Integer = 0 To Objects.List.Count - 1
            If Objects.List(idx).Button Is sender Then
                If Objects.List(idx).Side = "Blue" Then
                    GameProperties.idxOfCurrentlySelectedObject = idx
                End If
            End If
        Next
    End Sub

    Private Sub MoveBlueButton()
        Grid.SetColumn(Objects.List(GameProperties.idxOfCurrentlySelectedObject).Button, Objects.List(GameProperties.idxOfCurrentlySelectedObject).Position.X)
        Grid.SetRow(Objects.List(GameProperties.idxOfCurrentlySelectedObject).Button, Objects.List(GameProperties.idxOfCurrentlySelectedObject).Position.Y)
        GameProperties.idxOfCurrentlySelectedObject = vbNull
    End Sub

End Class

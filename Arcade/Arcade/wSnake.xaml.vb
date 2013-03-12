Imports System.Timers

Public Class wSnake
    Dim lButtons As List(Of CObjects)
    Dim bJustAteFruit As Boolean
    WithEvents tTimer As Timer
    WithEvents bgWorkerTimerEvents As ComponentModel.BackgroundWorker
    WithEvents bgWorkerCloseWindow As ComponentModel.BackgroundWorker

    Private Sub wSnake_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Select Case e.Key
            Case Key.Up
                Objects.List(0).Facing = "N"
            Case Key.Right
                Objects.List(0).Facing = "E"
            Case Key.Down
                Objects.List(0).Facing = "S"
            Case Key.Left
                Objects.List(0).Facing = "W"

            Case Key.Escape
                If MessageBox.Show("Do you really want to quit?", "Quit?", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then Me.Close()
        End Select
    End Sub

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Setupgrid()
        'PopulateGridWithWhiteSpace(grdMain)
        CreateSnake()
        CreateFruit()
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

    Private Sub InitialiseTimer()
        tTimer = New Timer
        tTimer.Interval = GameProperties.Delay * 1000
        tTimer.Start()
    End Sub

    Private Sub tTimer_Elapsed(sender As Object, e As ElapsedEventArgs) Handles tTimer.Elapsed

        DetermineSnakeMove()
        CheckIfGameOver()
        bJustAteFruit = False
        ASyncEvents()
    End Sub

    Private Sub DetermineSnakeMove()
        Dim newPosition As Point
        For idx As Integer = Objects.List.Count - 1 To 1

            With Objects
                If .List(idx).Side = "Snake" Then
                    'If .List(idx - 1).Side = "Snake" Then
                    .List(idx).Position = .List(idx - 1).Position
                    'Else
                    '.List(idx).Position = .List(idx - 2).Position
                    'End If
                End If

                'Move head
                Select Case .List(0).Facing
                    Case "N"
                        newPosition.X = .List(0).Position.X
                        newPosition.Y = .List(0).Position.Y - 1
                    Case "E"
                        newPosition.X = .List(0).Position.X + 1
                        newPosition.Y = .List(0).Position.Y
                    Case "S"
                        newPosition.X = .List(0).Position.X
                        newPosition.Y = .List(0).Position.Y + 1
                    Case "W"
                        newPosition.X = .List(0).Position.X - 1
                        newPosition.Y = .List(0).Position.Y
                End Select
                .List(0).Position = newPosition
            End With
        Next
    End Sub

    Private Sub MoveSnake()
        For idx As Integer = 0 To Objects.List.Count - 1
            With Objects.List(idx)
                Grid.SetColumn(.Button, .Position.X)
                Grid.SetRow(.Button, .Position.Y)
            End With
        Next
    End Sub

    Private Sub CheckIfGameOver()
        'Check if snake head is outside playing area boundaries
        With Objects
            If .List(0).Position.X < 0 Or .List(0).Position.X > GameProperties.Columns Or _
               .List(0).Position.Y < 0 Or .List(0).Position.Y > GameProperties.Rows Then
                GameOver("Lose")
                Exit Sub
            End If

            If bJustAteFruit = False Then
                'check if tail overlaps
                For idx As Integer = 0 To .List().Count - 3
                    For idx2 As Integer = idx + 1 To .List.Count - 2
                        If .List(idx).Position = .List(idx2).Position Then
                            GameOver("Lose")
                            Exit Sub
                        End If
                    Next
                Next
            End If

            'Check if snake fills the playing area
            If .List(0).Position = .List(.List.Count - 1).Position And _
                .List.Count = GameProperties.Columns * GameProperties.Rows Then GameOver("Win")
        End With
    End Sub

    Private Sub GameOver(WinOrLose As String)
        tTimer.Stop()
        tTimer.Dispose()

        MessageBox.Show("You " & WinOrLose, WinOrLose, MessageBoxButton.OK)
        For idx As Integer = Objects.List.Count - 1 To 0 Step -1
            Objects.List.RemoveAt(idx)
        Next
        ASyncCloseWindow()
    End Sub

    Private Sub CollisionDetection()
        'This only determines whether the snake eats the button; see CheckIfGameOver for other collision detection
        If Objects.List(0).Position = Objects.List(Objects.List.Count - 1).Position Then
            Objects.List(Objects.List.Count - 1).Side = "Snake"
            CreateFruit()
            bJustAteFruit = True
        End If
    End Sub

    Private Sub ASyncEvents()
        bgWorkerTimerEvents = New ComponentModel.BackgroundWorker
        bgWorkerTimerEvents.RunWorkerAsync()
    End Sub

    Private Sub bgWorkerTimerEvents_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles bgWorkerTimerEvents.DoWork
        Dispatcher.BeginInvoke(New Action(AddressOf UpdateUI))
    End Sub

    Private Sub ASyncCloseWindow()
        bgWorkerCloseWindow = New ComponentModel.BackgroundWorker
        bgWorkerCloseWindow.RunWorkerAsync()
    End Sub

    Private Sub bgWorkerCloseWindow_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles bgWorkerCloseWindow.DoWork
        Dispatcher.BeginInvoke(New Action(AddressOf CloseWindow))
    End Sub

    Private Sub CloseWindow()
        Me.Close()
    End Sub

    Private Sub UpdateUI()
        MoveSnake()
        CollisionDetection()
    End Sub

    Private Sub CreateFruit()
        Dim rand As New Random
        Dim bSpaceIsFree As Boolean
        Dim iColumn As Integer
        Dim iRow As Integer
        Dim newColor As Color

        'Randomly find a free space
        While bSpaceIsFree = False
            iColumn = rand.Next(0, GameProperties.Columns)
            iRow = rand.Next(0, GameProperties.Rows)
            bSpaceIsFree = True
            For idx As Integer = 0 To Objects.List.Count - 1
                If Objects.List(idx).Position.X = iColumn And Objects.List(idx).Position.Y = iRow Then bSpaceIsFree = False
            Next
        End While

        newColor = Color.FromRgb(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256))
        Dim point As New Point(iColumn, iRow)
        Dim newObject As New CObjects
        Objects.NewObject(grdMain, point, newColor)
    End Sub

    Private Sub CreateSnake()
        Dim rand As New Random
        Dim newColor As Color

        newColor = Color.FromRgb(rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256))
        Dim newPoint As New Point(Math.Round(GameProperties.Columns / 2), Math.Round(GameProperties.Rows / 2))
        Dim newObject As New CObjects
        Objects.NewObject(grdMain, newPoint, newColor, "N")
    End Sub

End Class


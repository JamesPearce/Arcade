Public Class CProperties
    Dim sNameWithSpaces As String
    Dim sNameWithoutSpaces As String
    Dim bInstructions_Boolean As Boolean
    Dim bOptions_Boolean As Boolean
    Dim sInstructions_String As String
    Dim iRows As Integer
    Dim iColumns As Integer
    Dim iBlueButtons As Integer
    Dim iRedButtons As Integer
    Dim sDelay As Single
    Dim _configFolderLocation As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
    Dim _configFolder As String = "Arcade"
    Dim _configFile As String = "Config.txt"
    Dim _idxOfCurrentlySelectedObject As Integer

    Public Sub NewGame(sGamename As String)
        Select Case sGamename
            Case "Button Stamp"
                sNameWithSpaces = "Button Stamp"
                sNameWithoutSpaces = "Button Stamp"

                bInstructions_Boolean = True
                sInstructions_String = "Welcome, Blue Button Leader!" & vbCrLf & _
                                        "The red buttons are invading and it's your task to move your blue buttons on top of them. Click on a blue button to " & _
                                        "select it, then click on any adjacent space to move it there. If there is already a button in the space then " & _
                                        "it will be destroyed: Destroy all red buttons to win." & vbCrLf & _
                                        "Beware though, the red buttons move regularly and if they move to a space that contains a blue button, then the blue " & _
                                        "button will be destroyed. You will lose if all the blue buttons are destroyed." & vbCrLf & vbCrLf & _
                                        "Good luck!"

            Case "Snake"
                sNameWithSpaces = "Snake"
                sNameWithoutSpaces = "Snake"
                bInstructions_Boolean = True
                sInstructions_String = "You never played Snake before?!" & vbCrLf & _
                                        "Eat the buttons to grow, but don't run over your tail!" & _
                                        vbCrLf & vbCrLf & _
                                        "Good luck!"
        End Select
    End Sub

    Public Property idxOfCurrentlySelectedObject
        Get
            Return _idxOfCurrentlySelectedObject
        End Get
        Set(value)
            _idxOfCurrentlySelectedObject = value
        End Set
    End Property

    Public Property ConfigFile As String
        Get
            Return _configFile
        End Get
        Set(value As String)
            _configFile = value
        End Set
    End Property

    Public Property ConfigLocation As String
        Get
            Return _configFolderLocation
        End Get
        Set(value As String)
            _configFolderLocation = value
        End Set
    End Property

    Public Property ConfigFolder As String
        Get
            Return _configFolder
        End Get
        Set(value As String)
            _configFolder = value
        End Set
    End Property

    Public Property Delay As Single
        Get
            Return sDelay
        End Get
        Set(value As Single)
            sDelay = value
        End Set
    End Property

    Public Property RedButtons As Integer
        Get
            Return iRedButtons
        End Get
        Set(value As Integer)
            iRedButtons = value
        End Set
    End Property

    Public Property BlueButtons As Integer
        Get
            Return iBlueButtons
        End Get
        Set(value As Integer)
            iBlueButtons = value
        End Set
    End Property

    Public Property Rows As Integer
        Get
            Return iRows
        End Get
        Set(value As Integer)
            iRows = value
        End Set
    End Property

    Public Property Columns As Integer
        Get
            Return iColumns
        End Get
        Set(value As Integer)
            iColumns = value
        End Set
    End Property

    Public Property Name As String
        Get
            Return sNameWithSpaces
        End Get
        Set(value As String)
            sNameWithSpaces = value
        End Set
    End Property

    Public ReadOnly Property NameWithSpaces As String
        Get
            Return sNameWithSpaces
        End Get
    End Property

    Public ReadOnly Property NameWithoutSpaces As String
        Get
            Return sNameWithoutSpaces
        End Get
    End Property

    Public ReadOnly Property Instructions_Boolean As Boolean
        Get
            Return bInstructions_Boolean
        End Get
    End Property

    Public ReadOnly Property Instructions_String
        Get
            Return sInstructions_String
        End Get
    End Property
End Class
Module MGameInstance
    Public GameProperties As CProperties
    Public Options As COptions
    Public Objects As CObjects

    Public Sub NewGame(sGameName As String)
        GameProperties = New CProperties
        Options = New COptions
        Objects = New CObjects
        GameProperties.NewGame(sGameName)
        Options.NewOptions(sGameName)
    End Sub
End Module
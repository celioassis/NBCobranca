Option Explicit On
Public Class self

    Private aSettings As settings
    Private aAdmDB As ADM.admDB

    Friend ReadOnly Property Settings() As settings
        Get
            Return aSettings
        End Get
        'Set(ByVal Value As Settings)
        '  If Not mySettings Is Nothing Then
        '    mySettings.Dispose()
        '  End If
        '  mySettings = Nothing
        'End Set
    End Property

    Friend ReadOnly Property AdmDB() As ADM.admDB
        Get
            Return aAdmDB
        End Get
    End Property

    Public Sub New()
        aSettings = New settings(Me)
        aAdmDB = New ADM.admDB(Me)
    End Sub

    Public Sub dispose()
        aSettings.Dispose()
        aAdmDB.Dispose()
        aSettings = Nothing
        aAdmDB = Nothing
    End Sub

End Class

Option Explicit On 

Namespace Fachadas

  Public Class Connection

        Private myDB As NBdbm.ADM.admDB
        Private aSelf As self

        Public Sub New(ByRef pSelf As self)
            Me.New(pSelf, pSelf.Settings.Credencial)
        End Sub

        Public Sub New(ByRef pSelf As self, ByVal credencial As tipos.Retorno)
            aSelf = pSelf
            If credencial.Sucesso = True Then
                If aSelf.Settings.Password = NBFuncoes.decripto(credencial.Tag).ToString Then
                    If credencial.ToString = "NBdbm" Then
                        aSelf.Settings.Credencial = credencial
                        Me.myDB = New NBdbm.ADM.admDB(pSelf)
                    End If
                End If
            End If
        End Sub
        Public Sub New(ByRef pSelf As self, ByVal credencial As tipos.Retorno, ByVal TipoConexao As tipos.tiposConection)
            aSelf = pSelf
            If credencial.Sucesso = True Then
                If aSelf.Settings.Password = NBFuncoes.decripto(credencial.Tag).ToString Then
                    If credencial.ToString = "NBdbm" Then
                        aSelf.Settings.Credencial = credencial
                        Me.myDB = New NBdbm.ADM.admDB(aSelf, TipoConexao)
                    End If
                End If
            End If
        End Sub
    Public ReadOnly Property Command(ByVal stringCommand As String) As System.Data.IDbCommand
      Get
        Return myDB.Command(stringCommand)
      End Get
        End Property

    Public ReadOnly Property Connection() As System.Data.IDbConnection
      Get
        Return myDB.ConnectionReader
      End Get
    End Property

    Public ReadOnly Property dataAdapter(ByVal stringCommand As String) As System.Data.IDbDataAdapter
      Get
        Return myDB.dataAdapter(stringCommand)
      End Get
    End Property

    Public ReadOnly Property dataParameter(ByVal stringCommand As String) As System.Data.IDbDataParameter
      Get
        Return myDB.dataParameter(stringCommand)
      End Get
    End Property

    Public Sub Dispose()
      myDB.Dispose()
      myDB = Nothing
    End Sub

  End Class

End Namespace



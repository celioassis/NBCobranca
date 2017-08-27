Imports System
Imports System.Data

Namespace DotNetDataProviderTemplate

    Public Class TemplateTransaction
        Implements IDbTransaction

        Public ReadOnly Property IsolationLevel() As IsolationLevel Implements IDbTransaction.IsolationLevel
            '*
            '* Should return the current transaction isolation
            '* level. For the template, assume the default
            '* which is ReadCommitted.
            '*
            Get
                Return IsolationLevel.ReadCommitted
            End Get
        End Property

        Public Sub Commit() Implements IDbTransaction.Commit
            '*
            '* Implement Commit here. Although the template does
            '* not provide an implementation, it should never be 
            '* a no-op because data corruption could result.
            '*
        End Sub

        Public Sub Rollback() Implements IDbTransaction.Rollback
            '*
            '* Implement Rollback here. Although the template does
            '* not provide an implementation, it should never be
            '* a no-op because data corruption could result.
            '*
        End Sub

        Public ReadOnly Property Connection() As IDbConnection Implements IDbTransaction.Connection
            '
            ' Set and retrieve the connection for the current transaction.
            '

            Get
                Return Nothing
            End Get
        End Property

        Public Sub Dispose() Implements IDisposable.Dispose
            Me.Dispose(True)
            System.GC.SuppressFinalize(Me)
        End Sub

        Private Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If (Not Me.Connection Is Nothing) Then
                    ' implicitly rollback if transaction still valid
                    Me.Rollback()
                End If
            End If
        End Sub

    End Class
End Namespace

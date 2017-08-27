Namespace Manager
    Namespace CamadaDados

        Public Class NBdbm

            Public ReadOnly Property versao() As tipos.Versao
                Get
                    Dim txt As String
                    Dim mVersao As tipos.Versao = New tipos.Versao()
                    txt = System.Configuration.ConfigurationManager.AppSettings.Get("tipo")
                    Return mVersao
                End Get
            End Property
        End Class

        Public Class scriptCTR
            Private aSelf As self

            Public ReadOnly Property versao() As tipos.Versao
                Get
                    versao = New tipos.Versao
                    versao.major = 1
                    versao.minor = 0
                    versao.revision = 10
                End Get
            End Property

            Public Function checkCTR(ByVal versao As tipos.Versao) As tipos.Retorno

                checkCTR = New tipos.Retorno

                '"1.00.010" 
                If aSelf.Settings.Versao.toString < versao.toString Then
                    'Atualizar
                    Beep()
                ElseIf aSelf.Settings.Versao.toString > "1.00.010" Then
                    'Banco mais atualizado que eu:(NBdbm)
                    Beep()
                Else
                    'Banco ok!
                    checkCTR.Sucesso = True
                End If

            End Function

            Private Function runScript() As tipos.Retorno

                Dim TXT As String


                TXT = "create hgfghfhg f"
                Return Nothing
            End Function

            Public Sub New()
                aSelf = New self
            End Sub
        End Class
    End Namespace
End Namespace
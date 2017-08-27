Option Explicit On

Namespace Parsers

    Namespace Finan32

        Public Class parser_Finan32

            Private myCNN As NBdbm.ADM.admDB_OLEDB
            Private DataBase As String

            Public Function Action(ByVal DBQ As String, ByVal SystemDB As String, ByVal xmPath As String) As NBdbm.tipos.Retorno
                Me.DataBase = DBQ
                Dim C As New P_Fachadas.admDB_Finan32(DBQ, SystemDB)
                myCNN = C.cnn

                Call Me.script_run(xmPath)
                Return Nothing
            End Function

            Private ReadOnly Property CNN() As Interfaces.iAdmDB
                Get
                    Return myCNN
                End Get
            End Property

            Private Function script_run(ByVal xmPath As String) As NBdbm.tipos.Retorno
                Dim resultado As New NBdbm.tipos.Retorno
                Dim entidade As New P_Fachadas.Entidade(New self, CNN)
                Dim txt As String
                Dim cReg As String
                Dim I As Double

                cReg = entidade.DataSource.Count.ToString
                'txt = entidade.getFields(12).Count & " campos."
                MsgBox(cReg & " registros à importar.")
                Do
                    'Call entidade.getFields(I)
                    txt = entidade.campos.Key
                    resultado = addSQLEntidade(xmPath, entidade)
                    'If I / 50 = Int(I / 50) Then
                    '  MsgBox(I + 1 & " registros," & vbCrLf & resultado.ToString)
                    'End If
                    I += 1
                Loop Until I = cReg

                resultado.Sucesso = True
                resultado.Tag = "A importação da tabela:[Entidade] com " & cReg & "registros, do banco:" & Me.DataBase & " foi completada com sucesso."
                Return resultado
            End Function

            Private Function addSQLEntidade(ByVal xmPath As String, ByVal field As NBdbm.Parsers.Finan32.P_Fachadas.Entidade) As NBdbm.tipos.Retorno
                Dim resultado As New NBdbm.tipos.Retorno
                Dim oB As New NBdbm.Fachadas.CTR.CadastroEntidade

                Try
                    oB.xmPath_LinkEntNo = xmPath
                    oB.Entidade.PessoaFisica = field.campos.PessoaFisica
                    oB.Entidade.CPFCNPJ_key = field.campos.CPFCNPJ_Key
                    oB.Entidade.NomeRazaoSocial_key = field.campos.NomeRazaoSocial_Key
                    oB.Entidade.ApelidoNomeFantasia = field.campos.ApelidoNomeFantasia
                    oB.Entidade.RgIE = field.campos.RGIE
                    oB.Entidade.OrgaoEmissorIM = "SSP-" & field.campos.UF
                    oB.Entidade.dtNascimentoInicioAtividades = field.campos.dtNascimentoInicioAtividades
                    oB.Entidade.TextoRespeito = field.campos.TextoRespeito

                    oB.Endereco.Logradouro_key = field.campos.Logradouro
                    oB.Endereco.complemento = field.campos.Complemento
                    oB.Endereco.Comentario = field.campos.Comentario
                    oB.Endereco.Bairro = field.campos.Bairro
                    oB.Endereco.Municipio = field.campos.Municipio
                    oB.Endereco.UF = field.campos.UF
                    oB.Endereco.Contato = field.campos.Contato_Empresa
                    oB.Endereco.Principal = False
                    oB.colecaoEnderecos.Add(oB.Endereco.Key, CType(oB.Endereco, Object))

                    If Val(NBdbm.NBFuncoes.soNumero(field.campos.Fone1).ToString) > 0 Then
                        oB.Telefone.Contato = field.campos.Contato_Empresa
                        oB.Telefone.DDD_key = Left(field.campos.Fone1, 4)
                        oB.Telefone.Fone_key = field.campos.Fone1
                        oB.Telefone.Descricao = field.campos.FormaTratamento & "-" & field.campos.Cargo
                        Try
                            oB.colecaoTelefones.Add(oB.Telefone.Key, CType(oB.Telefone, Object))
                        Catch

                        End Try
                    End If

                    If Val(NBdbm.NBFuncoes.soNumero(field.campos.Fone2).ToString) > 0 Then
                        oB.Telefone.Contato = field.campos.Contato_Empresa
                        oB.Telefone.DDD_key = Left(field.campos.Fone2, 4)
                        oB.Telefone.Fone_key = field.campos.Fone2
                        oB.Telefone.Descricao = field.campos.FormaTratamento & "-" & field.campos.Cargo
                        Try
                            oB.colecaoTelefones.Add(oB.Telefone.Key, CType(oB.Telefone, Object))
                        Catch

                        End Try
                    End If

                    If Val(NBdbm.NBFuncoes.soNumero(field.campos.Fone3).ToString) > 0 Then
                        oB.Telefone.Contato = field.campos.Contato_Empresa
                        oB.Telefone.DDD_key = Left(field.campos.Fone3, 4)
                        oB.Telefone.Fone_key = field.campos.Fone3
                        oB.Telefone.Descricao = field.campos.FormaTratamento & "-" & field.campos.Cargo
                        Try
                            oB.colecaoTelefones.Add(oB.Telefone.Key, CType(oB.Telefone, Object))
                        Catch

                        End Try
                    End If

                    If Val(NBdbm.NBFuncoes.soNumero(field.campos.Fone4).ToString) > 0 Then
                        oB.Telefone.Contato = field.campos.Contato_Empresa
                        oB.Telefone.DDD_key = Left(field.campos.Fone4, 4)
                        oB.Telefone.Fone_key = field.campos.Fone4
                        oB.Telefone.Descricao = field.campos.FormaTratamento & "-" & field.campos.Cargo
                        Try
                            oB.colecaoTelefones.Add(oB.Telefone.Key, CType(oB.Telefone, Object))
                        Catch

                        End Try
                    End If

                    If InStr(field.campos.Comentario, "@") > 1 Then
                        oB.eMail.eMail_key = field.campos.Comentario
                        oB.colecaoEmail.Add(oB.eMail.Key, CType(oB.eMail, Object))
                    End If

                    If InStr(field.campos.Comentario, "www") > 1 Or InStr(field.campos.Comentario, "http") > 1 Then
                        oB.Url.Url_key = field.campos.Comentario

                        oB.colecaoUrl.Add(oB.Url.Key, CType(oB.Url, Object))
                    End If

                    oB.Salvar(False)
                Catch ex As Exception
                    Beep()
                End Try

                Return resultado
            End Function

            'Private Function script_run_teste() As NBdbm.tipos.Retorno
            '    Dim txt As String = ""
            '    txt = CNN.sqlListaCampos("txt")
            '    MsgBox("Olá mundo!")
            '    Dim db As New NBdbm.Fachadas.allClass("Usuarios", CNN)

            '    txt = db.getFields.Item(1).caption & db.getFields.Item(1).value
            '    txt = db.getFields.Item(2).caption & db.getFields.Item(2).value
            '    txt = db.getFields.Item(3).caption & db.getFields.Item(3).value

            '    txt = ""
            '    Return Nothing
            'End Function

        End Class

    End Namespace
End Namespace

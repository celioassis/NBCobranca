Namespace Parsers

    Namespace Finan32

        Namespace P_Fachadas

            Public Class admDB_Finan32

                Private mCNN As ADM.admDB_OLEDB
                Private DBQ As String
                Private SystemDB As String

                Public Sub New(ByVal DBQ As String, ByVal SystemDB As String)
                    'Public Function Action(ByVal DBQ As String, ByVal SystemDB As String) As NBdbm.tipos.Retorno

                    Me.DBQ = DBQ
                    Me.SystemDB = SystemDB

                End Sub

                Friend ReadOnly Property cnn() As ADM.admDB_OLEDB
                    Get
                        If mCNN Is Nothing Then
                            'este não funciona! "Driver={Microsoft Access Driver (*.mdb)}; Dbq=" & DBQ & "; SystemDB=" & SystemDB & ";"
                            'mCNN.connection.ConnectionString = "Provider=SQLOLEDB.1;database=" & DBQ & ";User ID=ProSystem_;Password=nitromate;SystemDB=" & SystemDB
                            'cnnSTR = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DBQ
                            Dim cnnSTR As New NBdbm.ADM.stringConnection(New self)
                            Call cnnSTR.ConnProperty.Add("Provider", "Provider=Microsoft.Jet.OLEDB.4.0", System.Type.GetType("System.String"), True)
                            Call cnnSTR.ConnProperty.Add("DataSource", "Data Source=" & DBQ, System.Type.GetType("System.String"), True)
                            'Call cnnSTR.ConnProperty.Add("Login", "User ID=login", System.Type.GetType("System.String"), True)
                            'Call cnnSTR.ConnProperty.Add("Senha", "Password=senha", System.Type.GetType("System.String"), True)
                            'Call cnnSTR.ConnProperty.Add("SystemDB", "SystemDB=" & SystemDB, System.Type.GetType("System.String"), True)
                            mCNN = New ADM.admDB_OLEDB(New self, cnnSTR.StringConnection)
                        End If
                        Return mCNN
                    End Get
                End Property
            End Class

            '===[ Entidade ]==================================================================================================
            'Off - Edgar em construção
            Public Class Entidade
                Inherits NBdbm.Fachadas.allClass

                'Private admDB As Interfaces.iAdmDB
                Private mCampos As EntidadeCampos

                Public Sub New(ByRef pSelf As self, ByVal cnn As Interfaces.iAdmDB)
                    MyBase.New(pSelf, "Cadastros de Entidades", cnn)
                    mCampos = New EntidadeCampos(Me)
                End Sub

                Public Property campos() As EntidadeCampos
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As EntidadeCampos)
                        mCampos = Value
                        'Atenção: Quando vc chama uma property ou uma function
                        'E não atribui o seu retorno, o value vem nothing mesmo, 
                        'isso é normal, não invalida a função que apenas se comporta
                        'momentaneamente como uma 'sub'
                        If Not Value Is Nothing Then
                            MyBase.toObject = mCampos.Parent
                        End If
                    End Set
                End Property

                Public Class EntidadeCampos

                    Private mParent As NBdbm.Fachadas.allClass

                    Public Sub New(ByRef allClass As NBdbm.Fachadas.allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As NBdbm.Fachadas.allClass
                        Get
                            Return mParent
                        End Get
                        Set(ByVal Value As NBdbm.Fachadas.allClass)
                            mParent = Value
                        End Set
                    End Property
                    Public Sub Dispose()
                        Parent = Nothing
                    End Sub
                    Public Function salvar(ByVal noCommit As Boolean) As tipos.Retorno
                        'Implentar:
                        'Aqui será feita a verificação do me.id = null para inserir ou caso
                        'contrário será realizado uma edição
                        Dim retorno As New tipos.Retorno
                        'Parent.var("dtAlteracao").Dirty = True
                        'Parent.var("dtAlteracao").value = CDate(Now).ToString(self.Settings.sintaxeData)
                        'Parent.filterWhere = " idCPFCNPJ = " & Parent.var("idCPFCNPJ").value & " and NomePrimary = '" & Me.NomeRazaoSocial_Key & "' "
                        'retorno = Parent.editar(noCommit)
                        'retorno.Objeto = "Editado "
                        'If retorno.Sucesso = False Then
                        '  Parent.var("dtCriacao").Dirty = True
                        '  Parent.var("dtCriacao").value = CDate(Now).ToString(self.Settings.sintaxeData)
                        '  retorno = Parent.inserir(noCommit)
                        '  retorno.Objeto = "Inserido "
                        'End If
                        'retorno.Objeto = retorno.Objeto & " | CTRL_Entidade | Reg:" & Me.ID.ToString
                        Return retorno
                    End Function
                    Public Sub Clear_filters()
                        Parent.Clear_filters()
                    End Sub
                    Public Sub Clear_vars()
                        Parent.Clear_vars()
                    End Sub

#Region "   Propriedades - Fields   "

                    'SELECT 
                    '[Cadastros de entidades].IdFix
                    '[Cadastros de entidades].Id 
                    '[Cadastros de entidades].CPF
                    '[Cadastros de entidades].RG
                    '[Cadastros de entidades].Nome 
                    '[Cadastros de entidades].[Conhecido como]
                    '[Cadastros de entidades].Cargo
                    '[Cadastros de entidades].Empresa
                    '[Cadastros de entidades].[Forma de tratamento]
                    '[Cadastros de entidades].[Data de nascimento]
                    '[Cadastros de entidades].Atividade
                    '[Cadastros de entidades].Endereço
                    '[Cadastros de entidades].Complemento
                    '[Cadastros de entidades].Bairro
                    '[Cadastros de entidades].Cidade
                    '[Cadastros de entidades].UF
                    '[Cadastros de entidades].Cep
                    '[Cadastros de entidades].Coment
                    '[Cadastros de entidades].Mais
                    '[Cadastros de entidades].Classificação
                    '[Cadastros de entidades].[Sub-classificação]
                    '[Cadastros de entidades].P1
                    '[Cadastros de entidades].P2
                    '[Cadastros de entidades].P3
                    '[Cadastros de entidades].P4
                    '[Cadastros de entidades].P5
                    '[Cadastros de entidades].P6
                    '[Cadastros de entidades].N1
                    '[Cadastros de entidades].N2
                    '[Cadastros de entidades].N3
                    '[Cadastros de entidades].N4
                    '[Cadastros de entidades].N5
                    '[Cadastros de entidades].N6
                    '[Cadastros de entidades].Fone1
                    '[Cadastros de entidades].Fone2
                    '[Cadastros de entidades].Fone3
                    '[Cadastros de entidades].Fone4
                    '[Cadastros de entidades].Fornecedor
                    '[Cadastros de entidades].Cliente
                    '[Cadastros de entidades].Outros
                    '[Cadastros de entidades].[Pessoa Juridica]
                    '[Cadastros de entidades].[Prazo de carencia em dias]
                    '[Cadastros de entidades].[Multa por atraso]
                    '[Cadastros de entidades].[Juro mensal sobre inadimplencia]
                    '[Cadastros de entidades].[Data de criação]
                    '[Cadastros de entidades].[Ultima alteração]
                    '[Cadastros de entidades].Foto
                    '[Cadastros de entidades].Versao
                    '[Cadastros de entidades].Rep_Atu
                    'FROM [Cadastros de entidades];

                    Public ReadOnly Property ID() As Integer
                        Get
                            Return Parent.var("ID").value
                        End Get
                    End Property
                    Public ReadOnly Property Key() As String
                        Get
                            Return Me.CPFCNPJ_Key.ToString & Me.NomeRazaoSocial_Key.ToString
                        End Get
                    End Property
                    Public ReadOnly Property CPFCNPJ_Key() As String
                        Get
                            Return Parent.var("CPF").value
                        End Get
                    End Property
                    Public ReadOnly Property RGIE() As String
                        Get
                            Return Parent.var("RG").value
                        End Get
                    End Property
                    Public ReadOnly Property NomeRazaoSocial_Key() As String
                        Get
                            Return Parent.var("nome").value
                        End Get
                    End Property
                    Public ReadOnly Property ApelidoNomeFantasia() As String
                        Get
                            Return Parent.var("Conhecido como").value
                        End Get
                    End Property
                    Public ReadOnly Property Cargo() As String
                        Get
                            Return Parent.var("cargo").value
                        End Get
                    End Property
                    Public ReadOnly Property Contato_Empresa() As String
                        Get
                            Return Parent.var("Empresa").value
                        End Get
                    End Property
                    Public ReadOnly Property FormaTratamento() As String
                        Get
                            Return Parent.var("Forma de Tratamento").value
                        End Get
                    End Property
                    Public ReadOnly Property dtNascimentoInicioAtividades() As Date
                        Get
                            Dim d As Date
                            Try
                                d = CDate(Parent.var("Data de Nascimento").value)
                            Catch ex As Exception
                                d = Now
                            End Try
                            Return d
                        End Get
                    End Property
                    Public ReadOnly Property Atividade() As String
                        Get
                            Return Parent.var("Atividade").value
                        End Get
                    End Property
                    Public ReadOnly Property Logradouro() As String
                        Get
                            Return Parent.var("Endereço").value
                        End Get
                    End Property
                    Public ReadOnly Property Complemento() As String
                        Get
                            Return Parent.var("Complemento").value
                        End Get
                    End Property
                    Public ReadOnly Property Bairro() As String
                        Get
                            Return Parent.var("Bairro").value
                        End Get
                    End Property
                    Public ReadOnly Property Municipio() As String
                        Get
                            Return Parent.var("Cidade").value
                        End Get
                    End Property
                    Public ReadOnly Property UF() As String
                        Get
                            Return Parent.var("UF").value
                        End Get
                    End Property
                    Public ReadOnly Property Cep() As String
                        Get
                            Return Parent.var("CEP").value
                        End Get
                    End Property
                    Public ReadOnly Property Comentario() As String
                        Get
                            Return Parent.var("Coment").value
                        End Get
                    End Property
                    Public ReadOnly Property TextoRespeito() As String
                        Get
                            Return Parent.var("Mais").value
                        End Get
                    End Property
                    Public ReadOnly Property Classificacao() As String
                        Get
                            Return Parent.var("Classificação").value
                        End Get
                    End Property
                    Public ReadOnly Property SubClassificacao() As String
                        Get
                            Return Parent.var("Sub-Classificação").value
                        End Get
                    End Property
                    Public ReadOnly Property P1() As String
                        Get
                            Return Parent.var("P1").value
                        End Get
                    End Property
                    Public ReadOnly Property P2() As String
                        Get
                            Return Parent.var("P2").value
                        End Get
                    End Property
                    Public ReadOnly Property P3() As String
                        Get
                            Return Parent.var("P3").value
                        End Get
                    End Property
                    Public ReadOnly Property P4() As String
                        Get
                            Return Parent.var("P4").value
                        End Get
                    End Property
                    Public ReadOnly Property P5() As String
                        Get
                            Return Parent.var("P5").value
                        End Get
                    End Property
                    Public ReadOnly Property P6() As String
                        Get
                            Return Parent.var("P6").value
                        End Get
                    End Property
                    Public ReadOnly Property N1() As String
                        Get
                            Return Parent.var("N1").value
                        End Get
                    End Property
                    Public ReadOnly Property N2() As String
                        Get
                            Return Parent.var("N2").value
                        End Get
                    End Property
                    Public ReadOnly Property N3() As String
                        Get
                            Return Parent.var("N3").value
                        End Get
                    End Property
                    Public ReadOnly Property N4() As String
                        Get
                            Return Parent.var("N4").value
                        End Get
                    End Property
                    Public ReadOnly Property N5() As String
                        Get
                            Return Parent.var("N5").value
                        End Get
                    End Property
                    Public ReadOnly Property N6() As String
                        Get
                            Return Parent.var("N6").value
                        End Get
                    End Property
                    Public ReadOnly Property Fone1() As String
                        Get
                            Return Parent.var("Fone1").value
                        End Get
                    End Property
                    Public ReadOnly Property Fone2() As String
                        Get
                            Return Parent.var("Fone2").value
                        End Get
                    End Property
                    Public ReadOnly Property Fone3() As String
                        Get
                            Return Parent.var("Fone3").value
                        End Get
                    End Property
                    Public ReadOnly Property Fone4() As String
                        Get
                            Return Parent.var("Fone4").value
                        End Get
                    End Property
                    Public ReadOnly Property Fornecedor() As String
                        Get
                            Return Parent.var("Fornecedor").value
                        End Get
                    End Property
                    Public ReadOnly Property Cliente() As String
                        Get
                            Return Parent.var("Cliente").value
                        End Get
                    End Property
                    Public ReadOnly Property Outros() As String
                        Get
                            Return Parent.var("Outros").value
                        End Get
                    End Property
                    Public ReadOnly Property PessoaFisica() As Boolean
                        Get
                            Dim pF As Boolean
                            If Parent.var("Pessoa Juridica").value = False Then
                                pF = True
                            End If
                            Return pF
                        End Get
                    End Property
                    Public ReadOnly Property PrazoCarencia() As String
                        Get
                            Return Parent.var("Prazo de carencia em dias").value
                        End Get
                    End Property
                    Public ReadOnly Property MultaAtraso() As String
                        Get
                            Return Parent.var("Multa por atraso").value
                        End Get
                    End Property
                    Public ReadOnly Property JuroMensal() As String
                        Get
                            Return Parent.var("Juro mensal sobre inadimplencia").value
                        End Get
                    End Property
                    Public ReadOnly Property dtCriacao() As Date
                        Get
                            'CONVERT(DATETIME, '12/08/1973',102)
                            Return CDate(Parent.var("Data de Criação").value).ToString(Parent.Self.Settings.sintaxeData)
                        End Get
                    End Property
                    Public ReadOnly Property dtAlteracao() As Date
                        Get
                            'parent.var("dtAlteracao").Value = CDate(Now).ToString(self.Settings.sintaxeData)
                            Return CDate(Parent.var("ultima alteração").value).ToString(Parent.Self.Settings.sintaxeData)
                        End Get
                    End Property
                    Public ReadOnly Property Foto() As Byte()
                        Get
                            Return Parent.var("foto").value
                        End Get
                    End Property
#End Region

                End Class
            End Class

        End Namespace

    End Namespace
End Namespace
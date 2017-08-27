Option Explicit On

Namespace Fachadas

    Namespace CTR

        Public Class CadastroEntidade
            Implements Interfaces.iCTR.iCadastroEntidade

            Protected aSource As String
            Protected aColTelefone As New Fachadas.NbCollection
            Protected aColEndereco As New Fachadas.NbCollection
            Protected aColEmail As New Fachadas.NbCollection
            Protected aColUrl As New Fachadas.NbCollection
            Protected aLinkNo As Fachadas.CTR.primitivas.LinkEntidadeNo
            Protected aEntidade As Fachadas.CTR.primitivas.Entidade
            Protected aEndereco As Fachadas.CTR.primitivas.Endereco
            Protected aTelefone As Fachadas.CTR.primitivas.Telefone
            Protected aEMail As Fachadas.CTR.primitivas.eMail
            Protected aUrl As Fachadas.CTR.primitivas.Url
            Protected aTipoConexao As tipos.tiposConection
            Protected aSelf As self
            Private xmPath_EntNo As String
            Private aEntidadeFields As Fields
            Private aEnderecoFields As Fields
            Private aTelefoneFields As Fields
            Private aEmailFields As Fields
            Private aURLFields As Fields
            Private aLinkNoFields As Fields


            Public Sub New()
                aSelf = New self
                aEntidade = New Fachadas.CTR.primitivas.Entidade(aSelf)
                aEndereco = New Fachadas.CTR.primitivas.Endereco(aself)
                aTelefone = New Fachadas.CTR.primitivas.Telefone(aSelf)
                aEMail = New Fachadas.CTR.primitivas.eMail(aSelf)
                aUrl = New Fachadas.CTR.primitivas.Url(aSelf)
                aLinkNo = New Fachadas.CTR.primitivas.LinkEntidadeNo(aSelf)
                Me.InicializaFields()
            End Sub
            Public Sub New(ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                aSelf = New self
                aTipoConexao = TipoConexao
                aEntidade = New Fachadas.CTR.primitivas.Entidade(aSelf, TipoConexao, True)
                aEndereco = New Fachadas.CTR.primitivas.Endereco(aSelf, TipoConexao, True)
                aTelefone = New Fachadas.CTR.primitivas.Telefone(aSelf, TipoConexao, True)
                aEMail = New Fachadas.CTR.primitivas.eMail(aSelf, TipoConexao, True)
                aUrl = New Fachadas.CTR.primitivas.Url(aSelf, TipoConexao, True)
                aLinkNo = New Fachadas.CTR.primitivas.LinkEntidadeNo(aSelf, TipoConexao, pManterConexaoAberta)
                Me.InicializaFields()
            End Sub

            Private Sub InicializaFields()
                aEntidadeFields = aEntidade.var
                aEnderecoFields = aEndereco.var
                aTelefoneFields = aTelefone.var
                aEmailFields = aEMail.var
                aURLFields = aUrl.var
                aLinkNoFields = aLinkNo.var
            End Sub

            Public Overridable Sub Dispose() Implements System.IDisposable.Dispose
                If Not IsNothing(aColEndereco) Then aColEndereco.Dispose() : aColEndereco = Nothing
                If Not IsNothing(aColTelefone) Then aColTelefone.Dispose() : aColTelefone = Nothing
                If Not IsNothing(aColEmail) Then aColEmail.Dispose() : aColEmail = Nothing
                If Not IsNothing(aColUrl) Then aColUrl.Dispose() : aColUrl = Nothing
                If Not IsNothing(aEntidade) Then aEntidade.Dispose() : aEntidade = Nothing
                If Not IsNothing(aEndereco) Then aEndereco.Dispose() : aEndereco = Nothing
                If Not IsNothing(aTelefone) Then aTelefone.Dispose() : aTelefone = Nothing
                If Not IsNothing(aEMail) Then aEMail.Dispose() : aEMail = Nothing
                If Not IsNothing(aUrl) Then aUrl.Dispose() : aUrl = Nothing
                If Not IsNothing(aLinkNo) Then aLinkNo.Dispose() : aLinkNo = Nothing

                aTipoConexao = Nothing
                xmPath_EntNo = Nothing
                If Not IsNothing(aEntidadeFields) Then aEntidadeFields.Dispose() : aEntidadeFields = Nothing
                If Not IsNothing(aEnderecoFields) Then aEnderecoFields.Dispose() : aEnderecoFields = Nothing
                If Not IsNothing(aTelefoneFields) Then aTelefoneFields.Dispose() : aTelefoneFields = Nothing
                If Not IsNothing(aEmailFields) Then aEmailFields.Dispose() : aEmailFields = Nothing
                If Not IsNothing(aURLFields) Then aURLFields.Dispose() : aURLFields = Nothing
                If Not IsNothing(aLinkNoFields) Then aLinkNoFields.Dispose() : aLinkNoFields = Nothing

            End Sub

            Public ReadOnly Property colecaoTelefones() As NbCollection
                Get
                    Return aColTelefone
                End Get
            End Property
            Public ReadOnly Property colecaoEnderecos() As NbCollection
                Get
                    Return aColEndereco
                End Get
            End Property
            Public ReadOnly Property colecaoEmail() As NbCollection
                Get
                    Return aColEmail
                End Get
            End Property
            Public ReadOnly Property colecaoUrl() As NbCollection
                Get
                    Return aColUrl
                End Get
            End Property

            Protected Sub LimparColecoes()
                aColEmail.Clear()
                aColEndereco.Clear()
                aColTelefone.Clear()
                aColUrl.Clear()
            End Sub

            Public Function EstaEmMuitosNos(ByVal ExcluirClasseAtual As Boolean) As Boolean
                Dim obNo As primitivas.No
                Dim obLinkNo As Fachadas.CTR.primitivas.LinkEntidadeNo

                'Localizando o Nó pelo xmPath
                'Dim idNo As Integer
                'Verifica se foi usado outro tipo de conexao que não a padrao
                If Not IsNothing(aTipoConexao) Then
                    obNo = New primitivas.No(aSelf, aTipoConexao, True)
                    obLinkNo = New primitivas.LinkEntidadeNo(aSelf, aTipoConexao, True)
                Else
                    obNo = New primitivas.No(aSelf)
                    obLinkNo = New primitivas.LinkEntidadeNo(aSelf)
                End If
                obNo.filterWhere = "xmPath like '*" & xmPath_EntNo & "'"
                obNo.getFields(False)

                'Verificando se a entidade esta em mais de um Nó.
                obLinkNo.filterWhere = "idEntidade = " & Me.Entidade.ID.ToString
                If obLinkNo.DataSource.Count > 1 Then
                    If ExcluirClasseAtual Then
                        obLinkNo.Clear_filters()
                        obLinkNo.Clear_vars()
                        obLinkNo.filterWhere = "idEntidade = " & Me.Entidade.ID.ToString & " and idNo = " & obNo.Campos.idNo_key.ToString
                        obLinkNo.getFields(False)
                        obLinkNo.excluir(False)
                    End If
                    Return True
                Else
                    Return False
                End If
                obNo.Dispose()
                obNo = Nothing
                obLinkNo.Dispose()
                obLinkNo = Nothing
            End Function

            Public Overridable Sub getFromEntidade(ByVal idEntidade As Double, ByVal pManterConexaoAberta As Boolean)
                Me.aEntidade.Clear_vars()
                Me.LimparColecoes()
                'Busca os dados da Entidade
                Me.aEntidade.filterWhere = "IdEntidade = " & idEntidade
                Me.aEntidade.getFields(True)

                'Busca os dados de Endereços
                Me.aEndereco.filterWhere = "idEntidade = " & idEntidade
                Me.aColEndereco = Me.aEndereco.CriaColecaoFields(GetType(CTR.primitivas.Endereco.EnderecoCampos), True)

                'Busca os dados de emails
                Me.aEMail.filterWhere = "idEntidade = " & idEntidade
                Me.aColEmail = Me.aEMail.CriaColecaoFields(GetType(CTR.primitivas.eMail.EmailCampos), True)

                'Busca os dados de Sites
                Me.aUrl.filterWhere = "idEntidade = " & idEntidade
                Me.aColUrl = Me.aUrl.CriaColecaoFields(GetType(CTR.primitivas.Url.UrlCampos), True)

                'Busca os dados de telefones
                Me.aTelefone.filterWhere = "idEntidade = " & idEntidade
                Me.aColTelefone = Me.aTelefone.CriaColecaoFields(GetType(CTR.primitivas.Telefone.TelefoneCampos), pManterConexaoAberta)

            End Sub

            Public Overridable Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.iCTR.iCadastroEntidade.Salvar
                'implementar: Salvar primeiro a entidade, depois os telefones, endereços e emails
                Dim idE As Double
                Dim idEnd As Double
                'Dim strParser As String
                Dim idNo As Long
                Dim mNo As primitivas.No
                Try
                    Me.aSelf.AdmDB.AbreTransaction()

                    'Localizando o Nó pelo xmPath
                    'Verifica se foi usado outro tipo de conexao que não a padrao
                    If Not IsNothing(Me.xmPath_EntNo) Then
                        If Not IsNothing(aTipoConexao) Then
                            mNo = New primitivas.No(aSelf, aTipoConexao, True)
                        Else
                            mNo = New primitivas.No(aSelf)
                        End If

                        Try
                            mNo.filterWhere = "xmPath like '%" & xmPath_EntNo & "'"
                            mNo.getFields(True)
                            idNo = mNo.Campos.idNo_key
                        Catch ex As Exception
                            mNo.filterWhere = "xmPath like '%'"
                            mNo.getFields(True)
                            idNo = mNo.Campos.idNo_key
                        End Try
                    End If
                    'Salvando a Entidade
                    Me.Entidade.salvar(True)
                    idE = Me.Entidade.ID

                    If Not IsNothing(Me.xmPath_EntNo) Then
                        'Salvando o Link Entidade Nó
                        Me.LinkEntidadeNo.idEntidade = idE
                        Me.LinkEntidadeNo.idNo = idNo
                        Me.LinkEntidadeNo.salvar(True)
                    End If

                    'Salvando Endereços
                    For Each endereco As Interfaces.iCTR.Primitivas.iEndereco In aColEndereco.Values
                        endereco.idEntidade_key = idE
                        endereco.salvar(True)
                        If endereco.Principal = True Then
                            idEnd = endereco.ID
                        End If
                    Next

                    'Salvando os Telefones
                    For Each Fone As Interfaces.iCTR.Primitivas.iTelefone In aColTelefone.Values
                        Fone.idEntidade_key = idE
                        If idEnd > 0 Then
                            Fone.idEndereco = idEnd
                        End If
                        Fone.salvar(True)
                    Next

                    'Salvando os Emails
                    For Each email As Interfaces.iCTR.Primitivas.iEmail In aColEmail.Values
                        email.idEntidade_key = idE
                        email.salvar(True)
                    Next

                    'Salvando as URLs
                    For Each url As Interfaces.iCTR.Primitivas.iUrl In aColUrl.Values
                        url.idEntidade_key = idE
                        url.salvar(True)
                    Next

                    'Finaliza a Transação
                    aSelf.AdmDB.FinalizaTransaction(noCommit)

                Catch ex As Exception
                    aSelf.AdmDB.CancelarTransaction()
                    Dim mNBEx As New NBexception("Não foi possível Salvar o Cadastro da Entidade - " & Me.Entidade.NomeRazaoSocial_key, ex)
                    mNBEx.Source = "NBdbm.Fachadas.CTR.CadastroEntidade.Salvar"
                    Throw mNBEx
                End Try
            End Sub

            Public Overridable Sub Salvar() Implements Interfaces.iCTR.iCadastroEntidade.Salvar
                Me.Salvar(False)
            End Sub

            Public Overridable Sub Excluir(ByVal NoCommit As Boolean) Implements Interfaces.iCTR.iCadastroEntidade.Excluir
                aEntidade.excluir(NoCommit)
            End Sub

            Public Overridable Sub Excluir() Implements Interfaces.iCTR.iCadastroEntidade.Excluir
                Me.Excluir(False)
            End Sub

            Public Property Entidade() As Interfaces.iCTR.Primitivas.iEntidade Implements Interfaces.iCTR.iCadastroEntidade.Entidade
                Get
                    Return aEntidade.campos
                End Get
                Set(ByVal Value As Interfaces.iCTR.Primitivas.iEntidade)
                    aEntidade.campos = Value
                End Set
            End Property

            Public Overridable Property xmPath_LinkEntNo() As String Implements Interfaces.iCTR.iCadastroEntidade.xmPath_LinkEntNo
                Get
                    Return xmPath_EntNo
                End Get
                Set(ByVal Value As String)
                    xmPath_EntNo = Value
                End Set
            End Property

            Private Property LinkEntidadeNo() As Interfaces.iCTR.Primitivas.iLinkEntidadeNo
                Get
                    Return aLinkNo.Campos
                End Get
                Set(ByVal Value As Interfaces.iCTR.Primitivas.iLinkEntidadeNo)
                    aLinkNo.Campos = Value
                End Set
            End Property

            Public Property Endereco() As Interfaces.iCTR.Primitivas.iEndereco Implements Interfaces.iCTR.iCadastroEntidade.Endereco
                Get
                    Return aEndereco.Campos
                End Get
                Set(ByVal Value As Interfaces.iCTR.Primitivas.iEndereco)
                    aEndereco.Campos = Value
                End Set
            End Property

            Public Sub NovoEndereco()
                Me.aEndereco = Me.NovoEndereco(Me.aEnderecoFields)
            End Sub

            Friend Function NovoEndereco(ByVal pFields As Fields) As Fachadas.CTR.primitivas.Endereco
                If Not IsNothing(aTipoConexao) Then
                    Return New Fachadas.CTR.primitivas.Endereco(aSelf, Me.aTipoConexao, pFields, False)
                Else
                    Return New Fachadas.CTR.primitivas.Endereco(aSelf, pFields)
                End If
            End Function

            Public Property eMail() As Interfaces.iCTR.Primitivas.iEmail Implements Interfaces.iCTR.iCadastroEntidade.eMail
                Get
                    Return aEMail.Campos
                End Get
                Set(ByVal Value As Interfaces.iCTR.Primitivas.iEmail)
                    aEMail.Campos = Value
                End Set
            End Property

            Public Sub NovoEmail()
                Me.NovoEmail(Me.aEmailFields)
            End Sub
            Friend Function NovoEmail(ByVal pFields As Fields) As Fachadas.CTR.primitivas.eMail
                If Not IsNothing(aTipoConexao) Then
                    Return New Fachadas.CTR.primitivas.eMail(aSelf, Me.aTipoConexao, pFields, False)
                Else
                    Return New Fachadas.CTR.primitivas.eMail(aSelf, pFields)
                End If
            End Function

            Public Property Url() As Interfaces.iCTR.Primitivas.iUrl Implements Interfaces.iCTR.iCadastroEntidade.Url
                Get
                    Return aUrl.Campos
                End Get
                Set(ByVal Value As Interfaces.iCTR.Primitivas.iUrl)
                    aUrl.Campos = Value
                End Set
            End Property

            Public Sub NovaUrl()
                Me.aUrl = Me.NovaUrl(Me.aURLFields)
            End Sub
            Friend Function NovaUrl(ByVal pFields As Fields) As Fachadas.CTR.primitivas.Url
                If Not IsNothing(aTipoConexao) Then
                    Return New Fachadas.CTR.primitivas.Url(aSelf, Me.aTipoConexao, pFields, False)
                Else
                    Return New Fachadas.CTR.primitivas.Url(aSelf, pFields)
                End If
            End Function

            Public Property Telefone() As Interfaces.iCTR.Primitivas.iTelefone Implements Interfaces.iCTR.iCadastroEntidade.Telefone
                Get
                    Return aTelefone.Campos
                End Get
                Set(ByVal Value As Interfaces.iCTR.Primitivas.iTelefone)
                    aTelefone.Campos = Value
                End Set
            End Property

            Public Sub NovoTelefone()
                Me.aTelefone = Me.NovoTelefone(Me.aTelefoneFields)
            End Sub
            Friend Function NovoTelefone(ByVal pFields As Fields) As Fachadas.CTR.primitivas.Telefone
                If Not IsNothing(aTipoConexao) Then
                    Return New Fachadas.CTR.primitivas.Telefone(aSelf, Me.aTipoConexao, pFields, False)
                Else
                    Return New Fachadas.CTR.primitivas.Telefone(aSelf, pFields)
                End If
            End Function

            Friend ReadOnly Property Self() As self
                Get
                    Return aSelf
                End Get
            End Property

        End Class
        Public Class CadastroUsuario

            Implements Interfaces.iCTR.iCadastroUsuario

            Protected mSource As String = "Cadastro de Usuários"
            Protected mEntidade As Fachadas.CTR.primitivas.Entidade
            Protected mEndereco As Fachadas.CTR.primitivas.Endereco
            Protected mTelefone As Fachadas.CTR.primitivas.Telefone
            Protected mCelular As Fachadas.CTR.primitivas.Telefone
            Protected mEMail As Fachadas.CTR.primitivas.eMail
            Protected mUsuario As Fachadas.CTR.primitivas.Usuario
            Protected mConfig As Fachadas.CTR.primitivas.UsuarioConfig
            Protected mLinkNo As Fachadas.CTR.primitivas.LinkEntidadeNo
            Protected mTipoConexao As tipos.tiposConection
            Protected mXmPath_EntNo As String
            Protected aSelf As self


            Public Sub New()
                aSelf = New self
                mEntidade = New Fachadas.CTR.primitivas.Entidade(aSelf)
                mEndereco = New Fachadas.CTR.primitivas.Endereco(aSelf)
                mTelefone = New Fachadas.CTR.primitivas.Telefone(aSelf)
                mCelular = New Fachadas.CTR.primitivas.Telefone(aSelf)
                mEMail = New Fachadas.CTR.primitivas.eMail(aSelf)
                mUsuario = New Fachadas.CTR.primitivas.Usuario(aSelf)
                mConfig = New Fachadas.CTR.primitivas.UsuarioConfig(aSelf)
                mLinkNo = New Fachadas.CTR.primitivas.LinkEntidadeNo(aSelf)
            End Sub
            Public Sub New(ByVal TipoConexao As tipos.tiposConection)
                aSelf = New self
                mTipoConexao = TipoConexao
                mEntidade = New Fachadas.CTR.primitivas.Entidade(aSelf, TipoConexao, True)
                mEndereco = New Fachadas.CTR.primitivas.Endereco(aSelf, TipoConexao, True)
                mTelefone = New Fachadas.CTR.primitivas.Telefone(aSelf, TipoConexao, True)
                mCelular = New Fachadas.CTR.primitivas.Telefone(aSelf, TipoConexao, True)
                mEMail = New Fachadas.CTR.primitivas.eMail(aSelf, TipoConexao, True)
                mUsuario = New Fachadas.CTR.primitivas.Usuario(aSelf, TipoConexao, True)
                mConfig = New Fachadas.CTR.primitivas.UsuarioConfig(aSelf, TipoConexao, True)
                mLinkNo = New Fachadas.CTR.primitivas.LinkEntidadeNo(aSelf, TipoConexao, False)
            End Sub
            Public Sub getFieldsFromEntidade(ByVal idEntidade As Double)

                Me.mEntidade.filterWhere = "IdEntidade = " & idEntidade
                Me.mEntidade.getFields(True)

                Me.mEndereco.filterWhere = "idEntidade = " & idEntidade
                Me.mEndereco.getFields(True)

                Me.mTelefone.filterWhere = "Descricao = 'Res' and idEntidade =" & idEntidade
                Me.mTelefone.getFields(True)

                Me.mCelular.filterWhere = "Descricao = 'Cel' and idEntidade = " & idEntidade
                Me.mCelular.getFields(True)

                Me.mEMail.filterWhere = "idEntidade = " & idEntidade
                Me.mEMail.getFields(True)

                Me.mUsuario.filterWhere = "idEntidade = " & idEntidade
                Me.mUsuario.getFields(True)

                Me.mConfig.filterWhere = "idUsuario = " & Me.Usuario.ID
                Me.mConfig.getFields(False)

            End Sub
            Public Sub getFieldsFromUsuario(ByVal idUsuario As Double)

                Me.mUsuario.filterWhere = "idUsuario = " & idUsuario
                Me.mUsuario.getFields(True)

                Me.mConfig.filterWhere = "idUsuario = " & idUsuario
                Me.mConfig.getFields(True)

                Me.getFieldsFromEntidade(Me.Usuario.idEntidade)

            End Sub
            Public Sub getFieldsFromUsuario(ByVal Login As String)

                Me.mUsuario.filterWhere = "Login = '" & Login & "'"
                Me.mUsuario.getFields(True)

                Me.mConfig.filterWhere = "idUsuario = " & mUsuario.Campos.ID
                Me.mConfig.getFields(True)

                Me.getFieldsFromEntidade(Me.Usuario.idEntidade)

            End Sub
            Public Sub Salvar(ByVal NoCommit As Boolean) Implements Interfaces.iCTR.iCadastroUsuario.Salvar
                Dim ob As primitivas.No

                Try
                    'Verifica se foi usado outro tipo de conexao que não a padrao
                    If Not IsNothing(mTipoConexao) Then
                        ob = New primitivas.No(aSelf, mTipoConexao, True)
                    Else
                        ob = New primitivas.No(aSelf)
                    End If

                    'Localizando o Nó pelo xmPath
                    ob.filterWhere = "xmPath like '" & XmPath_EntNo & "%'"
                    ob.getFields(True)

                    'Salvando a Entidade
                    Me.mEntidade.campos.salvar(True)

                    'Salvando o Link Entidade Nó
                    Me.mLinkNo.Campos.idEntidade = Me.Entidade.ID
                    Me.mLinkNo.Campos.idNo = ob.Campos.idNo_key
                    Me.mLinkNo.Campos.salvar(True)


                    'Salvar o Endereco
                    Me.Endereco.idEntidade_key = Me.Entidade.ID
                    Me.Endereco.salvar(True)

                    'Salvar o Telefone
                    Me.Telefone.idEndereco = Me.Endereco.ID
                    Me.Telefone.idEntidade_key = Me.Entidade.ID
                    Me.Telefone.salvar(True)

                    'Salvar Celular
                    Me.Celular.idEndereco = Me.Endereco.ID
                    Me.Celular.idEntidade_key = Me.Entidade.ID
                    Me.Celular.salvar(True)

                    'Salvar Email
                    Me.Email.idEntidade_key = Me.Entidade.ID
                    Me.Email.salvar(True)

                    'Salvando o Usuario
                    Me.Usuario.idEntidade = Me.Entidade.ID
                    Me.Usuario.salvar(True)

                    'Salvando as Configurações
                    Me.UsuarioConfig.idUsuario_key = Me.Usuario.ID
                    UsuarioConfig.salvar(True)

                    'Finaliza a Transação
                    aself.AdmDB.FinalizaTransaction(NoCommit)

                Catch ex As Exception
                    If (aself.AdmDB.connection.State = ConnectionState.Open) Then
                        aself.AdmDB.connection.Close()
                    End If

                    Dim mNBEx As New NBexception("Não foi possível Salvar o Cadastro do Usuário - " & Me.Entidade.NomeRazaoSocial_key, ex)
                    mNBEx.Source = "NBdbm.Fachadas.CTR.CadastroUsuario.Salvar"
                    Throw mNBEx
                End Try
            End Sub
            Public Sub Salvar() Implements Interfaces.iCTR.iCadastroUsuario.Salvar
                Me.Salvar(False)
            End Sub
            Public Sub Excluir(ByVal NoCommit As Boolean) Implements Interfaces.iCTR.iCadastroUsuario.Excluir
                Try
                    Me.mEntidade.excluir(NoCommit)
                Catch ex As NBexception
                    ex.Source = Me.mSource
                    Throw ex
                End Try
            End Sub
            Public Sub Excluir() Implements Interfaces.iCTR.iCadastroUsuario.Excluir
                Me.Excluir(False)
            End Sub
            Public Sub ExcluirUsuarioAtual(ByVal NoCommit As Boolean)
                Try
                    mUsuario.excluir(NoCommit)
                Catch ex As NBexception
                    ex.Source = Me.mSource
                    Throw ex
                End Try
            End Sub
            Public Property Entidade() As Interfaces.iCTR.Primitivas.iEntidade Implements Interfaces.iCTR.iCadastroUsuario.Entidade
                Get
                    Return Me.mEntidade.campos
                End Get
                Set(ByVal Value As Interfaces.iCTR.Primitivas.iEntidade)
                    Me.mEntidade.campos = Value
                End Set
            End Property
            Public Property Endereco() As Interfaces.iCTR.Primitivas.iEndereco Implements Interfaces.iCTR.iCadastroUsuario.Endereco
                Get
                    Return Me.mEndereco.Campos
                End Get
                Set(ByVal Value As Interfaces.iCTR.Primitivas.iEndereco)
                    Me.mEndereco.Campos = Value
                End Set
            End Property
            Public Property Telefone() As Interfaces.iCTR.Primitivas.iTelefone Implements Interfaces.iCTR.iCadastroUsuario.Telefone
                Get
                    Return Me.mTelefone.Campos
                End Get
                Set(ByVal Value As Interfaces.iCTR.Primitivas.iTelefone)
                    Me.mTelefone.Campos = Value
                End Set
            End Property
            Public Property Celular() As Interfaces.iCTR.Primitivas.iTelefone Implements Interfaces.iCTR.iCadastroUsuario.Celular
                Get
                    Return Me.mCelular.Campos
                End Get
                Set(ByVal Value As Interfaces.iCTR.Primitivas.iTelefone)
                    Me.mCelular.Campos = Value
                End Set
            End Property
            Public Property Email() As Interfaces.iCTR.Primitivas.iEmail Implements Interfaces.iCTR.iCadastroUsuario.Email
                Get
                    Return Me.mEMail.Campos
                End Get
                Set(ByVal Value As Interfaces.iCTR.Primitivas.iEmail)
                    Me.mEMail.Campos = Value
                End Set
            End Property
            Public Property Usuario() As Interfaces.iCTR.Primitivas.iUsuario Implements Interfaces.iCTR.iCadastroUsuario.Usuario
                Get
                    Return mUsuario.Campos
                End Get
                Set(ByVal Value As Interfaces.iCTR.Primitivas.iUsuario)
                    mUsuario.Campos = Value
                End Set
            End Property
            Public Property UsuarioConfig() As Interfaces.iCTR.Primitivas.iUsuarioConfig Implements Interfaces.iCTR.iCadastroUsuario.UsuarioConfig
                Get
                    Return mConfig.Campos
                End Get
                Set(ByVal Value As Interfaces.iCTR.Primitivas.iUsuarioConfig)
                    mConfig.Campos = Value
                End Set
            End Property
            Public Property XmPath_EntNo() As String
                Get
                    Return mXmPath_EntNo
                End Get
                Set(ByVal Value As String)
                    mXmPath_EntNo = Value
                End Set
            End Property
            Public Overridable Sub Dispose() Implements System.IDisposable.Dispose
                If Not IsNothing(mEntidade) Then mEntidade.Dispose()
                If Not IsNothing(mEndereco) Then mEndereco.Dispose()
                If Not IsNothing(mTelefone) Then mTelefone.Dispose()
                If Not IsNothing(mCelular) Then mCelular.Dispose()
                If Not IsNothing(mEMail) Then mEMail.Dispose()
                If Not IsNothing(mUsuario) Then mUsuario.Dispose()
                If Not IsNothing(mConfig) Then mConfig.Dispose()
                If Not IsNothing(mLinkNo) Then mLinkNo.Dispose()
                mEntidade = Nothing
                mEndereco = Nothing
                mTelefone = Nothing
                mCelular = Nothing
                mEMail = Nothing
                mUsuario = Nothing
                mConfig = Nothing
                mLinkNo = Nothing
            End Sub
        End Class
#Region "===[ Cadastro de Funcionario ]==="
        '===[ Cadastro de Funcionario ]=======================================================================================
        Public Class CadastroFuncionario
            Inherits Fachadas.CTR.CadastroUsuario

            Public Sub New()
                MyBase.New()
                Me.InicializacaoPadrao()
            End Sub
            Public Sub New(ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(TipoConexao)
                Me.mTipoConexao = TipoConexao
                Me.InicializacaoPadrao()
            End Sub
            Public Sub NovoEndereco()
                Me.mEndereco = New NBdbm.Fachadas.CTR.primitivas.Endereco(aSelf, Me.mTipoConexao, False)
            End Sub
            Public Sub NovoEmail()
                Me.mEMail = New NBdbm.Fachadas.CTR.primitivas.eMail(aSelf, Me.mTipoConexao, False)
            End Sub
            Public Sub NovoTelefone()
                Me.mTelefone = New NBdbm.Fachadas.CTR.primitivas.Telefone(aSelf, Me.mTipoConexao, False)
            End Sub
            Public Sub NovoCelular()
                Me.mCelular = New NBdbm.Fachadas.CTR.primitivas.Telefone(aSelf, Me.mTipoConexao, False)
            End Sub
            Public ReadOnly Property Source() As String
                Get
                    Return Me.mSource
                End Get
            End Property
            Private Sub InicializacaoPadrao()
                mSource = "**** Cadastro de Funcionários ****"
                Me.mXmPath_EntNo = "<Entidades><Funcionários>"
            End Sub
        End Class
#End Region

        Namespace primitivas
            Public Class Entidade
                Inherits allClass
                Private mCampos As EntidadeCampos

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing, False)
                End Sub

                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub

                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pSelf, "CTRL_Entidades", pFields)
                    MyBase.var.Add("CPFCNPJ", Me.CPFCNPJ, System.Type.GetType("System.String"))
                    mCampos = New EntidadeCampos(Me)
                End Sub

                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_Entidades", TipoConexao, pFields, pManterConexaoAberta)
                    MyBase.var.Add("CPFCNPJ", Me.CPFCNPJ, System.Type.GetType("System.String"))
                    mCampos = New EntidadeCampos(Me)
                End Sub

                Public Property campos() As Interfaces.iCTR.Primitivas.iEntidade
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iEntidade)
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
                Public Sub getFieldsFromNomeEntidade(ByVal NomePrimary As String)
                    Me.Clear_filters()
                    Me.filterWhere = "NomePrimary='" & NomePrimary & "'"
                    Me.getFields(False)
                End Sub
                Public Sub getFieldsFromCnpjCpf(ByVal CNPJ_CPF As String)
                    Dim mIdCPFCNPJ As Integer
                    'Dim mPessoaFisica As Boolean
                    Try
                        mIdCPFCNPJ = Me.RetornaIdCPFCNPJ(NBFuncoes.validaCPFCNPJ(CNPJ_CPF, True))
                        If mIdCPFCNPJ > 0 Then
                            Me.Clear_vars()
                            Me.Clear_filters()
                            Me.filterWhere = "idCPFCNPJ=" & mIdCPFCNPJ
                            Me.getFields(False)
                        End If
                    Catch ex As Exception
                        Dim NBEx As New NBexception("Não foi Possível Buscar a Entidade Pelo CNPJ_CPF", ex)
                        NBEx.Source = "Entidade.getFieldsFromCnpjCpf"
                        If aself.AdmDB.connection.State = ConnectionState.Closed Then
                            aself.AdmDB.connection.Close()
                        End If
                        Throw NBEx
                    End Try

                End Sub
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub
                Friend Class EntidadeCampos
                    Implements Interfaces.iCTR.Primitivas.iEntidade

                    Private mParent As allClass

                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return mParent
                        End Get
                        Set(ByVal Value As allClass)
                            mParent = Value
                        End Set
                    End Property
                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        mParent = Nothing
                    End Sub
                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        'Implentar:
                        'Aqui será feita a verificação do me.id = null para inserir ou caso
                        'contrário será realizado uma edição
                        'Dim retorno As tipos.Retorno
                        Parent.var("dtAlteracao").Dirty = True
                        Parent.var("dtAlteracao").value = Now
                        Parent.filterWhere = "idEntidade = " & Me.ID & " and idCPFCNPJ = " & Parent.var("idCPFCNPJ").value & " and NomePrimary = '" & Me.NomeRazaoSocial_Key & "' "
                        Parent.editar(noCommit)
                        If Me.Parent.Inclusao Then
                            Parent.var("dtCriacao").Dirty = True
                            Parent.var("dtCriacao").value = Now
                            Parent.inserir(noCommit)
                        End If
                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub
                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub
#Region "   Propriedades - Fields   "

                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("IdEntidade").value
                        End Get
                    End Property

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.CPFCNPJ_Key.ToString & Me.NomeRazaoSocial_Key.ToString
                        End Get
                    End Property

                    Public Property ApelidoNomeFantasia() As String Implements Interfaces.iCTR.Primitivas.iEntidade.ApelidoNomeFantasia
                        Get
                            Return Parent.var("NomeSecundary").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("NomeSecundary").value = newValue
                            Parent.var("NomeSecundary").Dirty = True
                        End Set
                    End Property

                    Public Property CPFCNPJ_Key() As String Implements Interfaces.iCTR.Primitivas.iEntidade.CPFCNPJ_key
                        Get
                            Return Parent.CPFCNPJ
                        End Get
                        Set(ByVal newValue As String)
                            Parent.CPFCNPJ = newValue
                            Parent.var("idCPFCNPJ").Dirty = True
                        End Set
                    End Property

                    Public ReadOnly Property dtAlteracao() As Date Implements Interfaces.iCTR.Primitivas.iEntidade.dtAlteracao
                        Get
                            'parent.var("dtAlteracao").Value = CDate(Now).ToString(self.Settings.sintaxeData)
                            Return CDate(Parent.var("dtAlteracao").value)
                        End Get
                    End Property

                    Public ReadOnly Property dtCriacao() As Date Implements Interfaces.iCTR.Primitivas.iEntidade.dtCriacao
                        Get
                            'CONVERT(DATETIME, '12/08/1973',102)
                            Return CDate(Parent.var("dtCriacao").value)
                        End Get
                    End Property

                    Public Property dtNascimentoInicioAtividades() As Date Implements Interfaces.iCTR.Primitivas.iEntidade.dtNascimentoInicioAtividades
                        Get
                            Return CDate(Parent.var("dtNascimento").value)
                        End Get
                        Set(ByVal newValue As Date)
                            Parent.var("dtNascimento").value = newValue
                            Parent.var("dtNascimento").Dirty = True
                        End Set
                    End Property

                    Public ReadOnly Property IdadeTempoExistencia() As Integer Implements Interfaces.iCTR.Primitivas.iEntidade.IdadeTempoExistencia
                        Get
                            Dim d As New Object
                            CDate(d).op_Subtraction(Now, Parent.var("dtNascimento").value)
                            Return CInt(d)
                        End Get
                    End Property

                    Public Property NomeRazaoSocial_Key() As String Implements Interfaces.iCTR.Primitivas.iEntidade.NomeRazaoSocial_key
                        Get
                            Return Parent.var("NomePrimary").value
                        End Get
                        Set(ByVal newValue As String)
                            If newValue = "" Then
                                Dim nbEX As NBdbm.NBexception
                                nbEX = New NBexception("Este Campo não pode conter valor em branco")
                                nbEX.Source = "Entidade - NomeRazaoSocial_Key"
                                Throw nbEX
                            End If

                            Parent.var("NomePrimary").value = newValue
                            Parent.var("NomePrimary").Dirty = True
                        End Set
                    End Property

                    Public Property OrgaoEmissorIM() As String Implements Interfaces.iCTR.Primitivas.iEntidade.OrgaoEmissorIM
                        Get
                            Return Parent.var("OrgaoEmissor_RGIE").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("OrgaoEmissor_RGIE").value = newValue
                            Parent.var("OrgaoEmissor_RGIE").Dirty = True
                        End Set
                    End Property

                    Public Property PessoaFisica() As Boolean Implements Interfaces.iCTR.Primitivas.iEntidade.PessoaFisica
                        Get
                            Return Parent.var("PessoaFJ").value
                        End Get
                        Set(ByVal Value As Boolean)
                            Parent.var("PessoaFJ").Dirty = True
                            Parent.var("PessoaFJ").value = Value
                        End Set
                    End Property

                    Public Property RGIE() As String Implements Interfaces.iCTR.Primitivas.iEntidade.RgIE
                        Get
                            Return Parent.var("RGIE").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("RGIE").value = newValue
                            Parent.var("RGIE").Dirty = True
                        End Set
                    End Property

                    Public Property TextoRespeito() As String Implements Interfaces.iCTR.Primitivas.iEntidade.TextoRespeito
                        Get
                            Return Parent.var("txtRespeito").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("txtRespeito").value = newValue
                            Parent.var("txtRespeito").Dirty = True
                        End Set
                    End Property
#End Region
                End Class
            End Class


            Public Class Endereco
                Inherits allClass
                Private mCampos As EnderecoCampos 'Interfaces.iCTR.Primitivas.iEndereco

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pSelf, "CTRL_Enderecos", pFields)
                    mCampos = New EnderecoCampos(Me)
                End Sub
                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_Enderecos", TipoConexao, pFields, pManterConexaoAberta)
                    mCampos = New EnderecoCampos(Me)
                End Sub
                Public Property Campos() As Interfaces.iCTR.Primitivas.iEndereco
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iEndereco)
                        mCampos = Value
                        If Not Value Is Nothing Then
                            MyBase.toObject = mCampos.Parent
                        End If
                    End Set
                End Property
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub
                Friend Class EnderecoCampos
                    Implements Interfaces.iCTR.Primitivas.iEndereco

                    Private mParent As allClass

                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return mParent
                        End Get
                        Set(ByVal Value As allClass)
                            mParent = Value
                        End Set
                    End Property
                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        mParent = Nothing
                    End Sub
                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Try
                            Dim filtro As String
                            filtro = " idEndereco = " & Me.ID & " And idEntidade = " & Me.idEntidade_key.ToString
                            Me.Parent.SalvarPadrao(noCommit, filtro)

                            'Finaliza a Transação
                            mParent.Self.AdmDB.FinalizaTransaction(noCommit)

                        Catch ex As Exception
                            Dim mNBEx As New NBexception("Não foi possível Salvar o Endereço - " & Me.Logradouro_key, ex)
                            mNBEx.Source = "NBdbm.Fachadas.CTR.primitivas.Endereco.Salvar"
                            Throw mNBEx
                        End Try

                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub
                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub

#Region "   Propriedades - Fields   "

                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("IDEndereco").value
                        End Get
                    End Property

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.ID.ToString & Me.Logradouro_key.ToString
                        End Get
                    End Property

                    Public Property idEntidade_key() As Integer Implements Interfaces.iCTR.Primitivas.iEndereco.idEntidade_key
                        Get
                            Return Parent.var("idEntidade").value
                        End Get
                        Set(ByVal newValue As Integer)
                            Parent.var("idEntidade").value = newValue
                            Parent.var("idEntidade").Dirty = True
                        End Set
                    End Property

                    Public Property Logradouro_key() As String Implements Interfaces.iCTR.Primitivas.iEndereco.Logradouro_key
                        Get
                            Return Parent.var("Logradouro").value
                        End Get
                        Set(ByVal newValue As String)
                            If newValue = "" Then
                                Dim nbEX As NBdbm.NBexception
                                nbEX = New NBexception("Este Campo não pode conter valor em branco")
                                nbEX.Source = "Endereço - Logradouro_Key"
                                Throw nbEX
                            End If
                            Parent.var("Logradouro").value = newValue
                            Parent.var("Logradouro").Dirty = True
                        End Set
                    End Property

                    Public Property complemento() As String Implements Interfaces.iCTR.Primitivas.iEndereco.complemento
                        Get
                            Return Parent.var("Complemento").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("Complemento").value = newValue
                            Parent.var("Complemento").Dirty = True
                        End Set
                    End Property

                    Public Property Bairro() As String Implements Interfaces.iCTR.Primitivas.iEndereco.Bairro
                        Get
                            Return Parent.var("Bairro").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("Bairro").value = newValue
                            Parent.var("Bairro").Dirty = True
                        End Set
                    End Property

                    Public Property CEP() As String Implements Interfaces.iCTR.Primitivas.iEndereco.CEP
                        Get
                            Return Parent.var("CEP").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("CEP").value = NBFuncoes.soNumero(newValue)
                            Parent.var("CEP").Dirty = True
                        End Set
                    End Property

                    Public Property Municipio() As String Implements Interfaces.iCTR.Primitivas.iEndereco.Municipio
                        Get
                            Return Parent.var("Municipio").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("Municipio").value = newValue
                            Parent.var("Municipio").Dirty = True
                        End Set
                    End Property

                    Public Property UF() As String Implements Interfaces.iCTR.Primitivas.iEndereco.UF
                        Get
                            Return Parent.var("UF").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("UF").value = newValue
                            Parent.var("UF").Dirty = True
                        End Set
                    End Property

                    Public Property Comentario() As String Implements Interfaces.iCTR.Primitivas.iEndereco.Comentario
                        Get
                            Return Parent.var("Comentario").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("Comentario").value = newValue
                            Parent.var("Comentario").Dirty = True
                        End Set
                    End Property

                    Public Property Contato() As String Implements Interfaces.iCTR.Primitivas.iEndereco.Contato
                        Get
                            Return Parent.var("Contato").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("Contato").Dirty = True
                            Parent.var("Contato").value = Left(newValue, 15)
                        End Set
                    End Property

                    Public Property Principal() As Boolean Implements Interfaces.iCTR.Primitivas.iEndereco.Principal
                        Get
                            Return Convert.ToBoolean(Parent.var("Principal").value)
                        End Get
                        Set(ByVal newValue As Boolean)
                            Parent.var("Principal").value = newValue
                            Parent.var("Principal").Dirty = True
                        End Set
                    End Property
#End Region

                End Class

            End Class
            Public Class eMail
                Inherits allClass
                Private mCampos As EmailCampos 'Interfaces.iCTR.Primitivas.iEmail

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pSelf, "CTRL_Email", pFields)
                    mCampos = New EmailCampos(Me)
                End Sub
                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_Email", TipoConexao, pFields, pManterConexaoAberta)
                    mCampos = New EmailCampos(Me)
                End Sub

                Public Property Campos() As Interfaces.iCTR.Primitivas.iEmail
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iEmail)
                        mCampos = Value
                        If Not Value Is Nothing Then
                            MyBase.toObject = mCampos.Parent
                        End If
                    End Set
                End Property
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub

                Friend Class EmailCampos

                    Implements Interfaces.iCTR.Primitivas.iEmail

                    Private mParent As allClass
                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return Me.mParent
                        End Get
                        Set(ByVal Value As allClass)
                            Me.mParent = Value
                        End Set
                    End Property
                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        mParent = Nothing
                    End Sub
                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Try
                            Dim filtro As String
                            filtro = " idEmail = " & Me.ID.ToString & " And idEntidade = " & Me.idEntidade_key.ToString
                            If Me.eMail_key <> "" Then Me.Parent.SalvarPadrao(noCommit, filtro)

                            'Finaliza a Transação
                            Parent.Self.AdmDB.FinalizaTransaction(noCommit)

                        Catch ex As Exception
                            Dim mNBEx As New NBexception("Não foi possível Salvar o Email - " & Me.eMail_key, ex)
                            mNBEx.Source = "NBdbm.Fachadas.CTR.primitivas.Email.Salvar"
                            Throw mNBEx
                        End Try

                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub
                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub
#Region "   Propriedades - Fields   "

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.ID.ToString & Me.eMail_key.ToString
                        End Get
                    End Property

                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("idEMail").value
                        End Get
                    End Property

                    Public Property Descricao() As String Implements Interfaces.iCTR.Primitivas.iEmail.Descricao
                        Get
                            Return Parent.var("Descricao").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("Descricao").value = newValue
                            Parent.var("Descricao").Dirty = True
                        End Set
                    End Property

                    Public Property eMail_key() As String Implements Interfaces.iCTR.Primitivas.iEmail.eMail_key
                        Get
                            Return Parent.var("eMail").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("eMail").value = newValue
                            Parent.var("eMail").Dirty = True
                        End Set
                    End Property

                    Public Property idEntidade_key() As Integer Implements Interfaces.iCTR.Primitivas.iEmail.idEntidade_key
                        Get
                            Return Parent.var("idEntidade").value
                        End Get
                        Set(ByVal newValue As Integer)
                            Parent.var("idEntidade").value = newValue
                            Parent.var("idEntidade").Dirty = True
                        End Set
                    End Property
#End Region

                End Class

            End Class
            Public Class Url
                Inherits allClass
                Private mCampos As UrlCampos 'Interfaces.iCTR.Primitivas.iUrl

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pSelf, "CTRL_Url", pFields)
                    mCampos = New UrlCampos(Me)
                End Sub
                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_Url", TipoConexao, pFields, pManterConexaoAberta)
                    mCampos = New UrlCampos(Me)
                End Sub

                Public Property Campos() As Interfaces.iCTR.Primitivas.iUrl
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iUrl)
                        mCampos = Value
                        If Not Value Is Nothing Then
                            MyBase.toObject = mCampos.Parent
                        End If
                    End Set
                End Property
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub

                Friend Class UrlCampos
                    Implements Interfaces.iCTR.Primitivas.iUrl

                    Private mParent As allClass
                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return Me.mParent
                        End Get
                        Set(ByVal Value As allClass)
                            Me.mParent = Value
                        End Set
                    End Property

                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        mParent = Nothing
                    End Sub
                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Try
                            Dim filtro As String
                            filtro = " idUrl = " & Me.ID.ToString & " And idEntidade = " & Me.idEntidade_key.ToString
                            If Me.URL_key <> "" Then Me.Parent.SalvarPadrao(noCommit, filtro)

                            'Finaliza a Transação
                            Parent.Self.AdmDB.FinalizaTransaction(noCommit)

                        Catch ex As Exception
                            Dim mNBEx As New NBexception("Não foi possível Salvar a URL - " & Me.URL_key, ex)
                            mNBEx.Source = "NBdbm.Fachadas.CTR.primitivas.URL.Salvar"
                            Throw mNBEx
                        End Try

                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub
                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub
#Region "   Propriedades - Fields   "

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.ID.ToString & Me.URL_key.ToString
                        End Get
                    End Property

                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("idUrl").value
                        End Get
                    End Property

                    Public Property Descricao() As String Implements Interfaces.iCTR.Primitivas.iUrl.Descricao
                        Get
                            Return Parent.var("Descricao").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("Descricao").value = newValue
                            Parent.var("Descricao").Dirty = True
                        End Set
                    End Property

                    Public Property URL_key() As String Implements Interfaces.iCTR.Primitivas.iUrl.Url_key
                        Get
                            Return Parent.var("url").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("url").value = newValue
                            Parent.var("url").Dirty = True
                        End Set
                    End Property

                    Public Property idEntidade_key() As Integer Implements Interfaces.iCTR.Primitivas.iUrl.idEntidade_key
                        Get
                            Return Parent.var("idEntidade").value
                        End Get
                        Set(ByVal newValue As Integer)
                            Parent.var("idEntidade").value = newValue
                            Parent.var("idEntidade").Dirty = True
                        End Set
                    End Property
#End Region

                End Class

            End Class
            Public Class Telefone
                Inherits allClass
                Private mCampos As TelefoneCampos 'Interfaces.iCTR.Primitivas.iTelefone

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pSelf, "CTRL_Fones", pFields)
                    mCampos = New TelefoneCampos(Me)
                End Sub
                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_Fones", TipoConexao, pFields, pManterConexaoAberta)
                    mCampos = New TelefoneCampos(Me)
                End Sub

                Public Property Campos() As Interfaces.iCTR.Primitivas.iTelefone
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iTelefone)
                        mCampos = Value
                        If Not Value Is Nothing Then
                            MyBase.toObject = mCampos.Parent
                        End If
                    End Set
                End Property
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub

                Friend Class TelefoneCampos

                    Implements Interfaces.iCTR.Primitivas.iTelefone, ICloneable

                    Private mParent As allClass

                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return mParent
                        End Get
                        Set(ByVal Value As allClass)
                            mParent = Value
                        End Set
                    End Property
                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        mParent = Nothing
                    End Sub
                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Try
                            Dim filtro As String
                            filtro = " idFone = " & Me.ID.ToString & " And idEntidade = " & Me.idEntidade_key.ToString
                            If Me.Fone_key <> "" Then Me.Parent.SalvarPadrao(noCommit, filtro)

                            'Finaliza a Transação
                            Parent.Self.AdmDB.FinalizaTransaction(noCommit)

                        Catch ex As Exception
                            Dim mNBEx As New NBexception("Não foi possível Salvar o Telefone - " & Me.Fone_key, ex)
                            mNBEx.Source = "NBdbm.Fachadas.CTR.primitivas.Telefone.Salvar"
                            Throw mNBEx
                        End Try

                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub
                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub
#Region "   Propriedades - Fields   "

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.ID.ToString & Me.DDD_key.ToString & Me.Fone_key.ToString
                        End Get
                    End Property

                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("idFone").value
                        End Get
                    End Property

                    Public Property Descricao() As String Implements Interfaces.iCTR.Primitivas.iTelefone.Descricao
                        Get
                            Return Parent.var("Descricao").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("Descricao").value = newValue
                            Parent.var("Descricao").Dirty = True
                        End Set
                    End Property

                    Public Property DDD_key() As String Implements Interfaces.iCTR.Primitivas.iTelefone.DDD_key
                        Get
                            Return Parent.var("DDD").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("DDD").value = NBFuncoes.soNumero(newValue).ToString
                            Parent.var("DDD").Dirty = True
                        End Set
                    End Property

                    Public Property Fone_key() As String Implements Interfaces.iCTR.Primitivas.iTelefone.Fone_key
                        Get
                            Return Parent.var("Fone").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("Fone").value = NBFuncoes.soNumero(newValue).ToString
                            Parent.var("Fone").Dirty = True
                        End Set
                    End Property

                    Public Property Ramal() As String Implements Interfaces.iCTR.Primitivas.iTelefone.Ramal
                        Get
                            Return Parent.var("Ramal").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("Ramal").value = newValue
                            Parent.var("Ramal").Dirty = True
                        End Set
                    End Property

                    Public Property Contato() As String Implements Interfaces.iCTR.Primitivas.iTelefone.Contato
                        Get
                            Return Parent.var("Contato").value
                        End Get
                        Set(ByVal newValue As String)
                            Parent.var("Contato").value = newValue
                            Parent.var("Contato").Dirty = True
                        End Set
                    End Property

                    Public Property idEndereco() As Integer Implements Interfaces.iCTR.Primitivas.iTelefone.idEndereco
                        Get
                            Return Parent.var("idEndereco").value
                        End Get
                        Set(ByVal newValue As Integer)
                            Parent.var("idEndereco").value = newValue
                            Parent.var("idEndereco").Dirty = True
                        End Set
                    End Property

                    Public Property idEntidade_key() As Integer Implements Interfaces.iCTR.Primitivas.iTelefone.idEntidade_key
                        Get
                            Return Parent.var("idEntidade").value
                        End Get
                        Set(ByVal newValue As Integer)
                            Parent.var("idEntidade").value = newValue
                            Parent.var("idEntidade").Dirty = True
                        End Set
                    End Property
#End Region

                    Public Function Clone() As Object Implements System.ICloneable.Clone
                        Return Me.MemberwiseClone
                    End Function
                End Class

            End Class
            Public Class Usuario
                Inherits allClass
                Private mCampos As UsuarioCampos 'Interfaces.iCTR.Primitivas.iUsuario

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing, False)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pSelf, "CTRL_Usuario", pFields)
                    mCampos = New UsuarioCampos(Me)
                End Sub
                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_Usuario", TipoConexao, pFields, pManterConexaoAberta)
                    mCampos = New UsuarioCampos(Me)
                End Sub

                Public Property Campos() As Interfaces.iCTR.Primitivas.iUsuario
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iUsuario)
                        mCampos = Value
                        If Not Value Is Nothing Then
                            MyBase.toObject = mCampos.Parent
                        End If
                    End Set
                End Property
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub
                Public Sub getUsuarioFromLogin(ByVal Login As String)
                    Me.Clear_filters()
                    Me.Clear_vars()
                    Me.filterWhere = "Login='" & Login & "'"
                    Me.getFields(False)
                End Sub
                Friend Class UsuarioCampos
                    Implements Interfaces.iCTR.Primitivas.iUsuario

                    Private mParent As allClass

                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return Me.mParent
                        End Get
                        Set(ByVal Value As allClass)
                            Me.mParent = Value
                        End Set
                    End Property
                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        Parent = Nothing
                    End Sub
                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Try
                            Dim filtro As String
                            filtro = " Login = '" & Me.login_key & "' "
                            Me.Parent.SalvarPadrao(noCommit, filtro)

                            'Finaliza a Transação
                            Parent.Self.AdmDB.FinalizaTransaction(noCommit)

                        Catch ex As Exception
                            Dim mNBEx As New NBexception("Não foi possível Salvar o Usuário - " & Me.login_key, ex)
                            mNBEx.Source = "NBdbm.Fachadas.CTR.primitivas.Usuario.Salvar"
                            Throw mNBEx
                        End Try

                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub
                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub

#Region "   Propriedades - Fields   "

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.login_key.ToString
                        End Get
                    End Property

                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("idUsuario").value
                        End Get
                    End Property

                    Public Property login_key() As String Implements Interfaces.iCTR.Primitivas.iUsuario.login_key
                        Get
                            Return Parent.var("Login").value
                        End Get
                        Set(ByVal Value As String)
                            If Value = "" Then
                                Dim nbEX As NBdbm.NBexception
                                nbEX = New NBexception("Este Campo não pode conter valor em branco")
                                nbEX.Source = "Usuário - Login_Key"
                                Throw nbEX
                            End If
                            Parent.var("Login").Dirty = True
                            Parent.var("Login").value = Value
                        End Set
                    End Property

                    Public Property senha() As String Implements Interfaces.iCTR.Primitivas.iUsuario.senha
                        Get
                            Return NBFuncoes.decripto(Parent.var("Senha").value)
                        End Get
                        Set(ByVal Value As String)
                            If Value = "" Then
                                Dim nbEX As NBdbm.NBexception
                                nbEX = New NBexception("Este Campo não pode conter valor em branco")
                                nbEX.Source = "Usuário - Senha"
                                Throw nbEX
                            End If
                            Parent.var("Senha").Dirty = True
                            Parent.var("Senha").value = NBFuncoes.cripto(Value)
                        End Set
                    End Property

                    Public Property dtAdmissao() As Date Implements Interfaces.iCTR.Primitivas.iUsuario.dtAdmissao
                        Get
                            Return Parent.var("dtAdmissao").value
                        End Get
                        Set(ByVal Value As Date)
                            Parent.var("dtAdmissao").Dirty = True
                            Parent.var("dtAdmissao").value = Value
                            '                        'Return CDate(Parent.var("dtCriacao").Value).ToString(self.Settings.sintaxeData)
                        End Set
                    End Property

                    Public Property dtDesligamento() As Date Implements Interfaces.iCTR.Primitivas.iUsuario.dtDesligamento
                        Get
                            Return Parent.var("dtDesligamento").value
                        End Get
                        Set(ByVal Value As Date)
                            Parent.var("dtDesligamento").Dirty = True
                            Parent.var("dtDesligamento").value = Value
                        End Set
                    End Property

                    Public Property idUsuarioCadastrador() As Integer Implements Interfaces.iCTR.Primitivas.iUsuario.idUsuarioCadastrador
                        Get
                            Return Parent.var("idUsuarioCadastrador").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("idUsuarioCadastrador").Dirty = True
                            Parent.var("idUsuarioCadastrador").value = Value
                        End Set
                    End Property

                    Public Property idEmpresa() As String Implements Interfaces.iCTR.Primitivas.iUsuario.idEmpresa
                        Get
                            Return Parent.var("idEmpresa").value
                        End Get
                        Set(ByVal Value As String)
                            Parent.var("idEmpresa").Dirty = True
                            Parent.var("idEmpresa").value = Value
                        End Set
                    End Property

                    Public Property idEntidade() As Integer Implements Interfaces.iCTR.Primitivas.iUsuario.idEntidade
                        Get
                            Return Parent.var("idEntidade").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("idEntidade").Dirty = True
                            Parent.var("idEntidade").value = Value
                        End Set
                    End Property

                    Public Property matricula() As String Implements Interfaces.iCTR.Primitivas.iUsuario.matricula
                        Get
                            Return Parent.var("matricula").value
                        End Get
                        Set(ByVal Value As String)
                            Parent.var("matricula").Dirty = True
                            Parent.var("matricula").value = Value
                        End Set
                    End Property
#End Region

                End Class

            End Class
            Public Class UsuarioConfig
                Inherits allClass
                Private mCampos As UsuarioConfigCampos 'Interfaces.iCTR.Primitivas.iUsuarioConfig

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing, False)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pSelf, "CTRL_UsuarioConfig", pFields)
                    mCampos = New UsuarioConfigCampos(Me)
                End Sub
                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_UsuarioConfig", TipoConexao, pFields, pManterConexaoAberta)
                    mCampos = New UsuarioConfigCampos(Me)
                End Sub

                Public Property Campos() As Interfaces.iCTR.Primitivas.iUsuarioConfig
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iUsuarioConfig)
                        mCampos = Value
                        If Not Value Is Nothing Then
                            MyBase.toObject = mCampos.Parent
                        End If
                    End Set
                End Property
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub

                Friend Class UsuarioConfigCampos
                    Implements Interfaces.iCTR.Primitivas.iUsuarioConfig

                    Private mParent As allClass

                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return Me.mParent
                        End Get
                        Set(ByVal Value As allClass)
                            Me.mParent = Value
                        End Set
                    End Property
                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        Parent = Nothing
                    End Sub
                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Try
                            Dim filtro As String
                            filtro = " idUsuario = " & Me.idUsuario_key
                            Me.Parent.SalvarPadrao(noCommit, filtro)

                            'Finaliza a Transação
                            Parent.Self.AdmDB.FinalizaTransaction(noCommit)

                        Catch ex As Exception
                            Dim mNBEx As New NBexception("Não foi possível Salvar as Configurações do Usuário - " & Me.idUsuario_key.ToString, ex)
                            mNBEx.Source = "NBdbm.Fachadas.CTR.primitivas.UsuarioConfig.Salvar"
                            Throw mNBEx
                        End Try

                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub
                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub
                    Public Sub AtivaDesativa(ByVal ativa As Boolean) Implements Interfaces.iCTR.Primitivas.iUsuarioConfig.AtivaDesativa
                        Try
                            Me.UsuarioAtivo = ativa
                            Me.Salvar()
                        Catch ex As NBexception
                            ex.Source = "**** Configurações do Usuário ****"
                            Throw ex
                        End Try
                    End Sub

#Region "   Propriedades - Fields   "

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.idUsuario_key.ToString
                        End Get
                    End Property


                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("idUsuarioConfig").value
                        End Get
                    End Property

                    Public Property idUsuario_key() As Integer Implements Interfaces.iCTR.Primitivas.iUsuarioConfig.idUsuario_key
                        Get
                            Return Parent.var("idUsuario").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("idUsuario").Dirty = True
                            Parent.var("idUsuario").value = Value
                        End Set
                    End Property

                    Public Property funcao() As String Implements Interfaces.iCTR.Primitivas.iUsuarioConfig.Funcao
                        Get
                            Return Parent.var("funcao").value
                        End Get
                        Set(ByVal Value As String)
                            Parent.var("funcao").Dirty = True
                            Parent.var("funcao").value = Value
                        End Set
                    End Property

                    Public Property UsuarioAtivo() As Boolean Implements Interfaces.iCTR.Primitivas.iUsuarioConfig.UsuarioAtivo
                        Get
                            Return Parent.var("ativo").value
                        End Get
                        Set(ByVal Value As Boolean)
                            Parent.var("ativo").Dirty = True
                            Parent.var("ativo").value = Value
                        End Set
                    End Property

                    Public Property pmLer() As Boolean Implements Interfaces.iCTR.Primitivas.iUsuarioConfig.pmLer
                        Get
                            Return Parent.var("pmler").value
                        End Get
                        Set(ByVal Value As Boolean)
                            Parent.var("pmler").Dirty = True
                            Parent.var("pmler").value = Value
                        End Set
                    End Property

                    Public Property pmEditar() As Boolean Implements Interfaces.iCTR.Primitivas.iUsuarioConfig.pmEditar
                        Get
                            Return Parent.var("pmEditar").value
                        End Get
                        Set(ByVal Value As Boolean)
                            Parent.var("pmEditar").Dirty = True
                            Parent.var("pmEditar").value = Value
                        End Set
                    End Property

                    Public Property pmIncluir() As Boolean Implements Interfaces.iCTR.Primitivas.iUsuarioConfig.pmIncluir
                        Get
                            Return Parent.var("pmIncluir").value
                        End Get
                        Set(ByVal Value As Boolean)
                            Parent.var("pmIncluir").Dirty = True
                            Parent.var("pmIncluir").value = Value
                        End Set
                    End Property

                    Public Property pmExcluir() As Boolean Implements Interfaces.iCTR.Primitivas.iUsuarioConfig.pmExcluir
                        Get
                            Return Parent.var("pmExcluir").value
                        End Get
                        Set(ByVal Value As Boolean)
                            Parent.var("pmExcluir").Dirty = True
                            Parent.var("pmExcluir").value = Value
                        End Set
                    End Property

                    Public Property pmSisExecutavel() As Boolean Implements Interfaces.iCTR.Primitivas.iUsuarioConfig.pmSisExecutavel
                        Get
                            Return Parent.var("pmSisExecutavel").value
                        End Get
                        Set(ByVal Value As Boolean)
                            Parent.var("pmSisExecutavel").Dirty = True
                            Parent.var("pmSisExecutavel").value = Value
                        End Set
                    End Property

                    Public Property pmSisWeb() As Boolean Implements Interfaces.iCTR.Primitivas.iUsuarioConfig.pmSisWeb
                        Get
                            Return Parent.var("pmSisWeb").value
                        End Get
                        Set(ByVal Value As Boolean)
                            Parent.var("pmSisWeb").Dirty = True
                            Parent.var("pmSisWeb").value = Value
                        End Set
                    End Property

                    Public Property xmlConfig() As System.Text.StringBuilder Implements Interfaces.iCTR.Primitivas.iUsuarioConfig.xmlConfig
                        Get
                            Return Parent.var("xmlConfig").value
                        End Get
                        Set(ByVal Value As System.Text.StringBuilder)
                            Parent.var("xmlConfig").Dirty = True
                            Parent.var("xmlConfig").value = Value.ToString
                        End Set
                    End Property

                    Public Property Credencial() As Byte Implements Interfaces.iCTR.Primitivas.iUsuarioConfig.Credencial
                        Get
                            Return Parent.var("Credencial").value
                        End Get
                        Set(ByVal Value As Byte)
                            Parent.var("Credencial").Dirty = True
                            Parent.var("Credencial").value = Value
                        End Set
                    End Property

#End Region

                End Class
            End Class
            Public Class HistoricoLogin
                Inherits allClass
                Private mCampos As HistoricoLoginCampos 'Interfaces.iCTR.Primitivas.iHistoricoLogin

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing, False)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pSelf, "CTRL_HistoricoLogin", pFields)
                    mCampos = New HistoricoLoginCampos(Me)
                End Sub
                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_HistoricoLogin", TipoConexao, pFields, pManterConexaoAberta)
                    mCampos = New HistoricoLoginCampos(Me)
                End Sub

                Public Property Campos() As Interfaces.iCTR.Primitivas.iHistoricoLogin
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iHistoricoLogin)
                        mCampos = Value
                        If Not Value Is Nothing Then
                            MyBase.toObject = mCampos.Parent
                        End If
                    End Set
                End Property
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub

                Private Class HistoricoLoginCampos
                    Implements Interfaces.iCTR.Primitivas.iHistoricoLogin

                    Private mParent As allClass

                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return Me.mParent
                        End Get
                        Set(ByVal Value As allClass)
                            Me.mParent = Value
                        End Set
                    End Property
                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        Parent = Nothing
                    End Sub
                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Dim filtro As String
                        filtro = " IdHistoricoLogin = " & Me.ID
                        Me.Parent.SalvarPadrao(noCommit, filtro)
                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub
                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub

#Region "   Propriedades - Fields   "

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.ID.ToString
                        End Get
                    End Property

                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("idHistoricoLogin").value
                        End Get
                    End Property

                    Public Property dtHoraLogoff() As Date Implements Interfaces.iCTR.Primitivas.iHistoricoLogin.dtHoraLogoff
                        Get
                            Return Parent.var("dtHoraLogOff").value
                        End Get
                        Set(ByVal Value As Date)
                            Parent.var("dtHoraLogOff").Dirty = True
                            Parent.var("dtHoraLogOff").value = Value
                        End Set
                    End Property

                    Public Property dtHoraLogon() As Date Implements Interfaces.iCTR.Primitivas.iHistoricoLogin.dtHoraLogon
                        Get
                            Return Parent.var("dtHoraLogon").value
                        End Get
                        Set(ByVal Value As Date)
                            Parent.var("dtHoraLogOff").Dirty = True
                            Parent.var("dtHoraLogOff").value = Value
                        End Set
                    End Property

                    Public Property nomeLogin() As String Implements Interfaces.iCTR.Primitivas.iHistoricoLogin.nomeLogin
                        Get
                            Return Parent.var("nomeLogin").value
                        End Get
                        Set(ByVal Value As String)
                            Parent.var("nomeLogin").Dirty = True
                            Parent.var("nomeLogin").value = Value
                        End Set
                    End Property

                    Public Property nomeMaquina() As String Implements Interfaces.iCTR.Primitivas.iHistoricoLogin.nomeMaquina
                        Get
                            Return Parent.var("nomeMaquina").value
                        End Get
                        Set(ByVal Value As String)
                            Parent.var("nomeMaquina").Dirty = True
                            Parent.var("nomeMaquina").value = Value
                        End Set
                    End Property
#End Region

                End Class
            End Class
            Public Class HistoricoTabela
                Inherits allClass
                Private mCampos As HistoricoTBCampos 'Interfaces.iCTR.Primitivas.iHistoricoTabela

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing, False)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pself, "CTRL_HistoricoTB", pFields)
                    mCampos = New HistoricoTBCampos(Me)
                End Sub
                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_HistoricoTB", TipoConexao, pFields, pManterConexaoAberta)
                    mCampos = New HistoricoTBCampos(Me)
                End Sub

                Public Property Campos() As Interfaces.iCTR.Primitivas.iHistoricoTabela
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iHistoricoTabela)
                        mCampos = Value
                        If Not Value Is Nothing Then
                            MyBase.toObject = mCampos.Parent
                        End If
                    End Set
                End Property
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub

                Private Class HistoricoTBCampos
                    Implements NBdbm.Interfaces.iCTR.Primitivas.iHistoricoTabela

                    Private mParent As allClass
                    Private mColFields As Collection

                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return Me.mParent
                        End Get
                        Set(ByVal Value As allClass)
                            Me.mParent = Value
                        End Set
                    End Property
                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        Parent = Nothing
                    End Sub
                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Parent.inserir(noCommit)
                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub
                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub

#Region "   Propriedades - Fields   "

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.idAutoNum_key.ToString & Me.tbLog_key.ToString
                        End Get
                    End Property

                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("idHistoricoTB").value
                        End Get
                    End Property

                    Public Property idAutoNum_key() As Integer Implements Interfaces.iCTR.Primitivas.iHistoricoTabela.idAutoNum_key
                        Get
                            Return Parent.var("idAutoNum").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("idAutoNum").Dirty = True
                            Parent.var("idAutoNum").value = Value
                        End Set
                    End Property

                    Public WriteOnly Property colFields() As Microsoft.VisualBasic.Collection Implements Interfaces.iCTR.Primitivas.iHistoricoTabela.colFields
                        Set(ByVal Value As Microsoft.VisualBasic.Collection)
                            mColFields = Value
                        End Set
                    End Property

                    Public Property dtHistorico() As Date Implements Interfaces.iCTR.Primitivas.iHistoricoTabela.dtHistorico
                        Get
                            Return Parent.var("dtHistorico").value
                        End Get
                        Set(ByVal Value As Date)
                            Parent.var("dtHistorico").Dirty = True
                            Parent.var("dtHistorico").value = Value
                        End Set
                    End Property

                    Public Property idEntidade() As Integer Implements Interfaces.iCTR.Primitivas.iHistoricoTabela.idEntidade
                        Get
                            Return Parent.var("idEntidade").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("idEntidade").Dirty = True
                            Parent.var("idEntidade").value = Value
                        End Set
                    End Property

                    Public Property idUsuario() As Integer Implements Interfaces.iCTR.Primitivas.iHistoricoTabela.idUsuario
                        Get
                            Return Parent.var("idUsuario").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("idUsuario").Dirty = True
                            Parent.var("idUsuario").value = Value
                        End Set
                    End Property

                    Public Property tbLog_key() As String Implements Interfaces.iCTR.Primitivas.iHistoricoTabela.tbLog_key
                        Get
                            Return Parent.var("tb_Log").value
                        End Get
                        Set(ByVal Value As String)
                            Parent.var("tb_Log").Dirty = True
                            Parent.var("tb_Log").value = Value
                        End Set
                    End Property

                    Public ReadOnly Property xmlLog() As String Implements Interfaces.iCTR.Primitivas.iHistoricoTabela.xmlLog
                        Get
                            Return Parent.xmlColFields(mColFields)
                        End Get

                    End Property
#End Region

                End Class
            End Class
            Public Class No
                Inherits allClass
                Private mCampos As NoCampos 'Interfaces.iCTR.Primitivas.iNo

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing, False)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pSelf, "CTRL_Nos", pFields)
                    mCampos = New NoCampos(Me)
                End Sub
                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_Nos", TipoConexao, pFields, pManterConexaoAberta)
                    mCampos = New NoCampos(Me)
                End Sub

                Public Property Campos() As Interfaces.iCTR.Primitivas.iNo
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iNo)
                        mCampos = Value
                        If Not Value Is Nothing Then
                            MyBase.toObject = mCampos.Parent
                        End If
                    End Set
                End Property
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub

                Friend Class NoCampos
                    Implements Interfaces.iCTR.Primitivas.iNo

                    Private mParent As allClass

                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return Me.mParent
                        End Get
                        Set(ByVal Value As allClass)
                            Me.mParent = Value
                        End Set
                    End Property
                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        mParent = Nothing
                    End Sub

                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Throw New NBexception("Somente a NBArvore Grava, Altera ou Exclui dados na Tabela de Nós.")
                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub
                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub

#Region "   Propriedades - Fields   "

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.idNo_key.ToString & Me.xmPath_key.ToString
                        End Get
                    End Property

                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("IDNo").value
                        End Get
                    End Property

                    Public Property idNo_key() As Integer Implements Interfaces.iCTR.Primitivas.iNo.idNo_key
                        Get
                            Return Parent.var("IDNo").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("IDNo").value = Value
                            Parent.var("IDNo").Dirty = True
                        End Set
                    End Property

                    Public Property filhos() As String Implements Interfaces.iCTR.Primitivas.iNo.filhos
                        Get
                            Return Parent.var("filhos").value
                        End Get
                        Set(ByVal Value As String)
                            Parent.var("filhos").value = Value
                            Parent.var("filhos").Dirty = True
                        End Set
                    End Property

                    Public Property indice() As String Implements Interfaces.iCTR.Primitivas.iNo.indice
                        Get
                            Return Parent.var("Indice").value
                        End Get
                        Set(ByVal Value As String)
                            Parent.var("Indice").value = Value
                            Parent.var("Indice").Dirty = True
                        End Set
                    End Property

                    Public Property nome() As String Implements Interfaces.iCTR.Primitivas.iNo.nome
                        Get
                            Return Parent.var("nome").value
                        End Get
                        Set(ByVal Value As String)
                            Parent.var("nome").value = Value
                            Parent.var("nome").Dirty = True
                        End Set
                    End Property

                    Public Property xmPath_key() As String Implements Interfaces.iCTR.Primitivas.iNo.xmPath_key
                        Get
                            Return Parent.var("xmPath").value
                        End Get
                        Set(ByVal Value As String)
                            Parent.var("xmPath").value = Value
                            Parent.var("xmPath").Dirty = True
                        End Set
                    End Property

#End Region

                End Class

            End Class
            Public Class LinkUsuarioNo
                Inherits allClass
                Private mCampos As LinkUsuarioNoCampos 'Interfaces.iCTR.Primitivas.iLinkUsuarioNo

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing, False)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pSelf, "CTRL_Link_usuario_No", pFields)
                    mCampos = New LinkUsuarioNoCampos(Me)
                End Sub
                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_Link_usuario_No", TipoConexao, pFields, pManterConexaoAberta)
                    mCampos = New LinkUsuarioNoCampos(Me)
                End Sub

                Public Property Campos() As Interfaces.iCTR.Primitivas.iLinkUsuarioNo
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iLinkUsuarioNo)
                        mCampos = Value
                        If Not Value Is Nothing Then
                            MyBase.toObject = mCampos.Parent
                        End If
                    End Set
                End Property
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub

                Friend Class LinkUsuarioNoCampos
                    Implements Interfaces.iCTR.Primitivas.iLinkUsuarioNo

                    Private mParent As allClass
                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return Me.mParent
                        End Get
                        Set(ByVal Value As allClass)
                            Me.mParent = Value
                        End Set
                    End Property
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub
                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub
                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Dim filtro As String
                        filtro = " IdLink_Usuario_No = " & Me.ID
                        Me.Parent.SalvarPadrao(noCommit, filtro)
                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub
                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        mParent = Nothing
                    End Sub

#Region "   Propriedades - Fields   "

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.ID.ToString
                        End Get
                    End Property

                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("IdLink_Usuario_No").value
                        End Get
                    End Property

                    Public Property idNo() As Integer Implements Interfaces.iCTR.Primitivas.iLinkUsuarioNo.idNo
                        Get
                            Return Parent.var("IdNo").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("IdNo").value = Value
                            Parent.var("IdNo").Dirty = True
                        End Set
                    End Property

                    Public Property idUsuario() As Integer Implements Interfaces.iCTR.Primitivas.iLinkUsuarioNo.idUsuario
                        Get
                            Return Parent.var("IdUsuario").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("IdUsuario").value = Value
                            Parent.var("IdUsuario").Dirty = True
                        End Set
                    End Property
#End Region

                End Class

            End Class
            Public Class LinkEntidadeNo
                Inherits allClass
                Private mCampos As LinkEntidadeNoCampos 'Interfaces.iCTR.Primitivas.iLinkEntidadeNo

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing, False)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pSelf, "CTRL_Link_Entidade_No", pFields)
                    mCampos = New LinkEntidadeNoCampos(Me)
                End Sub
                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_Link_Entidade_No", TipoConexao, pFields, pManterConexaoAberta)
                    mCampos = New LinkEntidadeNoCampos(Me)
                End Sub

                Public Property Campos() As Interfaces.iCTR.Primitivas.iLinkEntidadeNo
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iLinkEntidadeNo)
                        mCampos = Value
                        If Not Value Is Nothing Then
                            MyBase.toObject = mCampos.Parent
                        End If
                    End Set
                End Property
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub

                Private Class LinkEntidadeNoCampos
                    Implements Interfaces.iCTR.Primitivas.iLinkEntidadeNo

                    Private mParent As allClass
                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return Me.mParent
                        End Get
                        Set(ByVal Value As allClass)
                            Me.mParent = Value
                        End Set
                    End Property
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub

                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub

                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Dim filtro As String
                        filtro = " idEntidade = " & Me.idEntidade & " and idNo = " & Me.idNo
                        Me.Parent.SalvarPadrao(noCommit, filtro)
                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub

                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        mParent = Nothing
                    End Sub

#Region "   Propriedades - Fields   "

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.ID.ToString
                        End Get
                    End Property

                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("IdLink_Entidade_No").value
                        End Get
                    End Property

                    Public Property idEntidade() As Integer Implements Interfaces.iCTR.Primitivas.iLinkEntidadeNo.idEntidade
                        Get
                            Return Parent.var("IdEntidade").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("IdEntidade").value = Value
                            Parent.var("IdEntidade").Dirty = True
                        End Set
                    End Property

                    Public Property idNo() As Integer Implements Interfaces.iCTR.Primitivas.iLinkEntidadeNo.idNo
                        Get
                            Return Parent.var("IdNo").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("IdNo").value = Value
                            Parent.var("IdNo").Dirty = True
                        End Set
                    End Property
#End Region


                End Class
            End Class
            Public Class LinkEntidadeUsuario
                Inherits allClass
                Private mCampos As LinkEntidadeUsuarioCampos ' Interfaces.iCTR.Primitivas.iLinkEntidadeUsuario

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing, False)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pSelf, "CTRL_Link_Usuario_Entidade", pFields)
                    mCampos = New LinkEntidadeUsuarioCampos(Me)
                End Sub
                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_Link_Usuario_Entidade", TipoConexao, pFields, pManterConexaoAberta)
                    mCampos = New LinkEntidadeUsuarioCampos(Me)
                End Sub

                Public Property Campos() As Interfaces.iCTR.Primitivas.iLinkEntidadeUsuario
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iLinkEntidadeUsuario)
                        mCampos = Value
                        If Not Value Is Nothing Then
                            MyBase.toObject = mCampos.Parent
                        End If
                    End Set
                End Property
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub

                Friend Class LinkEntidadeUsuarioCampos
                    Implements Interfaces.iCTR.Primitivas.iLinkEntidadeUsuario
                    Private mParent As allClass
                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return Me.mParent
                        End Get
                        Set(ByVal Value As allClass)
                            Me.mParent = Value
                        End Set
                    End Property
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub

                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub

                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Dim filtro As String
                        filtro = " IdLink_Entidade_Usuario = " & Me.ID
                        Me.Parent.SalvarPadrao(noCommit, filtro)
                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub

                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        mParent = Nothing
                    End Sub

#Region "   Propriedades - Fields   "

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.ID.ToString
                        End Get
                    End Property

                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("IdLink_Entidade_Usuario").value
                        End Get
                    End Property

                    Public Property idEntidade() As Integer Implements Interfaces.iCTR.Primitivas.iLinkEntidadeUsuario.idEntidade
                        Get
                            Return Parent.var("IdEntidade").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("IdEntidade").value = Value
                            Parent.var("IdEntidade").Dirty = True
                        End Set
                    End Property

                    Public Property idUsuario() As Integer Implements Interfaces.iCTR.Primitivas.iLinkEntidadeUsuario.idUsuario
                        Get
                            Return Parent.var("idUsuario").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("idUsuario").value = Value
                            Parent.var("idusuario").Dirty = True
                        End Set
                    End Property

#End Region

                End Class
            End Class
            Public Class LinkEntidadePlx
                Inherits allClass
                Private mCampos As LinkEntidadePlxCampos 'Interfaces.iCTR.Primitivas.iLinkEntidadePlx

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing, False)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pSelf, "CTRL_Link_Entidade_PLX", pFields)
                    mCampos = New LinkEntidadePlxCampos(Me)
                End Sub
                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_Link_Entidade_PLX", TipoConexao, pFields, pManterConexaoAberta)
                    mCampos = New LinkEntidadePlxCampos(Me)
                End Sub

                Public Property Campos() As Interfaces.iCTR.Primitivas.iLinkEntidadePlx
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iLinkEntidadePlx)
                        mCampos = Value
                    End Set
                End Property
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub

                Friend Class LinkEntidadePlxCampos
                    Implements Interfaces.iCTR.Primitivas.iLinkEntidadePlx
                    Private mParent As allClass

                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return Me.mParent
                        End Get
                        Set(ByVal Value As allClass)
                            Me.mParent = Value
                        End Set
                    End Property
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub

                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub

                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Dim filtro As String
                        filtro = " IdLink = " & Me.ID
                        Me.Parent.SalvarPadrao(noCommit, filtro)
                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub

                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        mParent = Nothing
                    End Sub

#Region "   Propriedades - Fields   "

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.idEntidade_key.ToString & Me.Plx_key.ToString
                        End Get
                    End Property

                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("IdLink").value
                        End Get
                    End Property

                    Public Property idEntidade_key() As Integer Implements Interfaces.iCTR.Primitivas.iLinkEntidadePlx.idEntidade_key
                        Get
                            Return Parent.var("idEntidade").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("idEntidade").value = Value
                            Parent.var("idEntidade").Dirty = True
                        End Set
                    End Property

                    Public Property idAutoNumPlx() As Integer Implements Interfaces.iCTR.Primitivas.iLinkEntidadePlx.idAutoNumPlx_Key
                        Get
                            Return Parent.var("idAutoNumPlx").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("idAutoNumPlx").value = Value
                            Parent.var("idAutoNumPlx").Dirty = True
                        End Set
                    End Property

                    Public Property Plx_key() As String Implements Interfaces.iCTR.Primitivas.iLinkEntidadePlx.Plx_key
                        Get
                            Return Parent.var("PLX").value
                        End Get
                        Set(ByVal Value As String)
                            Parent.var("PLX").value = Value
                            Parent.var("PLX").Dirty = True
                        End Set
                    End Property
#End Region

                End Class
            End Class
            Public Class LinkEntidadeEntidade
                Inherits allClass
                Private mCampos As LinkEntidadeEntidadeCampos 'Interfaces.iCTR.Primitivas.iLinkEntidadeEntidade

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing, False)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pSelf, "CTRL_LinkEntidadeEntidade", pFields)
                    mCampos = New LinkEntidadeEntidadeCampos(Me)
                End Sub
                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_LinkEntidadeEntidade", TipoConexao, pFields, pManterConexaoAberta)
                    mCampos = New LinkEntidadeEntidadeCampos(Me)
                End Sub

                Public Property Campos() As Interfaces.iCTR.Primitivas.iLinkEntidadeEntidade
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iLinkEntidadeEntidade)
                        mCampos = Value
                        If Not Value Is Nothing Then
                            MyBase.toObject = mCampos.Parent
                        End If
                    End Set
                End Property
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub

                Private Class LinkEntidadeEntidadeCampos
                    Implements Interfaces.iCTR.Primitivas.iLinkEntidadeEntidade
                    Private mParent As allClass

                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return Me.mParent
                        End Get
                        Set(ByVal Value As allClass)
                            Me.mParent = Value
                        End Set
                    End Property
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub

                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub

                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Dim filtro As String
                        filtro = " IdRelacionamento = " & Me.ID
                        Me.Parent.SalvarPadrao(noCommit, filtro)
                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub

                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        mParent = Nothing
                    End Sub

#Region "   Propriedades - Fields   "

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.idEntidadeBase_key.ToString
                        End Get
                    End Property

                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("idLinkEntidadeEntidade").value
                        End Get
                    End Property

                    Public Property idEntidadeBase_key() As Integer Implements Interfaces.iCTR.Primitivas.iLinkEntidadeEntidade.idEntidadeBase_key
                        Get
                            Return Parent.var("idEntidadeBase").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("idEntidadeBase").value = Value
                            Parent.var("idEntidadeBase").Dirty = True
                        End Set
                    End Property

                    Public Property idAutoNumPlx() As Integer Implements Interfaces.iCTR.Primitivas.iLinkEntidadeEntidade.idEntidadeLink_Key
                        Get
                            Return Parent.var("idEntidadeLink").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("idEntidadeLink").value = Value
                            Parent.var("idEntidadeLink").Dirty = True
                        End Set
                    End Property

                    Public Property grauRelacionamento() As String Implements Interfaces.iCTR.Primitivas.iLinkEntidadeEntidade.grauRelacionamento
                        Get
                            Return Parent.var("grauRelacionamento").value
                        End Get
                        Set(ByVal Value As String)
                            Parent.var("grauRelacionamento").value = Value
                            Parent.var("grauRelacionamento").Dirty = True
                        End Set
                    End Property
#End Region

                End Class
            End Class
            Public Class Spool
                Inherits allClass
                Private mCampos As spoolCampos 'Interfaces.iCTR.Primitivas.iSpool

                Public Sub New(ByRef pSelf As self)
                    Me.New(pSelf, tipos.tiposConection.Default_, Nothing, False)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByRef pFields As Fields)
                    MyBase.New(pSelf, "CTRL_Spool", pFields)
                    mCampos = New spoolCampos(Me)
                End Sub
                Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                    Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                End Sub
                Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                    MyBase.New(pSelf, "CTRL_Spool", TipoConexao, pFields, pManterConexaoAberta)
                    mCampos = New spoolCampos(Me)
                End Sub

                Public Property Campos() As Interfaces.iCTR.Primitivas.iSpool
                    Get
                        Return mCampos
                    End Get
                    Set(ByVal Value As Interfaces.iCTR.Primitivas.iSpool)
                        mCampos = Value
                        If Not Value Is Nothing Then
                            MyBase.toObject = mCampos.Parent
                        End If
                    End Set
                End Property
                Public Overrides Sub Dispose()
                    If Not IsNothing(mCampos) Then mCampos.Dispose()
                    mCampos = Nothing
                    MyBase.Dispose()
                End Sub

                Friend Class spoolCampos
                    Implements Interfaces.iCTR.Primitivas.iSpool
                    Private mParent As allClass

                    Public Sub New(ByRef allClass As allClass)
                        Me.mParent = allClass
                    End Sub
                    Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                        Get
                            Return Me.mParent
                        End Get
                        Set(ByVal Value As allClass)
                            Me.mParent = Value
                        End Set
                    End Property
                    Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                        Parent.Clear_filters()
                    End Sub
                    Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                        Parent.Clear_vars()
                    End Sub
                    Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Dim filtro As String
                        filtro = " idSpool = " & Me.ID
                        Me.Parent.SalvarPadrao(noCommit, filtro)
                    End Sub
                    Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                        Me.Salvar(False)
                    End Sub
                    Public Sub Dispose() Implements System.IDisposable.Dispose

                        mParent = Nothing
                    End Sub

#Region "   Propriedades - Fields   "

                    Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                        Get
                            Return Me.ID.ToString
                        End Get
                    End Property

                    Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                        Get
                            Return Parent.var("idSpool").value
                        End Get
                    End Property

                    Public Property idAutoNum() As Integer Implements Interfaces.iCTR.Primitivas.iSpool.idAutoNum
                        Get
                            Return Parent.var("idAutoNum").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("idAutoNum").Dirty = True
                            Parent.var("idAutoNum").value = Value
                        End Set
                    End Property

                    Public Property impressa() As Boolean Implements Interfaces.iCTR.Primitivas.iSpool.impressa
                        Get
                            Dim retorno As Boolean
                            If Parent.var("impressa").value = 1 Then
                                retorno = True
                            Else
                                retorno = False
                            End If
                            Return retorno
                        End Get
                        Set(ByVal Value As Boolean)
                            Parent.var("impressa").value = Value
                            Parent.var("impressa").Dirty = True
                        End Set
                    End Property

                    Public Property quantidade() As Integer Implements Interfaces.iCTR.Primitivas.iSpool.quantidade
                        Get
                            Return Parent.var("quantidade").value
                        End Get
                        Set(ByVal Value As Integer)
                            Parent.var("quantidade").Dirty = True
                            Parent.var("quantidade").value = Value
                        End Set
                    End Property

                    Public Property tabela() As String Implements Interfaces.iCTR.Primitivas.iSpool.tabela
                        Get
                            Return Parent.var("tabela").value
                        End Get
                        Set(ByVal Value As String)
                            Parent.var("tabela").Dirty = True
                            Parent.var("tabela").value = Value
                        End Set
                    End Property

                    Public Property xmlCSS() As String Implements Interfaces.iCTR.Primitivas.iSpool.xmlCSS
                        Get
                            Return Parent.var("xmlcss").value
                        End Get
                        Set(ByVal Value As String)
                            Parent.var("xmlcss").Dirty = True
                            Parent.var("xmlcss").value = Value
                        End Set
                    End Property

                    Public Property xmlInfo() As String Implements Interfaces.iCTR.Primitivas.iSpool.xmlInfo
                        Get
                            Return Parent.var("xmlInfo").value
                        End Get
                        Set(ByVal Value As String)
                            Parent.var("xmlInfo").Dirty = True
                            Parent.var("xmlInfo").value = Value
                        End Set
                    End Property
#End Region

                End Class
            End Class

        End Namespace

    End Namespace

End Namespace










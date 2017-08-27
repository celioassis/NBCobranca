Option Explicit On

Namespace Fachadas.plxCOBR
    Public Class CadastroEntidade
        Inherits Fachadas.CTR.CadastroEntidade

        Protected aTipoEntidade As tipos.TipoEntidade
        Protected aColDividas As New Fachadas.NbCollection
        Protected aColAcionamentos As New Fachadas.NbCollection
        Protected aDivida As Fachadas.plxCOBR.primitivas.Divida
        Protected aAcionamentos As Fachadas.plxCOBR.primitivas.Acionamentos
        Protected aTarifas As Fachadas.plxCOBR.primitivas.Tarifas

        Private aAcionamentosFields As Fachadas.Fields
        Private aDividaFields As Fachadas.Fields
        Private aTarifasFields As Fachadas.Fields

        Public Sub New(ByVal TipoConexao As tipos.tiposConection, ByVal TipoEntidade As tipos.TipoEntidade, ByVal pManterConexaoAberta As Boolean)
            MyBase.New(TipoConexao, True)
            aTipoEntidade = TipoEntidade
            aDivida = New Fachadas.plxCOBR.primitivas.Divida(aSelf, TipoConexao, True)
            aDividaFields = aDivida.var
            aAcionamentos = New Fachadas.plxCOBR.primitivas.Acionamentos(aSelf, TipoConexao, True)
            aAcionamentosFields = aAcionamentos.var
            aTarifas = New Fachadas.plxCOBR.primitivas.Tarifas(aSelf, TipoConexao, pManterConexaoAberta)
            aTarifasFields = aTarifas.var
        End Sub

        Public ReadOnly Property colecaoDividas() As NbCollection
            Get
                Return aColDividas
            End Get
        End Property

        Public ReadOnly Property colecaoAcionamentos() As NbCollection
            Get
                Return Me.aColAcionamentos
            End Get
        End Property

        Public Sub NovaDivida()
            aDivida = New Fachadas.plxCOBR.primitivas.Divida(aSelf, aTipoConexao, False)
        End Sub

        Public Property Acionamentos() As Interfaces.iCOBR.Primitivas.iAcionamentos
            Get
                Return Me.aAcionamentos.Campos
            End Get
            Set(ByVal Value As Interfaces.iCOBR.Primitivas.iAcionamentos)
                Me.aAcionamentos.Campos = Value
            End Set
        End Property

        Public Property Divida() As Interfaces.iCOBR.Primitivas.iDivida
            Get
                Return Me.aDivida.Campos
            End Get
            Set(ByVal Value As Interfaces.iCOBR.Primitivas.iDivida)
                Me.aDivida.Campos = Value
            End Set
        End Property

        Public Property Tarifa() As Interfaces.iCOBR.Primitivas.iTarifas
            Get
                Return Me.aTarifas.Campos
            End Get
            Set(ByVal Value As Interfaces.iCOBR.Primitivas.iTarifas)
                Me.aTarifas.Campos = Value
            End Set
        End Property

        Public Shadows Sub Salvar(ByVal noCommit As Boolean)
            Try
                'Salvando a Entidade
                MyBase.Salvar(True)

                'Salvando as Dividas
                If (Me.aTipoEntidade = tipos.TipoEntidade.Devedores) Then
                    For Each mDiv As Interfaces.iCOBR.Primitivas.iDivida In aColDividas.Values
                        mDiv.idEntidade = Me.Entidade.ID
                        mDiv.salvar(True)
                    Next
                End If

                'Salvando as Tarifas
                If (Me.aTipoEntidade = tipos.TipoEntidade.Clientes) Then
                    aTarifas.Campos.idEntidade = Me.Entidade.ID
                    aTarifas.Campos.salvar(True)
                End If

                'Finaliza a Transação
                aself.AdmDB.FinalizaTransaction(noCommit)

            Catch ex As Exception
                Dim mNBEx As New NBexception("Não foi possível Salvar o Cadastro de " & aTipoEntidade.ToString(), ex)
                mNBEx.Source = "NBdbm.Fachadas.plxCOBR.CadastroEntidade.Salvar"
                Throw mNBEx
            End Try
        End Sub

        Public Overloads Sub getFieldsFromEntidade(ByVal idEntidade As Double)
            If Not Me.aColDividas Is Nothing Then Me.aColDividas.Clear()
            If Not Me.aColAcionamentos Is Nothing Then Me.aColAcionamentos.Clear()
            Dim mManterConexaoAberta As Boolean = False

            If (Me.aTipoEntidade = tipos.TipoEntidade.Clientes Or Me.aTipoEntidade = tipos.TipoEntidade.Devedores) Then mManterConexaoAberta = True

            MyBase.getFromEntidade(idEntidade, mManterConexaoAberta)

            'Se a Entidade for um Devedor então busca as suas dividas.
            If Me.aTipoEntidade = tipos.TipoEntidade.Devedores Then

                'Busca os dados das dívidas
                Me.aDivida.filterWhere = "idEntidade = " & idEntidade
                Me.aColDividas = Me.aDivida.CriaColecaoFields(GetType(plxCOBR.primitivas.Divida.DividaCampos), True)

                'Busca os dados dos Acionamentos
                Me.aAcionamentos.filterWhere = "idEntidade = " & idEntidade
                Me.aAcionamentos.filterTop = 100
                Me.aAcionamentos.filterOrderBy = "ID desc"
                Me.aColAcionamentos = Me.aAcionamentos.CriaColecaoFields(GetType(plxCOBR.primitivas.Acionamentos.AcionamentosCampos), False)
                'Dim mSL As System.Collections.SortedList = New System.Collections.SortedList(Me.aColAcionamentos.Hastable)
                'If mSL.Count > 0 Then
                '    Me.aAcionamentos.Campos = mSL.GetByIndex(mSL.Count - 1)
                'End If
            End If

            'Se a Entidade for um Cliente então busca as tarifas.
            If Me.aTipoEntidade = tipos.TipoEntidade.Clientes Then
                Me.aTarifas.getFields("idEntidade = " & idEntidade, False)
            End If

        End Sub

        Public Overloads Sub Dispose()
            MyBase.Dispose()
            If Not aColDividas Is Nothing Then aColDividas.Dispose() : aColDividas = Nothing
            If Not aColAcionamentos Is Nothing Then aColAcionamentos.Dispose() : aColAcionamentos = Nothing
            If Not aDivida Is Nothing Then aDivida.Dispose() : aDivida = Nothing
            If Not aAcionamentos Is Nothing Then aAcionamentos.Dispose() : aAcionamentos = Nothing
            If Not aTarifas Is Nothing Then aTarifas.Dispose() : aTarifas = Nothing
            If Not IsNothing(aAcionamentosFields) Then aAcionamentosFields.Dispose() : aAcionamentosFields = Nothing
            If Not IsNothing(aDividaFields) Then aDividaFields.Dispose() : aDividaFields = Nothing
            If Not IsNothing(aTarifasFields) Then aTarifasFields.Dispose() : aTarifasFields = Nothing

        End Sub

        Public Function CriarAcionamentos() As Fachadas.plxCOBR.primitivas.Acionamentos
            Return CriarAcionamentos(Me.aAcionamentosFields)
        End Function
        Friend Function CriarAcionamentos(ByVal pFields As Fields) As Fachadas.plxCOBR.primitivas.Acionamentos
            If Not IsNothing(aTipoConexao) Then
                Me.aAcionamentos = New Fachadas.plxCOBR.primitivas.Acionamentos(aSelf, aTipoConexao, New Fields(aSelf, pFields), False)
            Else
                Me.aAcionamentos = New Fachadas.plxCOBR.primitivas.Acionamentos(aSelf, New Fields(aSelf, pFields))
            End If
            Return Me.aAcionamentos
        End Function

        Public Function CriarDivida() As Fachadas.plxCOBR.primitivas.Divida
            Return CriarDivida(Me.aDividaFields)
        End Function
        Friend Function CriarDivida(ByVal pFields As Fields) As Fachadas.plxCOBR.primitivas.Divida
            If Not IsNothing(aTipoConexao) Then
                Return New Fachadas.plxCOBR.primitivas.Divida(aSelf, aTipoConexao, pFields, False)
            Else
                Return New Fachadas.plxCOBR.primitivas.Divida(aSelf, pFields)
            End If
        End Function

    End Class

    Public Class CadastroAcionamentos
        Implements NBdbm.Interfaces.iCOBR.iCadastroAcionamentos

        Protected mCadDevedor As NBdbm.Fachadas.plxCOBR.CadastroEntidade
        Protected mColAcionamentosNovos As NbCollection
        Protected mTipoConexao As NBdbm.tipos.tiposConection
        Protected aSelf As self


        Public Sub New(ByVal pTipoConexao As tipos.tiposConection, ByVal pCadDevedor As NBdbm.Fachadas.plxCOBR.CadastroEntidade)
            mTipoConexao = pTipoConexao
            mCadDevedor = pCadDevedor
            mColAcionamentosNovos = New NbCollection
            aSelf = pCadDevedor.Self
        End Sub

        Public Property Acionamento() As Interfaces.iCOBR.Primitivas.iAcionamentos Implements Interfaces.iCOBR.iCadastroAcionamentos.Acionamento
            Get
                Return Me.mCadDevedor.Acionamentos
            End Get
            Set(ByVal Value As Interfaces.iCOBR.Primitivas.iAcionamentos)
                Me.mCadDevedor.Acionamentos = Value
            End Set
        End Property

        Public Sub NovoAcionamento()
            Me.mCadDevedor.CriarAcionamentos()
        End Sub

        Public ReadOnly Property ColecaoAcionamentos() As NbCollection Implements Interfaces.iCOBR.iCadastroAcionamentos.ColecaoAcionamentos
            Get
                Return Me.mCadDevedor.colecaoAcionamentos
            End Get
        End Property

        Public ReadOnly Property ColecaoNovosAcionamentos() As NbCollection Implements Interfaces.iCOBR.iCadastroAcionamentos.ColecaoNovosAcionamentos
            Get
                Return Me.mColAcionamentosNovos
            End Get
        End Property

        Public ReadOnly Property FichaDevedor() As CadastroEntidade Implements Interfaces.iCOBR.iCadastroAcionamentos.FichaDevedor
            Get
                Return Me.mCadDevedor
            End Get
        End Property

        Public Sub GetFieldsFromEntidade(ByVal pCodigoDevedor As Double)

            Me.mColAcionamentosNovos.Clear()
            Me.mCadDevedor.Dispose()
            Me.mCadDevedor = Nothing
            Me.mCadDevedor = New CadastroEntidade(Me.mTipoConexao, tipos.TipoEntidade.Devedores, False)
            Me.mCadDevedor.getFieldsFromEntidade(pCodigoDevedor)
        End Sub

        Public Overloads Sub Salvar() Implements Interfaces.iCOBR.iCadastroAcionamentos.Salvar
            Me.Salvar(False)
        End Sub

        Public Overloads Sub Salvar(ByVal NoCommit As Boolean) Implements Interfaces.iCOBR.iCadastroAcionamentos.Salvar
            Try
                aSelf.AdmDB.AbreTransaction()
                'Salvando os Acionamentos
                'Dim mColAcionamentosClassificas As New System.Collections.SortedList(Me.mColAcionamentosNovos.Hastable)
                For Each mAciona As Interfaces.iCOBR.Primitivas.iAcionamentos In mColAcionamentosNovos.Values
                    mAciona.idEntidade = Me.mCadDevedor.Entidade.ID
                    mAciona.salvar(True)
                Next

                'Finaliza a Transação
                aSelf.AdmDB.FinalizaTransaction(NoCommit)

                Me.ColecaoNovosAcionamentos.Clear()

            Catch ex As Exception
                Dim mNBEx As New NBexception("Não foi possível Salvar o Cadastro de Acionamentos", ex)
                mNBEx.Source = "NBdbm.Fachadas.plxCOBR.CadastroAcionamentos.Salvar"
                aSelf.AdmDB.CancelarTransaction()
                Throw mNBEx
            Finally
                If aSelf.AdmDB.connection.State = ConnectionState.Open Then
                    aSelf.AdmDB.connection.Close()
                End If
            End Try
        End Sub

        Public Overridable Sub Dispose() Implements System.IDisposable.Dispose
            mCadDevedor = Nothing

            If Not mColAcionamentosNovos Is Nothing Then
                mColAcionamentosNovos.Dispose()
                mColAcionamentosNovos = Nothing
            End If

        End Sub

    End Class

    Public Class LancamentoBaixa
        Implements Interfaces.iCOBR.iLacamentoBaixa

        Private mTipoConexao As tipos.tiposConection
        Private mDividasParaBaixar As NbCollection
        Private mBaixa As NBdbm.Fachadas.plxCOBR.primitivas.Baixas
        Protected aSelf As self

        Public Sub New(ByVal pTipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
            mTipoConexao = pTipoConexao
            mBaixa = New NBdbm.Fachadas.plxCOBR.primitivas.Baixas(aSelf, mTipoConexao, pManterConexaoAberta)
            mDividasParaBaixar = New NbCollection
            aSelf = mBaixa.Self
        End Sub
        Friend Sub New(ByVal pTipoConexao As tipos.tiposConection, ByVal pFields As Fields, ByVal pManterConexaoAberta As Boolean)
            mTipoConexao = pTipoConexao
            mBaixa = New primitivas.Baixas(aSelf, mTipoConexao, pFields, pManterConexaoAberta)
            mDividasParaBaixar = New NbCollection
            aSelf = mBaixa.Self
        End Sub
        Public Property Baixa() As Interfaces.iCOBR.Primitivas.iBaixas Implements Interfaces.iCOBR.iLacamentoBaixa.Baixa
            Get
                Return Me.mBaixa.Campos
            End Get
            Set(ByVal Value As Interfaces.iCOBR.Primitivas.iBaixas)
                Me.mBaixa.Campos = Value
            End Set
        End Property

        Public ReadOnly Property DividasParaBaixar() As NbCollection
            Get
                Return Me.mDividasParaBaixar
            End Get
        End Property

        Public Overloads Sub Salvar() Implements Interfaces.iCOBR.iLacamentoBaixa.Salvar
            Me.Salvar(False)
        End Sub

        Public Overloads Sub Salvar(ByVal NoCommit As Boolean) Implements Interfaces.iCOBR.iLacamentoBaixa.Salvar

            Try
                'Atualizando Dívidas que serão Baixadas
                For Each mmDivida As Interfaces.iCOBR.Primitivas.iDivida In mDividasParaBaixar.Values
                    'Salvando Baixas
                    Dim mmBaixa As New NBdbm.Fachadas.plxCOBR.primitivas.Baixas(aSelf)
                    With mmBaixa.Campos
                        .idDivida = mmDivida.ID
                        .idEntidade = mmDivida.idEntidade
                        .DataBaixa = mmDivida.DataBaixa
                        .BaixadoCliente = mmDivida.BaixaNoCliente
                        .NumBordero = mmDivida.BorderoBaixa

                        'Ocorre quando esta sendo feito uma Baixa parcial.
                        'Obs: Não é feito Multiplas Baixas Parcial, então esta condição
                        'só será válida quando estiver sendo feito a baixa de uma única
                        'dívida parcial.
                        If mmDivida.BaixaParcial Then
                            .ValorNominal = mBaixa.Campos.ValorNominal
                            .ValorBaixa = mBaixa.Campos.ValorBaixa
                            .ValorRecebido = mBaixa.Campos.ValorRecebido
                        Else
                            'Ocorre em baixas multiplas e que uma das dívidas 
                            'foi baixada parcialmente em outro processo de baixa.
                            If mmDivida.ValorNominal > mmDivida.ValorNominalParcial Then
                                .ValorNominal = mmDivida.ValorNominalParcial
                                .ValorBaixa = mmDivida.ValorNominalParcial
                                mmDivida.BaixaParcial = True
                            Else
                                .ValorNominal = mmDivida.ValorNominal
                                .ValorBaixa = mmDivida.ValorNominal
                            End If
                            'Valor Recebido sempre será o valor recebido da baixa
                            'dividido pelo número de dividas que estão sendo baixadas.
                            .ValorRecebido = mBaixa.Campos.ValorRecebido / mDividasParaBaixar.Count
                        End If
                        'Salva a baixa.
                        .salvar(False)
                    End With
                    'Atualiza o status da divida que esta sendo baixada.
                    mmDivida.salvar(False)
                Next

                'Finaliza a Transação
                aself.AdmDB.FinalizaTransaction(NoCommit)

            Catch ex As Exception
                Dim mNBEx As New NBexception("Não foi possível Salvar a Baixa", ex)
                mNBEx.Source = "NBdbm.Fachadas.plxCOBR.LancamentoBaixa.Salvar"
                Throw mNBEx
            End Try

        End Sub

        Public Function CriaNovoLancamento(ByVal pTipoConexao As tipos.tiposConection) As LancamentoBaixa
            Return New LancamentoBaixa(pTipoConexao, CType(mBaixa.var.Clone, Fields), False)
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            If Not Me.mBaixa Is Nothing Then
                Me.mBaixa.Dispose()
                Me.mBaixa = Nothing
            End If
            If Not Me.mDividasParaBaixar Is Nothing Then
                Me.mDividasParaBaixar.Dispose()
                Me.mDividasParaBaixar = Nothing
            End If
        End Sub

    End Class

    Namespace primitivas

#Region "===[ Tipo de Divida ]==="
        Public Class TipoDivida
            Inherits allClass
            Private mCampos As TipoDividaCampos 'Interfaces.iEVT.Primitivas.iLocalidades

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "COBR_TipoDivida")
                mCampos = New TipoDividaCampos(Me)
            End Sub
            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "COBR_TipoDivida", TipoConexao)
                mCampos = New TipoDividaCampos(Me)
            End Sub
            Public Property Campos() As Interfaces.iCOBR.Primitivas.iTipoDivida
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iCOBR.Primitivas.iTipoDivida)
                    mCampos = Value
                    If Not Value Is Nothing Then
                        MyBase.toObject = mCampos.Parent
                    End If
                End Set
            End Property

            Friend Class TipoDividaCampos
                Implements Interfaces.iCOBR.Primitivas.iTipoDivida

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
                    Dim filtro As String
                    filtro = " descricao = '" & Me.Descricao & "'"
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

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("ID").value
                    End Get
                End Property

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.ID.ToString & Me.Descricao
                    End Get
                End Property

                Public Property Descricao() As String Implements Interfaces.iCOBR.Primitivas.iTipoDivida.Descricao
                    Get
                        Return Parent.var("descricao").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("descricao").Dirty = True
                        Parent.var("descricao").value = Value
                    End Set
                End Property

#End Region

            End Class
        End Class
#End Region

#Region "===[ Tipo de Acionamento ]==="
        Public Class TipoAcionamento
            Inherits allClass
            Private mCampos As TipoAcionamentoCampos

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "COBR_TipoAcionamento")
                mCampos = New TipoAcionamentoCampos(Me)
            End Sub
            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "COBR_TipoAcionamento", TipoConexao)
                mCampos = New TipoAcionamentoCampos(Me)
            End Sub
            Public Property Campos() As Interfaces.iCOBR.Primitivas.iTipoAcionamento
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iCOBR.Primitivas.iTipoAcionamento)
                    mCampos = Value
                    If Not Value Is Nothing Then
                        MyBase.toObject = mCampos.Parent
                    End If
                End Set
            End Property

            Friend Class TipoAcionamentoCampos
                Implements Interfaces.iCOBR.Primitivas.iTipoAcionamento

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
                    Dim filtro As String
                    filtro = " descricao = '" & Me.Descricao & "'"
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

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("ID").value
                    End Get
                End Property

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.ID.ToString & Me.Descricao
                    End Get
                End Property

                Public Property Descricao() As String Implements Interfaces.iCOBR.Primitivas.iTipoAcionamento.Descricao
                    Get
                        Return Parent.var("descricao").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("descricao").Dirty = True
                        Parent.var("descricao").value = Value
                    End Set
                End Property

                Public Property DiasReacionamento() As Integer Implements Interfaces.iCOBR.Primitivas.iTipoAcionamento.DiasReacionamento
                    Get
                        Return Parent.var("DiasReacionamento").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("DiasReacionamento").Dirty = True
                        Parent.var("DiasReacionamento").value = Value
                    End Set
                End Property

                Public Property CredencialExigida() As Integer Implements Interfaces.iCOBR.Primitivas.iTipoAcionamento.CredencialExigida
                    Get
                        Return Parent.var("CredencialExigida").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("CredencialExigida").Dirty = True
                        Parent.var("CredencialExigida").value = Value
                    End Set
                End Property

#End Region

            End Class
        End Class
#End Region

#Region "===[ Divida ]==="
        Public Class Divida
            Inherits allClass
            Private mCampos As DividaCampos

            Public Sub New(ByRef pSelf As self)
                Me.New(pSelf, tipos.tiposConection.Default_, Nothing, False)
            End Sub
            Friend Sub New(ByRef pSelf As self, ByVal pFields As Fields)
                MyBase.New(pSelf, "COBR_Divida", pFields)
                mCampos = New DividaCampos(Me)
            End Sub
            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
            End Sub
            Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                MyBase.New(pSelf, "COBR_Divida", TipoConexao, pFields, pManterConexaoAberta)
                mCampos = New DividaCampos(Me)
            End Sub
            Public Property Campos() As Interfaces.iCOBR.Primitivas.iDivida
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iCOBR.Primitivas.iDivida)
                    mCampos = Value
                    If Not Value Is Nothing Then
                        MyBase.toObject = mCampos.Parent
                    End If
                End Set
            End Property

            Friend Class DividaCampos
                Implements Interfaces.iCOBR.Primitivas.iDivida

                Private mParent As allClass
                Private mValorNominalParcial As Double = 0

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
                    Dim filtro As String
                    filtro = " id = " & Me.ID.ToString & " And idEntidade = " & Me.idEntidade.ToString
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

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("ID").value
                    End Get
                End Property

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.XmPathCliente & Me.idTipoDivida.ToString("000000") & Me.DataVencimento.Ticks.ToString() & Me.Contrato & Me.NumDoc.ToString("000000")
                    End Get
                End Property

                Public Property BorderoBaixa() As Integer Implements Interfaces.iCOBR.Primitivas.iDivida.BorderoBaixa
                    Get
                        Return Parent.var("BorderoBaixa").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("BorderoBaixa").Dirty = True
                        Parent.var("BorderoBaixa").value = Value
                    End Set
                End Property

                Public Property DataBaixa() As Date Implements Interfaces.iCOBR.Primitivas.iDivida.DataBaixa
                    Get
                        Return Parent.var("DataBaixa").value
                    End Get
                    Set(ByVal Value As Date)
                        Parent.var("DataBaixa").Dirty = True
                        Parent.var("DataBaixa").value = Value
                    End Set
                End Property

                Public Property DataVencimento() As Date Implements Interfaces.iCOBR.Primitivas.iDivida.DataVencimento
                    Get
                        Return Parent.var("DataVencimento").value
                    End Get
                    Set(ByVal Value As Date)
                        Parent.var("DataVencimento").Dirty = True
                        Parent.var("DataVencimento").value = Value
                    End Set
                End Property

                Public Property idEntidade() As Integer Implements Interfaces.iCOBR.Primitivas.iDivida.idEntidade
                    Get
                        Return Parent.var("idEntidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idEntidade").Dirty = True
                        Parent.var("idEntidade").value = Value
                    End Set
                End Property

                Public Property idTipoDivida() As Integer Implements Interfaces.iCOBR.Primitivas.iDivida.idTipoDivida
                    Get
                        Return Parent.var("idTipoDivida").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idTipoDivida").Dirty = True
                        Parent.var("idTipoDivida").value = Value
                    End Set
                End Property

                Public Property NumDoc() As Integer Implements Interfaces.iCOBR.Primitivas.iDivida.NumDoc
                    Get
                        Return Parent.var("NumDoc").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("NumDoc").Dirty = True
                        Parent.var("NumDoc").value = Value
                    End Set
                End Property

                Public Property ValorNominal() As Double Implements Interfaces.iCOBR.Primitivas.iDivida.ValorNominal
                    Get
                        Dim tmp As String
                        tmp = Parent.var("ValorNominal").value
                        If mValorNominalParcial = 0 Then mValorNominalParcial = Convert.ToDouble(tmp.Replace(".", ","))
                        Return Convert.ToDouble(tmp.Replace(".", ","))
                    End Get
                    Set(ByVal Value As Double)
                        Parent.var("ValorNominal").Dirty = True
                        Parent.var("ValorNominal").value = Value
                        If mValorNominalParcial = 0 Then mValorNominalParcial = Value
                    End Set
                End Property

                Public Property ValorNominalParcial() As Double Implements Interfaces.iCOBR.Primitivas.iDivida.ValorNominalParcial
                    Get
                        Return Me.mValorNominalParcial
                    End Get
                    Set(ByVal value As Double)
                        Me.mValorNominalParcial = value
                    End Set
                End Property

                Public Property Contrato() As String Implements Interfaces.iCOBR.Primitivas.iDivida.Contrato
                    Get
                        Return Parent.var("Contrato").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("Contrato").Dirty = True
                        Parent.var("Contrato").value = Value
                    End Set
                End Property

                Public Property NumRecibo() As Integer Implements Interfaces.iCOBR.Primitivas.iDivida.NumRecibo
                    Get
                        Return Parent.var("NumRecibo").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("NumRecibo").Dirty = True
                        Parent.var("NumRecibo").value = Value
                    End Set
                End Property

                Public Property XmPathCliente() As String Implements Interfaces.iCOBR.Primitivas.iDivida.XmPathCliente
                    Get
                        Return Parent.var("XmPathCliente").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("XmPathCliente").Dirty = True
                        Parent.var("XmPathCliente").value = Value
                    End Set
                End Property

                Public Property idCobrador() As Integer Implements Interfaces.iCOBR.Primitivas.iDivida.idCobrador
                    Get
                        Return Parent.var("idCobrador").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idCobrador").Dirty = True
                        Parent.var("idCobrador").value = Value
                    End Set
                End Property

                Public Property idUsuarioBaixa() As Integer Implements Interfaces.iCOBR.Primitivas.iDivida.idUsuarioBaixa
                    Get
                        Return Parent.var("IdUsuarioBaixa").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("IdUsuarioBaixa").Dirty = True
                        Parent.var("IdUsuarioBaixa").value = Value
                    End Set
                End Property

                Public Property PerCobrador() As Double Implements Interfaces.iCOBR.Primitivas.iDivida.PerCobrador
                    Get
                        Dim tmp As String
                        tmp = Parent.var("PerCobrador").value
                        Return Convert.ToDouble(tmp.Replace(".", ","))
                    End Get
                    Set(ByVal Value As Double)
                        Parent.var("PerCobrador").Dirty = True
                        Parent.var("PerCobrador").value = Value
                    End Set
                End Property

                Public Property BaixaNoCliente() As Boolean Implements Interfaces.iCOBR.Primitivas.iDivida.BaixaNoCliente
                    Get
                        Return Parent.var("BaixaNoCliente").value
                    End Get
                    Set(ByVal Value As Boolean)
                        Parent.var("BaixaNoCliente").Dirty = True
                        Parent.var("BaixaNoCliente").value = Value
                    End Set
                End Property

                Public Property Baixada() As Boolean Implements Interfaces.iCOBR.Primitivas.iDivida.Baixada
                    Get
                        Return Parent.var("Baixada").value
                    End Get
                    Set(ByVal Value As Boolean)
                        Parent.var("Baixada").Dirty = True
                        Parent.var("Baixada").value = Value
                    End Set
                End Property

                Public Property BaixaParcial() As Boolean Implements Interfaces.iCOBR.Primitivas.iDivida.BaixaParcial
                    Get
                        Return Parent.var("BaixaParcial").value
                    End Get
                    Set(ByVal Value As Boolean)
                        Parent.var("BaixaParcial").Dirty = True
                        Parent.var("BaixaParcial").value = Value
                    End Set
                End Property

#End Region

            End Class
        End Class
#End Region

#Region "===[ Acionamentos ]==="
        Public Class Acionamentos
            Inherits allClass
            Private mCampos As AcionamentosCampos

            Public Sub New(ByRef pSelf As self)
                Me.New(pSelf, tipos.tiposConection.Default_, Nothing)
            End Sub
            Friend Sub New(ByRef pSelf As self, ByVal pFields As Fields)
                Me.New(pSelf, tipos.tiposConection.Default_, pFields, False)
            End Sub
            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                Me.New(pSelf, tipos.tiposConection.Default_, Nothing, pManterConexaoAberta)
            End Sub
            Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                MyBase.New(pSelf, "COBR_Acionamentos", TipoConexao, pFields, pManterConexaoAberta)
                mCampos = New AcionamentosCampos(Me)
            End Sub

            Public Property Campos() As Interfaces.iCOBR.Primitivas.iAcionamentos
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iCOBR.Primitivas.iAcionamentos)
                    mCampos = Value
                    If Not Value Is Nothing Then
                        MyBase.toObject = mCampos.Parent
                    End If
                End Set
            End Property

            Friend Class AcionamentosCampos
                Implements Interfaces.iCOBR.Primitivas.iAcionamentos

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
                    Dim filtro As String
                    filtro = " idUsuario = " & Me.idUsuario.ToString & " And DataAcionamento = CONVERT(DateTime,'" & Me.DataAcionamento.ToString("MM-dd-yy HH:mm:ss") & "',1)"
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

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("ID").value
                    End Get
                End Property

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.DataAcionamento.Ticks.ToString()
                    End Get
                End Property

                Public Property DataAcionamento() As DateTime Implements Interfaces.iCOBR.Primitivas.iAcionamentos.DataAcionamento
                    Get
                        Return Parent.var("DataAcionamento").value
                    End Get
                    Set(ByVal Value As DateTime)
                        Parent.var("DataAcionamento").Dirty = True
                        Parent.var("DataAcionamento").value = Value
                    End Set
                End Property

                Public Property DataPromessa() As Date Implements Interfaces.iCOBR.Primitivas.iAcionamentos.DataPromessa
                    Get
                        Return Parent.var("DataPromessa").value
                    End Get
                    Set(ByVal Value As Date)
                        Parent.var("DataPromessa").Dirty = True
                        Parent.var("DataPromessa").value = Value
                    End Set
                End Property

                Public Property idEntidade() As Integer Implements Interfaces.iCOBR.Primitivas.iAcionamentos.idEntidade
                    Get
                        Return Parent.var("idEntidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idEntidade").Dirty = True
                        Parent.var("idEntidade").value = Value
                    End Set
                End Property

                Public Property idTipoAcionamento() As Integer Implements Interfaces.iCOBR.Primitivas.iAcionamentos.idTipoAcionamento
                    Get
                        Return Parent.var("idTipoAcionamento").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idTipoAcionamento").Dirty = True
                        Parent.var("idTipoAcionamento").value = Value
                    End Set
                End Property

                Public Property idUsuario() As Integer Implements Interfaces.iCOBR.Primitivas.iAcionamentos.idUsuario
                    Get
                        Return Parent.var("idUsuario").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idUsuario").Dirty = True
                        Parent.var("idUsuario").value = Value
                    End Set
                End Property

                Public Property TextoRespeito() As String Implements Interfaces.iCOBR.Primitivas.iAcionamentos.TextoRespeito
                    Get
                        Return Parent.var("TextoRespeito").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("TextoRespeito").Dirty = True
                        Parent.var("TextoRespeito").value = Value
                    End Set
                End Property

#End Region

            End Class
        End Class
#End Region

#Region "===[ Tarifas ]==="
        Public Class Tarifas
            Inherits allClass
            Private mCampos As TarifasCampos

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "COBR_Tarifas")
                mCampos = New TarifasCampos(Me)
            End Sub
            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                MyBase.New(pSelf, "COBR_Tarifas", TipoConexao, Nothing, pManterConexaoAberta)
                mCampos = New TarifasCampos(Me)
            End Sub
            Public Property Campos() As Interfaces.iCOBR.Primitivas.iTarifas
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iCOBR.Primitivas.iTarifas)
                    mCampos = Value
                    If Not Value Is Nothing Then
                        MyBase.toObject = mCampos.Parent
                    End If
                End Set
            End Property

            Friend Class TarifasCampos
                Implements Interfaces.iCOBR.Primitivas.iTarifas


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
                    Dim filtro As String
                    filtro = " idEntidade = " & Me.idEntidade
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

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("ID").value
                    End Get
                End Property

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.ID.ToString
                    End Get
                End Property

                Public Property idEntidade() As Integer Implements Interfaces.iCOBR.Primitivas.iTarifas.idEntidade
                    Get
                        Return Parent.var("idEntidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idEntidade").Dirty = True
                        Parent.var("idEntidade").value = Value
                    End Set
                End Property

                Public Property Juros() As Double Implements Interfaces.iCOBR.Primitivas.iTarifas.Juros
                    Get
                        Return Parent.var("Juros").value
                    End Get
                    Set(ByVal Value As Double)
                        Parent.var("Juros").Dirty = True
                        Parent.var("Juros").value = Value
                    End Set
                End Property

                Public Property Multa() As Double Implements Interfaces.iCOBR.Primitivas.iTarifas.Multa
                    Get
                        Return Parent.var("Multa").value
                    End Get
                    Set(ByVal Value As Double)
                        Parent.var("Multa").Dirty = True
                        Parent.var("Multa").value = Value
                    End Set
                End Property
#End Region
            End Class
        End Class
#End Region

#Region "===[ Baixas ]==="
        Public Class Baixas
            Inherits allClass
            Private mCampos As BaixasCampos

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "COBR_Baixas")
                mCampos = New BaixasCampos(Me)
            End Sub
            Friend Sub New(ByRef pSelf As self, ByVal pFields As Fields)
                MyBase.New(pSelf, "COBR_Baixas", pFields)
                mCampos = New BaixasCampos(Me)
            End Sub
            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pManterConexaoAberta As Boolean)
                Me.New(pSelf, TipoConexao, Nothing, pManterConexaoAberta)
                mCampos = New BaixasCampos(Me)
            End Sub
            Friend Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection, ByVal pFields As Fields, ByVal pManterConexaoAberta As Boolean)
                MyBase.New(pSelf, "COBR_Baixas", TipoConexao, pFields, pManterConexaoAberta)
                mCampos = New BaixasCampos(Me)
            End Sub
            Public Property Campos() As Interfaces.iCOBR.Primitivas.iBaixas
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iCOBR.Primitivas.iBaixas)
                    mCampos = Value
                    If Not Value Is Nothing Then
                        MyBase.toObject = mCampos.Parent
                    End If
                End Set
            End Property

            Friend Class BaixasCampos
                Implements Interfaces.iCOBR.Primitivas.iBaixas

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
                    Dim filtro As String
                    filtro = "idDivida = " & Me.idDivida & " And DataBaixa = '" & Me.DataBaixa.ToString(Parent.Self.Settings.sintaxeData) & "'" & _
                    " And NumBordero = " & Me.NumBordero & " and ValorBaixa = " & Me.ValorBaixa.ToString().Replace(",", ".")
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

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("Id").value
                    End Get
                End Property

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.idEntidade & Me.DataBaixa.ToShortDateString
                    End Get
                End Property

                Public Property idEntidade() As Integer Implements Interfaces.iCOBR.Primitivas.iBaixas.idEntidade
                    Get
                        Return Parent.var("idEntidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idEntidade").Dirty = True
                        Parent.var("idEntidade").value = Value
                    End Set
                End Property

                Public Property idDivida() As Integer Implements Interfaces.iCOBR.Primitivas.iBaixas.idDivida
                    Get
                        Return Parent.var("idDivida").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idDivida").Dirty = True
                        Parent.var("idDivida").value = Value
                    End Set
                End Property
                Public Property BaixadoCliente() As Boolean Implements Interfaces.iCOBR.Primitivas.iBaixas.BaixadoCliente
                    Get
                        Return Parent.var("BaixadoCliente").value
                    End Get
                    Set(ByVal Value As Boolean)
                        Parent.var("BaixadoCliente").Dirty = True
                        Parent.var("BaixadoCliente").value = Value
                    End Set
                End Property

                Public Property NumBordero() As Integer Implements Interfaces.iCOBR.Primitivas.iBaixas.NumBordero
                    Get
                        Return Parent.var("NumBordero").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("NumBordero").Dirty = True
                        Parent.var("NumBordero").value = Value
                    End Set
                End Property
                Public Property ValorNominal() As Double Implements Interfaces.iCOBR.Primitivas.iBaixas.ValorNominal
                    Get
                        Dim tmp As String
                        tmp = Parent.var("ValorNominal").value
                        Return Convert.ToDouble(tmp.Replace(".", ","))
                    End Get
                    Set(ByVal Value As Double)
                        Parent.var("ValorNominal").Dirty = True
                        Parent.var("ValorNominal").value = Value
                    End Set
                End Property

                Public Property ValorBaixa() As Double Implements Interfaces.iCOBR.Primitivas.iBaixas.ValorBaixa
                    Get
                        Dim tmp As String
                        tmp = Parent.var("ValorBaixa").value
                        Return Convert.ToDouble(tmp.Replace(".", ","))
                    End Get
                    Set(ByVal Value As Double)
                        Parent.var("ValorBaixa").Dirty = True
                        Parent.var("ValorBaixa").value = Value
                    End Set
                End Property

                Public Property ValorRecebido() As Double Implements Interfaces.iCOBR.Primitivas.iBaixas.ValorRecebido
                    Get
                        Dim tmp As String
                        tmp = Parent.var("ValorRecebido").value
                        Return Convert.ToDouble(tmp.Replace(".", ","))
                    End Get
                    Set(ByVal Value As Double)
                        Parent.var("ValorRecebido").Dirty = True
                        Parent.var("ValorRecebido").value = Value
                    End Set
                End Property

                Public Property DataBaixa() As Date Implements Interfaces.iCOBR.Primitivas.iBaixas.DataBaixa
                    Get
                        Return Parent.var("DataBaixa").value
                    End Get
                    Set(ByVal Value As Date)
                        Parent.var("DataBaixa").Dirty = True
                        Parent.var("DataBaixa").value = Value
                    End Set
                End Property
#End Region

            End Class
        End Class
#End Region

    End Namespace
End Namespace

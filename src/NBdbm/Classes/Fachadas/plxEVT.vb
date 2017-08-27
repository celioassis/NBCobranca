Option Explicit On

Namespace Fachadas.plxEVT

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

#Region "===[ Cadastro de Itens ]==="
    '===[ cadastro de Estoque Patrimonial ]===============================================================================
    Public Class CadastroItens
        Implements Interfaces.iEVT.iCadastroItens

        Private Const mSource As String = "**** Cadastro de Itens ****"
        Private mItem As Fachadas.plxEVT.primitivas.Item
        Private mLinkNo As Fachadas.plxEVT.primitivas.LinkItemNo
        Private mStatusReal As Fachadas.plxEVT.primitivas.StatusReal
        Private mXmPathItemNo As String
        Private mQuantAtualItem As Integer
        Private mTipoConexao As tipos.tiposConection
        Private aSelf As self

        Public Sub New()
            mItem = New Fachadas.plxEVT.primitivas.Item(aSelf)
            mLinkNo = New Fachadas.plxEVT.primitivas.LinkItemNo(aSelf)
            mStatusReal = New Fachadas.plxEVT.primitivas.StatusReal(aSelf)
            aSelf = mItem.Self
        End Sub
        Public Sub New(ByVal TipoConexao As tipos.tiposConection)
            mTipoConexao = TipoConexao
            mItem = New Fachadas.plxEVT.primitivas.Item(aSelf, TipoConexao)
            mLinkNo = New Fachadas.plxEVT.primitivas.LinkItemNo(aSelf, TipoConexao)
            mStatusReal = New Fachadas.plxEVT.primitivas.StatusReal(aSelf, TipoConexao)
            aSelf = mItem.Self
        End Sub
        Public Sub Excluir(ByVal NoCommit As Boolean) Implements Interfaces.iEVT.iCadastroItens.Excluir
            Try
                'Excluindo o Kit
                mItem.excluir(NoCommit)
            Catch ex As NBexception
                ex.Source = mSource
                Throw ex
            End Try
        End Sub
        Public Sub Excluir(ByVal idItem As Integer)
            mItem.Campos.Clear_filters()
            mItem.filterWhere = "idItem=" + idItem.ToString()
            Me.Excluir(False)
        End Sub
        Public Sub Excluir() Implements Interfaces.iEVT.iCadastroItens.Excluir
            Me.Excluir(False)
        End Sub
        Public Sub Salvar() Implements Interfaces.iEVT.iCadastroItens.Salvar
            Salvar(False)
        End Sub
        Public Sub Salvar(ByVal NoCommit As Boolean) Implements Interfaces.iEVT.iCadastroItens.Salvar
            'implementar: Salvar primeiro o Item do Estoque
            Dim idItem As Integer
            'Dim strParser As String
            Dim idNo As Long
            Dim ob As Fachadas.CTR.primitivas.No

            Try
                'Verifica se foi usado outro tipo de conexao que não a padrao
                If Not IsNothing(mTipoConexao) Then
                    ob = New Fachadas.CTR.primitivas.No(aSelf, mTipoConexao, True)
                Else
                    ob = New Fachadas.CTR.primitivas.No(aSelf)
                End If

                'Localizando o Nó pelo xmPath
                ob.filterWhere = "xmPath like '*" & Me.mXmPathItemNo & "'"
                ob.getFields(False)
                idNo = ob.Campos.idNo_key

                'Salvando o Item.
                Me.Item.salvar(True)

                'Criando StatusReal
                Me.AdicionarItensQuantitativos()

                'Salvando o link da Classe do EstoquePatrimonial
                idItem = Me.Item.ID
                Me.mLinkNo.Clear_filters()
                Me.mLinkNo.Campos.idItem = idItem
                Me.mLinkNo.Campos.idNo = idNo
                Me.mLinkNo.Campos.salvar(True)

                'Finaliza a Transação
                aSelf.AdmDB.FinalizaTransaction(NoCommit)

            Catch ex As Exception
                If (aSelf.AdmDB.connection.State = ConnectionState.Open) Then
                    aSelf.AdmDB.connection.Close()
                End If
                ex.Source = CadastroItens.mSource
                Throw ex
            End Try

        End Sub
        Public Function DataSource(ByVal Filtro As String) As System.Data.DataView
            If Filtro <> "" Then
                Me.mItem.Clear_filters()
                Me.mItem.filterWhere = Filtro
            End If
            Return Me.mItem.DataSource()
        End Function
        Public Property Item() As Interfaces.iEVT.Primitivas.iItem Implements Interfaces.iEVT.iCadastroItens.Item
            Get
                Return Me.mItem.Campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iItem)
                Me.mItem.Campos = Value
            End Set
        End Property
        Public Property xmPath_LinkItemNo() As String Implements Interfaces.iEVT.iCadastroItens.xmPath_LinkItemNo
            Get
                Return Me.mXmPathItemNo
            End Get
            Set(ByVal Value As String)
                Me.mXmPathItemNo = Value
            End Set
        End Property
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Me.mXmPathItemNo = Nothing
            Me.mLinkNo.Dispose()
            Me.mLinkNo = Nothing
            Me.mItem.Dispose()
            Me.mItem = Nothing
            Me.mStatusReal.Dispose()
            Me.mStatusReal = Nothing
        End Sub
        Public Sub getFieldsFromItem(ByVal iditem As String)
            Me.mItem.Clear_filters()
            Me.mItem.filterWhere = "idItem = " + iditem
            Me.mItem.getFields(True)
            Me.mQuantAtualItem = Me.mItem.Campos.Quantidade

            'Localizando o idNó pelo IdEstoque
            Me.mLinkNo.filterWhere = "idItem=" & iditem
            Me.mLinkNo.getFields(True)

            'Localizando o Nó pelo xmPath
            Dim ob As Fachadas.CTR.primitivas.No
            'Verifica se foi usado outro tipo de conexao que não a padrao
            If Not IsNothing(mTipoConexao) Then
                ob = New Fachadas.CTR.primitivas.No(aSelf, mTipoConexao, True)
            Else
                ob = New Fachadas.CTR.primitivas.No(aSelf)
            End If

            ob.filterWhere = "idNo=" & mLinkNo.Campos.idNo.ToString()
            ob.getFields(False)
            Me.mXmPathItemNo = ob.Campos.xmPath_key


        End Sub
        Public Sub AlterarKey()
            Try
                Me.mItem.filterWhere = "idItem=" + Me.Item.ID.ToString()
                Me.mItem.editar(False)
            Catch ex As NBexception
                ex.Source = CadastroItens.mSource
                Throw ex
            End Try
        End Sub
        Public Sub AdicionarItensQuantitativos()
            Dim mInicio As Integer

            If Me.mItem.Inclusao = True Then
                mInicio = 1
            Else
                mInicio = Me.mQuantAtualItem + 1
            End If
            For cont As Integer = mInicio To Me.Item.Quantidade
                mStatusReal.Campos.IdItem = Me.Item.ID
                mStatusReal.Campos.IdObj = "I" + Me.Item.ID.ToString
                mStatusReal.Campos.CodBarras = "I" + Format(Me.Item.ID, "0000000") & Format(cont, "0000000")
                mStatusReal.Campos.Status = 3
                mStatusReal.Campos.salvar(True)
            Next
        End Sub
    End Class
#End Region

#Region "===[ Cadastro de Localidades ]==="
    '===[ cadastro de Localidades ]=======================================================================================
    Public Class CadastroLocalidades
        Inherits Fachadas.CTR.CadastroEntidade
        Implements Interfaces.iEVT.iCadastroLocalidades

        Private Shadows Const mSource As String = "**** Cadastro de Localidades ****"
        Protected mLocalidade As Fachadas.plxEVT.primitivas.Localidades

        Public Sub New()
            MyBase.New()
            mLocalidade = New Fachadas.plxEVT.primitivas.Localidades(aSelf)

        End Sub
        Public Sub New(ByVal TipoConexao As tipos.tiposConection)
            mLocalidade = New Fachadas.plxEVT.primitivas.Localidades(aSelf, TipoConexao)
        End Sub

        Public Overrides Sub getFromEntidade(ByVal idEntidade As Double, ByVal pManterConexaoAberta As Boolean)

            MyBase.getFromEntidade(idEntidade, pManterConexaoAberta)

            Me.mLocalidade.filterWhere = "idEntidade = " & idEntidade
            Me.mLocalidade.getFields(False)

        End Sub

        Public Sub getFieldsFromLocalidade(ByVal idLocalidade As Double)

            Me.mLocalidade.filterWhere = "idLocalidade = " & idLocalidade
            Me.mLocalidade.getFields(True)

            MyBase.getFromEntidade(Me.Localidade.idEntidade, True)

        End Sub

        Public Overloads Sub Salvar(ByVal NoCommit As Boolean) Implements Interfaces.iEVT.iCadastroLocalidades.Salvar
            Dim idE As Double
            Try

                'Salva a Entidade
                MyBase.Salvar(True)
                idE = Me.Entidade.ID

                'Salva a Localidade
                Me.Localidade.idEntidade = idE
                Me.Localidade.salvar(True)

                'Finaliza a Transação
                aSelf.AdmDB.FinalizaTransaction(NoCommit)

            Catch ex As Exception
                ex.Source = CadastroLocalidades.mSource
                Throw ex
            End Try
        End Sub

        Public Overloads Sub Salvar() Implements Interfaces.iEVT.iCadastroLocalidades.Salvar
            Me.Salvar(False)
        End Sub

        Public Overloads Sub Excluir(ByVal NoCommit As Boolean) Implements Interfaces.iEVT.iCadastroLocalidades.Excluir
            Try
                'Excluindo a Entidade
                MyBase.Excluir(True)
                'Excluindo a Localidade
                Me.mLocalidade.excluir(NoCommit)
            Catch ex As NBexception
                ex.Source = CadastroLocalidades.mSource
                Throw ex
            End Try
        End Sub

        Public Overloads Sub Excluir() Implements Interfaces.iEVT.iCadastroLocalidades.Excluir
            Me.Excluir(False)
        End Sub

        Public Property Localidade() As Interfaces.iEVT.Primitivas.iLocalidades Implements Interfaces.iEVT.iCadastroLocalidades.Localidade
            Get
                Return mLocalidade.Campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iLocalidades)
                mLocalidade.Campos = Value
            End Set
        End Property
        Public Overrides Sub Dispose()
            mLocalidade.Dispose()
            mLocalidade = Nothing
            MyBase.Dispose()
        End Sub

    End Class
#End Region

#Region "===[ Cadastro de Kits ]==="
    '===[ cadastro de Kits ]=======================================================================================
    Public Class CadastroKit
        'Inherits Fachadas.allClass
        Implements Interfaces.iEVT.iCadatroKit

        Private Const mSource As String = "**** Cadastro de Kits ****"
        Protected mColKitItem As New Fachadas.NbCollection
        Protected mKit As Fachadas.plxEVT.primitivas.kit
        Protected mKitItem As Fachadas.plxEVT.primitivas.KitItem
        Protected mTipoConexao As tipos.tiposConection
        Private mStatusReal As Fachadas.plxEVT.primitivas.StatusReal
        Private aSelf As self

        Public Sub New()
            mKit = New Fachadas.plxEVT.primitivas.kit(aSelf)
            mKitItem = New Fachadas.plxEVT.primitivas.KitItem(aSelf)
            mStatusReal = New Fachadas.plxEVT.primitivas.StatusReal(aSelf)
            aSelf = mKit.Self
        End Sub
        Public Sub New(ByVal TipoConexao As tipos.tiposConection)
            mTipoConexao = TipoConexao
            mKit = New Fachadas.plxEVT.primitivas.kit(aSelf, TipoConexao)
            mKitItem = New Fachadas.plxEVT.primitivas.KitItem(aSelf, TipoConexao)
            mStatusReal = New Fachadas.plxEVT.primitivas.StatusReal(aSelf, TipoConexao)
            aSelf = mKit.Self
        End Sub

        Public Sub Dispose() Implements System.IDisposable.Dispose
            mColKitItem.Dispose()
            mColKitItem = Nothing
            mKit.Dispose()
            mKit = Nothing
            mKitItem.Dispose()
            mKitItem = Nothing
            mTipoConexao = Nothing
        End Sub

        Public Sub Excluir(ByVal NoCommit As Boolean) Implements Interfaces.iEVT.iCadatroKit.Excluir
            Try
                'Excluindo o Kit
                Me.mKit.excluir(NoCommit)
            Catch ex As NBexception
                ex.Source = mSource
                Throw ex
            End Try
        End Sub

        Public Sub Excluir() Implements Interfaces.iEVT.iCadatroKit.Excluir
            Me.Excluir(False)
        End Sub

        Public Sub salvar(ByVal NoCommit As Boolean) Implements Interfaces.iEVT.iCadatroKit.Salvar
            'implementar: Salvar primeiro o Kit, depois os Itens do Kit
            Dim idK As Double
            Try
                'Salvando o Kit
                Me.Kit.salvar(True)

                'Criando StatusReal
                If Me.mKit.Inclusao = True Then
                    Dim cont As Integer
                    For cont = 1 To Me.Kit.Quantidade
                        mStatusReal.Campos.IdKit = Me.Kit.ID
                        mStatusReal.Campos.IdObj = "K" + Me.Kit.ID.ToString
                        mStatusReal.Campos.CodBarras = "K" + Format(Me.Kit.ID, "0000000") & Format(cont, "0000000")
                        mStatusReal.Campos.Status = 3
                        mStatusReal.Campos.salvar(True)
                    Next
                End If

                'Salvando os Itens do Kit
                idK = Me.Kit.ID
                For Each kItem As Interfaces.iEVT.Primitivas.iKitItem In mColKitItem.Values
                    kItem.idKit_key = idK
                    kItem.salvar(True)
                Next


                'Finaliza a Transação
                aself.AdmDB.FinalizaTransaction(NoCommit)

            Catch ex As Exception
                Dim mNBEx As New NBexception("Não foi possível salvar o Kit - " & Me.Kit.Nome_key, ex)
                mNBEx.Source = "NBdbm.Fachadas.plxEVT.CadastroKit.Salvar"
                Throw mNBEx
            End Try
        End Sub

        Public Sub Salvar() Implements Interfaces.iEVT.iCadatroKit.Salvar
            Me.Salvar(False)
        End Sub

        Public ReadOnly Property colecaoKitItens() As NbCollection
            Get
                Return mColKitItem
            End Get
        End Property

        Public Overridable Sub getFieldsFromKit(ByVal idKit As Double)
            Dim DR As Data.DataRow
            Me.mColKitItem.Clear()
            Me.mKit.filterWhere = " IdKit = " & idKit
            Me.mKit.getFields(True)
            Me.mKitItem.filterWhere = " idKit = " & idKit
            If Me.mKitItem.DataSource.Count > 0 Then
                For Each DR In Me.mKitItem.DataSource.Table.Rows
                    Dim KItem As NBdbm.Fachadas.plxEVT.primitivas.KitItem
                    Dim Item As Fachadas.plxEVT.primitivas.Item
                    'Verifica se foi usado outro tipo de conexao que não a padrao
                    If Not IsNothing(mTipoConexao) Then
                        KItem = New NBdbm.Fachadas.plxEVT.primitivas.KitItem(aSelf, mTipoConexao)
                        Item = New Fachadas.plxEVT.primitivas.Item(aSelf, mTipoConexao)
                    Else
                        KItem = New NBdbm.Fachadas.plxEVT.primitivas.KitItem(aSelf)
                        Item = New Fachadas.plxEVT.primitivas.Item(aSelf)
                    End If
                    KItem.filterWhere = " idKit = " & idKit & " and IdKitItem = " & DR.Item("IdKitItem").ToString
                    KItem.getFields(True)
                    Item.filterWhere = " idItem = " & Integer.Parse(KItem.Campos.CodBarrasItem_key.Substring(1, 7)).ToString()
                    Item.getFields(True)
                    KItem.Campos.Descricao = Item.Campos.Descricao_key
                    KItem.Campos.ValorLocacao = Item.Campos.ValorLocacao
                    Me.KitItem = KItem.Campos 'preenchendo a property com o último.
                    Me.colecaoKitItens.Add(KItem.Campos.Key, CType(KItem.Campos, Object))
                Next
            Else
                mKitItem.Clear_vars()
            End If
        End Sub

        Public Property Kit() As Interfaces.iEVT.Primitivas.iKit Implements Interfaces.iEVT.iCadatroKit.Kit
            Get
                Return mKit.campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iKit)
                mKit.campos = Value
            End Set
        End Property

        Public Property KitItem() As Interfaces.iEVT.Primitivas.iKitItem Implements Interfaces.iEVT.iCadatroKit.KitItem
            Get
                If mKitItem Is Nothing Then
                    mKitItem = New Fachadas.plxEVT.primitivas.KitItem(aSelf)
                End If
                If mKitItem.Campos Is Nothing Then
                    mKitItem = New Fachadas.plxEVT.primitivas.KitItem(aSelf)
                End If
                Return mKitItem.Campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iKitItem)
                mKitItem.Campos = Value
            End Set
        End Property
    End Class
#End Region

#Region "===[ Cadastro de Orçamento ]==="
    '===[ cadastro de Ordem de Serviços ]====================================================================================
    Public Class CadastroOrcamento
        'Inherits Fachadas.allClass
        Implements Interfaces.iEVT.iCadatroOrcamento

        Private Const mSource As String = "**** Cadastro de Orçamento ****"
        Protected mColOrcItem As New Fachadas.NbCollection
        Protected mColOrcKit As New Fachadas.NbCollection
        Protected mColOrcDespAdic As New Fachadas.NbCollection
        Protected mColOrcParcelas As New Fachadas.NbCollection
        Protected mOrc As Fachadas.plxEVT.primitivas.Orcamento
        Protected mOrcItem As Fachadas.plxEVT.primitivas.OrcamentoObjs
        Protected mOrcKit As Fachadas.plxEVT.primitivas.OrcamentoObjs
        Protected mOrcDespAdic As Fachadas.plxEVT.primitivas.OrcamentoDespAdicional
        Protected mOrcParcela As Fachadas.plxEVT.primitivas.OrcamentoParcelas
        Protected mTipoConexao As tipos.tiposConection
        Protected aSelf As self

        Public Sub New()
            Me.mOrc = New Fachadas.plxEVT.primitivas.Orcamento(aSelf)
            Me.mOrcItem = New Fachadas.plxEVT.primitivas.OrcamentoObjs(aSelf)
            Me.mOrcKit = New Fachadas.plxEVT.primitivas.OrcamentoObjs(aSelf)
            Me.mOrcDespAdic = New Fachadas.plxEVT.primitivas.OrcamentoDespAdicional(aSelf)
            Me.mOrcParcela = New Fachadas.plxEVT.primitivas.OrcamentoParcelas(aSelf)
            Me.aSelf = mOrc.Self
        End Sub
        Public Sub New(ByVal TipoConexao As tipos.tiposConection)
            Me.mTipoConexao = TipoConexao
            Me.mOrc = New Fachadas.plxEVT.primitivas.Orcamento(aSelf, TipoConexao)
            Me.mOrcItem = New Fachadas.plxEVT.primitivas.OrcamentoObjs(aSelf, TipoConexao)
            Me.mOrcKit = New Fachadas.plxEVT.primitivas.OrcamentoObjs(aSelf, TipoConexao)
            Me.mOrcDespAdic = New Fachadas.plxEVT.primitivas.OrcamentoDespAdicional(aSelf, TipoConexao)
            Me.mOrcParcela = New Fachadas.plxEVT.primitivas.OrcamentoParcelas(aSelf, TipoConexao)
            Me.aSelf = mOrc.Self
        End Sub
        Public Sub Dispose() Implements System.IDisposable.Dispose
            If Not IsNothing(Me.mColOrcItem) Then Me.mColOrcItem.Dispose()
            Me.mColOrcItem = Nothing
            If Not IsNothing(Me.mColOrcKit) Then Me.mColOrcKit.Dispose()
            Me.mColOrcKit = Nothing
            If Not IsNothing(Me.mColOrcDespAdic) Then Me.mColOrcDespAdic.Dispose()
            Me.mColOrcDespAdic = Nothing
            If Not IsNothing(Me.mOrc) Then Me.mOrc.Dispose()
            Me.mOrc = Nothing
            If Not IsNothing(Me.mOrcItem) Then Me.mOrcItem.Dispose()
            Me.mOrcItem = Nothing
            If Not IsNothing(Me.mOrcKit) Then Me.mOrcKit.Dispose()
            Me.mOrcKit = Nothing
            If Not IsNothing(Me.mOrcDespAdic) Then Me.mOrcDespAdic.Dispose()
            Me.mOrcDespAdic = Nothing
            If Not IsNothing(Me.mOrcParcela) Then Me.mOrcParcela.Dispose()
            Me.mOrcParcela = Nothing
            Me.aSelf = Nothing
        End Sub
        Public Sub Excluir(ByVal NoCommit As Boolean) Implements Interfaces.iEVT.iCadatroOrcamento.Excluir
            Try
                mOrc.excluir(NoCommit)
            Catch ex As NBexception
                ex.Source = mSource
                Throw ex
            End Try
        End Sub
        Public Sub Excluir() Implements Interfaces.iEVT.iCadatroOrcamento.Excluir
            Me.Excluir(False)
        End Sub
        Public Sub Salvar(ByVal NoCommit As Boolean) Implements Interfaces.iEVT.iCadatroOrcamento.Salvar
            'implementar: Salvar primeiro o Orçamento, depois os Itens, Kits, Despesas Adicionais e Parcelas.
            Dim idOrc As Double
            Try

                'Salvando o Orçamento
                Me.Orc.salvar(True)
                idOrc = Me.Orc.ID

                'Salvando os Itens do Orçamento
                For Each OrcItem As Interfaces.iEVT.Primitivas.iOrcamentoObjs In mColOrcItem.Values
                    OrcItem.IdOrcamento_key = idOrc
                    OrcItem.salvar(True)
                    If Me.Orc.IsModelo = False Then
                        OrcItem.CtrObj.SalvarPresumido(True, idOrc, OrcItem.ID)
                    End If
                Next

                'Salvando os Kits do Orçamento
                For Each OrcKit As Interfaces.iEVT.Primitivas.iOrcamentoObjs In mColOrcKit.Values
                    OrcKit.IdOrcamento_key = idOrc
                    OrcKit.salvar(True)
                    If Me.Orc.IsModelo = False Then
                        OrcKit.CtrObj.SalvarPresumido(True, idOrc, OrcKit.ID)
                    End If
                Next

                'Salvando as Despesas Adicionais do Orçamento
                For Each OrcDespAdic As Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional In mColOrcDespAdic.Values
                    OrcDespAdic.IdOrcamento_Key = idOrc
                    OrcDespAdic.salvar(True)
                Next

                'Salvando o Parcelamento do Orçamento.
                For Each OrcParcela As Interfaces.iEVT.Primitivas.iOrcamentoParcelas In Me.mColOrcParcelas.Values
                    OrcParcela.IdOrcamento_key = idOrc
                    OrcParcela.salvar(True)
                Next

                'Finaliza a Transação
                aself.AdmDB.FinalizaTransaction(NoCommit)

            Catch ex As NBexception
                ex.Source = mSource
                Throw ex
            End Try

        End Sub
        Public Sub Salvar() Implements Interfaces.iEVT.iCadatroOrcamento.Salvar
            Me.Salvar(False)
        End Sub
        Public ReadOnly Property colecaoOrcItens() As NbCollection
            Get
                Return mColOrcItem
            End Get
        End Property
        Public ReadOnly Property colecaoOrcKits() As NbCollection
            Get
                Return mColOrcKit
            End Get
        End Property
        Public ReadOnly Property colecaoOrcDespAdic() As NbCollection
            Get
                Return mColOrcDespAdic
            End Get
        End Property
        Public ReadOnly Property colecaoOrcParcelas() As NbCollection
            Get
                Return Me.mColOrcParcelas
            End Get
        End Property

        Public Overridable Sub getFieldsFromOrc(ByVal idOrc As Double)
            Dim DR As Data.DataRow
            Me.mOrc.filterWhere = " idOrcamento = " & idOrc
            Me.mOrc.getFields(True)

            '=== Carrega os Dados dos Itens ===
            Me.mOrcItem.filterWhere = " idOrcamento = " & idOrc & " AND idObj LIKE 'I%'"
            Me.mOrcItem.getFields(True)
            If Me.mOrcItem.DataSource.Count > 0 Then
                For Each DR In Me.mOrcItem.DataSource.Table.Rows
                    Dim OrcItem As NBdbm.Fachadas.plxEVT.primitivas.OrcamentoObjs
                    'Verifica se esta sendo usado outro Tipo de Conexão que não seja a Padrão.
                    If Not IsNothing(mTipoConexao) Then
                        OrcItem = New NBdbm.Fachadas.plxEVT.primitivas.OrcamentoObjs(aSelf, mTipoConexao)
                    Else
                        OrcItem = New NBdbm.Fachadas.plxEVT.primitivas.OrcamentoObjs(aSelf)
                    End If
                    OrcItem.filterWhere = " idorcamento = " & idOrc & " and idObj = '" & DR.Item("idObj").ToString & "'"
                    OrcItem.getFields(True)
                    Me.mOrcItem.Campos = OrcItem.Campos 'preenchendo a property com o último.
                    Me.colecaoOrcItens.Add(OrcItem.Campos.Key, CType(OrcItem.Campos, Object))
                Next
            Else
                mOrcItem.Clear_vars()
            End If

            '=== Carrega os Dados dos Kits ===
            Me.mOrcKit.filterWhere = " idOrcamento = " & idOrc & " AND idObj LIKE 'K%'"
            Me.mOrcKit.getFields(True)
            If Me.mOrcKit.DataSource.Count > 0 Then
                For Each DR In Me.mOrcKit.DataSource.Table.Rows
                    Dim OrcKit As NBdbm.Fachadas.plxEVT.primitivas.OrcamentoObjs
                    'Verifica se esta sendo usado outro Tipo de Conexão que não seja a Padrão.
                    If Not IsNothing(mTipoConexao) Then
                        OrcKit = New NBdbm.Fachadas.plxEVT.primitivas.OrcamentoObjs(aSelf, mTipoConexao)
                    Else
                        OrcKit = New NBdbm.Fachadas.plxEVT.primitivas.OrcamentoObjs(aSelf)
                    End If
                    OrcKit.filterWhere = " idorcamento = " & idOrc & " and idObj = '" & DR.Item("idObj").ToString & "'"
                    OrcKit.getFields(True)
                    Me.mOrcItem.Campos = OrcKit.Campos 'preenchendo a property com o último.
                    Me.colecaoOrcKits.Add(OrcKit.Campos.Key, CType(OrcKit.Campos, Object))
                Next
            Else
                mOrcItem.Clear_vars()
            End If

            '=== Carrega os Dados das Despesas Adicionais ===
            Me.mOrcDespAdic.filterWhere = " idOrcamento = " & idOrc
            Me.mOrcDespAdic.getFields(True)
            If Me.mOrcDespAdic.DataSource.Count > 0 Then
                For Each DR In Me.mOrcDespAdic.DataSource.Table.Rows
                    Dim OrcDespAdic As NBdbm.Fachadas.plxEVT.primitivas.OrcamentoDespAdicional
                    'Verifica se esta sendo usado outro tipo de conexão que não seja a padrão.
                    If Not IsNothing(mTipoConexao) Then
                        OrcDespAdic = New NBdbm.Fachadas.plxEVT.primitivas.OrcamentoDespAdicional(aSelf, mTipoConexao)
                    Else
                        OrcDespAdic = New NBdbm.Fachadas.plxEVT.primitivas.OrcamentoDespAdicional(aSelf)
                    End If
                    OrcDespAdic.filterWhere = " idorcamento = " & idOrc & " and idDespAdicional = " & DR.Item("idDespAdicional").ToString
                    OrcDespAdic.getFields(True)
                    Me.mOrcDespAdic.Campos = OrcDespAdic.Campos 'preenchendo a property com o último.
                    Me.colecaoOrcDespAdic.Add(OrcDespAdic.Campos.Key, CType(OrcDespAdic.Campos, Object))
                Next
            Else
                mOrcDespAdic.Clear_vars()
            End If

            '" === Carrega os Dados de Parcelamentos ==="
            Me.mOrcParcela.filterWhere = " idOrcamento = " & idOrc
            Me.mOrcParcela.getFields(True)
            If Me.mOrcParcela.DataSource.Count > 0 Then
                For Each DR In Me.mOrcParcela.DataSource.Table.Rows
                    Dim OrcParcela As NBdbm.Fachadas.plxEVT.primitivas.OrcamentoParcelas
                    'Verifica se esta sendo usado outro tipo de conexão que não seja a padrão.
                    If Not IsNothing(mTipoConexao) Then
                        OrcParcela = New NBdbm.Fachadas.plxEVT.primitivas.OrcamentoParcelas(aSelf, mTipoConexao)
                    Else
                        OrcParcela = New NBdbm.Fachadas.plxEVT.primitivas.OrcamentoParcelas(aSelf)
                    End If
                    OrcParcela.filterWhere = " idorcamento = " & idOrc & " and idparcela = " & DR.Item("idparcela").ToString
                    OrcParcela.getFields(True)
                    Me.mOrcParcela.Campos = OrcParcela.Campos 'preenchendo a property com o último.
                    Me.colecaoOrcParcelas.Add(OrcParcela.Campos.Key, CType(OrcParcela.Campos, Object))
                Next
            Else
                mOrcParcela.Clear_vars()
            End If

            If aSelf.AdmDB.connection.State = ConnectionState.Open Then
                aSelf.AdmDB.connection.Close()
            End If
        End Sub
        Public Sub LimpaColecoes()
            Me.mColOrcItem.Clear()
            Me.mColOrcKit.Clear()
            Me.mColOrcDespAdic.Clear()
            Me.mColOrcParcelas.Clear()
        End Sub
        Public Sub LimpaVars()
            Me.mOrc.Clear_vars()
            Me.mOrcDespAdic.Clear_vars()
            Me.mOrcItem.Clear_vars()
            Me.mOrcKit.Clear_vars()
        End Sub
        Public Property Orc() As Interfaces.iEVT.Primitivas.iOrcamento Implements Interfaces.iEVT.iCadatroOrcamento.Orc
            Get
                Return mOrc.campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iOrcamento)
                mOrc.campos = Value
            End Set
        End Property
        Public Property OrcItem() As Interfaces.iEVT.Primitivas.iOrcamentoObjs Implements Interfaces.iEVT.iCadatroOrcamento.OrcItem
            Get
                Return mOrcItem.Campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iOrcamentoObjs)
                mOrcItem.Campos = Value
            End Set
        End Property
        Public Property OrcKit() As Interfaces.iEVT.Primitivas.iOrcamentoObjs Implements Interfaces.iEVT.iCadatroOrcamento.OrcKit
            Get
                Return mOrcKit.Campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iOrcamentoObjs)
                mOrcKit.Campos = Value
            End Set
        End Property
        Public Property OrcDespAdic() As Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional Implements Interfaces.iEVT.iCadatroOrcamento.OrcDespAdic
            Get
                Return mOrcDespAdic.Campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional)
                mOrcDespAdic.Campos = Value
            End Set
        End Property
        Public Property OrcParcela() As Interfaces.iEVT.Primitivas.iOrcamentoParcelas Implements Interfaces.iEVT.iCadatroOrcamento.OrcParcela
            Get
                Return Me.mOrcParcela.Campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iOrcamentoParcelas)
                Me.mOrcParcela.Campos = Value
            End Set
        End Property
    End Class
#End Region

#Region "===[ Cadastro de Ordem de Serviço ]=============================================================="
    Public Class CadastroOrdemServico
        'Inherits Fachadas.allClass
        Implements Interfaces.iEVT.iCadatroOrdemServico

        Private Const mSource As String = "**** Cadastro de Ordem de Serviço ****"
        Protected mColOSObjs As New Fachadas.NbCollection
        Protected mOS As Fachadas.plxEVT.primitivas.OrdemServico
        Protected mOSObj As Fachadas.plxEVT.primitivas.OrdemServicoObjs
        Protected mTipoConexao As tipos.tiposConection
        Protected aSelf As self

        Public Sub New()
            Me.mOS = New Fachadas.plxEVT.primitivas.OrdemServico(aSelf)
            Me.mOSObj = New Fachadas.plxEVT.primitivas.OrdemServicoObjs(aSelf)
            aSelf = Me.mOS.Self
        End Sub
        Public Sub New(ByVal TipoConexao As tipos.tiposConection)
            Me.mTipoConexao = TipoConexao
            Me.mOS = New Fachadas.plxEVT.primitivas.OrdemServico(aSelf, TipoConexao)
            Me.mOSObj = New Fachadas.plxEVT.primitivas.OrdemServicoObjs(aSelf, TipoConexao)
            aSelf = Me.mOS.Self
        End Sub
        Public Sub Dispose() Implements System.IDisposable.Dispose
            If Not IsNothing(Me.mColOSObjs) Then Me.mColOSObjs.Dispose()
            Me.mColOSObjs = Nothing
            If Not IsNothing(Me.mOS) Then Me.mOS.Dispose()
            Me.mOS = Nothing
            If Not IsNothing(Me.mOSObj) Then Me.mOSObj.Dispose()
            Me.mOSObj = Nothing
        End Sub
        Public Sub Excluir(ByVal NoCommit As Boolean) Implements Interfaces.iEVT.iCadatroOrdemServico.Excluir
            Try
                'Excluindo o Kit
                mOS.excluir(NoCommit)
            Catch ex As NBexception
                ex.Source = mSource
                Throw ex
            End Try
        End Sub
        Public Sub Excluir() Implements Interfaces.iEVT.iCadatroOrdemServico.Excluir
            Me.Excluir(False)
        End Sub
        Public Sub Salvar(ByVal NoCommit As Boolean) Implements Interfaces.iEVT.iCadatroOrdemServico.Salvar
            'implementar: Salvar primeiro o Orçamento, depois os Itens, Kits, Despesas Adicionais e Parcelas.
            Dim idOS As Double
            Try

                'Salvando o Orçamento
                Me.OS.salvar(True)
                idOS = Me.OS.ID

                'Salvando os Objetos da Ordem Serviço
                For Each OSOBJ As Interfaces.iEVT.Primitivas.iOrdemServicoObjs In mColOSObjs.Values
                    OSOBJ.IdOS = idOS
                    OSOBJ.salvar(True)
                Next

                'Finaliza a Transação
                aself.AdmDB.FinalizaTransaction(NoCommit)

            Catch ex As NBexception
                ex.Source = mSource
                Throw ex
            End Try
        End Sub
        Public Sub Salvar() Implements Interfaces.iEVT.iCadatroOrdemServico.Salvar
            Me.Salvar(False)
        End Sub
        Public ReadOnly Property ColecaoOSObjs() As NbCollection
            Get
                Return mColOSObjs
            End Get
        End Property

        Public Overridable Sub getFieldsFromOS(ByVal idOS As Double)
            Dim DR As Data.DataRow
            Me.mOS.filterWhere = " idOS = " & idOS
            Me.mOS.getFields(True)

            '=== Carrega os Dados dos Itens ===
            Me.mOSObj.filterWhere = " idOrcamento = " & idOS & " AND idObj LIKE 'I%'"
            Me.mOSObj.getFields(True)
            If Me.mOSObj.DataSource.Count > 0 Then
                For Each DR In Me.mOSObj.DataSource.Table.Rows
                    Dim OSObj As NBdbm.Fachadas.plxEVT.primitivas.OrdemServicoObjs
                    'Verifica se esta sendo usado outro Tipo de Conexão que não seja a Padrão.
                    If Not IsNothing(mTipoConexao) Then
                        OSObj = New NBdbm.Fachadas.plxEVT.primitivas.OrdemServicoObjs(aSelf, mTipoConexao)
                    Else
                        OSObj = New NBdbm.Fachadas.plxEVT.primitivas.OrdemServicoObjs(aSelf)
                    End If
                    OSObj.filterWhere = " idorcamento = " & idOS & " and idObj = " & DR.Item("idObj").ToString
                    OSObj.getFields(True)
                    Me.OSObj = OSObj.Campos 'preenchendo a property com o último.
                    Me.ColecaoOSObjs.Add(OSObj.Campos.Key, CType(OSObj.Campos, Object))
                Next
            Else
                mOSObj.Clear_vars()
            End If
            If (aSelf.AdmDB.connection.State = ConnectionState.Open) Then
                aSelf.AdmDB.connection.Close()
            End If
        End Sub
        Public Sub LimpaColecoes()
            Me.mColOSObjs.Clear()
        End Sub
        Public Sub LimpaVars()
            Me.mOS.Clear_vars()
            Me.mOSObj.Clear_vars()
        End Sub
        Public Property OS() As Interfaces.iEVT.Primitivas.iOrdemServico Implements Interfaces.iEVT.iCadatroOrdemServico.OS
            Get
                Return mOS.campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iOrdemServico)
                mOS.campos = Value
            End Set
        End Property
        Public Property OSObj() As Interfaces.iEVT.Primitivas.iOrdemServicoObjs Implements Interfaces.iEVT.iCadatroOrdemServico.OSObjs
            Get
                Return mOSObj.Campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iOrdemServicoObjs)
                mOSObj.Campos = Value
            End Set
        End Property
    End Class
#End Region

#Region "===[ cadastro de Contas a Pagar e a Receber ]==="
    '===[ cadastro de Contas a Pagar e a Receber ]=======================================================================================
    Public Class CadastroContasPagarReceber
        Implements Interfaces.iEVT.iCadastroContasPagarReceber

        Private Const mSource As String = "**** Cadastro de Contas a Pagar e a Receber ****"
        Protected aParcelas As New Fachadas.NbCollection
        Protected aConta As Fachadas.plxEVT.primitivas.ContasPagarReceber
        Protected aParcela As Fachadas.plxEVT.primitivas.ContasPagarReceber_Parcelas
        Protected aTipoConexao As tipos.tiposConection
        Protected aSelf As self

        Public Sub New()
            aSelf = New self
            Me.aConta = New Fachadas.plxEVT.primitivas.ContasPagarReceber(aSelf)
            Me.aParcela = New Fachadas.plxEVT.primitivas.ContasPagarReceber_Parcelas(aSelf)
        End Sub

        Public Sub New(ByVal TipoConexao As tipos.tiposConection)
            aSelf = New self
            aTipoConexao = TipoConexao
            Me.aConta = New Fachadas.plxEVT.primitivas.ContasPagarReceber(aSelf, TipoConexao)
            Me.aParcela = New Fachadas.plxEVT.primitivas.ContasPagarReceber_Parcelas(aSelf, TipoConexao)
        End Sub

        Public Sub Dispose() Implements System.IDisposable.Dispose
            Me.aParcelas.Dispose()
            Me.aParcelas = Nothing
            Me.aParcela.Dispose()
            Me.aParcela = Nothing
            aTipoConexao = Nothing
        End Sub

        Public Overridable Sub getFieldsFromConta(ByVal idconta As Double)
            Dim DR As Data.DataRow
            Me.aConta.filterWhere = " ID = " & idconta
            Me.aConta.getFields(True)
            Me.aParcela.filterWhere = " idConta = " & idconta
            If Me.aParcela.DataSource.Count > 0 Then
                For Each DR In Me.aParcela.DataSource.Table.Rows
                    Dim parcela As NBdbm.Fachadas.plxEVT.primitivas.ContasPagarReceber_Parcelas
                    'Verifica se foi usado outro tipo de conexao que não a padrao
                    If Not IsNothing(Me.aTipoConexao) Then
                        parcela = New NBdbm.Fachadas.plxEVT.primitivas.ContasPagarReceber_Parcelas(aSelf, Me.aTipoConexao)
                    Else
                        parcela = New NBdbm.Fachadas.plxEVT.primitivas.ContasPagarReceber_Parcelas(aSelf)
                    End If
                    parcela.filterWhere = " idConta = " & idconta & " and ID = " & DR.Item("ID").ToString
                    parcela.getFields(True)
                    Me.aParcela = parcela.Campos 'preenchendo a property com o último.
                    Me.aParcelas.Add(parcela.Campos.Key, CType(parcela.Campos, Object))
                Next
            Else
                Me.aParcela.Clear_vars()
            End If
            If (aself.AdmDB.connection.State = ConnectionState.Open) Then
                aSelf.AdmDB.connection.Close()
            End If
        End Sub

        Public Property Conta() As Interfaces.iEVT.Primitivas.iContasPagarReceber Implements Interfaces.iEVT.iCadastroContasPagarReceber.Conta
            Get
                Return Me.aConta.Campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iContasPagarReceber)
                Me.aConta.Campos = Value
            End Set
        End Property

        Public Sub Excluir(ByVal NoCommit As Boolean) Implements Interfaces.iEVT.iCadastroContasPagarReceber.Excluir
            Try
                'Excluindo o Kit
                Me.aConta.excluir(NoCommit)
            Catch ex As NBexception
                ex.Source = mSource
                Throw ex
            End Try

        End Sub

        Public Sub Excluir() Implements Interfaces.iEVT.iCadastroContasPagarReceber.Excluir
            Me.Excluir(False)
        End Sub

        Public Sub Salvar(ByVal NoCommit As Boolean) Implements Interfaces.iEVT.iCadastroContasPagarReceber.Salvar
            Dim idConta As Double
            Try

                'Salvando a Conta
                Me.aConta.Campos.salvar(True)
                idConta = Me.aConta.Campos.ID

                'Salvando as Parcelas da Conta
                For Each parcela As Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas In Me.aParcelas.Values
                    parcela.idConta = idConta
                    parcela.salvar(True)
                Next

                'Finaliza a Transação
                aSelf.AdmDB.FinalizaTransaction(NoCommit)

            Catch ex As Exception
                ex.Source = mSource
                Throw ex
            End Try
        End Sub

        Public Sub Salvar() Implements Interfaces.iEVT.iCadastroContasPagarReceber.Salvar
            Me.Salvar(False)
        End Sub

        Public Property Parcela() As Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas Implements Interfaces.iEVT.iCadastroContasPagarReceber.Parcela
            Get
                Return Me.aParcela.Campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas)
                Me.aParcela.Campos = Value
            End Set
        End Property

        Public ReadOnly Property Parcelas() As NbCollection Implements Interfaces.iEVT.iCadastroContasPagarReceber.Parcelas
            Get
                Return Me.aParcelas
            End Get
        End Property

    End Class
#End Region

#Region "===[ Cadastro de Relatórios Customizados ]==="
    'Criador - Célio em 24/03/2006

    Public Class CadastroCustomROrc
        Implements Interfaces.iEVT.iCadastroCustomROrc

        Private Const mSource As String = "**** Cadastro de Relatórios de Orçamento - Customizados ****"
        Private mCustomROrc As Fachadas.plxEVT.primitivas.CustomROrc
        Private mCustomROrc_Grupos As Fachadas.plxEVT.primitivas.CustomROrc_Grupos
        Private mCustomROrc_ItemGrupo As Fachadas.plxEVT.primitivas.CustomROrc_ItensGrupo
        Private mColCustomROrc_Grupos As System.Collections.Hashtable
        Private mColCustomROrc_ItensGrupo As System.Collections.Hashtable
        Private aSelf As self

        Public Sub New()
            Me.New(tipos.tiposConection.Default_)
        End Sub
        Public Sub New(ByVal tipoconexao As tipos.tiposConection)
            mCustomROrc = New Fachadas.plxEVT.primitivas.CustomROrc(aSelf, tipoconexao)
            mCustomROrc_Grupos = New Fachadas.plxEVT.primitivas.CustomROrc_Grupos(aSelf, tipoconexao)
            mCustomROrc_ItemGrupo = New Fachadas.plxEVT.primitivas.CustomROrc_ItensGrupo(aSelf, tipoconexao)
            mColCustomROrc_Grupos = New System.Collections.Hashtable
            mColCustomROrc_ItensGrupo = New System.Collections.Hashtable
            aSelf = Me.mCustomROrc.Self

        End Sub
        Public ReadOnly Property ColCustomROrc_Grupos() As System.Collections.Hashtable Implements Interfaces.iEVT.iCadastroCustomROrc.ColCustomROrc_Grupos
            Get
                Return Me.mColCustomROrc_Grupos
            End Get
        End Property

        Public ReadOnly Property ColCustomROrc_ItensGrupo() As System.Collections.Hashtable Implements Interfaces.iEVT.iCadastroCustomROrc.ColCustomROrc_ItensGrupo
            Get
                Return Me.mColCustomROrc_ItensGrupo
            End Get
        End Property

        Public Property CustomROrc() As Interfaces.iEVT.Primitivas.iCustomROrc Implements Interfaces.iEVT.iCadastroCustomROrc.CustomROrc
            Get
                Return Me.mCustomROrc.Campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iCustomROrc)
                Me.mCustomROrc = Value
            End Set
        End Property

        Public Property CustomROrc_Grupos() As Interfaces.iEVT.Primitivas.iCustomROrc_Grupos Implements Interfaces.iEVT.iCadastroCustomROrc.CustomROrc_Grupos
            Get
                Return Me.mCustomROrc_Grupos.Campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iCustomROrc_Grupos)
                Me.mCustomROrc_Grupos = Value
            End Set
        End Property

        Public Property CustomROrc_ItemGrupo() As Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo Implements Interfaces.iEVT.iCadastroCustomROrc.CustomROrc_ItemGrupo
            Get
                Return Me.mCustomROrc_ItemGrupo.Campos
            End Get
            Set(ByVal Value As Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo)
                Me.mCustomROrc_ItemGrupo.Campos = Value
            End Set
        End Property

        Public Overloads Sub Excluir() Implements Interfaces.iEVT.iCadastroCustomROrc.Excluir
            Me.Excluir(False)
        End Sub

        Public Overloads Sub Excluir(ByVal nocommit As Boolean) Implements Interfaces.iEVT.iCadastroCustomROrc.Excluir
            Me.mCustomROrc.excluir(nocommit)
        End Sub

        Public Overloads Sub Salvar() Implements Interfaces.iEVT.iCadastroCustomROrc.Salvar
            Me.Salvar(False)
        End Sub

        Public Overloads Sub Salvar(ByVal nocommit As Boolean) Implements Interfaces.iEVT.iCadastroCustomROrc.Salvar
            Dim idCustomOrc As Integer

            Try
                'Salvando o CustomOrc
                Me.CustomROrc.salvar(True)

                'Salvando os Grupos de CustomOrc.
                For Each Grupo As Interfaces.iEVT.Primitivas.iCustomROrc_Grupos In Me.mColCustomROrc_Grupos.Values
                    Grupo.IdCustomRO = idCustomOrc
                    Grupo.salvar(True)
                    'Salvando os Itens do Grupo.
                    For Each ItemGrupo As Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo In Me.mColCustomROrc_ItensGrupo
                        ItemGrupo.Salvar(True)
                    Next
                Next

                'Finaliza a Transação
                aSelf.AdmDB.FinalizaTransaction(nocommit)

            Catch ex As NBexception
                ex.Source = mSource
                Throw ex
            End Try

        End Sub

        Public Sub Dispose() Implements System.IDisposable.Dispose
            If Not IsNothing(Me.mCustomROrc) Then Me.mCustomROrc.Dispose()
            Me.mCustomROrc = Nothing

            If Not IsNothing(Me.mCustomROrc_Grupos) Then Me.mCustomROrc_Grupos.Dispose()
            Me.mCustomROrc_Grupos = Nothing

            If Not IsNothing(Me.mCustomROrc_ItemGrupo) Then Me.mCustomROrc_ItemGrupo.Dispose()
            Me.mCustomROrc_ItemGrupo = Nothing

            If Not IsNothing(Me.mColCustomROrc_Grupos) Then Me.mColCustomROrc_Grupos.Clear()
            Me.mColCustomROrc_Grupos = Nothing

            If Not IsNothing(Me.mColCustomROrc_ItensGrupo) Then Me.mColCustomROrc_ItensGrupo.Clear()
            Me.mColCustomROrc_ItensGrupo = Nothing
        End Sub
    End Class
#End Region

    Namespace primitivas

#Region "===[ SPOOL ]==="
        '===[ SPOOL ]=======================================================================================
        'Off - Edgar
        Public Class Spool
            Inherits Fachadas.CTR.primitivas.Spool
            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf)
            End Sub

        End Class
#End Region

#Region "===[ QUALIDADE ]==="
        '===[ QUALIDADE ]=======================================================================================
        'Off - Edgar
        Public Class Qualidade
            Inherits allClass
            Protected mCampos As Interfaces.iEVT.Primitivas.iQualidade

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_Qualidade")
                mCampos = New QualidadeCampos(Me)
            End Sub
            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_Qualidade", TipoConexao)
                mCampos = New QualidadeCampos(Me)
            End Sub
            Public Property campos() As Interfaces.iEVT.Primitivas.iQualidade
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iQualidade)
                    mCampos = Value
                End Set
            End Property
            Public Sub getFieldsFromQualidade(ByVal idQualidade As Double)
                Me.filterWhere = "idQualidade = " & idQualidade
                Me.getFields(False)
            End Sub

            Private Class QualidadeCampos
                Implements Interfaces.iEVT.Primitivas.iQualidade
                Private aParent As allClass

                Public Sub New(ByRef parent As allClass)
                    Me.aParent = parent
                End Sub

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return aParent
                    End Get
                    Set(ByVal value As allClass)
                        Me.aParent = value
                    End Set
                End Property

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub
                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    aParent.Clear_filters()
                End Sub
                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    aParent.Clear_vars()
                End Sub
                Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " qualidade = '" & Me.qualidade_key & "' "
                    Me.aParent.SalvarPadrao(noCommit, filtro)
                End Sub
                Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub


#Region "   Propriedades - Fields   "

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.qualidade_key
                    End Get
                End Property

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return aParent.var("idQualidade").value
                    End Get
                End Property

                Public Property qualidade_key() As String Implements Interfaces.iEVT.Primitivas.iQualidade.qualidade_key
                    Get
                        Return aParent.var("qualidade").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("qualidade").Dirty = True
                        aParent.var("qualidade").value = Value
                    End Set
                End Property
#End Region
            End Class
        End Class
#End Region

#Region "===[ NOTA FISCAL ]==="
        '===[ NOTA FISCAL ]=======================================================================================
        'Off - Edgar
        Public Class NotaFiscal
            Inherits allClass
            Protected mCampos As Interfaces.iEVT.Primitivas.iNotaFiscal

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_NotaFiscal")
                mCampos = New NotaFiscalCampos(Me)
            End Sub
            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_NotaFiscal", TipoConexao)
                mCampos = New NotaFiscalCampos(Me)
            End Sub

            Public Property campos() As Interfaces.iEVT.Primitivas.iNotaFiscal
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iNotaFiscal)
                    mCampos = Value
                End Set
            End Property

            Private Class NotaFiscalCampos
                Implements Interfaces.iEVT.Primitivas.iNotaFiscal
                Private aParent As allClass

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal value As allClass)
                        Me.aParent = value
                    End Set
                End Property

                Public Sub New(ByRef parent As allClass)
                    Me.aParent = parent
                End Sub
                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub
                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    aParent.Clear_filters()
                End Sub
                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    aParent.Clear_vars()
                End Sub
                Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " numeroFiscal = " & Me.numeroFiscal_key
                    Me.aParent.SalvarPadrao(noCommit, filtro)
                End Sub
                Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

#Region "   Propriedades - Fields   "

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.numeroFiscal_key
                    End Get
                End Property

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return aParent.var("idNotaFiscal").value
                    End Get
                End Property

                Public Property numeroFiscal_key() As Integer Implements Interfaces.iEVT.Primitivas.iNotaFiscal.numeroFiscal_key
                    Get
                        Return aParent.var("numeroFiscal").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("numeroFiscal").Dirty = True
                        aParent.var("numeroFiscal").value = Value
                    End Set
                End Property

                Public Property beneficiario() As String Implements Interfaces.iEVT.Primitivas.iNotaFiscal.beneficiario
                    Get
                        Return aParent.var("beneficiario").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("beneficiario").Dirty = True
                        aParent.var("beneficiario").value = Value
                    End Set
                End Property

                Public Property cancelada() As Boolean Implements Interfaces.iEVT.Primitivas.iNotaFiscal.cancelada
                    Get
                        Return aParent.var("cancelada").value
                    End Get
                    Set(ByVal Value As Boolean)
                        aParent.var("cancelada").Dirty = True
                        aParent.var("cancelada").value = Value
                    End Set
                End Property

                Public Property CNPJ() As String Implements Interfaces.iEVT.Primitivas.iNotaFiscal.CNPJ
                    Get
                        Return aParent.var("CNPJ").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("CNPJ").Dirty = True
                        aParent.var("CNPJ").value = Value
                    End Set
                End Property

                Public Property dataFiscal() As Date Implements Interfaces.iEVT.Primitivas.iNotaFiscal.dataFiscal
                    Get
                        Return aParent.var("dataFiscal").value
                    End Get
                    Set(ByVal Value As Date)
                        aParent.var("dataFiscal").Dirty = True
                        aParent.var("dataFiscal").value = Value
                    End Set
                End Property

                Public Property endereco() As String Implements Interfaces.iEVT.Primitivas.iNotaFiscal.endereco
                    Get
                        Return aParent.var("endereco").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("endereco").Dirty = True
                        aParent.var("endereco").value = Value
                    End Set
                End Property

                Public Property fone() As String Implements Interfaces.iEVT.Primitivas.iNotaFiscal.fone
                    Get
                        Return aParent.var("fone").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("fone").Dirty = True
                        aParent.var("fone").value = Value
                    End Set
                End Property

                Public Property formaPagamento() As String Implements Interfaces.iEVT.Primitivas.iNotaFiscal.formaPagamento
                    Get
                        Return aParent.var("formaPagamento").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("fone").Dirty = True
                        aParent.var("fone").value = Value
                    End Set
                End Property

                Public Property idOrdemServico() As Integer Implements Interfaces.iEVT.Primitivas.iNotaFiscal.idOrdemServico
                    Get
                        Return aParent.var("idPrdemServico").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("idPrdemServico").Dirty = True
                        aParent.var("idPrdemServico").value = Value
                    End Set
                End Property

                Public Property IE() As String Implements Interfaces.iEVT.Primitivas.iNotaFiscal.IE
                    Get
                        Return aParent.var("IE").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("IE").Dirty = True
                        aParent.var("IE").value = Value
                    End Set
                End Property

                Public Property IM() As String Implements Interfaces.iEVT.Primitivas.iNotaFiscal.IM
                    Get
                        Return aParent.var("IM").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("IM").Dirty = True
                        aParent.var("IM").value = Value
                    End Set
                End Property

                Public Property municipio() As String Implements Interfaces.iEVT.Primitivas.iNotaFiscal.municipio
                    Get
                        Return aParent.var("municipio").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("municipio").Dirty = True
                        aParent.var("municipio").value = Value
                    End Set
                End Property

                Public Property UF() As String Implements Interfaces.iEVT.Primitivas.iNotaFiscal.UF
                    Get
                        Return aParent.var("UF").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("UF").Dirty = True
                        aParent.var("UF").value = Value
                    End Set
                End Property

#End Region

            End Class
        End Class
#End Region

#Region "===[ ITEM DA NOTA FISCAL ]==="
        '===[ ITEM DA NOTA FISCAL ]=======================================================================================
        'Off - Edgar
        Public Class NotaFiscalItem
            Inherits allClass
            Protected mCampos As Interfaces.iEVT.Primitivas.iNotaFiscalItem

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_NotaFiscalItem")
                mCampos = New NotaFiscalItemCampos(Me)
            End Sub
            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_NotaFiscalItem", TipoConexao)
                mCampos = New NotaFiscalItemCampos(Me)
            End Sub

            Public Property Campos() As Interfaces.iEVT.Primitivas.iNotaFiscalItem
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iNotaFiscalItem)
                    mCampos = Value
                End Set
            End Property

            Private Class NotaFiscalItemCampos
                Implements Interfaces.iEVT.Primitivas.iNotaFiscalItem
                Private aParent As allClass

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal value As allClass)
                        Me.aParent = value
                    End Set
                End Property

                Public Sub New(ByRef parent As allClass)
                    Me.aParent = parent
                End Sub
                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub
                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    aParent.Clear_filters()
                End Sub
                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    aParent.Clear_vars()
                End Sub
                Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " idnotafiscal = " & Me.idNotaFiscal_key & " and descricao = '" & Me.descricao_key & "' "
                    Me.aParent.SalvarPadrao(noCommit, filtro)
                End Sub
                Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

#Region "   Propriedades - Fields   "

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.idNotaFiscal_key & Me.descricao_key
                    End Get
                End Property

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return aParent.var("idNotaFiscalItem").value
                    End Get
                End Property

                Public Property descricao_key() As String Implements Interfaces.iEVT.Primitivas.iNotaFiscalItem.descricao_key
                    Get
                        Return aParent.var("descricao").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("descricao").Dirty = True
                        aParent.var("descricao").value = Value
                    End Set
                End Property

                Public Property idNotaFiscal_key() As Integer Implements Interfaces.iEVT.Primitivas.iNotaFiscalItem.idNotaFiscal_key
                    Get
                        Return aParent.var("idNotaFiscal").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("idNotaFiscal").Dirty = True
                        aParent.var("idNotaFiscal").value = Value
                    End Set
                End Property

                Public Property quantidade() As Integer Implements Interfaces.iEVT.Primitivas.iNotaFiscalItem.quantidade
                    Get
                        Return aParent.var("quantidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("quantidade").Dirty = True
                        aParent.var("quantidade").value = Value
                    End Set
                End Property

                Public Property unidade() As String Implements Interfaces.iEVT.Primitivas.iNotaFiscalItem.unidade
                    Get
                        Return aParent.var("unidade").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("unidade").Dirty = True
                        aParent.var("unidade").value = Value
                    End Set
                End Property

                Public Property valor() As Decimal Implements Interfaces.iEVT.Primitivas.iNotaFiscalItem.valor
                    Get
                        Return aParent.var("valor").value
                    End Get
                    Set(ByVal Value As Decimal)
                        aParent.var("valor").Dirty = True
                        aParent.var("valor").value = Value
                    End Set
                End Property

#End Region

            End Class
        End Class
#End Region

#Region "===[ ITEM ]==="
        '===[ ITEM ]=======================================================================================
        'Off - Célio
        Public Class Item
            Inherits allClass
            Private mCampos As ItemCampos 'Interfaces.iEVT.Primitivas.iLocalidades

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_Itens")
                mCampos = New ItemCampos(Me)
            End Sub
            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_Itens", TipoConexao)
                mCampos = New ItemCampos(Me)
            End Sub
            Public Property Campos() As Interfaces.iEVT.Primitivas.iItem
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iItem)
                    mCampos = Value
                    If Not Value Is Nothing Then
                        MyBase.toObject = mCampos.Parent
                    End If
                End Set
            End Property

            Private Class ItemCampos
                Implements Interfaces.iEVT.Primitivas.iItem

                Private aParent As allClass

                Public Sub New(ByRef allClass As allClass)
                    Me.aParent = allClass
                End Sub

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal Value As allClass)
                        Me.aParent = Value
                    End Set
                End Property

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub

                Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " descricao = '" & Me.Descricao_key & "' and NumeroSerie='" & Me.NumeroSerie & "' "
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
                        Return Parent.var("idItem").value
                    End Get
                End Property

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.Descricao_key & Me.NumeroSerie
                    End Get
                End Property

                Public ReadOnly Property IdObj() As String Implements Interfaces.iEVT.Primitivas.iItem.IdObj
                    Get
                        Return Parent.var("idObj").value
                    End Get
                End Property

                Public Property Descricao_key() As String Implements Interfaces.iEVT.Primitivas.iItem.Descricao_key
                    Get
                        Return Parent.var("descricao").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("descricao").Dirty = True
                        Parent.var("descricao").value = Value
                    End Set
                End Property

                Public Property Quantidade() As Integer Implements Interfaces.iEVT.Primitivas.iItem.Quantidade
                    Get
                        Return Parent.var("quantidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("quantidade").Dirty = True
                        Parent.var("quantidade").value = Value
                    End Set
                End Property

                Public Property ValorLocacao() As Double Implements Interfaces.iEVT.Primitivas.iItem.ValorLocacao
                    Get
                        Return CStr(Parent.var("ValorLocacao").value).Replace(".", ",")
                    End Get
                    Set(ByVal Value As Double)
                        Parent.var("ValorLocacao").Dirty = True
                        Parent.var("ValorLocacao").value = Value
                    End Set
                End Property

                Public Property DescMax() As Decimal Implements Interfaces.iEVT.Primitivas.iItem.DescMax
                    Get
                        Return Parent.var("DescMax").value
                    End Get
                    Set(ByVal Value As Decimal)
                        Parent.var("DescMax").Dirty = True
                        Parent.var("DescMax").value = Value
                    End Set
                End Property

                Public Property VidaUtil() As Integer Implements Interfaces.iEVT.Primitivas.iItem.VidaUtil
                    Get
                        Return Parent.var("vidaUtil").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("vidaUtil").Dirty = True
                        Parent.var("vidaUtil").value = Value
                    End Set
                End Property

                Public Property NumeroSerie() As String Implements Interfaces.iEVT.Primitivas.iItem.NumeroSerie
                    Get
                        Return Parent.var("NumeroSerie").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("NumeroSerie").Dirty = True
                        Parent.var("NumeroSerie").value = Value
                    End Set
                End Property

                Public Property IdQualidade() As Integer Implements Interfaces.iEVT.Primitivas.iItem.IdQualidade
                    Get
                        Return Parent.var("idQualidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idQualidade").Dirty = True
                        Parent.var("idQualidade").value = Value
                    End Set
                End Property

                Public Property IdDeposito() As Integer Implements Interfaces.iEVT.Primitivas.iItem.IdDeposito
                    Get
                        Return Parent.var("idDeposito").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idDeposito").Dirty = True
                        Parent.var("idDeposito").value = Value
                    End Set
                End Property

                Public Property IdFornecedor() As Integer Implements Interfaces.iEVT.Primitivas.iItem.IdFornecedor
                    Get
                        Return Parent.var("idFornecedor").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idFornecedor").Dirty = True
                        Parent.var("idFornecedor").value = Value
                    End Set
                End Property

                Public Property Comentario() As String Implements Interfaces.iEVT.Primitivas.iItem.Comentario
                    Get
                        Return Parent.var("comentarios").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("comentarios").Dirty = True
                        Parent.var("comentarios").value = Value
                    End Set
                End Property


#End Region

            End Class
        End Class
#End Region

#Region "===[ LOCALIDADES ]==="
        '===[ LOCALIDADES ]=======================================================================================
        'Off - Edgar
        Public Class Localidades
            Inherits allClass
            Private mCampos As LocalidadesCampos 'Interfaces.iEVT.Primitivas.iLocalidades

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_Localidades")
                mCampos = New LocalidadesCampos(Me)
            End Sub
            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_Localidades", TipoConexao)
                mCampos = New LocalidadesCampos(Me)
            End Sub
            Public Property Campos() As Interfaces.iEVT.Primitivas.iLocalidades
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iLocalidades)
                    mCampos = Value
                    If Not Value Is Nothing Then
                        MyBase.toObject = mCampos.Parent
                    End If
                End Set
            End Property

            Private Class LocalidadesCampos
                Implements Interfaces.iEVT.Primitivas.iLocalidades

                Private aParent As allClass

                Public Sub New(ByRef allClass As allClass)
                    Me.aParent = allClass
                End Sub
                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal Value As allClass)
                        Me.aParent = Value
                    End Set
                End Property

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub
                Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " idLocalidade = " & Me.ID & " and Nome = '" & Me.Nome_Key & "'  "
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
                        Return Me.idEntidade.ToString & Me.Nome_Key.ToString
                    End Get
                End Property

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("idLocalidade").value
                    End Get
                End Property

                Public Property Nome_Key() As String Implements Interfaces.iEVT.Primitivas.iLocalidades.Nome_key
                    Get
                        Return Parent.var("nome").value
                    End Get
                    Set(ByVal newValue As String)
                        Parent.var("nome").Dirty = True
                        Parent.var("nome").value = newValue
                    End Set
                End Property

                Public Property idEntidade() As Integer Implements Interfaces.iEVT.Primitivas.iLocalidades.idEntidade
                    Get
                        Return Parent.var("idEntidade").value
                    End Get
                    Set(ByVal newValue As Integer)
                        Parent.var("idEntidade").Dirty = True
                        Parent.var("idEntidade").value = newValue
                    End Set
                End Property

                Public Property ComoChegar() As String Implements Interfaces.iEVT.Primitivas.iLocalidades.comoChegar
                    Get
                        Return Parent.var("comoChegar").value
                    End Get
                    Set(ByVal newValue As String)
                        Parent.var("comoChegar").Dirty = True
                        Parent.var("comoChegar").value = newValue
                    End Set
                End Property
#End Region

            End Class

        End Class
#End Region

#Region "===[ DEPÓSITOS ]==="
        '===[ DEPÓSITOS ]=======================================================================================
        'Off - Célio
        Public Class Depositos
            Inherits allClass
            Private mCampos As DepositoCampos 'Interfaces.iEVT.Primitivas.iDeposito

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_Deposito")
                mCampos = New DepositoCampos(Me)
            End Sub
            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_Deposito", TipoConexao)
                mCampos = New DepositoCampos(Me)
            End Sub
            Public Property Campos() As Interfaces.iEVT.Primitivas.iDeposito
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iDeposito)
                    mCampos = Value
                    If Not Value Is Nothing Then
                        MyBase.toObject = mCampos.Parent
                    End If
                End Set
            End Property
            Public Sub getFieldsFromDeposito(ByVal idDeposito As Double)
                Me.filterWhere = "idDeposito = " & idDeposito
                Me.getFields(False)
            End Sub
            Private Class DepositoCampos
                Implements Interfaces.iEVT.Primitivas.iDeposito

                Private aParent As allClass

                Public Sub New(ByRef allClass As allClass)
                    Me.aParent = allClass
                End Sub
                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal Value As allClass)
                        Me.aParent = Value
                    End Set
                End Property
                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub
                Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " idDeposito = " & Me.ID & " and Nome = '" & Me.Nome_Key & "'  "
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
                        Return Me.ID.ToString & Me.Nome_Key.ToString
                    End Get
                End Property

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("idDeposito").value
                    End Get
                End Property

                Public Property Nome_Key() As String Implements Interfaces.iEVT.Primitivas.iDeposito.Nome_Key
                    Get
                        Return Parent.var("Nome").value
                    End Get
                    Set(ByVal newValue As String)
                        Parent.var("Nome").Dirty = True
                        Parent.var("Nome").value = newValue
                    End Set
                End Property

                Public Property Endereco() As String Implements Interfaces.iEVT.Primitivas.iDeposito.Endereco
                    Get
                        Return Parent.var("Endereco").value
                    End Get
                    Set(ByVal newValue As String)
                        Parent.var("Endereco").Dirty = True
                        Parent.var("Endereco").value = newValue
                    End Set
                End Property
#End Region

            End Class

        End Class
#End Region

#Region "===[ KIT ]==="
        '===[ KIT ]=======================================================================================
        'Off - Edgar
        Public Class kit
            Inherits allClass
            Protected mCampos As Interfaces.iEVT.Primitivas.iKit

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_Kit")
                mCampos = New KitCampos(Me)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_Kit", TipoConexao)
                mCampos = New KitCampos(Me)
            End Sub

            Public Property campos() As Interfaces.iEVT.Primitivas.iKit
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iKit)
                    mCampos = Value
                End Set
            End Property

            Private Class KitCampos
                Implements Interfaces.iEVT.Primitivas.iKit

                Private aParent As allClass

                Public Sub New(ByRef parent As allClass)
                    Me.aParent = parent
                End Sub

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal value As allClass)
                        Me.aParent = value
                    End Set
                End Property

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub
                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    aParent.Clear_filters()
                End Sub
                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    aParent.Clear_vars()
                End Sub
                Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " idKit = " & Me.ID.ToString
                    Me.aParent.SalvarPadrao(noCommit, filtro)
                End Sub
                Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

#Region "   Propriedades - Fields   "

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return aParent.var("idKit").value
                    End Get
                End Property

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.Nome_key
                    End Get
                End Property

                Public ReadOnly Property idObj() As String Implements Interfaces.iEVT.Primitivas.iKit.idObj
                    Get
                        Return aParent.var("idObj").value
                    End Get
                End Property

                Public Property Nome_key() As String Implements Interfaces.iEVT.Primitivas.iKit.Nome_key
                    Get
                        Return aParent.var("nome").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("nome").Dirty = True
                        aParent.var("nome").value = Value
                    End Set
                End Property

                Public Property DescMax() As Double Implements Interfaces.iEVT.Primitivas.iKit.DescMax
                    Get
                        Return CStr(aParent.var("DescMax").value).Replace(".", ",")
                    End Get
                    Set(ByVal Value As Double)
                        aParent.var("DescMax").Dirty = True
                        aParent.var("DescMax").value = Value
                    End Set
                End Property

                Public Property ValorLocacao() As Double Implements Interfaces.iEVT.Primitivas.iKit.ValorLocacao
                    Get
                        Return CStr(aParent.var("ValorLocacao").value).Replace(".", ",")
                    End Get
                    Set(ByVal Value As Double)
                        aParent.var("ValorLocacao").Dirty = True
                        aParent.var("ValorLocacao").value = Value
                    End Set
                End Property

                Public Property instrucoes() As String Implements Interfaces.iEVT.Primitivas.iKit.Instrucoes
                    Get
                        Return aParent.var("instrucoes").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("instrucoes").Dirty = True
                        aParent.var("instrucoes").value = Value
                    End Set
                End Property

                Public Property Quantidade() As Integer Implements Interfaces.iEVT.Primitivas.iKit.Quantidade
                    Get
                        Return aParent.var("Quantidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("Quantidade").Dirty = True
                        aParent.var("Quantidade").value = Value
                    End Set
                End Property
#End Region

            End Class
        End Class
#End Region

#Region "===[ KIT ITEM ]==="
        '===[ KIT ITEM ]=======================================================================================
        'Off - Edgar
        Public Class KitItem
            Inherits allClass
            Protected mCampos As Interfaces.iEVT.Primitivas.iKitItem

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_KitItem")
                mCampos = New KitItemCampos(Me)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_KitItem", TipoConexao)
                mCampos = New KitItemCampos(Me)
            End Sub

            Public Property Campos() As Interfaces.iEVT.Primitivas.iKitItem
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iKitItem)
                    mCampos = Value
                End Set
            End Property

            Private Class KitItemCampos
                Implements Interfaces.iEVT.Primitivas.iKitItem

                Private aParent As allClass
                Private aDescricao As String
                Private aValorLocacao As Double

                Public Sub New(ByRef parent As allClass)
                    Me.aParent = parent
                End Sub
                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal value As allClass)
                        Me.aParent = value
                    End Set
                End Property
                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub

                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    aParent.Clear_filters()
                End Sub

                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    aParent.Clear_vars()
                End Sub

                Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " idKit = " & Me.idKit_key & " and CodBarrasItem = '" & Me.CodBarrasItem_key + "'"
                    Me.aParent.SalvarPadrao(noCommit, filtro)
                End Sub
                Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

#Region "   Propriedades - Fields   "

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return aParent.var("idKitItem").value
                    End Get
                End Property

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.idKit_key.ToString & Me.CodBarrasItem_key.ToString
                    End Get
                End Property

                Public Property idKit_key() As Integer Implements Interfaces.iEVT.Primitivas.iKitItem.idKit_key
                    Get
                        Return aParent.var("idKit").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("idKit").Dirty = True
                        aParent.var("idKit").value = Value
                    End Set
                End Property

                Public Property CodBarrasItem_key() As String Implements Interfaces.iEVT.Primitivas.iKitItem.CodBarrasItem_key
                    Get
                        Return aParent.var("CodBarrasItem").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("CodBarrasItem").Dirty = True
                        aParent.var("CodBarrasItem").value = Value
                    End Set
                End Property

                Public Property Status() As Single Implements Interfaces.iEVT.Primitivas.iKitItem.Status
                    Get
                        Return aParent.var("Status").value
                    End Get
                    Set(ByVal Value As Single)
                        aParent.var("Status").Dirty = True
                        aParent.var("Status").value = Value
                    End Set
                End Property

                Public Property TransferidoPara() As String Implements Interfaces.iEVT.Primitivas.iKitItem.TransferidoPara
                    Get
                        Return aParent.var("TransferidoPara").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("TransferidoPara").Dirty = True
                        aParent.var("TransferidoPara").value = Value
                    End Set
                End Property

                'Propriedades Usadas somente para apresentar os dados nas Grids dos Kits.
                Public Property Descricao() As String Implements Interfaces.iEVT.Primitivas.iKitItem.Descricao
                    Get
                        Return Me.aDescricao
                    End Get
                    Set(ByVal Value As String)
                        Me.aDescricao = Value
                    End Set
                End Property

                Public Property ValorLocacao() As Double Implements Interfaces.iEVT.Primitivas.iKitItem.ValorLocacao
                    Get
                        Return Me.aValorLocacao
                    End Get
                    Set(ByVal Value As Double)
                        Me.aValorLocacao = Value
                    End Set
                End Property

#End Region
            End Class
        End Class
#End Region

#Region "===[ Orçamento ]==="
        '===[ Orçamento ]=======================================================================================
        'Off - Celio
        Public Class Orcamento
            Inherits allClass
            Protected mCampos As Interfaces.iEVT.Primitivas.iOrcamento

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_Orcamento")
                mCampos = New OrcamentoCampos(Me)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_Orcamento", TipoConexao)
                mCampos = New OrcamentoCampos(Me)
            End Sub

            Public Property campos() As Interfaces.iEVT.Primitivas.iOrcamento
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iOrcamento)
                    mCampos = Value
                End Set
            End Property

            Private Class OrcamentoCampos
                Implements Interfaces.iEVT.Primitivas.iOrcamento
                Private aParent As allClass
                Public Sub New(ByRef parent As allClass)
                    Me.aParent = parent
                End Sub
                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal value As allClass)
                        Me.aParent = value
                    End Set
                End Property
                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub
                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    aParent.Clear_filters()
                End Sub
                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    aParent.Clear_vars()
                End Sub
                Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " descricao = '" & Me.Descricao_Key & "' "
                    Me.aParent.SalvarPadrao(noCommit, filtro)
                End Sub
                Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

#Region "   Propriedades - Fields   "

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return aParent.var("idOrcamento").value
                    End Get
                End Property

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.Descricao_Key.ToString & Me.DataEmissao_Key.ToString("dd/MM/yyyy")
                    End Get
                End Property

                Public Property Contato() As String Implements Interfaces.iEVT.Primitivas.iOrcamento.Contato
                    Get
                        Return aParent.var("contato").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("contato").Dirty = True
                        aParent.var("contato").value = Value
                    End Set
                End Property

                Public Property Descricao_Key() As String Implements Interfaces.iEVT.Primitivas.iOrcamento.Descricao_Key
                    Get
                        Return aParent.var("descricao").value
                    End Get
                    Set(ByVal Value As String)
                        If Value = "" Then
                            Throw New EVTexception("Este campo é uma chave portanto ele não pode conter valor em branco", "*** Orçamento - Descrição ***")
                        End If
                        aParent.var("descricao").Dirty = True
                        aParent.var("descricao").value = Value
                    End Set
                End Property

                Public Property DataEmissao_Key() As Date Implements Interfaces.iEVT.Primitivas.iOrcamento.DataEmissao_Key
                    Get
                        Return aParent.var("DataEmissao").value
                    End Get
                    Set(ByVal Value As Date)
                        Dim dt As Date
                        Try
                            dt = aParent.var("DataEmissao").value
                        Catch ex As Exception
                            dt = "01/01/00"
                        End Try
                        If Value <> dt And Value < Today Then
                            Throw New EVTexception("A data de emissão não pode ser menor que a data de hoje", "*** Orçamento - Data de Emissão ***")
                        End If
                        aParent.var("DataEmissao").Dirty = True
                        aParent.var("DataEmissao").value = Value
                    End Set
                End Property

                Public Property DataValidade() As Date Implements Interfaces.iEVT.Primitivas.iOrcamento.DataValidade
                    Get
                        Return aParent.var("DataValidade").value
                    End Get
                    Set(ByVal Value As Date)
                        Dim dt As Date
                        Try
                            dt = aParent.var("DataValidade").value
                        Catch ex As Exception
                            dt = "01/01/00"
                        End Try
                        If Value <> dt And Value < Today And Not Convert.ToBoolean(Me.IsModelo) Then
                            Throw New EVTexception("A data de validade não pode ser menor que a data de hoje", "*** Orçamento - Data de Validade ***")
                        End If
                        aParent.var("DataValidade").Dirty = True
                        aParent.var("DataValidade").value = Value
                    End Set
                End Property

                Public Property DataVencimento() As Date Implements Interfaces.iEVT.Primitivas.iOrcamento.DataVencimento
                    Get
                        Return aParent.var("DataVencimento").value
                    End Get
                    Set(ByVal Value As Date)
                        aParent.var("DataVencimento").Dirty = True
                        aParent.var("DataVencimento").value = Value
                    End Set
                End Property

                Public Property idEntidade() As Integer Implements Interfaces.iEVT.Primitivas.iOrcamento.IdEntidade
                    Get
                        Return aParent.var("idEntidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("idEntidade").Dirty = True
                        aParent.var("idEntidade").value = Value
                    End Set
                End Property

                Public Property idLocalidade() As Integer Implements Interfaces.iEVT.Primitivas.iOrcamento.IdLocalidade
                    Get
                        Return aParent.var("idLocalidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("idLocalidade").Dirty = True
                        aParent.var("idLocalidade").value = Value
                    End Set
                End Property

                Public Property Confirmado() As Short Implements Interfaces.iEVT.Primitivas.iOrcamento.Confirmado
                    Get
                        Dim tmp As Boolean
                        tmp = aParent.var("confirmado").value
                        Return Convert.ToInt32(tmp)
                    End Get
                    Set(ByVal Value As Short)
                        aParent.var("confirmado").Dirty = True
                        aParent.var("confirmado").value = Value
                    End Set
                End Property

                Public Property FormaPagamento() As String Implements Interfaces.iEVT.Primitivas.iOrcamento.FormaPagamento
                    Get
                        Return aParent.var("formaPagamento").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("formaPagamento").Dirty = True
                        aParent.var("formaPagamento").value = Value
                    End Set
                End Property

                Public Property PercDesconto() As Double Implements Interfaces.iEVT.Primitivas.iOrcamento.PercDesconto
                    Get
                        Return CStr(aParent.var("percDesc").value).Replace(".", ",")
                    End Get
                    Set(ByVal Value As Double)
                        aParent.var("percDesc").Dirty = True
                        aParent.var("percDesc").value = Value
                    End Set
                End Property

                Public Property IsModelo() As Short Implements Interfaces.iEVT.Primitivas.iOrcamento.IsModelo
                    Get
                        Dim tmp As Boolean
                        tmp = aParent.var("ismodelo").value
                        Return Convert.ToInt32(tmp)
                    End Get
                    Set(ByVal Value As Short)
                        aParent.var("ismodelo").Dirty = True
                        aParent.var("ismodelo").value = Value
                    End Set
                End Property

                Public Property Valor() As Double Implements Interfaces.iEVT.Primitivas.iOrcamento.Valor
                    Get
                        Return CStr(aParent.var("Valor").value).Replace(".", ",")
                    End Get
                    Set(ByVal Value As Double)
                        aParent.var("Valor").Dirty = True
                        aParent.var("Valor").value = Value
                    End Set
                End Property

#End Region
            End Class
        End Class
#End Region

#Region "===[ Orçamento Objetos ]==="
        '===[ Orçamento Objetos ]=======================================================================================
        'Off - Edgar
        Public Class OrcamentoObjs
            Inherits allClass
            Protected mCampos As Interfaces.iEVT.Primitivas.iOrcamentoObjs

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_OrcamentoObjs")
                mCampos = New OrcamentoObjsCampos(Me)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_OrcamentoObjs", TipoConexao)
                mCampos = New OrcamentoObjsCampos(Me)
            End Sub

            Public Property Campos() As Interfaces.iEVT.Primitivas.iOrcamentoObjs
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iOrcamentoObjs)
                    mCampos = Value
                End Set
            End Property

            Private Class OrcamentoObjsCampos
                Implements Interfaces.iEVT.Primitivas.iOrcamentoObjs

                Private aParent As allClass
                Private mCtrObj As Interfaces.iEVT.Primitivas.iCtrObj

                Public Sub New(ByRef parent As allClass)
                    Me.aParent = parent
                End Sub
                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal value As allClass)
                        Me.aParent = value
                    End Set
                End Property
                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub
                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    aParent.Clear_filters()
                End Sub
                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    aParent.Clear_vars()
                End Sub
                Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " idobj = '" & Me.IdObj_key & "' and idOrcamento = " & Me.idOrcamento_key.ToString & " and dataLocacaoInicio = '" & Me.DataLocacaoInicio_key.ToString("MM/dd/yyyy") & "' "
                    Me.aParent.SalvarPadrao(noCommit, filtro)
                End Sub
                Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub
                Public Property CtrObj() As Interfaces.iEVT.Primitivas.iCtrObj Implements Interfaces.iEVT.Primitivas.iOrcamentoObjs.CtrObj
                    Get
                        Return Me.mCtrObj
                    End Get
                    Set(ByVal Value As Interfaces.iEVT.Primitivas.iCtrObj)
                        Me.mCtrObj = Value
                    End Set
                End Property

#Region "   Propriedades - Fields   "

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return aParent.var("idOrcObj").value
                    End Get
                End Property

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.idOrcamento_key.ToString & Me.IdObj_key.ToString & Me.DataLocacaoInicio_key.ToShortDateString
                    End Get
                End Property

                Public Property IdObj_key() As String Implements Interfaces.iEVT.Primitivas.iOrcamentoObjs.IdObj_key
                    Get
                        Return aParent.var("idobj").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("idobj").Dirty = True
                        aParent.var("idobj").value = Value
                    End Set
                End Property

                Public Property idOrcamento_key() As Integer Implements Interfaces.iEVT.Primitivas.iOrcamentoObjs.IdOrcamento_key
                    Get
                        Return aParent.var("idOrcamento").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("idOrcamento").Dirty = True
                        aParent.var("idOrcamento").value = Value
                    End Set
                End Property

                Public Property DataLocacaoFinal() As Date Implements Interfaces.iEVT.Primitivas.iOrcamentoObjs.DataLocacaoFinal
                    Get
                        Return aParent.var("dataLocacaoFim").value
                    End Get
                    Set(ByVal Value As Date)
                        aParent.var("dataLocacaoFim").Dirty = True
                        aParent.var("dataLocacaoFim").value = Value
                    End Set
                End Property

                Public Property DataLocacaoInicio_key() As Date Implements Interfaces.iEVT.Primitivas.iOrcamentoObjs.DataLocacaoInicio_key
                    Get
                        Return aParent.var("dataLocacaoInicio").value
                    End Get
                    Set(ByVal Value As Date)
                        aParent.var("dataLocacaoInicio").Dirty = True
                        aParent.var("dataLocacaoInicio").value = Value
                    End Set
                End Property

                Public Property Quantidade() As Double Implements Interfaces.iEVT.Primitivas.iOrcamentoObjs.Quantidade
                    Get
                        Return CStr(aParent.var("quantidade").value).Replace(".", ",")
                    End Get
                    Set(ByVal Value As Double)
                        aParent.var("quantidade").Dirty = True
                        aParent.var("quantidade").value = Value
                    End Set
                End Property

                Public Property ValorUnitario() As Double Implements Interfaces.iEVT.Primitivas.iOrcamentoObjs.ValorUnitario
                    Get
                        Return CStr(aParent.var("ValorUnitario").value).Replace(".", ",")
                    End Get
                    Set(ByVal Value As Double)
                        aParent.var("ValorUnitario").Dirty = True
                        aParent.var("ValorUnitario").value = Value
                    End Set
                End Property

                Public Property ValorLocacao() As Double Implements Interfaces.iEVT.Primitivas.iOrcamentoObjs.ValorLocacao
                    Get
                        Return CStr(aParent.var("ValorLocacao").value).Replace(".", ",")
                    End Get
                    Set(ByVal Value As Double)
                        aParent.var("valorLocacao").Dirty = True
                        aParent.var("valorLocacao").value = Value
                    End Set
                End Property

                Public Property Descricao() As String Implements Interfaces.iEVT.Primitivas.iOrcamentoObjs.Descricao
                    Get
                        Return aParent.var("descricao").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("descricao").Dirty = True
                        aParent.var("descricao").value = Value
                    End Set
                End Property

#End Region

            End Class
        End Class
#End Region

#Region "===[ Orçamento Despesas Adicionais ]==="
        '===[ Ordem de Servico Despesas Adicionais ]=======================================================================================
        'Off - Edgar
        Public Class OrcamentoDespAdicional
            Inherits allClass
            Protected mCampos As Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_OrcamentoDespAdicional")
                mCampos = New OrcamentoDespAdicionalCampos(Me)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_OrcamentoDespAdicional", TipoConexao)
                mCampos = New OrcamentoDespAdicionalCampos(Me)
            End Sub

            Public Property Campos() As Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional)
                    mCampos = Value
                End Set
            End Property

            Private Class OrcamentoDespAdicionalCampos
                Implements Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional

                Private aParent As allClass

                Public Sub New(ByRef parent As allClass)
                    Me.aParent = parent
                End Sub

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal value As allClass)
                        Me.aParent = value
                    End Set
                End Property

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub
                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    aParent.Clear_filters()
                End Sub
                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    aParent.Clear_vars()
                End Sub
                Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " idOrcamento = " & Me.IdOrcamento_Key & " and Descricao = '" & Me.Descricao_Key & "' "
                    Me.aParent.SalvarPadrao(noCommit, filtro)
                End Sub
                Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

#Region "   Propriedades - Fields   "

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return aParent.var("idDespAdicional").value
                    End Get
                End Property

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.IdOrcamento_Key.ToString & Me.Descricao_Key.ToString
                    End Get
                End Property

                Public Property IdOrcamento_Key() As Integer Implements Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional.IdOrcamento_Key
                    Get
                        Return aParent.var("idOrcamento").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("idOrcamento").Dirty = True
                        aParent.var("idOrcamento").value = Value
                    End Set
                End Property

                Public Property Descricao_Key() As String Implements Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional.Descricao_Key
                    Get
                        Return aParent.var("descricao").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("descricao").Dirty = True
                        aParent.var("descricao").value = Value
                    End Set
                End Property

                Public Property Quantidade() As Integer Implements Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional.Quantidade
                    Get
                        Return aParent.var("Quantidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("Quantidade").Dirty = True
                        aParent.var("Quantidade").value = Value
                    End Set
                End Property

                Public Property ValorDespesa() As Double Implements Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional.ValorDespesa
                    Get
                        Return CStr(aParent.var("ValorDespesa").value).Replace(".", ",")
                    End Get
                    Set(ByVal Value As Double)
                        aParent.var("ValorDespesa").Dirty = True
                        aParent.var("ValorDespesa").value = Value
                    End Set
                End Property

#End Region

            End Class
        End Class
#End Region

#Region "===[ Orçameto Parcelas ]==="
        '===[ Orçameto Parcelas ]=======================================================================================
        'Off - Célio
        Public Class OrcamentoParcelas
            Inherits allClass
            Protected mCampos As Interfaces.iEVT.Primitivas.iOrcamentoParcelas

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_OrcamentoParcelas")
                mCampos = New OrcamentoParcelasCampos(Me)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_OrcamentoParcelas", TipoConexao)
                mCampos = New OrcamentoParcelasCampos(Me)
            End Sub

            Public Property Campos() As Interfaces.iEVT.Primitivas.iOrcamentoParcelas
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iOrcamentoParcelas)
                    mCampos = Value
                End Set
            End Property

            Private Class OrcamentoParcelasCampos
                Implements Interfaces.iEVT.Primitivas.iOrcamentoParcelas

                Private aParent As allClass

                Public Sub New(ByRef parent As allClass)
                    Me.aParent = parent
                End Sub

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal value As allClass)
                        Me.aParent = value
                    End Set
                End Property

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub
                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    aParent.Clear_filters()
                End Sub
                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    aParent.Clear_vars()
                End Sub
                Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " idOrcamento = " & Me.IdOrcamento_key.ToString & " and numeroParcela = " & Me.NumeroParcela_key.ToString & " "
                    Me.aParent.SalvarPadrao(noCommit, filtro)
                End Sub
                Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

#Region "   Propriedades - Fields   "

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return aParent.var("idParcela").value
                    End Get
                End Property

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.IdOrcamento_key.ToString & Me.NumeroParcela_key.ToString("000")
                    End Get
                End Property

                Public Property DataVencimento() As Date Implements Interfaces.iEVT.Primitivas.iOrcamentoParcelas.DataVencimento
                    Get
                        Return aParent.var("dataVencimento").value
                    End Get
                    Set(ByVal Value As Date)
                        aParent.var("dataVencimento").Dirty = True
                        aParent.var("dataVencimento").value = Value
                    End Set
                End Property

                Public Property IdOrcamento_key() As Integer Implements Interfaces.iEVT.Primitivas.iOrcamentoParcelas.IdOrcamento_key
                    Get
                        Return aParent.var("idOrcamento").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("idOrcamento").Dirty = True
                        aParent.var("idOrcamento").value = Value
                    End Set
                End Property

                Public Property NumeroParcela_key() As Integer Implements Interfaces.iEVT.Primitivas.iOrcamentoParcelas.NumeroParcela_key
                    Get
                        Return aParent.var("numeroParcela").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("numeroParcela").Dirty = True
                        aParent.var("numeroParcela").value = Value
                    End Set
                End Property

                Public Property ValorParcela() As Double Implements Interfaces.iEVT.Primitivas.iOrcamentoParcelas.ValorParcela
                    Get
                        Dim tmp As String
                        tmp = aParent.var("valorParcela").value
                        Return Convert.ToDouble(tmp.Replace(".", ","))
                    End Get
                    Set(ByVal Value As Double)
                        aParent.var("valorParcela").Dirty = True
                        aParent.var("valorParcela").value = Value
                    End Set
                End Property
#End Region
            End Class
        End Class
#End Region

#Region "===[ Ordem de Serviço ]==="
        '===[ Ordem de Serviço ]=======================================================================================
        'Off - Celio
        Public Class OrdemServico
            Inherits allClass
            Protected mCampos As Interfaces.iEVT.Primitivas.iOrdemServico

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_OrdemServico")
                mCampos = New OrdemServicoCampos(Me)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_OrdemServico", TipoConexao)
                mCampos = New OrdemServicoCampos(Me)
            End Sub

            Public Property campos() As Interfaces.iEVT.Primitivas.iOrdemServico
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iOrdemServico)
                    mCampos = Value
                End Set
            End Property

            Private Class OrdemServicoCampos
                Implements Interfaces.iEVT.Primitivas.iOrdemServico
                Private aParent As allClass
                Public Sub New(ByRef parent As allClass)
                    Me.aParent = parent
                End Sub

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal value As allClass)
                        Me.aParent = value
                    End Set
                End Property

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub
                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    aParent.Clear_filters()
                End Sub
                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    aParent.Clear_vars()
                End Sub
                Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " descricao = " & Me.Descricao & " "
                    Me.aParent.SalvarPadrao(noCommit, filtro)
                End Sub
                Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

#Region "   Propriedades - Fields   "

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return aParent.var("idOS").value
                    End Get
                End Property

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.ID
                    End Get
                End Property

                Public Property IdOrcamento() As Integer Implements Interfaces.iEVT.Primitivas.iOrdemServico.IdOrcamento
                    Get
                        Return aParent.var("idorcamento").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("idorcamento").Dirty = True
                        aParent.var("idorcamento").value = Value
                    End Set
                End Property

                Public Property Descricao() As String Implements Interfaces.iEVT.Primitivas.iOrdemServico.Descricao
                    Get
                        Return aParent.var("descricao").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("descricao").Dirty = True
                        aParent.var("descricao").value = Value
                    End Set
                End Property

                Public Property DataEmissao() As Date Implements Interfaces.iEVT.Primitivas.iOrdemServico.DataEmissao
                    Get
                        Return aParent.var("DataEmissao").value
                    End Get
                    Set(ByVal Value As Date)
                        aParent.var("DataEmissao").Dirty = True
                        aParent.var("DataEmissao").value = Value
                    End Set
                End Property

                Public Property DataAlteracao() As Date Implements Interfaces.iEVT.Primitivas.iOrdemServico.DataAlteracao
                    Get
                        Return aParent.var("DataAlteracao").value
                    End Get
                    Set(ByVal Value As Date)
                        aParent.var("DataAlteracao").Dirty = True
                        aParent.var("DataAlteracao").value = Value
                    End Set
                End Property

                Public Property idUsuario() As Integer Implements Interfaces.iEVT.Primitivas.iOrdemServico.IdUsuario
                    Get
                        Return aParent.var("idUsuario").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("idUsuario").Dirty = True
                        aParent.var("idUsuario").value = Value
                    End Set
                End Property
#End Region
            End Class
        End Class
#End Region

#Region "===[ Ordem de Serviço Objetos ]==="
        '===[ Ordem de Serviço Objetos ]=======================================================================================
        'Off - Edgar
        Public Class OrdemServicoObjs
            Inherits allClass
            Protected mCampos As Interfaces.iEVT.Primitivas.iOrdemServicoObjs
            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_OrdemServicoObjs")
                mCampos = New OrdemServicoObjsCampos(Me)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_OrdemServicoObjs", TipoConexao)
                mCampos = New OrdemServicoObjsCampos(Me)
            End Sub

            Public Property Campos() As Interfaces.iEVT.Primitivas.iOrdemServicoObjs
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iOrdemServicoObjs)
                    mCampos = Value
                End Set
            End Property

            Private Class OrdemServicoObjsCampos
                Implements Interfaces.iEVT.Primitivas.iOrdemServicoObjs

                Private aParent As allClass
                Private mCtrObj As Interfaces.iEVT.Primitivas.iCtrObj

                Public Sub New(ByRef parent As allClass)
                    Me.aParent = parent
                End Sub

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal value As allClass)
                        Me.aParent = value
                    End Set
                End Property

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub
                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    aParent.Clear_filters()
                End Sub
                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    aParent.Clear_vars()
                End Sub
                Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " idOS = " & Me.IdOS_key.ToString & " and idobj = '" & Me.IdObj_key.ToString & "'"
                    Me.aParent.SalvarPadrao(noCommit, filtro)
                End Sub
                Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub
                Public Property CtrObj() As Interfaces.iEVT.Primitivas.iCtrObj Implements Interfaces.iEVT.Primitivas.iOrdemServicoObjs.CtrObj
                    Get
                        Return Me.mCtrObj
                    End Get
                    Set(ByVal Value As Interfaces.iEVT.Primitivas.iCtrObj)
                        Me.mCtrObj = Value
                    End Set
                End Property

#Region "   Propriedades - Fields   "

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return aParent.var("idOSObj").value
                    End Get
                End Property

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.IdOS_key.ToString & Me.IdObj_key.ToString
                    End Get
                End Property

                Public Property IdObj_key() As String Implements Interfaces.iEVT.Primitivas.iOrdemServicoObjs.IdObj
                    Get
                        Return aParent.var("idobj").value
                    End Get
                    Set(ByVal Value As String)
                        aParent.var("idobj").Dirty = True
                        aParent.var("idobj").value = Value
                    End Set
                End Property

                Public Property IdOS_key() As Integer Implements Interfaces.iEVT.Primitivas.iOrdemServicoObjs.IdOS
                    Get
                        Return aParent.var("idOS").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("idOS").Dirty = True
                        aParent.var("idOS").value = Value
                    End Set
                End Property

                Public Property DataDevolucao() As Date Implements Interfaces.iEVT.Primitivas.iOrdemServicoObjs.DataDevolucao
                    Get
                        Return aParent.var("DataDevolucao").value
                    End Get
                    Set(ByVal Value As Date)
                        aParent.var("DataDevolucao").Dirty = True
                        aParent.var("DataDevolucao").value = Value
                    End Set
                End Property

                Public Property Quantidade() As Integer Implements Interfaces.iEVT.Primitivas.iOrdemServicoObjs.Quantidade
                    Get
                        Return aParent.var("quantidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        aParent.var("quantidade").Dirty = True
                        aParent.var("quantidade").value = Value
                    End Set
                End Property

#End Region

            End Class
        End Class
#End Region

#Region "===[ LinkItemNO ]==="
        '===[ LinkEstoqueNO ]==================================================================================================
        'Ok - Célio em 05/07/2005
        Public Class LinkItemNo
            Inherits allClass
            Private mCampos As LinkItemNoCampos 'Interfaces.iEVT.Primitivas.iLinkEstoqueNo

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_Link_Item_No")
                mCampos = New LinkItemNoCampos(Me)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_Link_Item_No", TipoConexao)
                mCampos = New LinkItemNoCampos(Me)
            End Sub

            Public Property Campos() As Interfaces.iEVT.Primitivas.iLinkItemNo
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iLinkItemNo)
                    mCampos = Value
                    If Not Value Is Nothing Then
                        MyBase.toObject = mCampos.Parent
                    End If
                End Set
            End Property

            Private Class LinkItemNoCampos
                Implements Interfaces.iEVT.Primitivas.iLinkItemNo

                Private aParent As allClass
                Public Sub New(ByRef allClass As allClass)
                    Me.aParent = allClass
                End Sub
                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal Value As allClass)
                        Me.aParent = Value
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
                    filtro = " idItem = " & Me.idItem
                    Me.Parent.SalvarPadrao(noCommit, filtro)
                End Sub
                Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    Parent = Nothing
                End Sub

#Region "   Propriedades - Fields   "

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.ID.ToString
                    End Get
                End Property

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("IdLink_Item_No").value
                    End Get
                End Property

                Public Property idItem() As Integer Implements Interfaces.iEVT.Primitivas.iLinkItemNo.idItem
                    Get
                        Return Parent.var("idItem").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idItem").value = Value
                        Parent.var("idItem").Dirty = True
                    End Set
                End Property

                Public Property idNo() As Integer Implements Interfaces.iEVT.Primitivas.iLinkItemNo.idNo
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
#End Region

#Region "===[ StatusReal ]==="
        '===[ StatusReal ]==================================================================================================
        'Ok - Célio em 17/11/2005
        Public Class StatusReal
            Inherits allClass
            Private mCampos As StatusRealCampos 'Interfaces.iEVT.Primitivas.iStatusReal

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_StatusReal")
                mCampos = New StatusRealCampos(Me)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_StatusReal", TipoConexao)
                mCampos = New StatusRealCampos(Me)
            End Sub

            Public Sub GetFieldsFromIdObj(ByVal idobj As String)
                Me.Clear_filters()
                Me.filterWhere = "idobj = '" + idobj + "'"
                Me.getFields(False)
            End Sub
            Public Sub GetFieldsFromCodBarras(ByVal codbarras As String)
                Me.Clear_filters()
                Me.filterWhere = "codbarras='" + codbarras + "'"
                Me.getFields(False)
            End Sub
            Public Property Campos() As Interfaces.iEVT.Primitivas.iStatusReal
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iStatusReal)
                    mCampos = Value
                    If Not Value Is Nothing Then
                        MyBase.toObject = mCampos.Parent
                    End If
                End Set
            End Property

            Private Class StatusRealCampos
                Implements Interfaces.iEVT.Primitivas.iStatusReal

                Private aParent As allClass

                Public Sub New(ByRef allClass As allClass)
                    Me.aParent = allClass
                End Sub

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal Value As allClass)
                        Me.aParent = Value
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
                    filtro = " CodBarras = '" & Me.CodBarras & "'"
                    Me.Parent.SalvarPadrao(noCommit, filtro)
                End Sub

                Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    Parent = Nothing
                End Sub

#Region "   Propriedades - Fields   "

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.ID.ToString
                    End Get
                End Property

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("ID").value
                    End Get
                End Property

                Public Property IdItem() As Integer Implements Interfaces.iEVT.Primitivas.iStatusReal.IdItem
                    Get
                        Return Parent.var("idItem").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idItem").value = Value
                        Parent.var("idItem").Dirty = True
                    End Set
                End Property

                Public Property IdKit() As Integer Implements Interfaces.iEVT.Primitivas.iStatusReal.IdKit
                    Get
                        Return Parent.var("IdKit").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("IdKit").value = Value
                        Parent.var("IdKit").Dirty = True
                    End Set
                End Property

                Public Property IdObj() As String Implements Interfaces.iEVT.Primitivas.iStatusReal.IdObj
                    Get
                        Return Parent.var("IdObj").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("IdObj").value = Value
                        Parent.var("IdObj").Dirty = True
                    End Set
                End Property

                Public Property IdEntidade() As Integer Implements Interfaces.iEVT.Primitivas.iStatusReal.IdEntidade
                    Get
                        Return Parent.var("idEntidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idEntidade").value = Value
                        Parent.var("idEntidade").Dirty = True
                    End Set
                End Property

                Public Property CodBarras() As String Implements Interfaces.iEVT.Primitivas.iStatusReal.CodBarras
                    Get
                        Return Parent.var("CodBarras").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("CodBarras").value = Value
                        Parent.var("CodBarras").Dirty = True
                    End Set
                End Property

                Public Property DataDevolucao() As Date Implements Interfaces.iEVT.Primitivas.iStatusReal.DataDevolucao
                    Get
                        Return Parent.var("DataDevolucao").value
                    End Get
                    Set(ByVal Value As Date)
                        Parent.var("DataDevolucao").value = Value
                        Parent.var("DataDevolucao").Dirty = True
                    End Set
                End Property

                Public Property Status() As Single Implements Interfaces.iEVT.Primitivas.iStatusReal.Status
                    Get
                        Return Parent.var("Status").value
                    End Get
                    Set(ByVal Value As Single)
                        Parent.var("Status").value = Value
                        Parent.var("Status").Dirty = True
                    End Set
                End Property

#End Region
            End Class
        End Class
#End Region

#Region "===[ StatusHistorico ]==="
        '===[ StatusHistorico ]==================================================================================================
        'Ok - Célio em 17/11/2005
        Public Class StatusHistorico
            Inherits allClass
            Private mCampos As StatusHistoricoCampos 'Interfaces.iEVT.Primitivas.iStatusReal

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_StatusHistorico")
                mCampos = New StatusHistoricoCampos(Me)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_StatusHistorico", TipoConexao)
                mCampos = New StatusHistoricoCampos(Me)
            End Sub

            Public Property Campos() As Interfaces.iEVT.Primitivas.iStatusReal
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iStatusReal)
                    mCampos = Value
                    If Not Value Is Nothing Then
                        MyBase.toObject = mCampos.Parent
                    End If
                End Set
            End Property

            Private Class StatusHistoricoCampos
                Implements Interfaces.iEVT.Primitivas.iStatusHistorico

                Private aParent As allClass

                Public Sub New(ByRef allClass As allClass)
                    Me.aParent = allClass
                End Sub

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal Value As allClass)
                        Me.aParent = Value
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
                    filtro = " CodBarras = '" & Me.CodBarras & "'"
                    Me.Parent.SalvarPadrao(noCommit, filtro)
                End Sub

                Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    Parent = Nothing
                End Sub

#Region "   Propriedades - Fields   "

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.ID.ToString
                    End Get
                End Property

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("ID").value
                    End Get
                End Property

                Public Property IdItem() As Integer Implements Interfaces.iEVT.Primitivas.iStatusHistorico.IdItem
                    Get
                        Return Parent.var("idItem").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idItem").value = Value
                        Parent.var("idItem").Dirty = True
                    End Set
                End Property

                Public Property IdKit() As Integer Implements Interfaces.iEVT.Primitivas.iStatusHistorico.IdKit
                    Get
                        Return Parent.var("IdKit").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("IdKit").value = Value
                        Parent.var("IdKit").Dirty = True
                    End Set
                End Property

                Public Property IdObj() As String Implements Interfaces.iEVT.Primitivas.iStatusHistorico.IdObj
                    Get
                        Return Parent.var("IdObj").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("IdObj").value = Value
                        Parent.var("IdObj").Dirty = True
                    End Set
                End Property

                Public Property IdEntidade() As Integer Implements Interfaces.iEVT.Primitivas.iStatusHistorico.IdEntidade
                    Get
                        Return Parent.var("idEntidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idEntidade").value = Value
                        Parent.var("idEntidade").Dirty = True
                    End Set
                End Property

                Public Property Dia() As Date Implements Interfaces.iEVT.Primitivas.iStatusHistorico.Dia
                    Get
                        Return Parent.var("Dia").value
                    End Get
                    Set(ByVal Value As Date)
                        Parent.var("Dia").value = Value
                        Parent.var("Dia").Dirty = True
                    End Set
                End Property

                Public Property CodBarras() As String Implements Interfaces.iEVT.Primitivas.iStatusHistorico.CodBarras
                    Get
                        Return Parent.var("CodBarras").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("CodBarras").value = Value
                        Parent.var("CodBarras").Dirty = True
                    End Set
                End Property

                Public Property Status() As Single Implements Interfaces.iEVT.Primitivas.iStatusHistorico.Status
                    Get
                        Return Parent.var("Status").value
                    End Get
                    Set(ByVal Value As Single)
                        Parent.var("Status").value = Value
                        Parent.var("Status").Dirty = True
                    End Set
                End Property

#End Region
            End Class
        End Class
#End Region

#Region "===[ StatusPresumido ]==="
        '===[ StatusReal ]==================================================================================================
        'Ok - Célio em 17/11/2005
        Public Class StatusPresumido
            Inherits allClass
            Private mCampos As StatusPresumidoCampos 'Interfaces.iEVT.Primitivas.iStatusPresumido

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_StatusPresumido")
                mCampos = New StatusPresumidoCampos(Me)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_StatusPresumido", TipoConexao)
                mCampos = New StatusPresumidoCampos(Me)
            End Sub

            Public Sub GetFieldsFromIdObjDia(ByVal idObj As String, ByVal Dia As Date)
                Me.Clear_filters()
                Me.filterWhere = "idobj = '" + idObj.Trim + "' and Dia='" + Dia.ToString("MM/dd/yyyy") + "'"
                Me.getFields(False)
                If Me.Campos.ID > 0 Then
                    Me.Campos.Dia_Key = Me.Campos.Dia_Key
                    Me.Campos.IdObj_Key = Me.Campos.IdObj_Key
                    Me.Campos.IdOrcamento_Key = Me.Campos.IdOrcamento_Key
                End If
            End Sub

            Public Property Campos() As Interfaces.iEVT.Primitivas.iStatusPresumido
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iStatusPresumido)
                    mCampos = Value
                    If Not Value Is Nothing Then
                        MyBase.toObject = mCampos.Parent
                    End If
                End Set
            End Property

            Private Class StatusPresumidoCampos
                Implements Interfaces.iEVT.Primitivas.iStatusPresumido

                Private aParent As allClass

                Public Sub New(ByRef allClass As allClass)
                    Me.aParent = allClass
                End Sub

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal Value As allClass)
                        Me.aParent = Value
                    End Set
                End Property

                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    Parent.Clear_filters()
                End Sub

                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    Parent.Clear_vars()
                End Sub

                Public Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    If Me.IdObj_Key.Substring(0, 1) = "I" Then
                        Me.IdItem = Me.IdObj_Key.Substring(1)
                    Else
                        Me.IdKit = Me.IdObj_Key.Substring(1)
                    End If
                    Dim filtro As String
                    filtro = " idObj = '" & Me.IdObj_Key & "' and idOrcamento = " & Me.IdOrcamento_Key & " and Dia = '" & Me.Dia_Key.ToString("MM/dd/yyyy") & "'"
                    Me.Parent.SalvarPadrao(noCommit, filtro)
                End Sub

                Public Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    Parent = Nothing
                End Sub

#Region "   Propriedades - Fields   "

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.ID.ToString
                    End Get
                End Property

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("ID").value
                    End Get
                End Property

                Public Property IdItem() As Integer Implements Interfaces.iEVT.Primitivas.iStatusPresumido.IdItem
                    Get
                        Return Parent.var("idItem").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idItem").value = Value
                        Parent.var("idItem").Dirty = True
                    End Set
                End Property

                Public Property IdKit() As Integer Implements Interfaces.iEVT.Primitivas.iStatusPresumido.IdKit
                    Get
                        Return Parent.var("IdKit").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("IdKit").value = Value
                        Parent.var("IdKit").Dirty = True
                    End Set
                End Property

                Public Property IdOrcObj() As Integer Implements Interfaces.iEVT.Primitivas.iStatusPresumido.idOrcObj
                    Get
                        Return Parent.var("IdOrcObj").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("IdOrcObj").value = Value
                        Parent.var("IdOrcObj").Dirty = True
                    End Set
                End Property

                Public Property IdObj_Key() As String Implements Interfaces.iEVT.Primitivas.iStatusPresumido.IdObj_Key
                    Get
                        Return Parent.var("IdObj").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("IdObj").value = Value
                        Parent.var("IdObj").Dirty = True
                    End Set
                End Property

                Public Property IdOrcamento_Key() As Integer Implements Interfaces.iEVT.Primitivas.iStatusPresumido.IdOrcamento_Key
                    Get
                        Return Parent.var("idOrcamento").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idOrcamento").value = Value
                        Parent.var("idOrcamento").Dirty = True
                    End Set
                End Property

                Public Property Dia_Key() As Date Implements Interfaces.iEVT.Primitivas.iStatusPresumido.Dia_Key
                    Get
                        Return Parent.var("Dia").value
                    End Get
                    Set(ByVal Value As Date)
                        Parent.var("Dia").value = Value
                        Parent.var("Dia").Dirty = True
                    End Set
                End Property

                Public Property Quantidade() As Integer Implements Interfaces.iEVT.Primitivas.iStatusPresumido.Quantidade
                    Get
                        Return Parent.var("Quantidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("Quantidade").value = Value
                        Parent.var("Quantidade").Dirty = True
                    End Set
                End Property

                Public Property Status() As Single Implements Interfaces.iEVT.Primitivas.iStatusPresumido.Status
                    Get
                        Return Parent.var("Status").value
                    End Get
                    Set(ByVal Value As Single)
                        Parent.var("Status").value = Value
                        Parent.var("Status").Dirty = True
                    End Set
                End Property

#End Region

            End Class
        End Class
#End Region

#Region "===[ Contas a Pagar e a Receber ]==="
        '===[ Contas a Pagar e a Receber ]==================================================================================================
        'Ok - Célio em 27/09/2005
        Public Class ContasPagarReceber
            Inherits allClass
            Private mCampos As Interfaces.iEVT.Primitivas.iContasPagarReceber

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_ContasPagarReceber")
                mCampos = New ContasPagarReceberCampos(Me)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_ContasPagarReceber", TipoConexao)
                mCampos = New ContasPagarReceberCampos(Me)
            End Sub

            Public Property Campos() As Interfaces.iEVT.Primitivas.iContasPagarReceber
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iContasPagarReceber)
                    mCampos = Value
                End Set
            End Property

            Public Overrides Sub Dispose()
                If Not IsNothing(mCampos) Then mCampos.Dispose()
                mCampos = Nothing
            End Sub

            Private Class ContasPagarReceberCampos
                Implements Interfaces.iEVT.Primitivas.iContasPagarReceber
                Private aParent As allClass

                Public Sub New(ByRef allClass As allClass)
                    Me.aParent = allClass
                End Sub

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal Value As allClass)
                        Me.aParent = Value
                    End Set
                End Property

#Region "   Propriedades - Fields   "

                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("ID").value
                    End Get
                End Property

                Public Property TipoConta() As Integer Implements Interfaces.iEVT.Primitivas.iContasPagarReceber.TipoConta
                    Get
                        Return Parent.var("TipoConta").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("TipoConta").value = Value
                    End Set
                End Property

                Public Property Descricao() As String Implements Interfaces.iEVT.Primitivas.iContasPagarReceber.Descricao
                    Get
                        Return Parent.var("Descricao").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("Descricao").value = Value
                    End Set
                End Property

                Public Property Vencimento() As Date Implements Interfaces.iEVT.Primitivas.iContasPagarReceber.Vencimento
                    Get
                        Return Parent.var("Vencimento").value
                    End Get
                    Set(ByVal Value As Date)
                        Parent.var("Vencimento").Dirty = True
                        Parent.var("Vencimento").value = Value
                    End Set
                End Property

                Public Property ValorTotal() As Double Implements Interfaces.iEVT.Primitivas.iContasPagarReceber.ValorTotal
                    Get
                        Return Parent.var("ValorTotal").value
                    End Get
                    Set(ByVal Value As Double)
                        Parent.var("ValorTotal").value = Value
                    End Set
                End Property

                Public Property Parcelas() As Integer Implements Interfaces.iEVT.Primitivas.iContasPagarReceber.Parcelas
                    Get
                        Return Parent.var("Parcelas").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("Parcelas").value = Value
                    End Set
                End Property

                Public Property AnotacoesAdicionais() As String Implements Interfaces.iEVT.Primitivas.iContasPagarReceber.AnotacoesAdicionais
                    Get
                        Return Parent.var("AnotacoesAdicionais").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("AnotacoesAdicionais").value = Value
                    End Set
                End Property

#End Region

                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    aParent.Clear_filters()
                End Sub

                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    aParent.Clear_vars()
                End Sub

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.ID.ToString()
                    End Get
                End Property

                Public Overloads Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

                Public Overloads Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " ID = " & Me.ID.ToString
                    Me.Parent.SalvarPadrao(noCommit, filtro)
                End Sub

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    Me.aParent = Nothing
                End Sub
            End Class

        End Class
#End Region

#Region "===[ Contas a Pagar e a Receber Parcela]==="
        '===[ Contas a Pagar e a Receber Parcela]==================================================================================================
        'Ok - Célio em 27/09/2005
        Public Class ContasPagarReceber_Parcelas
            Inherits allClass

            Private mCampos As Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_ContasPagarReceber_Parcelas")
                mCampos = New ContasPagarReceber_ParcelasCampos(Me)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_ContasPagarReceber", TipoConexao)
                mCampos = New ContasPagarReceber_ParcelasCampos(Me)
            End Sub

            Public Property Campos() As Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas
                Get
                    Return mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas)
                    mCampos = Value
                End Set
            End Property

            Public Overrides Sub Dispose()
                If Not IsNothing(mCampos) Then mCampos.Dispose()
                mCampos = Nothing
            End Sub

            Private Class ContasPagarReceber_ParcelasCampos
                Implements Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas

                Private aParent As allClass

                Public Sub New(ByRef allClass As allClass)
                    Me.aParent = allClass
                End Sub

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal Value As allClass)
                        Me.aParent = Value
                    End Set
                End Property

#Region "Propriedade - Fields"
                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("ID").value
                    End Get
                End Property

                Public Property idConta() As Integer Implements Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas.idConta
                    Get
                        Return Parent.var("idConta").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idConta").Dirty = True
                        Parent.var("idConta").value = Value
                    End Set
                End Property

                Public Property Data() As Date Implements Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas.Data
                    Get
                        Return Parent.var("Data").value
                    End Get
                    Set(ByVal Value As Date)
                        Parent.var("Data").Dirty = True
                        Parent.var("Data").value = Value
                    End Set
                End Property

                Public Property Valor() As Double Implements Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas.Valor
                    Get
                        Return Parent.var("Valor").value
                    End Get
                    Set(ByVal Value As Double)
                        Parent.var("Valor").Dirty = True
                        Parent.var("Valor").value = Value
                    End Set
                End Property
#End Region

                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    aParent.Clear_filters()
                End Sub

                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    aParent.Clear_vars()
                End Sub

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.idConta.ToString & Me.Data.ToShortDateString
                    End Get
                End Property

                Public Overloads Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

                Public Overloads Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " ID = " & Me.ID.ToString
                    Me.Parent.SalvarPadrao(noCommit, filtro)
                End Sub

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub
            End Class


        End Class
#End Region

#Region "===[ Custom Relatório de Orçamentos ]==="
        'Ok - Célio em 24/03/2006
        Public Class CustomROrc
            Inherits allClass
            Private mCampos As Interfaces.iEVT.Primitivas.iCustomROrc

            Public Sub New(ByRef pSelf As self)
                MyBase.New(pSelf, "EVT_CustomROrc")
                mCampos = New CustomROrc_Campos(Me)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_CustomROrc", TipoConexao)
                mCampos = New CustomROrc_Campos(Me)
            End Sub

            Public Property Campos() As Interfaces.iEVT.Primitivas.iCustomROrc
                Get
                    Return Me.mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iCustomROrc)
                    Me.mCampos = Value
                End Set
            End Property

            Public Overrides Sub Dispose()
                If Not IsNothing(mCampos) Then mCampos.Dispose()
                mCampos = Nothing
            End Sub

            Private Class CustomROrc_Campos
                Implements Interfaces.iEVT.Primitivas.iCustomROrc

                Private aParent As allClass

                Public Sub New(ByRef parent As allClass)
                    Me.aParent = parent
                End Sub

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal Value As allClass)
                        Me.aParent = Value
                    End Set
                End Property

#Region "Propriedades - Fields"
                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("idCustomRO").value
                    End Get
                End Property

                Public Property IdOrcamento() As Integer Implements Interfaces.iEVT.Primitivas.iCustomROrc.IdOrcamento
                    Get
                        Return Parent.var("idOrcamento").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("idOrcamento").Dirty = True
                        Parent.var("idOrcamento").value = Value
                    End Set
                End Property

                Public Property PathImgCab() As String Implements Interfaces.iEVT.Primitivas.iCustomROrc.PathImgCab
                    Get
                        Return Parent.var("PathImgCab").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("PathImgCab").Dirty = True
                        Parent.var("PathImgCab").value = Value
                    End Set
                End Property

                Public Property TxtApresentacao() As String Implements Interfaces.iEVT.Primitivas.iCustomROrc.TxtApresentacao
                    Get
                        Return Parent.var("TxtApresentacao").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("TxtApresentacao").Dirty = True
                        Parent.var("TxtApresentacao").value = Value
                    End Set
                End Property
#End Region

                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    aParent.Clear_filters()
                End Sub

                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    aParent.Clear_vars()
                End Sub

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.IdOrcamento.ToString
                    End Get
                End Property

                Public Overloads Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

                Public Overloads Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " idCustomRO = " & Me.ID.ToString
                    Me.Parent.SalvarPadrao(noCommit, filtro)
                End Sub

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub
            End Class

        End Class
#End Region

#Region "===[ Custom Relatório de Orcamentos - Grupos ] ==="
        'Ok - Célio em 24/03/2006
        Public Class CustomROrc_Grupos
            Inherits allClass
            Private mCampos As Interfaces.iEVT.Primitivas.iCustomROrc_Grupos

            Public Sub New(ByRef pSelf As self)
                Me.New(pSelf, tipos.tiposConection.Default_)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_CustomROrc_Grupos", TipoConexao)
                mCampos = New CustomROrc_Grupos_Campos(Me)
            End Sub

            Public Property Campos() As Interfaces.iEVT.Primitivas.iCustomROrc_Grupos
                Get
                    Return Me.mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iCustomROrc_Grupos)
                    Me.mCampos = Value
                End Set
            End Property

            Public Overrides Sub Dispose()
                If Not IsNothing(mCampos) Then mCampos.Dispose()
                mCampos = Nothing
            End Sub

            Private Class CustomROrc_Grupos_Campos
                Implements Interfaces.iEVT.Primitivas.iCustomROrc_Grupos

                Private aParent As allClass

                Public Sub New(ByVal parent As allClass)
                    Me.aParent = parent
                End Sub

                Friend Property Parent() As allClass Implements Interfaces.Primitivas.iObjetoTabela.Parent
                    Get
                        Return Me.aParent
                    End Get
                    Set(ByVal Value As allClass)
                        Me.aParent = Value
                    End Set
                End Property

#Region "Propriedades - Fields"
                Public ReadOnly Property ID() As Integer Implements Interfaces.Primitivas.iObjetoTabela.ID
                    Get
                        Return Parent.var("IdCustomRO_Grupos").value
                    End Get
                End Property

                Public Property IdCustomRO() As Integer Implements Interfaces.iEVT.Primitivas.iCustomROrc_Grupos.IdCustomRO
                    Get
                        Return Parent.var("IdCustomRO").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("IdCustomRO").Dirty = True
                        Parent.var("IdCustomRO").value = Value
                    End Set
                End Property

                Public Property SubTitulo() As String Implements Interfaces.iEVT.Primitivas.iCustomROrc_Grupos.SubTitulo
                    Get
                        Return Parent.var("SubTitulo").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("SubTitulo").Dirty = True
                        Parent.var("SubTitulo").value = Value
                    End Set
                End Property

                Public Property Titulo() As String Implements Interfaces.iEVT.Primitivas.iCustomROrc_Grupos.Titulo
                    Get
                        Return Parent.var("Titulo").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("Titulo").Dirty = True
                        Parent.var("Titulo").value = Value
                    End Set
                End Property
#End Region

                Public Sub Clear_filters() Implements Interfaces.Primitivas.iObjetoTabela.Clear_filters
                    aParent.Clear_filters()
                End Sub

                Public Sub Clear_vars() Implements Interfaces.Primitivas.iObjetoTabela.Clear_vars
                    aParent.Clear_vars()
                End Sub

                Public ReadOnly Property Key() As String Implements Interfaces.Primitivas.iObjetoTabela.Key
                    Get
                        Return Me.IdCustomRO.ToString & Me.Titulo & Me.SubTitulo
                    End Get
                End Property

                Public Overloads Sub Salvar() Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Me.Salvar(False)
                End Sub

                Public Overloads Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iObjetoTabela.salvar
                    Dim filtro As String
                    filtro = " idCustomRO_Grupos = " & Me.ID.ToString
                    Me.Parent.SalvarPadrao(noCommit, filtro)
                End Sub

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    aParent = Nothing
                End Sub
            End Class
        End Class
#End Region

#Region "===[ Custom Relatório de Orçamentos - Itens dos Grupos ]==="
        'Ok - Célio em 24/03/2006
        Public Class CustomROrc_ItensGrupo
            Inherits allClass
            Private mCampos As Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo

            Public Sub New(ByRef pSelf As self)
                Me.New(pSelf, tipos.tiposConection.Default_)
            End Sub

            Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
                MyBase.New(pSelf, "EVT_CustomROrc_ItensGrupo", TipoConexao)
                mCampos = New CustomROrc_ItensGrupo_Campos(Me)
            End Sub

            Public Property Campos() As Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo
                Get
                    Return Me.mCampos
                End Get
                Set(ByVal Value As Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo)
                    Me.mCampos = Value
                End Set
            End Property

            Public Overrides Sub Dispose()
                If Not IsNothing(mCampos) Then mCampos.Dispose()
                mCampos = Nothing
            End Sub

            Public Class CustomROrc_ItensGrupo_Campos
                Implements Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo
                Private mParent As allClass

                Public Sub New(ByVal parent As allClass)
                    Me.mParent = parent
                End Sub

                Public Property Parent() As allClass
                    Get
                        Return Me.mParent
                    End Get
                    Set(ByVal Value As allClass)
                        Me.mParent = Value
                    End Set
                End Property

#Region "Propriedades - Fields"
                Public Property Descricao() As String Implements Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo.Descricao
                    Get
                        Return Parent.var("Descricao").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("Descricao").Dirty = True
                        Parent.var("Descricao").value = Value
                    End Set
                End Property

                Public Property IdCustomRO_Grupos() As Integer Implements Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo.IdCustomRO_Grupos
                    Get
                        Return Parent.var("IdCustomRO_Grupos").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("IdCustomRO_Grupos").Dirty = True
                        Parent.var("IdCustomRO_Grupos").value = Value
                    End Set
                End Property

                Public Property IdObj() As String Implements Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo.IdObj
                    Get
                        Return Parent.var("IdObj").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("IdObj").Dirty = True
                        Parent.var("IdObj").value = Value
                    End Set
                End Property

                Public Property Marca() As String Implements Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo.Marca
                    Get
                        Return Parent.var("Marca").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("Marca").Dirty = True
                        Parent.var("Marca").value = Value
                    End Set
                End Property

                Public Property Modelo() As String Implements Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo.Modelo
                    Get
                        Return Parent.var("Modelo").value
                    End Get
                    Set(ByVal Value As String)
                        Parent.var("Modelo").Dirty = True
                        Parent.var("Modelo").value = Value
                    End Set
                End Property

                Public Property Quantidade() As Integer Implements Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo.Quantidade
                    Get
                        Return Parent.var("Quantidade").value
                    End Get
                    Set(ByVal Value As Integer)
                        Parent.var("Quantidade").Dirty = True
                        Parent.var("Quantidade").value = Value
                    End Set
                End Property
#End Region

                Public Sub Clear_filters()
                    mParent.Clear_filters()
                End Sub

                Public Sub Clear_vars()
                    mParent.Clear_vars()
                End Sub

                Public ReadOnly Property Key() As String
                    Get
                        Return Me.IdCustomRO_Grupos.ToString & Me.IdObj.ToString
                    End Get
                End Property

                Public Overloads Sub Salvar() Implements Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo.Salvar
                    Me.Salvar(False)
                End Sub

                Public Overloads Sub Salvar(ByVal noCommit As Boolean) Implements Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo.Salvar
                    Dim filtro As String
                    filtro = " idCustomRO_Grupos = " & Me.IdCustomRO_Grupos.ToString & "and idObj=" & Me.IdObj.ToString
                    Me.Parent.SalvarPadrao(noCommit, filtro)
                End Sub

                Public Sub Dispose() Implements System.IDisposable.Dispose
                    mParent = Nothing
                End Sub

            End Class
        End Class
#End Region
    End Namespace

End Namespace

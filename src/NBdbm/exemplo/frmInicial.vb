Imports System.Runtime.Serialization

Public Class frmInicial
  Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

  Public Sub New()
    MyBase.New()

    'This call is required by the Windows Form Designer.
    InitializeComponent()

    'Add any initialization after the InitializeComponent() call

  End Sub

  'Form overrides dispose to clean up the component list.
  Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing Then
      If Not (components Is Nothing) Then
        components.Dispose()
      End If
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  Friend WithEvents bt_sair As System.Windows.Forms.Button
  Friend WithEvents bt_Com As System.Windows.Forms.Button
  Friend WithEvents bt_Criar_DB As System.Windows.Forms.Button
  Friend WithEvents bt_frmConfig As System.Windows.Forms.Button
  Friend WithEvents bt_about As System.Windows.Forms.Button
  Friend WithEvents bt_CTRLEntidades As System.Windows.Forms.Button
  Friend WithEvents bt_tarefa As System.Windows.Forms.Button
  Friend WithEvents bt_Teste As System.Windows.Forms.Button
  Friend WithEvents bt_Credencial As System.Windows.Forms.Button
  Friend WithEvents bt_wizardCodigo As System.Windows.Forms.Button
  Friend WithEvents bt_info As System.Windows.Forms.Button
  Friend WithEvents bt_StressTest As System.Windows.Forms.Button
  Friend WithEvents bt_Parser As System.Windows.Forms.Button
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmInicial))
    Me.bt_wizardCodigo = New System.Windows.Forms.Button
    Me.bt_sair = New System.Windows.Forms.Button
    Me.bt_Com = New System.Windows.Forms.Button
    Me.bt_tarefa = New System.Windows.Forms.Button
    Me.bt_Criar_DB = New System.Windows.Forms.Button
    Me.bt_frmConfig = New System.Windows.Forms.Button
    Me.bt_about = New System.Windows.Forms.Button
    Me.bt_CTRLEntidades = New System.Windows.Forms.Button
    Me.bt_info = New System.Windows.Forms.Button
    Me.bt_Teste = New System.Windows.Forms.Button
    Me.bt_Credencial = New System.Windows.Forms.Button
    Me.bt_StressTest = New System.Windows.Forms.Button
    Me.bt_Parser = New System.Windows.Forms.Button
    Me.SuspendLayout()
    '
    'bt_wizardCodigo
    '
    Me.bt_wizardCodigo.Location = New System.Drawing.Point(8, 8)
    Me.bt_wizardCodigo.Name = "bt_wizardCodigo"
    Me.bt_wizardCodigo.Size = New System.Drawing.Size(112, 24)
    Me.bt_wizardCodigo.TabIndex = 0
    Me.bt_wizardCodigo.Text = "Wizard de Código"
    '
    'bt_sair
    '
    Me.bt_sair.Location = New System.Drawing.Point(288, 240)
    Me.bt_sair.Name = "bt_sair"
    Me.bt_sair.Size = New System.Drawing.Size(96, 24)
    Me.bt_sair.TabIndex = 10
    Me.bt_sair.Text = "Sair"
    '
    'bt_Com
    '
    Me.bt_Com.Location = New System.Drawing.Point(8, 136)
    Me.bt_Com.Name = "bt_Com"
    Me.bt_Com.Size = New System.Drawing.Size(112, 24)
    Me.bt_Com.TabIndex = 2
    Me.bt_Com.Text = "New COM+"
    '
    'bt_tarefa
    '
    Me.bt_tarefa.Location = New System.Drawing.Point(288, 88)
    Me.bt_tarefa.Name = "bt_tarefa"
    Me.bt_tarefa.Size = New System.Drawing.Size(96, 24)
    Me.bt_tarefa.TabIndex = 7
    Me.bt_tarefa.Text = "Testa Tarefa"
    '
    'bt_Criar_DB
    '
    Me.bt_Criar_DB.Location = New System.Drawing.Point(8, 168)
    Me.bt_Criar_DB.Name = "bt_Criar_DB"
    Me.bt_Criar_DB.Size = New System.Drawing.Size(112, 24)
    Me.bt_Criar_DB.TabIndex = 3
    Me.bt_Criar_DB.Text = "Criar DB"
    '
    'bt_frmConfig
    '
    Me.bt_frmConfig.Location = New System.Drawing.Point(288, 8)
    Me.bt_frmConfig.Name = "bt_frmConfig"
    Me.bt_frmConfig.Size = New System.Drawing.Size(96, 24)
    Me.bt_frmConfig.TabIndex = 5
    Me.bt_frmConfig.Text = "&Configurações"
    '
    'bt_about
    '
    Me.bt_about.Location = New System.Drawing.Point(288, 48)
    Me.bt_about.Name = "bt_about"
    Me.bt_about.Size = New System.Drawing.Size(96, 24)
    Me.bt_about.TabIndex = 6
    Me.bt_about.Text = "About"
    '
    'bt_CTRLEntidades
    '
    Me.bt_CTRLEntidades.Location = New System.Drawing.Point(8, 208)
    Me.bt_CTRLEntidades.Name = "bt_CTRLEntidades"
    Me.bt_CTRLEntidades.Size = New System.Drawing.Size(112, 24)
    Me.bt_CTRLEntidades.TabIndex = 4
    Me.bt_CTRLEntidades.Text = "CTRL Entidades"
    '
    'bt_info
    '
    Me.bt_info.Location = New System.Drawing.Point(288, 192)
    Me.bt_info.Name = "bt_info"
    Me.bt_info.Size = New System.Drawing.Size(96, 24)
    Me.bt_info.TabIndex = 9
    Me.bt_info.Text = "Informações"
    '
    'bt_Teste
    '
    Me.bt_Teste.Location = New System.Drawing.Point(8, 40)
    Me.bt_Teste.Name = "bt_Teste"
    Me.bt_Teste.Size = New System.Drawing.Size(112, 32)
    Me.bt_Teste.TabIndex = 1
    Me.bt_Teste.Text = "Teste Rapido"
    '
    'bt_Credencial
    '
    Me.bt_Credencial.Location = New System.Drawing.Point(288, 128)
    Me.bt_Credencial.Name = "bt_Credencial"
    Me.bt_Credencial.Size = New System.Drawing.Size(96, 24)
    Me.bt_Credencial.TabIndex = 8
    Me.bt_Credencial.Text = "Credencial"
    '
    'bt_StressTest
    '
    Me.bt_StressTest.Location = New System.Drawing.Point(288, 160)
    Me.bt_StressTest.Name = "bt_StressTest"
    Me.bt_StressTest.Size = New System.Drawing.Size(96, 24)
    Me.bt_StressTest.TabIndex = 11
    Me.bt_StressTest.Text = "Stress Test"
    '
    'bt_Parser
    '
    Me.bt_Parser.Location = New System.Drawing.Point(8, 80)
    Me.bt_Parser.Name = "bt_Parser"
    Me.bt_Parser.Size = New System.Drawing.Size(112, 24)
    Me.bt_Parser.TabIndex = 12
    Me.bt_Parser.Text = "Parser Import"
    '
    'frmInicial
    '
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.ClientSize = New System.Drawing.Size(392, 269)
    Me.Controls.Add(Me.bt_Parser)
    Me.Controls.Add(Me.bt_StressTest)
    Me.Controls.Add(Me.bt_Credencial)
    Me.Controls.Add(Me.bt_Teste)
    Me.Controls.Add(Me.bt_info)
    Me.Controls.Add(Me.bt_CTRLEntidades)
    Me.Controls.Add(Me.bt_about)
    Me.Controls.Add(Me.bt_frmConfig)
    Me.Controls.Add(Me.bt_Criar_DB)
    Me.Controls.Add(Me.bt_tarefa)
    Me.Controls.Add(Me.bt_Com)
    Me.Controls.Add(Me.bt_sair)
    Me.Controls.Add(Me.bt_wizardCodigo)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "frmInicial"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Projeto Exemplo"
    Me.ResumeLayout(False)

  End Sub

#End Region

  Private Sub bt_sair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_sair.Click
    Me.Dispose()
    End
  End Sub

  Private Sub bt_Com_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Com.Click


  End Sub

  Private Sub bt_Criar_DB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Criar_DB.Click

    Dim o As New NBdbm.Manager.CamadaDados.NBdbm
    Dim Sucesso As NBdbm.tipos.Retorno
    Dim mVersao As New NBdbm.tipos.Versao

    mVersao.major = 1
    mVersao.minor = 1
    mVersao.revision = 1
    MsgBox(o.versao)
    'Sucesso = o.checkCTR(mVersao)


    mVersao = o.versao

  End Sub

  Private Sub bt_frmConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_frmConfig.Click
    Dim o As New NBdbm.forms.frmConfig
    o.ShowDialog()
    o.Dispose()
    o = Nothing
    'Dim obj As System.Runtime.Remoting.ObjectHandle
    'Dim h As Object

    'obj = Activator.CreateInstanceFrom("../../bin/nbdbm.dll", "NBdbm.forms.frmConfig")

    'h = CType(obj.Unwrap(), Object)

    'h.showdialog()
    'h.dispose()
    'h = Nothing
  End Sub

  Private Sub bt_about_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_about.Click
    'Dim o As New NBdbm.forms.frmAbout()
    'Call o.ShowDialog()
    'o.Dispose()
    'o = Nothing

    Dim obj As System.Runtime.Remoting.ObjectHandle
    Dim h As Object

    obj = Activator.CreateInstanceFrom("../../bin/nbdbm.dll", "NBdbm.forms.frmAbout")

    h = CType(obj.Unwrap(), Object)

    h.showdialog()
    h.dispose()
    h = Nothing
  End Sub

  Private Sub bt_CTRLEntidades_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_CTRLEntidades.Click

    Dim frm As frmExemplo_Cadastro

    frm = New frmExemplo_Cadastro

    Call frm.ShowDialog()

    frm.Dispose()
    frm = Nothing

  End Sub

  Private Sub bt_tarefa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_tarefa.Click

    Dim t As New NBdbm.TarefaExemplo

    t.exemplo()

  End Sub

  Private Sub bt_Teste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Teste.Click
    Try
      'Call Me.exemplo_EVT_gravaFuncionario()
      Call Me.exemplo_EVT_gravaOrdemServico()
      'Call Me.exemplo_EVT_gravaPatrimonio()
      'Call Me.exemplo_EVT_gravaKit()
      'Call Me.exemplo_EVT_gravaFuncionario_MetodoAdd()
      'Call Me.exemplo_CTR_gravaFornecedor()
      'Call Me.exemplo_CTR_GetFields()
      'Call Me.exemplo_EVT_GetFieldsLoc()
      'call Me.exemplo_GetFields2()
      'Call Me.exemplo_Exception()
      'call me.exemplo_Credencial
      'Call Me.exemplo_Exclusao()
      'Call Me.exemplo_EVT_GravaLocalidade()
    Catch ex As Exception
      MsgBox(ex.ToString & vbCrLf & vbCrLf & ex.Source)
    End Try
  End Sub

  Private Sub bt_Credencial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Credencial.Click
    Call Me.exemplo_Credencial()
  End Sub

  Private Sub bt_wizardCodigo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_wizardCodigo.Click
    Dim frm As New frmFields
    frm.ShowDialog()
    frm.Dispose()
    frm = Nothing
  End Sub

  Private Sub bt_info_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_info.Click
    Dim value As Object

    value = 123

    value = String.Empty

    value = Nothing

  End Sub

  Private Sub exemplo_EVT_GetFieldsLoc()
    Dim o As New NBdbm.Fachadas.plxEVT.CadastroLocalidades
    Dim txt As String

    o.getFieldsFromEntidade(531)
    txt = o.Localidade.idEntidade



  End Sub

  Private Sub exemplo_CTR_GetFields()
    Dim o As New NBdbm.Fachadas.CTR.primitivas.Entidade
    Dim colFields As Collection
    Dim txt As String
    Dim Dg As New DataGrid

    'o.campos.PessoaFJ = True
    'MsgBox(o.campos.PessoaFJ)

    o.filterWhere = "IdEntidade = 474"
    colFields = o.getFields
    Dg.DataSource = o.DataSource


    txt = o.campos.ID
  End Sub

  Private Sub exemplo_CTR_GetFields2()
    Dim o As New NBdbm.Fachadas.CTR.CadastroUsuario
    'Dim t As New NBdbm.Fachadas.CTR.primitivas.Telefone
    Dim txt As String
    Try
      o.getFieldsFromEntidade(8)

      For Each t As NBdbm.Interfaces.iCTR.Primitivas.iTelefone In o.colecaoTelefones.Values
        txt = t.Fone_key
      Next

    Catch ex As Exception
      Beep()
    End Try

  End Sub

  Private Sub Exemplo_Evt_getUsuario()
    Dim Ob As New NBdbm.Fachadas.plxEVT.CadastroFuncionario
    Dim E As New NBdbm.Fachadas.CTR.primitivas.Entidade
    Dim En As New NBdbm.Fachadas.CTR.primitivas.Endereco
    Dim EM As New NBdbm.Fachadas.CTR.primitivas.eMail
    Dim UU As New NBdbm.Fachadas.CTR.primitivas.Url
    Dim U As New NBdbm.Fachadas.CTR.primitivas.Usuario
    Dim Uc As New NBdbm.Fachadas.CTR.primitivas.UsuarioConfig
    Dim T As New NBdbm.Fachadas.CTR.primitivas.Telefone

    Dim txt As String

    Call Ob.getFieldsFromEntidade(470)

    E.campos = Ob.Entidade
    txt = E.campos.ID
    En.Campos = Ob.Endereco
    txt = En.Campos.idEntidade_key
    T.Campos = Ob.Telefone
    txt = T.Campos.idEntidade_key
    EM.Campos = Ob.eMail
    txt = EM.Campos.idEntidade_key
    UU.Campos = Ob.Url
    txt = UU.Campos.idEntidade_key
    U.Campos = Ob.Usuario
    txt = U.Campos.idEntidade
    txt = U.Campos.ID
    Uc.Campos = Ob.UsuarioConfig
    txt = Uc.Campos.idUsuario_key


  End Sub

  Private Sub exemplo_Credencial()
    Dim o As NBdbm.Fachadas.Connection
    Dim credencial As New NBdbm.tipos.Retorno
    Dim Cnn As New SqlClient.SqlConnection
    credencial.Sucesso = True
    credencial.Tag = "=*MZÏ>ÙMÝ"
    credencial.Objeto = "NBdbm"
    o = New NBdbm.Fachadas.Connection(credencial)

    Cnn = o.Connection

    MsgBox(Cnn.State)

    o.Dispose()
  End Sub

  Private Sub exemplo_UsoAllClass()
    Dim ob As TesteAllClass


  End Sub

  Public Sub exemplo_Exception()

    Dim o As New NBdbm.testaClasseException

    Call o.doIt()


  End Sub

  Public Sub exemplo_CTR_Exclusao()

    Dim ob As New NBdbm.Fachadas.CTR.CadastroEntidade
    Dim T As New NBdbm.Fachadas.CTR.primitivas.Telefone

    Dim retorno As NBdbm.tipos.Retorno

    ob.getFieldsFromEntidade(515)
    Try

      For Each fone As NBdbm.Interfaces.iCTR.primitivas.iTelefone In ob.colecaoTelefones.Values

        T.Campos = fone
        retorno = T.excluir(False)
      Next
    Catch ex As Exception
      Beep()
    End Try

    'retorno = ob.excluir()

    MsgBox(retorno.Tag)
    MsgBox(retorno.ToString)


  End Sub

  Public Sub exemplo_CTR_Exclusao1()

    Dim ob As New NBdbm.Fachadas.CTR.CadastroUsuario
    Dim retorno As NBdbm.tipos.Retorno

    ob.getFieldsFromEntidade(490)

    retorno = ob.excluir(False)

    MsgBox(retorno.Tag)
    MsgBox(retorno.ToString)


  End Sub

  Private Function exemplo_EVT_gravaFuncionario_MetodoAdd()

    Dim resultado As NBdbm.tipos.Retorno
    Dim ob As New NBdbm.Fachadas.plxEVT.CadastroFuncionario
    Dim oEntidade As New NBdbm.Fachadas.CTR.primitivas.Entidade
    Dim oFone As New NBdbm.Fachadas.CTR.primitivas.Telefone
    Dim oCelular As New NBdbm.Fachadas.CTR.primitivas.Telefone
    Dim oEndereco As New NBdbm.Fachadas.CTR.primitivas.Endereco
    Dim oEMail As New NBdbm.Fachadas.CTR.primitivas.eMail
    Dim oUrl As New NBdbm.Fachadas.CTR.primitivas.Url

    Beep()

    With oEntidade.campos
      .Clear_vars()
      Try
        .CPFCNPJ_key = "807.829.149-34"
      Catch ex As Exception
        Beep()
      End Try
      .NomeRazaoSocial_key = "Edgar fulano 001"
      .ApelidoNomeFantasia = "Nosso novo Usuario"
      .dtNascimentoInicioAtividades = CDate("08/12/1973")
      .OrgaoEmissorIM = "SSP-SC"
      .RgIE = "2.503.661"
      .TextoRespeito = "esse é o texto a respeito novo"
      ob.Entidade = oEntidade.campos
    End With
    With oFone.Campos
      .Clear_vars()
      .DDD_key = "47"
      .Fone_key = "252-1624"
      .Ramal = "222"
      .Contato = "Com beltrano"
      .Descricao = "Residencial"
      ob.colecaoTelefones.Add(.Key, oFone.Campos)
    End With
    With oCelular.Campos
      .Clear_vars()
      .DDD_key = "47"
      .Fone_key = "9987-1624"
      .Ramal = "232"
      .Contato = "Cicrano"
      .Descricao = "Celular Pessoal"
      ob.colecaoTelefones.Add(.Key, oCelular.Campos)
    End With
    With oEndereco.Campos
      .Clear_vars()
      .Logradouro_key = "minha rua, 002"
      .Bairro = "meu bairro"
      .Municipio = "minha cidade"
      .UF = "SC"
      .complemento = "casa dos fundos"
      .Comentario = "endereço da vovozinha"
      .CEP = "88040420"
      .Principal = True
      ob.colecaoEnderecos.Add(.Key, oEndereco.Campos)
    End With
    With oEMail.Campos
      .Clear_vars()
      .Descricao = "Email do chefe"
      .eMail_key = "edgar@neobridge.com.br"
      ob.colecaoEmail.Add(.Key, oEMail.Campos)
    End With
    With oUrl.Campos
      .Clear_vars()
      .Descricao = "página principal"
      .Url_key = "http://www.neobridge.com.br"
      ob.colecaoUrl.Add(.Key, oUrl.Campos)
    End With

    '------------------
    'Exemplo de familiaridade das classes!
    '
    'ob.Entidade.NomeRazaoSocial_key = ""
    'ob.Endereco.Logradouro_key = ""
    'ob.Telefone.Fone_key = ""
    'ob.EnderecoEletronico.EnderecoEletronico_key = ""
    'ob.Usuario.login_key = ""
    '---------------------
    With ob.Usuario
      .login_key = "eddy0001"
      .senha = "123456"
      .matricula = "CFDRT1256"
      .dtAdmissao = "12/08/2001"
      .idEmpresa = "1"
      .idUsuarioCadastrador = "7"
    End With

    With ob.UsuarioConfig
      .UsuarioAtivo = True
      .Funcao = "Auxiliar"
      .Credencial = 3
      .pmEditar = True
      .pmIncluir = True
      .pmLer = True
    End With

    resultado = ob.Salvar(False)
    'MsgBox(Me.admTB.Campos.IdadeTempoExistencia())
    'resultado = Me.admTB.inserir

    'Me.DG.DataSource = ob.DataSource

    MsgBox(resultado.Tag & " - id: " & ob.Entidade.ID & " - " & ob.Entidade.NomeRazaoSocial_key)
    MsgBox(resultado.ToString)

  End Function

  Private Function exemplo_EVT_gravaPatrimonio()

    Dim resultado As NBdbm.tipos.Retorno
    Dim S As New NBdbm.Fachadas.plxEVT.primitivas.Situacao
    Dim Q As New NBdbm.Fachadas.plxEVT.primitivas.Qualidade
    Dim EP As New NBdbm.Fachadas.plxEVT.primitivas.EstoquePatrimonial

    With S.campos
      .Clear_vars()
      .situacao_key = "Locado"
    End With

    resultado = S.campos.salvar(False)
    MsgBox(resultado.Tag & " - id: " & S.campos.ID & " - " & S.campos.Key)
    MsgBox(resultado.ToString)

    With Q.campos
      .Clear_vars()
      .qualidade_key = "sei lá qual qualidade é essa?"
    End With

    resultado = Q.campos.salvar(False)
    MsgBox(resultado.Tag & " - id: " & Q.campos.ID & " - " & Q.campos.Key)
    MsgBox(resultado.ToString)

    With EP.Campos
      .Clear_vars()
      .descricao_key = "Martelo Azul"
      .comentario = "aparentemente ótimo!"
      .idLocalidade = "1"
      .idQualidade = "3"
      .idSituacao = "2"
      .maxDesconto = "10" '%
      .numeroSerie = "123457"
      .quantidade = "1"
      .tipoItem = "1"
      .vidaUtil = "1" 'anos
      .vlrLocacao = "5" 'moeda
      .vlrVenal = "55" 'moeda
    End With

    resultado = EP.Campos.salvar(False)
    MsgBox(resultado.Tag & " - id: " & EP.Campos.ID & " - " & EP.Campos.Key)
    MsgBox(resultado.ToString)

  End Function

  Private Function exemplo_EVT_gravaKit()

    Dim resultado As NBdbm.tipos.Retorno
    Dim ob As New NBdbm.Fachadas.plxEVT.CadastroKit
    Dim S As New NBdbm.Fachadas.plxEVT.primitivas.Situacao
    Dim Q As New NBdbm.Fachadas.plxEVT.primitivas.Qualidade
    Dim EP As New NBdbm.Fachadas.plxEVT.primitivas.EstoquePatrimonial

    With S
      .Clear_vars()
      .Clear_filters()
      .filterWhere = " situacao = 'locado' "
      .getFields()
    End With

    With Q
      .Clear_vars()
      .Clear_filters()
      .filterWhere = " qualidade like 'sei lá qual qualidade é essa*' "
      .getFields()
    End With

    With EP
      .Clear_vars()
      .Clear_filters()
      .filterWhere = " descricao = 'Martelo Azul' "
      .getFields()
    End With

    With ob.Kit
      .Nome_key = "Stand 25mtsQ"
      .idClasseEP = 0
      .instrucoes = "montagem 2hs - 2pax"
    End With

    With ob.KitItem
      .Clear_vars()
      .idEstoquePatrimonial_key = EP.Campos.ID
      .quantidade = 5
      ob.colecaoKitItens.Add(.Key, ob.KitItem)
    End With

    resultado = ob.salvar(False)
    MsgBox(resultado.Tag & " - id: " & ob.Kit.ID & " - " & ob.Kit.Key)
    MsgBox(resultado.ToString)

  End Function

  Private Function exemplo_EVT_gravaOrdemServico()

    Dim resultado As NBdbm.tipos.Retorno
    Dim ob As New NBdbm.Fachadas.plxEVT.CadastroOrdemServico

    Beep()
    With ob.OS
      .Clear_vars()
      .idEntidade = 538
      .idLocalidade = 1
      .nome = "tekskj"
      .numero_key = 12454
      .contrato = 54654654
      .orcamento = True
      .dtFinal = "12/08/2005" 'Data da Validade da Proposta ou a Data da Entrega do serviço.
    End With

    ob.xmPath_LinkEntNo = "*<Funcionários>"

    With ob.OSItem
      .Clear_vars()
      .idKit_key = 10
      .idEstoquePatrimonial_key = 0
      .quantidade = 10.5
      .vlrLocacao = 6.02
      .dtBloqueioInicio = "01/07/2005"
      .dtUsoInicio = "02/07/2005"
      .dtUsoFim = "05/07/2005"
      .dtBloqueioFim = "07/07/2005"
      .Entrada = False
      .Saida = False
      ob.colecaoOSItens.Add(.Key, ob.OSItem)
    End With

    With ob.OSItem
      .Clear_vars()
      .idKit_key = 0
      .idEstoquePatrimonial_key = 7
      .quantidade = 5
      .vlrLocacao = 1.02
      .dtBloqueioInicio = "01/07/2005"
      .dtUsoInicio = "02/07/2005"
      .dtUsoFim = "05/07/2005"
      .dtBloqueioFim = "07/07/2005"
      .Saida = False
      .Entrada = False
      ob.colecaoOSItens.Add(.Key, ob.OSItem)
    End With

    With ob.OSAvul
      .Clear_vars()
      .Descricao_key = "Instalação especial"
      .dtBloqueioInicio = "01/07/2005"
      .dtUsoInicio = "01/07/2005"
      .dtUsoFim = "06/07/2005"
      .dtBloqueioFim = "06/07/2005"
      .quantidade = 1
      .vlrLocacao = 150.55
      .Entrada = False
      .Saida = False
      ob.colecaoOSAvul.Add(.Key, ob.OSAvul)
    End With

    resultado = ob.salvar(False)

    MsgBox(resultado.Tag & " - id: " & ob.OS.ID & " - " & ob.OS.Key)
    MsgBox(resultado.ToString)

  End Function

  Private Function exemplo_EVT_gravaFuncionario()

    Dim resultado As NBdbm.tipos.Retorno
    Dim ob As New NBdbm.Fachadas.plxEVT.CadastroFuncionario
    'Dim oEntidade As New NBdbm.Fachadas.CTR.primitivas.Entidade
    'Dim oFone As New NBdbm.Fachadas.CTR.primitivas.Telefone
    'Dim oCelular As New NBdbm.Fachadas.CTR.primitivas.Telefone
    'Dim oEndereco As New NBdbm.Fachadas.CTR.primitivas.Endereco
    'Dim oEnderecoEletronico As New NBdbm.Fachadas.CTR.primitivas.EnderecoEletronico

    Beep()
    With ob.Entidade
      .Clear_vars()
      Try
        .CPFCNPJ_key = "807.829.149-34"
      Catch ex As Exception
        Beep()
      End Try
      .NomeRazaoSocial_key = "Edgar Francis Novissimo 15"
      .dtNascimentoInicioAtividades = CDate("08/09/75")
      .OrgaoEmissorIM = "SSP/PR"
      .RgIE = "5.225.520-1"
      .TextoRespeito = "Programador em C#"
    End With

    ob.xmPath_LinkEntNo = "*<Funcionários>"

    With ob.Telefone
      .Clear_vars()
      .DDD_key = "51"
      .Fone_key = "3252-7014"
      .Ramal = "0"
      .Descricao = "Res"
      ob.colecaoTelefones.Add(.Key, ob.Telefone)
    End With
    With ob.Telefone
      .Clear_vars()
      .DDD_key = "47"
      .Fone_key = "9987-1624"
      .Ramal = "0"
      .Descricao = "Cel"
      ob.colecaoTelefones.Add(.Key, ob.Telefone)
    End With
    With ob.Endereco
      .Clear_vars()
      .Logradouro_key = "Rod. Antonio Heil, 1300"
      .Bairro = "Nova Brasilia"
      .Municipio = "Brusque"
      .UF = "SC"
      .complemento = "Apto 101"
      .CEP = "88352-501"
      .Principal = True
      ob.colecaoEnderecos.Add(.Key, ob.Endereco)
    End With
    With ob.eMail
      .Clear_vars()
      .Descricao = "email"
      .eMail_key = "celioassis@brturbo.com"
      ob.colecaoEmail.Add(.Key, ob.eMail)
    End With
    With ob.Url
      .Clear_vars()
      .Descricao = "site"
      .Url_key = "http://www.neobridge.com.br"
      ob.colecaoUrl.Add(.Key, ob.Url)
    End With
    '------------------
    'Exemplo de familiaridade das classes!
    '
    'ob.Entidade.NomeRazaoSocial_key = ""
    'ob.Endereco.Logradouro_key = ""
    'ob.Telefone.Fone_key = ""
    'ob.EnderecoEletronico.EnderecoEletronico_key = ""
    'ob.Usuario.login_key = ""
    '---------------------
    With ob.Usuario
      .login_key = "celioassis"
      .senha = "123456"
    End With

    With ob.UsuarioConfig
      .UsuarioAtivo = True
      .Funcao = "Programador"
      .Credencial = 3
    End With

    resultado = ob.Salvar(False)
    'MsgBox(Me.admTB.Campos.IdadeTempoExistencia())
    'resultado = Me.admTB.inserir

    'Me.DG.DataSource = ob.DataSource

    MsgBox(resultado.Tag & " - id: " & ob.Entidade.ID & " - " & ob.Entidade.NomeRazaoSocial_key)
    MsgBox(resultado.ToString)

  End Function

  Private Function exemplo_CTR_gravaFornecedor()

    Dim resultado As NBdbm.tipos.Retorno
    Dim ob As New NBdbm.Fachadas.CTR.CadastroEntidade
    'Dim oEntidade As New NBdbm.Fachadas.CTR.primitivas.Entidade
    'Dim oFone As New NBdbm.Fachadas.CTR.primitivas.Telefone
    'Dim oCelular As New NBdbm.Fachadas.CTR.primitivas.Telefone
    'Dim oEndereco As New NBdbm.Fachadas.CTR.primitivas.Endereco
    'Dim oEnderecoEletronico As New NBdbm.Fachadas.CTR.primitivas.EnderecoEletronico

    Beep()
    With ob.Entidade
      .Clear_vars()
      .PessoaFisica = True
      Try
        .CPFCNPJ_key = "04721618000100"
      Catch ex As Exception
        Beep()
      End Try
      .NomeRazaoSocial_key = "Neobridge Sistemas Ltda"
      .dtNascimentoInicioAtividades = CDate("01/06/2001")
      .OrgaoEmissorIM = "JUCESC"
      .RgIE = "415970-5"
      .TextoRespeito = "Sistemas e Soluções"
    End With

    ob.xmPath_LinkEntNo = "*<Fornecedores>"

    With ob.Telefone
      .Clear_vars()
      .DDD_key = "48"
      .Fone_key = "234 7620"
      .Ramal = "0"
      .Descricao = "COM"
      ob.colecaoTelefones.Add(.Key, ob.Telefone)
    End With
    With ob.Telefone
      .Clear_vars()
      .DDD_key = "48"
      .Fone_key = "233 4327"
      .Ramal = "0"
      .Descricao = "FAX"
      ob.colecaoTelefones.Add(.Key, ob.Telefone)
    End With
    With ob.Telefone
      .Clear_vars()
      .DDD_key = "48"
      .Fone_key = "9973 3063"
      .Ramal = "0"
      .Descricao = "CEL"
      ob.colecaoTelefones.Add(.Key, ob.Telefone)
    End With
    With ob.Endereco
      .Clear_vars()
      .Logradouro_key = "Rua José João Martendal, 151"
      .Bairro = "Trindade SUL"
      .Municipio = "Florianópolis"
      .UF = "SC"
      .complemento = "Apto 303"
      .CEP = "88040-420"
      .Principal = True
      ob.colecaoEnderecos.Add(.Key, ob.Endereco)
    End With
    With ob.eMail
      .Clear_vars()
      .Descricao = "email"
      .eMail_key = "neo@neobridge.com"
      ob.colecaoEmail.Add(.Key, ob.eMail)
    End With
    With ob.Url
      .Clear_vars()
      .Descricao = "site"
      .Url_key = "http://www.neobridge.com.br"
      ob.colecaoUrl.Add(.Key, ob.Url)
    End With

    resultado = ob.Salvar(False)

    MsgBox(resultado.Tag & " - id: " & ob.Entidade.ID & " - " & ob.Entidade.NomeRazaoSocial_key)
    MsgBox(resultado.ToString)

  End Function

  Public Sub exemplo_EVT_GravaLocalidade()

    Dim ob As New NBdbm.Fachadas.plxEVT.CadastroLocalidades
    Dim resultado As NBdbm.tipos.Retorno

    Beep()
    With ob.Entidade
      .Clear_vars()
      .CPFCNPJ_key = "0" '"000.000.000-00"
      .NomeRazaoSocial_key = "Terra do Nunca"
      .TextoRespeito = "Lá onde tudo se esconde."
    End With
    With ob.Telefone
      .Clear_vars()
      .DDD_key = "48"
      .Fone_key = "2025-0010"
      .Descricao = "COM"
      ob.colecaoTelefones.Add(.Key, ob.Telefone)
    End With
    With ob.Telefone
      .Clear_vars()
      .DDD_key = "48"
      .Fone_key = "2025-0025"
      .Descricao = "FAX"
      ob.colecaoTelefones.Add(.Key, ob.Telefone)
    End With
    With ob.Endereco
      .Clear_vars()
      .Logradouro_key = "Av. que nunca acaba s/n"
      .Bairro = "Por ali"
      .Municipio = "Sei la onde"
      .UF = "SC"
      .CEP = "88000-002"
      .Principal = True
      ob.colecaoEnderecos.Add(.Key, ob.Endereco)
    End With
    With ob.eMail
      .Clear_vars()
      .Descricao = "email"
      .eMail_key = "porck@pop.com"
      ob.colecaoEmail.Add(.Key, ob.eMail)
    End With
    With ob.Url
      .Clear_vars()
      .Descricao = "site"
      .Url_key = "http://www.fink.com.br"
      ob.colecaoUrl.Add(.Key, ob.Url)
    End With

    ob.Localidade.comoChegar = "Saindo por ali."
    ob.Localidade.Nome_key = "Chegado por aqui.."

    resultado = ob.Salvar(False)

    MsgBox(resultado.Tag)
    MsgBox(resultado.ToString)

  End Sub

  Private Sub bt_StressTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_StressTest.Click
    Dim o As New frmStressTest

    o.ShowDialog()

    o.Dispose()
    o = Nothing

  End Sub

  Private Sub bt_Parser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bt_Parser.Click
    Dim ob As New NBdbm.Parsers.Finan32.parser_Finan32
    Dim DBQ As String
    Dim systemDB As String
    Dim xmPath As String
    Dim frmOpen As New System.Windows.Forms.OpenFileDialog


    frmOpen.AddExtension = True
    frmOpen.ValidateNames = True
    frmOpen.DefaultExt = "*.mdb"
    frmOpen.Filter = "Banco de Dados Access (*.mdb)|*.mdb|Outros Bancos (*.*)|*.*"
    frmOpen.Title = "Localize o arquivo do Banco de Dados"
    frmOpen.ShowDialog()

    If frmOpen.FileName <> "" Then
      DBQ = frmOpen.FileName
    Else
      MsgBox("Operação cancelada!")
      Exit Sub
    End If

    frmOpen.DefaultExt = "*.mdb"
    frmOpen.Filter = "System de Dados Access (*.mdw)|*.mdw|Outros Systems (*.*)|*.*"
    frmOpen.Title = "Localize o arquivo de system do Access"
    frmOpen.ShowDialog()

    If frmOpen.FileName <> "" Then
      systemDB = frmOpen.FileName
    Else
      MsgBox("Operação cancelada!")
      Exit Sub
    End If

    xmPath = InputBox("Insira as tags do caminho do nó, ex: <Todos>", "Caminho xmPath da árvore de entidades", "<entidades><clientes>")

    Dim time As DateTime
    time = Now
    Dim resultado As NBdbm.tipos.Retorno
    resultado = ob.Action(DBQ, systemDB, xmPath)

    MsgBox(time & vbCrLf & vbCrLf & resultado.ToString)

  End Sub
End Class

Public Class TesteAllClass
  Inherits NBdbm.Fachadas.allClass

  Public Sub New()
    MyBase.New("CX_TESTE")
  End Sub


End Class
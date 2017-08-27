'Informações aos outros programadores:
'Esta classe usa: TabSize = 2
'                 TabIdent = 2
'Lembre-se:
'   interface que começa com iCadastro - trata-se de uma interface de Base
'   interface que começa com iNMNMNMNM - trata-se de uma interface de especificidade
'
'   Toda classe tem uma propriedade ID que ao ser implementada faz referência
'   ao IdTabela autonumerável, para que se tenha certeza que:
'   classe.ID -> sempre será o ID autonumerável da tabela. Esta informação já está
'   na classe mais primitiva de todas: iCadastro

Namespace Interfaces

  Namespace iCTR

    Public Interface iCadastroEntidade
      'Inherits Interfaces.Primitivas.allClass - > Não herda, porque ela só aglutina!
      Inherits System.IDisposable
      Sub Salvar(ByVal NoCommit As Boolean)
      Sub Salvar()
      Sub Excluir(ByVal NoCommit As Boolean)
      Sub Excluir()
      Property Entidade() As Interfaces.iCTR.Primitivas.iEntidade
      Property xmPath_LinkEntNo() As String
      Property Endereco() As Interfaces.iCTR.Primitivas.iEndereco
      Property Telefone() As Interfaces.iCTR.Primitivas.iTelefone
      Property eMail() As Interfaces.iCTR.Primitivas.iEmail
      Property Url() As Interfaces.iCTR.Primitivas.iUrl
    End Interface

    Public Interface iCadastroUsuario
      'Inherits Interfaces.Primitivas.allClass - > Não herda porque ela só aglutina!
      Inherits System.IDisposable
      Sub Salvar(ByVal NoCommit As Boolean)
      Sub Salvar()
      Sub Excluir(ByVal NoCommit As Boolean)
      Sub Excluir()
      Property Entidade() As Interfaces.iCTR.Primitivas.iEntidade
      Property Endereco() As Interfaces.iCTR.Primitivas.iEndereco
      Property Telefone() As Interfaces.iCTR.Primitivas.iTelefone
      Property Celular() As Interfaces.iCTR.Primitivas.iTelefone
      Property Email() As Interfaces.iCTR.Primitivas.iEmail
      Property Usuario() As Interfaces.iCTR.Primitivas.iUsuario
      Property UsuarioConfig() As Interfaces.iCTR.Primitivas.iUsuarioConfig
    End Interface

    Namespace Primitivas

      Public Interface iEntidade
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property PessoaFisica() As Boolean
        Property TextoRespeito() As String
        Property NomeRazaoSocial_key() As String
        Property CPFCNPJ_key() As String
        Property ApelidoNomeFantasia() As String
        Property RgIE() As String
        Property OrgaoEmissorIM() As String
        ReadOnly Property IdadeTempoExistencia() As Integer
        Property dtNascimentoInicioAtividades() As DateTime
        ReadOnly Property dtCriacao() As DateTime
        ReadOnly Property dtAlteracao() As DateTime
      End Interface

      Public Interface iEndereco
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idEntidade_key() As Integer
        Property Logradouro_key() As String
        Property complemento() As String
        Property Bairro() As String
        Property CEP() As String
        Property Municipio() As String
        Property UF() As String 'Unidade Federativa do Brasil
        Property Comentario() As String
        Property Contato() As String
        Property Principal() As Boolean 'Código de Endereçamento Postal
      End Interface

      Public Interface iTelefone
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idEntidade_key() As Integer
        Property Descricao() As String
        Property DDD_key() As String
        Property Fone_key() As String
        Property Ramal() As String
        Property Contato() As String
        Property idEndereco() As Integer 'Permitido Nulo
      End Interface

      Public Interface iEmail
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idEntidade_key() As Integer
        Property eMail_key() As String
        Property Descricao() As String
      End Interface

      Public Interface iUrl
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idEntidade_key() As Integer
        Property Url_key() As String
        Property Descricao() As String
      End Interface

      Public Interface iUsuario
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idEntidade() As Integer
        Property login_key() As String    '-> tabela de Usuarios
        Property senha() As String
        Property dtAdmissao() As Date     '-> tabela de Usuarios
        Property dtDesligamento() As Date '-> tabela de Usuarios
        Property matricula() As String
        Property idEmpresa() As String
        Property idUsuarioCadastrador() As Integer
      End Interface

      Public Interface iUsuarioConfig
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idUsuario_key() As Integer '(0 maior ; n menor) => poderes
        'Property admin() As Boolean           '-> 0=não Adminitrador   1=Administrador
        'Property agenda() As Boolean          '-> 0=não usa    1=sim -> usa!
        'Property webpad() As Boolean          '-> 0=não usa    1=sim -> usa!
        Property Funcao() As String
        Property Credencial() As Byte
        Property UsuarioAtivo() As Boolean

        Property pmSisExecutavel() As Boolean '-> 0=notpermit  1=permite
        Property pmSisWeb() As Boolean        '-> 0=notpermit  1=permite
        Property pmLer() As Boolean           '-> 1=permite leitura
        Property pmIncluir() As Boolean       '-> 1=permite incluir
        Property pmEditar() As Boolean        '-> 1=permite editar
        Property pmExcluir() As Boolean       '-> 1=permite excluir
        Property xmlConfig() As Text.StringBuilder '->campo com a xml de todas as configurações acima.

        Sub AtivaDesativa(ByVal ativa As Boolean)

      End Interface

      Public Interface iNo
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idNo_key() As Integer
        Property xmPath_key() As String
        Property nome() As String
        Property indice() As String
        Property filhos() As String
      End Interface

      Public Interface iHistoricoLogin
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property nomeLogin() As String
        Property nomeMaquina() As String
        Property dtHoraLogon() As Date
        Property dtHoraLogoff() As Date
      End Interface

      Public Interface iHistoricoTabela
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idAutoNum_key() As Integer 'id Autonumerável da tabela
        Property tbLog_key() As String  'Nome da tabela
        Property dtHistorico() As Date 'DataHistorico
        Property idUsuario() As Integer
        Property idEntidade() As Integer 'Opcional Só é preenchido se for histórico da tbEntidade
        ReadOnly Property xmlLog() As String 'xml contendo todos os campos e respectivos valores
        WriteOnly Property colFields() As Collection
      End Interface

      Public Interface iLinkEntidadeUsuario
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idEntidade() As Integer
        Property idUsuario() As Integer
      End Interface

      Public Interface iLinkEntidadePlx
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idEntidade_key() As Integer
        Property idAutoNumPlx_Key() As Integer
        Property Plx_key() As String
      End Interface

      Public Interface iLinkEntidadeEntidade
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idEntidadeBase_key() As Integer
        Property idEntidadeLink_Key() As Integer
        Property grauRelacionamento() As String
      End Interface

      Public Interface iLinkEntidadeNo
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idNo() As Integer
        Property idEntidade() As Integer
      End Interface

      Public Interface iLinkUsuarioNo
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idNo() As Integer
        Property idUsuario() As Integer
      End Interface

      Public Interface iSpool
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property tabela() As String
        Property idAutoNum() As Integer
        Property quantidade() As Integer
        Property impressa() As Boolean
        Property xmlInfo() As String
        Property xmlCSS() As String
      End Interface

    End Namespace

  End Namespace
End Namespace
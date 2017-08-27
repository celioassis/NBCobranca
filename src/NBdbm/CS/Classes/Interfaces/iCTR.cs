using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;

//Informações aos outros programadores:
//Esta classe usa: TabSize = 2
//                 TabIdent = 2
//Lembre-se:
//   interface que começa com iCadastro - trata-se de uma interface de Base
//   interface que começa com iNMNMNMNM - trata-se de uma interface de especificidade
//
//   Toda classe tem uma propriedade ID que ao ser implementada faz referência
//   ao IdTabela autonumerável, para que se tenha certeza que:
//   classe.ID -> sempre será o ID autonumerável da tabela. Esta informação já está
//   na classe mais primitiva de todas: iCadastro

namespace NBdbm.Interfaces.iCTR
{
    public interface iCadastroEntidade : System.IDisposable
    {
        //Inherits Interfaces.Primitivas.allClass - > Não herda, porque ela só aglutina!
        void Salvar(bool NoCommit);
        void Salvar();
        void Excluir(bool NoCommit);
        void Excluir();
        Interfaces.iCTR.Primitivas.iEntidade Entidade
        {
            get;
            set;
        }
        string xmPath_LinkEntNo
        {
            get;
            set;
        }
        Interfaces.iCTR.Primitivas.iEndereco Endereco
        {
            get;
            set;
        }
        Interfaces.iCTR.Primitivas.iTelefone Telefone
        {
            get;
            set;
        }
        Interfaces.iCTR.Primitivas.iEmail eMail
        {
            get;
            set;
        }
        Interfaces.iCTR.Primitivas.iUrl Url
        {
            get;
            set;
        }
    }

    public interface iCadastroUsuario : System.IDisposable
    {
        //Inherits Interfaces.Primitivas.allClass - > Não herda porque ela só aglutina!
        void Salvar(bool NoCommit);
        void Salvar();
        void Excluir(bool NoCommit);
        void Excluir();
        Interfaces.iCTR.Primitivas.iEntidade Entidade
        {
            get;
            set;
        }
        Interfaces.iCTR.Primitivas.iEndereco Endereco
        {
            get;
            set;
        }
        Interfaces.iCTR.Primitivas.iTelefone Telefone
        {
            get;
            set;
        }
        Interfaces.iCTR.Primitivas.iTelefone Celular
        {
            get;
            set;
        }
        Interfaces.iCTR.Primitivas.iEmail Email
        {
            get;
            set;
        }
        Interfaces.iCTR.Primitivas.iUsuario Usuario
        {
            get;
            set;
        }
        Interfaces.iCTR.Primitivas.iUsuarioConfig UsuarioConfig
        {
            get;
            set;
        }
    }

    namespace Primitivas
    {

        public interface iEntidade : Interfaces.Primitivas.iObjetoTabela
        {
            bool PessoaFisica
            {
                get;
                set;
            }
            string TextoRespeito
            {
                get;
                set;
            }
            string NomeRazaoSocial_key
            {
                get;
                set;
            }
            string CPFCNPJ_key
            {
                get;
                set;
            }
            string ApelidoNomeFantasia
            {
                get;
                set;
            }
            string RgIE
            {
                get;
                set;
            }
            string OrgaoEmissorIM
            {
                get;
                set;
            }
            int IdadeTempoExistencia
            {
                get;
            }
            DateTime dtNascimentoInicioAtividades
            {
                get;
                set;
            }
            DateTime dtCriacao
            {
                get;
            }
            DateTime dtAlteracao
            {
                get;
            }
        }

        public interface iEndereco : Interfaces.Primitivas.iObjetoTabela
        {
            int idEntidade_key
            {
                get;
                set;
            }
            string Logradouro_key
            {
                get;
                set;
            }
            string complemento
            {
                get;
                set;
            }
            string Bairro
            {
                get;
                set;
            }
            string CEP
            {
                get;
                set;
            }
            string Municipio
            {
                get;
                set;
            }
            string UF
            { //Unidade Federativa do Brasil
                get;
                set;
            }
            string Comentario
            {
                get;
                set;
            }
            string Contato
            {
                get;
                set;
            }
            bool Principal
            { //Código de Endereçamento Postal
                get;
                set;
            }
        }

        public interface iTelefone : Interfaces.Primitivas.iObjetoTabela
        {
            int idEntidade_key
            {
                get;
                set;
            }
            string Descricao
            {
                get;
                set;
            }
            string DDD_key
            {
                get;
                set;
            }
            string Fone_key
            {
                get;
                set;
            }
            string Ramal
            {
                get;
                set;
            }
            string Contato
            {
                get;
                set;
            }
            int idEndereco
            { //Permitido Nulo
                get;
                set;
            }
        }

        public interface iEmail : Interfaces.Primitivas.iObjetoTabela
        {
            int idEntidade_key
            {
                get;
                set;
            }
            string eMail_key
            {
                get;
                set;
            }
            string Descricao
            {
                get;
                set;
            }
        }

        public interface iUrl : Interfaces.Primitivas.iObjetoTabela
        {
            int idEntidade_key
            {
                get;
                set;
            }
            string Url_key
            {
                get;
                set;
            }
            string Descricao
            {
                get;
                set;
            }
        }

        public interface iUsuario : Interfaces.Primitivas.iObjetoTabela
        {
            int idEntidade
            {
                get;
                set;
            }
            string login_key
            { //-> tabela de Usuarios
                get;
                set;
            }
            string senha
            {
                get;
                set;
            }
            DateTime dtAdmissao
            { //-> tabela de Usuarios
                get;
                set;
            }
            DateTime dtDesligamento
            { //-> tabela de Usuarios
                get;
                set;
            }
            string matricula
            {
                get;
                set;
            }
            string idEmpresa
            {
                get;
                set;
            }
            int idUsuarioCadastrador
            {
                get;
                set;
            }
        }

        public interface iUsuarioConfig : Interfaces.Primitivas.iObjetoTabela
        {
            int idUsuario_key
            { //(0 maior ; n menor) => poderes
                get;
                set;
            }
            //Property admin() As Boolean           '-> 0=não Adminitrador   1=Administrador
            //Property agenda() As Boolean          '-> 0=não usa    1=sim -> usa!
            //Property webpad() As Boolean          '-> 0=não usa    1=sim -> usa!
            string Funcao
            {
                get;
                set;
            }
            byte Credencial
            {
                get;
                set;
            }
            bool UsuarioAtivo
            {
                get;
                set;
            }

            bool pmSisExecutavel
            { //-> 0=notpermit  1=permite
                get;
                set;
            }
            bool pmSisWeb
            { //-> 0=notpermit  1=permite
                get;
                set;
            }
            bool pmLer
            { //-> 1=permite leitura
                get;
                set;
            }
            bool pmIncluir
            { //-> 1=permite incluir
                get;
                set;
            }
            bool pmEditar
            { //-> 1=permite editar
                get;
                set;
            }
            bool pmExcluir
            { //-> 1=permite excluir
                get;
                set;
            }
            System.Text.StringBuilder xmlConfig
            { //->campo com a xml de todas as configurações acima.
                get;
                set;
            }

            void AtivaDesativa(bool ativa);

        }

        public interface iNo : Interfaces.Primitivas.iObjetoTabela
        {
            int idNo_key
            {
                get;
                set;
            }
            string xmPath_key
            {
                get;
                set;
            }
            string nome
            {
                get;
                set;
            }
            string indice
            {
                get;
                set;
            }
            string filhos
            {
                get;
                set;
            }
        }

        public interface iHistoricoLogin : Interfaces.Primitivas.iObjetoTabela
        {
            string nomeLogin
            {
                get;
                set;
            }
            string nomeMaquina
            {
                get;
                set;
            }
            DateTime dtHoraLogon
            {
                get;
                set;
            }
            DateTime dtHoraLogoff
            {
                get;
                set;
            }
        }

        public interface iHistoricoTabela : Interfaces.Primitivas.iObjetoTabela
        {
            int idAutoNum_key
            { //id Autonumerável da tabela
                get;
                set;
            }
            string tbLog_key
            { //Nome da tabela
                get;
                set;
            }
            DateTime dtHistorico
            { //DataHistorico
                get;
                set;
            }
            int idUsuario
            {
                get;
                set;
            }
            int idEntidade
            { //Opcional Só é preenchido se for histórico da tbEntidade
                get;
                set;
            }
            string xmlLog
            { //xml contendo todos os campos e respectivos valores
                get;
            }
            Collection colFields
            {
                set;
            }
        }

        public interface iLinkEntidadeUsuario : Interfaces.Primitivas.iObjetoTabela
        {
            int idEntidade
            {
                get;
                set;
            }
            int idUsuario
            {
                get;
                set;
            }
        }

        public interface iLinkEntidadePlx : Interfaces.Primitivas.iObjetoTabela
        {
            int idEntidade_key
            {
                get;
                set;
            }
            int idAutoNumPlx_Key
            {
                get;
                set;
            }
            string Plx_key
            {
                get;
                set;
            }
        }

        public interface iLinkEntidadeEntidade : Interfaces.Primitivas.iObjetoTabela
        {
            int idEntidadeBase_key
            {
                get;
                set;
            }
            int idEntidadeLink_Key
            {
                get;
                set;
            }
            string grauRelacionamento
            {
                get;
                set;
            }
        }

        public interface iLinkEntidadeNo : Interfaces.Primitivas.iObjetoTabela
        {
            int idNo
            {
                get;
                set;
            }
            int idEntidade
            {
                get;
                set;
            }
        }

        public interface iLinkUsuarioNo : Interfaces.Primitivas.iObjetoTabela
        {
            int idNo
            {
                get;
                set;
            }
            int idUsuario
            {
                get;
                set;
            }
        }

        public interface iSpool : Interfaces.Primitivas.iObjetoTabela
        {
            string tabela
            {
                get;
                set;
            }
            int idAutoNum
            {
                get;
                set;
            }
            int quantidade
            {
                get;
                set;
            }
            bool impressa
            {
                get;
                set;
            }
            string xmlInfo
            {
                get;
                set;
            }
            string xmlCSS
            {
                get;
                set;
            }
        }

    }
}

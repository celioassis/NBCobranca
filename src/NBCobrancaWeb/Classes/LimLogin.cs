using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using NBCobranca.Tipos;

namespace NBCobranca.Classes
{
    /// <summary>
    /// Summary description for LimLogin.
    /// </summary>
    public class LimLogin : IDisposable
    {
        private string aEmpresaAtual = "";
        private string aUltimoUserValido = "";
        private int aNumTentativas = 1;
        private NBdbm.tipos.tiposConection aTipoConexao;
        private NBdbm.Fachadas.CTR.CadastroUsuario aCadUsuario;

        private string EmpresaAtual
        {
            set
            {
                aEmpresaAtual = value;
                switch (aEmpresaAtual.ToUpper())
                {
                    case "LUGPHIL":
                        aTipoConexao = NBdbm.tipos.tiposConection.SQLSVR_LUG;
                        break;
                    default:
                        aTipoConexao = NBdbm.tipos.tiposConection.SQLSERVER;
                        break;
                }
            }
        }

        public LimLogin()
        {
            aTipoConexao = NBdbm.tipos.tiposConection.SQLSERVER;
            aCadUsuario = new NBdbm.Fachadas.CTR.CadastroUsuario(TipoConexao);
        }

        public TipoCredencial Credencial
        {
            get
            {
                return (TipoCredencial)int.Parse(aCadUsuario.UsuarioConfig.Credencial.ToString());
            }
        }

        public string UsuarioLogado
        {
            get
            {
                return this.aCadUsuario.Usuario.login_key;
            }
        }
        public string NomeCompletoUsuario
        {
            get
            {
                return aCadUsuario.Entidade.NomeRazaoSocial_key;
            }
        }
        public NBdbm.tipos.tiposConection TipoConexao
        {
            get
            {
                return aTipoConexao;
            }
        }
        public void ValidarLogin(string User, string Password)
        {
            if (aCadUsuario.Usuario.login_key != User)
                aCadUsuario.getFieldsFromUsuario(User);

            if (aCadUsuario.Usuario.login_key == "")
                throw new NBdbm.COBR_Exception("Usuário Inválido", "Validação de Login");

            if (aCadUsuario.Usuario.senha == Password)
            {
                if (!aCadUsuario.UsuarioConfig.UsuarioAtivo)
                    throw new NBdbm.COBR_Exception("Usuário desativado!!!\\rEntre em contato com o administrador do Sistema para reativa-lo.", "Validação de Login");
            }
            else
            {
                if (this.aNumTentativas == 3)
                {

                    this.aNumTentativas = 0;
                    aCadUsuario.UsuarioConfig.UsuarioAtivo = false;
                    aCadUsuario.UsuarioConfig.salvar();
                    throw new NBdbm.COBR_Exception("O Usuário foi desativado!!!\\rEntre em contato com o administrador do Sistema para reativa-lo.", "Validação de Login");
                }
                if (User == this.aUltimoUserValido)
                    this.aNumTentativas += 1;
                else
                    this.aNumTentativas = 1;
                this.aUltimoUserValido = User;
                throw new NBdbm.COBR_Exception("Senha Inválida!!!", "Validação de Login");
            }
        }

        public void AlterarSenha(string SenhaAtual, string NovaSenha)
        {
            try
            {
                if (aCadUsuario.Usuario.senha != SenhaAtual)
                    throw new NBdbm.COBR_Exception("Senha Atual inválida", "LimLogin.AlterarSenha");

                if (string.IsNullOrEmpty(NovaSenha) || string.IsNullOrWhiteSpace(NovaSenha))
                    throw new NBdbm.COBR_Exception("A nova senha não pode ser em branco", "LimLogin.AlterarSenha");

                aCadUsuario.Usuario.senha = NovaSenha;
                aCadUsuario.Usuario.salvar();
            }
            catch (NBdbm.COBR_Exception CobrEx)
            {
                throw CobrEx;
            }
        }
        public string Logo
        {
            get
            {
                switch (aEmpresaAtual.ToUpper())
                {
                    case "LUGPHIL":
                        return "Logo_LugPhil.jpg";
                    default:
                        return "Logo.jpg";
                }
            }
        }
        public string ImageFundo
        {
            get
            {
                switch (aEmpresaAtual.ToUpper())
                {
                    case "LUGPHIL":
                        return "logo_lugphil_fundo.gif";
                    default:
                        return "";
                }
            }
        }
        public int UsuarioID
        {
            get
            {
                return this.aCadUsuario.Usuario.ID;
            }
        }
        public int IdEntidade
        {
            get
            {
                return this.aCadUsuario.Usuario.idEntidade;
            }
        }
        #region IDisposable Members
        public void Dispose()
        {
            aCadUsuario.Dispose();
            aCadUsuario = null;
        }

        #endregion
    }
}

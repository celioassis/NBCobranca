using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace NBCobranca.Controllers
{
    public class CtrFactory : IDisposable
    {
        System.Collections.Hashtable _listaControllers;
        Classes.Sistema _sistema;
        string _usuarioLogado = "";

        public CtrFactory(Classes.Sistema pSistema)
        {
            this._sistema = pSistema;
            this._listaControllers = new System.Collections.Hashtable();
        }

        public CtrFactory()
            : this(new Classes.Sistema())
        { }

        public T GetInstance<T>()
        {
            Type mClasse = typeof(T);

            if (_listaControllers.Contains(mClasse.FullName))
            { return (T)_listaControllers[mClasse.FullName]; }
            else
            {
                var mClass = Assembly.GetAssembly(mClasse);

                object[] mParam;
                { mParam = new object[2]; }

                mParam[0] = this._sistema;
                mParam[1] = this;

                var mNg = (T)mClass.CreateInstance(mClasse.FullName, true, BindingFlags.CreateInstance, null, mParam, null, null);
                this._listaControllers.Add(mClasse.FullName, mNg);
                return mNg;
            }
        }


        public void ValidarLogin(string pUsuario, string pSenha)
        {
            _sistema.LimLogin.ValidarLogin(pUsuario, pSenha);
            this._usuarioLogado = _sistema.LimLogin.UsuarioLogado;
        }

        [Obsolete("Propriedade obsoleta e será retirada do sistema nas próximas versões, esta mantida para manter compatibilidade.")]
        public Classes.Sistema Sistema => _sistema;

        /// <summary>
        /// Classe controladora do aspx Devedores
        /// </summary>
        public ctrDevedores ctrDevedores
        {
            get
            {
                if (!this._listaControllers.Contains("ctrDevedores"))
                    this._listaControllers.Add("ctrDevedores", new ctrDevedores(this._sistema, this));
                return (ctrDevedores)_listaControllers["ctrDevedores"];
            }
        }
        public ctrBaixas ctrBaixas
        {
            get
            {
                if (!this._listaControllers.Contains("ctrBaixas"))
                    this._listaControllers.Add("ctrBaixas", new ctrBaixas(this._sistema, this));
                return (ctrBaixas)_listaControllers["ctrBaixas"];
            }
        }
        public ctrCadastroEntidades ctrCadEntidades
        {
            get
            {
                if (!this._listaControllers.Contains("ctrCadastroEntidades"))
                    this._listaControllers.Add("ctrCadastroEntidades", new ctrCadastroEntidades(this._sistema, this));
                return (ctrCadastroEntidades)_listaControllers["ctrCadastroEntidades"];
            }
        }
        public ctrCadastroAlertas ctrCadAlertas
        {
            get
            {
                if (!this._listaControllers.Contains("ctrCadastroAlertas"))
                    this._listaControllers.Add("ctrCadastroAlertas", new ctrCadastroAlertas(this._sistema, this));
                return (ctrCadastroAlertas)_listaControllers["ctrCadastroAlertas"];
            }
        }

        public ctrEnvioSMS ctrEnvioSMS
        {
            get
            {
                if (!this._listaControllers.Contains("ctrEnvioSMS"))
                    this._listaControllers.Add("ctrEnvioSMS", new ctrEnvioSMS(this._sistema, this));
                return (ctrEnvioSMS)this._listaControllers["ctrEnvioSMS"];
            }
        }

        public string UsuarioLogado
        {
            get
            {
                if (string.IsNullOrEmpty(_usuarioLogado))
                    _usuarioLogado = _sistema.LimLogin.UsuarioLogado;
                return _usuarioLogado;
            }
        }

        public string NomeCompletoUsuario => this._sistema.LimLogin.NomeCompletoUsuario;

        #region IDisposable Members

        public void Dispose()
        {
            this._listaControllers.Clear();
            this._listaControllers = null;
            if (_sistema.Connection != null && _sistema.Connection.State == ConnectionState.Open)
                this._sistema.Connection.Close();
            this._sistema = null;
        }

        #endregion
    }
}

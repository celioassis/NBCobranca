using NBCobranca.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBCobranca.Classes;
using NBCobranca.Entidades;

namespace NBCobranca.Controllers
{
    public class CtrConfiguracoes : CtrBase, IControllerPresenter
    {
        IpresConfiguracoes _view;
        BusConfiguracoes _Bus;

        public CtrConfiguracoes(Sistema sistema, CtrFactory ctrFactory) : base(sistema, ctrFactory)
        {
            _Bus = sistema.GetInstance<BusConfiguracoes>();
        }

        private entConfiguracoes GetConfiguracoes
        {
            get
            {
                var msgValidacoes = new List<string>();

                if (string.IsNullOrEmpty(_view.ServidorSMTP))
                    msgValidacoes.Add("O campo Servidor de SMTP não pode ficar em branco.");

                if (_view.PortaSMTP <= 0)
                    msgValidacoes.Add("O campo Porta SMTP não foi definida");

                if (string.IsNullOrEmpty(_view.UsuarioSMTP))
                    msgValidacoes.Add("O campo Usuário não pode ficar em branco.");

                if (string.IsNullOrEmpty(_view.Senha))
                    msgValidacoes.Add("O campo Senha não pode ficar em branco.");

                if (msgValidacoes.Count > 0)
                {
                    string msgErro = "";
                    msgValidacoes.ForEach(a =>
                    {
                        msgErro += a + "\r\n";
                    });
                    throw new Exception(msgErro);
                }

                return new entConfiguracoes
                {
                    ID_BD = _view.IDConfig,
                    ServidorSMTP = _view.ServidorSMTP,
                    PortaSMTP = _view.PortaSMTP,
                    UsuarioSMTP = _view.UsuarioSMTP,
                    Senha = _view.Senha
                };
            }
        }

        private void ValidaConfirmacaoSenha()
        {
            if (string.IsNullOrEmpty(_view.Senha) || !_view.Senha.Equals(_view.ConfirmaSenha))
                throw new Exception("A senha não foi informada ou é diferente da confirmação de senha.");
        }

        public void SetView(IPresView view)
        {
            _view = view as IpresConfiguracoes;
            _view.Titulo = "Configurações do Sistemas";

            if (!_view.IsCallBack)
                CarregarConfiguracoesSalvas();
        }

        public void SalvarConfiguracoes()
        {
            var configuracoes = GetConfiguracoes;
            ValidaConfirmacaoSenha();
            _Bus.SalvarConfiguracoes(configuracoes);
            _view.IDConfig = configuracoes.ID;
        }

        public void ValidarConfiguracoes()
        {
            ValidaConfirmacaoSenha();
            _Bus.ValidarConfiguracoes(GetConfiguracoes, _view.EmailDeValidacao);
        }

        public void CarregarConfiguracoesSalvas()
        {
            var configuracoes = _Bus.GetConfiguracoes;
            if (configuracoes != null)
            {
                _view.IDConfig = configuracoes.ID_BD;
                _view.ServidorSMTP = configuracoes.ServidorSMTP;
                _view.PortaSMTP = configuracoes.PortaSMTP;
                _view.UsuarioSMTP = configuracoes.UsuarioSMTP;
                _view.Senha = configuracoes.Senha;
            }
        }
    }
}

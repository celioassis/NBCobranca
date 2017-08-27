using System;
using System.Data;
using NBCobranca.Classes;
using NBCobranca.Interfaces;
using Neobridge.NBUtil;

namespace NBCobranca.Controllers
{
    public class CtrDistribuicaoFichas : CtrBase, IControllerPresenter
    {
        IPresPesquisaDistribuicaoFichas _view;

        protected const string MsgErroDistribuicao = "Não foi possível realizar a distribuição das fichas.<br/>Ocorreu o seguinte erro: {0}";
        protected const string MsgErroZerarDistribuicao = "Não foi possível zerar a distribuição das fichas.<br/>Ocorreu o seguinte erro: {0}";
        protected const string MsgErroPesquisar = "Não foi possível realizar a pesquisa das fichas para distribuição.<br/>Ocorreu o seguinte erro: {0}";

        public CtrDistribuicaoFichas(Sistema sistema, CtrFactory ctrFactory)
            : base(sistema, ctrFactory)
        { }

        #region IcontrollerPresenter Members

        public virtual void SetView(IPresView view)
        {
            ValidaCredencial();
            _view = (IPresPesquisaDistribuicaoFichas) view;
            _view.Titulo = "Pesquisa de fichas para criação automática de carterias de acionamento";
            _view.CarregaListaClientes(Sistema.busEntidade.LoadClientes());
            _view.CarregaListaTiposDeDivida(Sistema.busTiposDivida.DataSource.DefaultView);
            _view.CarregaListaDeCobradores(Sistema.busFuncionarios.ListaSomenteCobradores());
        }

        #endregion

        public virtual void Pesquisar()
        {
            try
            {
                
                var mDados = Sistema.busDistribuirFichas
                    .PesquisarFichasParaDistribuicaoAutomatica(
                        _view.GetCarteiraSelecionada,
                        _view.GetTipoDeDividaSelecionada,
                        _view.GetFiltroDataVencimento,
                        _view.GetFiltroMes,
                        _view.GetFiltroAno);
                _view.CarregaFichasPesquisadas(mDados.DefaultView);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(MsgErroPesquisar, ex.Message));
            }
        }

        public virtual void Distribuir()
        {
            try
            {
                var mLogDistribuicao = Sistema.busDistribuirFichas
                    .DistribuirFichas(_view.GetCarteiraSelecionada
                        , CtrFactory.UsuarioLogado
                        , _view.GetTipoDeDividaSelecionada
                        , _view.GetFiltroDataVencimento
                        , _view.GetFiltroMes
                        , _view.GetFiltroAno
                        , _view.GetAcionadoresSelecionados);
                _view.EnviarMensagem(mLogDistribuicao, BootStrapDialog.TypeMessage.TYPE_SUCCESS);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(MsgErroDistribuicao, ex.Message));
            }
        }

        public virtual void ZerarDistribuicao(string pMotivo)
        {
            try
            {
                Sistema.busDistribuirFichas.ZerarDistribuicao(_view.GetCarteiraSelecionada, pMotivo, CtrFactory.UsuarioLogado);
                _view.EnviarMensagem("As fichas foram zeradas com sucesso!", BootStrapDialog.TypeMessage.TYPE_SUCCESS);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(MsgErroZerarDistribuicao, ex.Message));
            }

        }

    }
}

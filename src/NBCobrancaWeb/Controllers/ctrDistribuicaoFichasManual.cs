using System;
using System.Data;
using System.Configuration;
using System.Text;
using NBCobranca.Interfaces;
using Neobridge.NBUtil;

namespace NBCobranca.Controllers
{
    public class CtrDistribuicaoFichasManual : CtrDistribuicaoFichas
    {
        IPresPesquisaDistribuicaoFichasManual _view;

        public CtrDistribuicaoFichasManual(Classes.Sistema sistema, CtrFactory ctrFactory)
            : base(sistema, ctrFactory)
        { }

        public override void SetView(IPresView view)
        {
            this.ValidaCredencial();
            _view = (IPresPesquisaDistribuicaoFichasManual)view;
            this._view.Titulo = "Pesquisa de fichas para criação manual de carterias de acionamento";

            this._view.CarregaListaClientes(this.Sistema.busEntidade.LoadClientes());
            this._view.CarregaListaTiposDeDivida(this.Sistema.busTiposDivida.DataSource.DefaultView);
        }

        public override void Distribuir()
        {
            var acionador = Sistema.busFuncionarios.ListaIdEntidadeCobradores(false, _view.AcionadorDeDestino)[0];
            var fichasSelecionados = _view.ListaEntidadesSelecionadas;
            foreach (var id in fichasSelecionados)
            {
                Sistema.busDistribuirFichas.DistribuirFicha(id, _view.AcionadorDeDestino, CtrFactory.UsuarioLogado);
            }

            Sistema.busDistribuirFichas.RegistrarHistoricoDeDistribuicao();

            Pesquisar();
            var mLogDistribuicao = new StringBuilder();
            mLogDistribuicao.AppendFormat("<p><h4><strong>A distribuição manual de fichas para o Acionador {0}", acionador.NomePrimary);
            mLogDistribuicao.Append(" foi concluida com exito</strong></h4></p>");
            mLogDistribuicao.AppendFormat("<p>Este acionador recebeu {0} ficha(s)</p>", fichasSelecionados.Count);
            _view.EnviarMensagem(mLogDistribuicao.ToString(), BootStrapDialog.TypeMessage.TYPE_INFO);
        }

        public override void ZerarDistribuicao(string pMotivo)
        {
            var fichasSelecionados = _view.ListaEntidadesSelecionadas;
            foreach (var id in fichasSelecionados)
            {
                Sistema.busDistribuirFichas.LiberarFichaParaDistribuicao(id, pMotivo, CtrFactory.UsuarioLogado);
            }
            Pesquisar();
            _view.EnviarMensagem("<p><h4><strong>Fichas liberadas com sucesso</strong></h4></p>", BootStrapDialog.TypeMessage.TYPE_INFO);
        }

        public override void Pesquisar()
        {
            try
            {
                _view.CarregaListaDeCobradores(Sistema.busFuncionarios.ListaSomenteCobradores(false));

                var fichas = Sistema.busDistribuirFichas
                    .PesquisarFichasParaDistribuicaoManual(
                        _view.GetCarteiraSelecionada, 
                        _view.IdTipoDivida, 
                        _view.QuantidadeDeDividas, 
                        _view.SomenteDisponiveis,
                        _view.GetFiltroDataVencimento,
                        _view.GetFiltroMes,
                        _view.GetFiltroAno);
                _view.CarregaFichasPesquisadas(fichas.DefaultView);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(MsgErroPesquisar, ex.Message));
            }
        }
    }
}

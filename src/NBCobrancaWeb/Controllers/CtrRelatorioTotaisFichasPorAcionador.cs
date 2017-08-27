using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBCobranca.Classes;
using NBCobranca.Interfaces;
using NBCobranca.Tipos;
using NBdbm;

namespace NBCobranca.Controllers
{
    public class CtrRelatorioTotaisFichasPorAcionador : CtrBase, IControllerPresenter
    {
        private IPresRelatorioTotalDeFichasPorAcionador _view;

        public CtrRelatorioTotaisFichasPorAcionador(Sistema sistema, CtrFactory ctrFactory) : base(sistema, ctrFactory)
        {
        }

        public void SetView(IPresView view)
        {
            try
            {
                ValidaCredencial();
                _view = (IPresRelatorioTotalDeFichasPorAcionador)view;
                _view.Titulo = "Relatório de Totais de Fichas por Acionador";
                _view.CarregaListaTiposDeDivida(Sistema.busTiposDivida.DataSource.DefaultView);
                _view.CarregaListaDeCobradores(Sistema.busFuncionarios.ListaSomenteCobradores());
                _view.CarregaListaTipoDeAcionamentos(Sistema.busTiposAcionamento.ListarTodos);

            }
            catch (Exception ex)
            {
                throw new COBR_Exception("Não foi possível carregar a página do relatório de totais de fichas por acionador.", GetType().FullName, ex);
            }

        }

        public void Pesquisar()
        {
            try
            {
                var resultado = Sistema.busFuncionarios.ListarTotaisPorAcionador(
                    _view.GetAcionadorSelecionado,
                    _view.GetTipoDeDividaSelecionada,
                    _view.GetDataVencimentoInicial,
                    _view.GetDataVencimentoFinal,
                    _view.GetDataAcionamentoInicial,
                    _view.GetDataAcionamentoFinal,
                    _view.GetTipoDeAcionamentoSelecionada);
                _view.CarregaPesquisa(resultado);
            }
            catch (Exception ex)
            {
                throw new COBR_Exception("Não foi possível realizar a pesquisa para gerar o relatório de totais de fichas por acionador.", GetType().FullName, ex);
            }


        }

        public void BuscarDetalhesDoAcionador(int codigoAcionador)
        {
            try
            {
                _view.PreencherDetalhesDoAcionador(Sistema.busFuncionarios.BuscarDetalhesDoAcionador(
                        codigoAcionador,
                        _view.GetTipoDeDividaSelecionada,
                        _view.GetDataVencimentoInicial,
                        _view.GetDataVencimentoFinal,
                        _view.GetDataAcionamentoInicial,
                        _view.GetDataAcionamentoFinal,
                        _view.GetTipoDeAcionamentoSelecionada
                    ));
            }
            catch (Exception ex)
            {
                throw new COBR_Exception("Não foi possível realizar a busca de detalhes do acionador", GetType().FullName, ex);
            }
        }

        public void CarregarFicha(int codigoFicha)
        {
            try
            {
                Sistema.LimAcionamentos.GetDevedor(codigoFicha);
                _view.MostrarFicha();
            }
            catch (Exception ex)
            {
                throw new COBR_Exception("Não foi possível realizar a busca de detalhes do acionador", GetType().FullName, ex);
            }
        }
    }
}

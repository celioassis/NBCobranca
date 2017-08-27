using System;
using System.Collections;
using System.Collections.Generic;
using NBCobranca.Entidades;
using NBCobranca.Tipos;

namespace NBCobranca.Interfaces
{
    public interface IPresRelatorioTotalDeFichasPorAcionador: IPresView
    {
        /// <summary>
        /// Get - Retorna o Id do tipo de dívida selecionado no filtro de tipo de dívidas
        /// </summary>
        int? GetTipoDeDividaSelecionada { get; }

        /// <summary>
        /// Get - Retorna o Id do tipo de acionamento selecionado no filtro de tipos de acionamentos.
        /// </summary>
        int? GetTipoDeAcionamentoSelecionada { get; }

        /// <summary>
        /// Get - retorna o id do acionador selecionado para filtro.
        /// </summary>
        int? GetAcionadorSelecionado { get; }

        /// <summary>
        /// Get - Retorna a data informada no filtro de data de vencimento inicial
        /// </summary>
        DateTime? GetDataVencimentoInicial { get; }

        /// <summary>
        /// Get - Retorna a data informada no filtro de data de vencimento final
        /// </summary>
        DateTime? GetDataVencimentoFinal { get; }

        /// <summary>
        /// Get - Retorna a data informada no filtro de data de acionamento inicial
        /// </summary>
        DateTime? GetDataAcionamentoInicial { get; }

        /// <summary>
        /// Get - Retorna a data informada no filtro de data de acionamento final
        /// </summary>
        DateTime? GetDataAcionamentoFinal { get; }


        /// <summary>
        /// Popula uma Grid na View com os totais de fichas por acionador
        /// </summary>
        /// <param name="listaTotaisPorAcionador"></param>
        void CarregaPesquisa(IEnumerable listaTotaisPorAcionador);

        /// <summary>
        /// Carrega todos os tipos de dívida para uma combobox da view
        /// </summary>
        /// <param name="listaTiposDeDivida">Lista dos tipos de dívida que será populado na combobox</param>
        void CarregaListaTiposDeDivida(IEnumerable listaTiposDeDivida);

        /// <summary>
        /// Carrega todos os tipos de acionamentos para uma combobox da view
        /// </summary>
        /// <param name="listaTipoAcionamentos">Lista dos tipos de acionamentos que será populado na combobox</param>
        void CarregaListaTipoDeAcionamentos(IEnumerable listaTipoAcionamentos);
        
        /// <summary>
        /// Carrega a lista de acionadores para uma cobobox da view
        /// </summary>
        /// <param name="listaSomenteCobradores">Lista com todos os cobradores</param>
        void CarregaListaDeCobradores(IEnumerable listaSomenteCobradores);

        /// <summary>
        /// Preenche o Panel Com os detalhes do Acionador selecionado.
        /// </summary>
        /// <param name="detalhesDoAcionador"></param>
        void PreencherDetalhesDoAcionador(DtoDetalhesDoAcionador detalhesDoAcionador);

        /// <summary>
        /// Mostra uma ficha selecionada para ser acionada.
        /// </summary>
        void MostrarFicha();
    }
}
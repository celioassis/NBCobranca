using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NBCobranca.Interfaces
{
    public interface IPresPesquisaDistribuicaoFichas: IPresView
    {
        /// <summary>
        /// Carrega todos os clientes para uma combobox da view
        /// </summary>
        /// <param name="pListaClientes">Lista dos clientes que será populado na combobox</param>
        void CarregaListaClientes(System.Collections.ICollection pListaClientes);

        /// <summary>
        /// Popula uma Grid na View com as Fichas pesquisadas
        /// </summary>
        /// <param name="pListaFichasPesquisadas">Lista de fichas encontradas utilizando os filtros.</param>
        void CarregaFichasPesquisadas(System.Collections.ICollection pListaFichasPesquisadas);
        
        /// <summary>
        /// Get - Retorna o Id da entidade do Cliente Selecionado no filtro de Clientes, se o retorno for 0 então não foi definido um filtro de clientes.
        /// </summary>
        string GetCarteiraSelecionada { get;}

        /// <summary>
        /// Get - Retorna o Id do tipo de dívida selecionado no filtro de tipo de dívidas
        /// </summary>
        int GetTipoDeDividaSelecionada { get; }

        /// <summary>
        /// Get - Retorna a data informada no filtro de data de vencimento
        /// </summary>
        DateTime? GetFiltroDataVencimento { get; }

        /// <summary>
        /// Get - Retorna o Mes de Vencimento informado no filtro de Mes
        /// </summary>
        int? GetFiltroMes { get; }

        /// <summary>
        /// Get - Retorna o Ano de Vencimento de uma dívida informando no filtro de Ano
        /// </summary>
        int? GetFiltroAno { get; }

        /// <summary>
        /// Carrega todos os tipos de dívida para uma combobox da view
        /// </summary>
        /// <param name="pListaTiposDeDivida">Lista dos tipos de dívida que será populado na combobox</param>
        void CarregaListaTiposDeDivida(ICollection pListaTiposDeDivida);

        void CarregaListaDeCobradores(ICollection pListaDeCobradores);

        /// <summary>
        /// Get - Retorna a lista dos acionadores que receberão fichas
        /// </summary>
        List<int> GetAcionadoresSelecionados { get; }
    }
}

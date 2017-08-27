namespace NBCobranca.Interfaces
{
    public interface IPresDevedores: IPresView
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
        string GetCarteiraSelecionada { get; }

        /// <summary>
        /// Get - Retorna o value da combobox SelCampoSelecionar, onde indica qual campo será utilizado para realizar a pesquisa do devedor.
        /// <para>Eles podem ser: 1 - ID, 2 - Nome ou 3 - CPF/CNPJ</para>
        /// </summary>
        int GetCampoFiltro { get; }

        /// <summary>
        /// Get - Texto que foi digitado no vampo oque deseja procurar.
        /// </summary>
        string GetTextoProcurar { get; }
    }
}
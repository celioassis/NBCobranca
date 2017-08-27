using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using NBCobranca.Tipos;

namespace NBCobranca.Interfaces
{
    public interface IPresPesquisaDistribuicaoFichasManual: IPresPesquisaDistribuicaoFichas
    {
        /// <summary>
        /// Busca todas as entidades selecionads para a distribui��o.
        /// </summary>
        List<int> ListaEntidadesSelecionadas { get;}
        
        /// <summary>
        /// Busca o IdEntidade do acionador que ir� receber as fichas selecionadas para distribui��o.
        /// </summary>
        int AcionadorDeDestino { get;}

        /// <summary>
        /// Quantidade de d�vidas digitada pelo usu�rio.
        /// </summary>
        FiltroQuantidadeDeDividas QuantidadeDeDividas { get;}

        /// <summary>
        /// Id do tipo de d�vida selecionado pelo usu�rio
        /// </summary>
        int IdTipoDivida { get;}

        bool SomenteDisponiveis { get; }
    }
}

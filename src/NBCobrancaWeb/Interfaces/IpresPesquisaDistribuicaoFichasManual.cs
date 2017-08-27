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
        /// Busca todas as entidades selecionads para a distribuição.
        /// </summary>
        List<int> ListaEntidadesSelecionadas { get;}
        
        /// <summary>
        /// Busca o IdEntidade do acionador que irá receber as fichas selecionadas para distribuição.
        /// </summary>
        int AcionadorDeDestino { get;}

        /// <summary>
        /// Quantidade de dívidas digitada pelo usuário.
        /// </summary>
        FiltroQuantidadeDeDividas QuantidadeDeDividas { get;}

        /// <summary>
        /// Id do tipo de dívida selecionado pelo usuário
        /// </summary>
        int IdTipoDivida { get;}

        bool SomenteDisponiveis { get; }
    }
}

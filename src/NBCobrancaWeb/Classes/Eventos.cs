using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace NBCobranca.Classes
{
    /// <summary>
    /// Classe que contém todos os Delegates de Eventos do Sistema.
    /// </summary>
    public static class Eventos
    {
        /// <summary>
        /// Delegate para disparar eventos referente a ações de linkbuttons amarrados a coleções
        /// </summary>
        /// <param name="pAcao">Tipo da ação que o linkButton ira executou</param>
        /// <param name="pColecao">Nome da Coleção em que a ação do linkButton será executada.</param>
        public delegate void AcaoLinkButtonsHandler(Tipos.TipoAcaoLinkButtons pAcao, Tipos.TipoColecoes pColecao);
    }
}

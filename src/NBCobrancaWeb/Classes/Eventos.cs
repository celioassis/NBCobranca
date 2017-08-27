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
    /// Classe que cont�m todos os Delegates de Eventos do Sistema.
    /// </summary>
    public static class Eventos
    {
        /// <summary>
        /// Delegate para disparar eventos referente a a��es de linkbuttons amarrados a cole��es
        /// </summary>
        /// <param name="pAcao">Tipo da a��o que o linkButton ira executou</param>
        /// <param name="pColecao">Nome da Cole��o em que a a��o do linkButton ser� executada.</param>
        public delegate void AcaoLinkButtonsHandler(Tipos.TipoAcaoLinkButtons pAcao, Tipos.TipoColecoes pColecao);
    }
}

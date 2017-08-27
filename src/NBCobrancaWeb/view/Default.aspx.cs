using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NBCobranca.Classes;
using NBCobranca.Tipos;

namespace NBCobranca.view
{
    public partial class _Default : PageBaseComController<Controllers.ctrDefault>, Interfaces.IPresDefault
    {
        /// <summary>
        /// Variável utilizada para definir uma imagem de fundo futura.
        /// </summary>
        string aImagemFundoTemp = "";

        #region IpresDefault Members

        public string ImagemFundo
        {
            get { return aImagemFundoTemp; }
            set { aImagemFundoTemp = value; }
        }

        public string DataCompletaDoDia
        {
            set { this.lblDataCompleta.Text = value; }
        }

        public string MensagemDeBoasVindas
        {
            set { ((Interfaces.IPresDefault)this).Titulo = value; }
        }

        #endregion
    }
}

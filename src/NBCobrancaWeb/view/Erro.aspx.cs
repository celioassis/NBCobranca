using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBCobranca.view
{
    public partial class Erro : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = Request.QueryString["msgErro"].Replace("\\r", "<br/>");
        }
    }
}
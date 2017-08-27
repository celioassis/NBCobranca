using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NBCobranca.Controllers;
using NBCobranca.Interfaces;

namespace NBCobranca.view
{
    public partial class Rodizio : PageBaseComController<CtrDistribuicaoRodizio>, IpresDistribuicaoRodizio
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProcessarRodizio_Click(object sender, EventArgs e)
        {
            try
            {
                CtrPage.ProcessarRodizio(hfMotivo.Value);
            }
            catch (Exception ex)
            {
                EnviarMensagem(ex.Message);
            }
        }
    }
}
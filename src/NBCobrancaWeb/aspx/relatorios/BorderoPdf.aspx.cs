using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NBCobranca.Classes;
using NBCobranca.Classes.Relatorios;

namespace NBCobranca.aspx.relatorios
{
    public partial class BorderoPdf : System.Web.UI.Page
    {
        private Sistema _sistema;
        private BusBordero _obj;

        protected void Page_Load(object sender, EventArgs e)
        {
            NBFuncoes.ValidarSistema(this, ref _sistema);
            _obj = _sistema.BusBordero;
            var stream = new RelBorderos(Server).Print(_obj.RelatorioBordero);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", stream.Length.ToString());
            Response.BinaryWrite(stream.ToArray());
            Response.Flush();
            stream.Close();
        }
    }
}
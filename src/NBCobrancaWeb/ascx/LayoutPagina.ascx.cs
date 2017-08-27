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
using System.ComponentModel;

namespace NBCobranca.ascx
{
    public partial class LayoutPagina : System.Web.UI.UserControl
    {
        private ITemplate _ConteudoInterno;

        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TemplateInstance(TemplateInstance.Single)]
        public ITemplate ConteudoInterno
        {
            get { return _ConteudoInterno; }
            set { _ConteudoInterno = value; }
        }

        protected override void CreateChildControls()
        {
            if (this.ConteudoInterno != null)
            {
                this.phLayout.Controls.Clear();
                this.ConteudoInterno.InstantiateIn(this.phLayout);
            }
            base.CreateChildControls();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
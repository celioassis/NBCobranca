using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using NBCobranca.Classes;
using NBCobranca.Tipos;

namespace NBCobranca.aspx
{
	/// <summary>
	/// Login do Sistema
	/// </summary>
	public partial class Login : System.Web.UI.Page
	{
		private Sistema Sistema;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			ComponentArt.Web.UI.Menu menu;
			System.Web.UI.WebControls.Image imgBarraMenu;
			//Label mLblVersao;
			imgBarraMenu = (System.Web.UI.WebControls.Image)this.Master.FindControl("imgBarraMenu");
			menu = (ComponentArt.Web.UI.Menu)this.Master.FindControl("Menu1");
			
			menu.Visible = false;
			imgBarraMenu.Visible = true;
            if (!IsPostBack)
            {
                if (Session["Sistema"] != null)
                    ((Sistema)Session["Sistema"]).Dispose();
                Session.Clear();
                this.MessageBox.MoverFoco(txtUsuario.ClientID);
            }
            else
                this.Sistema = (Sistema)Session["Sistema"];
		}

		protected void imgBtnEntrar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (Session["Sistema"] == null)
			{
                Controllers.CtrFactory aController = new NBCobranca.Controllers.CtrFactory();
                Session["Controller"] = aController;
                this.Sistema = aController.Sistema;
				Session["Sistema"] = this.Sistema;
			}

			try
			{
				Sistema.LimLogin.ValidarLogin(txtUsuario.Text, txtSenha.Text);
				Response.Redirect("Default.aspx");
			}
			catch(Exception ex)
			{
				this.MessageBox.Show(ex.Message);
			}
			
		}

		public override void Dispose()
		{
			base.Dispose ();
			if(Sistema != null)
			{
				if (Sistema.Connection.State == System.Data.ConnectionState.Open)
					Sistema.Connection.Close();
				Sistema.Connection.Dispose();
				Sistema.Connection = null;
			}
		}

	}
}

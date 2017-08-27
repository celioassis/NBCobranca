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

namespace NBCobranca.aspx
{
	/// <summary>
	/// Summary description for ModalInput.
	/// </summary>
	public partial class ModalInput : System.Web.UI.Page
	{
		public string Titulo;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			this.Titulo = HttpUtility.UrlDecode(this.Request.QueryString["Titulo"].ToString());
			this.lblDescricao.Text = HttpUtility.UrlDecode(this.Request.QueryString["Descricao"].ToString());
			this.lblHelp.Text = HttpUtility.UrlDecode(this.Request.QueryString["Help"].ToString());
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.imgBtnOk.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnOk_Click);

		}
		#endregion

		private void imgBtnOk_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.MsgBox.ModalClose(txtDescricao.Text,"ModalInput",this.Request.QueryString["MsgBoxId"]);
		}
	}
}

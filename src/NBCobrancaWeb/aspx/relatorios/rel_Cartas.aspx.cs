using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NBCobranca.Classes;

namespace NBCobranca.aspx.relatorios
{
	/// <summary>
	/// Summary description for rel_Cartas.
	/// </summary>
	public partial class rel_Cartas : Page
	{
	
		private bool SegundoAviso;
		Sistema Sistema;
		LimAcionamentos obj;
		protected void Page_Load(object sender, EventArgs e)
		{
			NBFuncoes.ValidarSistema(this,ref Sistema);
			obj = Sistema.LimAcionamentos;

			if(!IsPostBack)
			{

				try
				{
					if(Request.QueryString["2Aviso"].ToUpper() != "FALSE")
						SegundoAviso = true;

					dgCartas.DataSource = obj.DataSourceCartas;
					dgCartas.DataBind();

				}
				catch(Exception ex)
				{
					string msg = ex.Message;
				}
			}
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
			this.dgCartas.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCartas_ItemDataBound);

		}
		#endregion

		private void dgCartas_ItemDataBound(object sender, DataGridItemEventArgs e)
		{

			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Label mlblNotificacao;
				mlblNotificacao = (Label)e.Item.FindControl("lblNotificacao");
				mlblNotificacao.Visible = SegundoAviso;
			}
		}
	}
}

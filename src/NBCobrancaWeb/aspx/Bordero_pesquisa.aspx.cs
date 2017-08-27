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

namespace NBCobranca.aspx
{
	/// <summary>
	/// Summary description for Bordero_pesquisa.
	/// </summary>
	public partial class Bordero_pesquisa : System.Web.UI.Page
	{
	
		Sistema Sistema;
		BusBordero Obj;
        LimAcionamentos ObjAcionamento;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			NBFuncoes.ValidarSistema(this,ref this.Sistema);
			
			this.Obj = this.Sistema.BusBordero;
            this.ObjAcionamento = this.Sistema.LimAcionamentos;
			this.Sistema.Legenda.Titulo = "Relatório de Borderos";

			if(!this.IsPostBack)
			{
				this.Sistema.ValidaCredencial();

				System.Web.UI.WebControls.ListItem mLI = new ListItem("Todas");
				mLI.Selected = true;
				
				this.ddlCarteiras.DataSource = this.Sistema.LimAcionamentos.Carteiras;
				this.ddlCarteiras.DataBind();
				this.ddlCarteiras.Items.Insert(0,mLI);
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
			this.imgBtnPesquisar.Click += new ImageClickEventHandler(this.imgBtnPesquisar_Click);

		}
		#endregion

		private void imgBtnPesquisar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.Obj.FiltroCarteira = this.ddlCarteiras.SelectedValue;
            this.Obj.FiltroDataInicial = null;
            this.Obj.FiltroDataFinal = null;

			if(this.txtDataInicial_DATA.Text != "")
				this.Obj.FiltroDataInicial = DateTime.Parse(txtDataInicial_DATA.Text);

			
			if(this.txtDataFinal_DATA.Text != "")
                this.Obj.FiltroDataFinal = DateTime.Parse(txtDataFinal_DATA.Text);
			else if(this.txtDataInicial_DATA.Text != "")
			{
				this.Obj.FiltroDataFinal = DateTime.Parse(txtDataInicial_DATA.Text);
				this.txtDataFinal_DATA.Text = txtDataInicial_DATA.Text;
			}
			this.Obj.FiltroBordero = this.txtNumBordero_INT.Text;

			if(ddlCarteiras.SelectedValue == "Todas")
				dgDados.Columns[0].Visible = true;
			else
				dgDados.Columns[0].Visible = false;
            
            Obj.GerarRelatorio();
		    this.dgDados.DataSource = Obj.RelatorioBordero.Registros;//this.Obj.DataSource;
			this.dgDados.DataBind();
			if(Obj.RelatorioBordero.Registros.Count > 0)
			{
				this.imgBtnPrint.Style["VISIBILITY"]="visible";
				this.dgDados.Visible = true;
				this.btnResumo.Visible=true;
				this.Obj.NovoResumo();
			}
			else
			{
				this.imgBtnPrint.Style["VISIBILITY"]="hidden";
				this.dgDados.Visible = false;
				this.btnResumo.Visible=false;
			}
		}

		protected void btnResumo_Click(object sender, System.EventArgs e)
		{
			this.MessageBox.ModalShow("Bordero_resumo.aspx");
		}

        protected void dgDados_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Acionar")
            {
                this.ObjAcionamento.GetDevedor(int.Parse(e.Item.Cells[1].Text));
                this.MessageBox.Largura = 900;
                this.MessageBox.ModalShow("Acionamento_Ficha.aspx");
            }

        }

	}
}

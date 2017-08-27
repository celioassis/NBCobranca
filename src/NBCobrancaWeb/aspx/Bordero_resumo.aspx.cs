using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NBCobranca.Classes;
using NBdbm;

namespace NBCobranca.aspx
{
	/// <summary>
	/// Summary description for Bordero_resumo.
	/// </summary>
	public partial class Bordero_resumo : Page
	{
		
		Sistema _sistema;
		BusBordero Obj;

		protected void Page_Load(object sender, EventArgs e)
		{
			NBFuncoes.ValidarSistema(this,ref _sistema);
			
			Obj = _sistema.BusBordero;

			if(!IsPostBack)
			{
				_sistema.ValidaCredencial();
				if (Obj.RelatorioBordero.ResumoRelBorderos.Count>0)
					AtualizaGridResumo();
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
			this.dgResumo.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgResumo_ItemCommand);

		}
		#endregion

		protected void btnNovo_Click(object sender, EventArgs e)
		{
			Obj.NovoItemResumo();
			MessageBox.MoverFoco(txtLinha_INT.ClientID);
			ModoEdicao = true;
		}

		protected void btnSalvar_Click(object sender, EventArgs e)
		{
			try
			{
				Obj.ItemResumo.NumeroLinha = Convert.ToInt32(txtLinha_INT.Text);
				Obj.ItemResumo.Descricao = txtDescResumo.Text;
				Obj.ItemResumo.Valor = double.Parse(txtValor_MOEDA.Text.Replace(".",""));
				Obj.SalvaItemResumo();
				AtualizaGridResumo();
				ModoEdicao = false;
			}
			catch(COBR_Exception CobrEx)
			{
				MessageBox.Show(CobrEx.Message);
			}
		}

        private void AtualizaGridResumo()
		{
			if(!dgResumo.Columns[3].Visible)
				dgResumo.Columns[3].Visible = true;
			if(Obj.RelatorioBordero.ResumoRelBorderos.Count>0)
			{
			    dgResumo.DataSource = Obj.RelatorioBordero.ResumoRelBorderos.Values;
				dgResumo.DataBind();
				dgResumo.Visible = true;
			}
			else
				dgResumo.Visible = false;
		}

		private void dgResumo_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			switch(e.CommandName)
			{
				case "Editar":
					Obj.CarregaItemResumo(Convert.ToInt32(e.CommandArgument));
					ModoEdicao = true;
					txtLinha_INT.Text = Obj.ItemResumo.NumeroLinha.ToString();
					txtDescResumo.Text = Obj.ItemResumo.Descricao;
					txtValor_MOEDA.Text = Obj.ItemResumo.Valor.ToString("N");
					dgResumo.Columns[3].Visible = false;
					MessageBox.MoverFoco(txtDescResumo.ClientID);
					break;
				case "Apagar":
					Obj.RemoveItemResumo(Convert.ToInt32(e.CommandArgument));
					AtualizaGridResumo();
					break;
			}
		}
	
		private bool ModoEdicao
		{
			set
			{
				txtLinha_INT.ReadOnly = !value;
				txtDescResumo.ReadOnly = !value;
				txtValor_MOEDA.ReadOnly = !value;

				txtLinha_INT.Text = "";
				txtDescResumo.Text = "";
				txtValor_MOEDA.Text = "";

				btnNovo.Visible = !value;
				btnSalvar.Visible = value;
			}
		}
	}
}

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
	/// Summary description for Acionamento_Simulacao.
	/// </summary>
	public partial class Acionamento_Simulacao : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lbtnTituloSnapDivida;
		private Classes.Sistema Sistema;
		private Classes.LimAcionamentos obj;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			NBFuncoes.ValidarSistema(this,ref Sistema,this.MessageBox);
			if(this.MessageBox.FechandoModal)
				return;
			obj = this.Sistema.LimAcionamentos;
			if (!this.IsPostBack)
			{
				txtDataParcela1_DATA.Text = DateTime.Today.AddDays(30).ToString("dd/MM/yyyy");
                txtPercJuros_MOEDA.Text = this.obj.Juros.ToString("N2");
				this.dgSnapDividas.DataSource = this.obj.DataSourceDividasVencidas;
				this.dgSnapDividas.DataBind();

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

		}
		#endregion

		public void dgDividas_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex >= 0)
			{
				NBdbm.Interfaces.iCOBR.Primitivas.iDivida mDivida = (NBdbm.Interfaces.iCOBR.Primitivas.iDivida)e.Item.DataItem;
				string txt =this.obj.GetDescricaoTipoDivida(mDivida.idTipoDivida);
				e.Item.Cells[3].Text = txt;
				if(mDivida.BaixaParcial)
					e.Item.Cells[7].Text = obj.ValorNominalParcial(mDivida).ToString("N");
				e.Item.Cells[8].Text = obj.ValorCorrigido(mDivida,double.Parse(e.Item.Cells[7].Text)).ToString("N");
				e.Item.Cells[2].Text = obj.GetCarteiraAtual;
				e.Item.Cells[0].Text = this.obj.GetKey(TipoColecoes.DividasVencidas,e.Item.ItemIndex);
				
			}
			if (e.Item.ItemType == ListItemType.Footer)
			{
				e.Item.Cells[2].Text = "Soma Total da Dívida:&nbsp;";
				e.Item.Cells[3].Text = obj.DividaTotalNominal.ToString("N");
				e.Item.Cells[4].Text = obj.DividaTotalCorrigida.ToString("N");
			}

		}
		public void dgDividas_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer)
			{
				for(int i=4;i>=1;i--)
					e.Item.Cells.RemoveAt(i);
				e.Item.Cells[2].ColumnSpan=5;
				e.Item.Cells[2].HorizontalAlign = HorizontalAlign.Right;
				e.Item.Cells[3].HorizontalAlign = HorizontalAlign.Right;
				e.Item.Cells[4].HorizontalAlign = HorizontalAlign.Right;
			}
		}

		public void ckbHeader_CheckedChanged(object sender, System.EventArgs e)
		{
			if (this.MessageBox.FechandoModal)
				return;
			Anthem.CheckBox mckbHeader = (Anthem.CheckBox)sender;
			this.SomaValorSelecionado(mckbHeader);
		}
		public void ckbItem_CheckedChanged(object sender, System.EventArgs e)
		{
			if (this.MessageBox.FechandoModal)
				return;
			this.SomaValorSelecionado();
		}

		protected void btnSalvarSimulacao_Click(object sender, System.EventArgs e)
		{
			System.Text.StringBuilder mTxtSimulacao = new System.Text.StringBuilder();
			mTxtSimulacao.Append("*** Simulação de Parcelamento ***|");
			mTxtSimulacao.Append("Valor Entrada: R$ " + txtValorEntrada_MOEDA.Text + "|");
			mTxtSimulacao.Append("Valor Parcela: R$ " + txtValorParcela_MOEDA.Text + "||");
			mTxtSimulacao.Append("*** Detalhamento ***|");
			mTxtSimulacao.Append("01 X " + double.Parse(txtTotParcela_INT.Text).ToString("00") + " - " + txtDataParcela1_DATA.Text);
			if (double.Parse(txtTotParcela_INT.Text)>1)
			{
				mTxtSimulacao.Append("|" + double.Parse(txtTotParcela_INT.Text).ToString("00") + " X " + double.Parse(txtTotParcela_INT.Text).ToString("00") + " - " + DateTime.Parse(txtDataParcela1_DATA.Text).AddDays(double.Parse(txtDiasParcela.Text) * (double.Parse(txtTotParcela_INT.Text)-1)).ToString("dd/MM/yyyy") +"|");
				mTxtSimulacao.Append("Dias entre cada parcela: "+ txtDiasParcela.Text + " dias|");
			}
			this.MessageBox.ModalClose(mTxtSimulacao.ToString(),"Simulacao");
		}

		protected void txtTotParcela_INT_TextChanged(object sender, System.EventArgs e)
		{
			if(int.Parse(txtTotParcela_INT.Text)<1)
				this.MessageBox.Show("Não é permitido Parcelas abaixo de 1",txtTotParcela_INT.ClientID);
			else
				SomaValorSelecionado();
		}

		protected void txtDiasParcela_TextChanged(object sender, System.EventArgs e)
		{
			int mDiasEntreParcela = int.Parse(txtDiasParcela.Text); 
			if(mDiasEntreParcela < 1)
				this.MessageBox.Show("Não é permitido dias entre cada parcela abaixo de 1",txtDiasParcela.ClientID);
				
			SomaValorSelecionado();
		
		}

        protected void txtPercJuros_MOEDA_TextChanged(object sender, System.EventArgs e)
        {
            double mPercentualJuros = Convert.ToDouble(txtPercJuros_MOEDA.Text);

            if (mPercentualJuros <= 0)
            {
                this.MessageBox.Show("Não é permitido Percentual de Juros menor ou igual a zero.", txtPercJuros_MOEDA.ClientID);
                txtPercJuros_MOEDA.Text = this.obj.Juros.ToString("N");
            }
           
            SomaValorSelecionado();

        }

		protected void txtValorEntrada_MOEDA_TextChanged(object sender, System.EventArgs e)
		{
			double mValorEntrada = double.Parse(txtValorEntrada_MOEDA.Text);
			if(mValorEntrada < 0)
				this.MessageBox.Show("Não é permitido valor negativo para entrada",txtValorEntrada_MOEDA.ClientID);
			else
				SomaValorSelecionado();
			txtValorEntrada_MOEDA.Text = mValorEntrada.ToString("N");
		}

		protected void txtDataParcela1_DATA_TextChanged(object sender, System.EventArgs e)
		{
			DateTime mDataParcela1 = DateTime.Parse(txtDataParcela1_DATA.Text);
			if(mDataParcela1 < DateTime.Today)
			{
				this.MessageBox.Show("Não é permitido data da primeira parcela inferior a data de Hoje",txtDataParcela1_DATA.ClientID);
				this.txtDataParcela1_DATA.Text = DateTime.Today.ToString("dd/MM/yyyy");
			}
			SomaValorSelecionado();

		}

		private void SomaValorSelecionado(Anthem.CheckBox pckbHeader)
		{
			double mValorTotalSelecionado = 0;
			double mValorParcela = 0;
			double mValorTotalFinanciamento = 0;
			double mValorEntrada = double.Parse(txtValorEntrada_MOEDA.Text);
            double? mJuros = null;
			int mDiasEntreParcela = int.Parse(txtDiasParcela.Text);
			int mTotalParcelas = int.Parse(txtTotParcela_INT.Text);
			DateTime mDataParcela1 = DateTime.Parse(txtDataParcela1_DATA.Text);
			DateTime mDataUltimaParcela = mDataParcela1;
            if (!string.IsNullOrEmpty(txtPercJuros_MOEDA.Text))
            {
                mJuros = Convert.ToDouble(txtPercJuros_MOEDA.Text);
                if (mJuros.Value == this.obj.Juros)
                    mJuros = null;
            }
			foreach (DataGridItem mDGI in dgSnapDividas.Items)
			{
				Anthem.CheckBox mCkb = (Anthem.CheckBox)mDGI.Cells[0].FindControl("ckbItem");
				if (pckbHeader != null)
				{
					mCkb.Checked = pckbHeader.Checked;
					mCkb.UpdateAfterCallBack = true;
				}
				if (mCkb.Checked)
				{
					mValorTotalSelecionado += double.Parse(mDGI.Cells[8].Text);
					this.ContratoSelecionado = true;
				}
			}
			if (mValorTotalSelecionado == 0) this.ContratoSelecionado=false;
			obj.CalculaParcelamento(mDataParcela1,mTotalParcelas,mDiasEntreParcela,mValorTotalSelecionado,mValorEntrada,ref mValorTotalFinanciamento,ref mValorParcela, ref mDataUltimaParcela, mJuros);
			txtValorParcela_MOEDA.Text = mValorParcela.ToString("N");
			lblTotalParcelamento.Text = mValorTotalFinanciamento.ToString("N");
			lblDtaParcelaFinal.Text = mDataUltimaParcela.ToString("dd/MM/yyyy");
		}

		private void SomaValorSelecionado()
		{
			this.SomaValorSelecionado(null);
		}
		private bool ContratoSelecionado
		{
			set
			{
				this.txtTotParcela_INT.ReadOnly = !value;
				this.txtDiasParcela.ReadOnly = !value;
				this.txtValorEntrada_MOEDA.ReadOnly = !value;
				this.txtDataParcela1_DATA.ReadOnly = !value;
			}
		}
	}
}

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
	/// Summary description for funcionarios_cadastro.
	/// </summary>
	public partial class funcionarios_cadastro : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton imgBtnCancelar;
		private Sistema Sistema;
		private LimFuncionarios obj;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			NBFuncoes.ValidarSistema(this, ref this.Sistema,this.MessageBox);
			obj = Sistema.LimFuncionarios;
			if (!IsPostBack)
			{
				this.Sistema.ValidaCredencial(Permissao.Administrador);
				MessageBox.MoverFoco(txtNomeFuncionario_STR.ClientID);
				try
				{
					string idFunc = Request.QueryString["IDFunc"].ToString();
					MostraDados(idFunc);
				}
				catch{}

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
			this.MessageBox.YesChoosed += new NBWebControls.YesChoosedEventHandler(this.MessageBox_YesChoosed);
			this.MessageBox.NoChoosed += new NBWebControls.NoChoosedEventHandler(this.MessageBox_NoChoosed);
			this.imgBtnSalvar.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSalvar_Click);

		}
		#endregion

		private void imgBtnSalvar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{

				//Dados Pessoais do Funcionário
				obj.Entidade.PessoaFisica = true;

				obj.Entidade.NomeRazaoSocial_key = txtNomeFuncionario_STR.Text;
				if (txtDataNascimento_DATA.Value!="")
					obj.Entidade.dtNascimentoInicioAtividades = DateTime.Parse(txtDataNascimento_DATA.Value);
				try
				{
					obj.Entidade.CPFCNPJ_key = txtCPF_CPF.Text;
				}
				catch
				{
					obj.Entidade.CPFCNPJ_key = "0";
				}
				obj.Entidade.RgIE = txtRG_RG.Value;
				obj.Entidade.TextoRespeito = txaAnotacoesAdicionais_TUD.Value;
				
				//Endereço
				if (!obj.isAlteracao)
					obj.Endereco.Clear_vars();
				obj.Endereco.Logradouro_key = txtEndereco_TUD.Value;
				obj.Endereco.complemento = txtComplemento_TUD.Value;
				obj.Endereco.Bairro = txtBairro_TUD.Value;
				obj.Endereco.CEP = txtCEP_CEP.Value;
				obj.Endereco.Municipio = txtCidade_STR.Value;
				obj.Endereco.UF = selUF.Value;
				obj.Endereco.Principal = true;
				
				//Telefone Residencial
				if(this.txtTelefoneResidencial_FONE.Value != "")
				{
					if (!obj.isAlteracao)
						obj.Telefone.Clear_vars();
					obj.Telefone.Descricao = "Res";
					obj.Telefone.DDD_key = txtDDD_INT.Value;
					obj.Telefone.Fone_key= txtTelefoneResidencial_FONE.Value;
				}
			
				//Telefone Celular
				if (this.txtCelular_FONE.Value != "")
				{
					if (!obj.isAlteracao)
						obj.Celular.Clear_vars();
					obj.Celular.Descricao = "Cel";
					obj.Celular.DDD_key = txtDDD_INT.Value;
					obj.Celular.Fone_key = txtCelular_FONE.Value;
				}
			
				//Email
				if (this.txtEmail_EMAIL.Value != "")
				{
					if (!obj.isAlteracao)
						obj.Email.Clear_vars();
					obj.Email.Descricao = "e-mail Pessoal";
					obj.Email.eMail_key = txtEmail_EMAIL.Value;
				}

				obj.UsuarioConfig.Funcao = txtFuncao_TUD.Value;
				obj.UsuarioConfig.Credencial = byte.Parse(rblCredencial.SelectedValue);
				obj.Usuario.login_key = txtUsuario_STR.Value;
				if (!(obj.isAlteracao && txtSenha_TUD.Value==""))
					obj.Usuario.senha = txtSenha_TUD.Value;
				obj.UsuarioConfig.UsuarioAtivo = Convert.ToBoolean(int.Parse(rblFuncionarioAtivo.SelectedValue));
				
				obj.Salvar();

				//Verifica se é uma alteração ou inclusão
				if (obj.isAlteracao)
					MessageBox.ShowConfirma(obj.Source + "\\r\\rA Alteração desse Registro foi Salva Com Sucesso, deseja continuar incluindo novos Registros? ","NovoRegistro",true,true);
				else
					//Neste caso é uma inclusão o sistema já é direcionado para uma no inclusão.
					MessageBox.ShowConfirma(obj.Source + "\\r\\rFuncionário Salvo com Sucesso, Deseja Continuar Incluindo?","NovoRegistro",true,true);
			}
			catch(NBdbm.NBexception nbEx)
			{
				NBdbm.EVTexception evtEx = new NBdbm.EVTexception(nbEx);
				MessageBox.Show(evtEx.Message);
			}
			catch(NBdbm.EVTexception evtEx)
			{
				MessageBox.Show(evtEx.Message);
			}
			catch(Exception ex)
			{

				MessageBox.Show(ex.Message);
			}
		}
		private void MostraDados(string idfunc)
		{
			if (idfunc=="0" || idfunc=="")
				return;
			
			obj.Consulta(idfunc);

			#region Dados Pessoais
			txtNomeFuncionario_STR.Text = obj.Entidade.NomeRazaoSocial_key;
			try
			{
				txtDataNascimento_DATA.Value = DateTime.Parse(obj.Entidade.dtNascimentoInicioAtividades.ToString()).ToString("dd/MM/yy");
			}
			catch
			{
				txtDataNascimento_DATA.Value = "";
			}
			txtRG_RG.Value = obj.Entidade.RgIE;
			txaAnotacoesAdicionais_TUD.Value = obj.Entidade.TextoRespeito;
			try
			{
				txtCPF_CPF.Text = obj.Entidade.CPFCNPJ_key;
			}
			catch
			{
				txtCPF_CPF.Text = "";
			}

			#region Usuário e Usuário Config
			txtUsuario_STR.Value = obj.Usuario.login_key;
			txtSenha_TUD.Value = obj.Usuario.senha;
			txtFuncao_TUD.Value = obj.UsuarioConfig.Funcao;
			rblFuncionarioAtivo.SelectedValue = Convert.ToInt16(obj.UsuarioConfig.UsuarioAtivo).ToString();
			rblCredencial.SelectedValue = obj.UsuarioConfig.Credencial.ToString();
			#endregion
			#endregion

			#region Endereco
			txtEndereco_TUD.Value = obj.Endereco.Logradouro_key;
			txtComplemento_TUD.Value = obj.Endereco.complemento;
			txtBairro_TUD.Value = obj.Endereco.Bairro;
			txtCEP_CEP.Value = obj.Endereco.CEP;
			txtCidade_STR.Value = obj.Endereco.Municipio; 
			selUF.Value = obj.Endereco.UF;
			#endregion

			#region Telefone e Celular
			txtDDD_INT.Value = obj.Telefone.DDD_key;
			txtTelefoneResidencial_FONE.Value = obj.Telefone.Fone_key;
			txtCelular_FONE.Value = obj.Celular.Fone_key;
			#endregion

			#region Endereco Eletronico
			txtEmail_EMAIL.Value = obj.Email.eMail_key;
			#endregion

		}

		private void MessageBox_NoChoosed(object sender, string Key)
		{
			switch(Key)
			{
				case "NovoRegistro":
					MessageBox.ModalClose();
					break;
				case "NomeJaCadastrado":
					MessageBox.MoverFoco(txtDataNascimento_DATA.ClientID);
					break;
			}
		
		}

		private void MessageBox_YesChoosed(object sender, string Key)
		{
			switch(Key)
			{
				case "NovoRegistro":
                    this.obj.NovoFuncionario();
					Response.Redirect("funcionarios_cadastro.aspx");
					break;
				case "NomeJaCadastrado":
					string tmpIdEntidade = this.obj.IdEntidadeDuplicada;
					this.obj.IdEntidadeDuplicada = "0";
					Response.Redirect("funcionarios_cadastro.aspx?idFunc=" + tmpIdEntidade);
					break;
			}
		
		
		}

		protected void txtNomeFuncionario_STR_TextChanged(object sender, System.EventArgs e)
		{
			if (!obj.NomeEntidadeJaCadastrada(txtNomeFuncionario_STR.Text,MessageBox))
				MessageBox.MoverFoco(txtDataNascimento_DATA.ClientID);
		}

	}
}

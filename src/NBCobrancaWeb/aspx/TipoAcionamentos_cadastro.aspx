<%@ Page language="c#" Codebehind="TipoAcionamentos_cadastro.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.aspx.TipoAcionamentos_cadastro" %>
<%@ Register TagPrefix="cc2" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Register TagPrefix="componentart" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>.:: Neobridge Controle de Cobrança Web ::..</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/links.css" rel="stylesheet">
		<LINK href="../estilos/texto.css" rel="stylesheet">
		<script language="JavaScript" src="../scripts/navegador_css.js"></script>
		<script language="JavaScript" src="../scripts/mostra_esconde_campos.js"></script>
		<script language="JavaScript" src="../scripts/trata_campo.js"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" marginwidth="0" marginheight="0">
		<form id="frmTipoAcionamentosCadastro" method="post" runat="server">
			<cc2:messagebox id="MessageBox" runat="server" PastaStyles="../estilos" PastaScripts="../scripts"
				Altura="80" Largura="100"></cc2:messagebox>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr align="right" height="52">
					<td vAlign="top" colSpan="2" height="52">
						<table cellSpacing="0" cellPadding="0" width="100%" background="../imagens/tela_peq_topo_meio.jpg"
							border="0">
							<tr>
								<td style="BACKGROUND-IMAGE: url(../imagens/tela_peq_topo_left.jpg); BACKGROUND-REPEAT: no-repeat"
									height="52">
									<table height="34" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td vAlign="top" width="30">&nbsp;</td>
											<td class="titulo" vAlign="top"><%=this.TituloCadastro%></td>
										</tr>
									</table>
								</td>
								<td style="BACKGROUND-POSITION-X: right; BACKGROUND-IMAGE: url(../imagens/tela_peq_topo_right.jpg); BACKGROUND-REPEAT: no-repeat"
									width="55"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="top" style="BACKGROUND-POSITION-X: left; BACKGROUND-IMAGE: url(../imagens/tela_peq_f_lateral.jpg); BACKGROUND-REPEAT: repeat-y">
						<!-- Conteúdo da Janela de Cadastro -->
						<table class="txt" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td width="10">&nbsp;</td>
								<td width="130" align="right">Descrição:</td>
								<td><asp:textbox id="txtDescricao" Width="100%" Runat="server"></asp:textbox></td>
							</tr>
							<tr>
								<td width="10">&nbsp;</td>
								<td width="130" align="right">Dias para Reacionamento:</td>
								<td><asp:textbox id="txtDiasReacionamento_INT" Width="50px" Runat="server"></asp:textbox></td>
							</tr>
							<tr>
								<td width="10">&nbsp;</td>
								<td width="130" align="right">Credencial Exigida:</td>
								<td><asp:radiobuttonlist id="rblCredencial" runat="server" BorderWidth="0px" BorderStyle="None" CssClass="txt"
										CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal">
										<asp:ListItem Value="1" Selected="True">Acionador ou Maior</asp:ListItem>
										<asp:ListItem Value="2">Encarregado ou Maior</asp:ListItem>
										<asp:ListItem Value="3">Administrador</asp:ListItem>
									</asp:radiobuttonlist>
								</td>
							</tr>
						</table>
						<!-- Inicio da tabela de Botões --><br>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr align="center">
								<td><asp:imagebutton id="imgBtnSalvar" height="18" width="71" ImageUrl="../imagens/botoes/btn_salvar.gif"
										Runat="server" BorderStyle="None"></asp:imagebutton></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

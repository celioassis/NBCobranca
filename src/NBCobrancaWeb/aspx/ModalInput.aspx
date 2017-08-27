<%@ Register TagPrefix="cc1" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Page language="c#" Codebehind="ModalInput.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.aspx.ModalInput" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ModalInput</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../estilos/links.css" rel="stylesheet">
		<link href="../estilos/texto.css" rel="stylesheet">
		<script language="JavaScript" src="../scripts/navegador_css.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0"
		marginwidth="0" marginheight="0">
		<form id="frmModalInput" method="post" runat="server">
			<cc1:MessageBox id="MsgBox" runat="server" PastaScripts="../Scripts" PastaStyles="../Styles"></cc1:MessageBox>
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
											<td class="titulo" vAlign="top"><%=this.Titulo%></td>
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
								<td align="center">
									<asp:Label id="lblHelp" runat="server"></asp:Label></td>
							</tr>
						</table>
						<table class="txt" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td width="5%">&nbsp;</td>
								<td width="30%">
									<asp:Label id="lblDescricao" runat="server"></asp:Label>:
								</td>
								<td><asp:textbox id="txtDescricao" Width="100%" Runat="server"></asp:textbox></td>
							</tr>
						</table>
						<!-- Inicio da tabela de Botões --><br>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr align="center">
								<td><asp:imagebutton id="imgBtnOk" height="18" width="35px" ImageUrl="../imagens/botoes/btn_ok.jpg" Runat="server"
										BorderStyle="None"></asp:imagebutton></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

<%@ Register TagPrefix="uc1" TagName="NovoRodape" Src="../ascx/NovoRodape.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NovoCabecalho" Src="../ascx/NovoCabecalho.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Page language="c#" Codebehind="senha_editar.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.aspx.senha_edicao" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>.:: Neobridge Controle de Cobrança Web - Funcionários</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../scripts/verifica_senha.js"></script>
	</HEAD>
	<body>
		<form id="formSenhaEditar" action="" method="post" runat="server">
			<cc1:MessageBox id="MessageBox" runat="server" PastaScripts="../scripts" PastaStyles="../estilos"></cc1:MessageBox>
			<uc1:NovoCabecalho id="NovoCabecalho1" runat="server"></uc1:NovoCabecalho>
			<div id="fundoLogin" style="BORDER-RIGHT: lightslategray 1px solid; PADDING-RIGHT: 12px; BORDER-TOP: lightslategray 1px solid; PADDING-LEFT: 12px; PADDING-BOTTOM: 12px; BORDER-LEFT: lightslategray 1px solid; WIDTH: 220px; PADDING-TOP: 12px; BORDER-BOTTOM: lightslategray 1px solid; BACKGROUND-COLOR: #f6f6f6">
				<b>
					<asp:label CssClass="txt" id="lblUsuario" runat="server">Nome do usuário</asp:label>
				</b>
				<br>
				<br>
				<div class="txt" style="FLOAT: left; LINE-HEIGHT: 22px">Senha atual<br>
					Nova senha<br>
					Confirmar senha
				</div>
				<div style="FLOAT: left; MARGIN: 2px; WIDTH: 120px; TEXT-ALIGN: right">
					<asp:textbox id="txtSenhaAtual_TUD" style="MARGIN: 1px" runat="server" TextMode="Password"></asp:textbox><br>
					<asp:textbox id="txtSenha_TUD" onblur="senhaCaracteres(this)" style="MARGIN: 1px" runat="server"
						TextMode="Password"></asp:textbox><br>
					<asp:textbox id="txtSenhaRepetir_TUD" onblur="senhaConfirma(this)" style="MARGIN: 1px" runat="server"
						TextMode="Password"></asp:textbox><br>
					<asp:imagebutton id="imgBtnEntrar" runat="server" BorderStyle="None" height="18px" width="35px" ImageUrl="../imagens/botoes/btn_ok.jpg"></asp:imagebutton>
				</div>
			</div>
			<uc1:NovoRodape id="NovoRodape1" runat="server"></uc1:NovoRodape>
		</form>
	</body>
</HTML>

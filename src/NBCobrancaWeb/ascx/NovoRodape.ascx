<%@ Control Language="c#" AutoEventWireup="True" Codebehind="NovoRodape.ascx.cs" Inherits="NBCobranca.ascx.NovoRodape" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
</div>
<div id="Rodape">
	<div id="RodCantoEsq"><img src="../imagens/layout/cbe.jpg"></div>
	<div id="RodCantoDir"><img src="../imagens/layout/cbd.jpg"></div>
	<div id="RodMeio">
		<div id="Versao">
			<asp:Label ID="lblVersao" Runat="server" Font-Bold="True" Font-Size="Smaller"></asp:Label>
		</div>
	</div>
	<div id="user">
		<asp:HyperLink id="hlUsuario" runat="server" ToolTip="Clique aqui para alterar sua Senha" NavigateUrl="../aspx/senha_editar.aspx"></asp:HyperLink>
	</div>
	<div id="LogoNeobridge">
		<a href="http://www.neobridge.com.br" target="_blank"><img src="../imagens/layout/copyright.jpg" border="0"></a>
	</div>
</div>

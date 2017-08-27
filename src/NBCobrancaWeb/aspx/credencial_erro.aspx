<%@ Register TagPrefix="uc1" TagName="NovoRodape" Src="../ascx/NovoRodape.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NovoCabecalho" Src="../ascx/NovoCabecalho.ascx" %>
<%@ Page language="c#" Codebehind="credencial_erro.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.aspx.credencial_erro" %>
<form id="Form1" method="post" runat="server">
	<uc1:NovoCabecalho id="NovoCabecalho1" runat="server"></uc1:NovoCabecalho>
	<div id="fundoCredencial">
		<table cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td align=center>
					<img src="../imagens/exclamacao.jpg" border="0">
					<br>
					Usuário sem credencial para acessar esta página.
				</td>
			</tr>
		</table>
	</div>
	<uc1:NovoRodape id="NovoRodape1" runat="server"></uc1:NovoRodape>
</form>

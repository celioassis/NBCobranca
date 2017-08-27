<%@ Page language="c#" Codebehind="indisponivel_erro.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.aspx.indisponivel_erro" %>
<%@ Register TagPrefix="uc1" TagName="NovoCabecalho" Src="../ascx/NovoCabecalho.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NovoRodape" Src="../ascx/NovoRodape.ascx" %>
<form id="Form1" method="post" runat="server">
	<uc1:NovoCabecalho id="NovoCabecalho" runat="server"></uc1:NovoCabecalho>
	<div id="fundoIndisponivel">
		<table cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td align="center">
					<img src="../imagens/exclamacao.jpg" border="0">
					<br>
					<b>Servidor indisponível no momento.</b>
					<br>Clique em Voltar e tente novamente, se o 
					problema percistir contate o Suporte Técnico.
					<br>
					<br>
				</td>
			</tr>
			<tr>
				<td align="center"><INPUT class="Botao" style="WIDTH:60px" type="button" value="Voltar" onclick="javascript:history.go(-1);"></td>
			</tr>
		</table>
	</div>
	<uc1:NovoRodape id="NovoRodape1" runat="server"></uc1:NovoRodape>
</form>

<%@ Page Language="C#" MasterPageFile="~/view/BootStrap.Master" AutoEventWireup="true" CodeBehind="credencial_erro.aspx.cs" Inherits="NBCobranca.view.credencial_erro" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phConteudo" runat="server">
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
</asp:Content>

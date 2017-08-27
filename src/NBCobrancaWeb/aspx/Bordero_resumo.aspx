<%@ Page language="c#" Codebehind="Bordero_resumo.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.aspx.Bordero_resumo" %>
<%@ Register TagPrefix="NBWebControls" Namespace="NBWebControls" Assembly="NBWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>.:: Neobridge Controle de Cobrança Web - Resumo do Bordero de Repasse ::..</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/links.css" rel="stylesheet">
		<LINK href="../estilos/texto.css" rel="stylesheet">
		<script language="JavaScript" src="../scripts/navegador_css.js"></script>
		<script language="JavaScript" src="../scripts/trata_campo.js"></script>
</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" marginheight="0" marginwidth="0">
		<form id="frmBordero_Resumo" method="post" runat="server">
			<NBWEBCONTROLS:MESSAGEBOX id="MessageBox" runat="server" PastaScripts="../scripts" PastaStyles="../estilos"
				ManterScroll="True" Altura="150" Largura="500"></NBWEBCONTROLS:MESSAGEBOX>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr align="right" height="52">
					<td vAlign="top" colSpan="3" height="52">
						<table cellSpacing="0" cellPadding="0" width="100%" background="../imagens/tela_peq_topo_meio.jpg"
							border="0">
							<tr>
								<td style="BACKGROUND-IMAGE: url(../imagens/tela_peq_topo_left.jpg); BACKGROUND-REPEAT: no-repeat"
									height="52">
									<table height="34" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td vAlign="top" width="30">&nbsp;</td>
											<td class="titulo" vAlign="top">Resumo do Bordero</td>
										</tr>
									</table>
								</td>
								<td style="BACKGROUND-POSITION-X: right; BACKGROUND-IMAGE: url(../imagens/tela_peq_topo_right.jpg); BACKGROUND-REPEAT: no-repeat"
									width="55"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" width="12" background="../imagens/tela_peq_f_lateral.jpg"><IMG height="25" src="../imagens/tela_peq_f_lateral.jpg" width="12"></td>
					<td vAlign="top" width="100%" colSpan="2">
						<table class="txt" cellSpacing="2" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="5%">Linha:</td>
								<td width="5%"><asp:textbox id="txtLinha_INT" Runat="server" MaxLength="2" ReadOnly="True"></asp:textbox></td>
								<td width="20%">Descrição do Resumo:</td>
								<td width="50%"><asp:textbox id="txtDescResumo" Runat="server" Width="100%" ReadOnly="True"></asp:textbox></td>
								<td width="5%">Valor:</td>
								<td width="15%"><asp:textbox id="txtValor_MOEDA" Runat="server" ReadOnly="True"></asp:textbox></td>
							</tr>
							<tr><td colspan=6><br></td></tr>
							<tr>
								<td align="center" colSpan="6"><asp:button id="btnNovo" Runat="server" Width="60px" Text="Novo" onclick="btnNovo_Click"></asp:button>&nbsp;
									<asp:button id="btnSalvar" Runat="server" Width="60px" Text="Salvar" Visible="False" onclick="btnSalvar_Click"></asp:button></td>
							</tr>
							<tr><td colspan=6><br></td></tr>
							<tr>
								<td colSpan="6">
									<asp:datagrid id="dgResumo" runat="server" Width="100%" CellPadding="2" BorderWidth="1px" BorderColor="#DDE4DA"
										AutoGenerateColumns="False" GridLines="Vertical" AllowCustomPaging="True" Caption="<h4>Lista das Itens do Resumo</h4>">
<AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3">
</AlternatingItemStyle>

<ItemStyle CssClass="dg_item" BackColor="#E9EFDD">
</ItemStyle>

<HeaderStyle CssClass="dg_header" BackColor="YellowGreen">
</HeaderStyle>

<Columns>
<asp:BoundColumn DataField="NumeroLinha" HeaderText="Linha" DataFormatString="{0:000}">
<HeaderStyle Width="30px">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="Descricao" HeaderText="Descri&#231;&#227;o do Item do Resumo">
<HeaderStyle HorizontalAlign="Left">
</HeaderStyle>
</asp:BoundColumn>
<asp:BoundColumn DataField="Valor" HeaderText="Valor" DataFormatString="{0:N}">
<ItemStyle HorizontalAlign="Right" Width="70px">
</ItemStyle>
</asp:BoundColumn>
<asp:TemplateColumn HeaderText="COMANDOS">
<HeaderStyle HorizontalAlign="Center" ForeColor="Black" BackColor="Silver">
</HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="60px">
</ItemStyle>

<ItemTemplate>
<asp:ImageButton id=imgBtnEditar Runat="server" Width="16px" Height="16px" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NumeroLinha") %>' ImageUrl="../imagens/botoes/c_editar.gif" CommandName="Editar" BorderStyle="None">
													</asp:ImageButton>
<asp:ImageButton id=imgBtnApagar Runat="server" Width="16px" Height="16px" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NumeroLinha") %>' ImageUrl="../imagens/botoes/c_remover.gif" CommandName="Apagar" BorderStyle="None">
													</asp:ImageButton>
</ItemTemplate>
</asp:TemplateColumn>
</Columns>

<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages">
</PagerStyle>
									</asp:datagrid></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

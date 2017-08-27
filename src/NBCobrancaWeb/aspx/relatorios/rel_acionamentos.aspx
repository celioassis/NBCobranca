<%@ Page language="c#" Codebehind="rel_acionamentos.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.aspx.relatorios.rel_acionamentos" EnableSessionState="True" enableViewState="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>relatorio</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK media="screen" href="tela.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="impressora.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="orcRelatorios" method="post" runat="server">
			<center>
				<div id="conteudo">
					<table cellSpacing="0" cellPadding="0" width="100%" border="0">
						<tr>
							<td align="center"><asp:image id="cabCoest" ImageUrl="../../imagens/cab_coest.gif" Runat="server"></asp:image><br>
								<br>
							</td>
						</tr>
						<tr>
							<td align="center">
								<h3>Relatório de Acionamentos</h3>
							</td>
						</tr>
						<tr>
							<td>
								<h4>
									<asp:Label id="lblPeriodo" Runat="server"></asp:Label>
								</h4>
							</td>
						</tr>
						<tr>
							<td>
								<asp:datagrid id="dgDados" runat="server" Width="100%" EnableViewState="False" GridLines="None"
									Font-Size="6pt" ForeColor="Black" BackColor="White" BorderStyle="None" AllowCustomPaging="True"
									AutoGenerateColumns="False" CellPadding="2" ShowFooter="True">
									<FooterStyle Font-Size="7pt" BackColor="Gray"></FooterStyle>
									<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#000099"></SelectedItemStyle>
									<AlternatingItemStyle BackColor="#CCCCCC"></AlternatingItemStyle>
									<HeaderStyle Font-Bold="True" ForeColor="Black" Font-Size="7pt" CssClass="dg_header" BackColor="Gray"></HeaderStyle>
									<Columns>
										<asp:BoundColumn DataField="NomePrimary" HeaderText="&#160;Nome Devedor"></asp:BoundColumn>
										<asp:BoundColumn DataField="TipoDivida" HeaderText="&#160;Tipo da D&#237;vida">
											<HeaderStyle Width="140px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="TipoAcionamento" HeaderText="&#160;Tipo do Ultimo Acionamento">
											<HeaderStyle Width="180px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="DataPromessa" HeaderText="&#160;Promessa" DataFormatString="{0:dd/MM/yyy}">
											<HeaderStyle Width="70px"></HeaderStyle>
										</asp:BoundColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" CssClass="dg_header"
										Wrap="False"></PagerStyle>
								</asp:datagrid>
							</td>
						</tr>
					</table>
				</div>
			</center>
		</form>
	</body>
</HTML>

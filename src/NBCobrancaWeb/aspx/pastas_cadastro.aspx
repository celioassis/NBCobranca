<%@ Register TagPrefix="componentart" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Register TagPrefix="uc1" TagName="NovoRodape" Src="../ascx/NovoRodape.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NovoCabecalho" Src="../ascx/NovoCabecalho.ascx" %>
<%@ Page language="c#" Codebehind="pastas_cadastro.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.aspx.pastas_cadastro" %>

<HTML>
  <HEAD>
		<title>.:: Neobridge Controle de Cobrança Web - Gerenciador de pastas ::.</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
  </HEAD>
	<body>
		<form id="frmItensEPClasses" action="" method="post" runat="server">
			<uc1:NovoCabecalho id="NovoCabecalho1" runat="server"></uc1:NovoCabecalho>
			<cc1:MessageBox id="MessageBox" runat="server" PastaScripts="../scripts" PastaStyles="../estilos"></cc1:MessageBox>
			<div id="borda" style="BORDER-RIGHT:medium none; BORDER-TOP:medium none; BORDER-LEFT:medium none; BORDER-BOTTOM:medium none">
				<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr height="100%">
						<td vAlign="top" width="100%">
							<table cellSpacing="0" cellPadding="2" width="99%" border="0">
								<tr>
									<td class="txt" vAlign="top">
										<table cellSpacing="1" cellPadding="0" width="99%" bgColor="#cccccc" border="0">
											<tr>
												<td class="dg_header" bgColor="#336666">&nbsp;Pastas</td>
											</tr>
											<tr>
												<td bgColor="#ffffff">
													<COMPONENTART:TREEVIEW id="TreeView1" runat="server" Width="100%" Height="320px" ShowLines="True" SelectedNodeCssClass="SelectedTreeNode"
														CssClass="TreeView" CutNodeCssClass="TreeNode" HoverNodeCssClass="HoverTreeNode" LineImageHeight="20"
														LineImageWidth="19" LineImagesFolderUrl="../imagens/arvore/lines/" ParentNodeImageUrl="../imagens/arvore/folders.gif"
														NodeLabelPadding="3" DefaultImageHeight="16" DefaultImageWidth="16" NodeCssClass="TreeNode" LeafNodeImageUrl="../imagens/arvore/folder.gif"></COMPONENTART:TREEVIEW></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table id="btnGerenciador" cellSpacing="0" cellPadding="0" width="99%" border="0">
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="2" width="100" border="0">
											<tr>
												<td><A href="javascript:ClasseBtnClick('txtNovaClasse','novo','nova_classe','btnGerenciador')"><IMG height="18" src="../imagens/botoes/btn_novo.gif" width="71" border="0"></A></td>
												<td><A href="javascript:ClasseBtnClick('txtRenomearClasse','renomear','renomear_classe','btnGerenciador')"><IMG height="18" src="../imagens/botoes/btn_renomear.gif" width="71" border="0"></A></td>
												<td><asp:imagebutton id="imgBtnExcluir" BorderStyle="None" ImageUrl="../imagens/botoes/btn_excluir.gif"
														Runat="server" width="71" height="18"></asp:imagebutton></td>
												<td align="center">&nbsp;&nbsp;</td>
												<td><asp:imagebutton id="imgBtnSalvarNoBanco" BorderStyle="None" ImageUrl="../imagens/botoes/btn_salvar.gif"
														Runat="server" width="71" height="18"></asp:imagebutton></td>
												<td><asp:imagebutton id="imgBtnCancelar" BorderStyle="None" ImageUrl="../imagens/botoes/btn_cancelar.gif"
														Runat="server" width="71" height="18"></asp:imagebutton></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<!-- Adicionar -->
							<table id="nova_classe" style="DISPLAY:none; disabled:false" cellSpacing="1" cellPadding="1"
								width="98%" border="0">
								<tr>
									<td vAlign="top" width="410"><input id="txtNovaClasse" onblur="if (this.value=='') {this.value='Digite o nome da classe'}"
											onfocus="if (this.value=='Digite o nome da classe') {this.value = ''}" type="text" value="Digite o nome da pasta"
											name="txtNovaClasse" runat="server"></td>
									<td><asp:imagebutton id="imgBtnAdicionar" BorderStyle="None" ImageUrl="../imagens/botoes/btn_adicionar.gif"
											Runat="server" width="71" height="18"></asp:imagebutton>
										<A href="javascript:TabelaFJMostraEsconde('btnGerenciador','nova_classe')"><IMG height="18" src="../imagens/botoes/btn_cancelar.gif" width="71" border="0"></A></td>
								</tr>
							</table>
							<!-- Renomear -->
							<table id="renomear_classe" style="DISPLAY:none; disabled:false" cellSpacing="1" cellPadding="1"
								width="98%" border="0">
								<tr>
									<td vAlign="top" width="410"><input id="txtRenomearClasse" type="text" name="txtRenomearClasse" runat="server"></td>
									<td><asp:imagebutton id="imgBtnRenomear" BorderStyle="None" ImageUrl="../imagens/botoes/btn_renomear.gif"
											Runat="server" width="71" height="18"></asp:imagebutton>
										<A href="javascript:TabelaFJMostraEsconde('btnGerenciador','renomear_classe')"><IMG height="18" src="../imagens/botoes/btn_cancelar.gif" width="71" border="0"></A></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>
			<uc1:NovoRodape id="NovoRodape1" runat="server"></uc1:NovoRodape>
		</form>
	</body>
</HTML>

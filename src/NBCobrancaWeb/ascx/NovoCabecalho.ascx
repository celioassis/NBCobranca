<%@ Register TagPrefix="componentart" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="NovoCabecalho.ascx.cs" Inherits="NBCobranca.ascx.NovoCabecalho" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<LINK href="../estilos/layout.css" type="text/css" rel="stylesheet">
<LINK href="../estilos/texto.css" type="text/css" rel="stylesheet">
<LINK href="../estilos/menuStyle.css" type="text/css" rel="stylesheet">
<LINK href="../estilos/gridStyle.css" type="text/css" rel="stylesheet">
<script src="../scripts/genericos.js" type="text/javascript"></script>
<script src="../scripts/navegador_css.js" type="text/javascript"></script>
<script src="../scripts/trata_campos_pesquisa.js" type="text/javascript"></script>
<script src="../scripts/mostra_esconde_campos.js" type="text/javascript"></script>
<script language="JavaScript" src="../scripts/AnthemCallBack.js"></script>
<LINK href="../estilos/treeStyle.css" type="text/css" rel="stylesheet">
<LINK href="../estilos/mapa.css" type="text/css" rel="stylesheet">
<LINK href="../estilos/pesquisa.css" type="text/css" rel="stylesheet">
<LINK href="../estilos/tooltip.css" type="text/css" rel="stylesheet">
<div id="Limite">
	<div id="Cabecalho">
		<div id="CabCantoEsquerdo"></div>
		<div id="CabCantoDireito"><IMG src="../imagens/layout/ccd.jpg"></div>
		<div id="menu">
			<div id="imgsair"><asp:HyperLink id="hlSair" runat="server" ToolTip="Efetuar Logoff" NavigateUrl="../aspx/login.aspx"></asp:HyperLink></div>
			<div id="imghelp"><A href="../help/Sistema_de_Controle_de_Eventos.htm"></A></div>
			<div style="MARGIN-LEFT: auto; WIDTH: 54%; MARGIN-RIGHT: auto; PADDING-TOP: 18px">
				<asp:image id="imgBarraMenu" ImageUrl="../imagens/layout/imgBarraMenu.gif" Runat="server" Visible="False"></asp:image>
				<componentart:menu id="Menu1" Width="200px" CssClass="TopGroup" ScrollDownLookId="DefaultItemLook"
					ScrollUpLookId="DefaultItemLook" ExpandDelay="100" EnableViewState="False" ImagesBaseUrl="../imagens/menu/"
					DefaultGroupItemSpacing="0px" TopGroupItemSpacing="0px" DefaultItemLookID="DefaultItemLook" DefaultSubGroupExpandOffsetY="-5"
					DefaultSubGroupExpandOffsetX="-7" DefaultGroupCssClass="MenuGroup" ClientObjectId="_ctl0_Menu1"
					ClientTarget="Auto" runat="server">
					<ItemLooks>
						<componentart:ItemLook HoverCssClass="TopMenuItemHover" LabelPaddingTop="2px" LabelPaddingRight="12px"
							LabelPaddingBottom="4px" LabelPaddingLeft="12px" LookId="TopItemLook" CssClass="TopMenuItem"></componentart:ItemLook>
						<componentart:ItemLook HoverCssClass="MenuItemHover" LabelPaddingTop="3px" ActiveCssClass="MenuItemExpanded"
							LabelPaddingRight="12px" LabelPaddingBottom="4px" ExpandedCssClass="MenuItemExpanded" LabelPaddingLeft="18px"
							LookId="DefaultItemLook" CssClass="MenuItem"></componentart:ItemLook>
						<componentart:ItemLook HoverCssClass="MenuItemHover" LabelPaddingTop="3px" RightIconUrl="arrow.gif" LabelPaddingRight="12px"
							LabelPaddingBottom="4px" RightIconWidth="20px" ExpandedCssClass="MenuItemExpanded" LabelPaddingLeft="18px"
							LookId="ExpandableItemLook" CssClass="MenuItem"></componentart:ItemLook>
						<componentart:ItemLook ImageUrl="break.gif" ImageWidth="100%" ImageHeight="2px" LookId="BreakItem" CssClass="MenuBreak"></componentart:ItemLook>
					</ItemLooks>
					<Items>
						<componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup" ID="mnuCadastros"
							Text="Cadastros" DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup" LookId="TopItemLook">
							<componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup" ID="mnuCadClientes"
								Text="Clientes" DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup" NavigateUrl="clientesfornecedores.aspx?TipoEntidade=2"></componentart:MenuItem>
							<componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup" Text="Funcion&#225;rios"
								DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup" NavigateUrl="funcionarios.aspx"></componentart:MenuItem>
							<componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup" Text="Devedores" DefaultSubGroupItemSpacing="0px"
								SubGroupCssClass="MenuGroup" NavigateUrl="devedores.aspx"></componentart:MenuItem>
							<componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup" Text="Tipos de Divida"
								DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup" NavigateUrl="TipoDividas.aspx"></componentart:MenuItem>
							<componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup" Text="Tipos de Acionamento"
								DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup" NavigateUrl="TipoAcionamentos.aspx"></componentart:MenuItem>
						</componentart:MenuItem>
						<componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup" Text="Lan&#231;amentos"
							DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup" LookId="TopItemLook">
							<componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup" Text="Acionamentos"
								DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup" NavigateUrl="Acionamento_pesquisa.aspx"></componentart:MenuItem>
						</componentart:MenuItem>
						<componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup" Text="Relat&#243;rios"
							DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup" LookId="TopItemLook">
							<componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup" Text="Acionamentos"
								DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup" NavigateUrl="RelAcionamentos_pesquisa.aspx"></componentart:MenuItem>
							<componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup" Text="Borderos de Pagamentos"
								DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup" NavigateUrl="Bordero_pesquisa.aspx"></componentart:MenuItem>
							<componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup" Text="Lista de Devedores"
								DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup" NavigateUrl="relDevedores_Pesquisa.aspx"></componentart:MenuItem>
							<componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup" Text="Lista para Correspond&#234;ncia"
								DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup" NavigateUrl="Cartas_pesquisa.aspx"></componentart:MenuItem>
						</componentart:MenuItem>
					</Items>
				</componentart:menu><ITEMLOOKS><COMPONENTART:ITEMLOOK CssClass="TopMenuItem" LookId="TopItemLook" LabelPaddingLeft="12px" LabelPaddingBottom="4px"
						LabelPaddingRight="12px" LabelPaddingTop="2px" HoverCssClass="TopMenuItemHover"></COMPONENTART:ITEMLOOK>
					<COMPONENTART:ITEMLOOK CssClass="MenuItem" LookId="DefaultItemLook" LabelPaddingLeft="18px" LabelPaddingBottom="4px"
						LabelPaddingRight="12px" LabelPaddingTop="3px" HoverCssClass="MenuItemHover" ExpandedCssClass="MenuItemExpanded"
						ActiveCssClass="MenuItemExpanded"></COMPONENTART:ITEMLOOK>
					<COMPONENTART:ITEMLOOK CssClass="MenuItem" LookId="ExpandableItemLook" LabelPaddingLeft="18px" LabelPaddingBottom="4px"
						LabelPaddingRight="12px" LabelPaddingTop="3px" HoverCssClass="MenuItemHover" ExpandedCssClass="MenuItemExpanded"
						RightIconWidth="20px" RightIconUrl="arrow.gif"></COMPONENTART:ITEMLOOK>
					<COMPONENTART:ITEMLOOK ImageUrl="break.gif" CssClass="MenuBreak" LookId="BreakItem" ImageHeight="2px" ImageWidth="100%"></COMPONENTART:ITEMLOOK>
				</ITEMLOOKS><ITEMS></ITEMS></div>
		</div>
	</div>
	<div id="Conteudo">
		<div id="BordaEsquerda"></div>
		<div id="BordaDireita"></div>
		<div id="tittela"><%=this.Titulo%></div>

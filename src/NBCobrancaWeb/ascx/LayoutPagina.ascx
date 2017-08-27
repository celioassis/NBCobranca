<%@ Register TagPrefix="componentart" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Control Language="C#" AutoEventWireup="true" Codebehind="LayoutPagina.ascx.cs" Inherits="NBCobranca.ascx.LayoutPagina" %>
<link href="../estilos/layout.css" type="text/css" rel="stylesheet">
<link href="../estilos/texto.css" type="text/css" rel="stylesheet">
<link href="../estilos/menuStyle.css" type="text/css" rel="stylesheet">
<link href="../estilos/gridStyle.css" type="text/css" rel="stylesheet">

<script src="../scripts/genericos.js" type="text/javascript"></script>

<script src="../scripts/navegador_css.js" type="text/javascript"></script>

<script src="../scripts/trata_campos_pesquisa.js" type="text/javascript"></script>

<script src="../scripts/mostra_esconde_campos.js" type="text/javascript"></script>

<script src="../scripts/AnthemCallBack.js" type="text/javascript"></script>

<link href="../estilos/treeStyle.css" type="text/css" rel="stylesheet">
<link href="../estilos/mapa.css" type="text/css" rel="stylesheet">
<link href="../estilos/pesquisa.css" type="text/css" rel="stylesheet">
<link href="../estilos/tooltip.css" type="text/css" rel="stylesheet">
<div id="Limite">
    <div id="Cabecalho">
        <div id="CabCantoEsquerdo">
        </div>
        <div id="CabCantoDireito">
            <img alt="" src="../imagens/layout/ccd.jpg"></div>
        <div id="menu">
            <div id="imgsair">
                <asp:HyperLink ID="hlSair" runat="server" ToolTip="Efetuar Logoff" NavigateUrl="../aspx/login.aspx"></asp:HyperLink></div>
            <div id="imghelp">                
            </div>
            <div style="margin-left: auto; width: 54%; margin-right: auto; padding-top: 18px">
                <asp:Image ID="imgBarraMenu" ImageUrl="../imagens/layout/imgBarraMenu.gif" runat="server"
                    Visible="False"></asp:Image>
                <componentart:menu id="Menu1" width="200px" cssclass="TopGroup" scrolldownlookid="DefaultItemLook"
                    scrolluplookid="DefaultItemLook" expanddelay="100" enableviewstate="False" imagesbaseurl="../imagens/menu/"
                    defaultgroupitemspacing="0px" topgroupitemspacing="0px" defaultitemlookid="DefaultItemLook"
                    defaultsubgroupexpandoffsety="-5" defaultsubgroupexpandoffsetx="-7" defaultgroupcssclass="MenuGroup"
                    clientobjectid="_ctl0_Menu1" clienttarget="Auto" runat="server">
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
				</componentart:menu>
            </div>
        </div>
    </div>
    <div id="Conteudo">
        <div id="BordaEsquerda">
        </div>
        <div id="BordaDireita">
        </div>
        <div id="tittela">
            Sem titulo
        </div>        
        <asp:PlaceHolder ID="phLayout" runat="server">
        </asp:PlaceHolder>
    </div>
    <div id="Rodape">
        <div id="RodCantoEsq">
            <img alt="" src="../imagens/layout/cbe.jpg"></div>
        <div id="RodCantoDir">
            <img alt="" src="../imagens/layout/cbd.jpg"></div>
        <div id="RodMeio">
            <div id="Versao">
                <asp:Label ID="lblVersao" runat="server" Font-Bold="True" Font-Size="Smaller"></asp:Label>
            </div>
        </div>
        <div id="user">
            <asp:HyperLink ID="hlUsuario" runat="server" ToolTip="Clique aqui para alterar sua Senha"
                NavigateUrl="../aspx/senha_editar.aspx"></asp:HyperLink>
        </div>
        <div id="LogoNeobridge">
            <a href="http://www.neobridge.com.br" target="_blank">
                <img alt="" src="../imagens/layout/copyright.jpg" border="0"></a>
        </div>
    </div>
</div>

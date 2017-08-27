<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CabecalhoXHTML.ascx.cs"
    Inherits="NBCobranca.ascx.CabecalhoXHTML" %>
<%@ Register TagPrefix="componentart" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<link href="../estilos/layout.css" type="text/css" rel="stylesheet" />
<link href="../estilos/texto.css" type="text/css" rel="stylesheet" />
<link href="../estilos/menuStyle.css" type="text/css" rel="stylesheet" />
<link href="../estilos/gridStyle.css" type="text/css" rel="stylesheet" />
<link href="../estilos/treeStyle.css" type="text/css" rel="stylesheet" />
<link href="../estilos/mapa.css" type="text/css" rel="stylesheet" />
<link href="../estilos/pesquisa.css" type="text/css" rel="stylesheet" />
<link href="../estilos/tooltip.css" type="text/css" rel="stylesheet" />

<script src="../scripts/genericos.js" type="text/javascript"></script>

<script src="../scripts/navegador_css.js" type="text/javascript"></script>

<script src="../scripts/trata_campos_pesquisa.js" type="text/javascript"></script>

<script src="../scripts/mostra_esconde_campos.js" type="text/javascript"></script>

<script type="text/javascript" src="../scripts/AnthemCallBack.js"></script>

<div id="Limite" >
    <div id="Cabecalho">
        <div id="CabCantoEsquerdo" style="left:0px;">
        </div>
        <div id="CabCantoDireito" style="right:0px;">
            <img alt="" src="../imagens/layout/ccd.jpg" />
        </div>
        <div id="menu">
            <div id="imgsair">
                <asp:HyperLink ID="hlSair" runat="server" ToolTip="Efetuar Logoff" NavigateUrl="../aspx/login.aspx"></asp:HyperLink></div>
            <div id="imghelp">
                <a href="../help/Sistema_de_Controle_de_Eventos.htm"></a>
            </div>
            <div style="margin-left: auto; width: 250px; margin-right: auto; padding-top: 18px">
                <asp:Image ID="imgBarraMenu" ImageUrl="../imagens/layout/imgBarraMenu.gif" runat="server"
                    Visible="False"></asp:Image>
                <componentart:Menu ID="Menu1" Width="200px" CssClass="TopGroup" ScrollDownLookId="DefaultItemLook"
                    ScrollUpLookId="DefaultItemLook" ExpandDelay="100" EnableViewState="False" ImagesBaseUrl="../imagens/menu/"
                    DefaultGroupItemSpacing="0px" TopGroupItemSpacing="0px" DefaultItemLookId="DefaultItemLook"
                    DefaultGroupCssClass="MenuGroup" ClientTarget="Auto" runat="server">
                    <ItemLooks>
                        <componentart:ItemLook HoverCssClass="TopMenuItemHover" LabelPaddingTop="2px" LabelPaddingRight="12px"
                            LabelPaddingBottom="4px" LabelPaddingLeft="12px" LookId="TopItemLook" CssClass="TopMenuItem">
                        </componentart:ItemLook>
                        <componentart:ItemLook HoverCssClass="MenuItemHover" LabelPaddingTop="3px" ActiveCssClass="MenuItemExpanded"
                            LabelPaddingRight="12px" LabelPaddingBottom="4px" ExpandedCssClass="MenuItemExpanded"
                            LabelPaddingLeft="18px" LookId="DefaultItemLook" CssClass="MenuItem"></componentart:ItemLook>
                        <componentart:ItemLook HoverCssClass="MenuItemHover" LabelPaddingTop="3px" RightIconUrl="arrow.gif"
                            LabelPaddingRight="12px" LabelPaddingBottom="4px" RightIconWidth="20px" ExpandedCssClass="MenuItemExpanded"
                            LabelPaddingLeft="18px" LookId="ExpandableItemLook" CssClass="MenuItem"></componentart:ItemLook>
                        <componentart:ItemLook ImageUrl="break.gif" ImageWidth="100%" ImageHeight="2px" LookId="BreakItem"
                            CssClass="MenuBreak"></componentart:ItemLook>
                    </ItemLooks>
                    <Items>
                        <componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup"
                            ID="mnuCadastros" Text="Cadastros" DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup"
                            LookId="TopItemLook" runat="server">
                            <componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup"
                                ID="mnuCadClientes" Text="Clientes" DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup"
                                NavigateUrl="clientesfornecedores.aspx?TipoEntidade=2" runat="server">
                            </componentart:MenuItem>
                            <componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup"
                                Text="Funcion&#225;rios" DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup"
                                NavigateUrl="funcionarios.aspx" runat="server">
                            </componentart:MenuItem>
                            <componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup"
                                Text="Devedores" DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup"
                                NavigateUrl="devedores.aspx" runat="server">
                            </componentart:MenuItem>
                            <componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup"
                                Text="Tipos de Divida" DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup"
                                NavigateUrl="TipoDividas.aspx" runat="server">
                            </componentart:MenuItem>
                            <componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup"
                                Text="Tipos de Acionamento" DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup"
                                NavigateUrl="TipoAcionamentos.aspx" runat="server">
                            </componentart:MenuItem>
                        </componentart:MenuItem>
                        <componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup"
                            Text="Lan&#231;amentos" DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup"
                            LookId="TopItemLook" runat="server">
                            <componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup"
                                Text="Acionamentos" DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup"
                                NavigateUrl="Acionamento_pesquisa.aspx" runat="server">
                            </componentart:MenuItem>
                        </componentart:MenuItem>
                        <componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup"
                            Text="Relat&#243;rios" DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup"
                            LookId="TopItemLook" runat="server">
                            <componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup"
                                Text="Acionamentos" DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup"
                                NavigateUrl="RelAcionamentos_pesquisa.aspx" runat="server">
                            </componentart:MenuItem>
                            <componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup"
                                Text="Borderos de Pagamentos" DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup"
                                NavigateUrl="Bordero_pesquisa.aspx" runat="server">
                            </componentart:MenuItem>
                            <componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup"
                                Text="Lista de Devedores" DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup"
                                NavigateUrl="relDevedores_Pesquisa.aspx" runat="server">
                            </componentart:MenuItem>
                            <componentart:MenuItem SubGroupItemSpacing="0px" DefaultSubGroupCssClass="MenuGroup"
                                Text="Lista para Correspond&#234;ncia" DefaultSubGroupItemSpacing="0px" SubGroupCssClass="MenuGroup"
                                NavigateUrl="Cartas_pesquisa.aspx" runat="server">
                            </componentart:MenuItem>
                        </componentart:MenuItem>
                    </Items>
                </componentart:Menu>
            </div>
        </div>
    </div>
    <div id="Conteudo">
        <div id="BordaEsquerda">
        </div>
        <div id="BordaDireita">
        </div>
        <div id="tittela">
            <%=this.Titulo%>
        </div>

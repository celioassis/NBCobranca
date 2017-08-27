<%@ Page Language="c#" Codebehind="Baixa.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.aspx.Baixa" %>

<%@ Register TagPrefix="componentart" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="NBWebControls" Namespace="NBWebControls" Assembly="NBWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>.:: Neobridge Controle de Cobrança Web - Baixa de Dívidas ::..</title>
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../estilos/links.css" rel="stylesheet">
    <link href="../estilos/texto.css" rel="stylesheet">

    <script type="text/javascript" language="JavaScript" src="../scripts/navegador_css.js"></script>

    <script type="text/javascript" language="JavaScript" src="../scripts/trata_campo.js"></script>

</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="frmBaixa" method="post" runat="server">
        <NBWebControls:MessageBox ID="MessageBox" runat="server" PastaScripts="../scripts"
            PastaStyles="../estilos" ManterScroll="True" Altura="150" Largura="500"></NBWebControls:MessageBox>
        <asp:Panel ID="pnConteudo" runat="server">
            <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr align="right" height="52">
                    <td valign="top" colspan="3" height="52">
                        <table cellspacing="0" cellpadding="0" width="100%" background="../imagens/tela_peq_topo_meio.jpg"
                            border="0">
                            <tr>
                                <td style="background-image: url(../imagens/tela_peq_topo_left.jpg); background-repeat: no-repeat"
                                    height="52">
                                    <table height="34" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td valign="top" width="30">
                                                &nbsp;</td>
                                            <td class="titulo" valign="top">
                                                Baixa de Dívidas
                                                <%=Titulo%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="background-position-x: right; background-image: url(../imagens/tela_peq_topo_right.jpg);
                                    background-repeat: no-repeat" width="55">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr height="100%">
                    <td valign="top" width="12" background="../imagens/tela_peq_f_lateral.jpg">
                        <img height="25" src="../imagens/tela_peq_f_lateral.jpg" width="12"></td>
                    <td valign="top" width="100%" colspan="2">
                        <asp:Panel ID="pnPF" runat="server">
                            <table id="tabPF" cellspacing="0" cellpadding="2" width="99%" border="0">
                                <tr>
                                    <td colspan="3" height="6">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="txt" valign="top" width="58%">
                                        Nome<br>
                                        <asp:TextBox ID="txtNome" runat="server" ReadOnly="True"></asp:TextBox></td>
                                    <td class="txt" width="21%">
                                        CPF
                                        <br>
                                        <asp:TextBox ID="txtCPF" runat="server" ReadOnly="True"></asp:TextBox></td>
                                    <td class="txt" valign="top" width="21%">
                                        RG
                                        <br>
                                        <asp:TextBox ID="txtRG" runat="server" ReadOnly="True"></asp:TextBox></td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnPJ" runat="server" Visible="False">
                            <table id="tabPJ" cellspacing="0" cellpadding="0" width="99%" border="0">
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                            <tr>
                                                <td colspan="2" height="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="txt" valign="top" width="54%">
                                                    Razão Social
                                                    <br>
                                                    <asp:TextBox ID="txtRazaoSocial" runat="server" ReadOnly="True"></asp:TextBox></td>
                                                <td class="txt" width="46%">
                                                    Nome Fantasia
                                                    <br>
                                                    <asp:TextBox ID="txtNomeFantasia" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                        <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                            <tr>
                                                <td class="txt" valign="top" width="25%">
                                                    CNPJ
                                                    <br>
                                                    <asp:TextBox ID="txtCNPJ" runat="server" ReadOnly="True"></asp:TextBox></td>
                                                <td class="txt" width="33%">
                                                    Inscrição Estadual
                                                    <br>
                                                    <asp:TextBox ID="txtInscEstadual" runat="server" ReadOnly="True"></asp:TextBox></td>
                                                <td class="txt" width="42%">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlAcionamentos" runat="server">
                            <table cellspacing="0" cellpadding="0" width="99%" border="0">
                                <tr>
                                    <td class="txt" id="linhaAcionamentos">
                                        <componentart:Snap ID="SnapAcionamentos" runat="server" CurrentDockingContainer="linhaAcionamentos"
                                            DockingContainers="linhaAcionamentos" Height="70" Width="160" DockingStyle="none"
                                            DraggingStyle="none" CurrentDockingIndex="0" MustBeDocked="True" IsCollapsed="true">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvAcionamentos" runat="server" CellPadding="0" BorderWidth="1px"
                                                    GridLines="Vertical" AutoGenerateColumns="False" BorderColor="#DDE4DA" Width="100%">
                                                    <AlternatingRowStyle CssClass="dg_item" BackColor="#F9FCF3" />
                                                    <RowStyle CssClass="dg_item" BackColor="#E9EFDD" />
                                                    <HeaderStyle CssClass="dg_header"></HeaderStyle>
                                                    <Columns>
                                                        <asp:BoundField DataField="Usuario" HeaderText="&#160;Usu&#225;rio">
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DataAcionamento" HeaderText="Acionamento" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                                            <HeaderStyle HorizontalAlign="Center" Width="115px"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DataPromessa" HeaderText="Promessa" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle HorizontalAlign="Center" Width="75px"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TipoAcionamento" HeaderText="Tipo Acionamento">
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TextoRespeito" HeaderText="Texto a Respeito" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <HeaderTemplate>
                                                <table cellspacing="0" cellpadding="0" border="0" width="100%" bgcolor="#a4c955"
                                                    style="cursor: hand" onclick="SnapAcionamentos.ToggleExpand();">
                                                    <tr>
                                                        <td class="txt_peq">
                                                            <asp:Label ID="lbtnTituloSnapAcionamentos" runat="server">Acionamentos Realizados</asp:Label></td>
                                                        <td width="10" align="right">
                                                            <img src="../imagens/botoes/btn_abrefecha.jpg" width="22" height="16" border="0"></td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                        </componentart:Snap>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <!-- Inicio do Panel de Dívidas -->
                        <asp:Panel ID="pnlDividas" runat="server">
                            <table cellspacing="0" cellpadding="0" width="99%" border="0">
                                <tr>
                                    <td class="txt">
                                        <asp:GridView ID="gvSnapDividas" runat="server" CellPadding="0" BorderWidth="1px"
                                            GridLines="Vertical" AutoGenerateColumns="False" BorderColor="#DDE4DA" Width="100%"
                                            ShowFooter="true" OnRowCreated="gvSnapDividas_RowCreated">
                                            <AlternatingRowStyle CssClass="dg_item" BackColor="#F9FCF3" />
                                            <RowStyle CssClass="dg_item" BackColor="#E9EFDD" />
                                            <HeaderStyle CssClass="dg_header"></HeaderStyle>
                                            <FooterStyle CssClass="dg_header_peq dg_header" ForeColor="black" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="ckbHeader" runat="server" Width="21px" BorderStyle="None" AutoPostBack="True"
                                                            OnCheckedChanged="ckbHeader_CheckedChanged"></asp:CheckBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckbItem" runat="server" Width="21px" BorderStyle="None" AutoPostBack="True"
                                                            OnCheckedChanged="ckbItem_CheckedChanged"></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Carteira" HeaderText="Carteira"></asp:BoundField>
                                                <asp:BoundField DataField="TipoDivida" HeaderText="Tipo da D&#237;vida"></asp:BoundField>
                                                <asp:BoundField DataField="Contrato" HeaderText="Contrato"></asp:BoundField>
                                                <asp:BoundField DataField="NumDoc" HeaderText="Num. Doc" DataFormatString="{0:000000}">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DataVencimento" HeaderText="Vencimento" DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle HorizontalAlign="Center" Width="75px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ValorNominalReal" HeaderText="Valor Nominal" DataFormatString="{0:N}">
                                                    <HeaderStyle Width="80px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ValorCorrigido" HeaderText="Valor Corrigido" DataFormatString="{0:N}">
                                                    <HeaderStyle Width="80px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnAcordos" runat="server" Visible="False">
                        </asp:Panel>
                        <br>
                        <asp:Panel ID="pnBaixa" runat="server">
                            <table class="txt" cellspacing="0" cellpadding="2">
                                <tr>
                                    <td colspan="5">
                                        <asp:CheckBox ID="ckbPagouNoCliente" runat="server" Text="Pagou no Cliente" AutoPostBack="True"
                                            OnCheckedChanged="ckbPagouNoCliente_CheckedChanged" Enabled="False"></asp:CheckBox>&nbsp;
                                        <asp:CheckBox ID="ckbBaixaParcial" runat="server" Text="Baixa Parcial" AutoPostBack="True"
                                            OnCheckedChanged="ckbBaixaParcial_CheckedChanged" Enabled="False"></asp:CheckBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Cobrador:&nbsp;</td>
                                    <td>
                                        <asp:DropDownList ID="ddlCobrador" runat="server" Width="100px" DataValueField="idUsuario"
                                            DataTextField="Login">
                                        </asp:DropDownList>&nbsp;</td>
                                    <td align="right">
                                        Comissão:&nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtPerComissao_MOEDA" Style="text-align: right" runat="server" Width="50px"
                                            MaxLength="7">0,00</asp:TextBox>%</td>
                                    <td align="right">
                                        Bordero:&nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtBordero_INT" runat="server" Width="40px" MaxLength="5">0</asp:TextBox></td>
                                    <td>
                                        Data de Baixa:&nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtDataBaixa_DATA" runat="server" Width="60px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Recibo:&nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtNumRecibo_INT" runat="server" Width="50px">0</asp:TextBox></td>
                                    <td align="right">
                                        Valor Baixa:&nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtValorBaixa_MOEDA" Style="text-align: right" runat="server" ReadOnly="True"
                                            Width="60px" AutoPostBack="True" OnTextChanged="txtValorBaixa_MOEDA_TextChanged">0,00</asp:TextBox></td>
                                    <td align="right">
                                        Valor Recebido:&nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtValorRecebido_MOEDA" Style="text-align: right" runat="server"
                                            Width="60px">0,00</asp:TextBox></td>
                                    <td colspan="2">
                                        &nbsp;
                                        <asp:Button ID="btnBaixar" runat="server" Width="50px" Text="Baixar" Enabled="false"
                                            OnClick="btnBaixar_Click"></asp:Button></td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="limitador" runat="server" Width="500px">
                            &nbsp;</asp:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>

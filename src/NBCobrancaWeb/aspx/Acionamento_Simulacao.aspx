<%@ Register TagPrefix="NBWebControls" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Register TagPrefix="componentart" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>

<%@ Page Language="c#" Codebehind="Acionamento_Simulacao.aspx.cs" AutoEventWireup="True"
    Inherits="NBCobranca.aspx.Acionamento_Simulacao" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>.:: Neobridge Controle de Cobrança Web - Simulação de Acordos ::.</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../estilos/links.css" rel="stylesheet">
    <link href="../estilos/texto.css" rel="stylesheet">

    <script language="JavaScript" src="../scripts/navegador_css.js"></script>

    <script language="JavaScript" src="../scripts/trata_campo.js"></script>

</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="frmAcionamento_Simulacao" method="post" runat="server">
        <NBWebControls:MessageBox ID="MessageBox" runat="server" PastaScripts="../scripts"
            PastaStyles="../estilos" Altura="150" Largura="500" UsandoAjaxAnthem="True"></NBWebControls:MessageBox>
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
                                                Simulação de Acordo
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
                        <!-- Inicio do Panel de Dívidas -->
                        <asp:Panel ID="pnlDividas" runat="server">
                            <table cellspacing="0" cellpadding="0" width="99%" border="0">
                                <tr>
                                    <td class="txt">
                                        Dividas</td>
                                </tr>
                                <tr>
                                    <td class="txt">
                                        <asp:DataGrid ID="dgSnapDividas" runat="server" GridLines="Vertical" BorderColor="#8AAD8A"
                                            ShowFooter="True" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
                                            OnItemDataBound="dgDividas_ItemDataBound" OnItemCreated="dgDividas_ItemCreated"
                                            Width="100%">
                                            <FooterStyle ForeColor="Black" CssClass="dg_header_peq" BackColor="#729C19"></FooterStyle>
                                            <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                                            <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                                            <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                                            <Columns>
                                                <asp:BoundColumn Visible="False" HeaderText="Key"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <HeaderTemplate>
                                                        <anthem:CheckBox ID="ckbHeader" runat="server" Width="21px" OnCheckedChanged="ckbHeader_CheckedChanged"
                                                            BorderStyle="None" AutoCallBack="True"></anthem:CheckBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <anthem:CheckBox ID="ckbItem" runat="server" Width="21px" OnCheckedChanged="ckbItem_CheckedChanged"
                                                            BorderStyle="None" AutoCallBack="True"></anthem:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn HeaderText="Carteira"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="IdTipoDivida" HeaderText="Tipo da D&#237;vida"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Contrato" HeaderText="Contrato"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="NumDoc" HeaderText="Num. Doc" DataFormatString="{0:000000}">
                                                    <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DataVencimento" HeaderText="Vencimento" DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle HorizontalAlign="Center" Width="75px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="ValorNominal" HeaderText="Valor Nominal" DataFormatString="{0:N}">
                                                    <HeaderStyle Width="80px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Valor Corrigido">
                                                    <HeaderStyle Width="80px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                        </asp:DataGrid></td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <anthem:Panel ID="pnCalculaSimulacao" runat="server" AutoUpdateAfterCallBack="True">
                            <table class="txt" cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td>
                                        <table class="txt" cellspacing="0" cellpadding="2" width="100%">
                                            <tr>
                                                <td align="right">
                                                    % Juros Mês:&nbsp;</td>
                                                <td>
                                                    <asp:TextBox ID="txtPercJuros_MOEDA" Style="text-align: right" TabIndex="1" runat="server"
                                                        Width="35px" AutoPostBack="True" OnTextChanged="txtPercJuros_MOEDA_TextChanged"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    Numero de Parcelas:&nbsp;</td>
                                                <td>
                                                    <asp:TextBox ID="txtTotParcela_INT" TabIndex="2" runat="server" Width="30px" ReadOnly="True"
                                                        AutoPostBack="True" OnTextChanged="txtTotParcela_INT_TextChanged">1</asp:TextBox></td>
                                                <td align="right">
                                                    Dias entre cada Parcela:&nbsp;</td>
                                                <td>
                                                    <asp:TextBox ID="txtDiasParcela" TabIndex="3" runat="server" Width="30px" ReadOnly="True"
                                                        AutoPostBack="True" OnTextChanged="txtDiasParcela_TextChanged">30</asp:TextBox></td>
                                                <td align="right">
                                                    Valor Entrada:&nbsp;</td>
                                                <td>
                                                    <asp:TextBox ID="txtValorEntrada_MOEDA" Style="text-align: right" TabIndex="3" runat="server"
                                                        Width="70px" ReadOnly="True" AutoPostBack="True" OnTextChanged="txtValorEntrada_MOEDA_TextChanged">0,00</asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="txt" cellspacing="0" cellpadding="2" width="100%">
                                            <tr>
                                                <td align="right">
                                                    Valor Parcela:&nbsp;</td>
                                                <td>
                                                    <anthem:TextBox ID="txtValorParcela_MOEDA" Style="text-align: right" TabIndex="4"
                                                        runat="server" Width="70px" ReadOnly="True" AutoPostBack="True">0,00</anthem:TextBox></td>
                                                <td align="right">
                                                    Data da 1ª Parcela:&nbsp;</td>
                                                <td>
                                                    <asp:TextBox ID="txtDataParcela1_DATA" Style="text-align: right" TabIndex="5" runat="server"
                                                        Width="70px" ReadOnly="True" AutoPostBack="True" OnTextChanged="txtDataParcela1_DATA_TextChanged"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titulo" align="center">
                                        Resultado
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="txt" cellspacing="0" cellpadding="2" width="100%">
                                            <tr>
                                                <td style="width: 140px">
                                                    Valor Total do Parcelamento:</td>
                                                <td>
                                                    <anthem:Label ID="lblTotalParcelamento" runat="server"></anthem:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 140px">
                                                    Data da Parcela Final:</td>
                                                <td>
                                                    <anthem:Label ID="lblDtaParcelaFinal" runat="server"></anthem:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <anthem:Button ID="btnSalvarSimulacao" TabIndex="6" runat="server" Width="50px" CssClass="botaoAzul"
                                            Text="Salvar" OnClick="btnSalvarSimulacao_Click"></anthem:Button></td>
                                </tr>
                            </table>
                        </anthem:Panel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>

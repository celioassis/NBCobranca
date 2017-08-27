<%@ Page Language="c#" Codebehind="rel_devedores.aspx.cs" AutoEventWireup="True"
    Inherits="NBCobranca.aspx.relatorios.rel_devedores" EnableSessionState="True"
    EnableViewState="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Relatório de Devedores</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link media="screen" href="tela.css" type="text/css" rel="stylesheet">
    <link media="print" href="impressora.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="frmRelDevedores" method="post" runat="server">
        <center>
            <div id="conteudo">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="center">
                            <asp:Image ID="cabCoest" ImageUrl="../../imagens/cab_coest.gif" runat="server"></asp:Image><br>
                            <br>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <h3>
                                Relatório de Devedores</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h4>
                                <asp:Label ID="lblPeriodo" runat="server"></asp:Label>
                            </h4>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataGrid ID="dgDados" runat="server" Width="100%" EnableViewState="False" GridLines="None"
                                Font-Size="6pt" ForeColor="Black" BackColor="White" BorderStyle="None" AllowCustomPaging="True"
                                AutoGenerateColumns="False" CellPadding="2" ShowFooter="True" OnItemDataBound="dgDados_ItemDataBound"
                                Visible="false">
                                <FooterStyle Font-Size="7pt" BackColor="Gray"></FooterStyle>
                                <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#000099"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#CCCCCC"></AlternatingItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="Black" Font-Size="7pt" CssClass="dg_header"
                                    BackColor="Gray"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="XmPathCliente" HeaderText="&#160;Carteira"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="NomePrimary" HeaderText="&#160;Nome Devedor"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DescTipoDivida" HeaderText="&#160;Tipo da D&#237;vida"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Contrato" HeaderText="&#160;Contrato">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="NumDoc" HeaderText="&#160;NumDoc">
                                        <ItemStyle Width="50px"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DataVencimento" HeaderText="&#160;Vencimento" DataFormatString="{0:dd/MM/yyy}">
                                        <ItemStyle Width="70px"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ValorNominal" HeaderText="&#160;Valor Nominal" DataFormatString="{0:N}">
                                        <ItemStyle HorizontalAlign="Right" Width="90px"></ItemStyle>
                                    </asp:BoundColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" CssClass="dg_header"
                                    Wrap="False"></PagerStyle>
                            </asp:DataGrid>
                            <asp:DataGrid ID="dgDadosAgrupados" runat="server" Width="100%" EnableViewState="False"
                                GridLines="None" Font-Size="6pt" ForeColor="Black" BackColor="White" BorderStyle="None"
                                AllowCustomPaging="True" AutoGenerateColumns="False" CellPadding="2" ShowFooter="True"
                                OnItemDataBound="dgDados_ItemDataBound" Visible="false">
                                <FooterStyle Font-Size="7pt" BackColor="Gray"></FooterStyle>
                                <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#000099"></SelectedItemStyle>
                                <AlternatingItemStyle BackColor="#CCCCCC"></AlternatingItemStyle>
                                <HeaderStyle Font-Bold="True" ForeColor="Black" Font-Size="7pt" CssClass="dg_header"
                                    BackColor="Gray"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="XmPathCliente" HeaderText="&#160;Carteira"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="NomePrimary" HeaderText="&#160;Nome Devedor"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="ValorNominalTotal" HeaderText="&#160;Valor Nominal Total"
                                        DataFormatString="{0:N}" HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="right">
                                    </asp:BoundColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" CssClass="dg_header"
                                    Wrap="False"></PagerStyle>
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </form>
</body>
</html>

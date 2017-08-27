<%@ Register TagPrefix="componentart" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="NBWebControls" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>

<%@ Page Language="c#" Codebehind="Acionamento_ficha_BKP.aspx.cs" AutoEventWireup="True"
    Inherits="NBCobranca.aspx.Acionamento_ficha" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>.:: Neobridge Controle de Cobrança Web - Ficha de Acionamento ::..</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../estilos/links.css" rel="stylesheet">
    <link href="../estilos/texto.css" rel="stylesheet">

    <script language="JavaScript" src="../scripts/navegador_css.js"></script>

    <script language="JavaScript" src="../scripts/trata_campo.js"></script>

    <script language="JavaScript" src="../scripts/relatorio.js"></script>

    <script language="JavaScript" src="../scripts/AnthemCallBack.js">function ddlCartas_onclick() {

}

</script>

</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0" marginheight="0"
    marginwidth="0">
    <form id="frmAcionamento_Ficha" method="post" runat="server">
        <NBWebControls:MessageBox ID="MessageBox" runat="server" UsandoAjaxAnthem="True"
            PastaScripts="../scripts" PastaStyles="../estilos" Altura="150" Largura="500"
            OnCloseModalChoosed="MessageBox_CloseModalChoosed" OnNoChoosed="MessageBox_NoChoosed">
        </NBWebControls:MessageBox>
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
                                            Ficha de Acionamento</td>
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
                    <asp:Panel ID="pnEndereco" runat="server">
                        <table cellspacing="1" cellpadding="0" width="99%" border="0">
                            <tr>
                                <td valign="top" width="48%">
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr bgcolor="#f8f9ff">
                                            <td class="txt_peq" valign="top" width="37%" bgcolor="#f8f9f6">
                                                Endereço<br>
                                                <asp:TextBox ID="txtEndereco" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            <td class="txt_peq" valign="top" width="21%" bgcolor="#f8f9f6">
                                                Complemento<br>
                                                <asp:TextBox ID="txtComplemento" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            <td class="txt_peq" valign="top" width="25%" bgcolor="#f8f9f6">
                                                Bairro<br>
                                                <asp:TextBox ID="txtBairro" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            <td class="txt_peq" valign="top" width="17%" bgcolor="#f8f9f6">
                                                CEP<br>
                                                <asp:TextBox ID="txtCEP" runat="server" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td class="txt_peq" valign="top" width="28%" bgcolor="#f8f9f6">
                                                Cidade<br>
                                                <asp:TextBox ID="txtCidade" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            <td class="txt_peq" width="9%" bgcolor="#f8f9f6">
                                                UF<br>
                                                <asp:TextBox ID="txtUF" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            <td class="txt_peq" valign="top" width="42%" bgcolor="#f8f9f6">
                                                Comentários<br>
                                                <asp:TextBox ID="txtComentarios" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            <td class="txt_peq" valign="top" width="21%" bgcolor="#f8f9f6">
                                                Contato<br>
                                                <asp:TextBox ID="txtContato" runat="server" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnSnapEndereco" runat="server" Visible="False">
                        <!--- Inicio Snap de Endereço --->
                        <table cellspacing="0" cellpadding="0" width="99%" border="0">
                            <tr>
                                <td class="txt" id="linhaEndereco">
                                    <componentart:Snap ID="SnapEndereco" runat="server" Width="160" IsCollapsed="false"
                                        MustBeDocked="True" CurrentDockingIndex="0" ExpandCollapseMode="Animated" DraggingStyle="none"
                                        DockingStyle="none" Height="70" DockingContainers="linhaEndereco" CurrentDockingContainer="linhaEndereco">
                                        <ContentTemplate>
                                            <asp:DataGrid ID="dgSnapEnderecos" runat="server" Width="100%" GridLines="Vertical"
                                                BorderColor="#8AAD8A" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False">
                                                <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                                                <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                                                <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                                                <Columns>
                                                    <asp:BoundColumn DataField="Logradouro_key" HeaderText="Endereco"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="complemento" HeaderText="Complemento"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="bairro" HeaderText="Bairro"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="municipio" HeaderText="Cidade"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="UF" HeaderText="UF">
                                                        <HeaderStyle Width="30px"></HeaderStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="contato" HeaderText="Contato"></asp:BoundColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                            </asp:DataGrid>
                                        </ContentTemplate>
                                        <HeaderTemplate>
                                            <table cellspacing="0" cellpadding="0" border="0" width="100%" bgcolor="#a4c955"
                                                style="cursor: hand" onclick="SnapEndereco.ToggleExpand();">
                                                <tr>
                                                    <td class="txt_peq">
                                                        <asp:Label ID="lblTituloSnapEnd" runat="server">Lista de Endereços</asp:Label></td>
                                                    <td width="10" align="right">
                                                        <img src="../imagens/botoes/btn_abrefecha.jpg" width="22" height="16" border="0"></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                    </componentart:Snap>
                                </td>
                            </tr>
                        </table>
                        <!--- Fim do Snap Endereço --->
                    </asp:Panel>
                    <asp:Panel ID="pnTelefone" runat="server">
                        <table cellspacing="1" cellpadding="0" width="99%" border="0">
                            <tr>
                                <td valign="top" width="48%">
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr bgcolor="#f8f9f6">
                                            <td class="txt_peq" valign="top" width="29%">
                                                Descrição<br>
                                                <asp:TextBox ID="txtDescricaoTelefone" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            <td class="txt_peq" valign="top" width="29%">
                                                Contato<br>
                                                <asp:TextBox ID="txtContatoTelefone" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            <td class="txt_peq" valign="top" width="10%">
                                                DDD<br>
                                                <asp:TextBox ID="txtDDDTelefone" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            <td class="txt_peq" valign="top" width="22%">
                                                Fone<br>
                                                <asp:TextBox ID="txtTelefone" runat="server" ReadOnly="True"></asp:TextBox></td>
                                            <td class="txt_peq" valign="top" width="10%">
                                                Ramal<br>
                                                <asp:TextBox ID="txtRamal" runat="server" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnSnapTelefones" runat="server" Visible="False">
                        <!--- Inicio Snap de Telefones --->
                        <table cellspacing="0" cellpadding="0" width="99%" border="0">
                            <tr>
                                <td class="txt" id="linhaTelefone">
                                    <componentart:Snap ID="SnapTelefone" runat="server" Width="160" IsCollapsed="false"
                                        MustBeDocked="True" CurrentDockingIndex="0" ExpandCollapseMode="Animated" DraggingStyle="none"
                                        DockingStyle="none" Height="70" DockingContainers="linhaTelefone" CurrentDockingContainer="linhaTelefone">
                                        <ContentTemplate>
                                            <asp:DataGrid ID="dgSnapTelefones" runat="server" Width="100%" GridLines="Vertical"
                                                BorderColor="#8AAD8A" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False">
                                                <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                                                <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                                                <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                                                <Columns>
                                                    <asp:BoundColumn DataField="descricao" HeaderText="&amp;nbsp;Descri&#231;&#227;o"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="contato" HeaderText="&amp;nbsp;Contato"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ddd_key" HeaderText="&amp;nbsp;DDD">
                                                        <HeaderStyle Width="30px"></HeaderStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="fone_key" HeaderText="&amp;nbsp;Fone">
                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ramal" HeaderText="&amp;nbsp;Ramal">
                                                        <HeaderStyle Width="40px"></HeaderStyle>
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                            </asp:DataGrid>
                                        </ContentTemplate>
                                        <HeaderTemplate>
                                            <table cellspacing="0" cellpadding="0" border="0" width="100%" bgcolor="#a4c955"
                                                style="cursor: hand" onclick="SnapTelefone.ToggleExpand();">
                                                <tr>
                                                    <td class="txt_peq">
                                                        <asp:Label ID="lblTituloSnapTel" runat="server">Telefones</asp:Label></td>
                                                    <td width="10" align="right">
                                                        <img src="../imagens/botoes/btn_abrefecha.jpg" width="22" height="16" border="0"></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                    </componentart:Snap>
                                </td>
                            </tr>
                        </table>
                        <!--- Fim do Snap Telefones --->
                    </asp:Panel>
                    <asp:Panel ID="pnEmail" runat="server">
                        <table cellspacing="1" cellpadding="0" width="99%" border="0">
                            <tr>
                                <td valign="top" width="48%">
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr bgcolor="#f8f9f6">
                                            <td class="txt_peq" valign="top" width="100%">
                                                E-mail<br>
                                                <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnSnapEmails" runat="server" Visible="False">
                        <!--- Inicio Snap de E-mails --->
                        <table cellspacing="0" cellpadding="0" width="99%" border="0">
                            <tr>
                                <td class="txt" id="linhaEmail">
                                    <componentart:Snap ID="SnapEmail" runat="server" Width="160" IsCollapsed="false"
                                        MustBeDocked="True" CurrentDockingIndex="0" ExpandCollapseMode="Animated" DraggingStyle="none"
                                        DockingStyle="none" Height="70" DockingContainers="linhaEmail" CurrentDockingContainer="linhaEmail">
                                        <ContentTemplate>
                                            <asp:DataGrid ID="dgSnapEmails" runat="server" Width="100%" GridLines="Vertical"
                                                BorderColor="#8AAD8A" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False">
                                                <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                                                <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                                                <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                                                <Columns>
                                                    <asp:BoundColumn DataField="Descricao" HeaderText="&nbsp;Descri&ccedil;&atilde;o"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="eMail_key" HeaderText="&nbsp;E-mail"></asp:BoundColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                            </asp:DataGrid>
                                        </ContentTemplate>
                                        <HeaderTemplate>
                                            <table cellspacing="0" cellpadding="0" border="0" width="100%" bgcolor="#a4c955"
                                                style="cursor: hand" onclick="SnapEmail.ToggleExpand();">
                                                <tr>
                                                    <td class="txt_peq">
                                                        <asp:Label ID="lblTituloSnapEma" runat="server">Lista de emails</asp:Label></td>
                                                    <td width="10" align="right">
                                                        <img src="../imagens/botoes/btn_abrefecha.jpg" width="22" height="16" border="0"></td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                    </componentart:Snap>
                                </td>
                            </tr>
                        </table>
                        <!--- Fim do Snap Emails --->
                    </asp:Panel>
                    <asp:Panel ID="pnBarraEdicao" runat="server" Width="100%">
                        <table cellspacing="0" cellpadding="2" width="99%" border="0">
                            <tr>
                                <td align="right">
                                    <asp:Button ID="btnEditarFichar" runat="server" Width="100px" CssClass="botao" Text="Editar Cadastro"
                                        OnClick="btnEditarFichar_Click"></asp:Button></td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <!-- Inicio do Panel Financeiro -->
                    <asp:Panel ID="pnlDividas" runat="server" AutoUpdateAfterCallBack="True">
                        <!--- Inicio Snap de Dívidas--->
                        <table cellspacing="0" cellpadding="0" width="99%" border="0">
                            <tr>
                                <td class="txt" id="linhaDividas">
                                    <componentart:Snap ID="SnapDividas" runat="server" Width="160" IsCollapsed="false"
                                        MustBeDocked="True" CurrentDockingIndex="0" ExpandCollapseMode="Animated" DraggingStyle="none"
                                        DockingStyle="none" Height="70" DockingContainers="linhaDividas" CurrentDockingContainer="linhaDividas">
                                        <ContentTemplate>
                                            <asp:DataGrid ID="dgSnapDividas" runat="server" Width="100%" GridLines="Vertical"
                                                BorderColor="#8AAD8A" ShowFooter="True" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
                                                OnItemDataBound="dgDividas_ItemDataBound" OnItemCreated="dgDividas_ItemCreated">
                                                <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                                                <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                                                <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                                                <FooterStyle CssClass="dg_header_peq" BackColor="#729c19" ForeColor="#000000"></FooterStyle>
                                                <Columns>
                                                    <asp:BoundColumn DataField="XmPathCliente" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn HeaderText="Carteira"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="IdTipoDivida" HeaderText="Tipo da Dívida"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Contrato" HeaderText="Contrato"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="NumDoc" HeaderText="Num. Doc" DataFormatString="{0:000000}">
                                                        <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DataVencimento" HeaderText="Vencimento" DataFormatString="{0:dd/MM/yyyy}">
                                                        <HeaderStyle Width="75px" HorizontalAlign="Center"></HeaderStyle>
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
                                                    <asp:BoundColumn HeaderText="Baixa">
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                            </asp:DataGrid>
                                        </ContentTemplate>
                                        <HeaderTemplate>
                                            <table cellspacing="0" cellpadding="0" border="0" width="100%" bgcolor="#a4c955"
                                                style="cursor: hand" onclick="SnapDividas.ToggleExpand();">
                                                <tr>
                                                    <td class="txt_peq">
                                                        <asp:Label ID="lbtnTituloSnapDivida" runat="server">Dívidas</asp:Label></td>
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
                    <!--- Fim do Snap Dívidas --->
                    <asp:Panel ID="pnObsCadastro" runat="server" Visible="False">
                        <table cellspacing="0" cellpadding="2" width="99%" border="0">
                            <tr>
                                <td class="txt">
                                    Observações sobre o Devedor:
                                    <asp:TextBox ID="txtObsDevedor" runat="server" Height="60px" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnSnapAcionamentos" runat="server">
                        <table cellspacing="0" cellpadding="0" width="99%" border="0">
                            <tr>
                                <td class="txt_peq">
                                    Acionamentos</td>
                            </tr>
                            <tr>
                                <td>
                                    <anthem:DataGrid ID="dgAcionamentos" runat="server" Width="100%" GridLines="Vertical"
                                        CellPadding="0" AutoGenerateColumns="False" BorderWidth="1px" BorderColor="#8AAD8A"
                                        OnItemDataBound="dgAcionamentos_ItemDataBound">
                                        <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                                        <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                                        <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                                        <Columns>
                                            <asp:BoundColumn DataField="IdUsuario" HeaderText="&#160;Usu&#225;rio">
                                                <headerstyle horizontalalign="Center"></headerstyle>
                                                <itemstyle wrap="False"></itemstyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DataAcionamento" HeaderText="Acionamento" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                                <headerstyle horizontalalign="Center" width="115px"></headerstyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DataPromessa" HeaderText="Promessa" DataFormatString="{0:dd/MM/yyyy}">
                                                <headerstyle horizontalalign="Center" width="75px"></headerstyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="IdTipoAcionamento" HeaderText="Tipo Acionamento">
                                                <headerstyle horizontalalign="Center"></headerstyle>
                                                <itemstyle wrap="False"></itemstyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="TextoRespeito" HeaderText="Texto a Respeito" DataFormatString="{0:dd/MM/yyyy}">
                                                <headerstyle horizontalalign="Center"></headerstyle>
                                            </asp:BoundColumn>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                    </anthem:DataGrid></td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br>
                    <!-- Inicio da Tabela de lançamento de Acionamento -->
                    <table cellspacing="0" cellpadding="2" width="99%" border="0">
                        <tr>
                            <td align="center">
                                <anthem:Button ID="btnSimulacao" runat="server" Width="160px" CssClass="botao" Text="Simulação de Acordo"
                                    OnClick="btnSimulacao_Click" />&nbsp;
                                <anthem:Button ID="btnCadastroAlertas" runat="server" Width="160px" Text="Cadastrar Alertas"
                                    CssClass="botao" OnClick="btnCadastroAlertas_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="txt" cellspacing="0" cellpadding="2" width="100%" border="0">
                                    <tr>
                                        <td width="100">
                                            Tipo Acionamento:</td>
                                        <td width="200">
                                            <anthem:DropDownList ID="ddlTipoAcionamento" runat="server" DataValueField="Codigo"
                                                DataTextField="Descricao" AutoCallBack="True" OnSelectedIndexChanged="ddlTipoAcionamento_SelectedIndexChanged">
                                            </anthem:DropDownList>
                                        </td>
                                        <td>
                                            <anthem:Panel ID="pnPromessa" runat="server" Visible="False">
                                                <anthem:Label ID="lblDescDataPromesa" runat="server">Data de Promessa:</anthem:Label>&nbsp;
                                                <anthem:TextBox ID="txtDataPromessa_DATA" runat="server" Width="70px"></anthem:TextBox>
                                            </anthem:Panel>
                                            <anthem:Panel ID="pnCartas" runat="server" Visible="False">
                                                Tipo de Cartas:&nbsp;<select id="ddlCartas" style="width:100px">
                                                    <option selected="selected" value="1">Aviso</option>
                                                    <option value="2">Judicial</option>
                                                </select>
                                                <anthem:CheckBox ID="ckbSegundoAviso" runat="server" Text="Segundo Aviso"></anthem:CheckBox>&nbsp;
                                                <input class="botao" id="btnEmitirCarta" style="width: 80px" type="button" value="Emitir Carta"
                                                    name="btnEmitirCarta" runat="server">
                                            </anthem:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                    <tr>
                                        <td class="txt" valign="top" width="80%">
                                            Texto a Respeito&nbsp;do Acionamento
                                            <br>
                                            <anthem:TextBox ID="txtAcionamentos" runat="server" Width="100%" TextMode="MultiLine"></anthem:TextBox></td>
                                        <td class="txt" valign="bottom">
                                            <anthem:Button ID="btnAdicAcionamento" runat="server" CssClass="botao" Text="Adicionar"
                                                OnClick="btnAdicAcionamento_Click"></anthem:Button></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br>
                    <!-- Inicio da tabela de Botões -->
                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td valign="top">
                                <anthem:Button ID="btnSalvar" runat="server" BorderStyle="Solid" ImageUrl="../imagens/botoes/btn_salvar.gif"
                                    Width="71" Height="18" CssClass="botaoAzul" Text="Salvar" OnClick="btnSalvar_Click">
                                </anthem:Button>
                                &nbsp;
                                <input type="button" value="Cancelar" style="width: 71px; height: 18px" class="botaoVermelho"
                                    onclick="javascript:window.parent.hidePopWin();">
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" cellpadding="0" width="550" border="0">
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

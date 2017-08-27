<%@ Page Language="c#" Codebehind="entidades_cadastro.aspx.cs" AutoEventWireup="True"
    Inherits="NBCobranca.aspx.entidades_cadastro" %>

<%@ Register Assembly="NBWebControls" Namespace="NBWebControls" TagPrefix="cc1" %>
<%@ Register TagPrefix="componentart" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>.:: Neobridge Controle de Cobrança Web ::..</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../estilos/links.css" rel="stylesheet">
    <link href="../estilos/texto.css" rel="stylesheet">

    <script language="JavaScript" src="../scripts/navegador_css.js"></script>

    <script language="JavaScript" src="../scripts/mostra_esconde_campos.js"></script>

    <script language="JavaScript" src="../scripts/trata_campo.js"></script>

</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="frmClientesFornecedoresCadastro" method="post" runat="server">
        <cc1:MessageBox ID="MessageBox" runat="server" Largura="500" Altura="150" ManterScroll="True"
            PastaStyles="../estilos" PastaScripts="../scripts" OnCloseModalChoosed="MessageBox_CloseModalChoosed" OnNoChoosed="MessageBox_NoChoosed" OnYesChoosed="MessageBox_YesChoosed" />
        <asp:Panel ID="pnGeral" runat="server">
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
                                                <%=this.TituloCadastro%>
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
                        <table cellspacing="0" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td class="txt" valign="top" width="54%">
                                    <input class="radio" id="radPessoaF" onclick="javascript:TabelaFJMostraEsconde('tabPF', 'tabPJ');"
                                        type="radio" checked value="PF" name="radPessoa" runat="server">
                                    <b>Pessoa Física</b><font color="#009966">&nbsp; </font>
                                    <input class="radio" id="radPessoaJ" onclick="javascript:TabelaFJMostraEsconde('tabPJ', 'tabPF');"
                                        type="radio" value="PJ" name="radPessoa" runat="server">
                                    <b>Pessoa Jurídica</b><br>
                                </td>
                                <td class="txt" width="12%">
                                    <br>
                                </td>
                                <td class="txt" valign="top" width="34%">
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <table id="tabPF" cellspacing="0" cellpadding="2" width="99%" border="0" runat="server">
                            <tr>
                                <td colspan="3" height="6">
                                </td>
                            </tr>
                            <tr>
                                <td class="txt" valign="top" width="58%">
                                    <asp:Label ID="lblNomeEntidade" runat="server"></asp:Label><br>
                                    <asp:TextBox ID="txtClienteFornecedor_STR" runat="server" AutoPostBack="True" OnTextChanged="txtClienteFornecedor_STR_TextChanged"></asp:TextBox></td>
                                <td class="txt" width="21%">
                                    CPF<br>
                                    <input id="txtCPF_CPF" type="text" name="txtCPF_CPF" runat="server">
                                </td>
                                <td class="txt" valign="top" width="21%">
                                    RG<br>
                                    <input id="txtRG_RG" type="text" name="txtRG_RG" runat="server">
                                </td>
                            </tr>
                        </table>
                        <table id="tabPJ" style="display: none" cellspacing="0" cellpadding="0" width="99%"
                            border="0" runat="server">
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td colspan="2" height="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="txt" valign="top" width="54%">
                                                Razão Social<br>
                                                <asp:TextBox ID="txtRazaoSocial_TUD" runat="server" AutoPostBack="True" OnTextChanged="txtClienteFornecedor_STR_TextChanged"></asp:TextBox></td>
                                            <td class="txt" width="46%">
                                                Nome Fantasia<br>
                                                <input id="txtNomeFantasia_TUD" type="text" maxlength="50" name="txtNomeFantasia_TUD"
                                                    runat="server">
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td class="txt" valign="top" width="25%">
                                                CNPJ<br>
                                                <input id="txtCNPJ_CNPJ" type="text" name="txtCNPJ_CNPJ" runat="server">
                                            </td>
                                            <td class="txt" width="33%">
                                                Inscrição Estadual<br>
                                                <input id="txtInscricaoEstadual_TUD" type="text" maxlength="15" name="txtInscricaoEstadual_TUD"
                                                    runat="server">
                                            </td>
                                            <td class="txt" width="42%">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <!-- Inicio da Tabela de Endereços -->
                        <table cellspacing="0" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td height="6">
                                </td>
                            </tr>
                            <tr>
                                <td class="txt">
                                    <strong>Endereços<br>
                                    </strong>
                                    <table cellspacing="1" cellpadding="0" width="99%" bgcolor="#8aad8a" border="0">
                                        <tr>
                                            <td valign="top" width="48%">
                                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                                    <tr bgcolor="#f8f9ff">
                                                        <td class="txt_peq" valign="top" width="37%" bgcolor="#f8f9f6">
                                                            Endereço<br>
                                                            <input id="txtEndereco_TUD" type="text" maxlength="130" name="txtEndereco_TUD" runat="server">
                                                        </td>
                                                        <td class="txt_peq" valign="top" width="21%" bgcolor="#f8f9f6">
                                                            Complemento<br>
                                                            <input id="txtComplemento_TUD" type="text" maxlength="50" name="txtComplemento_TUD"
                                                                runat="server">
                                                        </td>
                                                        <td class="txt_peq" valign="top" width="25%" bgcolor="#f8f9f6">
                                                            Bairro<br>
                                                            <input id="txtBairro_TUD" type="text" maxlength="35" name="txtBairro_TUD" runat="server">
                                                        </td>
                                                        <td class="txt_peq" valign="top" width="17%" bgcolor="#f8f9f6">
                                                            CEP<br>
                                                            <input id="txtCEP_CEP" type="text" maxlength="9" name="txtCEP_CEP" runat="server">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                                    <tr>
                                                        <td class="txt_peq" valign="top" width="28%" bgcolor="#f8f9f6">
                                                            Cidade<br>
                                                            <input id="txtCidade_STR" type="text" maxlength="35" name="txtCidade_STR" runat="server">
                                                        </td>
                                                        <td class="txt_peq" width="9%" bgcolor="#f8f9f6">
                                                            UF<br>
                                                            <select id="selUF" name="selUF" runat="server">
                                                                <option value="AC">AC</option>
                                                                <option value="AL">AL</option>
                                                                <option value="AM">AM</option>
                                                                <option value="AP">AP</option>
                                                                <option value="BA">BA</option>
                                                                <option value="CE">CE</option>
                                                                <option value="DF">DF</option>
                                                                <option value="GO">GO</option>
                                                                <option value="MA">MA</option>
                                                                <option value="MG">MG</option>
                                                                <option value="MS">MS</option>
                                                                <option value="MT">MT</option>
                                                                <option value="PA">PA</option>
                                                                <option value="PB">PB</option>
                                                                <option value="PE">PE</option>
                                                                <option value="PI">PI</option>
                                                                <option value="PR">PR</option>
                                                                <option value="RJ">RJ</option>
                                                                <option value="RN">RN</option>
                                                                <option value="RO">RO</option>
                                                                <option value="RR">RR</option>
                                                                <option value="RS">RS</option>
                                                                <option value="SC" selected>SC</option>
                                                                <option value="SE">SE</option>
                                                                <option value="SP">SP</option>
                                                                <option value="TO">TO</option>
                                                            </select>
                                                        </td>
                                                        <td class="txt_peq" valign="top" width="21%" bgcolor="#f8f9f6">
                                                            Comentários<br>
                                                            <input id="txtComentarios_TUD" type="text" maxlength="255" name="txtComentarios_TUD"
                                                                runat="server">
                                                        </td>
                                                        <td class="txt_peq" valign="top" width="21%" bgcolor="#f8f9f6">
                                                            Contato<br>
                                                            <input id="txtContatoEndereco_TUD" type="text" maxlength="15" name="txtContatoEndereco_STR"
                                                                runat="server">
                                                        </td>
                                                        <td class="txt_peq" valign="bottom" width="21%" bgcolor="#f8f9f6">
                                                            <input class="radio" id="radEnderecoPrincipal" type="checkbox" checked value="s"
                                                                name="radEnderecoPrincipal" runat="server">
                                                            Endereço Principal</td>
                                                    </tr>
                                                    <tr bgcolor="#f8f9f6">
                                                        <td class="txt" valign="top" colspan="5">
                                                            <asp:LinkButton ID="lbtnMais_Endereco" runat="server" OnClick="LinkButton_Click">Mais</asp:LinkButton>&nbsp;
                                                            <asp:LinkButton ID="lbtnCancelar_Endereco" runat="server" Visible="False" OnClick="LinkButton_Click">Cancelar</asp:LinkButton></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <!--- Inicio Snap de Endereço --->
                                    <table cellspacing="0" cellpadding="0" width="99%" border="0">
                                        <tr>
                                            <td id="linhaEndereco">
                                                <componentart:Snap ID="SnapEndereco" runat="server" CurrentDockingContainer="linhaEndereco"
                                                    DockingContainers="linhaEndereco" Height="70" Width="160" DockingStyle="none"
                                                    DraggingStyle="none" ExpandCollapseMode="Animated" CurrentDockingIndex="0" MustBeDocked="True"
                                                    IsCollapsed="false" Visible="false">
                                                    <Footer></Footer>
                                                    <ContentTemplate>
                                                        <asp:DataGrid ID="dgSnapEnderecos" runat="server" Width="100%" GridLines="Vertical"
                                                            BorderColor="#8AAD8A" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
                                                            OnItemCommand="dgEnderecos_ItemCommand">
                                                            <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                                                            <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                                                            <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                                                            <Columns>
                                                                <asp:BoundColumn DataField="Key" HeaderText="Key" Visible="False"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="Logradouro_key" HeaderText="Endereco"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="complemento" HeaderText="Complemento"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="bairro" HeaderText="Bairro"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="municipio" HeaderText="Cidade"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="UF" HeaderText="UF">
                                                                    <HeaderStyle Width="30px"></HeaderStyle>
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="contato" HeaderText="Contato"></asp:BoundColumn>
                                                                <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_editar.gif' alt='Editar' border='0'&gt;"
                                                                    HeaderText="&lt;font size='1'&gt;COMA&lt;/font&gt;" CommandName="Edit">
                                                                    <HeaderStyle HorizontalAlign="Right" ForeColor="Black" Width="28px" BackColor="#8AAD8A">
                                                                    </HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:ButtonColumn>
                                                                <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_remover.gif' alt='Deletar' border='0'&gt;"
                                                                    HeaderText="&lt;font size='1'&gt;NDOS&lt;/font&gt;" CommandName="Delete">
                                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" Width="30px" BackColor="#8AAD8A">
                                                                    </HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:ButtonColumn>
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
                                </td>
                            </tr>
                            <tr>
                                <td height="10">
                                </td>
                            </tr>
                        </table>
                        <!-- Inicio da Tabela de Telefones -->
                        <table cellspacing="0" cellpadding="2" width="99%" border="0">
                            <tr>
                                <td class="txt" valign="top" width="58%">
                                    <strong>Telefones</strong><br>
                                    <table cellspacing="1" cellpadding="0" width="100%" bgcolor="#8aad8a" border="0">
                                        <tr>
                                            <td valign="top" width="48%">
                                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                                    <tr bgcolor="#f8f9f6">
                                                        <td class="txt_peq" valign="top" width="32%">
                                                            Descrição<br>
                                                            <input id="txtDescricaoTelefone_TUD" type="text" maxlength="60" name="txtDescricaoTelefone_TUD"
                                                                runat="server">
                                                        </td>
                                                        <td class="txt_peq" valign="top" width="26%">
                                                            Contato<br>
                                                            <input id="txtContatoTelefone_TUD" type="text" maxlength="35" name="txtContatoTelefone_STR"
                                                                runat="server">
                                                        </td>
                                                        <td class="txt_peq" valign="top" width="10%">
                                                            DDD<br>
                                                            <input id="txtDDD_INT" type="text" maxlength="5" name="txtDDD_INT" runat="server">
                                                        </td>
                                                        <td class="txt_peq" valign="top" width="22%">
                                                            Fone<br>
                                                            <input id="txtFone_FONE" type="text" maxlength="11" name="txtFone_FONE" runat="server">
                                                        </td>
                                                        <td class="txt_peq" valign="top" width="10%">
                                                            Ramal<br>
                                                            <input id="txtRamal_INT" type="text" maxlength="8" name="txtRamal_INT" runat="server">
                                                        </td>
                                                    </tr>
                                                    <tr bgcolor="#f8f9f6">
                                                        <td colspan="6">
                                                            <asp:LinkButton ID="lbtnMais_Telefone" runat="server" OnClick="LinkButton_Click">Mais</asp:LinkButton>&nbsp;
                                                            <asp:LinkButton ID="lbtnCancelar_Telefone" runat="server" Visible="False" OnClick="LinkButton_Click">Cancelar</asp:LinkButton></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <!--- Inicio Snap de Telefones --->
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td id="linhaTelefone">
                                                <componentart:Snap ID="SnapTelefone" runat="server" CurrentDockingContainer="linhaTelefone"
                                                    DockingContainers="linhaTelefone" Height="70" Width="160" DockingStyle="none"
                                                    DraggingStyle="none" ExpandCollapseMode="Animated" CurrentDockingIndex="0" MustBeDocked="True"
                                                    IsCollapsed="false" Visible="false">
                                                    <Footer></Footer>
                                                    <ContentTemplate>
                                                        <asp:DataGrid ID="dgSnapTelefones" runat="server" Width="100%" GridLines="Vertical"
                                                            BorderColor="#8AAD8A" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
                                                            OnItemCommand="dgTelefones_ItemCommand">
                                                            <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                                                            <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                                                            <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                                                            <Columns>
                                                                <asp:BoundColumn DataField="Key" HeaderText="Key" Visible="False"></asp:BoundColumn>
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
                                                                <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_editar.gif' alt='Editar' border='0'&gt;"
                                                                    HeaderText="&lt;font size='1'&gt; COMA&lt;/font&gt;" CommandName="Edit">
                                                                    <HeaderStyle Wrap="False" HorizontalAlign="Right" ForeColor="Black" Width="28px"
                                                                        BackColor="#8AAD8A"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:ButtonColumn>
                                                                <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_remover.gif' alt='Deletar' border='0'&gt;"
                                                                    HeaderText="&lt;font size='1'&gt;NDOS&lt;/font&gt;" CommandName="Delete">
                                                                    <HeaderStyle Wrap="False" HorizontalAlign="Left" ForeColor="Black" Width="28px" BackColor="#8AAD8A">
                                                                    </HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:ButtonColumn>
                                                            </Columns>
                                                            <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                                        </asp:DataGrid>
                                                    </ContentTemplate>
                                                    <HeaderTemplate>
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" bgcolor="#a4c955"
                                                            style="cursor: hand" onclick="SnapTelefone.ToggleExpand();">
                                                            <tr>
                                                                <td class="txt_peq">
                                                                    <asp:Label ID="lblTituloSnapTel" runat="server">&nbsp;Lista de Telefones</asp:Label></td>
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
                                </td>
                            </tr>
                            <tr>
                                <td height="10">
                                </td>
                            </tr>
                        </table>
                        <!-- Inicio da Tabela de E-mails -->
                        <table cellspacing="0" cellpadding="2" width="99%" border="0">
                            <tr>
                                <td class="txt" valign="top" width="50%">
                                    <strong>E-mails</strong><br>
                                    <table cellspacing="1" cellpadding="0" width="100%" bgcolor="#8aad8a" border="0">
                                        <tr>
                                            <td valign="top" width="48%">
                                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                                    <tr bgcolor="#f8f9f6">
                                                        <td class="txt_peq" valign="top" width="34%">
                                                            Nome<br>
                                                            <input id="txtContatoEmail_STR" type="text" maxlength="100" name="txtDescricaoEmail_STR"
                                                                runat="server">
                                                        </td>
                                                        <td class="txt_peq" valign="top" width="66%">
                                                            E-mail<br>
                                                            <input id="txtEmail_EMAIL" type="text" name="txtEmail_EMAIL" runat="server">
                                                        </td>
                                                    </tr>
                                                    <tr bgcolor="#f8f9f6">
                                                        <td colspan="2">
                                                            <asp:LinkButton ID="lbtnMais_Email" runat="server" OnClick="LinkButton_Click">Mais</asp:LinkButton>&nbsp;
                                                            <asp:LinkButton ID="lbtnCancelar_Email" runat="server" Visible="False" OnClick="LinkButton_Click">Cancelar</asp:LinkButton></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <!--- Inicio Snap de Emails --->
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td id="linhaEmail">
                                                <componentart:Snap ID="SnapEmail" runat="server" CurrentDockingContainer="linhaEmail"
                                                    DockingContainers="linhaEmail" Height="70" Width="160" DockingStyle="none" DraggingStyle="none"
                                                    ExpandCollapseMode="Animated" CurrentDockingIndex="0" MustBeDocked="True" IsCollapsed="false"
                                                    Visible="false">
                                                    <Footer></Footer>
                                                    <ContentTemplate>
                                                        <asp:DataGrid ID="dgSnapEmails" runat="server" Width="100%" GridLines="Vertical"
                                                            BorderColor="#8AAD8A" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
                                                            OnItemCommand="dgEmails_ItemCommand">
                                                            <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                                                            <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                                                            <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                                                            <Columns>
                                                                <asp:BoundColumn DataField="Key" HeaderText="Key" Visible="False"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="Descricao" HeaderText="&nbsp;Descri&ccedil;&atilde;o"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="eMail_key" HeaderText="&nbsp;E-mail"></asp:BoundColumn>
                                                                <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_editar.gif' alt='Editar' border='0'&gt;"
                                                                    HeaderText="&lt;font size='1'&gt;COMA&lt;/font&gt;" CommandName="Edit">
                                                                    <HeaderStyle HorizontalAlign="Right" ForeColor="Black" Width="28px" BackColor="#8AAD8A">
                                                                    </HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:ButtonColumn>
                                                                <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_remover.gif' alt='Deletar' border='0'&gt;"
                                                                    HeaderText="&lt;font size='1'&gt;NDOS&lt;/font&gt;" CommandName="Delete">
                                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" Width="30px" BackColor="#8AAD8A">
                                                                    </HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:ButtonColumn>
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
                                </td>
                                <!-- Inicio da Tabela de Sites, que uma Coluna da Tabela de E-mails -->
                                <td class="txt" valign="top" width="50%">
                                    <strong>Sites</strong><br>
                                    <table cellspacing="1" cellpadding="0" width="100%" bgcolor="#8aad8a" border="0">
                                        <tr>
                                            <td valign="top" width="48%">
                                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                                    <tr bgcolor="#f8f9f6">
                                                        <td class="txt_peq" valign="top" width="34%">
                                                            Descrição<br>
                                                            <input id="txtDescricaoSite_STR" type="text" maxlength="100" name="txtDescricaoSite_STR"
                                                                runat="server">
                                                        </td>
                                                        <td class="txt_peq" valign="top" width="66%">
                                                            Site<br>
                                                            <input id="txtSite_SITE" type="text" name="txtSite_SITE" runat="server">
                                                        </td>
                                                    </tr>
                                                    <tr bgcolor="#f8f9f6">
                                                        <td colspan="2">
                                                            <asp:LinkButton ID="lbtnMais_Site" runat="server" OnClick="LinkButton_Click">Mais</asp:LinkButton>&nbsp;
                                                            <asp:LinkButton ID="lbtnCancelar_Site" runat="server" Visible="False" OnClick="LinkButton_Click">Cancelar</asp:LinkButton></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <!--- Inicio Snap de Sites --->
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td id="linhaSite">
                                                <componentart:Snap ID="SnapSite" runat="server" CurrentDockingContainer="linhaSite"
                                                    DockingContainers="linhaSite" Height="70" Width="160" DockingStyle="none" DraggingStyle="none"
                                                    ExpandCollapseMode="Animated" CurrentDockingIndex="0" MustBeDocked="True" IsCollapsed="false"
                                                    Visible="false">
                                                    <Footer></Footer>
                                                    <ContentTemplate>
                                                        <asp:DataGrid ID="dgSnapSites" runat="server" Width="100%" GridLines="Vertical" BorderColor="#8AAD8A"
                                                            BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False" OnItemCommand="dgSites_ItemCommand">
                                                            <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                                                            <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                                                            <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                                                            <Columns>
                                                                <asp:BoundColumn DataField="Key" HeaderText="Key" Visible="False"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="Descricao" HeaderText="&nbsp;Descri&ccedil;&atilde;o"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="url_Key" HeaderText="&nbsp;Site"></asp:BoundColumn>
                                                                <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_editar.gif' alt='Editar' border='0'&gt;"
                                                                    HeaderText="&lt;font size='1'&gt;COMA&lt;/font&gt;" CommandName="Edit">
                                                                    <HeaderStyle HorizontalAlign="Right" ForeColor="Black" Width="28px" BackColor="#8AAD8A">
                                                                    </HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:ButtonColumn>
                                                                <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_remover.gif' alt='Deletar' border='0'&gt;"
                                                                    HeaderText="&lt;font size='1'&gt;NDOS&lt;/font&gt;" CommandName="Delete">
                                                                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" Width="30px" BackColor="#8AAD8A">
                                                                    </HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:ButtonColumn>
                                                            </Columns>
                                                            <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                                        </asp:DataGrid>
                                                    </ContentTemplate>
                                                    <HeaderTemplate>
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" bgcolor="#a4c955"
                                                            style="cursor: hand" onclick="SnapSite.ToggleExpand();">
                                                            <tr>
                                                                <td class="txt_peq">
                                                                    <asp:Label ID="lblTituloSnapSit" runat="server">Lista de emails</asp:Label></td>
                                                                <td width="10" align="right">
                                                                    <img src="../imagens/botoes/btn_abrefecha.jpg" width="22" height="16" border="0"></td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                </componentart:Snap>
                                            </td>
                                        </tr>
                                    </table>
                                    <!--- Fim do Snap Sites --->
                                </td>
                            </tr>
                        </table>
                        <!-- Inicio da Panel de Tarifas -->
                        <asp:Panel ID="pnlTarifas" runat="server" Visible="False">
                            <table id="Table1" cellspacing="0" cellpadding="2" width="100%" border="0">
                                <tr>
                                    <td height="6">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="txt">
                                        <strong>Tarifas</strong>
                                        <table id="Table2" cellspacing="1" cellpadding="0" width="99%" bgcolor="#8aad8a"
                                            border="0">
                                            <tr>
                                                <td valign="top" width="48%">
                                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                                        <tr bgcolor="#f8f9ff">
                                                            <td class="txt_peq" valign="top" width="10%" bgcolor="#f8f9f6">
                                                                % Juros<br>
                                                                <input id="txtTarJuros_MOEDA" type="text" maxlength="5" value="0" name="txtTarJuros_MOEDA"
                                                                    runat="server">
                                                            </td>
                                                            <td class="txt_peq" valign="top" width="10%" bgcolor="#f8f9f6">
                                                                % Multa<br>
                                                                <input id="txtTarMulta_MOEDA" type="text" maxlength="5" value="0" name="txtTarMulta_MOEDA"
                                                                    runat="server">
                                                            </td>
                                                            <td class="txt_peq" valign="top" width="80%" bgcolor="#f8f9f6">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <!-- Inicio do Panel Financeiro -->
                        <asp:Panel ID="pnlDividas" runat="server" Visible="False">
                            <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                <tr>
                                    <td height="6">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="txt">
                                        <strong>Ficha Financeira</strong>
                                        <table cellspacing="1" cellpadding="0" width="99%" bgcolor="#8aad8a" border="0">
                                            <tr>
                                                <td valign="top" width="48%">
                                                    <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                                        <tr bgcolor="#f8f9ff">
                                                            <td class="txt_peq" valign="top" width="30%" bgcolor="#f8f9f6">
                                                                Tipo de Dívida<br>
                                                                <asp:DropDownList ID="ddlDivTipoDivida" runat="server" DataTextField="Descricao"
                                                                    DataValueField="ID">
                                                                </asp:DropDownList></td>
                                                            <td class="txt_peq" valign="top" width="30%" bgcolor="#f8f9f6">
                                                                Contrato<br>
                                                                <input id="txtDivContrato_TUD" type="text" maxlength="30" runat="server">
                                                            </td>
                                                            <td class="txt_peq" valign="top" width="10%" bgcolor="#f8f9f6">
                                                                Num. Doc.<br>
                                                                <input id="txtDivNumDoc_INT" type="text" maxlength="20" runat="server">
                                                            </td>
                                                            <td class="txt_peq" valign="top" width="15%" bgcolor="#f8f9f6">
                                                                Vencimento<br>
                                                                <input id="txtDivDataVencimento_DATA" type="text" runat="server">
                                                            </td>
                                                            <td class="txt_peq" valign="top" width="15%" bgcolor="#f8f9f6">
                                                                Valor Nominal<br>
                                                                <input id="txtDivValorNominal_MOEDA" style="text-align: right" type="text" runat="server">
                                                            </td>
                                                        </tr>
                                                        <tr bgcolor="#f8f9f6">
                                                            <td class="txt" valign="top" colspan="5">
                                                                <asp:LinkButton ID="lbtnParcelasAutomaticas" runat="server" Visible="False" ToolTip="Parcelamento Automático"
                                                                    OnClick="lbtnParcelasAutomaticas_Click">Parc.Auto&nbsp;</asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnMais_Dividas" runat="server" OnClick="LinkButton_Click">Mais</asp:LinkButton>
                                                                <asp:LinkButton ID="lbltnExcluirTudo" runat="server" Visible="False" OnClick="lbltnExcluirTudo_Click">&nbsp;Excluir Todas as Dívidas&nbsp;</asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnCancelar_Dividas" runat="server" Visible="False" OnClick="LinkButton_Click">Cancelar</asp:LinkButton></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <!--- Inicio Snap de Dívidas--->
                                        <table cellspacing="0" cellpadding="0" width="99%" border="0">
                                            <tr>
                                                <td id="linhaDividas">
                                                    <componentart:Snap ID="SnapDividas" runat="server" Visible="False" CurrentDockingContainer="linhaDividas"
                                                        DockingContainers="linhaDividas" Height="70" Width="160" DockingStyle="none"
                                                        DraggingStyle="none" ExpandCollapseMode="Animated" CurrentDockingIndex="0" MustBeDocked="True"
                                                        IsCollapsed="false">
                                                        <ContentTemplate>
                                                            <asp:DataGrid ID="dgSnapDividas" runat="server" Width="100%" GridLines="Vertical"
                                                                BorderColor="#8AAD8A" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
                                                                OnItemCommand="dgDividas_ItemCommand" OnItemDataBound="dgDividas_ItemDataBound">
                                                                <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                                                                <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                                                                <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                                                                <Columns>
                                                                    <asp:BoundColumn DataField="Key" HeaderText="Key" Visible="False"></asp:BoundColumn>
                                                                    <asp:BoundColumn HeaderText="Carteira"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="IdTipoDivida" HeaderText="Tipo da D&#237;vida"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="Contrato" HeaderText="Contrato"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="NumDoc" HeaderText="Num. Doc" DataFormatString="{0:000000}">
                                                                        <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="DataVencimento" HeaderText="Vencimento" DataFormatString="{0:dd/MM/yyyy}">
                                                                        <HeaderStyle Width="75px" HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="ValorNominal" HeaderText="Valor Nominal" DataFormatString="{0:C}">
                                                                        <HeaderStyle Width="80px"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn HeaderText="Baixa">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:BoundColumn>
                                                                    <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_editar.gif' alt='Editar' border='0'&gt;"
                                                                        HeaderText="&lt;font size='1'&gt;CO&lt;/font&gt;" CommandName="Edit">
                                                                        <HeaderStyle HorizontalAlign="Right" ForeColor="Black" Width="28px" BackColor="#8AAD8A">
                                                                        </HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:ButtonColumn>
                                                                    <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_remover.gif' alt='Deletar' border='0'&gt;"
                                                                        HeaderText="&lt;font size='1'&gt;MAND&lt;/font&gt;" CommandName="Delete">
                                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="Black" Width="30px" BackColor="#8AAD8A">
                                                                        </HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:ButtonColumn>
                                                                    <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/btn_Cheque.gif' alt='Baixar D&#237;vida' border='0'&gt;"
                                                                        HeaderText="&lt;font size='1'&gt;OS&lt;/font&gt;" CommandName="Baixar">
                                                                        <HeaderStyle HorizontalAlign="Left" ForeColor="Black" Width="30px" BackColor="#8AAD8A">
                                                                        </HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    </asp:ButtonColumn>
                                                                </Columns>
                                                                <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                                            </asp:DataGrid>
                                                        </ContentTemplate>
                                                        <HeaderTemplate>
                                                            <table cellspacing="0" cellpadding="0" border="0" width="100%" bgcolor="#a4c955"
                                                                style="cursor: hand" onclick="SnapDividas.ToggleExpand();">
                                                                <tr>
                                                                    <td class="txt_peq">
                                                                        <asp:Label ID="lbtnTituloSnapDivida" runat="server">Lista de Dívidas</asp:Label></td>
                                                                    <td width="10" align="right">
                                                                        <img src="../imagens/botoes/btn_abrefecha.jpg" width="22" height="16" border="0"></td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                    </componentart:Snap>
                                                </td>
                                            </tr>
                                        </table>
                                        <!--- Fim do Snap Dívidas --->
                                    </td>
                                </tr>
                                <tr>
                                    <td height="10">
                                    </td>
                                </tr>
                            </table>
                            <!---Snap de Acionamentos -->
                            <table cellspacing="0" cellpadding="0" width="99%" border="0">
                                <tr>
                                    <td id="LinhaAcionamentos">
                                        <componentart:Snap ID="SnapAcionamentos" runat="server" Visible="False" CurrentDockingContainer="LinhaAcionamentos"
                                            DockingContainers="LinhaAcionamentos" Height="70" Width="160" DockingStyle="none"
                                            DraggingStyle="none" ExpandCollapseMode="Animated" CurrentDockingIndex="0" MustBeDocked="True"
                                            IsCollapsed="true">
                                            <Footer></Footer>
                                            <ContentTemplate>
                                                <asp:DataGrid ID="dgAcionamentos" runat="server" Width="100%" AutoGenerateColumns="False"
                                                    CellPadding="0" BorderWidth="1px" BorderColor="#8AAD8A" GridLines="Vertical"
                                                    OnItemDataBound="dgAcionamentos_ItemDataBound">
                                                    <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                                                    <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                                                    <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                                                    <Columns>
                                                        <asp:BoundColumn DataField="IdUsuario" HeaderText="&#160;Usu&#225;rio">
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DataAcionamento" HeaderText="Acionamento" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                                                            <HeaderStyle HorizontalAlign="Center" Width="115px"></HeaderStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DataPromessa" HeaderText="Promessa" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle HorizontalAlign="Center" Width="75px"></HeaderStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="IdTipoAcionamento" HeaderText="Tipo Acionamento">
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle Wrap="False"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="TextoRespeito" HeaderText="Texto a Respeito" DataFormatString="{0:dd/MM/yyyy}">
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                                                </asp:DataGrid>
                                            </ContentTemplate>
                                            <HeaderTemplate>
                                                <table cellspacing="0" cellpadding="0" border="0" width="100%" bgcolor="#a4c955"
                                                    style="cursor: hand" onclick="SnapAcionamentos.ToggleExpand();">
                                                    <tr>
                                                        <td class="txt_peq">
                                                            <asp:Label ID="lblSnapAcionamentos" runat="server">Acionamentos Realizados</asp:Label></td>
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
                        <!-- Inicio da Tabela de Anotações Individuais -->
                        <table cellspacing="0" cellpadding="2" width="99%" border="0">
                            <tr height="6">
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td class="txt" valign="top" width="42%">
                                    Anotações Adicionais<br>
                                    <textarea id="txaAnotacoesAdicionais_TUD" name="txaAnotacoesAdicionais_TUD" rows="2"
                                        runat="server"></textarea>
                                </td>
                                <td class="txt" valign="top" width="42%">
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <br>
                        <!-- Inicio da tabela de Botões -->
                        <table cellspacing="0" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td valign="top">
                                    <asp:ImageButton ID="imgBtnSalvar" runat="server" Height="18" Width="71" ImageUrl="../imagens/botoes/btn_salvar.gif"
                                        BorderStyle="None" OnClick="imgBtnSalvar_Click"></asp:ImageButton>&nbsp;<a href="javascript:window.top.hidePopWin()"><img
                                            src="../imagens/botoes/btn_cancelar.gif" border="0"></a></td>
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
        </asp:Panel>
    </form>
</body>
</html>

<%@ Page Language="c#" Codebehind="Cadastro_Entidades.aspx.cs" AutoEventWireup="True"
    Inherits="NBCobranca.aspx.Cadastro_Entidades" %>

<%@ Register Assembly="NBWebControls" Namespace="NBWebControls" TagPrefix="cc1" %>
<%@ Register TagPrefix="componentart" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>.:: Neobridge Controle de Cobrança Web ::..</title>
    <link href="../estilos/links.css" rel="stylesheet">
    <link href="../estilos/texto.css" rel="stylesheet">
    <link href="../Styles/jquery.webui-popover.min.css" rel="stylesheet" />

    <script type="text/javascript" src="../scripts/navegador_css.js"></script>

    <script type="text/javascript" src="../scripts/mostra_esconde_campos.js"></script>

    <script type="text/javascript" src="../scripts/trata_campo.js"></script>



</head>
<body>
    <form id="frmClientesFornecedoresCadastro" method="post" runat="server">
        <cc1:MessageBox ID="MsgBox" runat="server" Largura="500" Altura="150" PastaStyles="../estilos"
            PastaScripts="../scripts" OnYesChoosed="MsgBox_YesChoosed" OnCloseModalChoosed="MsgBox_CloseModalChoosed" OnNoChoosed="MsgBox_NoChoosed" />
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr align="right" style="height:52px;">
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
                                            <asp:Label ID="lblTituloCadastro" runat="server" Text=""></asp:Label>
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
            <tr style="height:100%;">
                <td valign="top" width="12" background="../imagens/tela_peq_f_lateral.jpg">
                    <img height="25" src="../imagens/tela_peq_f_lateral.jpg" width="12">
                </td>
                <td valign="top" width="100%" colspan="2">
                    <asp:MultiView ID="mvCadastro" runat="server" ActiveViewIndex="0">
                        <asp:View ID="vCadastro" runat="server">
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
                                        <asp:TextBox ID="txtClienteFornecedor_STR" runat="server" AutoPostBack="True" OnTextChanged="VerificarCadastroExistente_TextChanged"></asp:TextBox>
                                    </td>
                                    <td class="txt" width="21%">
                                        CPF<br>
                                        <asp:TextBox ID="txtCPF_CPF" runat="server" MaxLength="40" AutoPostBack="true" OnTextChanged="VerificarCadastroExistente_TextChanged"></asp:TextBox>
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
                                                <td class="txt">
                                                    Razão Social<br>
                                                    <asp:TextBox ID="txtRazaoSocial_TUD" runat="server" AutoPostBack="True" OnTextChanged="VerificarCadastroExistente_TextChanged"></asp:TextBox></td>
                                                <td class="txt">
                                                    Nome Fantasia<br>
                                                    <input id="txtNomeFantasia_TUD" type="text" maxlength="50" name="txtNomeFantasia_TUD"
                                                        runat="server">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="txt">
                                                    CNPJ<br>
                                                    <asp:TextBox ID="txtCNPJ_CNPJ" runat="server" MaxLength="25" OnTextChanged="VerificarCadastroExistente_TextChanged"
                                                        AutoPostBack="true"></asp:TextBox>
                                                </td>
                                                <td class="txt">
                                                    Inscrição Estadual<br>
                                                    <input id="txtInscricaoEstadual_TUD" type="text" maxlength="15" name="txtInscricaoEstadual_TUD"
                                                        runat="server">
                                                </td>
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
                                                                    <option value="SC" selected="selected">SC</option>
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
                                                                <asp:LinkButton ID="lbtn_Novo_Endereco" runat="server" OnClick="LinkButton_Click">Mais</asp:LinkButton>&nbsp;
                                                                <asp:LinkButton ID="lbtn_Cancelar_Endereco" runat="server" Visible="False" OnClick="LinkButton_Click">Cancelar</asp:LinkButton>
                                                            </td>
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
                                                        IsCollapsed="false" Visible="false" EnableViewState="true">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gvSnap_Endereco" runat="server" Width="100%" GridLines="Vertical"
                                                                BorderColor="#8AAD8A" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
                                                                DataKeyNames="UniqueID" OnRowDeleting="Colecao_RowDeleting" OnRowEditing="Colecao_RowEditing">
                                                                <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19" />
                                                                <RowStyle CssClass="dg_item" BackColor="#E9EFDD" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Logradouro" HeaderText="Endereco" />
                                                                    <asp:BoundField DataField="Complemento" HeaderText="Complemento" />
                                                                    <asp:BoundField DataField="Bairro" HeaderText="Bairro" />
                                                                    <asp:BoundField DataField="Municipio" HeaderText="Cidade" />
                                                                    <asp:BoundField DataField="UF" HeaderText="UF">
                                                                        <HeaderStyle Width="30px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Contato" HeaderText="Contato" />
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <center>
                                                                                COMANDOS
                                                                            </center>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <center>
                                                                                <asp:ImageButton ID="imgBtnEditar" runat="server" ImageUrl="../imagens/botoes/c_editar.gif"
                                                                                    CommandName="Edit" ForeColor="Black" Width="15px" Height="16px" />
                                                                                &nbsp;
                                                                                <asp:ImageButton ID="imgBtnRemover" runat="server" ImageUrl="../imagens/botoes/c_remover.gif"
                                                                                    CommandName="Delete" ForeColor="Black" Width="15px" Height="16px" />
                                                                            </center>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="50px" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
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
                                                                <asp:LinkButton ID="lbtn_Novo_Telefone" runat="server" OnClick="LinkButton_Click">Mais</asp:LinkButton>&nbsp;
                                                                <asp:LinkButton ID="lbtn_Cancelar_Telefone" runat="server" Visible="False" OnClick="LinkButton_Click">Cancelar</asp:LinkButton></td>
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
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gvSnap_Telefone" runat="server" Width="100%" GridLines="Vertical"
                                                                BorderColor="#8AAD8A" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
                                                                OnRowDeleting="Colecao_RowDeleting" OnRowEditing="Colecao_RowEditing" DataKeyNames="UniqueID">
                                                                <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19" />
                                                                <RowStyle CssClass="dg_item" BackColor="#E9EFDD" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="descricao" HeaderText="Descri&#231;&#227;o" />
                                                                    <asp:BoundField DataField="contato" HeaderText="Contato" />
                                                                    <asp:BoundField DataField="ddd" HeaderText="DDD">
                                                                        <HeaderStyle Width="30px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="fone" HeaderText="Fone">
                                                                        <HeaderStyle Width="70px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ramal" HeaderText="Ramal">
                                                                        <HeaderStyle Width="40px" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <center>
                                                                                COMANDOS
                                                                            </center>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <center>
                                                                                <asp:ImageButton ID="imgBtnEditar" runat="server" ImageUrl="../imagens/botoes/c_editar.gif"
                                                                                    CommandName="Edit" ForeColor="Black" Width="15px" Height="16px" />
                                                                                &nbsp;
                                                                                <asp:ImageButton ID="imgBtnRemover" runat="server" ImageUrl="../imagens/botoes/c_remover.gif"
                                                                                    CommandName="Delete" ForeColor="Black" Width="15px" Height="16px" />
                                                                            </center>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="50px" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
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
                                                                <asp:LinkButton ID="lbtn_Novo_Email" runat="server" OnClick="LinkButton_Click">Mais</asp:LinkButton>&nbsp;
                                                                <asp:LinkButton ID="lbtn_Cancelar_Email" runat="server" Visible="False" OnClick="LinkButton_Click">Cancelar</asp:LinkButton></td>
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
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gvSnap_Email" runat="server" CellPadding="0" BorderWidth="1px"
                                                                GridLines="Vertical" AutoGenerateColumns="False" BorderColor="#DDE4DA" Width="100%"
                                                                ShowFooter="True" OnRowDeleting="Colecao_RowDeleting" OnRowEditing="Colecao_RowEditing"
                                                                DataKeyNames="UniqueID">
                                                                <RowStyle CssClass="dg_item" BackColor="#E9EFDD" />
                                                                <HeaderStyle CssClass="dg_header"></HeaderStyle>
                                                                <FooterStyle CssClass="dg_header_peq dg_header" ForeColor="Black" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Descricao" HeaderText="&#160;Descri&#231;&#227;o" />
                                                                    <asp:BoundField DataField="eMail" HeaderText="&#160;E-mail" />
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <center>
                                                                                COMANDOS
                                                                            </center>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <center>
                                                                                <asp:ImageButton ID="imgBtnEditar" runat="server" ImageUrl="../imagens/botoes/c_editar.gif"
                                                                                    CommandName="Edit" ForeColor="Black" Width="15px" Height="16px" />
                                                                                &nbsp;
                                                                                <asp:ImageButton ID="imgBtnRemover" runat="server" ImageUrl="../imagens/botoes/c_remover.gif"
                                                                                    CommandName="Delete" ForeColor="Black" Width="15px" Height="16px" />
                                                                            </center>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="50px" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
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
                                                                <asp:LinkButton ID="lbtn_Novo_Site" runat="server" OnClick="LinkButton_Click">Mais</asp:LinkButton>&nbsp;
                                                                <asp:LinkButton ID="lbtn_Cancelar_Site" runat="server" Visible="False" OnClick="LinkButton_Click">Cancelar</asp:LinkButton></td>
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
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gvSnap_Site" runat="server" CellPadding="0" BorderWidth="1px" GridLines="Vertical"
                                                                AutoGenerateColumns="False" BorderColor="#DDE4DA" Width="100%" ShowFooter="True"
                                                                OnRowDeleting="Colecao_RowDeleting" OnRowEditing="Colecao_RowEditing" DataKeyNames="UniqueID">
                                                                <RowStyle CssClass="dg_item" BackColor="#E9EFDD" />
                                                                <HeaderStyle CssClass="dg_header"></HeaderStyle>
                                                                <FooterStyle CssClass="dg_header_peq dg_header" ForeColor="Black" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Descricao" HeaderText="&#160;Descri&#231;&#227;o"></asp:BoundField>
                                                                    <asp:BoundField DataField="url" HeaderText="&#160;Site"></asp:BoundField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <center>
                                                                                COMANDOS
                                                                            </center>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <center>
                                                                                <asp:ImageButton ID="imgBtnEditar" runat="server" ImageUrl="../imagens/botoes/c_editar.gif"
                                                                                    CommandName="Edit" ForeColor="Black" Width="15px" Height="16px" />
                                                                                &nbsp;
                                                                                <asp:ImageButton ID="imgBtnRemover" runat="server" ImageUrl="../imagens/botoes/c_remover.gif"
                                                                                    CommandName="Delete" ForeColor="Black" Width="15px" Height="16px" />
                                                                            </center>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="50px" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
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
                            <asp:Panel ID="pnlDividas" runat="server" Visible="false">
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
                                                                        DataValueField="ID" Enabled="false">
                                                                    </asp:DropDownList></td>
                                                                <td class="txt_peq" valign="top" width="30%" bgcolor="#f8f9f6">
                                                                    Contrato<br>
                                                                    <input id="txtDivContrato_TUD" type="text" maxlength="30" runat="server" disabled="disabled">
                                                                </td>
                                                                <td class="txt_peq" valign="top" width="10%" bgcolor="#f8f9f6">
                                                                    Num. Doc.<br>
                                                                    <input id="txtDivNumDoc_INT" type="text" maxlength="20" runat="server" disabled="disabled">
                                                                </td>
                                                                <td class="txt_peq" valign="top" width="15%" bgcolor="#f8f9f6">
                                                                    Vencimento<br>
                                                                    <input id="txtDivDataVencimento_DATA" type="text" runat="server" disabled="disabled">
                                                                </td>
                                                                <td class="txt_peq" valign="top" width="15%" bgcolor="#f8f9f6">
                                                                    Valor Nominal<br>
                                                                    <input id="txtDivValorNominal_MOEDA" style="text-align: right" type="text" runat="server"
                                                                        disabled="disabled">
                                                                </td>
                                                            </tr>
                                                            <tr bgcolor="#f8f9f6">
                                                                <td class="txt" valign="top" colspan="5">
                                                                    <asp:LinkButton ID="lbtn_AutoParcelar_Dividas" runat="server" Visible="False" ToolTip="Parcelamento Automático"
                                                                        OnClick="LinkButton_Click">Parc.Auto&nbsp;</asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtn_Novo_Dividas" runat="server" OnClick="LinkButton_Click">Mais</asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtn_Excluir_Dividas" runat="server" Visible="False" OnClick="LinkButton_Click">&nbsp;Excluir Todas as Dívidas&nbsp;</asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtn_Cancelar_Dividas" runat="server" Visible="False" OnClick="LinkButton_Click">Cancelar</asp:LinkButton></td>
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
                                                                <asp:GridView ID="gvSnap_Dividas" runat="server" CellPadding="0" BorderWidth="1px"
                                                                    GridLines="Vertical" AutoGenerateColumns="False" BorderColor="#DDE4DA" Width="100%"
                                                                    ShowFooter="True" OnRowCommand="gvSnap_Dividas_RowCommand" OnRowDeleting="Colecao_RowDeleting"
                                                                    OnRowEditing="Colecao_RowEditing" DataKeyNames="UniqueID" OnRowDataBound="gvSnap_Dividas_RowDataBound">
                                                                    <AlternatingRowStyle CssClass="dg_item" BackColor="#F9FCF3" />
                                                                    <RowStyle CssClass="dg_item" BackColor="#E9EFDD" />
                                                                    <HeaderStyle CssClass="dg_header"></HeaderStyle>
                                                                    <FooterStyle CssClass="dg_header_peq dg_header" ForeColor="Black" />
                                                                    <Columns>
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
                                                                        <asp:BoundField DataField="ImageStatus" HeaderText="Baixa" HtmlEncode="False" HtmlEncodeFormatString="False">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <center>
                                                                                    COMANDOS
                                                                                </center>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <center>
                                                                                    <asp:ImageButton ID="imgBtnEditar" runat="server" ImageUrl="../imagens/botoes/c_editar.gif"
                                                                                        CommandName="Edit" ForeColor="Black" Width="15px" Height="16px" />
                                                                                    <asp:ImageButton ID="imgBtnRemover" runat="server" ImageUrl="../imagens/botoes/c_remover.gif"
                                                                                        CommandName="Delete" ForeColor="Black" Width="15px" Height="16px" />
                                                                                    <asp:ImageButton ID="imgBtnBaixar" runat="server" ImageUrl="../imagens/botoes/btn_Cheque.gif"
                                                                                        CommandName="Baixar" CommandArgument='<%# Container.DisplayIndex %>' ForeColor="Black"
                                                                                        Width="15px" Height="16px" />
                                                                                </center>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                            <HeaderTemplate>
                                                                <table cellspacing="0" cellpadding="0" border="0" width="100%" bgcolor="#a4c955"
                                                                    style="cursor: hand" onclick="SnapDividas.ToggleExpand();">
                                                                    <tr>
                                                                        <td class="txt_peq">
                                                                            <asp:Label ID="lblTituloSnapDiv" runat="server">Lista de Dívidas</asp:Label></td>
                                                                        <td width="10" align="right">
                                                                            <img alt="" src="../imagens/botoes/btn_abrefecha.jpg" width="22" height="16" border="0" /></td>
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
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvSnap_Acionamentos" runat="server" CellPadding="0" BorderWidth="1px"
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
                                <tr>
                                    <td class="txt" valign="top">
                                        Anotações Adicionais<br>
                                        <textarea id="txaAnotacoesAdicionais_TUD" name="txaAnotacoesAdicionais_TUD" rows="2"
                                            runat="server"></textarea>
                                    </td>
                                </tr>
                            </table>
                            <br>
                            <!-- Inicio da tabela de Botões -->
                            <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                <tr>
                                    <td valign="top">
                                        <asp:ImageButton ID="imgBtnSalvar" runat="server" Height="18" Width="71" ImageUrl="../imagens/botoes/btn_salvar.gif"
                                            BorderStyle="None" OnClick="imgBtnSalvar_Click"></asp:ImageButton>&nbsp; <a href="javascript:window.top.hidePopWin()">
                                                <img src="../imagens/botoes/btn_cancelar.gif" border="0"></a></td>
                                </tr>
                            </table>
                            <table cellspacing="0" cellpadding="0" width="550" border="0">
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="vBaixaUnica" runat="server">
                            <div style="width: 100%; height: 250px; margin-top: 50px; text-align: center;">
                                <table id="tblPF_Baixa" cellspacing="0" cellpadding="2" width="99%" border="0" runat="server"
                                    visible="false">
                                    <tr>
                                        <td class="txt">
                                            <asp:Label ID="lblNomeEntidade_Baixa" runat="server"></asp:Label><br>
                                            <asp:TextBox ID="txtClienteFornecedor_Baixa" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td class="txt">
                                            CPF<br>
                                            <input id="txtCPF_Baixa" type="text" name="txtCPF_Baixa" runat="server" readonly="readonly">
                                        </td>
                                        <td class="txt">
                                            RG<br>
                                            <input id="txtRG_Baixa" type="text" name="txtRG_Baixa" runat="server" readonly="readonly">
                                        </td>
                                    </tr>
                                </table>
                                <table id="tblPJ_Baixa" cellspacing="0" cellpadding="0" width="99%" border="0" runat="server"
                                    visible="false">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                                <tr>
                                                    <td class="txt">
                                                        Razão Social<br>
                                                        <asp:TextBox ID="txtRazaoSocial_Baixa" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                    <td class="txt">
                                                        Nome Fantasia<br>
                                                        <input id="txtNomeFantasia_Baixa" type="text" name="txtNomeFantasia_Baixa" runat="server"
                                                            readonly="readonly">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="txt">
                                                        CNPJ<br>
                                                        <input id="txtCNPJ_Baixa" type="text" name="txtCNPJ_Baixa" runat="server" readonly="readonly">
                                                    </td>
                                                    <td class="txt">
                                                        Inscrição Estadual<br>
                                                        <input id="txtInscricaoEstadual_Baixa" type="text" name="txtInscricaoEstadual_Baixa"
                                                            runat="server" readonly="readonly">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:GridView ID="gvDividaBaixar" runat="server" CellPadding="0" BorderWidth="1px"
                                    GridLines="Vertical" AutoGenerateColumns="False" BorderColor="#DDE4DA" Width="100%"
                                    ShowFooter="true">
                                    <AlternatingRowStyle CssClass="dg_item" BackColor="#F9FCF3" />
                                    <RowStyle CssClass="dg_item" BackColor="#E9EFDD" />
                                    <HeaderStyle CssClass="dg_header"></HeaderStyle>
                                    <FooterStyle CssClass="dg_header_peq dg_header" ForeColor="black" />
                                    <Columns>
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
                                <br />
                                <table cellpadding="2" cellspacing="0" class="txt">
                                    <tr>
                                        <td colspan="6">
                                            <table class="txt">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="ckbPagouNoCliente" runat="server" AutoPostBack="True" Text="Pagou no Cliente"
                                                            OnCheckedChanged="ckbPagouNoCliente_CheckedChanged" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="ckbBaixaParcial" runat="server" AutoPostBack="True" Text="Baixa Parcial"
                                                            OnCheckedChanged="ckbBaixaParcial_CheckedChanged" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td>
                                            Cobrador:&nbsp;
                                            <asp:DropDownList ID="ddlCobrador" runat="server" DataTextField="Login" DataValueField="idUsuario"
                                                Width="100px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            Comissão:</td>
                                        <td>
                                            <asp:TextBox ID="txtPerComissao_MOEDA" runat="server" MaxLength="7" Style="text-align: right"
                                                Width="50px">0,00</asp:TextBox>%
                                        </td>
                                        <td align="right">
                                            Bordero:</td>
                                        <td>
                                            <asp:TextBox ID="txtBordero_INT" runat="server" MaxLength="5" Width="40px">0</asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Data de Baixa:</td>
                                        <td>
                                            <asp:TextBox ID="txtDataBaixa_DATA" runat="server" Width="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td colspan="2" align="right">
                                            Recibo:</td>
                                        <td>
                                            <asp:TextBox ID="txtNumRecibo_INT" runat="server" Width="50px">0</asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Valor Baixa:</td>
                                        <td>
                                            <asp:TextBox ID="txtValorBaixa_MOEDA" runat="server" ReadOnly="True" Style="text-align: right"
                                                Width="60px" AutoPostBack="True" OnTextChanged="txtValorBaixa_MOEDA_TextChanged">0,00</asp:TextBox>
                                        </td>
                                        <td align="right">
                                            Valor Recebido:</td>
                                        <td>
                                            <asp:TextBox ID="txtValorRecebido_MOEDA" runat="server" Style="text-align: right"
                                                Width="60px">0,00</asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <asp:Button ID="btnCancelarBaixa" runat="server" Width="71px" Text="Cancelar" OnClick="btnCancelarBaixa_Click" />
                                &nbsp;
                                <asp:Button ID="btnBaixar" runat="server" Width="50px" Text="Baixar" OnClick="btnBaixar_Click" />
                            </div>
                        </asp:View>
                    </asp:MultiView>
                </td>
            </tr>
        </table>
    </form>
    <script language="javascript" src="../scripts/jquery-2.1.4.min.js"></script>
    <script language="javascript" src="../scripts/jquery.webui-popover.min.js"></script>
    <script src="../scripts/configuraPopOver.js"></script>
</body>
</html>

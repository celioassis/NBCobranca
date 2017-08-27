<%@ Page Language="c#" Codebehind="devedores.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.aspx.devedores"
    MasterPageFile="~/aspx/LayoutPagina.Master" Title=".:: Neobridge Controle de Cobrança Web - Filtro para Devedores ::." %>

<%@ Register TagPrefix="cc2" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<%@ Register TagPrefix="componentart" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phConteudo" runat="server">
    <cc2:MessageBox ID="MessageBox" runat="server" PastaStyles="../estilos" PastaScripts="../Scripts"
        OnYesChoosed="MessageBox_YesChoosed" OnCloseModalChoosed="MessageBox_CloseModalChoosed">
    </cc2:MessageBox>
    <asp:ObjectDataSource ID="ObjDS_Devedores" runat="server" EnablePaging="True" OnObjectCreating="ObjDS_Devedores_ObjectCreating"
        SelectCountMethod="TotalDevedoresPesquisados" SelectMethod="PesquisaDevedores"
        StartRowIndexParameterName="pPagina" TypeName="NBCobranca.Controllers.ctrDevedores"
        MaximumRowsParameterName="pLinhasPorPagina"></asp:ObjectDataSource>
    <div id="Carteiras" class="Borda" style="float: left; width:20%; margin-right:2px">
        <componentart:TreeView ID="TreeView1" runat="server" Width="100%" Height="400px" CssClass="TreeView"
            ShowLines="True" SelectedNodeCssClass="SelectedTreeNode" CutNodeCssClass="TreeNode"
            HoverNodeCssClass="HoverTreeNode" LineImageHeight="20" LineImageWidth="19" LineImagesFolderUrl="../imagens/arvore/lines"
            ParentNodeImageUrl="../imagens/arvore/folders.gif" NodeLabelPadding="3" DefaultImageHeight="16"
            DefaultImageWidth="16" NodeCssClass="TreeNode" LeafNodeImageUrl="../imagens/arvore/folder.gif"
            AutoPostBackOnSelect="True" OnNodeSelected="TreeView1_NodeSelected">
        </componentart:TreeView>
    </div>
    <div id="PesquisaDevedores" style="float:left; width:79%">
        <div id="Pesquisa" class="Borda PesquisaComCadastro">
            <div id="pesquisaCabecalho">
                <img alt="" height="18" src="../imagens/pesquisar.gif" width="84" />
            </div>
            <div id="PesquisaFiltros">
                <div class="Filtro" style="width: 230px">
                    <strong>Campo a procurar</strong><br />
                    <asp:DropDownList ID="selProcurarCampo" runat="server" onChange="limpatxtProcurar()">
                        <asp:ListItem Value="1">ID</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">Nome</asp:ListItem>
                        <asp:ListItem Value="3">CPF/CNPJ</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="Filtro" style="width: 230px">
                    <strong>O que deseja procurar</strong><br />
                    <input onkeypress="return vCampoProcurar(ctl00_phConteudo_selProcurarCampo,event)" id="txtProcurar"
                        type="text" name="txtProcurar" runat="server" />
                </div>
                <div class="Filtro" style="width: 130px">
                    <br />
                    <asp:ImageButton ID="imgBtnPesquisar" BorderStyle="None" Height="18" Width="79" ImageUrl="../imagens/botoes/btn_pesquisar.gif"
                        runat="server" OnClick="imgBtnPesquisar_Click"></asp:ImageButton>
                </div>
            </div>
        </div>
        <div class="Borda PesquisaCadastrar">
            <div id="cadcima" class="txt_peq">
                <strong>Gerenciador de devedores</strong></div>
            <div id="cadbaixo">
                <br />
                <asp:ImageButton ID="imgBtnNovoDevedor" BorderStyle="None" Height="25px" Width="93px"
                    ImageUrl="~/imagens/botoes/btn_novo_devedor.gif" runat="server" OnClick="imgBtnNovoDevedor_Click">
                </asp:ImageButton>
            </div>
        </div>
        <div id="pesquisaResultado">
            <asp:GridView ID="gvDados" runat="server" CellPadding="0" BorderWidth="1px" GridLines="Vertical"
                AutoGenerateColumns="False" BorderColor="#DDE4DA" Width="100%" AllowPaging="True"
                OnRowCommand="gvDados_RowCommand" OnRowEditing="gvDados_RowEditing" OnPageIndexChanging="gvDados_PageIndexChanging"
                OnRowDeleting="gvDados_RowDeleting" DataKeyNames="IdEntidade,NomePrimary">
                <AlternatingRowStyle CssClass="dg_item" BackColor="#F9FCF3" />
                <RowStyle CssClass="dg_item" BackColor="#E9EFDD" />
                <HeaderStyle CssClass="dg_header"></HeaderStyle>
                <PagerStyle CssClass="dg_header" HorizontalAlign="Left" />
                <Columns>
                    <asp:BoundField DataField="IdEntidade" HeaderText="&#160;C&#243;digo" DataFormatString="{0:d6}">
                        <HeaderStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NomePrimary" HeaderText="&#160;Nome">
                        <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Logradouro" HeaderText="&#160;Endere&#231;o" />
                    <asp:BoundField DataField="Municipio" HeaderText="&#160;Cidade">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UF" HeaderText="&#160;UF">
                        <HeaderStyle Width="25px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="COMANDOS" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lkbtnEditar" runat="server" CausesValidation="false" CommandName="Edit"
                                Text="<img src='../imagens/botoes/c_editar.gif' alt='Editar' border='0'>"></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lkbtnDeletar" runat="server" CausesValidation="false" CommandName="Delete"
                                Text="<img src='../imagens/botoes/c_remover.gif' alt='Excluir devedor' border='0'>"></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lkbtnBaixar" runat="server" CausesValidation="false" CommandName="Baixar"
                                CommandArgument='<%# Container.DisplayIndex %>' Text="<img src='../imagens/botoes/btn_Cheque.gif' alt='Baixar Dívida' border='0'>"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle CssClass="dg_header" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>
                    <asp:Button ID="btnPrimeiro" runat="server" Text="|<" CommandName="Page" CommandArgument="First"
                        Width="25px" CssClass="botao" />
                    <asp:Button ID="btnAnterior" runat="server" Text="<" CommandName="Page" CommandArgument="Prev"
                        Width="25px" CssClass="botao" />
                    Página&nbsp;
                    <asp:TextBox ID="txtPage_INT" runat="server" Text="<% #gvDados.PageIndex + 1 %>"
                        AutoPostBack="True" OnTextChanged="txtPage_INT_TextChanged" Width="25px" BackColor="#EDF8D5"></asp:TextBox>
                    &nbsp;de&nbsp;
                    <asp:Label ID="lbTotalPaginas" runat="server" Text="<% #gvDados.PageCount %>"></asp:Label>
                    <asp:Button ID="btnProximo" runat="server" Text=">" CommandName="Page" CommandArgument="Next"
                        Width="25px" CssClass="botao" />
                    <asp:Button ID="btnUltimo" runat="server" Text=">|" CommandName="Page" CommandArgument="Last"
                        Width="25px" CssClass="botao" />
                </PagerTemplate>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

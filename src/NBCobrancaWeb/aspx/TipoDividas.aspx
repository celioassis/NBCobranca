<%@ Page Language="c#" Codebehind="TipoDividas.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.aspx.TipoDivida"
    MasterPageFile="~/aspx/LayoutPagina.Master" Title=".:: Neobridge Controle de Cobrança Web - Filtro para Tipo de Dívidas ::." %>

<%@ Register TagPrefix="cc1" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phConteudo" runat="server">
    <cc1:MessageBox ID="MessageBox" runat="server" PastaScripts="../scripts" PastaStyles="../estilos"
        Altura="130"></cc1:MessageBox>
    <div id="Pesquisa" class="Borda PesquisaComCadastro">
        <div id="pesquisaCabecalho">
            <img alt="" height="18" src="../imagens/pesquisar.gif" width="84" />
        </div>
        <div id="PesquisaFiltros">
            <div class="Filtro" style="width: 130px">
                Campo a procurar<br />
                <select id="selProcurarCampo" onchange="limpatxtProcurar()" name="selProcurarCampo"
                    runat="server">
                    <option value="1">Código</option>
                    <option value="5" selected="selected">Descrição</option>
                </select>
            </div>
            <div class="Filtro" style="width: 200px">
                O que deseja procurar<br />
                <input onkeypress="return vCampoProcurar(ctl00_phConteudo_selProcurarCampo,event)" id="txtProcurar"
                    type="text" name="txtProcurar" runat="server" />
            </div>
            <div class="Filtro">
                <br />
                <asp:ImageButton ID="imgBtnPesquisar" runat="server" ImageUrl="../imagens/botoes/btn_pesquisar.gif"
                    Height="18px" Width="79px" BorderStyle="None"></asp:ImageButton>
            </div>
        </div>
    </div>
    <div class="Borda PesquisaCadastrar">
        <div id="cadcima">
            <img alt="" height="18" src="../imagens/cadastrar.gif" width="88" />
        </div>
        <div id="cadbaixo">
            Para adicionar novo <span class="destacar">Tipo de Dívida</span> clique no botão:<br />
            <asp:ImageButton ID="imgBtnAdicionar" runat="server" ImageUrl="../imagens/botoes/btn_adicionar.gif"
                Height="18px" Width="79px" BorderStyle="None"></asp:ImageButton></div>
    </div>
    <div id="pesquisaResultado">
        <asp:DataGrid ID="dgDados" runat="server" CellPadding="0" BorderWidth="1px" BorderColor="#DDE4DA"
            AutoGenerateColumns="False" Width="100%" GridLines="Vertical">
            <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
            <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
            <HeaderStyle CssClass="dg_header" BackColor="YellowGreen"></HeaderStyle>
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="&#160;C&#243;digo" DataFormatString="{0:d6}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Descricao" HeaderText="Descri&#231;&#227;o"></asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_remover.gif' alt='Deletar' border='0'&gt;"
                    HeaderText="&lt;font size='1'&gt;COMANDOS&lt;/font&gt;" CommandName="Delete">
                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" Width="30px" BackColor="#E0E0E0">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
        </asp:DataGrid>
    </div>
</asp:Content>

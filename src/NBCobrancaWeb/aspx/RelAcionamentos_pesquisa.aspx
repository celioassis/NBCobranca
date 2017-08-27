<%@ Page Language="c#" Codebehind="RelAcionamentos_pesquisa.aspx.cs" AutoEventWireup="True"
    Inherits="NBCobranca.aspx.RelAcionamentosPesquisa_pesquisa" MasterPageFile="~/aspx/LayoutPagina.Master"
    Title=".:: Neobridge Controle de Cobrança Web - Filtro para Relatório de Acionamentos ::." %>

<%@ Register TagPrefix="cc1" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phConteudo" runat="server">

    <script type="text/javascript" src="../scripts/trata_campo.js"></script>

    <script type="text/javascript" src="../scripts/relatorio.js"></script>

    <cc1:MessageBox ID="MessageBox" runat="server" PastaStyles="../Estilos" PastaScripts="../Scripts">
    </cc1:MessageBox>
    <div id="Pesquisa" class="Borda">
        <div id="pesquisaCabecalho">
            <img alt="" height="18" src="../imagens/pesquisar.gif" width="84" />
        </div>
        <div id="PesquisaFiltros">
            <div class="Filtro" style="width: 130px">
                Carteiras<br />
                <asp:DropDownList ID="ddlCarteiras" runat="server" DataValueField="Nome" DataTextField="Nome">
                </asp:DropDownList>
            </div>
            <!-- -->
            <div class="Filtro" style="width: 130px">
                Tipo de Acionamento<br />
                <asp:DropDownList ID="ddlTipoAcionamento" runat="server" DataValueField="ID" DataTextField="Descricao">
                </asp:DropDownList>
            </div>
            <!-- -->
            <div class="Filtro" style="width: 130px">
                Acionadores<br />
                <asp:DropDownList ID="ddlAcionadores" runat="server" DataValueField="idUsuario" DataTextField="Login">
                </asp:DropDownList>
            </div>
            <!-- -->
            <div class="Filtro" style="width: 260px">
                <center>
                    Período de Acionamentos</center>
                Data Inicial:&nbsp;<asp:TextBox ID="txtDataInicial_DATA" runat="server" Width="65px"></asp:TextBox>
                Data Final:&nbsp;<asp:TextBox ID="txtDataFinal_DATA" runat="server" Width="65px"></asp:TextBox>
            </div>
            <div class="Filtro">
                <br />
                <asp:ImageButton ID="imgBtnPesquisar" BorderStyle="None" Width="79px" Height="18px"
                    ImageUrl="../imagens/botoes/btn_pesquisar.gif" runat="server"></asp:ImageButton>
                &nbsp;<img id="imgBtnPrint" style="visibility: hidden; cursor: hand" onclick="VisualizaRelatorio('Relatorios/rel_acionamentos.aspx','_Blank');"
                    alt="Visualiza a Impressão da Pesquisa" src="../imagens/botoes/btn_imprimir.gif"
                    runat="server" />
            </div>
        </div>
    </div>
    <div id="pesquisaResultado">
        <asp:DataGrid ID="dgDados" runat="server" Width="100%" PageSize="15" GridLines="Vertical"
            AutoGenerateColumns="False" BorderColor="#DDE4DA" BorderWidth="1px" CellPadding="0"
            ShowFooter="True" AllowPaging="True" OnItemCommand="dgDados_ItemCommand">
            <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
            <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
            <HeaderStyle CssClass="dg_header" BackColor="YellowGreen"></HeaderStyle>
            <Columns>
                <asp:BoundColumn DataField="idEntidade" HeaderText="&#160;C&#243;digo" DataFormatString="{0:d6}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NomePrimary" HeaderText="&#160;Nome Devedor"></asp:BoundColumn>
                <asp:BoundColumn DataField="TipoDivida" HeaderText="&#160;Tipo da D&#237;vida">
                    <HeaderStyle Width="140px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TipoAcionamento" HeaderText="&#160;Tipo do Ultimo Acionamento">
                    <HeaderStyle Width="200px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="DataPromessa" HeaderText="&#160;Promessa"
                    DataFormatString="{0:dd/MM/yyy}">
                    <HeaderStyle Width="70px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_dados_cadastrais.gif' alt='Acionar Devedor' border='0'&gt;"
                    HeaderText="&lt;font size='1'&gt;COMANDOS&lt;/font&gt;" CommandName="Acionar">
                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" Width="30px" BackColor="#E0E0E0">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Left" BackColor="YellowGreen" PageButtonCount="15" CssClass="dg_header">
            </PagerStyle>
            <FooterStyle BackColor="YellowGreen" CssClass="dg_header"></FooterStyle>
        </asp:DataGrid>
    </div>
</asp:Content>

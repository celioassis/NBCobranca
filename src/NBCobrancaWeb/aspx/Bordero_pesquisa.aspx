<%@ Page Language="c#" Codebehind="Bordero_pesquisa.aspx.cs" AutoEventWireup="True"
    Inherits="NBCobranca.aspx.Bordero_pesquisa" MasterPageFile="~/aspx/LayoutPagina.Master"
    Title=".:: Neobridge Controle de Cobrança Web - Relatório de Borderos ::." %>

<%@ Register TagPrefix="nbw" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phConteudo" runat="server">

    <script type="text/javascript" src="../scripts/trata_campo.js"></script>

    <script type="text/javascript" src="../scripts/relatorio.js"></script>

    <nbw:MessageBox ID="MessageBox" runat="server" PastaScripts="../Scripts" PastaStyles="../Styles">
    </nbw:MessageBox>
    <div id="Pesquisa" class="Borda">
        <div id="pesquisaCabecalho">
            <img alt="" height="18" src="../imagens/pesquisar.gif" width="84" />
        </div>
        <div id="PesquisaFiltros">
            <div class="Filtro" style="width: 130px">
                Carteiras<br />
                <asp:DropDownList ID="ddlCarteiras" runat="server" DataTextField="Nome" DataValueField="Nome">
                </asp:DropDownList>
            </div>
            <div class="Filtro" style="width: 260px;">
                <center>
                    Período das Baixas
                </center>
                Data Inicial:&nbsp;<asp:TextBox ID="txtDataInicial_DATA" runat="server" Width="65px"></asp:TextBox>
                Data Final:&nbsp;<asp:TextBox ID="txtDataFinal_DATA" runat="server" Width="65px"></asp:TextBox>
            </div>
            <!-- -->
            <div class="Filtro" style="width: 80px">
                Bordero<br />
                <asp:TextBox ID="txtNumBordero_INT" runat="server" Width="65px"></asp:TextBox>
            </div>
            <div class="Filtro">
                <br />
                <asp:ImageButton ID="imgBtnPesquisar" runat="server" ImageUrl="../imagens/botoes/btn_pesquisar.gif"
                    Height="18px" Width="79px" BorderStyle="None"></asp:ImageButton>
                &nbsp;<img id="imgBtnPrint"
                        style="visibility: hidden; cursor: hand" onclick="VisualizaRelatorio('Relatorios/BorderoPdf.aspx','_Blank');"
                        alt="Visualiza a Impressão do Bordero" src="../imagens/botoes/btn_imprimir.gif"
                        runat="server" />
                &nbsp;<asp:Button ID="btnResumo" runat="server" Text="Resumo" Width="66px" Visible="False"
                    OnClick="btnResumo_Click"></asp:Button>
            </div>
        </div>
    </div>
    <div id="pesquisaResultado">
        <asp:DataGrid ID="dgDados" runat="server" Width="100%" ShowFooter="True" CellPadding="0"
            BorderWidth="1px" BorderColor="#DDE4DA" AutoGenerateColumns="False" GridLines="Vertical"
            AllowCustomPaging="True" PageSize="15" OnItemCommand="dgDados_ItemCommand">
            <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
            <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
            <HeaderStyle CssClass="dg_header" BackColor="YellowGreen"></HeaderStyle>
            <Columns>
                <asp:BoundColumn DataField="Carteira" HeaderText="&#160;Carteira"></asp:BoundColumn>
                <asp:BoundColumn DataField="CodigoDevedor" HeaderText="&#160;C&#243;digo" DataFormatString="{0:d6}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NomeDevedor" HeaderText="&#160;Nome Devedor"></asp:BoundColumn>
                <asp:BoundColumn DataField="Contrato" HeaderText="&#160;Contrato"></asp:BoundColumn>
                <asp:BoundColumn DataField="NumDoc" HeaderText="&#160;NumDoc"></asp:BoundColumn>
                <asp:BoundColumn DataField="DataVencimento" HeaderText="&#160;Vencimento" DataFormatString="{0:dd/MM/yyy}">
                    <HeaderStyle Width="70px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DataBaixa" HeaderText="&#160;Data Pag." DataFormatString="{0:dd/MM/yyy}">
                    <HeaderStyle Width="70px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ValorRecebido" HeaderText="&#160;Valor Recebido" DataFormatString="{0:N}">
                    <HeaderStyle Width="90px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_dados_cadastrais.gif' alt='Acionar Devedor' border='0'&gt;"
                    HeaderText="&lt;font size='1'&gt;COMANDOS&lt;/font&gt;" CommandName="Acionar">
                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" Width="30px" BackColor="#E0E0E0">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Left" BackColor="YellowGreen" CssClass="dg_header" Mode="NumericPages">
            </PagerStyle>
        </asp:DataGrid>
    </div>
</asp:Content>

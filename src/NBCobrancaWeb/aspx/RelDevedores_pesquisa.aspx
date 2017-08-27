<%@ Page Language="c#" Codebehind="RelDevedores_pesquisa.aspx.cs" AutoEventWireup="True"
    Inherits="NBCobranca.aspx.RelDevedores_pesquisa" MasterPageFile="~/aspx/LayoutPagina.Master"
    Title=".:: Neobridge Controle de Cobrança Web - Filtro para Relatório de Devedores ::." %>

<%@ Register TagPrefix="cc1" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phConteudo" runat="server">

    <script type="text/javascript" src="../scripts/trata_campo.js"></script>

    <script type="text/javascript" src="../scripts/relatorio.js"></script>

    <cc1:MessageBox ID="MessageBox" runat="server" PastaScripts="../Scripts" PastaStyles="../Styles">
    </cc1:MessageBox>
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
            <div class="Filtro" style="width: 130px">
                Tipos de Dívida<br />
                <asp:DropDownList ID="ddlTiposDivida" runat="server" DataValueField="ID" DataTextField="Descricao">
                </asp:DropDownList>
            </div>
            <div class="Filtro" style="width: 90px">
                <center>
                    Agrupar dividas<br />
                    <asp:CheckBox ID="chkAgruparDividas" runat="server" Checked="false" />
                </center>
            </div>
            <div class="Filtro" style="width: 70px">
                Data Inicial<br />
                <asp:TextBox ID="txtDataInicial_DATA" runat="server" Width="65px"></asp:TextBox>
            </div>
            <div class="Filtro" style="width: 70px">
                Data Final<br />
                <asp:TextBox ID="txtDataFinal_DATA" runat="server" Width="65px"></asp:TextBox>
            </div>
            <div class="Filtro">
                <br />
                <asp:ImageButton ID="imgBtnPesquisar" runat="server" ImageUrl="../imagens/botoes/btn_pesquisar.gif"
                    Height="18px" Width="79px" BorderStyle="None" OnClick="imgBtnPesquisar_Click"></asp:ImageButton>
                &nbsp;<img id="imgBtnPrint" style="visibility: hidden; cursor: hand" onclick="VisualizaRelatorio('Relatorios/rel_devedores.aspx','_Blank');"
                    alt="Visualiza a Impressão do Bordero" src="../imagens/botoes/btn_imprimir.gif"
                    runat="server" />
            </div>
        </div>
    </div>
    <div id="pesquisaResultado">
        <asp:DataGrid ID="dgDados" runat="server" Width="100%" ShowFooter="True" CellPadding="0"
            BorderWidth="1px" BorderColor="#DDE4DA" AutoGenerateColumns="False" GridLines="Vertical"
            PageSize="15" AllowPaging="True" OnItemDataBound="dgDados_ItemDataBound" OnPageIndexChanged="dgDados_PageIndexChanged"
            OnItemCommand="dgDados_ItemCommand">
            <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
            <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
            <HeaderStyle CssClass="dg_header" BackColor="YellowGreen"></HeaderStyle>
            <Columns>
                <asp:BoundColumn DataField="XmPathCliente" HeaderText="&#160;Carteira">
                    <HeaderStyle Width="300px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="idEntidade" HeaderText="&#160;C&#243;digo" DataFormatString="{0:d6}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NomePrimary" HeaderText="&#160;Nome Devedor"></asp:BoundColumn>
                <asp:BoundColumn DataField="DescTipoDivida" HeaderText="&#160;Tipo da D&#237;vida">
                    <HeaderStyle Width="210px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Contrato" HeaderText="&#160;Contrato">
                    <HeaderStyle Width="100px" />
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
                <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_dados_cadastrais.gif' alt='Acionar Devedor' border='0'&gt;"
                    HeaderText="&lt;font size='1'&gt;COMANDOS&lt;/font&gt;" CommandName="Acionar">
                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" Width="30px" BackColor="#E0E0E0">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Left" BackColor="YellowGreen" CssClass="dg_header"></PagerStyle>
            <FooterStyle BackColor="YellowGreen" CssClass="dg_header" BorderStyle="Solid"></FooterStyle>
        </asp:DataGrid>
        <asp:DataGrid ID="dgDadosAgrupados" Visible="false" runat="server" Width="100%" ShowFooter="True"
            CellPadding="0" BorderWidth="1px" BorderColor="#DDE4DA" AutoGenerateColumns="False"
            GridLines="Vertical" PageSize="15" AllowPaging="True" OnItemDataBound="dgDados_ItemDataBound"
            OnPageIndexChanged="dgDados_PageIndexChanged" OnItemCommand="dgDados_ItemCommand">
            <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
            <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
            <HeaderStyle CssClass="dg_header" BackColor="YellowGreen"></HeaderStyle>
            <Columns>
                <asp:BoundColumn DataField="XmPathCliente" HeaderText="&#160;Carteira" HeaderStyle-Width="300px">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="idEntidade" HeaderText="&#160;C&#243;digo" DataFormatString="{0:d6}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NomePrimary" HeaderText="&#160;Nome Devedor"></asp:BoundColumn>
                <asp:BoundColumn DataField="ValorNominalTotal" HeaderText="&#160;Total Nominal" DataFormatString="{0:N}"
                    HeaderStyle-Width="110px" ItemStyle-HorizontalAlign="right"></asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_dados_cadastrais.gif' alt='Acionar Devedor' border='0'&gt;"
                    HeaderText="&lt;font size='1'&gt;COMANDOS&lt;/font&gt;" CommandName="Acionar">
                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" Width="30px" BackColor="#E0E0E0">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Left" BackColor="YellowGreen" CssClass="dg_header"></PagerStyle>
            <FooterStyle BackColor="YellowGreen" CssClass="dg_header" BorderStyle="Solid"></FooterStyle>
        </asp:DataGrid>
    </div>
</asp:Content>

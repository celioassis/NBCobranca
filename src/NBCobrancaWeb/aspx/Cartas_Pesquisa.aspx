<%@ Page Language="C#" MasterPageFile="~/aspx/LayoutPagina.Master" AutoEventWireup="true"
    Codebehind="Cartas_Pesquisa.aspx.cs" Inherits="NBCobranca.aspx.Cartas_Pesquisa"
    Title=".:: Neobridge Controle de Cobrança Web - Pesquisa para emissão de Cartas ::." %>

<%@ Register TagPrefix="cc1" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phConteudo" runat="server">

    <script type="text/javascript" src="../scripts/trata_campo.js"></script>

    <script type="text/javascript" src="../scripts/relatorio.js"></script>

    <cc1:MessageBox ID="MessageBox" runat="server" PastaStyles="../Styles" PastaScripts="../Scripts"
        UsandoAjaxAnthem="True"></cc1:MessageBox>
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
                <asp:DropDownList ID="ddlTiposDivida" runat="server" DataTextField="Descricao" DataValueField="ID">
                </asp:DropDownList>
            </div>
            <div class="Filtro" style="width: 130px">
                Quantas Dívidas<br />
                <asp:DropDownList ID="ddlQuantDivida" runat="server">
                    <asp:ListItem Value="0" Selected="True">Todas</asp:ListItem>
                    <asp:ListItem Value="1">Uma</asp:ListItem>
                    <asp:ListItem Value="2">Duas</asp:ListItem>
                    <asp:ListItem Value="3">Tr&#234;s</asp:ListItem>
                    <asp:ListItem Value="4">Mais que Tr&#234;s</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="Filtro" style="width: 80px">
                <br />
                <asp:ImageButton ID="imgBtnPesquisar" runat="server" ImageUrl="../imagens/botoes/btn_pesquisar.gif"
                    Height="18px" Width="79px" BorderStyle="None" OnClick="imgBtnPesquisar_Click"></asp:ImageButton>
            </div>
            <div class="Filtro" style="width: 410px;" id="blqImprimir" runat="server" visible="false">
                <asp:CheckBox ID="ckbSegundoAviso" CssClass="ckb" Text="Segundo Aviso" runat="server" />
                &nbsp;
                <img id="imgBtnPrint" style="cursor: hand" onclick="VisualizaRelatorio('Relatorios/rel_CartasCustomizado.aspx','_Blank','?2Aviso=' + ctl00_phConteudo_ckbSegundoAviso.checked + '&IdCarta=1');"
                    alt="Visualiza a Impressão das Cartas" src="../imagens/botoes/btn_imprimir.gif" />
                &nbsp;
                <asp:Button ID="btnRegistrarCartas" Text="Registrar Cartas" runat="server" Width="105px"
                    CssClass="botao" OnClick="btnRegistrarCartas_Click" />
            </div>
        </div>
    </div>
    <div id="pesquisaResultado">
        <asp:DataGrid ID="dgDados" runat="server" CellPadding="0" BorderWidth="1px" BorderColor="#DDE4DA"
            AutoGenerateColumns="False" Width="100%" GridLines="Vertical" AllowPaging="True"
            PageSize="15" ShowFooter="True" OnItemDataBound="dgDados_ItemDataBound" OnPageIndexChanged="dgDados_PageIndexChanged"
            OnItemCommand="dgDados_ItemCommand">
            <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
            <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
            <HeaderStyle CssClass="dg_header" BackColor="YellowGreen"></HeaderStyle>
            <Columns>
                <asp:BoundColumn DataField="idEntidade" HeaderText="&#160;C&#243;digo" DataFormatString="{0:d6}">
                    <HeaderStyle Width="60px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NomePrimary" HeaderText="&#160;Nome"></asp:BoundColumn>
                <asp:BoundColumn DataField="Logradouro" HeaderText="&#160;Endere&#231;o"></asp:BoundColumn>
                <asp:BoundColumn DataField="DescricaoAcionamento" HeaderText="&#160;Descricao Acionamento">
                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="UltimoAcionamento" HeaderText="&#160;Ultimo Acionamento"
                    DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                    <HeaderStyle HorizontalAlign="Center" Width="130px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_dados_cadastrais.gif' alt='Acionar Devedor' border='0'&gt;"
                    HeaderText="&lt;font size='1'&gt;COMANDOS&lt;/font&gt;" CommandName="Acionar">
                    <HeaderStyle HorizontalAlign="Left" ForeColor="Black" Width="30px" BackColor="#E0E0E0">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Left" BackColor="YellowGreen" PageButtonCount="20" CssClass="dg_header">
            </PagerStyle>
            <FooterStyle BackColor="YellowGreen" CssClass="dg_header"></FooterStyle>
        </asp:DataGrid>
    </div>
</asp:Content>

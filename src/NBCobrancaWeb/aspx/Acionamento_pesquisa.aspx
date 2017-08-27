<%@ Page Language="c#" Codebehind="Acionamento_pesquisa.aspx.cs" AutoEventWireup="True"
    Inherits="NBCobranca.aspx.Acionamento_pesquisa" MasterPageFile="~/aspx/LayoutPagina.Master"
    Title=".:: Neobridge Controle de Cobrança Web - Filtro para Acionamentos ::." %>

<%@ Register TagPrefix="cc1" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phConteudo" runat="server">

    <script type="text/javascript" src="../scripts/trata_campo.js"></script>

    <cc1:MessageBox ID="MessageBox" runat="server" PastaStyles="../Styles" PastaScripts="../Scripts"
        UsandoAjaxAnthem="True" OnYesChoosed="MessageBox_YesChoosed"></cc1:MessageBox>
    <anthem:Timer ID="tmAgenda" runat="server" Enabled="true" Interval="30000" OnTick="timerAgenda_Tick">
    </anthem:Timer>
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
            <div class="Filtro" style="width: 130px">
                Tipos de Dívida<br />
                <asp:DropDownList ID="ddlTiposDivida" runat="server" DataValueField="ID" DataTextField="Descricao">
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
            <div class="Filtro" style="width: 130px">
                Acionadores<br />
                <asp:DropDownList ID="ddlAcionadores" runat="server" DataValueField="IdEntidade" DataTextField="NomePrimary">
                </asp:DropDownList>
            </div>
            <div class="Filtro" style="width: 130px">
                Tipos de Acionamento<br />
                <asp:DropDownList ID="ddlTiposAcionamento" runat="server" DataValueField="ID" DataTextField="Descricao">
                </asp:DropDownList>
            </div>
            <div class="Filtro" style="width: 70px">
                Data Inicial<br />
                <asp:TextBox ID="txtDataInicial_DATA" runat="server" Width="65px"></asp:TextBox>
            </div>
            <div class="Filtro" style="width: 70px">
                Data Final<br />
                <asp:TextBox ID="txtDataFinal_DATA" runat="server" Width="65px"></asp:TextBox>
            </div>
            <div class="Filtro" style="width: 200px">
                Nome do Devedor<br />
                <input id="txtProcurar" type="text" name="txtProcurar" runat="server" />
            </div>
            <div class="Filtro">
                <br />
                <anthem:ImageButton ID="imgBtnPesquisar" BorderStyle="None" Width="79px" Height="18px"
                    ImageUrl="../imagens/botoes/btn_pesquisar.gif" runat="server" OnClick="imgBtnPesquisar_Click">
                </anthem:ImageButton>&nbsp;
                <asp:ImageButton ID="imgBtnSMS" BorderStyle="None" Width="35px" Height="18px" Visible="false"
                    ImageUrl="../imagens/botoes/btn_SMS.png" runat="server" OnClick="imgBtnSMS_Click">
                </asp:ImageButton>
            </div>
        </div>
    </div>
    <div id="pesquisaResultado">
        <anthem:DataGrid ID="dgDados" runat="server" BorderColor="#DDE4DA" BorderWidth="1px"
            Width="100%" PageSize="15" AllowPaging="True" GridLines="Vertical" AutoGenerateColumns="False"
            CellPadding="0" AutoUpdateAfterCallBack="True" OnItemCommand="dgDados_ItemCommand"
            OnItemDataBound="dgDados_ItemDataBound" OnPageIndexChanged="dgDados_PageIndexChanged">
            <PagerStyle HorizontalAlign="Left" BackColor="YellowGreen" PageButtonCount="20" CssClass="dg_header"
                Mode="NumericPages"></PagerStyle>
            <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
            <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
            <HeaderStyle CssClass="dg_header" BackColor="YellowGreen"></HeaderStyle>
            <Columns>
                <asp:BoundColumn DataField="idEntidade" HeaderText="&#160;C&#243;digo" DataFormatString="{0:d6}">
                    <headerstyle width="60px"></headerstyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NomePrimary" HeaderText="&#160;Nome"></asp:BoundColumn>
                <asp:BoundColumn DataField="dtAlteracao" HeaderText="&#160;Ultima Altera&#231;&#227;o"
                    DataFormatString="{0:dd/MM/yyyy}">
                    <headerstyle horizontalalign="Center" width="75px"></headerstyle>
                    <itemstyle horizontalalign="Center"></itemstyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="UltimoAcionamento" HeaderText="&#160;Ultimo Acionamento"
                    DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                    <headerstyle horizontalalign="Center" width="130px"></headerstyle>
                    <itemstyle horizontalalign="Left"></itemstyle>
                </asp:BoundColumn>
                <asp:BoundColumn  HeaderText="&#160;Acionador" DataField="NomeAcionador">
                    <headerstyle horizontalalign="Center" width="130px"></headerstyle>
                    <itemstyle horizontalalign="Left"></itemstyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;img src='../imagens/botoes/c_dados_cadastrais.gif' alt='Acionar Devedor' border='0'&gt;"
                    HeaderText="&lt;font size='1'&gt;COMANDOS&lt;/font&gt;" CommandName="Acionar">
                    <headerstyle horizontalalign="Left" forecolor="Black" width="30px" backcolor="#E0E0E0"></headerstyle>
                    <itemstyle horizontalalign="Center"></itemstyle>
                </asp:ButtonColumn>
            </Columns>
        </anthem:DataGrid>
    </div>
</asp:Content>

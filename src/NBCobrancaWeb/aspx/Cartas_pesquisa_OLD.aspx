<%@ Register TagPrefix="uc1" TagName="NovoRodape" Src="../ascx/NovoRodape.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NovoCabecalho" Src="../ascx/NovoCabecalho.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="NBWebControls" Assembly="NBWebControls" %>

<%@ Page Language="c#" Codebehind="Cartas_pesquisa.aspx.cs" AutoEventWireup="True"
    Inherits="NBCobranca.aspx.Cartas_pesquisa" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>.:: Neobridge Controle de Cobrança Web - Filtro para Acionamentos ::.</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script language="JavaScript" src="../scripts/relatorio.js"></script>

</head>
<body ms_positioning="GridLayout">
    <form id="frmFiltroAcionamento" method="post" runat="server">
        <cc1:MessageBox ID="MessageBox" runat="server" PastaScripts="../Scripts" PastaStyles="../Styles">
        </cc1:MessageBox>
        <uc1:NovoCabecalho ID="NovoCabecalho" runat="server"></uc1:NovoCabecalho>
        <div id="borda">
            <div id="esquerda">
                <div id="pescima">
                    <img height="18" src="../imagens/pesquisar.gif" width="84"></div>
                <div id="pesbaixo">
                    <blockquote style="padding-left: 5px; float: left; width: 130px">
                        Carteiras<br>
                        <asp:DropDownList ID="ddlCarteiras" runat="server" DataTextField="Nome" DataValueField="Nome">
                        </asp:DropDownList></blockquote>
                    <blockquote style="padding-left: 5px; float: left; width: 130px">
                        Tipos de Dívida<br>
                        <asp:DropDownList ID="ddlTiposDivida" runat="server" DataTextField="Descricao" DataValueField="ID">
                        </asp:DropDownList></blockquote>
                    <blockquote style="padding-left: 5px; float: left; width: 130px">
                        Quantas Dívidas<br>
                        <asp:DropDownList ID="ddlQuantDivida" runat="server">
                            <asp:ListItem Value="0" Selected="True">Todas</asp:ListItem>
                            <asp:ListItem Value="1">Uma</asp:ListItem>
                            <asp:ListItem Value="2">Duas</asp:ListItem>
                            <asp:ListItem Value="3">Tr&#234;s</asp:ListItem>
                            <asp:ListItem Value="4">Mais que Tr&#234;s</asp:ListItem>
                        </asp:DropDownList></blockquote>
                    <blockquote style="padding-left: 5px; float: left; width: 80px">
                        <br>
                        <asp:ImageButton ID="imgBtnPesquisar" runat="server" ImageUrl="../imagens/botoes/btn_pesquisar.gif"
                            Height="18px" Width="79px" BorderStyle="None" OnClick="imgBtnPesquisar_Click"></asp:ImageButton></blockquote>
                    <blockquote id="blqImprimir" style="float: left; width: 450px" runat="server" visible="false">
                        <br>
                        Tipos de Carta:&nbsp;<select id="ddlCartas" style="width: 100px">
                            <option selected="selected" value="1">Aviso</option>
                            <option value="2">Judicial</option>
                        </select>
                        &nbsp;<asp:CheckBox ID="ckbSegundoAviso" Text="Segundo Aviso" runat="server"></asp:CheckBox>
                        &nbsp;<img id="imgBtnPrint" style="cursor: hand" onclick="VisualizaRelatorio('Relatorios/rel_CartasCustomizado.aspx','_Blank','?2Aviso=' + ckbSegundoAviso.checked + '&IdCarta=' +document.getElementById('ddlCartas').value);"
                            alt="Visualiza a Impressão das Cartas" src="../imagens/botoes/btn_imprimir.gif">
                        &nbsp;<asp:Button ID="btnRegistrarCartas" Text="Registrar Cartas" runat="server"
                            Width="90px" CssClass="botao" OnClick="btnRegistrarCartas_Click"></asp:Button>
                    </blockquote>
                </div>
            </div>
            <asp:DataGrid ID="dgDados" runat="server" CellPadding="0" BorderWidth="1px" BorderColor="#DDE4DA"
                AutoGenerateColumns="False" Width="100%" GridLines="Vertical" AllowPaging="True"
                PageSize="15" ShowFooter="True">
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
                </Columns>
                <PagerStyle HorizontalAlign="Left" BackColor="YellowGreen" PageButtonCount="20" CssClass="dg_header">
                </PagerStyle>
                <FooterStyle BackColor="YellowGreen" CssClass="dg_header"></FooterStyle>
            </asp:DataGrid></div>
        <uc1:NovoRodape ID="NovoRodape" runat="server"></uc1:NovoRodape>
    </form>
</body>
</html>

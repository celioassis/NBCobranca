<%@ Page Language="C#" MasterPageFile="~/aspx/LayoutPagina.Master" AutoEventWireup="true"
    Codebehind="Default.aspx.cs" Inherits="NBCobranca.aspx._Default"
    Title=".:: Neobridge Controle de Cobrança Web ::." %>

<asp:Content ID="Content1" ContentPlaceHolderID="phConteudo" runat="server">
    <div id="divFundo" runat="server" style="background: url(../imagens/logo_lugphil_fundo.gif) no-repeat 50% 50%;
        height: 280px; text-align: center">
        <br />
        <br />
        <div style="font-size: 20px; color: blue; font-family: Arial, Helvetica, sans-serif">
            Seja bem-vindo,
            <asp:Label ID="lblUser" runat="server"></asp:Label>
        </div>
        <br />
        <br />
        <br />
        <div style="font-size: 15px; color: #333333; font-family: Arial, Helvetica, sans-serif">
            <asp:Label ID="lblComprimento" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>

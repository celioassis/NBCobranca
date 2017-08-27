<%@ Page Language="c#" Codebehind="login.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.aspx.Login"
    MasterPageFile="~/aspx/LayoutPagina.Master" Title=".:: Neobridge Controle de Cobrança Web - Login ::." %>

<%@ Register TagPrefix="NBWC" Namespace="NBWebControls" Assembly="NBWebControls" %>
<asp:Content ID="LayoutBase" ContentPlaceHolderID="phConteudo" runat="server">
    <NBWC:MessageBox ID="MessageBox" runat="server" PastaScripts="../scripts"
        PastaStyles="../estilos"></NBWC:MessageBox>
    <div id="fundoLogin" class="txt">
        <div class="linhaLogin">
            <label class="labelLogin">
                Usuário:</label>
            <asp:TextBox ID="txtUsuario" runat="server" Width="110px"></asp:TextBox>
        </div>
        <div class="linhaLogin">
            <label class="labelLogin">
                Senha:</label>
            <asp:TextBox ID="txtSenha" runat="server" Width="110px" TextMode="Password"></asp:TextBox>
        </div>
        <div class="linhaLogin">
            <asp:ImageButton ID="imgBtnEntrar" runat="server" ImageUrl="../imagens/botoes/btn_entrar.gif"
                Width="71px" Height="18px" BorderStyle="None" CausesValidation="False" OnClick="imgBtnEntrar_Click">
            </asp:ImageButton>
        </div>
    </div>
</asp:Content>

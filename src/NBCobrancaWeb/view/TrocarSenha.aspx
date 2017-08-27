<%@ Page Title="" Language="C#" MasterPageFile="~/view/BootStrap.Master" AutoEventWireup="true" CodeBehind="TrocarSenha.aspx.cs" Inherits="NBCobranca.view.TrocarSenha" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phConteudo" runat="server">
    <script type="text/javascript">
        function pageInit() {
        };
    </script>
    <anthem:Panel ID="pnCampos" runat="server" CssClass="panel-body">
        <div class="form-group">
            <label>Senha atual:</label>
            <asp:TextBox ID="txtSenhaAtual" runat="server" CssClass="form-control" TextMode="Password" placeholder="Senha Atual"
                autofocus></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Nova senha:</label>
            <asp:TextBox ID="txtNovaSenha" runat="server" TextMode="Password" CssClass="form-control"
                placeholder="Nova Senha"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Confirmar nova senha:</label>
            <asp:TextBox ID="txtConfirmaNovaSenha" runat="server" TextMode="Password" CssClass="form-control"
                placeholder="Repetir Nova Senha"></asp:TextBox>
        </div>
        <anthem:Button ID="btnTrocarSenha" Text="Trocar" runat="server" CssClass="btn btn-lg btn-success btn-block" OnClick="btnTrocarSenha_OnClick" />
    </anthem:Panel>
</asp:Content>

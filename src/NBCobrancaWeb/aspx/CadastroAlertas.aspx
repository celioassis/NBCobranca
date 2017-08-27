<%@ Page Language="c#" Codebehind="CadastroAlertas.aspx.cs" Inherits="NBCobranca.aspx.CadastroAlertas"
    MasterPageFile="~/aspx/LayoutPaginaModal.Master" Title=".:: Neobridge Controle de Cobrança Web - Filtro para Acionamentos ::." %>

<%@ Register TagPrefix="NBWebControls" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<asp:Content ID="titulo" ContentPlaceHolderID="TituloModal" runat="server">
    Cadastro de Alertas
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ConteudoPaginaModal" runat="server">
    <NBWebControls:MessageBox ID="MessageBox" runat="server" PastaScripts="../scripts"
        PastaStyles="../estilos"></NBWebControls:MessageBox>
    <table width="100%" border="0">
        <tr>
            <td class="txt">
                Data e Hora do alerta
            </td>
            <td class="txt">
                Usuário destino
            </td>
        </tr>
        <tr>
            <td style="height: 26px">
                <asp:TextBox ID="txtDataAlerta_DATA" runat="server" Width="60px"></asp:TextBox>
                <asp:TextBox ID="txtHoraAlerta_HORA" runat="server" Width="30px"></asp:TextBox>
            </td>
            <td style="height: 26px">
                <asp:DropDownList ID="ddlUsuarioDestinoAlerta" runat="server" Width="150px" DataTextField="Login"
                    DataValueField="IdUsuario">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="txt">
                Mensagem de alerta
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtMensagemAlerta" runat="server" TextMode="MultiLine" Height="50px"
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <br />
                <asp:ImageButton ID="imgBtnSalvar" OnClick="imgBtnSalvar_Click" runat="server" Width="71"
                    Height="18" BorderStyle="None" ImageUrl="../imagens/botoes/btn_salvar.gif"></asp:ImageButton>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>

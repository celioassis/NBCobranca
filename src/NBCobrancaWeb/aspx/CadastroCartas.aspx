<%@ Page Language="C#" MasterPageFile="~/aspx/LayoutPaginaModal.Master" AutoEventWireup="true"
    Codebehind="CadastroCartas.aspx.cs" Inherits="NBCobranca.aspx.CadastroCartas"
    Title=".:: Neobridge Controle de Cobrança Web - Cadastro de Cartas ::." %>

<%@ Register TagPrefix="NBWeb" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ConteudoPaginaModal" runat="server">
    <table width="100%" border="0">
        <tr>
            <td class="txt">
                Nome da Carta<br />
                <asp:TextBox ID="txtNomeCarta_STR" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="txt">
                Titulo 1° Aviso<br />
                <asp:TextBox ID="txtTituloAviso1_STR" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="txt">
                Titulo 2° Aviso<br />
                <asp:TextBox ID="txtTituloAviso2_STR" runat="server" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="txt">
                Lista de Campos dos Devedores&nbsp;
                <asp:DropDownList ID="ddlCamposDadosDevedor" runat="server">
                </asp:DropDownList>&nbsp;<br />
                <br />
                Dados do Devedor:<br />
                <asp:TextBox ID="txtDadosDevedor_TUD" runat="server" TextMode="MultiLine" Height="50px"
                    Width="100%"></asp:TextBox>
                   
            </td>
        </tr>
        <tr>
            <td class="txt">
                Lista de Campos para o Conteúdo&nbsp;
                <asp:DropDownList ID="ddlCamposConteudo" runat="server">
                </asp:DropDownList>
                <br />
                <br />
                Conteúdo da Carta<br />
                <asp:TextBox ID="txtConteudo_TUD" runat="server" TextMode="MultiLine" Height="100px"
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                <br />
                <asp:ImageButton ID="imgBtnSalvar" runat="server" Width="71"
                    Height="18" BorderStyle="None" ImageUrl="../imagens/botoes/btn_salvar.gif"></asp:ImageButton>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>

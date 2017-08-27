<%@ Page Language="C#" Codebehind="EnvioSMS.aspx.cs" Inherits="NBCobranca.aspx.EnvioSMS"
    MasterPageFile="~/aspx/LayoutPaginaModal.Master" Title="Envio de SMS" %>

<%@ Register TagPrefix="NBWebControls" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<asp:Content ID="cntTitulo" ContentPlaceHolderID="TituloModal" runat="server">
    Envio de Mensagens Via SMS
</asp:Content>
<asp:Content ID="cntSMS" ContentPlaceHolderID="ConteudoPaginaModal" runat="server">
    <NBWebControls:MessageBox ID="MessageBox" runat="server" PastaScripts="../scripts"
        PastaStyles="../estilos" UsandoAjaxAnthem="true"></NBWebControls:MessageBox>
    <table width="100%" border="0">
        <tr>
            <td class="txt">
                Devedores
            </td>
        </tr>
        <tr>
            <td>
                <anthem:GridView ID="gvDevedores" runat="server" Width="96%" AutoGenerateColumns="false"
                    DataKeyNames="CodigoDevedor">
                    <AlternatingRowStyle CssClass="dg_item" BackColor="#F9FCF3" />
                    <RowStyle CssClass="dg_item" BackColor="#E9EFDD" />
                    <HeaderStyle CssClass="dg_header" BackColor="YellowGreen"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" ID="ckbAll" Checked="true" OnCheckedChanged="ckbAll_CheckedChanged"
                                    AutoPostBack="true" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="ckbItem" Checked="true" />
                            </ItemTemplate>
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="NomeDevedor" HeaderText="Devedor">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Telefone" HeaderText="Celular">
                            <HeaderStyle Width="80px" />
                        </asp:BoundField>
                    </Columns>
                </anthem:GridView>
            </td>
        </tr>
        <tr>
            <td class="txt">
                <br />
                Menssagem:
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtMensagemSMS" runat="server" TextMode="MultiLine" Height="50px"
                    Width="100%" MaxLength="160">SR(A) [NOME_DEVEDOR], FAVOR RETORNAR A LIGACAO PARA O TELEFONE (049) 3251-6000, PARA TRATAR ASSUNTO DE SEU INTERESSE, ATENCIOSAMENTE COEST ASSESSORIA.</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                <anthem:ImageButton ID="imgBtnEnviarSMS" runat="server" Width="71" Height="18" BorderStyle="None"
                    ImageUrl="../imagens/botoes/btn_enviar.gif" OnClick="imgBtnEnviarSMS_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

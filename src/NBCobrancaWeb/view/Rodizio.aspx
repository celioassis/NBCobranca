<%@ Page Title="" Language="C#" MasterPageFile="~/view/BootStrap.Master" AutoEventWireup="true" CodeBehind="Rodizio.aspx.cs" Inherits="NBCobranca.view.Rodizio" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phConteudo" runat="server">
     <script type="text/javascript">
    function pageInit() {
        $('#mnuLancamentos').has("ul").children("ul").collapse("show");
        $('#mnuDistribuicao').has("ul").children("ul").collapse("show");
        $('#mnuDistribuicao').addClass('active');
        $('#mnuDistribuicao_Rodizio').addClass('active');
    };
            
    </script>
    <asp:HiddenField ID="hfMotivo" runat="server"/>
    <div class="navbar navbar-default navbar-static-top" style="padding-left: 10px; padding-bottom: 10px;">
        <anthem:Button ID="btnProcessarRodizio" runat="server" Text="Iniciar rodízio" CssClass="btn btn-primary btn-lg"
            OnClick="btnProcessarRodizio_Click" TextDuringCallBack="Processando ..." />
    </div>
</asp:Content>

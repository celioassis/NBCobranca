<%@ Page Title="" Language="C#" MasterPageFile="~/view/BootStrap.Master" AutoEventWireup="true" CodeBehind="Erro.aspx.cs" Inherits="NBCobranca.view.Erro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phConteudo" runat="server">
    <script type="text/javascript">
        function pageInit() {
        }
    </script>

    <div class="row">
        <div class="col-md-12 page-404">
            <div class="number">
                E r r o
            </div>
            <div class="details">
                <h3>Oops! Ocorreu um erro inesperado</h3>
                <p>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label><br />
                </p>
            </div>
        </div>
    </div>
</asp:Content>

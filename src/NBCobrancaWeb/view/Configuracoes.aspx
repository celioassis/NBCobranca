<%@ page title="" language="C#" masterpagefile="~/view/BootStrap.Master"
    autoeventwireup="true"
    codebehind="Configuracoes.aspx.cs"
    inherits="NBCobranca.view.Configuracoes" %>

<%@ register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<asp:content id="c_Configuracoes" contentplaceholderid="phConteudo" runat="server">
    <script type="text/javascript">
        function pageInit() {
        };

        function Validacao(btn) {
            ConfirmacaoComMotivo('Esta validação irá verificar se todos os dados informados estão preenchidos corretamente e irá tentar enviar um email de validação para o email de destino informado abaixo, deseja continuar?',
                function (result) {
                    if (result) {
                        <%=GetAnthemCallbackEvent(this.btnValidarConfiguracoes, "btn")%>
                    }
                },
                true,
                'Informe o email de destino para a validação',
                $('#<%=hfEmailDestino.ClientID%>'),
                'O email de destino é obrigatório.'
            );
        }

    </script>
    <anthem:HiddenField ID="hfEmailDestino" runat="server" />
    <anthem:HiddenField id="hfID" runat="server" Value="0" />
    <anthem:panel ID="pnCampos" runat="server" CssClass="panel-body">
        
        <div class="form-group">
            <label>Endreço do Servido de SMTP:</label>
            <asp:TextBox ID="txtServidorSMTP" runat="server" CssClass="form-control" placeholder="Servidor SMTP"
                autofocus></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Porta SMTP:</label>
            <asp:TextBox ID="txtPortaSMTP" runat="server" CssClass="form-control" placeholder="Porta SMTP"
                autofocus></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Usuário SMTP:</label>
            <asp:TextBox ID="txtUsuarioSMTP" runat="server" CssClass="form-control" placeholder="Usuário SMTP"
                autofocus></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Senha:</label>
            <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="form-control"
                placeholder="Senha" Text=""></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Confirmar senha:</label>
            <asp:TextBox ID="txtConfirmaSenha" runat="server" TextMode="Password" CssClass="form-control"
                placeholder="Repetir Senha" Text=""></asp:TextBox>
        </div>
        <div class="row">
            <div class="col-md-6">
                <anthem:Button ID="btnSalvar" Text="Salvar" runat="server" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnSalvar_Click" />
            </div>
            <div class="col-md-6">
                <anthem:Button ID="btnValidarConfiguracoes" Text="Validar Configurações" runat="server" CssClass="btn btn-lg btn-primary btn-block" OnClick="btnValidarConfiguracoes_Click"
                   TextDuringCallBack="Processando..." OnClientClick="Validacao(this);return false;" />
            </div>
        </div>
    </anthem:panel>
</asp:content>

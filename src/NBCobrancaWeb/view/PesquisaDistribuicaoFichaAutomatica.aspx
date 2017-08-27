<%@ Page Language="C#" MasterPageFile="~/view/BootStrap.Master" AutoEventWireup="True"
    CodeBehind="PesquisaDistribuicaoFichaAutomatica.aspx.cs" Inherits="NBCobranca.view.PesquisaDistribuicaoFichaAutomatica"
    EnableEventValidation="false" EnableViewState="false" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="C" ContentPlaceHolderID="phConteudo" runat="server">

    <script type="text/javascript">
        function configuraInputMask() {
                
        }

        function configuraClickCkbHeader(idCheckBox, idTable) {
            $(idCheckBox).on('click', function (e) {
                if (this.checked) {
                    $(idTable + ' tbody input[type="checkbox"]:not(:checked)').trigger('click');
                } else {
                    $(idTable + ' tbody input[type="checkbox"]:checked').trigger('click');
                }
                e.stopPropagation();
            });
        }

        function pageInit() {
            $('#mnuLancamentos').has("ul").children("ul").collapse("show");
            $('#mnuDistribuicao').has("ul").children("ul").collapse("show");
            $('#mnuDistribuicao').addClass('active');
            $('#mnuDistribuicao_Automatica').addClass('active');

            configuraClickCkbHeader('#chkTodosAcionadores', '#ctl00_phConteudo_chkListAcionadores');

        };

        //Modelo de function para chamar evento de botão via ajax.
        function ZerarDistribuicao() {

            document.forms[0].__EVENTTARGET.value = 'ctl00$phConteudo$btnExecutaZerarDistribuicao';
            var arForm = $(document.forms[0]);
            $.ajax({
                type: "POST",
                url: "PesquisaDistribuicaoFichaAutomatica.aspx",
                data: arForm.serialize(),
                dataType: "html",
                success: function (result) {
                    alert(result);
                },
                error: function (xhr, status) {
                    alert("An error occurred: " + status);
                }
            });
        };

        function Validacao(btn) {
            ConfirmacaoComMotivo('Ao zerar a distribuição das fichas os acionadores ficarão sem fichas para acionar, deseja Zerar a Distribuição das fichas?',
                function (result) {
                    if (result) {
                        <%=GetAnthemCallbackEvent(this.btnZerarDistribuicao, "btn")%>
                    }
                }, true, 'Favor informar um motivo para zerar a distribuição já realizada', $('#<%=hfMotivo.ClientID%>'));
        };

    </script>

    <anthem:HiddenField ID="hfMotivo" runat="server" />
    <div class="navbar navbar-default navbar-static-top" style="padding-left: 10px; padding-bottom: 10px;">
        <div class="row">
            <div class="col-md-3">
                <div class="form-group">
                    <label>Carteiras</label>
                    <anthem:DropDownList ID="ddlCarteiras" runat="server" DataValueField="NomePrimary"
                        DataTextField="NomePrimary" CssClass="input-sm form-control" />
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label>Tipos de Dívidas</label>
                    <anthem:DropDownList ID="ddlTipoDividas" runat="server" DataValueField="ID" DataTextField="Descricao"
                        CssClass="input-sm form-control" />
                </div>
            </div>
            <div class="col-sm-4 col-lg-2 col-md-3">
                <div class="form-group">
                    <label>Data de vencimento</label>
                    <anthem:TextBox runat="server" ID="txtFiltroDataVencimento" CssClass="input-sm form-control inputMask date-picker" data-inputmask="'mask': 'd/m/y', 'placeholder': 'dd/mm/aaaa'" />
                </div>
            </div>
            <div class="col-sm-1 col-md-1">
                <div class="form-group">
                    <label>Mês</label>
                    <anthem:TextBox runat="server" ID="txtFiltroMesVencimento" CssClass="input-sm form-control inputMask" MaxLength="2" Width="40px" data-inputmask="'alias': 'integer'"/>
                </div>
            </div>
            <div class="col-sm-2 col-md-1">
                <div class="form-group">
                    <label>Ano</label>
                    <anthem:TextBox runat="server" ID="txtFiltroAnoVencimento" CssClass="input-sm form-control inputMask" MaxLength="4" Width="50px" data-inputmask="'alias': 'integer'" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label>Acionadores
                        <input type="checkbox" id="chkTodosAcionadores"/> Todos
                    </label>
                    <anthem:CheckBoxList runat="server" id="chkListAcionadores" DataTextField="NomePrimary" DataValueField="IdEntidade" RepeatColumns="2"></anthem:CheckBoxList>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <anthem:Button ID="imgBtnPesquisar" runat="server" Text="Pesquisar" CssClass="btn btn-primary btn-sm"
                        OnClick="imgBtnPesquisar_Click" EnabledDuringCallBack="False" TextDuringCallBack="Processando ..."></anthem:Button>
                    <anthem:Button ID="btnDistribuir" runat="server" Text="Distribuir" CssClass="btn btn-primary btn-sm"
                        OnClick="btnDistribuir_Click" TextDuringCallBack="Processando ..." />
                    <anthem:Button ID="btnZerarDistribuicao" runat="server" Text="Zerar Distribuição"
                        TextDuringCallBack="Processando..." CssClass="btn btn-primary btn-sm" OnClick="btnZerarDistribuicao_Click"
                        OnClientClick="javascript:Validacao(this);return false;" />
                    <%//Botão de modelo para chamada via ajax pelo método zerar distribuição. %>
                    <asp:Button ID="btnExecutaZerarDistribuicao" runat="server" OnClick="btnExecutaZerarDistribuicao_Click"
                        Visible="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="table-responsive">
            <anthem:GridView ID="gvResultado" runat="server" Width="100%" PageSize="15" AutoGenerateColumns="False"
                BorderStyle="None" CssClass="table table-striped table-bordered table-hover">
                <HeaderStyle CssClass="info" />
                <EmptyDataTemplate>
                    Não existem fichas livres para distruição automática.
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="CodigoFicha" HeaderText="C&#243;digo">
                        <ItemStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NomeDevedor" HeaderText="Devedor" />
                </Columns>
            </anthem:GridView>
        </div>
</asp:Content>

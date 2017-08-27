<%@ page language="C#" masterpagefile="~/view/BootStrap.Master" autoeventwireup="true"
    codebehind="PesquisaDistribuicaoFichaManual.aspx.cs" inherits="NBCobranca.view.PesquisaDistribuicaoFichaManual"
    title="Untitled Page" %>

<%@ register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<asp:content id="C" contentplaceholderid="phConteudo" runat="server">

    <script type="text/javascript">
        var gvResultado;

        function pageInit() {
            $('#mnuLancamentos').has("ul").children("ul").collapse("show");
            $('#mnuDistribuicao').has("ul").children("ul").collapse("show");
            $('#mnuDistribuicao').addClass('active');
            $('#mnuDistribuicao_Manual').addClass('active');
        };

        function updateDataTableSelectAllCtrl(table) {
            var $table = table.table().node();
            var $chkbox_all = $('tbody input[type="checkbox"]', $table);
            var $chkbox_checked = $('tbody input[type="checkbox"]:checked', $table);
            var chkbox_select_all = $('thead input[type="checkbox"]', $table).get(0);

            // If none of the checkboxes are checked
            if ($chkbox_checked.length === 0) {
                chkbox_select_all.checked = false;
                if ('indeterminate' in chkbox_select_all) {
                    chkbox_select_all.indeterminate = false;
                }

                // If all of the checkboxes are checked
            } else if ($chkbox_checked.length === $chkbox_all.length) {
                chkbox_select_all.checked = true;
                if ('indeterminate' in chkbox_select_all) {
                    chkbox_select_all.indeterminate = false;
                }

                // If some of the checkboxes are checked
            } else {
                chkbox_select_all.checked = true;
                if ('indeterminate' in chkbox_select_all) {
                    chkbox_select_all.indeterminate = true;
                }
            }
        };

        function Validacao(btn) {
            ConfirmacaoComMotivo('Ao zerar a distribuição das fichas selecionadas as mesmas ficaram sem ser acionadas até a próxima distribuição, deseja Zerar a Distribuição dessas fichas?',
                function (result) {
                    if (result) {
                        //event = null;
                        <%=GetAnthemCallbackEvent(this.btnZerarDistribuicao, "btn")%>
                    }
                },
                true,
                'Informe o motivo para zerar a distribuição das fichas existentes',
                $('#<%=hfMotivo.ClientID%>')
            );
        }

        function configuraDataTable(idTable) {
            $(idTable).dataTable({
                'language': {
                    'url': 'Portuguese-Brasil.json'
                },
                'paging': false,
                'columnDefs': [{
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center'
                }],
                "initComplete": function () {
                    var checkBox = $('table thead th:first-child');
                    checkBox.removeClass('sorting_asc');
                    checkBox.css('text-align', 'center');
                }
            });
            gvResultado = $(idTable);
            gvResultado.DataTable().on('draw', function () {
                // Update state of "Select all" control
                updateDataTableSelectAllCtrl(gvResultado.DataTable());
            });
        }

        function configuraClickCkbHeader(idCheckBox, idTable) {
            $(idTable + ' thead ' + idCheckBox).on('click', function (e) {
                if (this.checked) {
                    $(idTable + ' tbody input[type="checkbox"]:not(:checked)').trigger('click');
                } else {
                    $(idTable + ' tbody input[type="checkbox"]:checked').trigger('click');
                }
                e.stopPropagation();
            });


            $(idTable + ' tbody').on('click', 'input[type="checkbox"]', function (e) {
                // Update state of "Select all" control
                updateDataTableSelectAllCtrl(gvResultado.DataTable());

                // Prevent click event from propagating to parent
                e.stopPropagation();
            });
        }

    </script>

    <anthem:HiddenField ID="hfMotivo" runat="server" />
    <div class="navbar navbar-default navbar-static-top small" style="padding-left: 10px; padding-bottom: 10px;">
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <anthem:CheckBox ID="chkDisponiveis" runat="server" ToolTip="Irá mostrar somente as fichas disponíveis para distribuição"
                        Text=" Somente Disponíveis" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-lg-3">
                <div class="form-group">
                    <label>Carteiras</label>
                    <anthem:DropDownList ID="ddlCarteiras" runat="server" DataValueField="NomePrimary"
                        DataTextField="NomePrimary" CssClass="input-sm" Width="100%" />
                </div>
            </div>
            <div class="col-md-3 col-lg-3">
                <div class="form-group">
                    <label>Tipos de Dívidas</label>
                    <anthem:DropDownList ID="ddlTipoDividas" runat="server" DataValueField="ID" DataTextField="Descricao"
                        CssClass="input-sm" Width="100%" />
                </div>
            </div>
            <div class="col-md-3 col-lg-2">
                <label>Quantas Dívidas</label>
                <anthem:DropDownList ID="ddlQuantDivida" runat="server" CssClass="input-sm" Width="100%">
                    <asp:ListItem Value="0" Selected="True">Todas</asp:ListItem>
                    <asp:ListItem Value="1">Uma</asp:ListItem>
                    <asp:ListItem Value="2">Duas</asp:ListItem>
                    <asp:ListItem Value="3">Tr&#234;s</asp:ListItem>
                    <asp:ListItem Value="4">Mais que Tr&#234;s</asp:ListItem>
                </anthem:DropDownList>
            </div>
        </div>
        <div class="row">
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

            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <br>
                    <anthem:Button ID="imgBtnPesquisar" runat="server" Text="Pesquisar" CssClass="btn btn-primary btn-sm"
                        OnClick="imgBtnPesquisar_Click" EnabledDuringCallBack="False" TextDuringCallBack="Processando ..."></anthem:Button>
                </div>
            </div>

        </div>
        <anthem:Panel ID="pnlAcoes" runat="server" CssClass="row" Visible="false">
            <div class="col-md-3">
                <div class="form-group">
                    <label>Selecione um acionador</label>
                    <anthem:DropDownList ID="ddlAcionadores" runat="server" DataValueField="idEntidade"
                        DataTextField="NomePrimary" CssClass="input-sm form-control" />
                </div>
            </div>
            <div class="col-md-9 bottom">
                <div class="form-group">
                    <br/>
                    <anthem:Button ID="btnDistribuir" runat="server" Text="Distribuir" CssClass="btn btn-primary btn-sm"
                        OnClick="btnDistribuir_Click" TextDuringCallBack="Processando ..." />
                    <anthem:Button ID="btnZerarDistribuicao" runat="server" Text="Zerar Distribuição"
                        TextDuringCallBack="Processando..." CssClass="btn btn-primary btn-sm" OnClick="btnZerarDistribuicao_Click"
                        OnClientClick="Validacao(this);return false;" />

                </div>
            </div>
        </anthem:Panel>
    </div>
    <div class="table-responsive">
        <anthem:GridView ID="gvResultado" runat="server" Width="100%" PageSize="15" AutoGenerateColumns="False"
            BorderStyle="None" CssClass="table table-striped table-bordered table-hover">
            <HeaderStyle CssClass="info" />
            <EmptyDataTemplate>
                Não existem fichas livres para distruição automática.
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ckbHeader" runat="server" Width="21px" BorderStyle="None"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="ckbItem" runat="server" Width="21px" BorderStyle="None"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CodigoFicha" HeaderText="C&#243;digo">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="NomeDevedor" HeaderText="Devedor" />
                <asp:BoundField DataField="CarteiraDeAcionamento" HeaderText="Esta sendo acionado por" />
                <asp:BoundField DataField="DataUltimoAcionamento" HeaderText="Ultimo Acionamento" DataFormatString="{0:dd/MM/yyyy  HH:mm:ss}" />
            </Columns>
        </anthem:GridView>
    </div>
</asp:content>

<%@ page title="" language="C#" masterpagefile="~/view/BootStrap.Master" autoeventwireup="true" codebehind="RelatorioTotalDeFichasPorAcionador.aspx.cs" inherits="NBCobranca.view.RelatorioTotalDeFichasPorAcionador" %>

<%@ register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
<asp:content id="Content1" contentplaceholderid="phConteudo" runat="server">
    <script type="text/javascript">
        function pageInit() {
            $('#mnuRelatorios').has("ul").children("ul").collapse("show");
            $('#mnuRelatorios_TotaisFichasPorAcionador').addClass('active');
        };
    </script>

    <div class="navbar navbar-default navbar-static-top" style="padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
        <div class="row">
            <div class="col-md-6"><strong><em>Período de Vencimentos</em></strong></div>
            <div class="col-md-6"><strong><em>Período de Acionamentos</em></strong></div>
            <hr />
        </div>
        <div class="row">
            <div class="col-md-6 col-lg-6">
                <div class="row">
                    <div class="col-md-6 col-lg-4">
                        <div class="form-group">
                            <label class="small">Vencimento Inicial</label>
                            <anthem:textbox runat="server" id="txtFiltroDataVencimentoInicial" cssclass="input-sm form-control inputMask date-picker" data-inputmask="'mask': 'd/m/y', 'placeholder': 'dd/mm/aaaa'" />
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-4">
                        <label class="small">Vencimento Final</label>
                        <anthem:textbox runat="server" id="txtFiltroDataVencimentoFinal" cssclass="input-sm form-control inputMask date-picker" data-inputmask="'mask': 'd/m/y', 'placeholder': 'dd/mm/aaaa'" />
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-lg-6">
                <div class="row">
                    <div class="col-md-6 col-lg-4">
                        <div class="form-group">
                            <label class="small">Acionamento Inicial</label>
                            <anthem:textbox runat="server" id="txtFiltroDataAcionamentoInicial" cssclass="input-sm form-control inputMask date-picker" data-inputmask="'mask': 'd/m/y', 'placeholder': 'dd/mm/aaaa'" />
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-4">
                        <label class="small">Acionamento Final</label>
                        <anthem:textbox runat="server" id="txtFiltroDataAcionamentoFinal" cssclass="input-sm form-control inputMask date-picker" data-inputmask="'mask': 'd/m/y', 'placeholder': 'dd/mm/aaaa'" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-lg-2">
                <div class="form-group">
                    <label class="small">Acionadores</label>
                    <anthem:dropdownlist id="ddlAcionadores" runat="server" datavaluefield="idEntidade"
                        datatextfield="NomePrimary" cssclass="input-sm form-control" />
                </div>
            </div>
            <div class="col-md-3 col-lg-2">
                <div class="form-group">
                    <label class="small">Tipos de Dívidas</label>
                    <anthem:dropdownlist id="ddlTipoDividas" runat="server" datavaluefield="ID" datatextfield="Descricao"
                        cssclass="input-sm form-control" />
                </div>
            </div>
            <div class="col-md-3 col-lg-2">
                <div class="form-group">
                    <label class="small">Tipos de Acionamentos</label>
                    <anthem:dropdownlist id="ddlTipoAcionamentos" runat="server" datavaluefield="ID" datatextfield="Descricao"
                        cssclass="input-sm form-control" />
                </div>
            </div>
            <div class="col-md-3 col-lg-2" style="padding-top: 23px;">
                <anthem:button id="imgBtnPesquisar" runat="server" text="Pesquisar" cssclass="btn btn-primary btn-sm"
                    onclick="imgBtnPesquisar_Click" enabledduringcallback="False" textduringcallback="Processando ..."></anthem:button>
            </div>
        </div>
    </div>
    <div class="table-responsive">
        <anthem:gridview id="gvResultado" runat="server" width="100%" pagesize="15" autogeneratecolumns="False"
            borderstyle="None" cssclass="table table-striped table-bordered table-hover" DataKeyNames="IdAcionador" 
            OnRowCommand="gvResultado_OnRowCommand">
                <HeaderStyle CssClass="info" />
                <Columns>
                    <asp:BoundField DataField="NomeAcionador" HeaderText="Acionador" />
                    <asp:BoundField DataField="TotalFichasRecebidas" HeaderText="Distribuidas" />
                    <asp:BoundField DataField="TotalFichasComPromessa" HeaderText="Com Promessa" />
                    <asp:BoundField DataField="TotalFichasLivresParaAcionarHoje" HeaderText="Livres Para Acionar Hoje" />
                    <asp:BoundField DataField="TotalFichasAcionadas" HeaderText="Acionadas no Periodo / Hoje" />
                    <asp:CommandField ButtonType="Button" CausesValidation="False" InsertVisible="False" SelectText="Detalhe" ShowCancelButton="False" ShowSelectButton="True"></asp:CommandField>
                </Columns>
            </anthem:gridview>
    </div>
    <div id="ancora"></div>
    <anthem:Panel runat="server" ID="detalhesAcionador" Visible="False">
        <div class="navbar navbar-default navbar-static-top" style="padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
            <h2>Detalhes de <anthem:Label runat="server" ID="lblAcionador" Text="Beltrano"></anthem:Label></h2>
            <hr />
            <div class="row">
                <div class="col-md-4"><strong><em>Fichas Distribuídas</em></strong></div>
                <div class="col-md-8"><anthem:Label runat="server" ID="lblFichasDistribuidas" text="000"></anthem:Label></div>            
            </div>        
            <div class="row">
                <div class="col-md-4"><strong><em>Fichas Liberadas para Acionar</em></strong></div>
                <div class="col-md-8"><anthem:Label runat="server" ID="lblFichasLiberadasParaAcionar" text="000"></anthem:Label></div>            
            </div>        
            <div class="row">
                <div class="col-md-4"><strong><em>Total para Cobrar</em></strong></div>
                <div class="col-md-8"><anthem:Label runat="server" ID="lblTotalParaCobrar" text="0,00"></anthem:Label></div>            
            </div>
            <div class="row">
                <div class="col-md-12"><h3>Total de Fichas por Tipo de Acionamento</h3></div>
            </div>
            <div class="row">
                <div class="table-responsive col-md-12">
                    <anthem:gridview id="gvFichasPorTipoAcionamento" runat="server" width="100%" autogeneratecolumns="False"
                        borderstyle="None" cssclass="table table-striped table-bordered table-hover" >
                        <EmptyDataTemplate>Sem Detalhes de Fichas por Tipo de Acionamento</EmptyDataTemplate>
                        <HeaderStyle CssClass="info" />
                        <Columns>
                            <asp:BoundField DataField="TipoAcionamento" HeaderText="Tipo do Acionamento" />
                            <asp:BoundField DataField="TotalDeFichas" HeaderText="Total de Fichas" />
                        </Columns>
                    </anthem:gridview>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12"><h3>Fichas</h3></div>
            </div>
            <div class="row">
                <div class="table-responsive col-md-12">
                    <anthem:gridview id="gvFichasDoAcionador" runat="server" width="100%" autogeneratecolumns="False"
                        borderstyle="None" cssclass="table table-striped table-bordered table-hover"
                        OnRowCommand="gvFichasDoAcionador_OnRowCommand" DataKeyNames="IdDevedor" >
                        <EmptyDataTemplate>Nenhuma Ficha Distribuída para o Acionador</EmptyDataTemplate>    
                        <HeaderStyle CssClass="info" />
                        <Columns>
                            <asp:BoundField DataField="Carteira" HeaderText="Carteira" />
                            <asp:BoundField DataField="NomeDevedor" HeaderText="Devedor" />
                            <asp:BoundField DataField="DataUltimoAcionamento" HeaderText="Último Acionamento" />
                            <asp:BoundField DataField="TipoDoAcionamento" HeaderText="Tipo do Acionamento" />
                            <asp:BoundField DataField="LiberadaParaAcionarAPartirDe" HeaderText="Liberada para Acionar A Partir De" />                                
                            <asp:CommandField ButtonType="Button" CausesValidation="False" InsertVisible="False" SelectText="Abrir" ShowCancelButton="False" ShowSelectButton="True"></asp:CommandField>
                        </Columns>
                    </anthem:gridview>
                </div>
            </div>
        </div>
    </anthem:Panel>
    <script>
        function Anthem_PreCallBack() {
            $.blockUI({ message: '<h1>Processando...</h1>', baseZ:1500 });
        }
        function Anthem_PostCallBack() {
            $.unblockUI();
        }

    </script>
</asp:content>

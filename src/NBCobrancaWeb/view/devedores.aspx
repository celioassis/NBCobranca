<%@ Page Title="" Language="C#" MasterPageFile="~/view/BootStrap.Master" AutoEventWireup="true" CodeBehind="devedores.aspx.cs" Inherits="NBCobranca.view.Devedores" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phConteudo" runat="server">
    <script type="text/javascript">
        function pageInit() {
            $('#mnuCadastros').has("ul").children("ul").collapse("show");
            $('#mnuCadastros_Devedores').addClass('active');            
        };

    </script>
    <div class="navbar navbar-default navbar-static-top small" style="padding-left: 10px; padding-bottom: 10px;">
        <br/>
        <div class="row">
            <div class="col-md-3 col-lg-3">
                <div class="form-group">
                    <label class="control-label" for="<% =ddlCarteiras.ClientID %>">Carteiras</label>
                    <anthem:dropdownlist id="ddlCarteiras" runat="server" datavaluefield="NomePrimary"
                        datatextfield="NomePrimary" cssclass="form-control" />
                </div>
            </div>
            <div class="col-md-2 col-lg-2">
                <div class="form-group">
                    <label class="control-label" for="<% =selCamposProcurar.ClientID %>">Campo a procurar</label>
                    <anthem:dropdownlist id="selCamposProcurar" runat="server"
                        cssclass="form-control">
                        <asp:ListItem Value="1">ID</asp:ListItem>
                        <asp:ListItem Selected="True" Value="2">Nome</asp:ListItem>
                        <asp:ListItem Value="3">CPF/CNPJ</asp:ListItem>
                    </anthem:dropdownlist>
                </div>
            </div>
            <div class="col-md-5 col-lg-5">
                <div class="form-group">
                    <label class="control-label" for="<%=txtProcurar.ClientID %>">O que deseja procurar</label>
                    <div class="input-group">
                        <anthem:textbox runat="server" id="txtProcurar" cssclass="form-control" />
                        <span class="input-group-btn">
                            <button id="btnPesquisar" class="btn btn-success" onclick="Anthem_InvokePageMethod('Pesquisar', null, null, null);return false;"><i class="fa fa-search fa-fw"></i></button>
                        </span>
                    </div>                                                                       
                </div>
            </div>
            <div class="col-md-2 col-lg-2">
                <br/>
                <button id="btnNovoDevedor" class="btn btn-info" style="margin-top: 5px;"><i class="fa fa-plus"></i> Novo Devedor</button>
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
                    <asp:BoundField DataField="IdEntidade" HeaderText="&#160;C&#243;digo" DataFormatString="{0:d6}">
                        <HeaderStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NomePrimary" HeaderText="&#160;Nome">
                        <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Logradouro" HeaderText="&#160;Endere&#231;o" />
                    <asp:BoundField DataField="Municipio" HeaderText="&#160;Cidade">
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UF" HeaderText="&#160;UF">
                        <HeaderStyle Width="25px" />
                    </asp:BoundField>
                <asp:TemplateField>
                    <HeaderTemplate>                       
                            COMANDOS
                    </HeaderTemplate>
                    <ItemTemplate>
                            <asp:LinkButton ID="lkbtnEditar" runat="server" CausesValidation="false" CommandName="Edit"
                                Text="<img src='../imagens/botoes/c_editar.gif' alt='Editar' border='0'>"></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lkbtnDeletar" runat="server" CausesValidation="false" CommandName="Delete"
                                Text="<img src='../imagens/botoes/c_remover.gif' alt='Excluir devedor' border='0'>"></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lkbtnBaixar" runat="server" CausesValidation="false" CommandName="Baixar"
                                CommandArgument='<%# Container.DisplayIndex %>' Text="<img src='../imagens/botoes/btn_Cheque.gif' alt='Baixar Dívida' border='0'>"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                </asp:TemplateField>
                
            </Columns>
        </anthem:GridView>
    </div>

    </div>
</asp:Content>

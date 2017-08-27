<%@ Page Language="C#" MasterPageFile="~/aspx/LayoutPaginaModal.Master" AutoEventWireup="true"
    Codebehind="AcionamentoFicha.aspx.cs" Inherits="NBCobranca.aspx.AcionamentoFicha"
    Title="Ficha de Acionamento" %>

<%@ Register TagPrefix="componentart" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="NBWebControls" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="TituloModal" runat="server">
    Ficha de Acionamento
</asp:Content>
<asp:Content ID="AcionamentoFicha_Modal" ContentPlaceHolderID="ConteudoPaginaModal"
    runat="server">
    <NBWebControls:MessageBox ID="MessageBox" runat="server" UsandoAjaxAnthem="True"
        PastaScripts="../scripts" PastaStyles="../estilos" Altura="150" Largura="500" />
    <asp:Panel ID="pnPF" runat="server" CssClass="txt divTabela" Visible="true">
        <div class="divColunas" style="width: 57%">
            Nome<br />
            <asp:TextBox ID="txtNome" runat="server" ReadOnly="True"></asp:TextBox>
        </div>
        <div class="divColunas" style="width: 20%">
            CPF
            <br />
            <asp:TextBox ID="txtCPF" runat="server" ReadOnly="True"></asp:TextBox>
        </div>
        <div class="divColunas" style="width: 21.5%">
            RG
            <br />
            <asp:TextBox ID="txtRG" runat="server" ReadOnly="True"></asp:TextBox>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnPJ" runat="server" CssClass="txt" Visible="FALSE">
        <div class="divTabela">
            <div class="divColunas" style="width: 54%">
                Razão Social
                <br />
                <asp:TextBox ID="txtRazaoSocial" runat="server" ReadOnly="True"></asp:TextBox>
            </div>
            <div class="divColunas" style="width: 45%">
                Nome Fantasia
                <br />
                <asp:TextBox ID="txtNomeFantasia" runat="server" ReadOnly="True"></asp:TextBox>
            </div>
        </div>
        <div class="divTabela">
            <div class="divColunas" style="width: 24%">
                CNPJ
                <br />
                <asp:TextBox ID="txtCNPJ" runat="server" ReadOnly="True"></asp:TextBox>
            </div>
            <div class="divColunas" style="width: 32%">
                Inscrição Estadual
                <br />
                <asp:TextBox ID="txtInscEstadual" runat="server" ReadOnly="True"></asp:TextBox>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnEndereco" runat="server" CssClass="txt_peq" Visible="true">
        <div class="divTabela divTabelaBgColor">
            <div class="divColunas" style="width: 37%;">
                Endereço<br />
                <asp:TextBox ID="txtEndereco" runat="server" ReadOnly="True"></asp:TextBox>
            </div>
            <div class="divColunas" style="width: 21%;">
                Complemento<br />
                <asp:TextBox ID="txtComplemento" runat="server" ReadOnly="True"></asp:TextBox>
            </div>
            <div class="divColunas" style="width: 24%;">
                Bairro<br />
                <asp:TextBox ID="txtBairro" runat="server" ReadOnly="True"></asp:TextBox>
            </div>
            <div class="divColunas" style="width: 16%;">
                CEP<br />
                <asp:TextBox ID="txtCEP" runat="server" ReadOnly="True"></asp:TextBox>
            </div>
        </div>
        <div class="divTabela divTabelaBgColor">
            <div class="divColunas" style="width: 28%;">
                Cidade<br />
                <asp:TextBox ID="txtCidade" runat="server" ReadOnly="True"></asp:TextBox>
            </div>
            <div class="divColunas" style="width: 9%;">
                UF<br />
                <asp:TextBox ID="txtUF" runat="server" ReadOnly="True"></asp:TextBox>
            </div>
            <div class="divColunas" style="width: 42%;">
                Comentários<br />
                <asp:TextBox ID="txtComentarios" runat="server" ReadOnly="True"></asp:TextBox>
            </div>
            <div class="divColunas" style="width: 19%;">
                Contato<br />
                <asp:TextBox ID="txtContato" runat="server" ReadOnly="True"></asp:TextBox>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnSnapEndereco" runat="server" Visible="false">
        <div id="linhaEndereco" class="divTabela">
            <componentart:Snap ID="SnapEndereco" runat="server" Width="160" IsCollapsed="true"
                MustBeDocked="True" CurrentDockingIndex="0" ExpandCollapseMode="Animated" DraggingStyle="none"
                DockingStyle="none" Height="70" DockingContainers="linhaEndereco" CurrentDockingContainer="linhaEndereco">
                <ContentTemplate>
                    <asp:DataGrid ID="dgSnapEnderecos" runat="server" Width="100%" GridLines="Vertical"
                        BorderColor="#8AAD8A" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False">
                        <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                        <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                        <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="Logradouro_key" HeaderText="Endereco"></asp:BoundColumn>
                            <asp:BoundColumn DataField="complemento" HeaderText="Complemento"></asp:BoundColumn>
                            <asp:BoundColumn DataField="bairro" HeaderText="Bairro"></asp:BoundColumn>
                            <asp:BoundColumn DataField="municipio" HeaderText="Cidade"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UF" HeaderText="UF">
                                <HeaderStyle Width="30px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="contato" HeaderText="Contato"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </ContentTemplate>
                <HeaderTemplate>
                    <div class="txt_peq barraTituloSnap" onclick="<%=SnapEndereco.ClientID + ".ToggleExpand();" %>">
                        <div>
                            Lista de Endereços
                        </div>
                    </div>
                </HeaderTemplate>
            </componentart:Snap>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnTelefone" runat="server" CssClass="txt_peq divTabela divTabelaBgColor"
        Visible="true">
        <div class="divColunas" style="width: 29%">
            Descrição<br />
            <asp:TextBox ID="txtDescricaoTelefone" runat="server" ReadOnly="True"></asp:TextBox></div>
        <div class="divColunas" style="width: 29%">
            Contato<br />
            <asp:TextBox ID="txtContatoTelefone" runat="server" ReadOnly="True"></asp:TextBox></div>
        <div class="divColunas" style="width: 10%">
            DDD<br />
            <asp:TextBox ID="txtDDDTelefone" runat="server" ReadOnly="True"></asp:TextBox></div>
        <div class="divColunas" style="width: 22%">
            Fone<br />
            <asp:TextBox ID="txtTelefone" runat="server" ReadOnly="True"></asp:TextBox></div>
        <div class="divColunas" style="width: 7.5%">
            Ramal<br />
            <asp:TextBox ID="txtRamal" runat="server" ReadOnly="True"></asp:TextBox></div>
    </asp:Panel>
    <asp:Panel ID="pnSnapTelefones" runat="server" Visible="false">
        <div class="divTabela" id="linhaTelefone">
            <componentart:Snap ID="SnapTelefone" runat="server" Width="160" IsCollapsed="false"
                MustBeDocked="True" CurrentDockingIndex="0" ExpandCollapseMode="Animated" DraggingStyle="none"
                DockingStyle="none" Height="70" DockingContainers="linhaTelefone" CurrentDockingContainer="linhaTelefone">
                <ContentTemplate>
                    <asp:DataGrid ID="dgSnapTelefones" runat="server" Width="100%" GridLines="Vertical"
                        BorderColor="#8AAD8A" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False">
                        <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                        <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                        <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="descricao" HeaderText="&amp;nbsp;Descri&#231;&#227;o"></asp:BoundColumn>
                            <asp:BoundColumn DataField="contato" HeaderText="&amp;nbsp;Contato"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ddd_key" HeaderText="&amp;nbsp;DDD">
                                <HeaderStyle Width="30px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="fone_key" HeaderText="&amp;nbsp;Fone">
                                <HeaderStyle Width="70px"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ramal" HeaderText="&amp;nbsp;Ramal">
                                <HeaderStyle Width="40px"></HeaderStyle>
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </ContentTemplate>
                <HeaderTemplate>
                    <div class="txt barraTituloSnap" onclick="<%=SnapTelefone.ClientID + ".ToggleExpand();" %>">
                        <div>
                            Lista de Telefones
                        </div>
                    </div>
                </HeaderTemplate>
            </componentart:Snap>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnEmail" runat="server" CssClass="txt_peq divTabela divTabelaBgColor"
        Visible="true">
        <div class="txt_peq divTabela">
            <label>
                E-mail</label>
            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnSnapEmails" runat="server" Visible="false">
        <div class="divTabela" id="linhaEmail">
            <componentart:Snap ID="SnapEmail" runat="server" Width="160" IsCollapsed="false"
                MustBeDocked="True" CurrentDockingIndex="0" ExpandCollapseMode="Animated" DraggingStyle="none"
                DockingStyle="none" Height="70" DockingContainers="linhaEmail" CurrentDockingContainer="linhaEmail">
                <ContentTemplate>
                    <asp:DataGrid ID="dgSnapEmails" runat="server" Width="100%" GridLines="Vertical"
                        BorderColor="#8AAD8A" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False">
                        <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                        <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                        <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="Descricao" HeaderText="&nbsp;Descri&ccedil;&atilde;o"></asp:BoundColumn>
                            <asp:BoundColumn DataField="eMail_key" HeaderText="&nbsp;E-mail"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </ContentTemplate>
                <HeaderTemplate>
                    <div class="txt barraTituloSnap" onclick="<%=SnapEmail.ClientID + ".ToggleExpand();" %>">
                        <div>
                            Lista de emails
                        </div>
                    </div>
                </HeaderTemplate>
            </componentart:Snap>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnBarraEdicao" runat="server" CssClass="divTabela">
        <div style="float: right;">
            <asp:Button ID="btnEditarFichar" runat="server" Width="100px" CssClass="botao" Text="Editar Cadastro"
                OnClick="btnEditarFichar_Click"></asp:Button>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlDividas" runat="server">
        <div class="txt barraTituloSnap" onclick="toggleview('linhaDividas');">
            <div>
                Lista de Dívidas
            </div>
        </div>
        <div class="txt divTabela" id="linhaDividas">
            <asp:DataGrid ID="dgSnapDividas" runat="server" Width="100%" GridLines="Vertical"
                BorderColor="#8AAD8A" ShowFooter="True" BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False"
                OnItemDataBound="dgDividas_ItemDataBound" OnItemCreated="dgDividas_ItemCreated">
                <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
                <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
                <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
                <FooterStyle CssClass="dg_header_peq" BackColor="#729c19" ForeColor="#000000"></FooterStyle>
                <Columns>
                    <asp:BoundColumn DataField="XmPathCliente" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Carteira"></asp:BoundColumn>
                    <asp:BoundColumn DataField="IdTipoDivida" HeaderText="Tipo da Dívida"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Contrato" HeaderText="Contrato"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NumDoc" HeaderText="Num. Doc" DataFormatString="{0:000000}">
                        <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DataVencimento" HeaderText="Vencimento" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="75px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ValorNominal" HeaderText="Valor Nominal" DataFormatString="{0:N}">
                        <HeaderStyle Width="80px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Valor Corrigido">
                        <HeaderStyle Width="80px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Baixa">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>            
        </div>
    </asp:Panel>
    <asp:Panel ID="pnObsCadastro" runat="server" Visible="False">
        <div class="divTabela txt">
            Observações sobre o Devedor:<br />
            <asp:TextBox ID="txtObsDevedor" runat="server" Height="60px" TextMode="MultiLine"></asp:TextBox>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnSnapAcionamentos" runat="server" CssClass="divTabela txt">
        Acionamentos
        <br />
        <anthem:DataGrid ID="dgAcionamentos" runat="server" Width="100%" GridLines="Vertical"
            CellPadding="0" AutoGenerateColumns="False" BorderWidth="1px" BorderColor="#8AAD8A"
            OnItemDataBound="dgAcionamentos_ItemDataBound">
            <AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
            <ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
            <HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
            <Columns>
                <asp:BoundColumn DataField="IdUsuario" HeaderText="&#160;Usu&#225;rio">
                    <headerstyle horizontalalign="Center"></headerstyle>
                    <itemstyle wrap="False"></itemstyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DataAcionamento" HeaderText="Acionamento" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                    <headerstyle horizontalalign="Center" width="115px"></headerstyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DataPromessa" HeaderText="Promessa" DataFormatString="{0:dd/MM/yyyy}">
                    <headerstyle horizontalalign="Center" width="75px"></headerstyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="IdTipoAcionamento" HeaderText="Tipo Acionamento">
                    <headerstyle horizontalalign="Center"></headerstyle>
                    <itemstyle wrap="False"></itemstyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TextoRespeito" HeaderText="Texto a Respeito" DataFormatString="{0:dd/MM/yyyy}">
                    <headerstyle horizontalalign="Center"></headerstyle>
                </asp:BoundColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
        </anthem:DataGrid>
    </asp:Panel>
    <br />
    <center>
        <anthem:Button ID="btnSimulacao" runat="server" Width="160px" CssClass="botao" Text="Simulação de Acordo"
            OnClick="btnSimulacao_Click" />&nbsp;
        <anthem:Button ID="btnCadastroAlertas" runat="server" Width="160px" Text="Cadastrar Alertas"
            CssClass="botao" OnClick="btnCadastroAlertas_Click" />
    </center>
    <br />
    <div class="divTabela txt">
        Tipo Acionamento:&nbsp;
        <anthem:DropDownList ID="ddlTipoAcionamento" runat="server" DataValueField="Codigo"
            DataTextField="Descricao" AutoCallBack="True" OnSelectedIndexChanged="ddlTipoAcionamento_SelectedIndexChanged"
            Width="200px">
        </anthem:DropDownList>
        &nbsp;
        <anthem:Panel ID="pnPromessa" runat="server" Visible="False">
            <anthem:Label ID="lblDescDataPromesa" runat="server">Data de Promessa:</anthem:Label>&nbsp;
            <anthem:TextBox ID="txtDataPromessa_DATA" runat="server" Width="70px"></anthem:TextBox>
        </anthem:Panel>
        &nbsp;
        <anthem:Panel ID="pnCartas" runat="server" Visible="False">
            Tipo de Cartas:&nbsp;<select id="ddlCartas" style="width: 100px">
                <option selected="selected" value="1">Aviso</option>
                <option value="2">Judicial</option>
            </select>
            <anthem:CheckBox ID="ckbSegundoAviso" runat="server" Text="Segundo Aviso"></anthem:CheckBox>&nbsp;
            <input class="botao" id="btnEmitirCarta" style="width: 80px" type="button" value="Emitir Carta"
                name="btnEmitirCarta" runat="server">
        </anthem:Panel>
    </div>
    <div class="divTabela txt">
        <div class="txt divColunas" style="width: 80%; padding-right: 15px;">
            Texto a Respeito&nbsp;do Acionamento
            <br />
            <anthem:TextBox ID="txtAcionamentos" runat="server" Width="100%" TextMode="MultiLine"></anthem:TextBox>
        </div>
        <div class="txt divColunas" style="width: 10%; padding-top: 20px;">
            <anthem:Button ID="btnAdicAcionamento" runat="server" CssClass="botao" Text="Adicionar"
                OnClick="btnAdicAcionamento_Click"></anthem:Button>
        </div>
    </div>
    <br />
    <div class="divTabela" style="height: 30px">
        <anthem:Button ID="btnSalvar" runat="server" BorderStyle="Solid" ImageUrl="../imagens/botoes/btn_salvar.gif"
            Width="71" Height="18" CssClass="botaoAzul" Text="Salvar" OnClick="btnSalvar_Click">
        </anthem:Button>
        &nbsp;
        <input type="button" value="Cancelar" style="width: 71px; height: 18px" class="botaoVermelho"
            onclick="javascript:window.parent.hidePopWin();">
    </div>
</asp:Content>

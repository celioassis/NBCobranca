<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="cc1" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Page language="c#" Codebehind="ModalTeste.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.ModalTeste" %>
<%@ Register TagPrefix="componentart" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ModalTeste</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/links.css" rel="stylesheet">
		<LINK href="../estilos/texto.css" rel="stylesheet">
		<script language="JavaScript" src="../scripts/navegador_css.js"></script>
		<script language="JavaScript" src="../scripts/trata_campo.js"></script>
		<script language="JavaScript" src="../scripts/relatorio.js"></script>
		<script language="JavaScript" src="../scripts/AnthemCallBack.js"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" marginwidth="0" marginheight="0">
		<form id="Form1" method="post" runat="server">
			<cc1:messagebox id="MessageBox" runat="server" PastaStyles="../estilos" PastaScripts="../scripts"
				UsandoAjaxAnthem="True"></cc1:messagebox>
			<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR align="right" height="52">
					<TD vAlign="top" colSpan="3" height="52">
						<TABLE cellSpacing="0" cellPadding="0" width="100%" background="../imagens/tela_peq_topo_meio.jpg"
							border="0">
							<TR>
								<TD style="BACKGROUND-IMAGE: url(../imagens/tela_peq_topo_left.jpg); BACKGROUND-REPEAT: no-repeat"
									height="52">
									<TABLE height="34" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD vAlign="top" width="30">&nbsp;</TD>
											<TD class="titulo" vAlign="top">Ficha de Acionamento</TD>
										</TR>
									</TABLE>
								</TD>
								<TD style="BACKGROUND-POSITION-X: right; BACKGROUND-IMAGE: url(../imagens/tela_peq_topo_right.jpg); BACKGROUND-REPEAT: no-repeat"
									width="55"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<!-- *** Linha da Tabela onde esta o conteúdo da página *** -->
				<TR height="100%">
					<TD vAlign="top" width="12" background="../imagens/tela_peq_f_lateral.jpg"><IMG height="25" src="../imagens/tela_peq_f_lateral.jpg" width="12"></TD>
					<TD vAlign="top" width="100%" colSpan="2"><asp:panel id="pnPF" Runat="server">
							<TABLE id="tabPF" cellSpacing="0" cellPadding="2" width="99%" border="0">
								<TR>
									<TD colSpan="3" height="6"></TD>
								</TR>
								<TR>
									<TD class="txt" vAlign="top" width="58%">Nome<BR>
										<asp:TextBox id="txtNome" runat="server" ReadOnly="True"></asp:TextBox></TD>
									<TD class="txt" width="21%">CPF
										<BR>
										<asp:TextBox id="txtCPF" runat="server" ReadOnly="True"></asp:TextBox></TD>
									<TD class="txt" vAlign="top" width="21%">RG
										<BR>
										<asp:TextBox id="txtRG" runat="server" ReadOnly="True"></asp:TextBox></TD>
								</TR>
							</TABLE>
						</asp:panel><asp:panel id="pnPJ" Runat="server" Visible="False">
							<TABLE id="tabPJ" cellSpacing="0" cellPadding="0" width="99%" border="0">
								<TR>
									<TD>
										<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
											<TR>
												<TD colSpan="2" height="6"></TD>
											</TR>
											<TR>
												<TD class="txt" vAlign="top" width="54%">Razão Social
													<BR>
													<asp:TextBox id="txtRazaoSocial" runat="server" ReadOnly="True"></asp:TextBox></TD>
												<TD class="txt" width="46%">Nome Fantasia
													<BR>
													<asp:TextBox id="txtNomeFantasia" runat="server" ReadOnly="True"></asp:TextBox></TD>
											</TR>
										</TABLE>
										<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
											<TR>
												<TD class="txt" vAlign="top" width="25%">CNPJ
													<BR>
													<asp:TextBox id="txtCNPJ" runat="server" ReadOnly="True"></asp:TextBox></TD>
												<TD class="txt" width="33%">Inscrição Estadual
													<BR>
													<asp:TextBox id="txtInscEstadual" runat="server" ReadOnly="True"></asp:TextBox></TD>
												<TD class="txt" width="42%">&nbsp;</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</asp:panel><asp:panel id="pnEndereco" Runat="server">
							<TABLE cellSpacing="1" cellPadding="0" width="99%" border="0">
								<TR>
									<TD vAlign="top" width="48%">
										<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
											<TR bgColor="#f8f9ff">
												<TD class="txt_peq" vAlign="top" width="37%" bgColor="#f8f9f6">Endereço<BR>
													<asp:TextBox id="txtEndereco" Runat="server" ReadOnly="True"></asp:TextBox></TD>
												<TD class="txt_peq" vAlign="top" width="21%" bgColor="#f8f9f6">Complemento<BR>
													<asp:TextBox id="txtComplemento" Runat="server" ReadOnly="True"></asp:TextBox></TD>
												<TD class="txt_peq" vAlign="top" width="25%" bgColor="#f8f9f6">Bairro<BR>
													<asp:TextBox id="txtBairro" Runat="server" ReadOnly="True"></asp:TextBox></TD>
												<TD class="txt_peq" vAlign="top" width="17%" bgColor="#f8f9f6">CEP<BR>
													<asp:TextBox id="txtCEP" Runat="server" ReadOnly="True"></asp:TextBox></TD>
											</TR>
										</TABLE>
										<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
											<TR>
												<TD class="txt_peq" vAlign="top" width="28%" bgColor="#f8f9f6">Cidade<BR>
													<asp:TextBox id="txtCidade" Runat="server" ReadOnly="True"></asp:TextBox></TD>
												<TD class="txt_peq" width="9%" bgColor="#f8f9f6">UF<BR>
													<asp:TextBox id="txtUF" Runat="server" ReadOnly="True"></asp:TextBox></TD>
												<TD class="txt_peq" vAlign="top" width="42%" bgColor="#f8f9f6">Comentários<BR>
													<asp:TextBox id="txtComentarios" Runat="server" ReadOnly="True"></asp:TextBox></TD>
												<TD class="txt_peq" vAlign="top" width="21%" bgColor="#f8f9f6">Contato<BR>
													<asp:TextBox id="txtContato" Runat="server" ReadOnly="True"></asp:TextBox></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</asp:panel><asp:panel id="pnSnapEndereco" Runat="server" Visible="False"><!--- Inicio Snap de Endereço --->
							<TABLE cellSpacing="0" cellPadding="0" width="99%" border="0">
								<TR>
									<TD class="txt" id="linhaEndereco">
										<componentart:snap id="SnapEndereco" runat="server" CurrentDockingContainer="linhaEndereco" DockingContainers="linhaEndereco"
											DockingStyle="none" DraggingStyle="none" ExpandCollapseMode="Animated" CurrentDockingIndex="0"
											MustBeDocked="True" IsCollapsed="false" Width="160" Height="70">
											<FOOTER></FOOTER>
											<CONTENT>
												<asp:DataGrid id="dgSnapEnderecos" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="0"
													BorderWidth="1px" BorderColor="#8AAD8A" GridLines="Vertical">
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
											</CONTENT>
											<HEADER>
												<TABLE style="CURSOR: hand" onclick="SnapEndereco.ToggleExpand();" cellSpacing="0" cellPadding="0"
													width="100%" bgColor="#a4c955" border="0">
													<TR>
														<TD class="txt_peq">
															<asp:Label id="lblTituloSnapEnd" runat="server">Lista de Endereços</asp:Label></TD>
														<TD align="right" width="10"><IMG height="16" src="../imagens/botoes/btn_abrefecha.jpg" width="22" border="0"></TD>
													</TR>
												</TABLE>
											</HEADER>
										</componentart:snap></TD>
								</TR>
							</TABLE> <!--- Fim do Snap Endereço ---></asp:panel><asp:panel id="pnTelefone" Runat="server">
							<TABLE cellSpacing="1" cellPadding="0" width="99%" border="0">
								<TR>
									<TD vAlign="top" width="48%">
										<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
											<TR bgColor="#f8f9f6">
												<TD class="txt_peq" vAlign="top" width="58%">Contato<BR>
													<asp:TextBox id="txtContatoTelefone" Runat="server" ReadOnly="True"></asp:TextBox></TD>
												<TD class="txt_peq" vAlign="top" width="10%">DDD<BR>
													<asp:TextBox id="txtDDDTelefone" Runat="server" ReadOnly="True"></asp:TextBox></TD>
												<TD class="txt_peq" vAlign="top" width="22%">Fone<BR>
													<asp:TextBox id="txtTelefone" Runat="server" ReadOnly="True"></asp:TextBox></TD>
												<TD class="txt_peq" vAlign="top" width="10%">Ramal<BR>
													<asp:TextBox id="txtRamal" Runat="server" ReadOnly="True"></asp:TextBox></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</asp:panel><asp:panel id="pnSnapTelefones" Runat="server" Visible="False"><!--- Inicio Snap de Telefones --->
							<TABLE cellSpacing="0" cellPadding="0" width="99%" border="0">
								<TR>
									<TD class="txt" id="linhaTelefone">
										<componentart:snap id="SnapTelefone" runat="server" CurrentDockingContainer="linhaTelefone" DockingContainers="linhaTelefone"
											DockingStyle="none" DraggingStyle="none" ExpandCollapseMode="Animated" CurrentDockingIndex="0"
											MustBeDocked="True" IsCollapsed="false" Width="160" Height="70">
											<CONTENT>
												<asp:DataGrid id="dgSnapTelefones" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="0"
													BorderWidth="1px" BorderColor="#8AAD8A" GridLines="Vertical">
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
											</CONTENT>
											<HEADER>
												<TABLE style="CURSOR: hand" onclick="SnapTelefone.ToggleExpand();" cellSpacing="0" cellPadding="0"
													width="100%" bgColor="#a4c955" border="0">
													<TR>
														<TD class="txt_peq">
															<asp:Label id="lblTituloSnapTel" runat="server">Telefones</asp:Label></TD>
														<TD align="right" width="10"><IMG height="16" src="../imagens/botoes/btn_abrefecha.jpg" width="22" border="0"></TD>
													</TR>
												</TABLE>
											</HEADER>
										</componentart:snap></TD>
								</TR>
							</TABLE> <!--- Fim do Snap Telefones ---></asp:panel><asp:panel id="pnEmail" Runat="server">
							<TABLE cellSpacing="1" cellPadding="0" width="99%" border="0">
								<TR>
									<TD vAlign="top" width="48%">
										<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
											<TR bgColor="#f8f9f6">
												<TD class="txt_peq" vAlign="top" width="100%">E-mail<BR>
													<asp:TextBox id="txtEmail" Runat="server" ReadOnly="True"></asp:TextBox></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</asp:panel><asp:panel id="pnSnapEmails" Runat="server" Visible="False"><!--- Inicio Snap de E-mails --->
							<TABLE cellSpacing="0" cellPadding="0" width="99%" border="0">
								<TR>
									<TD class="txt" id="linhaEmail">
										<componentart:snap id="SnapEmail" runat="server" CurrentDockingContainer="linhaEmail" DockingContainers="linhaEmail"
											DockingStyle="none" DraggingStyle="none" ExpandCollapseMode="Animated" CurrentDockingIndex="0"
											MustBeDocked="True" IsCollapsed="false" Width="160" Height="70">
											<CONTENT>
												<asp:datagrid id="dgSnapEmails" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="0"
													BorderWidth="1px" BorderColor="#8AAD8A" GridLines="Vertical">
													<AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
													<ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
													<HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
													<Columns>
														<asp:BoundColumn DataField="Descricao" HeaderText="&nbsp;Descri&ccedil;&atilde;o"></asp:BoundColumn>
														<asp:BoundColumn DataField="eMail_key" HeaderText="&nbsp;E-mail"></asp:BoundColumn>
													</Columns>
													<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
												</asp:datagrid>
											</CONTENT>
											<HEADER>
												<TABLE style="CURSOR: hand" onclick="SnapEmail.ToggleExpand();" cellSpacing="0" cellPadding="0"
													width="100%" bgColor="#a4c955" border="0">
													<TR>
														<TD class="txt_peq">
															<asp:Label id="lblTituloSnapEma" runat="server">Lista de emails</asp:Label></TD>
														<TD align="right" width="10"><IMG height="16" src="../imagens/botoes/btn_abrefecha.jpg" width="22" border="0"></TD>
													</TR>
												</TABLE>
											</HEADER>
										</componentart:snap></TD>
								</TR>
							</TABLE> <!--- Fim do Snap Emails ---></asp:panel><asp:panel id="pnBarraEdicao" Runat="server" Width="100%">
							<TABLE cellSpacing="0" cellPadding="2" width="99%" border="0">
								<TR>
									<TD align="right">
										<asp:Button id="btnEditarFichar" Runat="server" Width="100px" CssClass="botao" Text="Editar Cadastro"></asp:Button></TD>
								</TR>
							</TABLE>
						</asp:panel><!-- Inicio do Panel Financeiro --><asp:panel id="pnlDividas" runat="server"><!--- Inicio Snap de Dívidas--->
							<TABLE cellSpacing="0" cellPadding="0" width="99%" border="0">
								<TR>
									<TD class="txt" id="linhaDividas">
										<componentart:snap id="SnapDividas" runat="server" CurrentDockingContainer="linhaDividas" DockingContainers="linhaDividas"
											DockingStyle="none" DraggingStyle="none" ExpandCollapseMode="Animated" CurrentDockingIndex="0"
											MustBeDocked="True" IsCollapsed="false" Width="160" Height="70">
											<FOOTER></FOOTER>
											<CONTENT>
												<asp:DataGrid id="dgSnapDividas" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="0"
													BorderWidth="1px" BorderColor="#8AAD8A" GridLines="Vertical" ShowFooter="True" OnItemDataBound="dgDividas_ItemDataBound"
													OnItemCreated="dgDividas_ItemCreated">
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
											</CONTENT>
											<HEADER>
												<TABLE style="CURSOR: hand" onclick="SnapDividas.ToggleExpand();" cellSpacing="0" cellPadding="0"
													width="100%" bgColor="#a4c955" border="0">
													<TR>
														<TD class="txt_peq">
															<asp:Label id="lbtnTituloSnapDivida" runat="server">Dívidas</asp:Label></TD>
														<TD align="right" width="10"><IMG height="16" src="../imagens/botoes/btn_abrefecha.jpg" width="22" border="0"></TD>
													</TR>
												</TABLE>
											</HEADER>
										</componentart:snap></TD>
								</TR>
							</TABLE>
						</asp:panel> <!--- Fim do Snap Dívidas ---><asp:panel id="pnObsCadastro" Runat="server" Visible="False">
							<TABLE cellSpacing="0" cellPadding="2" width="99%" border="0">
								<TR>
									<TD class="txt">Observações sobre o Devedor:
										<asp:TextBox id="txtObsDevedor" Runat="server" Height="60px" TextMode="MultiLine"></asp:TextBox></TD>
								</TR>
							</TABLE>
						</asp:panel><asp:panel id="pnSnapAcionamentos" Runat="server">
							<TABLE cellSpacing="0" cellPadding="0" width="99%" border="0">
								<TR>
									<TD class="txt_peq">Acionamentos</TD>
								</TR>
								<TR>
									<TD>
										<asp:DataGrid id="dgAcionamentos" runat="server" Width="100%" GridLines="Vertical" BorderColor="#8AAD8A"
											BorderWidth="1px" CellPadding="0" AutoGenerateColumns="False">
											<AlternatingItemStyle CssClass="dg_item" BackColor="#F9FCF3"></AlternatingItemStyle>
											<ItemStyle CssClass="dg_item" BackColor="#E9EFDD"></ItemStyle>
											<HeaderStyle CssClass="dg_header_peq" BackColor="#729C19"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="IdUsuario" HeaderText="&#160;Usu&#225;rio">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DataAcionamento" HeaderText="Acionamento" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
													<HeaderStyle HorizontalAlign="Center" Width="115px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DataPromessa" HeaderText="Promessa" DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle HorizontalAlign="Center" Width="75px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="IdTipoAcionamento" HeaderText="Tipo Acionamento">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle Wrap="False"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TextoRespeito" HeaderText="Texto a Respeito" DataFormatString="{0:dd/MM/yyyy}">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Left" Mode="NumericPages"></PagerStyle>
										</asp:DataGrid></TD>
								</TR>
							</TABLE>
						</asp:panel><BR> <!-- Inicio da Tabela de lançamento de Acionamento -->
						<TABLE class="txt" cellSpacing="0" cellPadding="2" width="100%" border="0">
							<TR>
								<TD width="100">Tipo Acionamento:</TD>
								<TD width="200"><asp:dropdownlist id="ddlTipoAcionamento" Runat="server" DataTextField="Descricao" DataValueField="Codigo"
										AutoPostBack="True" onselectedindexchanged="ddlTipoAcionamento_SelectedIndexChanged"></asp:dropdownlist></TD>
								<TD><asp:panel id="pnPromessa" Runat="server" Visible="False">
<asp:label id="lblDescDataPromesa" Runat="server">Data de Promessa:</asp:label>&nbsp; 
<asp:textbox id="txtDataPromessa_DATA" Runat="server" Width="70px"></asp:textbox></asp:panel><asp:panel id="pnCartas" Runat="server" Visible="False">
<asp:CheckBox id="ckbSegundoAviso" Runat="server" Text="Segundo Aviso"></asp:CheckBox>&nbsp; 
<INPUT class="botao" id="btnEmitirCarta" style="WIDTH: 80px" type="button" value="Emitir Carta"
											name="btnEmitirCarta" runat="server"> 
            </asp:panel></TD>
							</TR>
						</TABLE>
						<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
							<TR>
								<TD class="txt" vAlign="top" width="80%">Texto a Respeito&nbsp;do Acionamento
									<BR>
									<asp:textbox id="txtAcionamentos" runat="server" Width="100%" TextMode="MultiLine"></asp:textbox></TD>
								<TD class="txt" vAlign="bottom"><asp:button id="btnAdicAcionamento" Runat="server" CssClass="botao" Text="Adicionar" onclick="btnAdicAcionamento_Click"></asp:button></TD>
							</TR>
						</TABLE>
						<BR> <!-- Inicio da tabela de Botões -->
						<TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
							<TR>
								<TD vAlign="top"><anthem:button id="BtnSalvar" Runat="server" CssClass="botao" Text="Salvar" height="18" width="71" onclick="BtnSalvar_Click"></anthem:button>&nbsp;<A href="javascript:window.parent.hidePopWin()"><IMG src="../imagens/botoes/btn_cancelar.gif" border="0"></A>
								</TD>
							</TR>
						</TABLE>
						<div align="center"><br>
							<anthem:button id="btnFechar" runat="server" Width="128px" Text="Fechar Modal" onclick="btnFechar_Click"></anthem:button><anthem:button id="btnNovaModal" runat="server" Width="161px" Text="Abrir Nova Janela Modal" onclick="btnNovaModal_Click"></anthem:button></div>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>

<%@ Register TagPrefix="cc1" Namespace="NBWebControls" Assembly="NBWebControls" %>
<%@ Page language="c#" Codebehind="funcionarios_cadastro.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.aspx.funcionarios_cadastro" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>.:: Neobridge Sistema de Cobrança Web ::.</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../estilos/links.css" rel="stylesheet">
		<LINK href="../estilos/texto.css" rel="stylesheet">
		<script language="JavaScript" src="../scripts/navegador_css.js"></script>
		<script language="JavaScript" src="../scripts/mostra_esconde_campos.js"></script>
		<script language="JavaScript" src="../scripts/trata_campo.js"></script>
		<script language="JavaScript" src="../scripts/verifica_senha.js"></script>
</HEAD>
	<body style="OVERFLOW:auto" leftMargin="0" topMargin="0" rightMargin="0" marginwidth="0"
		marginheight="0">
		<form id="frmFuncionariosCadastro" method="post" runat="server">
			<cc1:MessageBox id="MessageBox" runat="server" PastaScripts="../scripts" PastaStyles="../estilos"></cc1:MessageBox>
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr align="right" height="52">
					<td vAlign="top" colSpan="3" height="52">
						<table cellSpacing="0" cellPadding="0" width="100%" background="../imagens/tela_peq_topo_meio.jpg"
							border="0">
							<tr>
								<td width="333" background="../imagens/tela_peq_topo_left.jpg" height="52">
									<table height="34" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td vAlign="top" width="30">&nbsp;</td>
											<td class="titulo" vAlign="top">Cadastro de Funcionários
											</td>
										</tr>
									</table>
								</td>
								<td align="right"><IMG height="52" src="../imagens/tela_peq_topo_right.jpg" width="223"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td vAlign="top" width="12" background="../imagens/tela_peq_f_lateral.jpg"><IMG height="25" src="../imagens/tela_peq_f_lateral.jpg" width="12"></td>
					<td vAlign="top" width="100%" colSpan="2">
						<table cellSpacing="0" cellPadding="2" width="99%" border="0">
							<tr>
								<td class="txt" vAlign="top" width="42%">Nome do Funcionário<br>
									<asp:TextBox id="txtNomeFuncionario_STR" runat="server" MaxLength="50" AutoPostBack="True" ontextchanged="txtNomeFuncionario_STR_TextChanged"></asp:TextBox>
								</td>
								<td class="txt" width="22%">Data de Nascimento<br>
									<input id="txtDataNascimento_DATA" type="text" name="txtDataNascimento_DATA" runat="server">
								</td>
								<td class="txt" vAlign="top" width="19%">CPF<br>
									<asp:TextBox id="txtCPF_CPF" runat="server"></asp:TextBox>
								</td>
								<td class="txt" vAlign="top" width="17%">RG<br>
									<input id="txtRG_RG" type="text" name="txtRG_RG" runat="server">
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="2" width="99%" border="0">
							<tr>
								<td class="txt" vAlign="top" width="42%">Endereço<br>
									<input id="txtEndereco_TUD" type="text" name="txtEndereco_TUD" runat="server" maxlength="130">
								</td>
								<td class="txt" width="22%">Complemento<br>
									<input id="txtComplemento_TUD" type="text" name="txtComplemento_TUD" runat="server" maxlength="50"></td>
								<td class="txt" width="36%">Bairro<br>
									<input id="txtBairro_TUD" type="text" name="txtBairro_TUD" runat="server" maxlength="35">
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="2" width="99%" border="0">
							<tr>
								<td class="txt" vAlign="top" width="18%">CEP<br>
									<input id="txtCEP_CEP" type="text" runat="server" NAME="txtCEP_CEP">
								</td>
								<td class="txt" vAlign="top" width="24%">Cidade<br>
									<input id="txtCidade_STR" type="text" runat="server" NAME="txtCidade_STR" maxlength="35">
								</td>
								<td class="txt" vAlign="top" width="10%">UF<br>
									<select name="selUF" id="selUF" runat="server">
										<option value='AC' selected>AC</option>
										<option value='AL'>AL</option>
										<option value='AM'>AM</option>
										<option value='AP'>AP</option>
										<option value='BA'>BA</option>
										<option value='CE'>CE</option>
										<option value='DF'>DF</option>
										<option value='GO'>GO</option>
										<option value='MA'>MA</option>
										<option value='MG'>MG</option>
										<option value='MS'>MS</option>
										<option value='MT'>MT</option>
										<option value='PA'>PA</option>
										<option value='PB'>PB</option>
										<option value='PE'>PE</option>
										<option value='PI'>PI</option>
										<option value='PR'>PR</option>
										<option value='RJ'>RJ</option>
										<option value='RN'>RN</option>
										<option value='RO'>RO</option>
										<option value='RR'>RR</option>
										<option value='RS'>RS</option>
										<option value='SC'>SC</option>
										<option value='SE'>SE</option>
										<option value='SP'>SP</option>
										<option value='TO'>TO</option>
									</select>
								</td>
								<td class="txt" vAlign="top" width="48%">&nbsp;</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="2" width="99%" border="0">
							<tr>
								<td class="txt" vAlign="top" width="5%">DDD<br>
									<input id="txtDDD_INT" type="text" maxlength="5" runat="server"></td>
								<td class="txt" vAlign="top" width="22%">Telefone Residencial<br>
									<input id="txtTelefoneResidencial_FONE" type="text" runat="server" NAME="txtTelefoneResidencial_FONE"
										maxlength="11">
								</td>
								<td class="txt" width="22%">Celular<br>
									<input id="txtCelular_FONE" type="text" runat="server" NAME="txtCelular_FONE" maxlength="9">
								</td>
								<td class="txt" vAlign="top" width="51%">E-mail<br>
									<input id="txtEmail_EMAIL" type="text" runat="server" NAME="txtEmail_EMAIL">
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="2" width="99%" border="0">
							<tr>
								<td class="txt" vAlign="top" width="30%">Função<br>
									<input id="txtFuncao_TUD" type="text" runat="server" NAME="txtFuncao_TUD">
								</td>
								<td width="19%" vAlign="top" class="txt">Nome de Usuário<br>
									<input id="txtUsuario_STR" type="text" runat="server" NAME="txtUsuario_STR" maxlength="50"></td>
								<td width="15%" vAlign="top" class="txt">Senha<br>
									<input id="txtSenha_TUD" type="password" runat="server" NAME="txtSenha_TUD" maxlength="50"
										onBlur="senhaCaracteres(this)"></td>
								<td width="15%" vAlign="top" class="txt">Repita a Senha<br>
									<input id="txtSenhaRepetir_TUD" type="password" runat="server" NAME="txtSenhaRepetir_TUD"
										maxlength="50" onBlur="senhaConfirma(this)"></td>
								<td width="36%" vAlign="top" class="txt">Funcionário Ativo<br>
									<asp:radiobuttonlist id="rblFuncionarioAtivo" runat="server" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal"
										CssClass="txt" BorderStyle="None" BorderWidth="0px">
										<asp:ListItem Value="1" Selected="True">Sim</asp:ListItem>
										<asp:ListItem Value="0">N&#227;o</asp:ListItem>
									</asp:radiobuttonlist>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="2" width="99%" border="0">
							<tr>
								<td class="txt" vAlign="top">Credencial<br>
									<asp:radiobuttonlist id="rblCredencial" runat="server" CellPadding="0" CellSpacing="0" BorderStyle="None"
										BorderWidth="0px" CssClass="txt">
										<asp:ListItem Value="3">Administrador (&lt;span class=&quot;txt_peq&quot;&gt;Possui acesso a todos fun&#231;&#245;es do sistema inclusive edi&#231;&#227;o de usu&#225;rios e troca de senha&lt;/span&gt;)</asp:ListItem>
										<asp:ListItem Value="2">Encarregado (&lt;span class=&quot;txt_peq&quot;&gt;Possui acesso a maioria das fun&#231;&#245;es&lt;/span&gt;)</asp:ListItem>
										<asp:ListItem Value="1" Selected="True">Acionador (&lt;span class=&quot;txt_peq&quot;&gt;Tem acesso somente a &#225;rea de acionamento de devedores&lt;/span&gt;)</asp:ListItem>
									</asp:radiobuttonlist></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="2" width="99%" border="0">
							<tr>
								<td width="55%" vAlign="top" class="txt">Anotações Adicionais<br>
									<textarea id="txaAnotacoesAdicionais_TUD" cols="42" rows="2" wrap="VIRTUAL" runat="server"></textarea></td>
								<td width="45%" vAlign="top" class="txt">&nbsp;</td>
							</tr>
						</table>
						<br>
						<table width="100%" border="0" cellspacing="0" cellpadding="2">
							<tr>
								<td width="6%"><asp:imagebutton id="imgBtnSalvar" BorderStyle="None" Runat="server" ImageUrl="../imagens/botoes/btn_salvar.gif"
										width="71" height="18"></asp:imagebutton></td>
								<td width="94%"><a href="javascript:window.top.hidePopWin()"><img src="../imagens/botoes/btn_cancelar.gif" border="0"></a></td>
							</tr>
						</table>
						<table width="550" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

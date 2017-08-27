<%@ Page language="c#" Codebehind="rel_Cartas.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.aspx.relatorios.rel_Cartas" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Relação de Cartas para Correspondência</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK media="screen" href="tela.css" type="text/css" rel="stylesheet">
		<LINK media="print" href="impressora.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:DataGrid id="dgCartas" runat="server" AutoGenerateColumns="False" BorderStyle="None" ShowHeader="False"
				GridLines="None" DataKeyField="IdEntidade" Width="100%">
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<DIV id="conteudo">
								<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
									<TR>
										<TD align="center">
											<asp:image id="cabCoest" Runat="server" ImageUrl="../../imagens/cab_coest.gif"></asp:image><BR>
											<BR>
										</TD>
									</TR>
									<tr>
										<td>
											<table cellpadding="0" cellspacing="0" width="100%" border="0">
												<tr>
													<td style="WIDTH: 150px">&nbsp;</td>
													<td>
														<table cellpadding="0" cellspacing="0" width="100%" border="0">
															<TR>
																<TD>IL.mo Sr(a).</TD>
															</TR>
															<TR>
																<TD><B>
																		<%# DataBinder.Eval(Container, "DataItem.NomePrimary") %>
																	</B>
																</TD>
															</TR>
															<TR>
																<TD><%# DataBinder.Eval(Container, "DataItem.Logradouro") %>-
																	<%# DataBinder.Eval(Container, "DataItem.Complemento") %>
																</TD>
															</TR>
															<TR>
																<TD><%# DataBinder.Eval(Container, "DataItem.Bairro") %></TD>
															</TR>
															<TR>
																<TD><%# DataBinder.Eval(Container, "DataItem.CEP") %>-
																	<%# DataBinder.Eval(Container, "DataItem.Municipio") %>
																	-
																	<%# DataBinder.Eval(Container, "DataItem.UF") %>
																	-
																	<%# DataBinder.Eval(Container, "DataItem.Comentario") %>
																</TD>
															</TR>
														</table>
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<TR>
										<TD align="center"><BR>
											<BR>
											<BR>
											<BR>
											<BR>
											<asp:Label id="lblNotificacao" Runat="server" Font-Underline="True" Font-Bold="True">
												NOTIFICAÇÃO URGENTE<br>2° AVISO C/ PROTOCOLO DE NEGATIVAÇÃO
											</asp:Label><BR>
											<BR>
											<BR>
											<BR>
											<BR>
										</TD>
									</TR>
									<TR>
										<TD style="TEXT-INDENT: 50pt; TEXT-ALIGN: justify">Consta em nossos registros 
											dívida de sua responsabilidade, VENCIDA junto INSTITUIÇÃO DE LAGES.</TD>
									</TR>
									<TR>
										<TD style="TEXT-INDENT: 50pt; TEXT-ALIGN: justify">Solicitamos o comparecimento de 
											Vossa Senhoria em nosso escritório de endereço acima especificado, no prazo de 
											05 (cinco) dias, para tratar do assunto, a fim de evitar maiores problemas.</TD>
									</TR>
									<TR>
										<TD style="TEXT-INDENT: 50pt; TEXT-ALIGN: justify">Dentro do prazo supra, além de 
											evitar despesas maiores, podemos estudar as condições da para sua liquidação 
											PARCELADAMENTE.</TD>
									</TR>
									<TR>
										<TD style="TEXT-INDENT: 50pt; TEXT-ALIGN: justify">Caso Vossa Senhoria tenha 
											quitado seu compromisso, aceite nossas desculpas e nos informe através de 
											nossos escritórios, ou pelo fone (49) 3251-6000, para que possamos atualizar 
											nossos registros.
											<BR>
											<BR>
											<BR>
											<BR>
											<BR>
											<BR>
											<BR>
											<BR>
											<BR>
											<BR>
										</TD>
									</TR>
									<TR>
										<TD align="center">Cordialmente.
											<BR>
											<BR>
											COEST – ASSESSORIA EMPRESARIAL LTDA.
										</TD>
									</TR>
								</TABLE>
							</DIV>
							<DIV class="proximaPagina">&nbsp;</DIV>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</form>
	</body>
</HTML>

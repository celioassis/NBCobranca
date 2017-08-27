<%@ Page Language="c#" Codebehind="rel_Carta.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.aspx.relatorios.rel_Carta" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Carta para Correspond�ncia</title>
    <link media="screen" href="tela.css" type="text/css" rel="stylesheet" />
    <link media="print" href="impressora.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div id="conteudo">
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr>
                    <td align="center">
                        <asp:Image ID="cabCoest" ImageUrl="../../imagens/cab_coest.gif" runat="server"></asp:Image><br>
                        <br>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr>
                                <td style="width: 150px">
                                    &nbsp;</td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                IL.mo Sr(a).</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>
                                                    <asp:Label ID="lblDevedor" runat="server"></asp:Label></b></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblEndereco" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblBairro" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCepMunUF" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <br>
                        <br>
                        <br>
                        <br>
                        <br>
                        <asp:Label ID="lblNotificacao" runat="server" Font-Bold="True" Font-Underline="True">
								NOTIFICA��O URGENTE<br>2� AVISO C/ PROTOCOLO DE NEGATIVA��O
                        </asp:Label>
                        <br>
                        <br>
                        <br>
                        <br>
                        <br>
                    </td>
                </tr>
                <tr>
                    <td style="text-indent: 50pt; text-align: justify">
                        Consta em nossos registros d�vida de sua responsabilidade, VENCIDA junto INSTITUI��O
                        DE LAGES.</td>
                </tr>
                <tr>
                    <td style="text-indent: 50pt; text-align: justify">
                        Solicitamos o comparecimento de Vossa Senhoria em nosso escrit�rio de endere�o acima
                        especificado, no prazo de 05 (cinco) dias, para tratar do assunto, a fim de evitar
                        maiores problemas.</td>
                </tr>
                <tr>
                    <td style="text-indent: 50pt; text-align: justify">
                        Dentro do prazo supra, al�m de evitar despesas maiores, podemos estudar as condi��es
                        da para sua liquida��o PARCELADAMENTE.</td>
                </tr>
                <tr>
                    <td style="text-indent: 50pt; text-align: justify">
                        Caso Vossa Senhoria tenha quitado seu compromisso, aceite nossas desculpas e nos
                        informe atrav�s de nossos escrit�rios, ou pelo fone (49) 3251-6000, para que possamos
                        atualizar nossos registros.
                        <br>
                        <br>
                        <br>
                        <br>
                        <br>
                        <br>
                        <br>
                        <br>
                        <br>
                        <br>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        Cordialmente.
                        <br>
                        <br>
                        COEST � ASSESSORIA EMPRESARIAL LTDA.
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

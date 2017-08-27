<%@ Page Language="c#" Codebehind="login.aspx.cs" AutoEventWireup="True" Inherits="NBCobranca.view.Login"
    Title=".:: Neobridge Controle de Cobrança Web - Login ::." %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>.:: Neobridge Controle de Cobrança Web - Login ::.</title>
    <!-- Bootstrap Core CSS -->
    <link href="../Content/bootstrap.min.css" rel="stylesheet">
    <!-- MetisMenu CSS -->
    <link href="../assets/sb-admin/css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet">
    <!-- Custom CSS -->
    <link href="../assets/sb-admin/css/sb-admin-2.css" rel="stylesheet">
    <!-- Bootstrap Dialog CSS -->
    <link href="../assets/bootstrap-dialog/css/bootstrap-dialog.min.css" rel="stylesheet" type="text/css" />
    <!-- Custom Fonts -->
    <link href="../assets/sb-admin/font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet"
        type="text/css">
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Entre com suas credênciais
                        </h3>
                    </div>
                    <div class="panel-body">
                        <form id="frmLogin" runat="server" role="form">
                            <fieldset>
                                <div class="form-group">
                                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Usuário"
                                        autofocus></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="form-control"
                                        placeholder="Senha"></asp:TextBox>
                                </div>
                                <anthem:Button ID="btnEntrar" Text="Entrar" runat="server" CssClass="btn btn-lg btn-success btn-block"
                                    OnClick="btnEntrar_Click" />
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="myModalLabel">
                        Mensagem do Sistema</h4>
                </div>
                <div class="modal-body">
                    Mensagem de Erro de login
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">
                        OK</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->

    <script type="text/javascript" src="../scripts/jquery-1.9.1.min.js"></script>

    <!-- Bootstrap Core JavaScript -->

    <script src="../scripts/bootstrap.min.js"></script>
  
    <script type="text/javascript" src="../assets/bootstrap-dialog/js/bootstrap-dialog.min.js"></script>
    

</body>
</html>

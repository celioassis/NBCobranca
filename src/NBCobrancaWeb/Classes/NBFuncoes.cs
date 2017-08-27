using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Security.Cryptography;
using NBCobranca.Tipos;
using System.Data;
namespace NBCobranca.Classes
{
    /// <summary>
    /// Summary description for EvtFuncoes.
    /// </summary>
    public abstract class NBFuncoes
    {
        static string passPhrase = "Q#$rt&*+B";        // can be any string
        static string saltValue = "s@1tMerda";        // can be any string
        static string hashAlgorithm = "SHA1";             // can be "MD5"
        static int passwordIterations = 2;                  // can be any number
        static string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
        static int keySize = 256;                // can be 192 or 128

        /// <summary>
        /// Rotina para Abilitar e Desabilitar Campos das Paginas.
        /// </summary>
        /// <param name="Abilita">
        /// Valor que será atribuido para a propriedade Enabled ou Disabled dos Controles
        /// </param>
        /// <param name="Page">
        /// Página que Contém os Controles
        /// </param>
        public static void DesabilitaCampos(bool Abilita, System.Web.UI.Page Page)
        {
            //Faz uma busca nos controles da Pagina para encontrar o controle
            //o controle que se refere aos forms.
            foreach (Control Form in Page.Controls)
            {
                //Faz uma busca nos controles do form e aplica o metodo
                //verificatipo para abilitar ou desabilitar os controles.
                foreach (Control Controles in Form.Controls)
                {
                    VerificaTipo(Abilita, Controles);
                }
            }
        }

        /// <summary>
        /// Rotina para Abilitar e Desabilitar Campos das Paginas.
        /// </summary>
        /// <param name="Abilita">
        /// Valor que será atribuido para a propriedade Enabled ou Disabled dos Controles
        /// </param>
        /// <param name="Tabela">
        /// Tabela que contém os Controles
        /// </param>
        public static void DesabilitaCampos(bool Abilita, System.Web.UI.HtmlControls.HtmlTable Tabela)
        {
            //Faz uma busca em todos os controles da tabela
            foreach (Control mTabela in Tabela.Controls)
            {
                //Verifica se o controle encontrado é do tipo HtmlTableRow para poder
                //entrar nas linhas e encontrar as celulas que contenham os controles
                //que serão desabilitados ou abilitados.
                if (mTabela.GetType().FullName == "System.Web.UI.HtmlControls.HtmlTableRow")
                {
                    //Faz um busca em todos os controles da linha
                    foreach (Control Linha in mTabela.Controls)
                    {
                        //Verifica se o controle encontrado é do tipo HtmlTableCell para poder
                        //entrar nas celulas que contenham os controles
                        //que serão desabilitados ou abilitados.
                        if (Linha.GetType().FullName == "System.Web.UI.HtmlControls.HtmlTableCell")
                        {
                            //Faz uma busca em todos os controles da celula
                            //e executa o metodo verificatipo para abilitar
                            //ou desabilitar o controle encontrado na celula.
                            foreach (Control Celula in Linha.Controls)
                            {
                                VerificaTipo(Abilita, Celula);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Faz a Verificação dos Controles Vindos da função DesabilitaCampos.
        /// Os quais receberão o valor do Parametro Abilita nas suas propriedades,
        /// Enabled, Desabled ou Visible.
        /// </summary>
        /// <param name="Abilita">
        /// Valor que será atribuido para a propriedade Enabled ou Disabled dos Controles
        /// </param>
        /// <param name="Controles">
        /// Controle que Será Abilitado ou Desabilitado, conforme o Valor
        ///  do Paramentro Abilita.
        /// </param>
        private static void VerificaTipo(bool Abilita, Control Controles)
        {

            switch (Controles.GetType().FullName)
            {
                case "System.Web.UI.WebControls.TextBox":
                    ((System.Web.UI.WebControls.TextBox)Controles).Enabled = !Abilita;
                    break;
                case "System.Web.UI.HtmlControls.HtmlInputRadioButton":
                    ((System.Web.UI.HtmlControls.HtmlInputRadioButton)Controles).Disabled = Abilita;
                    break;
                case "System.Web.UI.HtmlControls.HtmlInputText":
                    ((System.Web.UI.HtmlControls.HtmlInputText)Controles).Disabled = Abilita;
                    break;
                case "System.Web.UI.HtmlControls.HtmlSelect":
                    ((System.Web.UI.HtmlControls.HtmlSelect)Controles).Disabled = Abilita;
                    break;
                case "System.Web.UI.HtmlControls.HtmlTextArea":
                    ((System.Web.UI.HtmlControls.HtmlTextArea)Controles).Disabled = Abilita;
                    break;
                case "System.Web.UI.WebControls.LinkButton":
                    ((System.Web.UI.WebControls.LinkButton)Controles).Visible = !Abilita;
                    break;
                case "System.Web.UI.WebControls.RadioButtonList":
                    ((System.Web.UI.WebControls.RadioButtonList)Controles).Enabled = !Abilita;
                    break;
                case "System.Web.UI.WebControls.DataGrid":
                    System.Web.UI.WebControls.DataGrid Grid;
                    Grid = (System.Web.UI.WebControls.DataGrid)Controles;
                    Grid.Columns[Grid.Columns.Count - 1].Visible = !Abilita;
                    Grid.Columns[Grid.Columns.Count - 2].Visible = !Abilita;
                    //Grid.Columns[Grid.Columns.Count - 3].Visible=AbilitaContrario;
                    Grid.Enabled = !Abilita;
                    break;
                case "System.Web.UI.WebControls.ImageButton":
                    ((System.Web.UI.WebControls.ImageButton)Controles).Visible = !Abilita;
                    break;

            }

        }
        /// <summary>
        /// Retorna a Conexão com o Banco de Dados da Neobridge
        /// </summary>
        public static System.Data.SqlClient.SqlConnection ConexaoPadrao(NBdbm.self pSelf)
        {
            //Cria-se um objeto do Tipo Connection para receber a conexão
            //da NBdbm.
            NBdbm.Fachadas.Connection nbdbmConn;
            //Cria um novo objeto do tipo Retorno o oqual ira receber a
            //credecial que será validada para que possa ser retornada 
            //uma conexção com o banco de dados.
            NBdbm.tipos.Retorno credencial = new NBdbm.tipos.Retorno();
            //Cria-se uma interface de conexão, a qual receberá a conexão
            //da NBdbm.
            System.Data.IDbConnection Cnn = new System.Data.SqlClient.SqlConnection();
            //Atribui true para a propriedade Sucesso
            credencial.Sucesso = true;
            //atribui a string de Credencial para a propriedade Tag
            credencial.Tag = "=*MZÏ>ÙMÝ";
            credencial.Objeto = "NBdbm";
            //Cria-se uma nova conexão com base na credencial passada por parametro.
            nbdbmConn = new NBdbm.Fachadas.Connection(ref pSelf, credencial);
            //Referencia a conexão criada para o objeto Cnn, o qual será
            //retornado para a aplicação que originou a chamada da função.
            Cnn = nbdbmConn.Connection;
            return (System.Data.SqlClient.SqlConnection)Cnn;
        }
        /// <summary>
        /// Conexão com o Banco de Dados com opção de escolher qual o Banco
        /// através do Tipo de Conexão.
        /// </summary>
        /// <param name="TipoConexao">
        /// Qual o Tipo de Conexão na qual estão relacionados os diversos bancos
        /// do SQLServer.
        /// </param>
        /// <returns>
        /// Retorna uma SQLConection.
        /// </returns>
        public static System.Data.IDbConnection Conexao(NBdbm.self pSelf, NBdbm.tipos.tiposConection TipoConexao)
        {
            try
            {
                //Cria-se um objeto do Tipo Connection para receber a conexão
                //da NBdbm.
                NBdbm.Fachadas.Connection nbdbmConn;
                //Cria um novo objeto do tipo Retorno o oqual ira receber a
                //credecial que será validada para que possa ser retornada 
                //uma conexção com o banco de dados.
                NBdbm.tipos.Retorno credencial = new NBdbm.tipos.Retorno();
                //Cria-se uma interface de conexão, a qual receberá a conexão
                //da NBdbm.
                //System.Data.IDbConnection Cnn = new System.Data.SqlClient.SqlConnection();
                //Atribui true para a propriedade Sucesso
                credencial.Sucesso = true;
                //atribui a string de Credencial para a propriedade Tag
                credencial.Tag = "=*MZÏ>ÙMÝ";
                credencial.Objeto = "NBdbm";
                //Cria-se uma nova conexão com base na credencial passada por parametro.
                nbdbmConn = new NBdbm.Fachadas.Connection(ref pSelf, credencial, TipoConexao);
                //Referencia a conexão criada para o objeto Cnn, o qual será
                //retornado para a aplicação que originou a chamada da função.
                if (nbdbmConn.Connection.State == System.Data.ConnectionState.Closed)
                    nbdbmConn.Connection.Open();

                //			System.Data.SqlClient.SqlConnection Cnn = new System.Data.SqlClient.SqlConnection(nbdbmConn.Connection.ConnectionString);
                //			Cnn.Open();
                return nbdbmConn.Connection;
            }
            catch (Exception ex)
            {
                throw new NBdbm.COBR_Exception("Problemas ao Retornar a Conexão", ex);
            }

        }

        /// <summary>
        /// Cria um Objeto SqlCommand com a StoreProcedure spPaginacao, 
        /// a mesma já deve der sido criada para a paginação funcionar.
        /// </summary>
        /// <param name="pSQL">Instrução SQL que ira retornar os Dados</param>
        /// <param name="pFieldOrder">Campo da Tabela que deverá ser usado para a Classificação</param>
        /// <param name="pTipoOrder">Tipo de Ordenação: 1 - ASC, 2 - DESC</param>
        /// <param name="pCurrentPage">Número da Página que deseja Retornar os Dados.</param>
        /// <param name="pRowsForPage">Número de Linhas por Páginas</param>
        /// <param name="pSqlConn">Objeto SqlConnection</param>
        /// <returns>Retorna um Objeto da Classe System.Data.SqlClient.SqlCommand</returns>
        public static System.Data.SqlClient.SqlCommand SqlCmdPaginacao(string pSQL, string pFieldOrder, int pTipoOrder, int pCurrentPage, int pRowsForPage, object pTipoConexao)
        {
            string mTipoOrdem;
            switch (pTipoOrder)
            {
                case 1:
                    mTipoOrdem = "ASC";
                    break;
                case 2:
                    mTipoOrdem = "DESC";
                    break;
                default:
                    mTipoOrdem = "ASC";
                    break;
            }
            System.Data.SqlClient.SqlCommand mSqlCmd;
            if (pTipoConexao == null)
                mSqlCmd = new System.Data.SqlClient.SqlCommand("spPaginacao");
            else
            {
                NBdbm.tipos.tiposConection mTipoConexao = (NBdbm.tipos.tiposConection)pTipoConexao;
                mSqlCmd = new System.Data.SqlClient.SqlCommand("spPaginacao", (System.Data.SqlClient.SqlConnection)Conexao(new NBdbm.self(), mTipoConexao));
            }
            mSqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            mSqlCmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SQL", System.Data.SqlDbType.VarChar, 4000));
            mSqlCmd.Parameters["@SQL"].Value = pSQL;
            mSqlCmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PAGE", System.Data.SqlDbType.Int));
            mSqlCmd.Parameters["@PAGE"].Value = pCurrentPage + 1;
            mSqlCmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PAGESIZE", System.Data.SqlDbType.Int));
            mSqlCmd.Parameters["@PAGESIZE"].Value = pRowsForPage;
            mSqlCmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ORDER", System.Data.SqlDbType.VarChar, 255));
            mSqlCmd.Parameters["@ORDER"].Value = pFieldOrder;
            mSqlCmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TIPOORDER", System.Data.SqlDbType.VarChar, 4));
            mSqlCmd.Parameters["@TIPOORDER"].Value = mTipoOrdem;
            mSqlCmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROWS_TOTAL", System.Data.SqlDbType.Int));
            mSqlCmd.Parameters["@ROWS_TOTAL"].Direction = System.Data.ParameterDirection.Output;
            mSqlCmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@INT_ERRO", System.Data.SqlDbType.Int));
            mSqlCmd.Parameters["@INT_ERRO"].Direction = System.Data.ParameterDirection.Output;
            mSqlCmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@INT_PAGETOTAL", System.Data.SqlDbType.Int));
            mSqlCmd.Parameters["@INT_PAGETOTAL"].Direction = System.Data.ParameterDirection.Output;
            return mSqlCmd;
        }
        /// <summary>
        /// Move o Foco para um determinado campo na página.
        /// </summary>
        /// <param name="NomeCampo">
        /// Nome do Campo que receberá o Foco.
        /// </param>
        /// <param name="Pagina">
        /// Página que receberá o script para mover o foco, normalmente é a atual.
        /// </param>
        public static void MoverFoco(string NomeCampo, Page Pagina)
        {
            //insere um script no inicio da Pagina o qual ira setar o foco para o campo informado por prametro.
            Pagina.Response.Write("<script>window.onload = function() {document.forms[0].elements['" + NomeCampo + "'].focus()}</script>");
        }

        /// <summary>
        /// Função para a retirada de acentos e ç de uma string
        /// </summary>
        /// <param name="pTexto"></param>
        /// <returns></returns>
        public static string RetiraAcentos(string pTexto)
        {
            /** Troca os caracteres acentuados por não acentuados **/
            char[] acentos = new char[] { 'ç', 'Ç', 'á', 'é', 'í', 'ó', 'ú', 'ý', 'Á', 'É', 'Í', 'Ó', 'Ú', 'Ý', 'à', 'è', 'ì', 'ò', 'ù', 'À', 'È', 'Ì', 'Ò', 'Ù', 'ã', 'õ', 'ñ', 'ä', 'ë', 'ï', 'ö', 'ü', 'ÿ', 'Ä', 'Ë', 'Ï', 'Ö', 'Ü', 'Ã', 'Õ', 'Ñ', 'â', 'ê', 'î', 'ô', 'û', 'Â', 'Ê', 'Î', 'Ô', 'Û' };
            char[] semAcento = new char[] { 'c', 'C', 'a', 'e', 'i', 'o', 'u', 'y', 'A', 'E', 'I', 'O', 'U', 'Y', 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U', 'a', 'o', 'n', 'a', 'e', 'i', 'o', 'u', 'y', 'A', 'E', 'I', 'O', 'U', 'A', 'O', 'N', 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

            for (int i = 0; i < acentos.Length; i++)
            {
                pTexto = pTexto.Replace(acentos[i], semAcento[i]);
            }
            return pTexto;
        }

        public static bool ValidarEmpresa(string NomeEmpresa)
        {
            switch (NomeEmpresa.ToUpper())
            {
                case "LUGPHIL":
                    return true;
                case "NEOBRIDGE":
                    return true;
                default:
                    return false;
            }
        }

        private static bool ValidaCredencial(LimLogin Login, Permissao permite)
        {
            switch (permite)
            {
                case Permissao.Administrador:
                    if (Login.Credencial == TipoCredencial.Administrador)
                        return true;
                    else
                        return false;
                case Permissao.Padrao:
                    switch (Login.Credencial)
                    {
                        case TipoCredencial.Acionador:
                            return false;
                        default:
                            return true;
                    }
                default:
                    return true;
            }
        }
        /// <summary>
        /// Valida a Sessão do Usuário, caso a sessão já tenha expirado Redireciona 
        /// a página para a página de login senão referencia o objeto sistema da Sessão
        /// no objeto de Sistema que esta indo como parametro.
        /// </summary>
        /// <param name="Pagina">Objeto Page Atual</param>
        /// <param name="SIS">Objeto Sistema da página</param>
        public static void ValidarSistema(Page Pagina, ref Sistema SIS)
        {
            if (Anthem.Manager.IsCallBack)
                Anthem.Manager.Register(Pagina);

            if (Pagina.Session["Sistema"] == null)
                Pagina.Response.Redirect("/view/login.aspx");
            else
            {
                SIS = (Sistema)Pagina.Session["Sistema"];
                SIS.PaginaWeb = Pagina;
            }
        }
        /// <summary>
        /// Valida a Sessão do Usuário que esta em uma janela Modal, caso a sessão já 
        /// tenha expirado Fecha a janela modal senão referencia o objeto sistema da Sessão
        /// que esta indo como parametro.
        /// </summary>
        /// <param name="Pagina">Objeto Page Atual</param>
        /// <param name="SIS">Objeto Sistema da página</param>
        /// <param name="pMsgBox">Objeto MessageBox Atual, usado para fechar a janela modal</param>
        public static void ValidarSistema(Page Pagina, ref Sistema SIS, NBWebControls.MessageBox pMsgBox)
        {

            if (Pagina.Session["Sistema"] == null)
            {
                if (Anthem.Manager.IsCallBack || pMsgBox.UsandoAjaxAnthem)
                    Anthem.Manager.Register(Pagina);

                pMsgBox.ModalClose();
            }
            else
                ValidarSistema(Pagina, ref SIS);
        }

        /// <summary>
        /// Valida a Sessão do Usuário, caso a sessão já tenha expirado Redireciona 
        /// a página para a página de login senão referencia o objeto Controller da Sessão
        /// no objeto que esta indo como parametro.
        /// </summary>
        /// <param name="Pagina">Objeto Page Atual</param>
        /// <param name="pController">Factory de controllers</param>
        /// <param name="pEstaEmJanelaModal">Indica se a página atual esta em uma janela modal</param>
        public static void ValidarSistema(Page Pagina, ref Controllers.CtrFactory pController, bool pEstaEmJanelaModal)
        {
            if (Anthem.Manager.IsCallBack)
                Anthem.Manager.Register(Pagina);

            if (Pagina.Session["Sistema"] == null)
            {
                if (pEstaEmJanelaModal)
                {
                    foreach (Control mControl in Pagina.Form.Controls)
                    {
                        if (mControl is NBWebControls.MessageBox)
                        {
                            ((NBWebControls.MessageBox)mControl).Show("Sua Sessão expirou, faça login novamente", true);
                            return;
                        }
                    }
                }
                Pagina.Response.Redirect("login.aspx");
            }
            else
            {
                Sistema mSIS = (Sistema)Pagina.Session["Sistema"];
                mSIS.PaginaWeb = Pagina;

                if (Pagina.Session["Controller"] == null)
                    Pagina.Session.Add("Controller", new Controllers.CtrFactory(mSIS));
                pController = (Controllers.CtrFactory)Pagina.Session["Controller"];
            }
        }

        public static System.Data.DataView DataSourceComboEntidades(NBdbm.tipos.TipoEntidade TipoEnt, NBdbm.tipos.tiposConection TipoConexao)
        {
            //SQL que Retorna todas os Registros de uma entidade, como por ex: 
            //Todos os Clientes, Fornecedores ou Funcionários, conforme o parametro 
            //sqlWhere.
            string comandoSQL = StrSQLDropDownList(TipoEnt);
            return DataView(comandoSQL, (System.Data.SqlClient.SqlConnection)Conexao(new NBdbm.self(), TipoConexao));
        }

        /// <summary>
        /// Preenche um DataView conforme a String de SQL
        /// </summary>
        /// <param name="SQL">string SQL para preencher DataView</param>
        /// <returns>Retorno um DataView conforme a String  de SQL</returns>
        public static System.Data.DataView DataView(string SQL, System.Data.IDbConnection pConnection)
        {
            System.Data.SqlClient.SqlCommand mCommad = new System.Data.SqlClient.SqlCommand(SQL, (System.Data.SqlClient.SqlConnection)pConnection);
            return DataView(mCommad);
        }
        public static System.Data.DataView DataView(System.Data.SqlClient.SqlCommand pSqlCmd)
        {
            System.Data.DataSet mDS = DataSet(pSqlCmd);
            if (mDS.Tables.Count > 0)
                //Retorna o DataView o mesmo será mostrado em uma DataGrid.
                return mDS.Tables[0].DefaultView;
            else
                return null;
        }

        public static System.Data.DataSet DataSet(string SQL, System.Data.IDbConnection pConnection)
        {
            System.Data.SqlClient.SqlCommand mCommad = new System.Data.SqlClient.SqlCommand(SQL, (System.Data.SqlClient.SqlConnection)pConnection);
            return DataSet(mCommad);
        }
        public static System.Data.DataSet DataSet(System.Data.SqlClient.SqlCommand pSqlCmd)
        {
            //Cria-se um novo DataSet para receber os dados da pesquisa.
            System.Data.DataSet mDS = new System.Data.DataSet();
            //Cria-se um DataAdapter para receber o comando sql que ira fazer a consulta.
            System.Data.SqlClient.SqlDataAdapter mDA;
            //Instancia-se o DataAdapter com o ComandoSQL e a conexão com o Banco de Dados.
            mDA = new System.Data.SqlClient.SqlDataAdapter(pSqlCmd);
            //Preenche o DataSet.
            mDA.Fill(mDS);
            pSqlCmd.Connection.Close();
            return mDS;
        }
        /// <summary>
        /// Cria uma SQL para Preencher DropDownList de Acordo com o Tipo da Entidade.
        /// </summary>
        /// <param name="TipoEnti">Define o Tipo da Entidade</param>
        /// <returns></returns>
        private static string StrSQLDropDownList(NBdbm.tipos.TipoEntidade TipoEnti)
        {
            string strSQL = "" +
                "SELECT     dbo.CTRL_Entidades.IdEntidade, dbo.CTRL_Entidades.NomePrimary " +
                "FROM         dbo.CTRL_Entidades INNER JOIN " +
                "dbo.CTRL_Link_Entidade_No ON dbo.CTRL_Entidades.IdEntidade = dbo.CTRL_Link_Entidade_No.IdEntidade INNER JOIN " +
                "dbo.CTRL_Nos ON dbo.CTRL_Link_Entidade_No.IdNo = dbo.CTRL_Nos.IdNo ";

            switch (TipoEnti)
            {
                case NBdbm.tipos.TipoEntidade.Fornecedores:
                    strSQL += "WHERE     (dbo.CTRL_Nos.XmPath LIKE '<Entidades><Fornecedores>')";
                    break;
                case NBdbm.tipos.TipoEntidade.Devedores:
                    strSQL += "WHERE     (dbo.CTRL_Nos.XmPath LIKE '<Entidades><Devedores>')";
                    break;
                case NBdbm.tipos.TipoEntidade.Clientes:
                    strSQL += "WHERE     (dbo.CTRL_Nos.XmPath LIKE '<Entidades><Clientes>')";
                    break;
            }
            return strSQL;

        }

        /// <summary>
        /// Criptografa a string passada por paramentro
        /// </summary>
        /// <param name="TextoDescriptado">String Descriptografada</param>
        /// <returns>Retorna String Criptografada</returns>
        public static string Encriptar(string TextoDescriptado)
        {
            return Criptografia.Encrypt(TextoDescriptado, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        }
        /// <summary>
        /// Descriptografa a string passada por paramentro
        /// </summary>
        /// <param name="TextoCriptografado">
        /// String Criptografada
        /// </param>
        /// <returns>
        /// Retorna String Descriptografada.
        /// </returns>
        public static string Decriptar(string TextoCriptografado)
        {
            return Criptografia.Decrypt(TextoCriptografado, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
        }
        /// <summary>
        /// Retorna o Valor da uma Parcela conforme os seus parametros, (Valor Total, 
        /// Taxa de Juros, Total de Parcelas)
        /// </summary>
        /// <param name="valortotal">Valor Total Financiado</param>
        /// <param name="taxajuros">Taxa de Juros que será usada para o financiamento</param>
        /// <param name="totalparcelas">Número Total de parcelas (Meses)</param>
        /// <returns>Retorna o Valor de Cada Parcela</returns>
        public static double GerarParcela(double valortotal, double taxajuros, double totalparcelas)
        {
            double juros = taxajuros / 100;
            double valorParcela = (valortotal * juros) / (1 - Math.Pow(1 / (1 + juros), totalparcelas));
            return valorParcela = Math.Round(valorParcela * 100) / 100;
        }
        ///////////////////////////////////////////////////////////////////////////////
        // SAMPLE: Symmetric key encryption and decryption using Rijndael algorithm.
        // 
        // To run this sample, create a new Visual C# project using the Console
        // Application template and replace the contents of the Class1.cs file with
        // the code below.
        //
        // THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
        // EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
        // WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
        // 
        // Copyright (C) 2002 Obviex(TM). All rights reserved.
        // 

        /// <summary>
        /// This class uses a symmetric key algorithm (Rijndael/AES) to encrypt and 
        /// decrypt data. As long as encryption and decryption routines use the same
        /// parameters to generate the keys, the keys are guaranteed to be the same.
        /// The class uses static functions with duplicate code to make it easier to
        /// demonstrate encryption and decryption logic. In a real-life application, 
        /// this may not be the most efficient way of handling encryption, so - as
        /// soon as you feel comfortable with it - you may want to redesign this class.
        /// </summary>
        protected class Criptografia
        {
            /// <summary>
            /// Encrypts specified plaintext using Rijndael symmetric key algorithm
            /// and returns a base64-encoded result.
            /// </summary>
            /// <param name="plainText">
            /// Plaintext value to be encrypted.
            /// </param>
            /// <param name="passPhrase">
            /// Passphrase from which a pseudo-random password will be derived. The
            /// derived password will be used to generate the encryption key.
            /// Passphrase can be any string. In this example we assume that this
            /// passphrase is an ASCII string.
            /// </param>
            /// <param name="saltValue">
            /// Salt value used along with passphrase to generate password. Salt can
            /// be any string. In this example we assume that salt is an ASCII string.
            /// </param>
            /// <param name="hashAlgorithm">
            /// Hash algorithm used to generate password. Allowed values are: "MD5" and
            /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
            /// </param>
            /// <param name="passwordIterations">
            /// Number of iterations used to generate password. One or two iterations
            /// should be enough.
            /// </param>
            /// <param name="initVector">
            /// Initialization vector (or IV). This value is required to encrypt the
            /// first block of plaintext data. For RijndaelManaged class IV must be 
            /// exactly 16 ASCII characters long.
            /// </param>
            /// <param name="keySize">
            /// Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
            /// Longer keys are more secure than shorter keys.
            /// </param>
            /// <returns>
            /// Encrypted value formatted as a base64-encoded string.
            /// </returns>
            public static string Encrypt(string plainText,
                string passPhrase,
                string saltValue,
                string hashAlgorithm,
                int passwordIterations,
                string initVector,
                int keySize)
            {
                // Convert strings into byte arrays.
                // Let us assume that strings only contain ASCII codes.
                // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
                // encoding.
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                // Convert our plaintext into a byte array.
                // Let us assume that plaintext contains UTF8-encoded characters.
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                // First, we must create a password, from which the key will be derived.
                // This password will be generated from the specified passphrase and 
                // salt value. The password will be created using the specified hash 
                // algorithm. Password creation can be done in several iterations.
                PasswordDeriveBytes password = new PasswordDeriveBytes(
                    passPhrase,
                    saltValueBytes,
                    hashAlgorithm,
                    passwordIterations);

                // Use the password to generate pseudo-random bytes for the encryption
                // key. Specify the size of the key in bytes (instead of bits).
                byte[] keyBytes = password.GetBytes(keySize / 8);

                // Create uninitialized Rijndael encryption object.
                RijndaelManaged symmetricKey = new RijndaelManaged();

                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = CipherMode.CBC;

                // Generate encryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                    keyBytes,
                    initVectorBytes);

                // Define memory stream which will be used to hold encrypted data.
                MemoryStream memoryStream = new MemoryStream();

                // Define cryptographic stream (always use Write mode for encryption).
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                    encryptor,
                    CryptoStreamMode.Write);
                // Start encrypting.
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                // Finish encrypting.
                cryptoStream.FlushFinalBlock();

                // Convert our encrypted data from a memory stream into a byte array.
                byte[] cipherTextBytes = memoryStream.ToArray();

                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Convert encrypted data into a base64-encoded string.
                string cipherText = Convert.ToBase64String(cipherTextBytes);

                // Return encrypted string.
                return cipherText;
            }

            /// <summary>
            /// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
            /// </summary>
            /// <param name="cipherText">
            /// Base64-formatted ciphertext value.
            /// </param>
            /// <param name="passPhrase">
            /// Passphrase from which a pseudo-random password will be derived. The
            /// derived password will be used to generate the encryption key.
            /// Passphrase can be any string. In this example we assume that this
            /// passphrase is an ASCII string.
            /// </param>
            /// <param name="saltValue">
            /// Salt value used along with passphrase to generate password. Salt can
            /// be any string. In this example we assume that salt is an ASCII string.
            /// </param>
            /// <param name="hashAlgorithm">
            /// Hash algorithm used to generate password. Allowed values are: "MD5" and
            /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
            /// </param>
            /// <param name="passwordIterations">
            /// Number of iterations used to generate password. One or two iterations
            /// should be enough.
            /// </param>
            /// <param name="initVector">
            /// Initialization vector (or IV). This value is required to encrypt the
            /// first block of plaintext data. For RijndaelManaged class IV must be
            /// exactly 16 ASCII characters long.
            /// </param>
            /// <param name="keySize">
            /// Size of encryption key in bits. Allowed values are: 128, 192, and 256.
            /// Longer keys are more secure than shorter keys.
            /// </param>
            /// <returns>
            /// Decrypted string value.
            /// </returns>
            /// <remarks>
            /// Most of the logic in this function is similar to the Encrypt
            /// logic. In order for decryption to work, all parameters of this function
            /// - except cipherText value - must match the corresponding parameters of
            /// the Encrypt function which was called to generate the
            /// ciphertext.
            /// </remarks>
            public static string Decrypt(string cipherText,
                string passPhrase,
                string saltValue,
                string hashAlgorithm,
                int passwordIterations,
                string initVector,
                int keySize)
            {
                // Convert strings defining encryption key characteristics into byte
                // arrays. Let us assume that strings only contain ASCII codes.
                // If strings include Unicode characters, use Unicode, UTF7, or UTF8
                // encoding.
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                // Convert our ciphertext into a byte array.
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                // First, we must create a password, from which the key will be 
                // derived. This password will be generated from the specified 
                // passphrase and salt value. The password will be created using
                // the specified hash algorithm. Password creation can be done in
                // several iterations.
                PasswordDeriveBytes password = new PasswordDeriveBytes(
                    passPhrase,
                    saltValueBytes,
                    hashAlgorithm,
                    passwordIterations);

                // Use the password to generate pseudo-random bytes for the encryption
                // key. Specify the size of the key in bytes (instead of bits).
                byte[] keyBytes = password.GetBytes(keySize / 8);

                // Create uninitialized Rijndael encryption object.
                RijndaelManaged symmetricKey = new RijndaelManaged();

                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = CipherMode.CBC;

                // Generate decryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                    keyBytes,
                    initVectorBytes);

                // Define memory stream which will be used to hold encrypted data.
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

                // Define cryptographic stream (always use Read mode for encryption).
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                    decryptor,
                    CryptoStreamMode.Read);

                // Since at this point we don't know what the size of decrypted data
                // will be, allocate the buffer long enough to hold ciphertext;
                // plaintext is never longer than ciphertext.
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                // Start decrypting.
                int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                    0,
                    plainTextBytes.Length);

                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Convert decrypted data into a string. 
                // Let us assume that the original plaintext string was UTF8-encoded.
                string plainText = Encoding.UTF8.GetString(plainTextBytes,
                    0,
                    decryptedByteCount);

                // Return decrypted string.   
                return plainText;
            }
        }

        public static void PosicionaScroll(Page pPage)
        {
            if (!pPage.IsPostBack)
                pPage.ClientScript.RegisterHiddenField("__SCROLLPOS", "0");

            System.Text.StringBuilder setScrollPosition = new System.Text.StringBuilder();
            System.Text.StringBuilder saveScrollPosition = new System.Text.StringBuilder();

            saveScrollPosition.Append("<script language='JavaScript1.2'>\r\t");
            saveScrollPosition.Append("function saveScrollPosition() {\r\t\t");
            saveScrollPosition.Append("var theBody = document.documentElement;\r\t\t");
            saveScrollPosition.Append("document.forms[0].__SCROLLPOS.value = theBody.scrollTop;}\r\t");
            saveScrollPosition.Append("window.onscroll=saveScrollPosition;\r");
            saveScrollPosition.Append("</script>\r");

            pPage.ClientScript.RegisterStartupScript(pPage.GetType(), "saveScroll", saveScrollPosition.ToString());

            if (pPage.IsPostBack)
            {
                setScrollPosition.Append("<script language='javascript'>\r\t");
                setScrollPosition.Append("function setScrollPosition() {\r\t\t");
                setScrollPosition.Append("var theBody = document.documentElement;\r\t\t");
                setScrollPosition.Append("theBody.scrollTop = " + pPage.Request.Params["__SCROLLPOS"] + ";}\r\t");
                setScrollPosition.Append("window.onload=setScrollPosition;\r");
                setScrollPosition.Append("</script>");

                pPage.ClientScript.RegisterStartupScript(pPage.GetType(), "setScroll", setScrollPosition.ToString());
                pPage.ClientScript.RegisterHiddenField("__SCROLLPOS", "0");
            }

        }

        public static string FormatCampoToSQL(object pValor)
        {
            if (pValor == null) return "NULL";

            string mValorDefault = pValor.ToString();

            switch (Type.GetTypeCode(pValor.GetType()))
            {
                case TypeCode.Boolean:
                    mValorDefault = Convert.ToInt32(pValor).ToString();
                    break;
                case TypeCode.Char:
                case TypeCode.String:
                    mValorDefault = string.Format("'{0}'", pValor);
                    break;
                case TypeCode.DateTime:
                    DateTime mDataTime = Convert.ToDateTime(pValor);
                    mValorDefault = string.Format("Convert(DateTime, '{0}', 102)",
                        (mDataTime.Hour > 0 || mDataTime.Minute > 0 || mDataTime.Second > 0 || mDataTime.Millisecond > 0) ?
                                mDataTime.ToString("yyyy/MM/dd HH:mm:ss") : mDataTime.ToString("yyyy/MM/dd"));
                    break;
                case TypeCode.Decimal:
                case TypeCode.Double:
                    mValorDefault = mValorDefault.Replace(".", "").Replace(",", ".");
                    break;
            }
            return mValorDefault;
        }

        public static string FormataCNPJ(string pCNPJ)
        {
            return string.Format("{0}.{1}.{2}/{3}-{4}",
                pCNPJ.Substring(0, 2),
                pCNPJ.Substring(2, 3),
                pCNPJ.Substring(5, 3),
                pCNPJ.Substring(8, 4),
                pCNPJ.Substring(12, 2));
        }

        public static string FormataCPF(string pCNPJ)
        {
            return string.Format("{0}.{1}.{2}-{3}",
                pCNPJ.Substring(0, 3),
                pCNPJ.Substring(3, 3),
                pCNPJ.Substring(6, 3),
                pCNPJ.Substring(8, 2));
        }

        public static void ChamaModalBootStrap(string pIdModal, string pMensagem)
        {
            string mScriptShowModal = "$('#btnModalSim').hide();$('#btnModalNao').hide();$('#{0} .modal-body').html('{1}');$('#{0}').modal('show');";
            Anthem.Manager.AddScriptForClientSideEval(string.Format(mScriptShowModal, pIdModal, pMensagem));
        }
    }
}

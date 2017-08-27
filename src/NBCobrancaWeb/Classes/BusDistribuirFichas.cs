using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using NBCobranca.Entidades;
using NBCobranca.Tipos;
using Neobridge.NBDB;

namespace NBCobranca.Classes
{
    public class BusDistribuirFichas : BusBase
    {
        public BusDistribuirFichas(Sistema sistema, DBDirect dbDirect)
            : base(sistema, dbDirect)
        { }

        /// <summary>
        /// Realiza a pesquisa das fichas de clientes livres para distribui��o
        /// </summary>
        /// <param name="nomeCarteira">Nome da carteira que ser� pesquisado as fichas, caso a pesquisa seja para todas as carteiras, este paramentro deve receber valor nulo.</param>
        /// <param name="idTipoDaDivida">Id do tipo da d�vida que se deseja filtrar caso a pesquisa seja para todos os tipos de d�vida, este paramentro deve receber valor 0.</param>
        /// <param name="quantidadeDeDividas">filtro de quantidade de d�vidas</param>
        /// <param name="somenteFichasLivres">true para trazer as fichas que n�o foram distribuidas ainda ou false para todas</param>
        /// <returns></returns>
        public DataTable PesquisarFichasParaDistribuicaoAutomatica(string nomeCarteira, int idTipoDaDivida = 0, DateTime? filtroDataVencimento = null, int? filtroMes = null, int? filtroAno = null)
        {
            var sql = Select();
            var mSql = new StringBuilder(sql);

            mSql.AppendLine(ClausulaFromParaPesquisarFichasParaDistribuicao(nomeCarteira, idTipoDaDivida,
                FiltroQuantidadeDeDividas.Todas, true, filtroDataVencimento, filtroMes, filtroAno));
            //Busca todas as fichas que n�o tiver�o acionamentos ainda
            mSql.AppendLine("union");
            mSql.AppendLine(sql);
            mSql.AppendLine(ClausulaFromParaPesquisarFichasParaDistribuicao(nomeCarteira, idTipoDaDivida,
                FiltroQuantidadeDeDividas.Todas, true, filtroDataVencimento, filtroMes, filtroAno, true));
            sql = mSql.ToString();
            var dt = DbDirect.CriarDataTable(sql);
            return dt;
        }

        /// <summary>
        /// Realiza a pesquisa das fichas de clientes livres para distribui��o
        /// </summary>
        /// <param name="nomeCarteira">Nome da carteira que ser� pesquisado as fichas, caso a pesquisa seja para todas as carteiras, este paramentro deve receber valor nulo.</param>
        /// <param name="idTipoDaDivida">Id do tipo da d�vida que se deseja filtrar caso a pesquisa seja para todos os tipos de d�vida, este paramentro deve receber valor 0.</param>
        /// <param name="quantidadeDeDividas">filtro de quantidade de d�vidas</param>
        /// <param name="somenteFichasLivres">true para trazer as fichas que n�o foram distribuidas ainda ou false para todas</param>
        /// <param name="filtroDataVencimento"></param>
        /// <param name="filtroMes"></param>
        /// <param name="filtroAno"></param>
        /// <returns></returns>
        public DataTable PesquisarFichasParaDistribuicaoManual(string nomeCarteira, int idTipoDaDivida, FiltroQuantidadeDeDividas quantidadeDeDividas, bool somenteFichasLivres, DateTime? filtroDataVencimento = null, int? filtroMes = null, int? filtroAno = null)
        {
            var sql = Select(true, !somenteFichasLivres);
            var mSql = new StringBuilder(sql);
            mSql.AppendLine(ClausulaFromParaPesquisarFichasParaDistribuicao(nomeCarteira, idTipoDaDivida,
                quantidadeDeDividas, somenteFichasLivres, filtroDataVencimento, filtroMes, filtroAno));
            mSql.AppendLine("union");
            mSql.AppendLine(Select(true, !somenteFichasLivres, true));
            mSql.AppendLine(ClausulaFromParaPesquisarFichasParaDistribuicao(nomeCarteira, idTipoDaDivida,
                quantidadeDeDividas, somenteFichasLivres, filtroDataVencimento, filtroMes, filtroAno, true));
            sql = mSql.ToString();
            var dt = DbDirect.CriarDataTable(sql);
            return dt;
        }

        /// <summary>
        /// Realiza a distribui��o de uma �nica ficha para um determinado cobrador ou para o cobrador que esta a mais tempo sem 
        /// receber fichas para acionar.
        /// </summary>
        /// <param name="idEntidadeFicha">ID da Entidade da Ficha que ser� distribuida.</param>
        /// <param name="idEntidadeCobrador">Id do Cobrador que receber� a ficha, podendo ficar nulo, neste caso que ir� receber a ficha ser� o cobrador que esta a mais tempo sem receber fichas.</param>
        /// <param name="usuarioLogado">Usu�rio que esta executando a distribui��o.</param>
        public void DistribuirFicha(int idEntidadeFicha, int? idEntidadeCobrador, string usuarioLogado, string grauRelacionamento = "CarteiraDeAcionamento")
        {
            try
            {
                var mCobrador = Sistema.busFuncionarios.ListaIdEntidadeCobradores(true, idEntidadeCobrador)[0];

                var mSqlDistribuicao = new StringBuilder();

                mSqlDistribuicao.AppendFormat("delete CTRL_Link_EntidadeEntidade where idEntidadeLink = {0};\r\n", idEntidadeFicha);
                mSqlDistribuicao.AppendLine("insert into CTRL_Link_EntidadeEntidade (idEntidadeBase,idEntidadeLink, GrauRelacionamento)");
                mSqlDistribuicao.AppendFormat("values({0},{1},'{2}');", idEntidadeCobrador, idEntidadeFicha, grauRelacionamento);
                DbDirect.Execute_NonQuery(mSqlDistribuicao.ToString());
                RegistraDataUltimaFichaRecebida(mCobrador.RGIE);
                GravarLog(TipoAcoesDistribuicaoFichas.DistribuicaoManual, usuarioLogado, idEntidadeFicha, string.Format("Distribui��o manualmente para o acionador {0}", mCobrador.NomePrimary));

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar realizar a distribui��o da fichas de c�digo: " + idEntidadeFicha.ToString() + ", segue mensagem de erro: " + ex.Message);
            }
        }

        /// <summary>
        /// Realiza a distribui��o de uma determinada carteira
        /// </summary>
        /// <param name="pNomeCarteira">Carteira que ser� distribuida autom�ticamente, caso a distribui��o seja de todas as carteiras, deixar este paramentro mo nulo.</param>
        /// <param name="usuarioLogado"></param>
        /// <param name="idTipoDaDivida"></param>
        /// <param name="filtroDataVencimento"></param>
        /// <param name="filtroMes"></param>
        /// <param name="filtroAno"></param>
        /// <param name="acionadores"></param>
        /// <returns>Retorna resumo da distribui��o</returns>
        public string DistribuirFichas(string pNomeCarteira, string usuarioLogado, int idTipoDaDivida = 0, DateTime? filtroDataVencimento = null, int? filtroMes = null, int? filtroAno = null, List<int> acionadores = null)
        {
            var msgErro = "Ocorreu um erro ao tentar realizar a distribui��o autom�tica das fichas";
            if (!string.IsNullOrEmpty(pNomeCarteira))
                msgErro += " da carteira " + pNomeCarteira;

            try
            {
                if (acionadores == null || acionadores.Count == 0)
                    throw new Exception("� preciso selecionar ao menos um acionador para a distribui��o");

                var sql = new StringBuilder();
                //Total de Fichas ignorando as quem tem promessa
                sql.AppendLine("select 0, COUNT(distinct ent.identidade) " +
                               ClausulaFromParaPesquisarFichasParaDistribuicao(pNomeCarteira, idTipoDaDivida,
                                   FiltroQuantidadeDeDividas.Todas, true, filtroDataVencimento, filtroMes, filtroAno, false, false, true));
                //Total de Fichas com Promessa
                sql.AppendLine("union");
                sql.AppendLine("select 1, COUNT(distinct ent.identidade) " +
                               ClausulaFromParaPesquisarFichasParaDistribuicao(pNomeCarteira, idTipoDaDivida,
                                   FiltroQuantidadeDeDividas.Todas, true, filtroDataVencimento, filtroMes, filtroAno, false, true));
                //Total de Fichas sem Acionamento
                sql.AppendLine("union");
                sql.AppendLine("select 2, COUNT(distinct ent.identidade) " +
                               ClausulaFromParaPesquisarFichasParaDistribuicao(pNomeCarteira, idTipoDaDivida,
                                   FiltroQuantidadeDeDividas.Todas, true, filtroDataVencimento, filtroMes, filtroAno, true));

                var rowsTotaisFichas = DbDirect.CriarDataTable(sql.ToString()).Rows;

                var mTotalFichasParaDistribuir = rowsTotaisFichas.Cast<DataRow>().Sum(dr => Convert.ToInt32(dr[1]));

                var mTotalFichasComPromessa = Convert.ToInt32(rowsTotaisFichas[1][1]);

                if (mTotalFichasParaDistribuir == 0)
                    throw new ExceptionNaoExisteFichasParaDistribuir();

                var mLogDistribuicao = new StringBuilder();
                mLogDistribuicao.Append("<p><b><h4>A distribui��o autom�tica de fichas");
                if (!string.IsNullOrEmpty(pNomeCarteira))
                    mLogDistribuicao.AppendFormat(" da Carteira {0},", pNomeCarteira);
                mLogDistribuicao.Append(" foi concluida com exito</h4></b></p>");
                mLogDistribuicao.Append("<br/>");
                mLogDistribuicao.Append("<p>Segue a divis�o das fichas entre os cobradores:<br/></p>");
                mLogDistribuicao.Append("<p>");
                var mIdEntidadeCobradores = Sistema.busFuncionarios.ListaSomenteCobradores();
                var mTotalFichasSemPromessaPorCobrador = Convert.ToInt32((mTotalFichasParaDistribuir - mTotalFichasComPromessa) / acionadores.Count);
                var mTotalFichaComPromessaPorCobrador = Convert.ToInt32(mTotalFichasComPromessa / acionadores.Count);
                var mResto = (mTotalFichasParaDistribuir - mTotalFichasComPromessa) % acionadores.Count;
                var mRestoPromessa = mTotalFichaComPromessaPorCobrador % acionadores.Count;

                DbDirect.Transaction_Begin();

                foreach (var mCobrador in mIdEntidadeCobradores)
                {
                    if (!acionadores.Contains(mCobrador.IdEntidade))
                        continue;

                    #region === Distribui somente fichas sem Promessa ===

                    //CAA - 13/09/2014 - RQ.02.04.01.03 - RN01, RN02
                    var mTotalFichasPorCobradorMaisResto = mTotalFichasSemPromessaPorCobrador;
                    if (mResto > 0)
                    {
                        mTotalFichasPorCobradorMaisResto++;
                        mResto--;
                    }
                    EfetuaDistribuicaoFichas(mCobrador.IdEntidade, mTotalFichasPorCobradorMaisResto, pNomeCarteira, "CarteiraDeAcionamento", idTipoDaDivida, filtroDataVencimento, filtroMes, filtroAno);
                    var totalFichasDistribuida = mTotalFichasPorCobradorMaisResto;

                    #endregion

                    #region === Distribui somente fichas com Promessa ===

                    //CAA - 13/09/2014 - RQ.02.04.01.03 - RN01, RN02
                    mTotalFichasPorCobradorMaisResto = mTotalFichaComPromessaPorCobrador;
                    if (mRestoPromessa > 0)
                    {
                        mTotalFichasPorCobradorMaisResto++;
                        mRestoPromessa--;
                    }
                    EfetuaDistribuicaoFichas(mCobrador.IdEntidade, mTotalFichasPorCobradorMaisResto, pNomeCarteira, "CarteiraDeAcionamento", idTipoDaDivida, filtroDataVencimento, filtroMes, filtroAno, true);
                    totalFichasDistribuida += mTotalFichasPorCobradorMaisResto;

                    #endregion

                    
                    RegistraDataUltimaFichaRecebida(mCobrador.RGIE);
                    mLogDistribuicao.AppendFormat("<b>{0}</b> recebeu {1} ficha(s).</br>", mCobrador.NomePrimary, totalFichasDistribuida);
                }
                mLogDistribuicao.AppendFormat("</p><p><h4>A distribui��o total foi de {0} fichas</h4></p>", mTotalFichasParaDistribuir);
                GravarLog(TipoAcoesDistribuicaoFichas.DistribuicaoAutomatica, usuarioLogado, 0, "Distribui��o autom�tica de fichas, segue Resumo da distribui��o: \r\n" + mLogDistribuicao);

                RegistrarHistoricoDeDistribuicao();                

                DbDirect.Transaction_Commit();
                return mLogDistribuicao.ToString();
            }
            catch (ExceptionNaoExisteFichasParaDistribuir)
            {
                DbDirect.Transaction_Cancel();
                throw;
            }
            catch (Exception e)
            {
                DbDirect.Transaction_Cancel();
                throw new Exception(msgErro + ". O erro ocorrido foi: " + e.Message);
            }
        }

        /// <summary>
        /// Zerar a distribui��o de fichas autom�ticas
        /// </summary>
        /// <param name="pNomeCarteira">Nome da carteira que ser� zerada a distribui��o</param>
        /// <param name="pMotivo">Motivo pelo qual esta sendo zerada a distribui��o</param>
        /// <param name="pUsuarioLogado">Usu�rio que esta realizando a a��o</param>
        /// <param name="grauRelacionamento">Define para qual tipo de distribui��o dever� ser zerada.
        /// <para>Ex: CarteiraDeAcionamento, Transfer�nciaDeFerias, etc...</para>
        /// </param>
        public void ZerarDistribuicao(string pNomeCarteira, string pMotivo, string pUsuarioLogado, string grauRelacionamento = "CarteiraDeAcionamento")
        {
            var mSql = new StringBuilder();
            try
            {
                if (string.IsNullOrEmpty(pNomeCarteira))
                {
                    mSql.AppendFormat("delete from CTRL_Link_EntidadeEntidade where GrauRelacionamento = '{0}'\r\n", grauRelacionamento);
                }
                else
                {
                    mSql.AppendLine("delete CTRL_Link_EntidadeEntidade where identidadeEntidade in ( ");
                    mSql.AppendLine("select lnkee.idEntidadeEntidade");
                    mSql.AppendLine("from CTRL_Link_EntidadeEntidade lnkEE");
                    mSql.AppendLine("join CTRL_Link_Entidade_No lnkEN  on lnkEE.idEntidadeLink = lnkEN.idEntidade");
                    mSql.AppendFormat("join CTRL_Nos Nos on Nos.IdNo = lnkEN.IdNo and Nos.XmPath = '<Entidades><Carteiras><{0}>')\r\n", pNomeCarteira);
                    mSql.AppendFormat("and GrauRelacionamento = '{0}'", grauRelacionamento);
                }
                DbDirect.Execute_NonQuery(mSql.ToString());

                GravarLog(TipoAcoesDistribuicaoFichas.ZerarDistribuicaoAutomatica, pUsuarioLogado, 0, pMotivo);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu uma falha ao tentar zerar a distribui��o das fichas, o erro foi: " + ex.Message);
            }
        }

        public void LiberarFichaParaDistribuicao(int idFicha, string motivo, string usuarioLogado, string grauRelacionamento = "CarteiraDeAcionamento")
        {
            try
            {
                var mSql = new StringBuilder();
                mSql.AppendFormat("delete from CTRL_Link_EntidadeEntidade where GrauRelacionamento = '{0}'\r\n",
                    grauRelacionamento);
                mSql.AppendFormat("and idEntidadeLink = {0}", idFicha);

                DbDirect.Transaction_Begin();
                DbDirect.Execute_NonQuery(mSql.ToString());
                GravarLog(TipoAcoesDistribuicaoFichas.ZerarDistribuicaoManual, usuarioLogado, idFicha, motivo);
                DbDirect.Transaction_Commit();
            }
            catch (Exception ex)
            {
                DbDirect.Transaction_Cancel();
                throw new Exception(string.Format("Ocorreu uma falha ao tentar liberar a ficha de c�digo {0}, o erro foi: {1}", idFicha, ex.Message));
            }


        }

        /// <summary>
        /// Realiza o rodizio de fichas entre os acionadores.
        /// <para>Este processo pode ser chamada autom�ticamente apartir do login de um administrador ou cordenador ou pela a��o manual dos mesmos.</para>
        /// </summary>
        /// <param name="usuarioLogado">Nome do Usu�rio que realizou o processo</param>
        /// <param name="automatico"></param>
        /// <param name="motivoRodizioManual"></param>
        public void ProcessarRodizio(string usuarioLogado, bool automatico = true, string motivoRodizioManual = "")
        {
            //Carrega Acionadores do sistema
            var acionadores = Sistema.busFuncionarios.ListaIdEntidadeCobradores();
            var indiceProximoAcionador = 0;
            var indiceFichas = 1;
            const string sqlFichas = "select idEntidadeLink idFicha, idEntidadeBase acionador from CTRL_Link_EntidadeEntidade where GrauRelacionamento = 'CarteiraDeAcionamento' order by idEntidadeBase";
            var sqlToExecute = new StringBuilder();
            try
            {
                DbDirect.Transaction_Begin();

                //Inicia o loop entre as fichas para acionamento.
                foreach (DataRow row in DbDirect.CriarDataTable(sqlFichas).Rows)
                {
                    var idAcionadorDaFicha = Convert.ToInt32(row["acionador"]);
                    var idFicha = Convert.ToInt32(row["idFicha"]);
                    var novoAcionador = acionadores[indiceProximoAcionador];

                    if (idAcionadorDaFicha == novoAcionador.IdEntidade)
                    {
                        DefineProximoAcionador(ref indiceProximoAcionador, acionadores.Count);
                        novoAcionador = acionadores[indiceProximoAcionador];
                    }

                    //DistribuirFicha(idFicha, novoAcionador.IdEntidade, usuarioLogado, "CarteiraEmRodizio");
                    DefineProximoAcionador(ref indiceProximoAcionador, acionadores.Count);
                    indiceFichas++;
                    sqlToExecute.AppendFormat("delete CTRL_Link_EntidadeEntidade where idEntidadeLink = {0};\r\n", idFicha);
                    //sqlToExecute.AppendFormat("update CTRL_Link_EntidadeEntidade set idEntidadeBase = {0}, GrauRelacionamento = 'CarteiraEmRodizio' where idEntidadeEntidade = {1};", novoAcionador.IdEntidade, idFicha);
                    sqlToExecute.Append("insert into CTRL_Link_EntidadeEntidade (idEntidadeBase,idEntidadeLink, GrauRelacionamento) ");
                    sqlToExecute.AppendFormat("values({0},{1},'{2}');\r\n", novoAcionador.IdEntidade, idFicha, "CarteiraEmRodizio");
                    if (indiceFichas >= 500)
                    {
                        DbDirect.Execute_NonQuery(sqlToExecute.ToString());
                        indiceFichas = 1;
                        sqlToExecute.Clear();
                    }
                }

                if (indiceFichas > 1)
                    DbDirect.Execute_NonQuery(sqlToExecute.ToString());

                //Em caso de rodizio manual ser� criado um log para saber quem foi que fez o rod�zio.
                if (!automatico)
                    GravarLog(TipoAcoesDistribuicaoFichas.Rodizio, usuarioLogado, 0, motivoRodizioManual);

                //Libera as fichas para acionamento ap�s a distribui��o do rod�zio.   
                const string sqlLiberacaoFichas = "update CTRL_Link_EntidadeEntidade set GrauRelacionamento = 'CarteiraDeAcionamento' where GrauRelacionamento = 'CarteiraEmRodizio'";
                DbDirect.Execute_NonQuery(sqlLiberacaoFichas);

                RegistrarHistoricoDeDistribuicao();

                DbDirect.Transaction_Commit();

            }
            catch (Exception)
            {
                DbDirect.Transaction_Cancel();
                throw;
            }

        }

        private void DefineProximoAcionador(ref int indiceProximoAcionador, int totalAcionadores)
        {
            if (indiceProximoAcionador < (totalAcionadores - 1))
                indiceProximoAcionador++;
            else
                indiceProximoAcionador = 0;

        }

        public void ProcessarRodizioAutomaticamente(string usuarioLogado)
        {

        }

        public void RegistrarHistoricoDeDistribuicao()
        {
            var sql = new StringBuilder();
            sql.AppendLine($"delete COBR_HistoricoDistribuicao where DataDistribuicao = '{DateTime.Today.ToShortDateString()}';");
            sql.AppendLine("insert into COBR_HistoricoDistribuicao");
            sql.AppendLine($"select Convert(Date, '{DateTime.Today.ToShortDateString()}', 103), idEntidadeBase, idEntidadeLink from CTRL_Link_EntidadeEntidade");

            DbDirect.Execute_NonQuery(sql.ToString());
        }

        #region === M�todos privados ===

        /// <summary>
        /// Grava log das a��es efetuadas referente a distribui��o de fichas
        /// </summary>
        /// <param name="tipoAcoesDistribuicaoFichas">Tipo da A��o que ser� logada</param>
        /// <param name="usuarioLogado">Usu�rio logado que efetuou a a��o</param>
        /// <param name="pCodigoFicha">C�digo da Ficha que recebeu a a��o, caso seja para todas as fichas dever� ser informado zero.</param>
        /// <param name="pMotivo">Descri��o do motivo da a��o</param>
        private void GravarLog(TipoAcoesDistribuicaoFichas tipoAcoesDistribuicaoFichas, string usuarioLogado, int pCodigoFicha, string pMotivo)
        {
            var mSql = new StringBuilder();
            try
            {
                mSql.AppendLine("INSERT INTO COBR_LogAcoes");
                mSql.AppendLine("([Data],[TipoAcao],[Usuario],[IDFicha],[Motivo])");
                mSql.AppendLine("VALUES (@Data, @TipoAcao, @Usuario, @IDFicha, @Motivo)");
                var mCommand = DbDirect.GetSqlCommand(mSql.ToString());
                mCommand.Parameters.Add(DbDirect.GetParameterCommand("@Data", DateTime.Now));
                mCommand.Parameters.Add(DbDirect.GetParameterCommand("@TipoAcao", tipoAcoesDistribuicaoFichas.ToString()));
                mCommand.Parameters.Add(DbDirect.GetParameterCommand("@Usuario", usuarioLogado));
                mCommand.Parameters.Add(DbDirect.GetParameterCommand("@IDFicha", pCodigoFicha));
                mCommand.Parameters.Add(DbDirect.GetParameterCommand("@Motivo", pMotivo));
                DbDirect.Execute_NonQuery(mCommand);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu uma falha ao tentar gravar o Log de A��o sobre " + tipoAcoesDistribuicaoFichas.ToString() + ", o erro foi: " + ex.Message);
            }
        }

        /// <summary>
        /// Realiza a distribui��o efetiva das fichas registrando-as no banco de dados.
        /// </summary>
        /// <param name="pIdEntidadeCobrador">ID da entidade do cobrador</param>
        /// <param name="pTotalFichasADistribui">Total m�ximo de fichas que pode ser distribuido para o cobrador</param>
        /// <param name="pNomeCarteira">Nome da carteira que est�o as fichas que ser�o distribuidas</param>
        /// <param name="grauRelacionamento"></param>
        /// <param name="idTipoDaDivida"></param>
        /// <param name="filtroDataVencimento"></param>
        /// <param name="filtroMes"></param>
        /// <param name="filtroAno"></param>
        /// <returns></returns>
        private int EfetuaDistribuicaoFichas(int pIdEntidadeCobrador, int pTotalFichasADistribui, string pNomeCarteira, string grauRelacionamento = "CarteiraDeAcionamento", int idTipoDaDivida = 0, DateTime? filtroDataVencimento = null, int? filtroMes = null, int? filtroAno = null, bool somentePromessa = false, bool ignorarPromessa = true)
        {
            var mSqlDistribuicao = new StringBuilder();
            mSqlDistribuicao.AppendLine("insert into CTRL_Link_EntidadeEntidade (idEntidadeBase,idEntidadeLink, GrauRelacionamento)");
            var select = $"select distinct top {pTotalFichasADistribui} {pIdEntidadeCobrador}, ent.IdEntidade, '{grauRelacionamento}'";
            mSqlDistribuicao.AppendLine(select);
            mSqlDistribuicao.AppendLine(ClausulaFromParaPesquisarFichasParaDistribuicao(pNomeCarteira, idTipoDaDivida, FiltroQuantidadeDeDividas.Todas, true, filtroDataVencimento, filtroMes, filtroAno, false, somentePromessa, ignorarPromessa));
            if (!somentePromessa)
            {
                mSqlDistribuicao.AppendLine("union");
                mSqlDistribuicao.AppendLine(select);
                mSqlDistribuicao.AppendLine(ClausulaFromParaPesquisarFichasParaDistribuicao(pNomeCarteira,
                    idTipoDaDivida, FiltroQuantidadeDeDividas.Todas, true, filtroDataVencimento, filtroMes, filtroAno,
                    true));
            }

            var sqlFinal = mSqlDistribuicao.ToString();
            return DbDirect.Execute_NonQuery(sqlFinal);
        }

        /// <summary>
        /// string com a Clausula SQL From para realizar uma pesquisa de fichas que contenham d�vidas n�o baixadas para distribui��o entre acionadores.
        /// </summary>
        /// <param name="nomeCarteira">Nome da carteira que se deseja filtrar</param>
        /// <param name="idTipoDaDivida">Id do tipo da d�vida que se deseja filtrar</param>
        /// <param name="quantidadeDeDividas">filtro de quantidade de d�vidas</param>
        /// <param name="somenteFichasLivres">true para trazer as fichas que n�o foram distribuidas ainda ou false para todas</param>
        /// <param name="filtroAno">Ano de vencimento de uma d�vida</param>
        /// <param name="filtroDataVencimento">Filtro para data de vencimento de uma d�vida</param>
        /// <param name="filtroMes">Mes de vencimento de uma d�vida</param>
        /// <param name="fichasSemAcionamento">Busca somente fichas que n�o tiveram acionamentos</param>
        /// <param name="somentePromessas">Busca somente fichas que tem promessa</param>
        /// <param name="ignorarPromessas">Busca fichas ignorando as que tem promessa</param>
        /// <returns></returns>
        private static string ClausulaFromParaPesquisarFichasParaDistribuicao(string nomeCarteira, int idTipoDaDivida = 0
            , FiltroQuantidadeDeDividas quantidadeDeDividas = FiltroQuantidadeDeDividas.Todas, bool somenteFichasLivres = true
            , DateTime? filtroDataVencimento = null, int? filtroMes = null, int? filtroAno = null, bool fichasSemAcionamento = false, bool somentePromessas = false, bool ignorarPromessas = false)
        {

            var mSql = new StringBuilder("\r\nfrom CTRL_Entidades Ent\r\n");
            mSql.AppendLine("join CTRL_Link_Entidade_No lnkEN  on Ent.idEntidade = lnkEN.idEntidade");

            mSql.AppendLine(JoinFiltroDeDividas(idTipoDaDivida, quantidadeDeDividas, filtroDataVencimento, filtroMes, filtroAno));

            #region === Filtro de Carteira ===

            var pXmPathCarteira = string.IsNullOrEmpty(nomeCarteira) ? "like '<Entidades><Carteiras><%'" : string.Format("= '<Entidades><Carteiras><{0}>'", nomeCarteira);
            mSql.AppendFormat("join CTRL_Nos Nos on Nos.IdNo = lnkEN.IdNo and Nos.XmPath {0}\r\n", pXmPathCarteira);

            #endregion

            if (!fichasSemAcionamento)
                mSql.AppendLine(JoinDataUltimoAcionamento(somentePromessas, ignorarPromessas));

            if (!somenteFichasLivres)
                mSql.AppendLine(JoinCarteiraAcionador());

            if (somenteFichasLivres)
            {
                mSql.AppendLine("where ent.IdEntidade not in (Select lnkEE.idEntidadeLink from CTRL_Link_EntidadeEntidade lnkEE)");
                if (fichasSemAcionamento)
                    mSql.AppendLine("and Ent.IdEntidade not in(select idEntidade from COBR_Acionamentos group by idEntidade)");
            }
            else if (fichasSemAcionamento)
                mSql.AppendLine("where Ent.IdEntidade not in(select idEntidade from COBR_Acionamentos group by idEntidade)");

            return mSql.ToString();

        }

        /// <summary>
        /// Registra para o cobrador a data em que o mesmo recebeu uma ficha para acionar, seja por distribui��o autom�tica, manual ou transfer�ncias.
        /// </summary>
        /// <param name="idUsuarioCobrador">C�digo do Usu�rio do Cobrador (Acionador) que esta registrado na tabela CTRL_UsuarioConfig</param>
        private void RegistraDataUltimaFichaRecebida(string idUsuarioCobrador)
        {
            //CAA - 13/09/2014 - RQ.02.04.01.03 - RN03
            DbDirect.Execute_NonQuery("update CTRL_UsuarioConfig set DataUltimaFichaRecebida = getdate() where idUsuario = " + idUsuarioCobrador);
        }

        private static string JoinFiltroDeDividas(int idTipoDaDivida, FiltroQuantidadeDeDividas quantidadeDeDividas, DateTime? filtroDataVencimento = null, int? filtroMes = null, int? filtroAno = null)
        {
            //, DateTime? filtroDataVencimento = null, int? filtroMes = null, int? filtroAno = null
            var join = new StringBuilder();
            //CAA - 13/09/2014 - RQ.02.04.01.03 - RN04
            join.Append("join (select IdEntidade from COBR_Divida where Baixada = 0");

            //CAA - 21/08/2014 - RQ.02.04.02.01
            if (idTipoDaDivida > 0)
                join.AppendFormat(" and IdTipoDivida = {0}", idTipoDaDivida);

            if (filtroDataVencimento != null)
                join.AppendFormat(" and DataVencimento = {0}", NBFuncoes.FormatCampoToSQL(filtroDataVencimento));

            if (filtroDataVencimento == null && (filtroMes != null && filtroAno != null))
                join.AppendFormat(" and Month(DataVencimento) = {0} and Year(DataVencimento) = {1}", filtroMes, filtroAno);

            //CAA - 21/08/2014 - RQ.02.04.02.01
            if (quantidadeDeDividas != FiltroQuantidadeDeDividas.Todas)
            {
                var filtroQuantidade = "= " + Convert.ToInt16(quantidadeDeDividas);
                if (quantidadeDeDividas == FiltroQuantidadeDeDividas.MaisQueTresDividas)
                    filtroQuantidade = " > 3";
                join.AppendFormat(" group by idEntidade having Count(idEntidade) {0}", filtroQuantidade);
            }
            join.AppendLine(") Divida on Divida.IdEntidade = Ent.IdEntidade");

            return join.ToString();
        }

        private static string JoinDataUltimoAcionamento(bool somentePromessas = false, bool igonorarPromessas = false)
        {
            var join = new StringBuilder();
            var filtroIgnorarPromessa = igonorarPromessas ? ",2" : "";
            join.AppendLine("join COBR_Acionamentos on COBR_Acionamentos.idEntidade = Ent.IdEntidade");
            @join.AppendLine(somentePromessas
                ? "   and COBR_Acionamentos.idTipoAcionamento = 2"
                : $"   and COBR_Acionamentos.idTipoAcionamento not in(9,11,13,17,18{filtroIgnorarPromessa})");
            join.Append("Join (SELECT MAX(ID) AS IdAcionamento, IdEntidade ");
            join.AppendLine("		FROM COBR_Acionamentos");
            join.AppendLine("		GROUP BY idEntidade");
            join.Append(") LastAC ON LastAC.IdAcionamento = COBR_Acionamentos.Id");

            return join.ToString();

        }

        private static string JoinCarteiraAcionador()
        {
            var join = new StringBuilder();

            join.AppendLine("left join (select NomePrimary, idEntidadeLink");
            join.AppendLine("			from CTRL_Entidades");
            join.AppendLine("			join CTRL_Link_EntidadeEntidade on idEntidadeBase = IdEntidade");
            join.Append(") AcionadoPor on AcionadoPor.idEntidadeLink = Ent.IdEntidade");

            return join.ToString();
        }

        private static string Select(bool mostrarDataUltimoAcionamento = false, bool mostrarCarteiraAcionador = false, bool joinUnion = false)
        {
            var select = new StringBuilder();
            select.Append("select distinct Ent.IdEntidade CodigoFicha, Ent.NomePrimary NomeDevedor");

            if (mostrarDataUltimoAcionamento && !joinUnion)
                select.Append(", COBR_Acionamentos.DataAcionamento DataUltimoAcionamento ");

            if (mostrarDataUltimoAcionamento && joinUnion)
                select.Append(", null DataUltimoAcionamento ");

            if (mostrarCarteiraAcionador)
                select.Append(", AcionadoPor.NomePrimary CarteiraDeAcionamento");

            return select.ToString();
        }

        #endregion

    }
}

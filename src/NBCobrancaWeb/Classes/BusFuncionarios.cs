using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using NBCobranca.Entidades;
using NBCobranca.Tipos;

namespace NBCobranca.Classes
{
    public class BusFuncionarios : BusBase
    {
        public BusFuncionarios(Sistema sistema, Neobridge.NBDB.DBDirect dbDirect)
            : base(sistema, dbDirect)
        { }


        /// <summary>
        /// Lista todos os cobradores ativos cuja a credencial seja igual a 1 ou 2.
        /// <para>A propriedade RGIE da Entidade estará recebendo o ID do Usuário da tabela CTRL_UsuarioConfig, para seja possível atualizar a data da ultima 
        /// distribuição de ficha.
        /// </para>
        /// </summary>
        /// <param name="ordenarPorNome"></param>
        /// <returns>
        /// </returns>
        public List<Entidades.entCTRL_Entidades> ListaSomenteCobradores(bool ordenarPorNome = true)
        {
            return ListaIdEntidadeCobradores(false, null, ordenarPorNome);
        }

        /// <summary>
        /// Lista todos os cobradores ativos cuja a credencial seja igual a 1 ou 2 ou somente o cobrador que tem a data de recebimento de ficha mais antiga.
        /// <para>A propriedade RGIE da Entidade estará recebendo o ID do Usuário da tabela CTRL_UsuarioConfig, para seja possível atualizar a data da ultima 
        /// distribuição de ficha.
        /// </para>
        /// </summary>
        /// <param name="pSomenteCobradorComDataMaisAntigaDeRecebimentoDeFicha">
        ///     Irá filtra pelo cobrador que esta a mais tempo sem receber ficha para acionamento.
        /// </param>
        /// <param name="pIdEntidadeCobrador">Id da Entidade do cobrador que receberá a ficha para acionar, passe nulo para ignorar o filtro.</param>
        /// <param name="ordenarPorNome"></param>
        /// <returns>
        /// </returns>
        public List<Entidades.entCTRL_Entidades> ListaIdEntidadeCobradores(bool pSomenteCobradorComDataMaisAntigaDeRecebimentoDeFicha = false, int? pIdEntidadeCobrador = null, bool ordenarPorNome = false)
        {
            try
            {
                StringBuilder mSQL = new StringBuilder();
                mSQL.Append("Select ");
                if (pSomenteCobradorComDataMaisAntigaDeRecebimentoDeFicha)
                    mSQL.Append("Top 1 ");
                mSQL.AppendLine("ent.IdEntidade, ent.NomePrimary, ltrim(str(usuConf.idUsuario)) RGIE");
                mSQL.AppendLine("from CTRL_Link_Entidade_No LkNo");
                mSQL.AppendLine("join CTRL_Entidades Ent on Ent.idEntidade = LkNo.idEntidade");
                mSQL.AppendLine("join CTRL_Usuario Usu on Usu.idEntidade = ent.IdEntidade");
                mSQL.AppendLine("join CTRL_UsuarioConfig UsuConf on UsuConf.idUsuario = usu.idUsuario and UsuConf.ativo = 1 and UsuConf.credencial in (1,2)");
                mSQL.AppendLine("where LkNo.IdNo = 6");
                if (pIdEntidadeCobrador != null && pIdEntidadeCobrador > 0)
                    mSQL.AppendFormat("and ent.idEntidade = {0}\r\n", pIdEntidadeCobrador);
                if (pSomenteCobradorComDataMaisAntigaDeRecebimentoDeFicha)
                    mSQL.AppendLine("order by UsuConf.DataUltimaFichaRecebida, usu.dtAdmissao");
                if (!pSomenteCobradorComDataMaisAntigaDeRecebimentoDeFicha && ordenarPorNome)
                    mSQL.AppendLine("order by ent.NomePrimary");
                List<Entidades.entCTRL_Entidades> mLista = new List<Entidades.entCTRL_Entidades>();
                DataTable mDT = this.DbDirect.CriarDataTable(mSQL.ToString());
                foreach (DataRow mRow in mDT.Rows)
                {
                    Entidades.entCTRL_Entidades mCobrador = new NBCobranca.Entidades.entCTRL_Entidades(mRow);
                    mLista.Add(mCobrador);
                }

                return mLista;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar calcular o total de cobradores existentes. O erro ocorrido foi: " + ex.Message);
            }

        }

        /// <summary>
        /// Lista todos os funcionários que tenham a credencial acionador com o total de fichas distribuidas para eles, 
        /// a data do ultimo acionamento e o total de fichas acionadas no dia em que a pesquisa foi executada.
        /// </summary>
        /// <param name="idEntidadeAcionador">Filtro pelo Acionador</param>
        /// <param name="idTipoDivida">Filtrar pelo tipo da dívida</param>
        /// <param name="idTipoUltimoAcionamento">Filtrar pelo tipo do acionamento realizado</param>
        /// <param name="vencimentoInicial">Filtrar pela data de vencimento, data inicial</param>
        /// <param name="vencimentoFinal">Filtrar pela data de vencimento, data final</param>
        /// <param name="acionamentoInicial"></param>
        /// <param name="acionamentoFinal"></param>
        /// <returns></returns>
        public List<DtoTotaisPorAcionador> ListarTotaisPorAcionador(int? idEntidadeAcionador = null, int? idTipoDivida = null, DateTime? vencimentoInicial = null, DateTime? vencimentoFinal = null, DateTime? acionamentoInicial = null, DateTime? acionamentoFinal = null, int? idTipoAcionamento = null)
        {

            var sql = new StringBuilder();

            if (acionamentoInicial == null || acionamentoFinal == null)
            {
                acionamentoInicial = DateTime.Today;
                acionamentoFinal = DateTime.Today;
            }
            var dataDoDiaDoAcionamento = DateTime.Today;

            if (acionamentoInicial == acionamentoFinal)
                dataDoDiaDoAcionamento = acionamentoFinal.Value;

            sql.AppendLine("select EF.IdEntidade, EF.NomePrimary Acionador");
            sql.AppendLine(", count(linkee.idEntidadeLink) TotalFichas");
            sql.AppendLine(", sum(isnull(uac.Promessa,0)) TotalPromessa");
            sql.AppendLine(", sum(isnull(livreParaAcionar,0)) TotalLivreParaAcionarHoje");
            sql.AppendLine(", isnull(TotalAcionamentos,0) TotalAcionamentos");
            sql.AppendLine("from (select distinct idEntidadeAcionador idEntidadeBase, idEntidadeDevedor idEntidadeLink");
            sql.AppendLine("        from COBR_HistoricoDistribuicao");
            sql.AppendLine($"        where DataDistribuicao between convert(date,'{acionamentoInicial.Value.ToShortDateString()}',103) and convert(date,'{acionamentoFinal.Value.ToShortDateString()}',103)");
            sql.AppendLine(") linkEE");
            sql.AppendLine("join CTRL_Entidades EF on EF.IdEntidade = linkEE.idEntidadeBase --Entidade Funcionário(Acionador)");
            sql.AppendLine("--Filtro por Tipo de Dívida");
            sql.AppendLine("JOIN (select IdEntidade from COBR_Divida ");
            sql.AppendLine("        where 1=1  ");
            if (idTipoDivida != null && idTipoDivida > 0)
                sql.AppendLine($"        and IdTipoDivida = {idTipoDivida}");
            if (vencimentoInicial != null && vencimentoFinal != null)
                sql.AppendLine($"        and DataVencimento between CONVERT(date,'{vencimentoInicial.Value.ToShortDateString()}',103) and CONVERT(date,'{vencimentoFinal.Value.ToShortDateString()}',103)");
            sql.AppendLine("        group by IdEntidade");
            sql.AppendLine(") Divida on Divida.IdEntidade = linkEE.idEntidadeLink");
            sql.AppendLine("--Busca dados dos Acionamentos");
            sql.AppendLine("left join (select COBR_Acionamentos.idEntidade");
            sql.AppendLine("            , Promessa = case idTipoAcionamento when 2 then 1 else 0 end");
            sql.AppendLine($"            , livreParaAcionar = case when (isNull(DataPromessa,'01/01/1900') <= CONVERT(Date,'{dataDoDiaDoAcionamento.ToShortDateString()}',103)) and (CONVERT(Date,(DataAcionamento + TAC.DiasReacionamento),103) <= CONVERT(Date,'{dataDoDiaDoAcionamento.ToShortDateString()}',103)) then 1 else 0 end");
            sql.AppendLine("            from COBR_Acionamentos");
            sql.AppendLine("            join COBR_TipoAcionamento TAC on TAC.Id = idTipoAcionamento");
            sql.AppendLine("            Join (SELECT MAX(ID) IdAcionamento");
            sql.AppendLine("                    FROM dbo.COBR_Acionamentos c");
            sql.AppendLine("                    join (select distinct idEntidadeAcionador idEntidadeBase, idEntidadeDevedor idEntidadeLink");
            sql.AppendLine("                          from COBR_HistoricoDistribuicao");
            sql.AppendLine($"                         where DataDistribuicao between convert(date,'{acionamentoInicial.Value.ToShortDateString()}',103) and convert(date,'{acionamentoFinal.Value.ToShortDateString()}',103)");
            sql.AppendLine("                    ) linkEE on linkee.idEntidadeLink = c.idEntidade");
            if (idTipoAcionamento != null && idTipoAcionamento.Value > 0)
                sql.AppendLine($"                and idTipoAcionamento = {idTipoAcionamento}");
            sql.AppendLine("                    group by c.idEntidade");
            sql.AppendLine("            ) LastAC ON LastAC.IdAcionamento = COBR_Acionamentos.Id");
            sql.AppendLine("        ) UAC on UAC.idEntidade = linkee.idEntidadeLink");
            sql.AppendLine("---Busca total de acionamentos realizado pelo acionador");
            sql.AppendLine("left join (select idAcionador, count(idEntidade) TotalAcionamentos");
            sql.AppendLine("			from (select distinct usu.idEntidade idAcionador, ac.idEntidade ");
            sql.AppendLine("					from COBR_Acionamentos ac");
            sql.AppendLine("					join  CTRL_Usuario usu on usu.idUsuario = ac.idUsuario");
            sql.AppendLine("                    join (select distinct idEntidadeAcionador idEntidadeBase, idEntidadeDevedor idEntidadeLink");
            sql.AppendLine("                          from COBR_HistoricoDistribuicao");
            sql.AppendLine($"                         where DataDistribuicao between convert(date,'{acionamentoInicial.Value.ToShortDateString()}',103) and convert(date,'{acionamentoFinal.Value.ToShortDateString()}',103)");
            sql.AppendLine("                    ) linkEE on linkee.idEntidadeLink = ac.idEntidade and linkee.idEntidadeBase = usu.idEntidade");
            sql.AppendLine("                    --Filtro por Tipo de Dívida");
            sql.AppendLine("                    JOIN (select IdEntidade from COBR_Divida ");
            sql.AppendLine("                    where 1=1  ");
            if (idTipoDivida != null && idTipoDivida > 0)
                sql.AppendLine($"                    and IdTipoDivida = {idTipoDivida}");
            if (vencimentoInicial != null && vencimentoFinal != null)
                sql.AppendLine($"                    and DataVencimento between CONVERT(date,'{vencimentoInicial.Value.ToShortDateString()}',103) and CONVERT(date,'{vencimentoFinal.Value.ToShortDateString()}',103)");
            sql.AppendLine("                    group by IdEntidade");
            sql.AppendLine("                    ) Divida on Divida.IdEntidade = linkEE.idEntidadeLink");
            sql.AppendLine($"					where convert(date, ac.DataAcionamento,103) between CONVERT(date,'{acionamentoInicial.Value.ToShortDateString()}',103) and CONVERT(date,'{acionamentoFinal.Value.ToShortDateString()}',103) ");
            if (idTipoAcionamento != null && idTipoAcionamento.Value > 0)
                sql.AppendLine($"                and idTipoAcionamento = {idTipoAcionamento}");
            sql.AppendLine("			) acionamentos");
            sql.AppendLine("			group by idAcionador");
            sql.AppendLine(") TotAc on TotAc.idAcionador = linkee.idEntidadeBase");

            if (idEntidadeAcionador != null && idEntidadeAcionador > 0)
                sql.AppendLine($"where EF.IdEntidade = {idEntidadeAcionador}");
            sql.AppendLine("group by EF.IdEntidade, EF.NomePrimary, TotAc.TotalAcionamentos");
            sql.AppendLine("order by EF.NomePrimary");


            var result = new List<DtoTotaisPorAcionador>();

            using (var dr = DbDirect.ExecuteDataReader(sql.ToString()))
            {
                while (dr.Read())
                {
                    result.Add(new DtoTotaisPorAcionador
                    {
                        IdAcionador = dr.GetInt32(0),
                        NomeAcionador = dr.GetString(1),
                        TotalFichasRecebidas = dr.GetInt32(2),
                        TotalFichasComPromessa = dr.GetInt32(3),
                        TotalFichasLivresParaAcionarHoje = dr.GetInt32(4),
                        TotalFichasAcionadas = dr.GetInt32(5)
                    });
                }

            }
            return result;
        }

        public List<DtoFichasPorAcionador> ListarFichasPorAcionador(int? idEntidadeAcionador = null, int? idTipoDivida = null,
            int? idTipoUltimoAcionamento = null, DateTime? vencimentoInicial = null, DateTime? vencimentoFinal = null, DateTime? acionamentoInicial = null, DateTime? acionamentoFinal = null)
        {
            var sql = new StringBuilder();
            if (acionamentoInicial == null || acionamentoFinal == null)
            {
                acionamentoInicial = DateTime.Today;
                acionamentoFinal = DateTime.Today;
            }
            sql.AppendLine("select Nos.Nome Carteira, ED.NomePrimary Devedor, DataAcionamento DataUltimoAcionamento, TipoAcionamento, LiberadoParaAcionar, ED.IdEntidade");
            sql.AppendLine("from (select distinct idEntidadeAcionador idEntidadeBase, idEntidadeDevedor idEntidadeLink");
            sql.AppendLine("        from COBR_HistoricoDistribuicao");
            sql.AppendLine($"        where DataDistribuicao between convert(date,'{acionamentoInicial.Value.ToShortDateString()}',103) and convert(date,'{acionamentoFinal.Value.ToShortDateString()}',103)");
            sql.AppendLine(") linkEE");
            sql.AppendLine("join CTRL_Entidades ED on ED.IdEntidade = linkEE.idEntidadeLink --Entidade Devedor");
            sql.AppendLine("join CTRL_Link_Entidade_No linkEN  on ED.idEntidade = linkEN.idEntidade");
            sql.AppendLine("join CTRL_Nos Nos on Nos.IdNo = linkEN.IdNo and Nos.IdNo <> 5");
            sql.AppendLine("--Filtro por Tipo de Dívida");
            sql.AppendLine("JOIN (select IdEntidade from COBR_Divida ");
            sql.AppendLine("        where Baixada = 0  ");
            if (idTipoDivida != null && idTipoDivida > 0)
                sql.AppendLine($"        and IdTipoDivida = {idTipoDivida}");
            if (vencimentoInicial != null && vencimentoFinal != null)
                sql.AppendLine($"        and DataVencimento between CONVERT(date,'{vencimentoInicial.Value.ToShortDateString()}',103) and CONVERT(date,'{vencimentoFinal.Value.ToShortDateString()}',103)");
            sql.AppendLine("        group by IdEntidade");
            sql.AppendLine(") Divida on Divida.IdEntidade = ED.IdEntidade");
            sql.AppendLine("--Ultimo Acionamento");
            sql.AppendLine("join (select ac.idEntidade, DataAcionamento, TAC.Descricao TipoAcionamento");
            sql.AppendLine("            , LiberadoParaAcionar = case when DataPromessa is null then CONVERT(Date,(DataAcionamento + TAC.DiasReacionamento),103) else DataPromessa end");
            sql.AppendLine("            , usu.idEntidade idEntidadeAcionador");
            sql.AppendLine("           from COBR_Acionamentos ac");
            sql.AppendLine("           join CTRL_Usuario usu on usu.idUsuario = ac.idUsuario");
            sql.AppendLine("           join COBR_TipoAcionamento TAC on TAC.Id = idTipoAcionamento");
            sql.AppendLine("           Join (SELECT MAX(ID) IdAcionamento");
            sql.AppendLine("                FROM dbo.COBR_Acionamentos c");
            sql.AppendLine("                join (select distinct idEntidadeAcionador idEntidadeBase, idEntidadeDevedor idEntidadeLink");
            sql.AppendLine("                      from COBR_HistoricoDistribuicao");
            sql.AppendLine($"                     where DataDistribuicao between convert(date,'{acionamentoInicial.Value.ToShortDateString()}',103) and convert(date,'{acionamentoFinal.Value.ToShortDateString()}',103)");
            sql.AppendLine("                ) linkEE on linkee.idEntidadeLink = c.idEntidade");
            sql.AppendLine($"               where convert(date, DataAcionamento,103) between CONVERT(date,'{acionamentoInicial.Value.ToShortDateString()}',103) and CONVERT(date,'{acionamentoFinal.Value.ToShortDateString()}',103)");
            if (idTipoUltimoAcionamento != null && idTipoUltimoAcionamento.Value > 0)
                sql.AppendLine($"              and idTipoAcionamento = {idTipoUltimoAcionamento}");
            sql.AppendLine("                group by c.idEntidade");
            sql.AppendLine("           ) LastAC ON LastAC.IdAcionamento = ac.Id");
            sql.AppendLine("           where 1=1");
            sql.AppendLine(") UAC on UAC.idEntidade = linkee.idEntidadeLink and UAC.idEntidadeAcionador = linkee.idEntidadeBase");
            sql.AppendLine("where 1=1");
            if (idEntidadeAcionador != null && idEntidadeAcionador > 0)
                sql.AppendLine($"and linkEE.idEntidadeBase = {idEntidadeAcionador}");
            sql.AppendLine("Order by Nos.Nome");


            var result = new List<DtoFichasPorAcionador>();

            using (var dr = DbDirect.ExecuteDataReader(sql.ToString()))
            {
                while (dr.Read())
                {
                    var ficha = new DtoFichasPorAcionador
                    {
                        Carteira = dr.GetString(0),
                        NomeDevedor = dr.GetString(1),
                        DataUltimoAcionamento = dr.IsDBNull(2) ? new DateTime?() : dr.GetDateTime(2),
                        TipoDoAcionamento = dr.GetString(3),
                        LiberadaParaAcionarAPartirDe = dr.GetDateTime(4),
                        IdDevedor = dr.GetInt32(5)
                    };

                    result.Add(ficha);
                }

            }

            return result;
        }

        public DtoDetalhesDoAcionador BuscarDetalhesDoAcionador(int codigoAcionador, int? idTipoDivida = null, DateTime? vencimentoInicial = null, DateTime? vencimentoFinal = null, DateTime? acionamentoInicial = null, DateTime? acionamentoFinal = null, int? idTipoAcionamento = null)
        {
            var totaisDoAcionador = ListarTotaisPorAcionador(codigoAcionador, idTipoDivida, vencimentoInicial, vencimentoFinal, acionamentoInicial, acionamentoFinal, idTipoAcionamento);
            return new DtoDetalhesDoAcionador
            {
                NomeAcionador = totaisDoAcionador[0].NomeAcionador,
                FichasDistribuidas = totaisDoAcionador[0].TotalFichasRecebidas,
                FichasLiberadasParaAcionar = totaisDoAcionador[0].TotalFichasLivresParaAcionarHoje,
                ValorTotalACobrar = TotalACobrarPeloAcionador(codigoAcionador, idTipoDivida, vencimentoInicial, vencimentoFinal, acionamentoInicial, acionamentoFinal),
                TotaisPorTipoAcionamentos = BuscaTotalFichasPorTipoDeAcionamento(codigoAcionador, idTipoDivida, vencimentoInicial, vencimentoFinal, acionamentoInicial, acionamentoFinal, idTipoAcionamento),
                FichasDoAcionador = ListarFichasPorAcionador(codigoAcionador, idTipoDivida, idTipoAcionamento, vencimentoInicial, vencimentoFinal, acionamentoInicial, acionamentoFinal)
            };
        }

        private List<DtoDetalhesDoAcionador.DtoTotalPorTipoAcionamento> BuscaTotalFichasPorTipoDeAcionamento(
            int? codigoAcionador = null, int? idTipoDivida = null, DateTime? vencimentoInicial = null, DateTime? vencimentoFinal = null, DateTime? acionamentoInicial = null, DateTime? acionamentoFinal = null, int? idTipoAcionamento = null)
        {
            var sql = new StringBuilder();

            if (acionamentoInicial == null || acionamentoFinal == null)
            {
                acionamentoInicial = DateTime.Today;
                acionamentoFinal = DateTime.Today;
            }

            sql.AppendLine("select uac.TipoAcionamento, count(linkEE.idEntidadeLink) TotalFichas");
            sql.AppendLine("from (select distinct idEntidadeAcionador idEntidadeBase, idEntidadeDevedor idEntidadeLink");
            sql.AppendLine("        from COBR_HistoricoDistribuicao");
            sql.AppendLine($"        where DataDistribuicao between convert(date,'{acionamentoInicial.Value.ToShortDateString()}',103) and convert(date,'{acionamentoFinal.Value.ToShortDateString()}',103)");
            sql.AppendLine(") linkEE");
            sql.AppendLine("--Filtro por Tipo de Dívida");
            sql.AppendLine("JOIN (select IdEntidade from COBR_Divida ");
            sql.AppendLine("        where 1=1  ");
            if (idTipoDivida != null && idTipoDivida > 0)
                sql.AppendLine($"        and IdTipoDivida = {idTipoDivida}");
            if (vencimentoInicial != null && vencimentoFinal != null)
                sql.AppendLine($"        and DataVencimento between CONVERT(date,'{vencimentoInicial.Value.ToShortDateString()}',103) and CONVERT(date,'{vencimentoFinal.Value.ToShortDateString()}',103)");
            sql.AppendLine("        group by IdEntidade");
            sql.AppendLine(") Divida on Divida.IdEntidade = linkEE.idEntidadeLink");
            sql.AppendLine("join (select usu.IdEntidade IdAcionador, ac.idEntidade, TAC.Descricao TipoAcionamento");
            sql.AppendLine("        from COBR_Acionamentos ac");
            sql.AppendLine("        join CTRL_Usuario usu on usu.idUsuario = ac.idUsuario");
            sql.AppendLine("        join COBR_TipoAcionamento TAC on TAC.Id = idTipoAcionamento");
            sql.AppendLine($"        where convert(date, DataAcionamento,103) between CONVERT(date,'{acionamentoInicial.Value.ToShortDateString()}',103) and CONVERT(date,'{acionamentoFinal.Value.ToShortDateString()}',103)");
            if (idTipoAcionamento != null && idTipoAcionamento.Value > 0)
                sql.AppendLine($"        and idTipoAcionamento = {idTipoAcionamento}");
            sql.AppendLine("        group by usu.IdEntidade, ac.idEntidade, TAC.Descricao");
            sql.AppendLine(") UAC on UAC.idEntidade = linkee.idEntidadeLink and UAC.IdAcionador = linkee.IdEntidadeBase");
            sql.AppendLine("where 1=1");
            if (codigoAcionador != null && codigoAcionador > 0)
                sql.AppendLine($"and linkEE.idEntidadeBase = {codigoAcionador}");
            sql.AppendLine("group by uac.TipoAcionamento");

            var result = new List<DtoDetalhesDoAcionador.DtoTotalPorTipoAcionamento>();

            using (var dr = DbDirect.ExecuteDataReader(sql.ToString()))
            {
                while (dr.Read())
                {
                    result.Add(new DtoDetalhesDoAcionador.DtoTotalPorTipoAcionamento
                    {
                        TipoAcionamento = dr.GetString(0),
                        TotalDeFichas = dr.GetInt32(1)
                    });
                }

            }
            return result;

        }

        private decimal TotalACobrarPeloAcionador(int codigoAcionador, int? idTipoDivida = null, DateTime? vencimentoInicial = null, DateTime? vencimentoFinal = null, DateTime? acionamentoInicial = null, DateTime? acionamentoFinal = null)
        {
            var sql = new StringBuilder();

            if (acionamentoInicial == null || acionamentoFinal == null)
            {
                acionamentoInicial = DateTime.Today;
                acionamentoFinal = DateTime.Today;
            }

            sql.AppendLine("select Sum(isNull(ValorACobrar,0)) TotalACobrar");
            sql.AppendLine("from (select distinct idEntidadeAcionador idEntidadeBase, idEntidadeDevedor idEntidadeLink");
            sql.AppendLine("        from COBR_HistoricoDistribuicao");
            sql.AppendLine($"        where DataDistribuicao between convert(date,'{acionamentoInicial.Value.ToShortDateString()}',103) and convert(date,'{acionamentoFinal.Value.ToShortDateString()}',103)");
            sql.AppendLine(") linkEE");
            sql.AppendLine("join CTRL_Entidades ED on ED.IdEntidade = linkEE.idEntidadeLink --Entidade Devedor");
            sql.AppendLine("--Soma das dividas de cada ficha");
            sql.AppendLine("join (select idEntidade, sum((ValorNominal - isnull(ValorBaixado,0))) ValorACobrar");
            sql.AppendLine("		from COBR_Divida");
            sql.AppendLine("		--Busca as baixas Parciais");
            sql.AppendLine("		left join (select iddivida, sum(ValorBaixa) ValorBaixado");
            sql.AppendLine("				 from COBR_Baixas");
            sql.AppendLine("				 group by IdDivida");
            sql.AppendLine("		) bx on bx.IdDivida = Id");
            sql.AppendLine("		where Baixada = 0");
            if (idTipoDivida != null && idTipoDivida > 0)
                sql.AppendLine($"        and IdTipoDivida = {idTipoDivida}");
            if (vencimentoInicial != null && vencimentoFinal != null)
                sql.AppendLine($"        and DataVencimento between CONVERT(date,'{vencimentoInicial.Value.ToShortDateString()}',103) and CONVERT(date,'{vencimentoFinal.Value.ToShortDateString()}',103)");
            sql.AppendLine("		group by IdEntidade");
            sql.AppendLine(") DividasACobrar on DividasACobrar.IdEntidade = ED.IdEntidade ");
            sql.AppendLine($"where linkEE.idEntidadeBase = {codigoAcionador}");

            return Convert.ToDecimal(DbDirect.Execute_Scalar(sql.ToString()));
        }
    }
}

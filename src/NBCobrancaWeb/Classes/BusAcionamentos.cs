using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NBCobranca.Entidades;
using NBdbm;

namespace NBCobranca.Classes
{
    public class BusAcionamentos : BusBase
    {
        public BusAcionamentos(Sistema sistema, Neobridge.NBDB.DBDirect dbDirect)
            : base(sistema, dbDirect)
        { }

        /// <summary>
        /// Carrega a lista de acionamentos de um determinado devedor com os seguintes campos:
        /// <para>
        /// ID, Usuario, TipoAcionamento, DataAcionamento, DataPromessa e TextoRespeito
        /// </para>
        /// </summary>
        /// <param name="pCodigoDevedor"></param>
        /// <returns>retorna um DataTable com os Campos ID, Usuario, TipoAcionamento, DataAcionamento, DataPromessa e TextoRespeito</returns>
        public DataTable Load(int pCodigoDevedor)
        {
            try
            {
                StringBuilder mSQL = new StringBuilder();

                mSQL.AppendLine("select * from");
                mSQL.AppendLine("	(select top 100 TBAcionamento.Id, TBUsuario.idUsuario, TBUsuario.Login Usuario, TBTipo.Descricao TipoAcionamento, TBAcionamento.DataAcionamento,");
                mSQL.AppendLine("	TBAcionamento.DataPromessa, TBAcionamento.TextoRespeito");
                mSQL.AppendLine("	from Cobr_Acionamentos TBAcionamento");
                mSQL.AppendLine("	join CTRL_Usuario TBUsuario on TBUsuario.idUsuario = TBAcionamento.idUsuario");
                mSQL.AppendLine("	join COBR_TipoAcionamento TBTipo on TBTipo.Id = TBAcionamento.idTipoAcionamento");
                mSQL.AppendFormat("	where TBAcionamento.idEntidade = {0}\r\n", pCodigoDevedor);
                mSQL.AppendLine("	Order by TBAcionamento.Id desc) UltimosAcionamentos");
                mSQL.AppendLine("Order by UltimosAcionamentos.Id Asc");
                return this.DbDirect.CriarDataTable(mSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("N�o foi poss�vel carregar os acionamentos realizados para o devedor de c�digo " + pCodigoDevedor.ToString(), ex);
            }
        }

        /// <summary>
        /// Carrega uma lista de todos os usu�rios cadastrados no sistema.
        /// </summary>
        /// <returns></returns>
        public DataTable LoadUsuarios()
        {
            return LoadUsuarios(false);
        }

        /// <summary>
        /// Carrega a lista de usu�rios ativos ou todos conforme o parametro pSomenteAtivos
        /// </summary>
        /// <param name="pSomenteAtivos">indica se � para retornar somente os usu�rios ativos ou n�o</param>
        /// <returns></returns>
        public DataTable LoadUsuarios(bool pSomenteAtivos)
        {
            try
            {
                StringBuilder mSQL = new StringBuilder();
                mSQL.AppendLine("SELECT CTRL_Usuario.*, CTRL_UsuarioConfig.Credencial");
                mSQL.AppendLine("FROM CTRL_Usuario, CTRL_UsuarioConfig");
                mSQL.AppendLine("WHERE CTRL_Usuario.idUsuario = CTRL_UsuarioConfig.idUsuario");
                mSQL.AppendLine("and login <> 'ProSystem_'");
                if (pSomenteAtivos)
                {
                    mSQL.AppendLine("and CTRL_UsuarioConfig.ativo <> '0'");
                    mSQL.AppendLine("Order by login");
                }
                return this.DbDirect.CriarDataTable(mSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("N�o foi poss�vel carregar a lista de Usu�rios", ex);
            }
        }

        /// <summary>
        /// Busca o acionador de uma determinada ficha
        /// </summary>
        /// <param name="codigoDevedor"></param>
        /// <returns>Pode retornar nulo caso a ficha n�o esteja sendo acionada por nenhum acionador</returns>
        public entCTRL_Entidades GetAcionador(int codigoDevedor)
        {
            var sql = new StringBuilder();
            sql.AppendLine("select ent.*");
            sql.AppendLine("from CTRL_Link_EntidadeEntidade");
            sql.AppendLine("join CTRL_Entidades ent on ent.IdEntidade = idEntidadeBase");
            sql.AppendLine($"where idEntidadeLink = {codigoDevedor}");
            try
            {
                var dt = DbDirect.CriarDataTable(sql.ToString());
                return dt.Rows.Count == 0 ? null : new entCTRL_Entidades(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                throw new COBR_Exception("N�o foi poss�vel carregar o acionador da ficha.", this.GetType().FullName, ex);
            }


        }
    }
}

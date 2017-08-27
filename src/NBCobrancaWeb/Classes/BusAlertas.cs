using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NBCobranca.Entidades;

namespace NBCobranca.Classes
{
    public class BusAlertas : BusBase
    {
        public BusAlertas(Sistema sistema, Neobridge.NBDB.DBDirect dbDirect)
            : base(sistema, dbDirect)
        {
        }

        /// <summary>
        /// Adiciona uma nova mensagem de alerta no banco de dados
        /// </summary>
        /// <param name="pID_UsuarioCriador">Código do usuário que criou o alerta</param>
        /// <param name="pID_UsuarioMensagem">Código do usuário que receberá a mensagem de alerta.</param>
        /// <param name="pMensagem">Mensagem que deverá ser mostrada para o usuário</param>
        /// <param name="pDataAlerta">Data e hora em que a mensagem deverá ser mostrada.</param>
        public void AddAlerta(int pID_UsuarioCriador, int pID_UsuarioMensagem, string pMensagem, DateTime pDataAlerta)
        {
            entCTRL_Alertas mAlerta = new entCTRL_Alertas();
            mAlerta.ID_UsuarioCriador = pID_UsuarioCriador;
            mAlerta.ID_UsuarioMensagem = pID_UsuarioMensagem;
            mAlerta.Mensagem = pMensagem;
            mAlerta.DataHora = pDataAlerta;
            mAlerta.Criacao = DateTime.Now;
            mAlerta.Lido = false;

            try
            {
                this.DbDirect.Transaction_Begin();
                mAlerta.Salvar(DbDirect);
                this.DbDirect.Transaction_Commit();
            }
            catch (Exception ex)
            {
                this.DbDirect.Transaction_Cancel();
                throw new Exception("Não foi possível salvar os dados do(a) {0}, erro inesperado, entre em contato com o suporte técnico", ex);
            }
        }

        /// <summary>
        /// Marca uma mensagem de alerta como Lida.
        /// </summary>
        /// <param name="pIdAlerta">ID do alerta que sera marcado como lido.</param>
        public void MarcarAlertaComoLido(int pIdAlerta)
        {
            try
            {
                entCTRL_Alertas mAlerta = new entCTRL_Alertas();
                mAlerta.ID = pIdAlerta;
                mAlerta.Lido = true;
                DbDirect.Transaction_Begin();
                mAlerta.Salvar(DbDirect);
                DbDirect.Transaction_Commit();
            }
            catch (Exception ex)
            {
                this.DbDirect.Transaction_Cancel();
                throw new Exception(string.Format("Não foi possível alterar o status de lido do alerta de código {0}, erro inesperado, entre em contato com o suporte técnico", pIdAlerta), ex);
            }
        }

        /// <summary>
        /// Prorroga o aviso de uma mensagem de alerta em mais alguns minutos
        /// </summary>
        /// <param name="pIdAlerta">ID do alerta que sera marcado como lido.</param>
        /// <param name="pMinutosAumento">Quantos minutos será acrescido para um novo alerta</param>
        public void AumentaTempoAlerta(int pIdAlerta, int pMinutosAumento)
        {
            try
            {
                entCTRL_Alertas mAlerta = new entCTRL_Alertas();
                mAlerta.ID = pIdAlerta;
                mAlerta.DataHora = DateTime.Now.AddMinutes(pMinutosAumento);
                DbDirect.Transaction_Begin();
                mAlerta.Salvar(DbDirect);
                DbDirect.Transaction_Commit();
            }
            catch (Exception ex)
            {
                this.DbDirect.Transaction_Cancel();
                throw new Exception(string.Format("Não foi possível prorrogar o alerta de código {0}, erro inesperado, entre em contato com o suporte técnico", pIdAlerta), ex);
            }
        }


        /// <summary>
        /// Exclui um alerta informando o Id do mesmo.
        /// </summary>
        /// <param name="pIdAlerta">Código referente ao campo ID da tabela.</param>
        public void ExcluirAlerta(int pIdAlerta)
        {
            try
            {
                StringBuilder mSql = new StringBuilder("Delete from CTRL_Alertas ");
                mSql.AppendFormat("where id = {0}", pIdAlerta);
                this.DbDirect.Execute_NonQuery(mSql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível excluir o Alerta - " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Retorna uma lista de alertas que deverão ser mostrados a um usuário.
        /// </summary>
        /// <param name="pIdUsuario">Código do usuário que deverá receber as mensagens</param>
        /// <param name="pDataHoraAgendamento">Data e hora em que as mensagens deverá ser apresentadas.</param>
        /// <returns></returns>
        public List<entCTRL_Alertas> ListaAlertasParaUsuario(int pIdUsuario, DateTime pDataHoraAgendamento)
        {
            try
            {
                entCTRL_Alertas mAlertaFiltro = new entCTRL_Alertas();
                Dictionary<string, object> mChave = new Dictionary<string, object>();
                mChave.Add("ID_UsuarioMensagem", pIdUsuario);
                mChave.Add("DataHora <", pDataHoraAgendamento);
                mChave.Add("Lido", false);
                DataTable mDT = this.DbDirect.CriarDataTable(mAlertaFiltro.SqlSelect(mChave));
                if (mDT.Rows.Count > 0)
                {
                    List<entCTRL_Alertas> mLista = new List<entCTRL_Alertas>();
                    foreach (DataRow mDr in mDT.Rows)
                    {
                        entCTRL_Alertas mAlerta = new entCTRL_Alertas();
                        mAlerta.Preencher(mDr);
                        mLista.Add(mAlerta);
                    }
                    return mLista;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível buscar a lista de alerta para o usuário de código {0} com data de agendamento igual a: {1}", pIdUsuario, pDataHoraAgendamento), ex);
            }
        }

        /// <summary>
        /// Lista todos os Login de Usuários ativos no sistema, retornando os campos idUsuario e Login.
        /// </summary>
        public DataView LoadLoginsAtivos
        {
            get
            {
                StringBuilder mSQL = new StringBuilder();
                mSQL.AppendLine("select Usuario.idUsuario, Usuario.Login from CTRL_Usuario Usuario");
                mSQL.AppendLine("join CTRL_UsuarioConfig conf on conf.idUsuario = Usuario.idUsuario");
                mSQL.AppendLine("where ativo = 1");
                mSQL.AppendLine("AND CONF.idUsuario not in (1,36)");
                mSQL.AppendLine("Order by Usuario.Login asc");

                return NBFuncoes.DataView(mSQL.ToString(), Sistema.Connection);
            }
        }

    }
}

using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using Neobridge.NBDB;
using NBCobranca.Tipos;
namespace NBCobranca.Classes
{
    public class BusSMS : BusBase
    {
        DBDirect _DBSMS;
        string _SQL_Mensagens_SMS = "insert into Mensagens (idCliente, Telefone, Mensagem, DtImportacao, TempoEntregaHrs) values(1,'{0}','{1}',getdate(),-1)";
        public BusSMS(Sistema sistema, Neobridge.NBDB.DBDirect dbDirect)
            : base(sistema, dbDirect)
        {
            _DBSMS = new DBDirect("SMS");
        }
        /// <summary>
        /// Realiza o registro de uma mensagem de SMS no Banco de dados de Envio de SMS
        /// </summary>
        /// <param name="pSMS">dto com a mensagem e telefone de destino</param>
        public void Enviar(DtoSms pSMS)
        {
            _DBSMS.Execute_NonQuery(string.Format(_SQL_Mensagens_SMS, pSMS.Telefone, pSMS.Mensagem));
        }

        /// <summary>
        /// Realiza o registro em massa de mensagens de SMS no Banco de dados de Envio de SMS
        /// </summary>
        /// <param name="pListaSMS">lista de dto com a mensagem e telefone de destino</param>
        public void Enviar(IList<DtoSms> pListaSMS)
        {
            try
            {
                foreach (DtoSms mSMS in pListaSMS)
                {
                    this.Enviar(mSMS);
                }
            }
            catch (Exception e)
            {
                throw new ExceptionSMS("Não foi possível registrar as mensagens para envio de SMS", e);
            }
        }

        /// <summary>
        /// Cria uma nova lista com os telefones dos devedores que irão receber SMS
        /// </summary>
        /// <param name="pListaSMS">Lista de dtoSMS somente com o código dos devedores que serão enviados sms</param>
        public IList<DtoSms> CriaListaComTelefones(IList<DtoSms> pListaSMS)
        {
            string mListaID = "";
            IList<DtoSms> mNovaListaDtoSMS = new List<DtoSms>();

            if (pListaSMS.Count > 100)
            {
                int mResto = pListaSMS.Count % 100;

                int mIndiceAtual = 0;
                for (int i = 0; i < Convert.ToInt16(pListaSMS.Count / 100); i++)
                {
                    mListaID = "";
                    for (int j = mIndiceAtual; j < (100 * (i + 1)); j++)
                    {
                        mListaID += string.Format("{0}, ", pListaSMS[j].CodigoDevedor);
                        mIndiceAtual = j + 1;
                    }
                    this.PopulaListaComTelefonesFromListaDevedores(mListaID, mNovaListaDtoSMS);
                }

            }
            else
            {
                foreach (DtoSms mDtoSMS in pListaSMS)
                    mListaID += string.Format("{0}, ", mDtoSMS.CodigoDevedor);

                this.PopulaListaComTelefonesFromListaDevedores(mListaID, mNovaListaDtoSMS);
            }

            return mNovaListaDtoSMS;
        }

        /// <summary>
        /// Metodo auxiliar para popular uma lista de telefones a partir de uma lista de ID de devedores
        /// </summary>
        /// <param name="pListaID">string com uma lista de ID separados por virgula, sem retirar a ultima virgula.</param>
        /// <param name="pListaASerPopulada">lista que será populada com os telefones</param>
        private void PopulaListaComTelefonesFromListaDevedores(string pListaID, IList<DtoSms> pListaASerPopulada)
        {
            StringBuilder mSQL = new StringBuilder("select e.IdEntidade, e.NomePrimary, CONVERT(int,DDD,103) DDD, Fone from ctrl_Fones f ");
            mSQL.AppendLine("join CTRL_Entidades e on e.IdEntidade = f.IdEntidade ").
                AppendLine("where LEFT(Fone,1) >= 7 ").
                AppendLine("and CONVERT(int,DDD,103)>0 ").
                AppendLine("and e.IdEntidade in({0})");

            pListaID = pListaID.Substring(0, pListaID.Length - 2);
            DataTable mDT = this.DbDirect.CriarDataTable(string.Format(mSQL.ToString(), pListaID));

            foreach (DataRow mRow in mDT.Rows)
                pListaASerPopulada.Add(new DtoSms(Convert.ToInt32(mRow[0]), mRow[1].ToString(), null, mRow[2].ToString() + mRow[3].ToString()));

        }

        public override void Dispose()
        {
            _DBSMS.Dispose();
            _DBSMS = null;
            base.Dispose();
        }
    }
}

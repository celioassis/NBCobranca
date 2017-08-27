using System;
using System.Data;
using System.Collections.Generic;
using NBCobranca.Tipos;

namespace NBCobranca.Controllers
{
    public class ctrEnvioSMS : CtrBase
    {
        Classes.BusSMS aBusSMS;
        IList<DtoSms> aListaSMS;
        string aDevedorSelecionado = null;
        int aTotalDevedores = 0;

        public ctrEnvioSMS(Classes.Sistema sistema, CtrFactory ctrFactory)
            : base(sistema, ctrFactory)
        {
            aBusSMS = this.Sistema.busSMS;
            this.aListaSMS = new List<DtoSms>();
        }

        public void LimpaListaSMS()
        {
            this.aListaSMS.Clear();
        }

        /// <summary>
        /// Lista de SMS que deverão ser enviados.
        /// </summary>
        public IList<DtoSms> ListaSMS
        {
            get
            {

                NBCobranca.Classes.LimAcionamentos mAcionamentos = this.Sistema.LimAcionamentos;
                IList<DtoSms> mListaSMS = this.aBusSMS.CriaListaComTelefones(mAcionamentos.ListaDtoSMS);

                aTotalDevedores = 0;
                int mCodigoDevedorAnterior = 0;

                foreach (DtoSms mSMS in mListaSMS)
                {
                    if (mSMS.CodigoDevedor != mCodigoDevedorAnterior)
                    {
                        aTotalDevedores++;
                        mCodigoDevedorAnterior = mSMS.CodigoDevedor;
                        aDevedorSelecionado = mSMS.NomeDevedor;
                    }
                }

                if (aTotalDevedores > 1)
                    aDevedorSelecionado = null;

                return mListaSMS;
            }
        }

        /// <summary>
        /// Retorna o total de devedores que foram selecionados em uma pesquisa prévia, isso pode ter ocorrido através de 
        /// uma seleção em massa ou via ficha de acionamento.
        /// </summary>
        public int TotalDevedores
        {
            get
            {
                return aTotalDevedores;
            }
        }

        /// <summary>
        /// Get - Devedor selecionado, só será preenchido quando o envio de sms for chamado via ficha de acionamento.
        /// </summary>
        public string DevedorSelecionado
        {
            get
            {
                return aDevedorSelecionado;
            }
        }

        public void AddSMS(string pMensagem, string pNumeroCelular)
        {
            this.aListaSMS.Add(new DtoSms(pMensagem, pNumeroCelular));
        }

        public void Enviar()
        {
            if (this.aListaSMS.Count == 0)
                throw new Exception("É preciso ter devedores selecionados para o envio de SMS");

            try
            {
                this.aBusSMS.Enviar(aListaSMS);
            }
            finally
            {
                this.aListaSMS.Clear();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neobridge.NBDB;
using NBCobranca.Entidades;
using System.Net.Mail;
using Neobridge.NBUtil;

namespace NBCobranca.Classes
{
    public class BusConfiguracoes : BusBase
    {
        public BusConfiguracoes(Sistema sistema, DBDirect dbDirect) : base(sistema, dbDirect)
        { }

        public entConfiguracoes GetConfiguracoes
        {
            get
            {
                try
                {
                    entConfiguracoes pConfiguracoes = new entConfiguracoes();
                    System.Data.DataTable dt = DbDirect.CriarDataTable("Select top 1 * from Configuracoes");
                    if (dt.Rows.Count == 0)
                        return null;
                    pConfiguracoes.Preencher(dt.Rows[0]);
                    return pConfiguracoes;

                }
                catch (Exception ex)
                {
                    throw new Exception("Não foi possível Carregar as configurações Salvas.\r\nOcorreu o seguinte erro:" + ex.Message);
                }
            }
        }

        public void SalvarConfiguracoes(entConfiguracoes pConfiguracoes)
        {
            try
            {
                DbDirect.Transaction_Begin();
                pConfiguracoes.Salvar(DbDirect);
                DbDirect.Transaction_Commit();
            }
            catch (Exception ex)
            {
                var msgErro = "Não foi possível salvar as configurações.\r\nOcorreu o seguinte erro: ";

                DbDirect.Transaction_Cancel();
                if (ex is NBException)
                    throw new Exception(msgErro + ex.Message + "\r\n" + ex.InnerException.Message);
                else
                    throw new Exception(msgErro = ex.Message);
            }

        }

        public void ValidarConfiguracoes(entConfiguracoes pConfiguracoes, string pEmailDestino)
        {
            try
            {
                var email = Sistema.GetInstance<BusEmail>();

                string sBobdy = "Este é um email de validação das configurações do sistema de cobrança Web da Neobrige.";

                email.CarregarServidorSMTP(pConfiguracoes, true);

                email.Enviar(pConfiguracoes.UsuarioSMTP, pEmailDestino, "Validação de configuração", sBobdy);

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível validar as configurações.\r\nOcorreu o seguinte erro: " + ex.Message);
            }
        }
    }
}

using NBCobranca.Entidades;
using Neobridge.NBDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace NBCobranca.Classes
{
    public class BusEmail : BusBase
    {
        SmtpClient _SmtpServer;

        public BusEmail(Sistema sistema, DBDirect dbDirect) : base(sistema, dbDirect)
        { }

        public SmtpClient CarregarServidorSMTP(entConfiguracoes pConfiguracoes, bool forcarCarregamento = false)
        {
            try
            {
                if (pConfiguracoes == null)
                    pConfiguracoes = Sistema.GetInstance<BusConfiguracoes>().GetConfiguracoes;

                if (pConfiguracoes == null)
                    throw new Exception("As configurações ainda não foram Salvas, favor preencher as configurações e salvar, acessando o Menu Configurações na tela principal.");


                if (_SmtpServer == null || forcarCarregamento)
                {
                    _SmtpServer = new SmtpClient("smtp.mail.yahoo.com");
                    _SmtpServer.Port = pConfiguracoes.PortaSMTP;
                    //_SmtpServer.EnableSsl = true;
                    
                    _SmtpServer.Credentials = new NetworkCredential("celioassis@yahoo.com", "tgracaa@162408", "yahoo.com");
                }

                return _SmtpServer;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível criar Client do Servidor de SMTP.\r\nOcorreu o seguinte erro: " + ex.Message);
            }
        }

        public void Enviar(string pEmailOrigem, string pEmailDestino, string pAssunto, string pMensagem, Dictionary<string, MemoryStream> pAnexos = null, bool pMensagemNoFormatoHtml = true)
        {
            try
            {
                if (_SmtpServer == null)
                    throw new Exception("Antes de tentar enviar um email as configurações de SMTP devem ser carregadas, utilize o método CarregarServidorSMTP para prosseguir.");

                if (string.IsNullOrEmpty(pEmailOrigem))
                    throw new Exception("O email de origem deve ser informado.");

                if (string.IsNullOrEmpty(pEmailDestino))
                    throw new Exception("O email de destino deve ser informado.");

                if (string.IsNullOrEmpty(pAssunto))
                    throw new Exception("Não é permitido o envio de email sem assunto.");

                MailMessage email = new MailMessage(pEmailOrigem,pEmailDestino);
                email.Subject = pAssunto;
                email.Body = pMensagem;
                email.IsBodyHtml = pMensagemNoFormatoHtml;
                
                if (pAnexos != null)
                {
                    foreach (var key in pAnexos.Keys)
                    {
                        var anexo = pAnexos[key];
                        email.Attachments.Add(new Attachment(anexo, key));
                    }
                }
                    
                _SmtpServer.Send(email);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível realizar o envio do email.\r\n Ocorreu o seguinte erro: " + ex.Message);
            }
        }

        public string EmailsDoDevedor(int pIdDevedor)
        {
            string emails = "";

            var dtEmails = DbDirect.CriarDataTable($"select eMail from CTRL_Email where IdEntidade = {pIdDevedor}");

            if (dtEmails.Rows.Count == 0)
                return emails;

            foreach (DataRow drEmail in dtEmails.Rows)
            {
                emails += drEmail["eMail"].ToString() + ",";
            }

            return emails.Substring(0, emails.Length - 1);

        }
    }
}

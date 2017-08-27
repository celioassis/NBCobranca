using EASendMail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBCobranca.Classes;
using NBCobranca.Classes.Relatorios;
using NBCobranca.Tipos;
using Neobridge.NBDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace NBCobrancaWebTest
{
    [TestClass]
    public class BusMailTest
    {
        [TestMethod]
        public void Test_EnviarEmail_Com_Anexo()
        {
            var sistema = new Sistema();
            using (var busMail = new BusEmail(sistema, new DBDirect("ConnString")))
            {
                var relatorio = new RelBorderos(System.IO.Path.GetFullPath);

                var dadosRelatorio = GetBordero();
                busMail.CarregarServidorSMTP(new NBCobranca.Entidades.entConfiguracoes
                {
                    PortaSMTP = 465,
                    Senha = "tgracaa162408",
                    ServidorSMTP = "smtp.mail.yahoo.com",
                    UsuarioSMTP = "celioassis@yahoo.com.br"
                });
                var anexo = new Dictionary<string,MemoryStream>();
                anexo.Add("Bordero.pdf", relatorio.Print(dadosRelatorio));
                var emailDestino = "celioassis@gmail.com,celioaassis@outlook.com,";
                emailDestino = emailDestino.Substring(0, emailDestino.Length - 1);
                busMail.Enviar(
                    "celioassis@gmail.com",
                    emailDestino,
                    "Teste de Envio de email com Anexo",
                    "Segue bordero em anexo",
                    anexo);
            }
        }

        private DtoRelBordero GetBordero()
        {
            var dadosRelatorio = new DtoRelBordero
            {
                Carteira = "TESTE",
                NumeroDoBordero = 100,
                DataInicial = new DateTime(2017, 12, 1),
                DataFinal = new DateTime(2017, 12, 31)
            };
            dadosRelatorio.AddRegistro(new DtoRelBordero.DtoRegistroRelBordero
            {
                Carteira = "TESTE",
                CodigoDevedor = 1515,
                Contrato = "MENSALIDADE-2017",
                NomeDevedor = "JOAQUIM DAS NEVES",
                NumDoc = "100",
                TipoDivida = "MENSALIDADE",
                ValorNominal = 1000,
                ValorRecebido = 1500
            });
            return dadosRelatorio;
        }
    }

}

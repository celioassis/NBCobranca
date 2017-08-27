using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBCobranca.Classes;
using NBCobranca.Tipos;
using Neobridge.NBDB;

namespace NBCobrancaWebTest
{
    [TestClass]
    public class BusFuncionariosTest
    {
        [TestMethod]
        public void TestPesquisa_Fichas_por_acionador_geral()
        {
            using (var busFunc = new BusFuncionarios(new Sistema(), new DBDirect("ConnString")))
            {
                var totaisPorAcionador = busFunc.ListarTotaisPorAcionador();

                Assert.AreEqual(9, totaisPorAcionador.Count);
                Assert.AreEqual("BRUNA", totaisPorAcionador[0].NomeAcionador);

            }
        }

        [TestMethod]
        public void TestPesquisa_Fichas_por_acionador_somente_de_um_acionador()
        {
            using (var busFunc = new BusFuncionarios(new Sistema(), new DBDirect("ConnString")))
            {
                var totaisPorAcionador = busFunc.ListarTotaisPorAcionador(8883);

                Assert.AreEqual(1, totaisPorAcionador.Count);
                Assert.AreEqual("BRUNA", totaisPorAcionador[0].NomeAcionador);

            }
        }

        [TestMethod]
        public void TestPesquisa_Fichas_por_acionador_tipo_de_acionamento_promessa()
        {
            using (var busFunc = new BusFuncionarios(new Sistema(), new DBDirect("ConnString")))
            {
                var totaisPorAcionador = busFunc.ListarTotaisPorAcionador(null, null);

                Assert.AreEqual(9, totaisPorAcionador.Count);
                Assert.AreEqual("BRUNA", totaisPorAcionador[0].NomeAcionador);
                Assert.AreEqual("JULIANO NIXON DE SOUZA", totaisPorAcionador[5].NomeAcionador);


            }
        }

        [TestMethod]
        public void TestPesquisa_Fichas_por_acionador_tipo_de_divida_mensalidade()
        {
            using (var busFunc = new BusFuncionarios(new Sistema(), new DBDirect("ConnString")))
            {
                var totaisPorAcionador = busFunc.ListarTotaisPorAcionador(null, 3);
                var acionador = totaisPorAcionador[0];

                Assert.AreEqual(9, totaisPorAcionador.Count);
                Assert.AreEqual("BRUNA", acionador.NomeAcionador);

                acionador = totaisPorAcionador[5];
                Assert.AreEqual("JULIANO NIXON DE SOUZA", acionador.NomeAcionador);


            }
        }

        [TestMethod]
        public void TestPesquisa_Fichas_por_acionador_periodo_de_vencimento()
        {
            using (var busFunc = new BusFuncionarios(new Sistema(), new DBDirect("ConnString")))
            {
                var totaisPorAcionador = busFunc.ListarTotaisPorAcionador(null, null, null, new DateTime(2011, 1, 1), new DateTime(2011, 12, 31));
                var acionador = totaisPorAcionador[0];

                Assert.AreEqual(9, totaisPorAcionador.Count);
                Assert.AreEqual("BRUNA", acionador.NomeAcionador);

                acionador = totaisPorAcionador[5];
                Assert.AreEqual("JULIANO NIXON DE SOUZA", acionador.NomeAcionador);


            }
        }

        [TestMethod]
        public void TestPesquisa_Fichas_por_acionador_usando_quase_todos_os_filtros()
        {
            using (var busFunc = new BusFuncionarios(new Sistema(), new DBDirect("ConnString")))
            {
                var totaisPorAcionador = busFunc.ListarTotaisPorAcionador(null, 3, new DateTime(2011, 1, 1), new DateTime(2011, 12, 31));
                var acionador = totaisPorAcionador[0];

                Assert.AreEqual(9, totaisPorAcionador.Count);
                Assert.AreEqual("BRUNA", acionador.NomeAcionador);

                acionador = totaisPorAcionador[5];
                Assert.AreEqual("JULIANO NIXON DE SOUZA", acionador.NomeAcionador);


            }
        }

    }
}

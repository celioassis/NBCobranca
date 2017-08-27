using System;
using System.Web.UI;
using NBCobranca.Classes;
using NBCobranca.Classes.Relatorios;
using System.IO;
using NBCobranca.Tipos;
using System.Collections.Generic;

namespace NBCobranca.aspx.relatorios
{
    public partial class rel_CartasCustomizado : Page
    {
        private Sistema Sistema;
        private LimAcionamentos obj;
        int mTotalRegistros = 0;
        private List<dtoCarta> ListaDeCartas;

        protected void Page_Load(object sender, EventArgs e)
        {
            NBFuncoes.ValidarSistema(this, ref Sistema);
            obj = Sistema.LimAcionamentos;

            if (!IsPostBack)
            {
                int? mIDDevedor = null;

                var m2Aviso = Request.QueryString["2Aviso"].ToUpper().Equals("TRUE");
                var mEnviarPorEmail = false;
                var mIdCarta = Convert.ToInt32(Request.QueryString["IdCarta"]);
                bool.TryParse(Request.QueryString["EnviarPorEmail"], out mEnviarPorEmail);
                var mCodigoDevedor = Request.QueryString["idDevedor"];
                if (!string.IsNullOrEmpty(mCodigoDevedor))
                    mIDDevedor = Convert.ToInt32(mCodigoDevedor);

                ListaDeCartas = obj.ListaCartas(mIdCarta, m2Aviso, mIDDevedor);

                if (ListaDeCartas == null)
                {
                    lblMensagem.Text = "Nâo existe registro de cartas para serem listadas.";
                    return;
                }

                var stream = new RelCartasCustomizadas(Server).Print(ListaDeCartas);

                if (mEnviarPorEmail)
                    EnviarPorEmail(stream);
                else
                    EnviarComoPDF(stream);

                stream.Close();
            }
        }

        private void EnviarComoPDF(MemoryStream stream)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", stream.Length.ToString());
            Response.BinaryWrite(stream.ToArray());
            Response.Flush();

        }

        private void EnviarPorEmail(MemoryStream stream)
        {
            try
            {
                var configuracoes = Sistema.GetInstance<BusConfiguracoes>().GetConfiguracoes;
                var busEmail = Sistema.GetInstance<BusEmail>();
                var carta = ListaDeCartas[0];
                var titulo = carta.SegundoAviso ? carta.TituloSegundoAviso[0].Texto.Replace("\n", "") : carta.Conteudo[0].Texto.Replace("\n", "");
                var mensagem = $"<b>Prezado(a):</b> {carta.DadosDevedor.Nome} <br><br><br>";
                mensagem += $"Segue anexo em formato PDF a carta com <b>{titulo}.</b><br><br>";
                mensagem += $"Para visualizá-la, você precisará ter neste computador o programa Acrobat Reader instalado.<br>";
                mensagem += $"Este programa é disponibilizado gratuitamente através do seguinte link: http://get.adobe.com/reader <br>";
                mensagem += "<br><br><br>";
                carta.Rodape.ForEach(x => { mensagem += x.Texto.Replace("\n", "<br/>"); });

                var anexos = new Dictionary<string, MemoryStream>();
                anexos.Add("NotificacaoExtraJudicial.pdf", stream);

                busEmail.CarregarServidorSMTP(configuracoes);
                busEmail.Enviar(configuracoes.UsuarioSMTP, carta.DadosDevedor.Email, titulo, mensagem, anexos);

                lblMensagem.Text = $"<center><br>A carta foi enviada com sucesso<br> a(o) Sr(a) <b>{carta.DadosDevedor.Nome}</b><br>para o email: {carta.DadosDevedor.Email}</center>";
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message.Replace("\r\n", "<br>");
            }

        }
    }
}

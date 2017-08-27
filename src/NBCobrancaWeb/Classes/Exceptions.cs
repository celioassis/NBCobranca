using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace NBCobranca.Classes
{
    /// <summary>
    /// Classe de exception para informar que uma entidade j� existe
    /// </summary>
    public class ExceptionEntidadeJaExiste : Exception
    {
        public ExceptionEntidadeJaExiste(string pMensagem)
            : base(pMensagem)
        { }
    }

    public class ExceptionVariasEntidadesComMesmoCNPJ_CPF : Exception
    {
        public ExceptionVariasEntidadesComMesmoCNPJ_CPF(string pMensagem)
            : base(pMensagem)
        { }
    }

    public class ExceptionCampoObrigatorio : Exception
    {
        string aNomeCampo;

        public ExceptionCampoObrigatorio(string pNomeCampoObrigatorio)
        {
            this.aNomeCampo = pNomeCampoObrigatorio;
        }

        /// <summary>
        /// Nome do campo que deveria ter seu valor preenchido obrigat�riamente.
        /// </summary>
        public string CampoObrigatorio
        {
            get { return this.aNomeCampo; }
        }
    }

    public class ExceptionNaoExisteFichasParaDistribuir : Exception
    {
        public ExceptionNaoExisteFichasParaDistribuir()
            : base("N�o existem fichas livres para Distruibui��o.")
        { }
    }

    /// <summary>
    /// Classe de exception respons�ve por informar qualquer erro no envio de SMS
    /// </summary>
    public class ExceptionSMS : Exception
    {
        public ExceptionSMS(string Mensagem, Exception innerException)
            : base(Mensagem, innerException)
        { }
    }
}

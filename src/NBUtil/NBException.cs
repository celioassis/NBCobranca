using System;
using System.Collections.Generic;
using System.Text;

namespace Neobridge.NBUtil
{
    public class NBException : Exception
    {
        string aStringSQL = "";
        public NBException() : base() { }
        public NBException(string pMensagem) : base(pMensagem) { }
        public NBException(string pMensagem, Exception pInnerException) : base(pMensagem, pInnerException) { }
        public NBException(string pMensagem, string pSource, Exception pInnerException)
            : base(pMensagem, pInnerException)
        {
            this.Source = pSource;
        }
        public NBException(string pMensagem, string pSource, Exception pInnerException, string pStringSQL)
            : this(pMensagem, pSource, pInnerException)
        {
            this.aStringSQL = pStringSQL;
        }
    }
}

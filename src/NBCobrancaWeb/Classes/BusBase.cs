using System;
using Neobridge.NBDB;

namespace NBCobranca.Classes
{
    /// <summary>
    /// Classe base de neg�cios
    /// </summary>
    public class BusBase : IDisposable
    {
        protected Sistema Sistema;
        protected DBDirect DbDirect;
        protected readonly string Source;

        protected BusBase(Sistema sistema, DBDirect dbDirect)
        {
            if (sistema == null)
                throw new NullReferenceException("A factory de Neg�cios n�o pode ser nula na contru��o de um objeto de neg�cio");
            Sistema = sistema;
            DbDirect = dbDirect;
            Source = GetType().FullName;
        }

        #region IDisposable Members

        public virtual void Dispose()
        {
            Sistema = null;
            DbDirect = null;
        }

        #endregion
    }
}

using System;
using Neobridge.NBDB;

namespace NBCobranca.Classes
{
    /// <summary>
    /// Classe base de negócios
    /// </summary>
    public class BusBase : IDisposable
    {
        protected Sistema Sistema;
        protected DBDirect DbDirect;
        protected readonly string Source;

        protected BusBase(Sistema sistema, DBDirect dbDirect)
        {
            if (sistema == null)
                throw new NullReferenceException("A factory de Negócios não pode ser nula na contrução de um objeto de negócio");
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

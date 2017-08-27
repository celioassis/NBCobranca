using System;
using System.Collections.Generic;
using System.Text;

namespace NBCobranca.Interfaces
{
    /// <summary>
    /// Interface para as Entidades relacionadas ao banco de dados.
    /// </summary>
    public interface IEntidade
    {
        int ID { get;}
        string Key { get;}
    }
}

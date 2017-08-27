using System;
using System.Collections.Generic;
using System.Text;

namespace NBCobranca.Interfaces
{
    public interface IpresAlertas
    {
        DateTime DataHora { get;set;}
        int ID_UsuarioDestino { get;set;}
        string MensagemAlerta { get;set;}
    }
}

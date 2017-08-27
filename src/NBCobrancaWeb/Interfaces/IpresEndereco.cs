using System;
using System.Collections.Generic;
using System.Text;

namespace NBCobranca.Interfaces
{
    public interface IpresEndereco
    {
        string Logradouro { get;set;}
        string complemento { get;set;}
        string Bairro { get;set;}
        string CEP { get;set;}
        string Municipio { get;set;}
        string UF { get;set;}
        string Comentario { get;set;}
        string Contato { get;set;}
        bool Principal { get;set;}
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NBCobranca.Interfaces
{
    public interface IpresTelefone
    {
        string Fone_Descricao { get;set;}
        string DDD { get;set;}
        string Fone { get;set;}
        string Ramal { get;set;}
        string Fone_Contato { get;set;}

    }
}

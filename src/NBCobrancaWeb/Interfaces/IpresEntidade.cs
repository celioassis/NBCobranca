using System;
using System.Collections.Generic;
using System.Text;

namespace NBCobranca.Interfaces
{
    public interface IpresEntidade
    {
        Tipos.TipoPessoa TipoPessoa { get;set;}
        string TextoRespeito { get;set;}
        string NomeRazaoSocial { get;set;}
        string CPFCNPJ { get;set;}
        string ApelidoNomeFantasia { get;set;}
        string RgIE { get;set;}
    }
}

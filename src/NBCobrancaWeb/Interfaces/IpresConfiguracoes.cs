using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBCobranca.Interfaces
{
    interface IpresConfiguracoes : IPresView
    {
        int IDConfig { get; set; }
        string ServidorSMTP { get; set; }
        int PortaSMTP { get; set; }
        string UsuarioSMTP { get; set; }
        string Senha { get; set; }
        string ConfirmaSenha { get; set; }
        string EmailDeValidacao { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NBCobranca.Interfaces
{
    public interface IPresDefault: IPresView
    {
        string ImagemFundo { get;set;}
        string DataCompletaDoDia { set;}
        string MensagemDeBoasVindas { set;}
    }
}

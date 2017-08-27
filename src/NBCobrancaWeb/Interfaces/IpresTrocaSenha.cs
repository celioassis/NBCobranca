using System.Security.Cryptography.X509Certificates;

namespace NBCobranca.Interfaces
{
    public interface IpresTrocaSenha: IPresView
    {
        string SenhaAtual { get; set; }
        string NovaSenha { get; set; }
        string ConfirmaNovaSenha { get; set; }
        bool AtualizaCampos { set; }
    }
}
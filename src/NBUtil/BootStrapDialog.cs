using System;
using System.Collections.Generic;
using System.Text;

namespace Neobridge.NBUtil
{
    public class BootStrapDialog
    {
        public enum TypeMessage
        {
            TYPE_DEFAULT,
            TYPE_INFO,
            TYPE_PRIMARY,
            TYPE_SUCCESS,
            TYPE_WARNING,
            TYPE_DANGER
        }
        public static string Mensagem(string Titulo, string Mensagem, TypeMessage TipoMensagem)
        {
            StringBuilder mMessage = new StringBuilder("BootstrapDialog.show({");
            mMessage.AppendFormat("type: BootstrapDialog.{0}, ", TipoMensagem);
            mMessage.AppendFormat("title: '{0}', ", Titulo);
            mMessage.AppendFormat("message: '{0}' ", Mensagem);
            mMessage.Append("});");
            return mMessage.ToString();
        }
    }
}

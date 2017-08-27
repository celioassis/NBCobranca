using NBCobranca.Classes;
using NBCobranca.Tipos;

namespace NBCobranca.Controllers
{
    public abstract class CtrBase
    {
        protected readonly Sistema Sistema;
        protected readonly CtrFactory CtrFactory;
        protected string Source;

        protected CtrBase(Sistema sistema, CtrFactory ctrFactory)
        {
            Sistema = sistema;
            CtrFactory = ctrFactory;
            Source = GetType().FullName;
        }

        public virtual void ValidaCredencial(Permissao pPermissao = Permissao.Padrao)
        {
            Sistema.ValidaCredencial(pPermissao);
        }
    }
}

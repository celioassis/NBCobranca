using System;
using System.Collections.Generic;
using System.Text;

namespace NBCobranca.Interfaces
{
    public interface IControllerPresenter
    {
        void SetView(IPresView view);
    }
}

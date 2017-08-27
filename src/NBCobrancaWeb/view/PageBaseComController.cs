using System;
using System.Reflection;
using System.Web.UI;
using Anthem;
using NBCobranca.Controllers;
using NBCobranca.Interfaces;
using Neobridge.NBUtil;
using Label = System.Web.UI.WebControls.Label;
using static Neobridge.NBUtil.BootStrapDialog.TypeMessage;

namespace NBCobranca.view
{
    /// <summary>
    /// Classe base com referencia ao controller genérico para as demais classes de página 
    /// que utilizem de controller próprio.
    /// </summary>
    public class PageBaseComController<T> : PageBase, IPresView
        where T : IControllerPresenter
    {
        protected T CtrPage;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            CtrPage = Controller.GetInstance<T>();
            try
            {
                CtrPage.SetView(this);
            }
            catch (Exception ex)
            {
                Server.Transfer($"Erro.aspx?msgErro={ex.Message}", true);
            }

        }

        #region IpresView Members

        public string Titulo
        {
            set
            {
                var lblTitulo = Master.FindControl("lblTitulo") as Label;
                lblTitulo.Text = value;
                Page.Title = "..:: " + value + " ::..";
            }
        }

        bool IPresView.IsCallBack => Manager.IsCallBack;

        #endregion

    }
}

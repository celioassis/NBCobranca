using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NBCobranca.Interfaces;

namespace NBCobranca.Controllers
{
    /// <summary>
    /// Classe responsável por controlar o fluxo do processo da página Devedores.aspx
    /// </summary>
    public class ctrDevedores : CtrBase, IControllerPresenter
    {
        IPresDevedores _view;

        Classes.LimEntidades aLimEntidades;
        Classes.BusClasses aClasses;
        Classes.BusEntidade aEntidades;
        Tipos.TipoPesquisa aTipoPesquisaAtual;
        string aFiltroPesquisa;
        string aXmPath_LinkEntNo;

        int aTotalRegistrosPesquisaAtual;

        public ctrDevedores(Classes.Sistema sistema, CtrFactory ctrFactory)
            : base(sistema, ctrFactory)
        {
            this.Sistema.TipoEntidade = NBdbm.tipos.TipoEntidade.Devedores;
            this.aLimEntidades = this.Sistema.LimEntidades;
            this.aClasses = this.Sistema.BusClasses;
            this.aEntidades = this.Sistema.busEntidade;
        }

        /// <summary>
        /// Set - Define qual é a treeview com a arvore de carteiras de devedores do sistema.
        /// </summary>
        public ComponentArt.Web.UI.TreeView ArvoreCarteiras
        {
            set
            {
                this.aClasses.TreeView = value;
                this.aLimEntidades.xmPath_LinkEntNo = this.aClasses.XmPathClasseSelecionada;
                this.aXmPath_LinkEntNo = this.aClasses.XmPathClasseSelecionada;
            }
        }

        public override void ValidaCredencial(NBCobranca.Tipos.Permissao pPermissao)
        {
            base.ValidaCredencial(pPermissao);
            this.Sistema.Legenda.Titulo = "Devedores";
            this.Sistema.Legenda.SubTitulo = "Devedor";
            this.Sistema.TipoEntidade = NBdbm.tipos.TipoEntidade.Devedores;
        }

        /// <summary>
        /// Atualiza a Lengenda da página conforme a carteira selecionada.
        /// </summary>
        public void AtualizaLegenda()
        {
            if (this.aClasses.NomeClasseSelecionada != "Carteiras")
                this.Sistema.Legenda.Titulo = "Devedores - " + this.aClasses.NomeClasseSelecionada;
            else
                this.Sistema.Legenda.Titulo = "Devedores";
        }

        /// <summary>
        /// Retorna a carteira selecionada na arvore de carteiras.
        /// </summary>
        public string CarteiraSelecionada
        {
            get
            {
                return this.aXmPath_LinkEntNo;
            }
        }

        /// <summary>
        /// Define qual serão os filtros aplicados para a pesquisa dos devedores.
        /// </summary>
        /// <param name="pTipoPesquisa">Enum que define qual é o tipo de pesquisa a ser realizada</param>
        /// <param name="pFiltroPesquisa">Valor da pesquisa conforme o tipo de pesquisa selecionado.</param>
        public void DefineValoresPesquisa(Tipos.TipoPesquisa pTipoPesquisa, string pFiltroPesquisa)
        {
            this.aTipoPesquisaAtual = pTipoPesquisa;
            this.aFiltroPesquisa = pFiltroPesquisa;
        }

        /// <summary>
        /// Realiza a pesquisa dos devedores trazendo somente a quantidade de registros informado pelo parametro 
        /// pLinhasPorPagina e da referida página indicado no parametro pPagina
        /// </summary>
        /// <param name="pLinhasPorPagina">Quantidade de registros de será retorna da pesquisa</param>
        /// <param name="pPagina">número da página que deverá buscar os registros da pesquisa.</param>
        /// <returns>Retorna um DataTable com os dados da pesquisa conforme os filtros definido no método DefineValoresPesquisa</returns>
        public DataTable PesquisaDevedores(int pLinhasPorPagina, int pPagina)
        {
            if (pPagina > 0)
                pPagina /= pLinhasPorPagina;
            return this.aEntidades.LoadEntidades(this.aFiltroPesquisa, this.aXmPath_LinkEntNo, pPagina,
                pLinhasPorPagina, ref this.aTotalRegistrosPesquisaAtual, this.aTipoPesquisaAtual,
                NBCobranca.Tipos.TipoPessoa.Todas);
        }

        public DataView PesquisaDevedores()
        {
            return this.aEntidades.LoadEntidades(_view.GetCarteiraSelecionada, (Tipos.TipoPesquisa)_view.GetCampoFiltro, _view.GetTextoProcurar);
        }
        /// <summary>
        /// Retorna o total de registros referente a ultima pesquisa realizada.
        /// </summary>
        /// <returns></returns>
        public int TotalDevedoresPesquisados()
        {
            return this.aTotalRegistrosPesquisaAtual;
        }

        /// <summary>
        /// Edita uma ficha de devedor
        /// </summary>
        /// <param name="pCodigoDevedor"></param>
        public void EditarDevedor(string pCodigoDevedor)
        {
            //this.aSistema.TipoEntidade = NBdbm.tipos.TipoEntidade.Devedores;
            //this.aLimEntidades.xmPath_LinkEntNo = this.aXmPath_LinkEntNo;
            //this.aLimEntidades.Consulta(pCodigoDevedor);
            this.CtrFactory.ctrCadEntidades.CarregaEntidade(Convert.ToInt32(pCodigoDevedor), Tipos.TipoEntidades.Devedores, this.aXmPath_LinkEntNo);
        }

        public void DeletarDevedor(string pCodigoDevedor, string pNomeDevedor)
        {
            this.aEntidades.Excluir(Convert.ToInt32(pCodigoDevedor), pNomeDevedor,
                NBCobranca.Tipos.TipoEntidades.Devedores, this.aXmPath_LinkEntNo);
        }

        /// <summary>
        /// Indica para o controller de Cadastro de Entidades que será iniciado o cadastro de um novo devedor.
        /// </summary>
        public void NovoDevedor()
        {
            this.CtrFactory.ctrCadEntidades.NovaEntidade(this.aXmPath_LinkEntNo, Tipos.TipoEntidades.Devedores);
        }

        public void SetView(IPresView view)
        {
            _view = (IPresDevedores)view;
            _view.Titulo = "Controle de Devedores";
        }
    }
}

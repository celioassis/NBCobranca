using System;

namespace NBCobranca.Classes
{
    /// <summary>
    /// Regra de Negócio do Gerenciamento de Classes
    /// </summary>
    public class BusClasses : IDisposable
    {
        private string aSource = "Regra de Negócio de Classes";
        private NBArvore.WebTree.UI aNBTree;
        private NBArvore.Arvore aNBArvore;
        private bool aArvoreAlterada = false;
        private System.Data.SqlClient.SqlConnection aConn;
        private string aConnString = "";
        private Classes.Sistema aSistema;
        private ComponentArt.Web.UI.TreeView aTreeView;

        public BusClasses(Classes.Sistema pSistema)
        {
            this.aSistema = pSistema;
            this.aNBArvore = new NBArvore.Arvore(0, this.Conn, false);
        }

        public NBArvore.WebTree.UI NBTree
        {
            get
            {
                if (this.aNBTree == null)
                {
                    if (this.aTreeView == null)
                        this.aTreeView = new ComponentArt.Web.UI.TreeView();
                    CarregaArvore("Entidades\\Carteiras", false);
                }
                return this.aNBTree;
            }
        }
        public void CarregaArvore(string pPath, bool pPermiteEdicao)
        {
            if (this.aTreeView == null)
                throw new NBdbm.COBR_Exception("TreeView não foi Informada no PageLoad", this.aSource);

            if (this.aNBTree == null)
                this.aNBTree = new NBArvore.WebTree.UI(pPath, this.aTreeView, this.Conn, pPermiteEdicao);
            else if (this.aArvoreAlterada)
            {
                this.aNBTree.Dispose();
                this.aNBTree = null;
                this.aTreeView.Nodes.Clear();
                this.aNBTree = new NBArvore.WebTree.UI(pPath, this.aTreeView, this.Conn, pPermiteEdicao);
                this.aArvoreAlterada = false;
            }
            else
                this.aNBTree.Refresh(this.aTreeView);

            this.aTreeView.CssClass = "TreeView";
            this.aTreeView.NodeCssClass = "TreeNode";
            this.aTreeView.SelectedNodeCssClass = "SelectedTreeNode";
            this.aTreeView.HoverNodeCssClass = "HoverTreeNode";
            this.aTreeView.LineImagesFolderUrl = @"../imagens/arvore/lines";
            this.aTreeView.ParentNodeImageUrl = "../imagens/arvore/folders.gif";
            this.aTreeView.LeafNodeImageUrl = "../imagens/arvore/folder.gif";
            if (this.aTreeView.SelectedNode == null)
                this.aTreeView.SelectedNode = this.aTreeView.Nodes[0];
        }

        private System.Data.SqlClient.SqlConnection Conn
        {
            get
            {
                if (this.aConn == null)
                {
                    this.aConn = new System.Data.SqlClient.SqlConnection(NBFuncoes.Conexao(this.aSistema.Self, this.aSistema.LimLogin.TipoConexao).ConnectionString);
                    this.aConnString = this.aConn.ConnectionString;
                }
                if (this.aConn.ConnectionString == "")
                    this.aConn.ConnectionString = this.aConnString;

                if (this.aConn.State == System.Data.ConnectionState.Closed)
                    this.aConn.Open();

                return this.aConn;
            }
        }

        public bool ArvoreAlterada
        {
            get
            {
                return this.aArvoreAlterada;
            }
        }

        public void SalvarArvoreTreeView()
        {
            try
            {
                this.aNBTree.SalvaArvore(this.TreeView);
                this.aArvoreAlterada = false;
            }
            catch (Exception ex)
            {
                throw new NBdbm.COBR_Exception("Problemas ao Salvar a Estrutura de Classes", this.aSource, ex);
            }

        }

        public void CriarFilho(string pPathPai, string pNomeFilho)
        {
            NBArvore.No mNoPai = new NBArvore.No(this.aNBArvore.Storage);
            mNoPai = (NBArvore.No)this.aNBArvore.Roots.ItemByPath(pPathPai);
            mNoPai.Filhos.Add(pNomeFilho);
        }
        public void SalvarArvore()
        {
            this.aNBArvore.Storage.SalvarArvore();
            this.aArvoreAlterada = true;
        }
        public void ExcluirNo(string pPathNo)
        {
            NBArvore.No mNoPai = new NBArvore.No(this.aNBArvore.Storage);
            mNoPai = (NBArvore.No)this.aNBArvore.Roots.ItemByPath(pPathNo);
            mNoPai.Delete();
            this.aArvoreAlterada = true;
        }

        /// <summary>
        /// Busca um NBArvore.No apartir de um XmPath
        /// </summary>
        /// <param name="pXmPath">Caminho XML que identifique o No</param>
        /// <returns>Retorna um Objeto do Tipo NBArvore.No</returns>
        public NBArvore.No GetNo(string pXmPath)
        {
            return this.aNBArvore.Roots.ItemByPath(pXmPath) as NBArvore.No;
        }
        public void ExcluirClasse()
        {
            try
            {
                this.aNBTree.Delete(this.TreeView);
                this.aArvoreAlterada = true;
            }
            catch (Exception ex)
            {
                throw new NBdbm.COBR_Exception("Problemas na Exclusão da Classes", this.aSource, ex);
            }

        }
        public void MoverClasse(object pSender, ComponentArt.Web.UI.TreeViewNodeMovedEventArgs pEvent)
        {
            try
            {
                this.aNBTree.TV_NodeMoved(pSender, pEvent, this.TreeView);
                this.aArvoreAlterada = true;
            }
            catch (Exception ex)
            {
                throw new NBdbm.COBR_Exception("Problemas Na Movimentação da Classe", this.aSource, ex);
            }
        }

        public void AdicionarClasse(string pNome)
        {
            try
            {
                this.aNBTree.CriarNo(pNome, this.TreeView);
                this.aArvoreAlterada = true;
            }
            catch (Exception ex)
            {
                throw new NBdbm.COBR_Exception("Problemas ao Adicionar a Classe.", this.aSource, ex);
            }
        }

        public void RenomearClasse(string pNome)
        {
            try
            {
                this.aNBTree.Rename(pNome, this.TreeView);
                this.aArvoreAlterada = true;
            }
            catch (Exception ex)
            {
                throw new NBdbm.COBR_Exception("Problemas ao renomear a Classe", this.aSource, ex);
            }
        }

        public string XmPathClasseSelecionada
        {
            get
            {
                return this.NBTree.getXMPathNoSelecionado(this.TreeView);
            }
        }
        public string PathClasseSelecionada
        {
            get
            {
                return this.NBTree.getPathNoSelecionado(this.TreeView);
            }
        }
        public string NomeClasseSelecionada
        {
            get
            {
                return this.NBTree.getNomeNoSelecionado(this.aTreeView);
            }
        }
        /// <summary>
        /// Define Qual TreeView o Sistema esta usando para Criar a Arvore, 
        /// esta propriedade deve ser alimentada sempre no PageLoad.
        /// </summary>
        public ComponentArt.Web.UI.TreeView TreeView
        {
            get
            {
                if (this.aTreeView == null)
                {
                    this.aTreeView = new ComponentArt.Web.UI.TreeView();
                    this.CarregaArvore("Entidades\\Carteiras", false);
                }
                return this.aTreeView;
            }
            set
            {
                this.aTreeView = value;
                this.CarregaArvore("Entidades\\Carteiras", false);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (aNBTree != null)
                aNBTree.Dispose();
            aNBTree = null;

            if (aNBArvore != null)
                aNBArvore.Dispose();
            aNBArvore = null;

            if (aConn != null)
                aConn.Dispose();
            aConn = null;

            aSistema = null;
            aTreeView = null;
        }

        #endregion
    }
}

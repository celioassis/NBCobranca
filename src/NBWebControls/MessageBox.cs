using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace NBWebControls
{
    public delegate void YesChoosedEventHandler(object sender, string Key);
    public delegate void NoChoosedEventHandler(object sender, string Key);
    public delegate void CloseModalEventHandler(object sender, string Key, string pValorRetorno);
    /// <summary>
    ///	Cria uma MessageBox nas páginas WEB com o uso de alert e confirm 
    ///	do JavaScript.
    ///	Também tem uma JanelaModal com retorno de dados.
    /// </summary>
    [DefaultProperty("Text"),
    ToolboxData("<{0}:MessageBox runat=server></{0}:MessageBox>")]
    public class MessageBox : System.Web.UI.WebControls.WebControl, IPostBackEventHandler, Anthem.ICallBackControl, Anthem.IUpdatableControl
    {
        private int aLargura = 600;
        private int aAltura = 440;
        private string aValorRetorno = "False";
        private string aPaginaAspx;
        private string aCampoRecebeFoco;
        private string aMessage;
        private string aKey;
        private string aPastaScripts = "Scripts";
        private string aPastaStyles = "Styles";
        private string aBotaoSubmit = "";
        private string aMsgBoxId = "";
        private bool aPostBackOnYes;
        private bool aPostBackOnNo;
        private bool aPostBackOnCloseModal;
        private bool aUsaModal = true;
        private bool aGerarArquivoScript = true;
        private bool aManterScroll = false;
        private bool aFechaModal = false;
        private bool aUsandoAjaxAnthem = false;
        private TipoAcao aTipoAcao;
        private enTipoSubmit aTipoSubmit = enTipoSubmit.Nenhum;

        // TODO: Populate Comments.
        /// <summary>YesChoosed Event</summary>
        /// <remarks></remarks>
        public event YesChoosedEventHandler YesChoosed;
        // TODO: Populate Comments.
        /// <summary>NoChoosed Event</summary>
        /// <remarks></remarks>
        public event NoChoosedEventHandler NoChoosed;
        // TODO: Populate Comments.
        /// <summary>CloseModalChoosed Event</summary>
        /// <remarks></remarks>
        public event CloseModalEventHandler CloseModalChoosed;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (aUsandoAjaxAnthem)
                Anthem.Manager.Register(this);
            if (!string.IsNullOrEmpty(this.Page.Request.QueryString["MsgBoxId"]))
                this.aMsgBoxId = this.Page.Request.QueryString["MsgBoxId"];
        }


        // TODO: Populate Comments.
        /// <summary>Largura Property</summary>
        /// <remarks></remarks>
        [Bindable(true),
        Description("Largura da Janela Modal"),
        Category("Janela Modal"),
        DefaultValue(600)]
        public int Largura
        {
            get
            {
                return this.aLargura;
            }

            set
            {
                this.aLargura = value;
            }
        }

        // TODO: Populate Comments.
        /// <summary>Altura Property</summary>
        /// <remarks></remarks>
        [Bindable(true),
        Description("Altura da Janela Modal"),
        Category("Janela Modal"),
        DefaultValue(440)]
        public int Altura
        {
            get
            {
                return this.aAltura;
            }

            set
            {
                this.aAltura = value;
            }
        }

        // TODO: Populate Comments.
        /// <summary>PastaScripts Property</summary>
        /// <remarks></remarks>
        [Bindable(true),
        Description("Pasta onde serão localizados os Scripts js da JanelaModal"),
        Category("Janela Modal"),
        DefaultValue("Scripts")]
        public string PastaScripts
        {
            get
            {
                return this.aPastaScripts;
            }

            set
            {
                this.aPastaScripts = value;
            }
        }

        // TODO: Populate Comments.
        /// <summary>PastaStyles Property</summary>
        /// <remarks></remarks>
        [Bindable(true),
        Description("Pasta onde serão localizados os Styles CSS da JanelaModal"),
        Category("Janela Modal"),
        DefaultValue("Styles")]
        public string PastaStyles
        {
            get
            {
                return this.aPastaStyles;
            }

            set
            {
                this.aPastaStyles = value;
            }
        }
        // TODO: Populate Comments.
        /// <summary>GerarArquivoScripts Property</summary>
        /// <remarks></remarks>
        [Bindable(false),
        Description("Gera os arquivos de javaScript e StyleSheets nas suas devidas pastas, mas para isso o usuário Asp.Net tem que ter permissão de gravação na pasta local da aplicação."),
        Category("Janela Modal"),
        DefaultValue(true)]
        public bool GerarArquivoScripts
        {
            get
            {
                return this.aGerarArquivoScript;
            }

            set
            {
                this.aGerarArquivoScript = value;
            }
        }
        // TODO: Populate Comments.
        /// <summary>ManterScroll Property</summary>
        /// <remarks></remarks>
        [Bindable(false),
        Description("Manter Posicionamento da Barra de Scroll"),
        DefaultValue(false)]
        public bool ManterScroll
        {
            get
            {
                return this.aManterScroll;
            }

            set
            {
                this.aManterScroll = value;
            }
        }
        /// <summary>Usando Componente Ajax Anthem</summary>
        /// <remarks></remarks>
        [Bindable(false),
        Description("Informe se a Aplicação esta usando o Componente Anthem para Funções Ajax."),
        DefaultValue(false)]
        public bool UsandoAjaxAnthem
        {
            get
            {
                return this.aUsandoAjaxAnthem;
            }

            set
            {
                this.aUsandoAjaxAnthem = value;
            }
        }

        /// <summary>
        /// Nome do Botão que irá executar um submit, usado para refazer uma pesquisa 
        /// após o fechamento de uma janela modal.
        /// </summary>
        public string BotaoSubmit
        {
            set
            {
                this.aTipoSubmit = enTipoSubmit.Botao;
                this.aBotaoSubmit = value;
            }
        }
        /// <summary>
        /// Define qual é o tipo do elemento que fará o submit, os tipos são: 
        /// Form e Botao.
        /// </summary>
        public enTipoSubmit TipoSubmit
        {
            set
            {
                if (value == enTipoSubmit.Botao)
                    throw new Exception("Somente Defina o Botão para Submit que a ação estará completa");
                this.aTipoSubmit = value;
            }
        }
        /// <summary>
        /// Mostra uma Janela Modal Padrão com uma página Aspx dentro.
        /// </summary>
        /// <param name="pPaginaAspx">Nome da página Aspx que deverá ser aberta.</param>
        public void ModalShow(string pPaginaAspx)
        {
            this.aUsaModal = true;
            this.aTipoAcao = TipoAcao.ModalShow;
            this.aPaginaAspx = pPaginaAspx;

            if (this.aUsandoAjaxAnthem)
            {
                string mFunction = "document.body.scrollTop = 0;showPopWin('" + pPaginaAspx + "', " + aLargura.ToString() + ", " + aAltura.ToString() + ", '" + aBotaoSubmit + "');";
                Anthem.Manager.AddScriptForClientSideEval(mFunction);
            }

        }
        /// <summary>
        /// Mostra uma Janela Modal Padrão com uma página Aspx dentro.
        /// </summary>
        /// <param name="pPaginaAspx">Nome da página Aspx que deverá ser aberta.</param>
        /// <param name="pPostBackOnCloseModal">Boolean informando se será feito um PostBack quando a janela modal  for fechada.</param>
        public void ModalShow(string pPaginaAspx, bool pPostBackOnCloseModal)
        {
            if (pPaginaAspx.Replace("?", "##") != pPaginaAspx)
                pPaginaAspx += "&MsgBoxId=" + this.ID;
            else
                pPaginaAspx += "?MsgBoxId=" + this.ID;
            this.ModalShow(pPaginaAspx);
            this.aPostBackOnCloseModal = pPostBackOnCloseModal;
        }
        /// <summary>
        /// Fecha uma janela modal Retornando um Valor
        /// </summary>
        /// <param name="pRetorno">Valor que será retornado no evento CloseModalChoosed</param>
        /// <param name="pKey">
        /// Valor que será retornado no Evento CloseModalChoosed indicando del qual janela modal
        /// esta vindo os dados.
        /// </param>
        public void ModalClose(string pRetorno, string pKey)
        {
            this.aTipoAcao = TipoAcao.ModalClose;
            if (pRetorno != "")
            {
                this.aPostBackOnCloseModal = true;
                this.aValorRetorno = pRetorno;
                this.aKey = pKey;
                if (this.aUsandoAjaxAnthem && this.aPostBackOnCloseModal)
                {
                    if (this.aMsgBoxId == "") this.aMsgBoxId = this.ClientID;
                    string mCallBackModalClose = "window.parent.";
                    mCallBackModalClose += string.Format("Anthem_InvokeControlMethod('{0}', 'OnCloseModal', ['{1}','{2}'], function() {{ }}, null);", this.aMsgBoxId, pKey, pRetorno);
                    string mFunction = "window.parent.FecharModal(); ";
                    mFunction += mCallBackModalClose;
                    mFunction += "window.parent.document.body.scrollTop = window.parent.document.body.scrollHeight;";
                    Anthem.Manager.AddScriptForClientSideEval(mFunction);
                }


            }
        }
        /// <summary>
        /// Fecha uma janela modal Retornando um Valor
        /// </summary>
        /// <param name="pRetorno">Valor que será retornado no evento CloseModalChoosed</param>
        /// <param name="pKey">
        /// Valor que será retornado no Evento CloseModalChoosed indicando del qual janela modal
        /// esta vindo os dados.
        /// </param>
        /// <param name="pMsgBoxId">string de identificação do componente MessageBox que 
        /// chamou a janela modal.
        /// </param>
        public void ModalClose(string pRetorno, string pKey, string pMsgBoxId)
        {
            this.aMsgBoxId = pMsgBoxId;
            this.ModalClose(pRetorno, pKey);
        }

        /// <summary>
        /// Fecha uma Janela Modal.
        /// </summary>
        public void ModalClose()
        {
            this.aFechaModal = true;
            this.aTipoAcao = TipoAcao.ModalClose;

            if (aUsandoAjaxAnthem)
            {
                string mFunction = "window.parent.FecharModal();";
                Anthem.Manager.AddScriptForClientSideEval(mFunction);
            }

        }

        public void ModalClose(bool pPostBackOnCloseModal)
        {
            this.aPostBackOnCloseModal = pPostBackOnCloseModal;
            this.ModalClose();
        }

        /// <summary>
        /// Indica se a Janela Modal esta sendo Fechada, é sempre marcada
        /// como true no momento da execução ModalClose.
        /// </summary>
        public bool FechandoModal
        {
            get
            {
                return this.aFechaModal;
            }
        }
        /// <summary>
        /// Exibe uma Mensagem de Texto em formato alert do javaScript.
        /// </summary>
        /// <param name="pMensagem">Mensagem que será exibida.</param>
        public void Show(string pMensagem)
        {
            this.aTipoAcao = TipoAcao.MsgAlert;
            this.aMessage = pMensagem.Replace("'", "#");

            if (this.aUsandoAjaxAnthem)
            {
                string mScript = "alert('" + aMessage + "'); ";
                Anthem.Manager.AddScriptForClientSideEval(mScript);
            }
        }
        /// <summary>
        /// Exibe uma Mensagem de Texto em formato alert do javaScript, retomando 
        /// para um determinado campo.
        /// </summary>
        /// <param name="pMensagem">Mensagem que será exibida.</param>
        /// <param name="pCampoRecebeFoco">Campo que receberá o foco após a mensagem.</param>
        public void Show(string pMensagem, string pCampoRecebeFoco)
        {
            this.aTipoAcao = TipoAcao.MsgAlertMoverFoco;
            this.aMessage = pMensagem.Replace("'", "#");
            this.aCampoRecebeFoco = pCampoRecebeFoco;

            if (this.aUsandoAjaxAnthem)
            {
                string mScript = "alert('" + aMessage + "'); ";
                mScript += "document.forms[0].elements['" + aCampoRecebeFoco + "'].focus();";
                Anthem.Manager.AddScriptForClientSideEval(mScript);
            }
        }
        /// <summary>
        /// Exibir Mensagem e Fechar a Janela Modal
        /// </summary>
        /// <param name="pMensagem"></param>
        /// <param name="pFecharModal"></param>
        public void Show(string pMensagem, bool pFecharModal)
        {
            if (pFecharModal)
            {
                this.aTipoAcao = TipoAcao.MsgAlertModalClose;
                this.aFechaModal = pFecharModal;
            }
            else
                this.aTipoAcao = TipoAcao.MsgAlert;
            this.aMessage = pMensagem.Replace("'", "#");

            if (this.aUsandoAjaxAnthem)
            {
                string mScript = "alert('" + aMessage + "'); ";
                if (pFecharModal)
                    mScript += "window.parent.hidePopWin(); ";
                Anthem.Manager.AddScriptForClientSideEval(mScript);
            }

        }
        /// <summary>
        /// Exibe uma mensagem de texto no formato confirm do javascript.
        /// </summary>
        /// <param name="pMensagem">Mensagem interrogativa que será exibida.</param>
        /// <param name="pKey">Usado para definir várias ações</param>
        /// <param name="pPostBackOnYes">Boolean que define se será excutando um postBack após um Yes(OK)</param>
        /// <param name="pPostBackOnNo">Boolean que define se será excutando um postBack após um No(CANCELAR)</param>
        public void ShowConfirma(string pMensagem, string pKey, bool pPostBackOnYes, bool pPostBackOnNo)
        {
            this.aTipoAcao = TipoAcao.MsgConfirm;
            this.aPostBackOnYes = pPostBackOnYes;
            this.aPostBackOnNo = pPostBackOnNo;
            this.aKey = pKey;
            this.aMessage = pMensagem.Replace("'", "#");
            if (this.aUsandoAjaxAnthem)
            {
                string mCallBackOnYes = "aTipoAcao=0;", mCallBackOnNo = "aTipoAcao=0;";

                if (this.aPostBackOnYes)
                    mCallBackOnYes = string.Format("Anthem_InvokeControlMethod('{0}', 'OnYes', ['{1}'], function() {{ }}, null);", this.ClientID, aKey);

                if (this.aPostBackOnNo)
                    mCallBackOnNo = string.Format("Anthem_InvokeControlMethod('{0}', 'OnNo', ['{1}'], function() {{ }}, null);", this.ClientID, aKey);

                string mScript = "if (confirm('" + aMessage + "')) ";

                mScript += mCallBackOnYes;

                if (aPostBackOnNo)
                    mScript += " else " + mCallBackOnNo;

                Anthem.Manager.AddScriptForClientSideEval(mScript);
            }
        }
        /// <summary>
        /// Move o Foco para um determinado campo no WebForm.
        /// </summary>
        /// <param name="pCampoRecebeFoco">Nome de Client do campo que receberá o Foco</param>
        public void MoverFoco(string pCampoRecebeFoco)
        {
            this.aTipoAcao = TipoAcao.MoverFoco;
            this.aCampoRecebeFoco = pCampoRecebeFoco;
            if (this.aUsandoAjaxAnthem)
            {
                string mScript = "document.forms[0].elements['" + aCampoRecebeFoco + "'].focus();";
                Anthem.Manager.AddScriptForClientSideEval(mScript);
            }

        }

        /// <summary>
        /// Executa uma ação de submit na página client conforme o tipo informado préviamente
        /// nas propriedades TipoSubmit e BotaoSubmit se for no caso de um botão fazer o submit.
        /// </summary>
        public void FazerSubmit()
        {
            switch (this.aTipoSubmit)
            {
                case enTipoSubmit.Nenhum:
                    throw new Exception("Não pode se fazer um submit sem antes definir o seu tipo");
                case enTipoSubmit.Botao:
                    if (this.aBotaoSubmit == "")
                        throw new Exception("É preciso definir um nome para o botão que ira fazer o submit");
                    if (this.aUsandoAjaxAnthem)
                    {
                        string mScript = "var botao = document.getElementById('" + aBotaoSubmit + "');";
                        mScript += " botao.click();";
                        Anthem.Manager.AddScriptForClientSideEval(mScript);
                    }
                    break;
                default:
                    if (this.aUsandoAjaxAnthem)
                    {
                        string mScript = "document.forms[0].submit();";
                        Anthem.Manager.AddScriptForClientSideEval(mScript);
                    }
                    break;
            }
            this.aTipoAcao = TipoAcao.ExecutarSubmit;

        }

        /// <summary>
        /// Move a Barra de Scroll para o fim da página.
        /// </summary>
        public void ScrollToEnd()
        {
            Anthem.Manager.AddScriptForClientSideEval("MoveScrollToEnd();");
        }


        private bool ModoDesign(System.Web.UI.WebControls.WebControl QueControl)
        {
            bool mododesingn = false;
            try
            {
                mododesingn = QueControl.Site.DesignMode;
            }
            catch { }
            return mododesingn;

        }
        private string ScriptAcao()
        {
            System.Text.StringBuilder script = new System.Text.StringBuilder();
            string mPostBackOnYes = "aTipoAcao=0;";
            string mPostBackOnNo = "aTipoAcao=0;";
            string mPostBackOnFechaModal = "aTipoAcao=0;";

            if (this.aPostBackOnYes)
                mPostBackOnYes = Page.GetPostBackEventReference(this, "Yes" + this.aKey);
            if (this.aPostBackOnNo)
                mPostBackOnNo = Page.GetPostBackEventReference(this, "No_" + this.aKey);
            if (this.aPostBackOnCloseModal)
            {
                mPostBackOnFechaModal = "window.parent." + Page.GetPostBackEventReference(this, "Clo#" + this.aKey + "#" + this.aValorRetorno);
                string[] mArrayPost = mPostBackOnFechaModal.Split('\'');
                if (this.aMsgBoxId != "")
                    mPostBackOnFechaModal = mArrayPost[0] + "'" + this.aMsgBoxId + "'" + mArrayPost[2] + "'" + mArrayPost[3] + "')";
            }

            #region === Definição de Variáveis ===
            script.Append("\r<script language=\"javascript\">\r\t");
            script.Append("var aTipoAcao;\r\t");
            script.Append("var aPaginaAspx;\r\t");
            script.Append("var aAltura = " + this.aAltura.ToString() + ";\r\t");
            script.Append("var aLargura = " + this.aLargura.ToString() + ";\r\t");
            script.Append("var aTipoSubmit = '" + this.aTipoSubmit.ToString() + "';\r\t");
            script.Append("var aBotaoSubmit = '" + this.aBotaoSubmit + "';\r\t");
            script.Append("var aManterScroll = '" + this.aManterScroll.ToString() + "';\r\t");
            script.Append("var aMessage;\r\t");
            script.Append("var aCampoRecebeFoco;\r\r\t");
            #endregion

            #region === Adiciona aos Eventos ===
            script.Append("addEvent(window, 'load', ExecuteAcao);\r\t");
            script.Append("if (aManterScroll=='True')\r\t");
            script.Append("{\r\t\t");
            script.Append("addEvent(window, 'load', setScrollPosition);\r\t\t");
            script.Append("addEvent(window, 'scroll', saveScrollPosition);\r\t");
            script.Append("}\r\t");

            #endregion

            #region === Execute Ação ===
            script.Append("function ExecuteAcao()\r\t");
            script.Append("{\r\t\t");

            script.Append("switch(aTipoAcao)\r\t\t");
            script.Append("{\r\t\t\t");

            script.Append("case '" + TipoAcao.ModalShow.ToString() + "':\r\t\t\t\t");
            script.Append("showPopWin(aJanelaAspx, aLargura, aAltura, aBotaoSubmit);\r\t\t\t\t");
            script.Append("break;\r\t\t\t");

            script.Append("case '" + TipoAcao.ExecutarSubmit.ToString() + "':\r\t\t\t\t");
            script.Append("FazerSubmit();\r\t\t\t\t");
            script.Append("break;\r\t\t\t");

            script.Append("case '" + TipoAcao.ModalClose.ToString() + "':\r\t\t\t\t");
            script.Append("window.parent.hidePopWin();\r\t\t\t\t");
            script.Append(mPostBackOnFechaModal + "\r\t\t\t\t");
            script.Append("break;\r\t\t\t");

            script.Append("case '" + TipoAcao.MoverFoco.ToString() + "':\r\t\t\t\t");
            script.Append("document.forms[0].elements[aCampoRecebeFoco].focus();\r\t\t\t\t");
            script.Append("break;\r\t\t\t");

            script.Append("case '" + TipoAcao.MsgAlertMoverFoco.ToString() + "':\r\t\t\t\t");
            script.Append("alert(aMessage);\r\t\t\t\t");
            script.Append("document.forms[0].elements[aCampoRecebeFoco].focus();\r\t\t\t\t");
            script.Append("break;\r\t\t\t");

            script.Append("case '" + TipoAcao.MsgAlertModalClose.ToString() + "':\r\t\t\t\t");
            script.Append("alert(aMessage);\r\t\t\t\t");
            script.Append("window.parent.hidePopWin();\r\t\t\t\t");
            script.Append("break;\r\t\t\t");

            script.Append("case '" + TipoAcao.MsgAlert.ToString() + "':\r\t\t\t\t");
            script.Append("alert(aMessage);\r\t\t\t\t");
            script.Append("break;\r\t\t\t");

            script.Append("case '" + TipoAcao.MsgConfirm.ToString() + "':\r\t\t\t\t");
            script.Append("if (confirm(aMessage))\r\t\t\t\t\t");
            script.Append(mPostBackOnYes + "\r\t\t\t\t");
            script.Append("else\r\t\t\t\t\t");
            script.Append(mPostBackOnNo + "\r\t\t\t\t");
            script.Append("break;\r\t\t");

            script.Append("}\r\t\t");
            script.Append("aTipoAcao = '" + TipoAcao.Nenhuma.ToString() + "'\r\t");
            script.Append("}\r\r");
            #endregion

            #region === Fazer Submit ===
            script.Append("\tfunction FazerSubmit()\r\t");
            script.Append("{\r\t\t");
            script.Append("switch(aTipoSubmit)\r\t\t");
            script.Append("{\r\t\t\t");

            script.Append("case '" + enTipoSubmit.Botao.ToString() + "':\r\t\t\t\t");
            script.Append("var botao = document.getElementById(aBotaoSubmit);\r\t\t\t\t");
            script.Append("botao.click();\r\t\t\t\t");
            script.Append("break;\r\t\t\t");

            script.Append("case '" + enTipoSubmit.AnthemCallBack.ToString() + "':\r\t\t\t\t");
            string mAnthemCallBack = string.Format("Anthem_InvokePageMethod('AnthemCallBack', [], function() {{ }}, null);");
            script.Append(mAnthemCallBack + "\r\t\t\t\t");
            script.Append("break;\r\t\t\t");

            script.Append("case '" + enTipoSubmit.Form.ToString() + "':\r\t\t\t\t");
            script.Append("document.forms[0].submit();\r\t\t\t\t");
            script.Append("break;\r\t\t");

            script.Append("}\r\t}\r");
            #endregion

            #region === Salva Posição do Scroll ===
            script.Append("\tfunction saveScrollPosition()\r\t");
            script.Append("{\r\t\t");
            script.Append("var theBody = document.body;\r\t\t");
            script.Append("document.forms[0].__SCROLLPOS.value = theBody.scrollTop/theBody.scrollHeight;\r\t\t");
            script.Append("}\r\r");
            #endregion

            #region === Define a Posição do Scroll ===
            script.Append("\tfunction setScrollPosition()\r\t");
            script.Append("{\r\t\t");
            script.Append("var theBody = document.body;\r\t\t");

            string mScrollPos = this.Page.Request.Params["__SCROLLPOS"];
            mScrollPos = string.IsNullOrEmpty(mScrollPos) ? "0" : string.Format("{0}", Convert.ToDouble(mScrollPos.Replace('.',',')));

            script.Append("theBody.scrollTop = theBody.scrollHeight * " + mScrollPos.Replace(',','.') + ";\r\t");
            //script.Append("alert(theBody.scrollTop);\r\t");
            script.Append("}\r\r");
            #endregion

            #region === Move Scroll para o Fim da página ===
            script.Append("\tfunction MoveScrollToEnd()\r\t");
            script.Append("{\r\t\t");
            script.Append("var theBody = document.body;\r\t\t");
            script.Append("theBody.scrollTop = theBody.scrollHeight;\r\t");
            script.Append("}\r\r");
            #endregion

            script.Append("</script>");
            return script.ToString();
        }

        private string ScriptShowModal()
        {
            System.Text.StringBuilder script = new System.Text.StringBuilder();
            #region === Declaração de Variáveis ===
            script.Append("var gPopupMask = null;\r\t");
            script.Append("var gPopupContainer = null;\r\t");
            script.Append("var gPopFrame = null;\r\t");
            script.Append("var gReturnFunc;\r\t");
            script.Append("var gPopupIsShown = false;\r\t");
            script.Append("var gRefazPesquisa = false;\r\t");
            script.Append("var gHideSelects = false;\r\t");
            script.Append("var gRefreshForm = false;\r\t");
            script.Append("var gTabIndexes = new Array();\r\t");
            script.Append("// Pre-defined list of tags we want to disable/enable tabbing into\r\t");
            script.Append("var gTabbableTags = new Array(\"A\",\"BUTTON\",\"TEXTAREA\",\"INPUT\",\"IFRAME\");\r\t");
            script.Append("// If using Mozilla or Firefox, use Tab-key trap.\r\t");
            script.Append("if (!document.all)\r\t\t");
            script.Append("document.onkeypress = keyDownHandler;\r\r\t");
            #endregion

            #region === Função de Inicialização ===
            script.Append("/**\r\t* Initializes popup code on load.\r\t*/\r\t");
            script.Append("function initPopUp()\r\t{\r\t\t");
            script.Append("gPopupMask = document.getElementById(\"popupMask\");\r\t\t");
            script.Append("gPopupContainer = document.getElementById(\"popupContainer\");\r\t\t");
            script.Append("gPopFrame = document.getElementById(\"popupFrame\");\r\t\t");
            script.Append("// check to see if this is IE version 6 or lower. hide select boxes if so\r\t\t");
            script.Append("// maybe they'll fix this in version 7?\r\t\t");
            script.Append("var brsVersion = parseInt(window.navigator.appVersion.charAt(0), 10);\r\t\t");
            script.Append("if (brsVersion <= 6 && window.navigator.userAgent.indexOf(\"MSIE\") > -1) \r\t\t{\r\t\t\t");
            script.Append("gHideSelects = true;\r\t\t}\r\t");
            script.Append("}\r\t");
            script.Append("addEvent(window, \"load\", initPopUp);\r\r\t");
            #endregion

            #region === Função ShowPopWin ===
            script.Append("/**\r\t");
            script.Append("* @argument width - int in pixels\r\t");
            script.Append("* @argument height - int in pixels\r\t");
            script.Append("* @argument url - url to display\r\t");
            script.Append("* @argument returnFunc - function to call when returning true from the window.\r\t*/\r\t");
            script.Append("function showPopWin(url, width, height, elementSubmit) \r\t{\r\t\t");
            script.Append("gPopupIsShown = true;\r\t\t");
            script.Append("if (elementSubmit!=null)\r\t\t\t");
            script.Append("gRefreshForm = elementSubmit;\r\t\t");
            script.Append("disableTabIndexes();\r\t\t");
            script.Append("gPopupMask.style.display = \"block\";\r\t\t");
            script.Append("gPopupContainer.style.display = \"block\";\r\r\t\t");
            script.Append("// calculate where to place the window on screen\r\t\t");
            script.Append("centerPopWin(width, height);\r\t\t");
            script.Append("var titleBarHeight = parseInt(document.getElementById(\"popupTitleBar\").offsetHeight, 10);\r\t\t");
            script.Append("gPopupContainer.style.width = width + \"px\";\r\t\t");
            script.Append("gPopupContainer.style.height = (height+titleBarHeight) + \"px\";\r\r\t\t");
            script.Append("// need to set the width of the iframe to the title bar width because of the dropshadow\r\t\t");
            script.Append("// some oddness was occuring and causing the frame to poke outside the border in IE6\r\t\t");
            script.Append("gPopFrame.style.width = parseInt(document.getElementById(\"popupTitleBar\").offsetWidth, 10) + \"px\";\r\t\t");
            script.Append("gPopFrame.style.height = (height) + \"px\";\r\r\t\t");
            script.Append("// set the url\r\t\t");
            script.Append("gPopFrame.src = url;\r\r\t\t");
            script.Append("// for IE\r\t\t");
            script.Append("if (gHideSelects == true)\r\t\t\t");
            script.Append("hideSelectBoxes();\r\t\t");
            script.Append("window.setTimeout(\"setPopTitle();\", 600);\r\t");
            script.Append("}\r\r\t");
            #endregion

            #region === Função CenterPopWin ===
            script.Append("var gi = 0;\r\t");
            script.Append("function centerPopWin(width, height) \r\t{\r\t\t");
            script.Append("if (gPopupIsShown == true) \r\t\t{\r\t\t\t");
            script.Append("if (width == null || isNaN(width)) \r\t\t\t\t");
            script.Append("width = gPopupContainer.offsetWidth;\r\r\t\t\t");
            script.Append("if (height == null) \r\t\t\t\t");
            script.Append("height = gPopupContainer.offsetHeight;\r\r\t\t\t");
            script.Append("var fullHeight = getViewportHeight();\r\t\t\t");
            script.Append("var fullWidth = getViewportWidth();\r\t\t\t");
            script.Append("var theBody = document.documentElement;\r\t\t\t");
            script.Append("var scTop = parseInt(theBody.scrollTop,10);\r\t\t\t");
            script.Append("var scLeft = parseInt(theBody.scrollLeft,10);\r\r\t\t\t");
            script.Append("gPopupMask.style.height = fullHeight + \"px\";\r\t\t\t");
            script.Append("gPopupMask.style.width = fullWidth + \"px\";\r\t\t\t");
            script.Append("gPopupMask.style.top = scTop + \"px\";\r\t\t\t");
            script.Append("gPopupMask.style.left = scLeft + \"px\";\r\t\t\t");
            script.Append("var titleBarHeight = parseInt(document.getElementById(\"popupTitleBar\").offsetHeight, 10);\r\t\t\t");
            script.Append("gPopupContainer.style.top = (scTop + ((fullHeight - (height+titleBarHeight)) / 2)) + \"px\";\r\t\t\t");
            script.Append("gPopupContainer.style.left =  (scLeft + ((fullWidth - width) / 2)) + \"px\";\r\t\t");
            script.Append("}\r\t");
            script.Append("}\r\t");
            script.Append("addEvent(window, \"resize\", centerPopWin);\r\t");
            script.Append("window.onscroll = centerPopWin;\r\r\t");
            #endregion

            #region === Função HidePopWin ===
            script.Append("/**\r\t");
            script.Append("* @argument callReturnFunc - bool - determines if we call the return function specified\r\t");
            script.Append("* @argument returnVal - anything - return value\r\t");
            script.Append("*/\r\t");
            script.Append("function hidePopWin() \r\t{\r\t\t");
            script.Append("gPopupIsShown = false;\r\t\t");
            script.Append("restoreTabIndexes();\r\t\t");
            script.Append("if (gPopupMask == null)\r\t\t\t");
            script.Append("return\r\t\t");
            script.Append("gPopupMask.style.display = \"none\";\r\t\t");
            script.Append("gPopupContainer.style.display = \"none\";\r\t\t");
            script.Append("//display all select boxes\r\t\t");
            script.Append("if (gHideSelects == true)\r\t\t\t");
            script.Append("displaySelectBoxes();\r\t\t");
            script.Append("window.top.focus();\r\t\t");
            script.Append("FazerSubmit();\r\t}\r\r\t");
            #endregion

            #region === Função SetPopTitle ===
            script.Append("/**\r\t");
            script.Append("* Sets the popup title based on the title of the html document it contains.\r\t");
            script.Append("* Uses a timeout to keep checking until the title is valid.\r\t");
            script.Append("*/\r\t");
            script.Append("function setPopTitle()\r\t{\r\t\t");
            script.Append("if (window.frames[\"popupFrame\"].document.title == null)\r\t\t\t");
            script.Append("window.setTimeout(\"setPopTitle();\", 10);\r\t\t");
            script.Append("else\r\t\t\t");
            script.Append("document.getElementById(\"popupTitle\").innerHTML = window.frames[\"popupFrame\"].document.title;\r\t}\r\r\t");
            #endregion

            #region === Função KeyDownHandler ===
            script.Append("// Tab key trap. iff popup is shown and key was [TAB], suppress it.\r\t");
            script.Append("// @argument e - event - keyboard event that caused this function to be called.\r\t");
            script.Append("function keyDownHandler(e) {\r\t\t");
            script.Append("if (gPopupIsShown && e.keyCode == 9)  return false;\r\t}\r\r\t");
            #endregion

            #region === Função DisableTabIndexes ===
            script.Append("// For IE.  Go through predefined tags and disable tabbing into them.\r\t");
            script.Append("function disableTabIndexes()\r\t{\r\t\t");
            script.Append("if (document.all)\r\t\t{\r\t\t\t");
            script.Append("var i = 0;\r\t\t\t");
            script.Append("for (var j = 0; j < gTabbableTags.length; j++)\r\t\t\t{\r\t\t\t\t");
            script.Append("var tagElements = document.getElementsByTagName(gTabbableTags[j]);\r\t\t\t\t");
            script.Append("for (var k = 0 ; k < tagElements.length; k++)\r\t\t\t\t{\r\t\t\t\t\t");
            script.Append("gTabIndexes[i] = tagElements[k].tabIndex;\r\t\t\t\t\t");
            script.Append("tagElements[k].tabIndex=\"-1\";\r\t\t\t\t\t");
            script.Append("i++;\r\t\t\t\t}\r\t\t\t}\r\t\t}\r\t}\r\r\t");
            #endregion

            #region === Função RestoreTabIndexes ===
            script.Append("//For IE. Restore tab-indexes.\r\t");
            script.Append("function restoreTabIndexes()\r\t{\r\t\t");
            script.Append("if (document.all)\r\t\t{\r\t\t\t");
            script.Append("var i = 0;\r\t\t\t");
            script.Append("for (var j = 0; j < gTabbableTags.length; j++) \r\t\t\t{\r\t\t\t\t");
            script.Append("var tagElements = document.getElementsByTagName(gTabbableTags[j]);\r\t\t\t\t");
            script.Append("for (var k = 0 ; k < tagElements.length; k++) \r\t\t\t\t{\r\t\t\t\t\t");
            script.Append("tagElements[k].tabIndex = gTabIndexes[i];\r\t\t\t\t\t");
            script.Append("tagElements[k].tabEnabled = true;\r\t\t\t\t\t");
            script.Append("i++;\r\t\t\t\t}\r\t\t\t}\r\t\t}\r\t}\r\r\t");
            #endregion

            #region === Função HideSelectBoxes ===
            script.Append("/**\r\t");
            script.Append("* Hides all drop down form select boxes on the screen so they do not appear above the mask layer.\r\t");
            script.Append("* IE has a problem with wanted select form tags to always be the topmost z-index or layer\r\t");
            script.Append("*\r\t");
            script.Append("* Thanks for the code Scott!\r\t");
            script.Append("*/\r\t");
            script.Append("function hideSelectBoxes()\r\t{\r\t\t");
            script.Append("for(var i = 0; i < document.forms.length; i++) \r\t\t{\r\t\t\t");
            script.Append("for(var e = 0; e < document.forms[i].length; e++)\r\t\t\t{\r\t\t\t\t");
            script.Append("if(document.forms[i].elements[e].tagName == \"SELECT\")\r\t\t\t\t\t");
            script.Append("document.forms[i].elements[e].style.visibility=\"hidden\";\r\t\t\t");
            script.Append("}\r\t\t}\r\t}\r\r\t");
            #endregion

            #region === Função displaySelectBoxes ===
            script.Append("/**\r\t");
            script.Append("* Makes all drop down form select boxes on the screen visible so they do not reappear after the dialog is closed.\r\t");
            script.Append("* IE has a problem with wanted select form tags to always be the topmost z-index or layer\r\t");
            script.Append("*/\r\t");
            script.Append("function displaySelectBoxes()\r\t{\r\t\t");
            script.Append("for(var i = 0; i < document.forms.length; i++) \r\t\t{\r\t\t\t");
            script.Append("for(var e = 0; e < document.forms[i].length; e++)\r\t\t\t{\r\t\t\t\t");
            script.Append("if(document.forms[i].elements[e].tagName == \"SELECT\")\r\t\t\t\t\t");
            script.Append("document.forms[i].elements[e].style.visibility=\"visible\";\r\t\t\t");
            script.Append("}\r\t\t}\r\t}\r\r");
            #endregion

            return script.ToString();

        }
        private string ScriptCommon()
        {
            System.Text.StringBuilder script = new System.Text.StringBuilder();
            //script.Append("\r<script language=\"javascript\">\r\t");
            script.Append("function addEvent(obj, evType, fn) \r\t{\r\t\t");
            script.Append("if (obj.addEventListener)\r\t\t{\r\t\t\t");
            script.Append("obj.addEventListener(evType, fn, true);\r\t\t\t");
            script.Append("return true;\r\t\t");
            script.Append("}\r\t\telse if (obj.attachEvent)\r\t\t{\r\t\t\t");
            script.Append("var r = obj.attachEvent(\"on\"+evType, fn);\r\t\t\t");
            script.Append("return r;\r\t\t");
            script.Append("}\r\t\telse\r\t\t\t");
            script.Append("return false;\r\t");
            script.Append("}\r\r\t");

            script.Append("function removeEvent(obj, evType, fn, useCapture)\r\t{\r\t\t");
            script.Append("if (obj.removeEventListener)\r\t\t{\r\t\t\t");
            script.Append("obj.removeEventListener(evType, fn, useCapture);\r\t\t\t");
            script.Append("return true;\r\t\t");
            script.Append("}\r\t\t");
            script.Append("else if (obj.detachEvent)\r\t\t{\r\t\t\t");
            script.Append("var r = obj.detachEvent(\"on\"+evType, fn);\r\t\t\t");
            script.Append("return r;\r\t\t");
            script.Append("}\r\t\t");
            script.Append("else\r\t\t\t");
            script.Append("alert('Handler could not be removed');\r\t");
            script.Append("}\r\r\t");

            script.Append("function getViewportHeight() \r\t{\r\t\t");
            script.Append("if (window.innerHeight!=window.undefined) return window.innerHeight;\r\t\t");
            script.Append("if (document.compatMode=='CSS1Compat') return document.documentElement.clientHeight;\r\t\t");
            script.Append("if (document.body) return document.body.clientHeight; \r\t\t");
            script.Append("return window.undefined;\r\t");
            script.Append("}\r\r\t");

            script.Append("function getViewportWidth()\r\t{\r\t\t");
            script.Append("if (window.innerWidth!=window.undefined) return window.innerWidth;\r\t\t");
            script.Append("if (document.compatMode=='CSS1Compat') return document.documentElement.clientWidth;\r\t\t");
            script.Append("if (document.body) return document.body.clientWidth;\r\t\t");
            script.Append("return window.undefined;\r\t");
            script.Append("}\r\t");
            script.Append("\r");
            //script.Append("</script>");
            return script.ToString();
        }
        private string StyleShowModal()
        {
            System.Text.StringBuilder style = new System.Text.StringBuilder();
            //style.Append("\r<style type=\"text/css\">\r\t");

            #region *** #popupMask ***
            style.Append("#popupMask {\r\t");
            style.Append("position: absolute;\r\t\t");
            style.Append("z-index: 200;\r\t\t");
            style.Append("top: 0px;\r\t\t");
            style.Append("left: 0px;\r\t\t");
            style.Append("width: 100%;\r\t\t");
            style.Append("height: 100%;\r\t\t");
            style.Append("opacity: .4;\r\t\t");
            style.Append("filter: alpha(opacity=40);\r\t\t");
            style.Append("background-color: transparent !important;\r\t\t");
            style.Append("background-color: #333333;\r\t\t");
            style.Append("background-image: url(../imagens/maskBG.png) !important; /* For browsers Moz, Opera, etc.*/\r\t\t");
            style.Append("background-image: none;\r\t\t");
            style.Append("background-repeat: repeat;\r\t\t");
            style.Append("display: none;\r\t");
            style.Append("}\r\t");
            #endregion

            #region *** #popupContainer ***
            style.Append("#popupContainer {\r\t\t");
            style.Append("padding-right: 0px;;\r\t");
            style.Append("display: none;;\r\t");
            style.Append("padding-left: 0px;;\r\t");
            style.Append("z-index: 201;;\r\t");
            style.Append("left: 0px;;\r\t");
            style.Append("padding-bottom: 0px;;\r\t");
            style.Append("padding-top: 0px;;\r\t");
            style.Append("position: absolute;;\r\t");
            style.Append("top: 0px;;\r\t");
            style.Append("background-color: #ffffff;\r\t");
            style.Append("border-right: #000000 1px solid;\r\t");
            style.Append("border-top: #000000 0px solid;\r\t");
            style.Append("border-left: #000000 1px solid;\r\t");
            style.Append("border-bottom: #000000 1px solid;\r\t");
            style.Append("}\r\t");
            #endregion

            #region *** #popupInner ***
            style.Append("#popupInner {\r\t\t");
            style.Append("border: 1px solid #000000;\r\t\t");
            style.Append("background-color: #ffffff;\r\t");
            style.Append("}\r\t");
            #endregion

            #region *** #popupFrame ***
            style.Append("#popupFrame {\r\t\t");
            style.Append("margin: 0px;\r\t\t");
            style.Append("width: 100%;\r\t\t");
            style.Append("height: 100%;\r\t\t");
            style.Append("position: relative;\r\t\t");
            style.Append("z-index: 202;\r\t");
            style.Append("}\r\t");
            #endregion

            #region *** #popupTitleBar ***
            style.Append("#popupTitleBar {\r\t\t");
            style.Append("background-color: #486CAE;\r\t\t");
            style.Append("color: #ffffff;\r\t\t");
            style.Append("font-weight: bold;\r\t\t");
            style.Append("height: 15px;\r\t\t");
            style.Append("padding: 3px;\r\t\t");
            style.Append("border-bottom: 1px solid #000000;\r\t\t");
            style.Append("border-top: 1px solid #78A3F2;\r\t\t");
            style.Append("border-left: 1px solid #78A3F2;\r\t\t");
            style.Append("border-right: 1px solid #204095;\r\t\t");
            style.Append("position: relative; \r\t\t");
            style.Append("z-index: 203;\r\t");
            style.Append("}\r\t");
            #endregion

            #region *** #popupTitle ***
            style.Append("#popupTitle {\r\t\t");
            style.Append("float:left;\r\t\t");
            style.Append("font-size: 0.7em;\r\t\t");
            style.Append("font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;\r\t");
            style.Append("}\r\t");
            #endregion

            #region *** #popupControls ***
            style.Append("#popupControls {\r\t\t");
            style.Append("float: right;\r\t\t");
            style.Append("cursor: pointer;\r\t\t");
            style.Append("cursor: hand;\r\t");
            style.Append("}\r");
            #endregion

            #region *** #popupFechar ***
            style.Append("#popupFechar {\r\t\t");
            style.Append("FONT-WEIGHT: bold;\r\t\t");
            style.Append("FONT-SIZE: 6pt;\r\t\t");
            style.Append("BORDER-LEFT-COLOR: firebrick;\r\t\t");
            style.Append("BORDER-BOTTOM-COLOR: darkred;\r\t\t");
            style.Append("WIDTH: 15px;\r\t\t");
            style.Append("CURSOR: hand;\r\t\t");
            style.Append("COLOR: white;\r\t\t");
            style.Append("BORDER-TOP-STYLE: outset;\r\t\t");
            style.Append("BORDER-TOP-COLOR: firebrick;\r\t\t");
            style.Append("FONT-FAMILY: Verdana, Tahoma, Arial, Helvetica, sans-serif;\r\t\t");
            style.Append("BORDER-RIGHT-STYLE: outset;\r\t\t");
            style.Append("BORDER-LEFT-STYLE: outset;\r\t\t");
            style.Append("HEIGHT: 15px;\r\t\t");
            style.Append("BACKGROUND-COLOR: darkred;\r\t\t");
            style.Append("TEXT-ALIGN: center;\r\t\t");
            style.Append("TEXT-DECORATION: none;\r\t\t");
            style.Append("BORDER-RIGHT-COLOR: darkred;\r\t\t");
            style.Append("BORDER-BOTTOM-STYLE: outset;\r\t");
            style.Append("}\r\t");
            #endregion

            //style.Append("</style>\r");
            return style.ToString();
        }
        private string DivShowModal()
        {
            System.Text.StringBuilder htmlTxtWrite = new System.Text.StringBuilder();
            htmlTxtWrite.Append("\r<!-- Div do ShowModal -->\r");
            htmlTxtWrite.Append("<div id=\"popupMask\" >&nbsp;</div>\r");
            htmlTxtWrite.Append("\t<div id=\"popupContainer\">\r");
            htmlTxtWrite.Append("\t\t<div id=\"popupInner\">\r");
            htmlTxtWrite.Append("\t\t\t<div id=\"popupTitleBar\">\r");
            htmlTxtWrite.Append("\t\t\t\t<div id=\"popupTitle\"></div>\r");
            htmlTxtWrite.Append("\t\t\t\t<div id=\"popupControls\">\r");
            htmlTxtWrite.Append("\t\t\t\t\t<INPUT type=\"button\" id=\"popupFechar\" value=\"X\"  onclick=\"hidePopWin();\" />\r");
            htmlTxtWrite.Append("\t\t\t\t</div>\r");
            htmlTxtWrite.Append("\t\t\t</div>\r");
            htmlTxtWrite.Append("\t\t</div>\r");
            htmlTxtWrite.Append("\t\t<iframe style=\"width:100%;height:100%;background-color:transparent;overflow:auto\"\r");
            //src=\"loading.html\" 
            htmlTxtWrite.Append("\t\t\tframeborder=\"0\" allowtransparency=\"true\" id=\"popupFrame\" name=\"popupFrame\">\r");
            htmlTxtWrite.Append("\t\t</iframe>\r");
            htmlTxtWrite.Append("\t</div>\r");
            htmlTxtWrite.Append("</div>\r");
            return htmlTxtWrite.ToString();

        }

        /// <summary>
        /// Cria os arquivos de ScriptCommon, ScriptShowModal e StyleShowModal.
        /// </summary>
        private void CriarArquivos(string pNomeArquivo, string pNomePasta)
        {
            //Verifica a Existencia do arquivo.
            if (!System.IO.File.Exists(this.MapPathSecure(pNomePasta) + "\\" + pNomeArquivo))
            {
                if (!System.IO.Directory.Exists(this.MapPathSecure(pNomePasta)))
                    System.IO.Directory.CreateDirectory(this.MapPathSecure(pNomePasta));
                System.IO.StreamWriter mGravaArquivo = System.IO.File.CreateText(this.MapPathSecure(pNomePasta) + "\\" + pNomeArquivo);
                switch (pNomeArquivo)
                {
                    case "ScriptCommonModal.js":
                        mGravaArquivo.Write(this.ScriptCommon());
                        break;
                    case "ScriptShowModal.js":
                        mGravaArquivo.Write(this.ScriptShowModal());
                        break;
                    case "StyleShowModal.css":
                        mGravaArquivo.Write(this.StyleShowModal());
                        break;
                }
                mGravaArquivo.Close();
            }
        }

        /// <summary>
        /// Registra na Página os Scripts passados por parametro.
        /// </summary>
        /// <param name="pScript">JavaScript que será registrado na página.</param>
        private string RegistraScript(string pScript)
        {
            System.Text.StringBuilder script = new System.Text.StringBuilder();
            script.Append("\r<script language=\"javascript\">\r\t");
            script.Append(pScript);
            script.Append("</script>");
            return script.ToString();

        }
        /// <summary>
        /// Registro StyleSheets na página.
        /// </summary>
        /// <param name="pStyle">StyleSheet que será registrado na página.</param>
        private string RegistraStyle(string pStyle)
        {
            System.Text.StringBuilder style = new System.Text.StringBuilder();
            style.Append("\r<style type=\"text/css\">\r\t");
            style.Append(pStyle);
            style.Append("</style>\r");
            return style.ToString();
        }
        /// <summary>
        /// Render this control to the output parameter specified.
        /// </summary>
        /// <param name="output"> The HTML writer to write out to </param>
        protected override void Render(HtmlTextWriter output)
        {
            if (this.ModoDesign(this))
            {
                output.Write(this.ID);
            }
            else if (this.aTipoAcao != TipoAcao.Nenhuma)
            {
                output.RenderBeginTag(HtmlTextWriterTag.Script);
                switch (this.aTipoAcao)
                {
                    case TipoAcao.ExecutarSubmit:
                        output.WriteLine("aTipoAcao='" + TipoAcao.ExecutarSubmit.ToString() + "';");
                        break;
                    case TipoAcao.ModalShow:
                        output.WriteLine("aTipoAcao='" + TipoAcao.ModalShow.ToString() + "';");
                        output.Write("aJanelaAspx='" + this.aPaginaAspx + "';");
                        break;
                    case TipoAcao.ModalClose:
                        output.WriteLine("aTipoAcao='" + TipoAcao.ModalClose.ToString() + "';");
                        break;
                    case TipoAcao.MsgAlert:
                        output.WriteLine("aTipoAcao='" + TipoAcao.MsgAlert.ToString() + "';");
                        output.Write("aMessage='" + this.aMessage + "';");
                        break;
                    case TipoAcao.MsgAlertMoverFoco:
                        output.WriteLine("aTipoAcao='" + TipoAcao.MsgAlertMoverFoco.ToString() + "';");
                        output.WriteLine("aMessage='" + this.aMessage + "';");
                        output.Write("aCampoRecebeFoco='" + this.aCampoRecebeFoco + "';");
                        break;
                    case TipoAcao.MsgAlertModalClose:
                        output.WriteLine("aTipoAcao='" + TipoAcao.MsgAlertModalClose.ToString() + "';");
                        output.WriteLine("aMessage='" + this.aMessage + "';");
                        break;
                    case TipoAcao.MsgConfirm:
                        output.WriteLine("aTipoAcao='" + TipoAcao.MsgConfirm.ToString() + "';");
                        output.Write("aMessage='" + this.aMessage + "';");
                        break;
                    case TipoAcao.MoverFoco:
                        output.WriteLine("aTipoAcao='" + TipoAcao.MoverFoco.ToString() + "';");
                        output.Write("aCampoRecebeFoco='" + this.aCampoRecebeFoco + "';");
                        break;
                    default:
                        output.Write("aTipoAcao='" + TipoAcao.Nenhuma.ToString() + "';");
                        break;
                }
                output.RenderEndTag();
                output.WriteLine("");
            }
        }
        // TODO: Populate Comments.
        /// <summary>OnPreRender Function</summary>
        /// <remarks></remarks>
        /// <param name='e'></param>
        protected override void OnPreRender(EventArgs e)
        {
            if (this.aUsaModal && !base.Page.IsClientScriptBlockRegistered("DivShowModal"))
            {
                if (this.aGerarArquivoScript)
                {
                    try
                    {
                        this.CriarArquivos("ScriptCommonModal.js", this.aPastaScripts);
                        this.CriarArquivos("ScriptShowModal.js", this.aPastaScripts);
                        this.CriarArquivos("StyleShowModal.css", this.aPastaStyles);

                        this.Page.RegisterClientScriptBlock("StyleShowModal", "<link href=\"" + this.aPastaStyles + "/StyleShowModal.css\" type=\"text/css\" rel=\"stylesheet\">");
                        this.Page.RegisterClientScriptBlock("ScriptCommonModal", "<script src=\"" + this.aPastaScripts + "/ScriptCommonModal.js\" type=\"text/javascript\"></script>");
                        this.Page.RegisterClientScriptBlock("ScriptShowModal", "<script src=\"" + this.aPastaScripts + "/ScriptShowModal.js\" type=\"text/javascript\"></script>");
                        this.Page.RegisterClientScriptBlock("DivShowModal", this.DivShowModal());
                    }
                    catch (Exception ex)
                    {
                        System.Text.StringBuilder mErro = new System.Text.StringBuilder();
                        mErro.Append("O usuário AspNet não tem permissão para gravar os arquivos ");
                        mErro.Append("de Script na pasta da aplicação, isso pode ocorrer por não ");
                        mErro.Append("ter sido definido uma permissão para o usuário AspNet na ");
                        mErro.Append("pasta base da aplicação ou pela inexistência do parametro ");
                        mErro.Append("<identity impersonate=\"true\" /> na tag <system.web> do ");
                        mErro.Append("arquivo Web.config ou as duas opções.\\n");
                        mErro.Append("Corrige-se o problema ou define a propriedade ");
                        mErro.Append("GerarArquivoScript do componente MessageBox para false, oque ");
                        mErro.Append("Resultara na composição dos Scripts junto com o HTML.\\n\\n");
                        mErro.Append("Mensagem Original:\\n");
                        mErro.Append(ex.Message.Replace(@"\", @"\\"));
                        this.Show(mErro.ToString());
                    }

                }
                else
                {
                    this.Page.RegisterClientScriptBlock("StyleShowModal", this.RegistraStyle(this.StyleShowModal()));
                    this.Page.RegisterClientScriptBlock("DivShowModal", this.DivShowModal());
                    this.Page.RegisterClientScriptBlock("ScriptCommonModal", this.RegistraScript(this.ScriptCommon()));
                    this.Page.RegisterClientScriptBlock("ScriptShowModal", this.RegistraScript(this.ScriptShowModal()));
                }

            }
            if (!base.Page.IsClientScriptBlockRegistered("ScriptAcao"))
                this.Page.RegisterClientScriptBlock("ScriptAcao", ScriptAcao());
            if (this.aManterScroll)
                this.Page.RegisterHiddenField("__SCROLLPOS", "0");
        }

        [Anthem.Method]
        public void OnCloseModal(string pKey, string pValorRetorno)
        {
            if (CloseModalChoosed != null)
            {
                CloseModalChoosed(this, pKey, pValorRetorno);
            }
        }

        [Anthem.Method]
        public void OnYes(string pKey)
        {
            if (YesChoosed != null)
            {
                YesChoosed(this, pKey);
            }
        }

        [Anthem.Method]
        public void OnNo(string pKey)
        {
            if (NoChoosed != null)
            {
                NoChoosed(this, pKey);
            }
        }


        #region Enumerações
        private enum TipoAcao
        {
            Nenhuma = 0,
            MsgAlert = 1,
            MsgAlertMoverFoco = 2,
            MsgAlertModalClose = 3,
            MsgConfirm = 4,
            MoverFoco = 5,
            ModalShow = 6,
            ModalShowRetorno = 7,
            ModalClose = 8,
            ExecutarSubmit = 9
        }
        // TODO: Populate Comments.
        /// <summary>enTipoSubmit Enum</summary>
        /// <remarks></remarks>
        public enum enTipoSubmit
        {
            // TODO: Populate Comments.
            /// <summary>Nenhum Variable</summary>
            /// <remarks></remarks>
            Nenhum = 0,
            // TODO: Populate Comments.
            /// <summary>Form Variable</summary>
            /// <remarks></remarks>
            Form = 1,
            // TODO: Populate Comments.
            /// <summary>Botao Variable</summary>
            /// <remarks></remarks>
            Botao = 3,
            /// <summary>
            /// Excuta função AnthemCallBack
            /// </summary>
            AnthemCallBack = 4

        }

        #endregion

        #region IPostBackEventHandler Members
        // TODO: Populate Comments.
        /// <summary>RaisePostBackEvent Function</summary>
        /// <remarks></remarks>
        /// <param name='eventArgument'></param>
        public void RaisePostBackEvent(string eventArgument)
        {
            switch (eventArgument.Substring(0, 3))
            {
                case "Yes":
                    OnYes(eventArgument.Substring(3));
                    break;
                case "No_":
                    OnNo(eventArgument.Substring(3));
                    break;
                case "Clo":
                    string[] mArrayParam = eventArgument.Split('#');
                    OnCloseModal(mArrayParam[1], mArrayParam[2]);
                    break;
            }
        }

        #endregion

        #region ICallBackControl implementation

        [DefaultValue("")]
        public virtual string CallBackCancelledFunction
        {
            get
            {
                if (null == ViewState["CallBackCancelledFunction"])
                    return string.Empty;
                else
                    return (string)ViewState["CallBackCancelledFunction"];
            }
            set { ViewState["CallBackCancelledFunction"] = value; }
        }

        [DefaultValue(true)]
        public virtual bool EnableCallBack
        {
            get
            {
                if (ViewState["EnableCallBack"] == null)
                    return true;
                else
                    return (bool)ViewState["EnableCallBack"];
            }
            set
            {
                ViewState["EnableCallBack"] = value;
            }
        }

        [DefaultValue(true)]
        public virtual bool EnabledDuringCallBack
        {
            get
            {
                if (null == ViewState["EnabledDuringCallBack"])
                    return true;
                else
                    return (bool)ViewState["EnabledDuringCallBack"];
            }
            set { ViewState["EnabledDuringCallBack"] = value; }
        }

        [DefaultValue("")]
        public virtual string PostCallBackFunction
        {
            get
            {
                if (null == ViewState["PostCallBackFunction"])
                    return string.Empty;
                else
                    return (string)ViewState["PostCallBackFunction"];
            }
            set { ViewState["PostCallBackFunction"] = value; }
        }

        [DefaultValue("")]
        public virtual string PreCallBackFunction
        {
            get
            {
                if (null == ViewState["PreCallBackFunction"])
                    return string.Empty;
                else
                    return (string)ViewState["PreCallBackFunction"];
            }
            set { ViewState["PreCallBackFunction"] = value; }
        }

        [DefaultValue("")]
        public virtual string TextDuringCallBack
        {
            get
            {
                if (null == ViewState["TextDuringCallBack"])
                    return string.Empty;
                else
                    return (string)ViewState["TextDuringCallBack"];
            }
            set { ViewState["TextDuringCallBack"] = value; }
        }

        #endregion

        #region IUpdatableControl implementation

        [DefaultValue(false)]
        public virtual bool AutoUpdateAfterCallBack
        {
            get
            {
                if (ViewState["AutoUpdateAfterCallBack"] == null)
                    return false;
                else
                    return (bool)ViewState["AutoUpdateAfterCallBack"];
            }
            set
            {
                if (value) UpdateAfterCallBack = true;
                ViewState["AutoUpdateAfterCallBack"] = value;
            }
        }

        private bool _updateAfterCallBack = false;

        [Browsable(false), DefaultValue(false)]
        public virtual bool UpdateAfterCallBack
        {
            get { return _updateAfterCallBack; }
            set { _updateAfterCallBack = value; }
        }

        [
        Category("Misc"),
        Description("Fires before the control is rendered with updated values.")
        ]
        public event EventHandler PreUpdate
        {
            add { Events.AddHandler(EventPreUpdateKey, value); }
            remove { Events.RemoveHandler(EventPreUpdateKey, value); }
        }
        private static readonly object EventPreUpdateKey = new object();

        public virtual void OnPreUpdate()
        {
            EventHandler EditHandler = (EventHandler)Events[EventPreUpdateKey];
            if (EditHandler != null)
                EditHandler(this, EventArgs.Empty);
        }

        #endregion

        #region Metodos Privados
        private string GetScriptCallBack(string pEvento)
        {
            string mScript = "";
            switch (pEvento)
            {
                case "Yes":
                    mScript = string.Format("Anthem_InvokeControlMethod('{0}', 'OnYes', ['{1}'], function() {{ }}, null)", this.ClientID, "");

                    break;
                case "No":
                    break;
                case "CloseModal":
                    break;

            }
            return mScript;
        }

        #endregion
    }
}

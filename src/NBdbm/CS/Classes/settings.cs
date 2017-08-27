using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;
using System.Configuration;
using System.Xml;

//Codigo fonte: Edgar Francis
//Incío: 01/03/2004
//última Alteração:
//mailto:edgar@yap.com.br
//http://www.yap.com.br
//file:\\stone\C\Pastas%20de%20Trabalho\Edgar\vss%20Working%20Folder\PB2000\CONTROLE_app\Bibliotecas\NBdbm\Classes\AdmDB\



namespace NBdbm
{
    internal class settings : IDisposable
    {



        #region   Start   //Variáveis, new, dispose, finalize

        private AssemblyInfo AblyI = new AssemblyInfo();
        private AppSettings xMnSetAll;
        private ADM.stringConnection pvt_stringConnection;
        private tipos.Retorno pvt_Credencial;
        private string pvt_tipoBanco;
        private string pvt_tipoConexao;
        private string pvt_sintaxeData;
        private string pvt_sintaxePontoDecimal;

        public settings()
        {
            xMnSetAll = new AppSettings(AblyI.Config_Arquivo, "configuration");
        }

        public void Dispose()
        {
            if (xMnSetAll != null)
            {
                xMnSetAll.Dispose();
            }
            if (pvt_stringConnection != null)
            {
                pvt_stringConnection.Dispose();
            }
            if (pvt_Credencial != null)
            {
                pvt_Credencial.Dispose();
            }

            AblyI = null;
            xMnSetAll = null;
            pvt_stringConnection = null;
            pvt_Credencial = null;
            pvt_tipoBanco = null;
            pvt_tipoConexao = null;
            pvt_sintaxeData = null;
            pvt_sintaxePontoDecimal = null;

        } //settings

        ~settings()
        {
            base.Finalize();
            Dispose();
        } //settings
        #endregion

        #region   Code Base   //sub and function privates

        internal void save(string Xml)
        {
            xMnSetAll.Save();
        }

        #endregion

        #region   Code Property's & Metodos

        internal tipos.Retorno Credencial
        {
            get
            {
                return pvt_Credencial;
            }
            set
            {
                pvt_Credencial = value;
            }
        }


        internal string BaseDirectory
        {
            get
            {
                //Return AppDomain.CurrentDomain.BaseDirectory()
                return AblyI.BaseDirectory;
            }
        }

        internal string appExeName
        {
            get
            {
                return System.Reflection.AssemblyName.GetAssemblyName("").Name;
            }
        }
        internal string tipoConexao
        {
            get
            {
                if (pvt_tipoConexao == null)
                {
                    pvt_tipoConexao = xMnSetAll.Item("appSettings", "tipoconexao", "SQLSERVER").Value;
                }
                return pvt_tipoConexao;
            }
            set
            {
                pvt_tipoConexao = value;
                //xMnSetAll.Update("appSettings", "tipoconexao", newTipoConexao)
                if (pvt_stringConnection != null)
                {
                    pvt_stringConnection.Dirty = true;
                }
            }
        }

        internal string tipoBanco(string atributo)
        {
            if (pvt_tipoConexao == null)
            {
                pvt_tipoConexao = xMnSetAll.Item("appSettings", "tipobanco", "SQLSERVER").Value;
            }
            return pvt_tipoBanco;
        }
        internal void SettipoBanco(string atributo, string value)
        {
            pvt_tipoBanco = value;
            //xMnSetAll.Update("appSettings", "tipobanco", value)
        }

        internal string sintaxeData(string atributo)
        {
            if (pvt_sintaxeData == null)
            {
                pvt_sintaxeData = xMnSetAll.Item("appSettings", "sintaxeData", "MM/dd/yyyy").Value;
            }
            return pvt_sintaxeData;
        }
        internal void SetsintaxeData(string atributo, string value)
        {
            pvt_sintaxeData = value;
        }

        internal string sintaxePontoDecimal(string atributo)
        {
            if (pvt_sintaxePontoDecimal == null)
            {
                pvt_sintaxePontoDecimal = xMnSetAll.Item("appSettings", "sintaxePontoDecimal", ".").Value;
            }
            return pvt_sintaxePontoDecimal;
        }
        internal void SetsintaxePontoDecimal(string atributo, string value)
        {
            pvt_sintaxePontoDecimal = value;
        }

        internal string stringConnection
        {
            get
            {
                if (pvt_stringConnection == null)
                {
                    pvt_stringConnection = new ADM.stringConnection();
                    pvt_stringConnection.Dirty = true;
                }
                if (pvt_stringConnection.Dirty == true)
                {
                    this.xMnSetAll.Item(this.tipoConexao, pvt_stringConnection);

                    //pvt_stringConnection.ApplicationName = xMnSetAll.Item(Me.tipoConexao, "aplicationName", "NeobridgeSistemas").Value
                    //pvt_stringConnection.AttachDBFilename = xMnSetAll.Item(Me.tipoConexao, "AttachDBFilename", "c:\dbs\Neobridge.mdf").Value
                    //pvt_stringConnection.ConnectionLifetime = xMnSetAll.Item(Me.tipoConexao, "ConnectionLifetime", 120).Value
                    //pvt_stringConnection.ConnectionReset = xMnSetAll.Item(Me.tipoConexao, "ConnectionReset", 200).Value
                    //pvt_stringConnection.ConnectTimeout = xMnSetAll.Item(Me.tipoConexao, "ConnectTimeout", 20).Value
                    //pvt_stringConnection.CurrentLanguage = xMnSetAll.Item(Me.tipoConexao, "CurrentLanguage", "").Value
                    //pvt_stringConnection.DataSource = xMnSetAll.Item(Me.tipoConexao, "DataSource", "127.0.0.1").Value
                    //pvt_stringConnection.DSN = xMnSetAll.Item(Me.tipoConexao, "DSN", "Neobridge").Value()
                    //pvt_stringConnection.SystemDB = xMnSetAll.Item(Me.tipoConexao, "SystemDB", "x:\Bancos de Dados\Logo.bmp").Value()
                    //pvt_stringConnection.DataSourcePorta = xMnSetAll.Item(Me.tipoConexao, "DataSourcePorta", "1433").Value
                    //pvt_stringConnection.Encrypt = xMnSetAll.Item(Me.tipoConexao, "Encrypt", "false").Value
                    //pvt_stringConnection.Enlist = xMnSetAll.Item(Me.tipoConexao, "Enlist", "true").Value
                    //pvt_stringConnection.InitialCatalog = xMnSetAll.Item(Me.tipoConexao, "InitialCatalog", "Neobridge").Value
                    //pvt_stringConnection.IntegratedSecurity = xMnSetAll.Item(Me.tipoConexao, "IntegratedSecurity", "true").Value
                    //pvt_stringConnection.MaxPoolSize = xMnSetAll.Item(Me.tipoConexao, "MaxPoolSize", "100").Value
                    //pvt_stringConnection.MinPoolSize = xMnSetAll.Item(Me.tipoConexao, "MinPoolSize", "0").Value
                    //pvt_stringConnection.NetworkLibrary = xMnSetAll.Item(Me.tipoConexao, "NetworkLibrary", "dbmssocn").Value
                    //pvt_stringConnection.PacketSize = xMnSetAll.Item(Me.tipoConexao, "PacketSize", "8192").Value
                    //pvt_stringConnection.PersistSecurityInfo = xMnSetAll.Item(Me.tipoConexao, "PersistSecurityInfo", "true").Value()
                    //pvt_stringConnection.Pooling = xMnSetAll.Item(Me.tipoConexao, "Pooling", "true").Value()
                    //pvt_stringConnection.WorkstationID = xMnSetAll.Item(Me.tipoConexao, "WorkstationID", "stone").Value()
                    //pvt_stringConnection.UserID = xMnSetAll.Item(Me.tipoConexao, "UserId", "com login!").Value()
                    //pvt_stringConnection.Password = xMnSetAll.Item(Me.tipoConexao, "Password", "com senha!").Value()
                    //MsgBox("Deveria ter parado aqui!!! - stringConnection - Settings")
                    pvt_stringConnection.Dirty = false;
                    //Me.save()
                }
                return pvt_stringConnection.StringConnection;
            }
        }

        internal NBdbm.tipos.Versao Versao
        {
            get
            {
                NBdbm.tipos.Versao mVersao = new NBdbm.tipos.Versao();
                //Implementar ler do arquivo "ini"
                //Versao = "01.00.0001"
                mVersao.major = AblyI.Version.Major;
                mVersao.minor = AblyI.Version.Minor;
                mVersao.revision = AblyI.Version.Revision;
                //Call xMng.writeNodo("versao nbDbm", "Valor", AblyI.Version.ToString)
                //xMnSetAll.Update("appSettings", "versao nbdbm", AblyI.Version.ToString)
                return mVersao;
            }
            //Set(ByVal newVersao As NBdbm.tipos.Versao)
            //newVersao
            //Implementar salvar no arquivo "ini"
            //End Set
        }

        internal string UserId
        {
            get
            {
                string returnValue;
                returnValue = NBFuncoes.decripto("BZÏÍs·MÝ>¶").ToString();
                return returnValue;
            }
        }

        internal string Password
        {
            get
            {
                string returnValue;
                returnValue = NBFuncoes.decripto("=*MZÏ>ÙMÝ").ToString();
                return returnValue;
            }
        }

        #endregion

    }

    #region  Motor settings

    internal class AppSetting
    {

        // A variável mParent será uma instância
        // Que permitirá a atualização das
        // configurações no arquivo
        private AppSettings mParent;
        private string mstrKey;
        private string mstrValue;

        public string Key
        {
            get
            {
                return mstrKey;
            }
            set
            {
                this.UpdateParent();
                //Value = mstrKey
                mstrKey = value;
            }
        }

        public string Value
        {
            get
            {
                return mstrValue;
            }
            set
            {
                this.UpdateParent();
                mstrValue = value;
            }
        }

        private void UpdateParent()
        {
            if (this.mParent != null)
            {
                //Verificar como se comporta a falta do "Block"
                //Me.mParent.Update(String.Empty, Me)
            }
        }

        public AppSetting()
            : this(string.Empty, string.Empty)
        {
        }

        public AppSetting(string Key, string Value)
            : this(Key, Value, null)
        {
        }

        internal AppSetting(string Key, string Value, AppSettings Parent)
        {
            this.mstrKey = Key;
            this.mstrValue = Value;
            this.mParent = Parent;
        }
    }

    internal class AppSettings : IEnumerable, IDisposable
    {


        // This classes wraps access to the configuration//appSettings
        // section of the config file specified when an instance is created.
        // XPath expressions are used to find values when requested.
        // In addition, the class supports enumeration by implementing
        // IEnumerable and providing a private Iterator which implements
        // IEnumerator.

        private XmlDocument cfg = new XmlDocument();
        //Private xAS As XmlNode
        private string mstrFileName; //Nome do Arquivo de Configurações
        private bool mblnAutoSave; //Informa se é para salvar ao sair
        private bool mblnDirty; //
        private AppSetting[] maItems; //Itens da configuração
        private bool mblnDisposing; //Informa que está sendo disponibilizada
        private bool mblnDisposed; //Informa que foi disponibilizada

        //Private Const APPSETTINGS_ELEMENT As String = "configuration//appSettings"
        private const string APPSETTINGS_ELEMENT = "configuration";
        private string xmlBLOCK = "configuration//appSettings";
        private const string NEWELEMENT = "item";
        private const string XPATH_KEY_ADD = "//item";
        private const string XPATH_KEY_ADD_KEY = "//item[@key=\'{0}\']";

        public AppSettings(string ConfigFile, string block)
            : this(ConfigFile, false)
        {
        }

        public AppSettings(string ConfigFile, bool AutoSave, string block)
        {
            this.xmlBLOCK = block;
            if (ConfigFile.Length == 0)
            {
                throw (new ArgumentNullException("Você deve Informar um nome de arquivo \'fqn\' válido."));
            }
            else
            {
                if (System.IO.File.Exists(ConfigFile))
                {
                    try
                    {
                        cfg.Load(ConfigFile);
                    }
                    catch (Exception exp)
                    {
                        throw (new System.IO.FileLoadException("O arquivo informado não pode ser aberto. Por favor, veja mais informações em \'more information\'.", exp));
                    }
                    // Get the main appSettings element
                    // so we can add new settings
                    //xAS = cfg.SelectSingleNode(APPSETTINGS_ELEMENT)

                    //If xAS(APPSETTINGS_ELEMENT) Is Nothing Then
                    if (xAS(block) == null)
                    {
                        throw (new ConfigurationErrorsException("O arquivo especificado não possui um XML válido, este arquivo não contém uma configuração válida."));
                    }

                    // If we get this far we need to
                    // store the file name for any changes
                    mstrFileName = ConfigFile;

                    this.AutoSave = AutoSave;
                }
                else
                {
                    //Throw New System.IO.FileNotFoundException(String.Format("O arquivo especificado não existe.", ConfigFile))
                }
            }

        }

        public void Dispose()
        {
            this.mblnDisposing = true;
            if (this.Dirty)
            {
                this.Save();
            }
            this.mblnDisposed = true;
            this.mblnDisposing = false;
            GC.SuppressFinalize(this);
        }

        ~AppSettings()
        {
            base.Finalize();
            if (this.Dirty)
            {
                if (!this.mblnDisposed)
                {
                    if (!this.mblnDisposing)
                    {
                        this.Dispose();
                    }
                }
            }
        }

        private XmlNode xAS(string block)
        {
            return cfg.SelectSingleNode("//" + block);
        }

        public bool AutoSave
        {
            get
            {
                return this.mblnAutoSave;
            }
            set
            {
                this.mblnAutoSave = value;
            }
        }

        public bool Dirty
        {
            get
            {
                return this.mblnDirty;
            }
        }

        public AppSetting Add(string block, string Key, string Value)
        {
            AppSetting NewSetting = new AppSetting(Key, Value, this);
            this.Add(block, NewSetting);
            return NewSetting;
        }

        public void Add(string block, AppSetting NewSetting)
        {
            XmlElement newElem;
            XmlAttribute newAttr;

            newElem = cfg.SelectSingleNode("//" + block);
            if (newElem == null)
            {
                newElem = cfg.CreateElement(block);
                xAS("configuration").AppendChild(newElem);
            }

            newElem = cfg.CreateElement(NEWELEMENT);

            newAttr = cfg.CreateAttribute("key");
            newAttr.Value = NewSetting.Key;
            newElem.Attributes.Append(newAttr);

            newAttr = cfg.CreateAttribute("value");
            newAttr.Value = NewSetting.Value;
            newElem.Attributes.Append(newAttr);

            xAS(block).AppendChild(newElem);

            this.mblnDirty = true;
            if (this.AutoSave)
            {
                //Me.Save()
            }
        }

        public Collection Item(string block, ADM.stringConnection stringConnection)
        {

            XmlNodeList xNodeList;
            try
            {
                xNodeList = cfg.SelectSingleNode("//" + block).SelectNodes("//" + block + "//item");

                if (stringConnection.ConnProperty.Count > 0)
                {
                    stringConnection.ZeraConnProperty();
                }

                foreach (XmlNode xNode in xNodeList)
                {
                    if (Strings.Trim(xNode.Attributes[1].InnerText) != string.Empty)
                    {
                        stringConnection.ConnProperty.Add(xNode.Attributes[0].InnerText, xNode.Attributes[1].InnerText, System.Type.GetType("System.String"), true);
                    }
                }
                return stringConnection.ConnProperty.Collection;
            }
            catch (Exception)
            {
                throw (new Exception("Não foi possível carregar a coleção."));
            }

        }

        public AppSetting Item(string block, string Key)
        {
            return this.Item(block, Key, string.Empty);
        }

        public AppSetting Item(string block, string Key, string valueDefault)
        {
            XmlNode xNode;
            string strSearch = "//item[@key=\'{0}\']";
            AppSetting las = new AppSetting();

            try
            {

                xNode = xAS(block).SelectSingleNode("//" + block + string.Format(strSearch, Key));

                if (xNode == null)
                {
                    las.Key = Key;
                    las.Value = valueDefault;
                    this.Add(block, las);
                    //Return las
                }
                else
                {
                    las.Key = Key;
                    las.Value = xNode.Attributes.Item(1).Value;
                }

                return las;

            }
            catch (Exception ex)
            {
                throw (ex);
                //Dim las As New AppSetting(Key, valueDefault)
                //Me.Add(block, las)
                //xNode = xAS(block).SelectSingleNode(String.Format(strSearch, Key))
            }

        }

        public void RemoveByKey(string block, AppSetting Setting)
        {
            XmlNode xNode;
            string strSearch = "//item[@key=\'{0}\']";

            xNode = xAS(block).SelectSingleNode(string.Format(strSearch, Setting.Key));

            if (xNode != null)
            {

            }
            this.mblnDirty = true;
            if (this.AutoSave)
            {
                this.Save();
            }

        }

        public void RemoveByKey(string Key)
        {
            Interaction.Beep();
            throw (new NotSupportedException("Função: RemoveByKey(ByVal Key As String), ainda não implementada!"));
        }

        public AppSetting Update(string block, string Key, string Value)
        {
            AppSetting NewSetting = new AppSetting(Key, Value, this);
            this.Update(block, NewSetting);
            return NewSetting;
        }

        public void Update(string block, AppSetting NewSetting)
        {
            XmlNode xNode;
            string strSearch = "//item[@key=\'{0}\']";

            xNode = xAS(block).SelectSingleNode(string.Format(strSearch, NewSetting.Key));

            if (xNode == null)
            {
                //Throw New ArgumentOutOfRangeException("Key", NewSetting.Key, "O item que está sendo solicitado existe. Um novo item está sendo criado.")
                this.Add(block, NewSetting);
            }
            else
            {
                xNode.Attributes.Item(1).Value = NewSetting.Value;
            }

            this.mblnDirty = true;

            if (this.AutoSave)
            {
                this.Save();
            }
        }

        public void Save()
        {
            // We don't have a try catch here so
            // that if we fail, it will bounce up
            // to our caller.
            //cfg.Save(Me.mstrFileName)
            this.mblnDirty = false;
        }

        private AppSetting[] GetAllItems(string block)
        {

            XmlNodeList xNodeList;
            XmlAttributeCollection atts;
            XmlDocument xmNew = new XmlDocument();

            if (block.Trim() == string.Empty)
            {
                block = APPSETTINGS_ELEMENT + "//appSettings";
            }

            xmNew.LoadXml(xAS(block).OuterXml);
            xNodeList = xmNew.SelectNodes(XPATH_KEY_ADD);

            AppSetting[] asa = new AppSetting[xNodeList.Count - 1 + 1];
            AppSetting asi;
            int i = -1;

            foreach (XmlNode xNode in xNodeList)
            {
                i++;
                atts = xNode.Attributes;

                asi = new AppSetting(atts.Item(0).Value, atts.Item(1).Value, this);
                asa[i] = asi;
            }

            return asa;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            //Observe que não há em AppSettings uma coleção de AppSetting
            //Porque a coleção está baseada no XMLdocument
            //Por isso há uma função getAllItens que lê o xml
            //e o envia para dentro da classe iterator
            //Para que esta classe faça o que o método GetEnumerator faz em uma coleção.
            //Dim colecao As Collection
            //Return colecao.GetEnumerator
            //
            //Neste caso maItens é uma matriz de appSetting
            this.maItems = this.GetAllItems(this.xmlBLOCK);
            return new Iterator(this.maItems);
        }

        private class Iterator : IEnumerator
        {


            // This private class exposes the necessary
            // functions so that For..Each will work.
            private AppSetting[] mData;
            private int Index = -1;

            public Iterator(AppSetting[] Keys)
            {
                mData = Keys;
            }
            public object Current
            {
                get
                {
                    return mData[Index];
                }
            }
            public bool MoveNext()
            {
                Index++;
                if (Index <= (mData.Length - 1))
                {
                    return true;
                }
            }
            public void Reset()
            {
                Index = -1;
            }
        }

        public class nodeCollection
        {

            public string Caption;
            public string Value;
            public bool Dirty;
        }
    }

    #endregion





}

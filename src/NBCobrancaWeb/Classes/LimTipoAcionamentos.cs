using System;

namespace NBCobranca.Classes
{
    /// <summary>
    /// Camada de Negócio dos Tipos de Acionamentos
    /// </summary>
    public class LimTipoAcionamentos
    {
        private const string aSource = "=== Tipo de Acionamento ===";
        private string aStringPesquisa;
        private int aIdTmp;
        private Tipos.TipoPesquisa aTipoPesquisa;
        private Classes.Sistema aParent;
        private NBdbm.Fachadas.plxCOBR.primitivas.TipoAcionamento aTipoAcionamento;
        private NBdbm.self aSelf;

        public LimTipoAcionamentos(Classes.Sistema pParent)
        {
            this.aParent = pParent;
            aSelf = pParent.Self;
            this.aTipoAcionamento = new NBdbm.Fachadas.plxCOBR.primitivas.TipoAcionamento(ref aSelf, this.aParent.LimLogin.TipoConexao);
        }
        /// <summary>
        /// ID Temporário, usado para a Exclusão de um Tipo de Dívida, 
        /// entre outras finalidades.
        /// </summary>
        public int IdTmp
        {
            get
            {
                return this.aIdTmp;
            }
            set
            {
                this.aIdTmp = value;
            }
        }
        /// <summary>
        /// Define-se por que campo será feita a pesquisa de uma dívida.
        /// </summary>
        public Tipos.TipoPesquisa TipoPesquisa
        {
            get
            {
                return this.aTipoPesquisa;
            }
            set
            {
                this.aTipoPesquisa = value;
            }
        }
        /// <summary>
        /// Define o String que será usada para filtrar a pesquisa
        /// de Tipo da Dívida. ex: Iniciais da Descrição, ou o Código da Dívida.
        /// </summary>
        public string StringPesquisa
        {
            set
            {
                this.aStringPesquisa = value;
            }
        }
        /// <summary>
        /// Campos do Tipo de Dívida.
        /// </summary>
        public NBdbm.Interfaces.iCOBR.Primitivas.iTipoAcionamento TipoAcionamento
        {
            get
            {
                return this.aTipoAcionamento.Campos;
            }
        }
        /// <summary>
        /// Busca no Banco de Dados uma lista de Tipos de Acionamento, usado 
        /// para preencher um DataGrid ou similar.
        /// </summary>
        /// <returns>Retorna um DataView com todas os Fields da Tabela</returns>
        public System.Data.DataView DataSource()
        {
            //Comando SQL que executa uma Pesquisa no banco de dados.
            string comandoSQL = "SELECT * FROM COBR_TipoAcionamento ";
            //Cria-se um novo DataSet para receber os dados da pesquisa.
            System.Data.DataSet DS = new System.Data.DataSet();
            //Concatena o comandoSQL com o sqlWhere + outras funcionálidades do comando SQL.
            comandoSQL += this.Where();
            //Cria-se um DataAdapter para receber o comando sql que ira fazer a consulta.
            System.Data.SqlClient.SqlDataAdapter DA;
            //Instancia-se o DataAdapter com o ComandoSQL e a conexão com o Banco de Dados.
            DA = new System.Data.SqlClient.SqlDataAdapter(comandoSQL, (System.Data.SqlClient.SqlConnection)this.aParent.Connection);
            //Preenche o DataSet.
            DA.Fill(DS);
            //Retorna o DataView o mesmo será mostrado em uma DataGrid ou similar.
            return DS.Tables[0].DefaultView;
        }
        /// <summary>
        /// Cria uma Nova instancia do Tipo de Acionamento.
        /// </summary>
        public void NovoTipoAcionamento()
        {
            this.aTipoAcionamento = new NBdbm.Fachadas.plxCOBR.primitivas.TipoAcionamento(ref aSelf, this.aParent.LimLogin.TipoConexao);
        }
        /// <summary>
        /// Exclui uma Dívida com base na propriedade IDTmp
        /// </summary>
        public void Excluir()
        {
            try
            {
                if (this.aIdTmp == 0)
                    throw new Exception("A Propriedade IdTmp não foi definida");

                this.aTipoAcionamento.filterWhere = "id=" + this.aIdTmp.ToString();
                this.aTipoAcionamento.excluir(false);
            }
            catch (NBdbm.NBexception nbEx)
            {
                throw new NBdbm.COBR_Exception(nbEx);
            }
            catch (Exception ex)
            {
                throw new NBdbm.COBR_Exception(ex.Message, aSource);
            }

        }
        public void GetTipoAcionamento()
        {
            try
            {
                if (this.aIdTmp == 0)
                    throw new Exception("A Propriedade IdTmp não foi definida");
                this.aTipoAcionamento.getFields("id=" + this.aIdTmp.ToString(), false);
            }
            catch (NBdbm.NBexception nbEx)
            {
                throw new NBdbm.COBR_Exception(nbEx);
            }
            catch (Exception ex)
            {
                throw new NBdbm.COBR_Exception(ex.Message, aSource, ex);
            }
        }
        /// <summary>
        /// Salva no Banco de Dados o Tipo da Dívida.
        /// </summary>
        public void Salvar()
        {
            try
            {
                this.aTipoAcionamento.Campos.salvar();

            }
            catch (NBdbm.NBexception nbEx)
            {
                throw new NBdbm.COBR_Exception(nbEx);
            }
            catch (Exception ex)
            {
                throw new NBdbm.COBR_Exception(ex.Message, aSource);
            }
        }
        /// <summary>
        /// Define o filtro Where de uma SQL, com base nas propriedades 
        /// TipoPesquisa e StringPesquisa.
        /// </summary>
        /// <returns>Retorna um Fragmento de SQL</returns>
        private string Where()
        {
            System.Text.StringBuilder sqlWhere = new System.Text.StringBuilder("WHERE ");

            //Verifica os Tipos de Pesquisa que podem ser por ID, Descrição.
            switch (this.aTipoPesquisa)
            {
                case Tipos.TipoPesquisa.ID:
                    sqlWhere.Append("ID=" + this.aStringPesquisa);
                    break;

                case Tipos.TipoPesquisa.Descricao:
                    sqlWhere.Append("Descricao Like '" + this.aStringPesquisa + "%'");
                    break;

            }
            sqlWhere.Append(" ORDER BY ID");
            return sqlWhere.ToString();
        }


    }
}

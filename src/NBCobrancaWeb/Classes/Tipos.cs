using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MigraDoc.DocumentObjectModel;

namespace NBCobranca.Tipos
{
    /// <summary>
    /// Tipo de Pesquisa que pode ser feita por ID, Nome, CPF, Cidade, 
    /// Descricao ou Número de Série
    /// </summary>
    public enum TipoPesquisa
    {
        ID = 1,
        Nome = 2,
        CPF = 3,
        Cidade = 4,
        Descricao = 5,
        NumeroSerie = 6,
        Cliente = 7,
        DataEmissao = 8,
        CodBarras = 9
    }
    /// <summary>
    /// Define os Períodos de Pesquisas - Usado para Pesquisa de Ordem de Serviços.
    /// </summary>
    public enum TipoPeriodo
    {
        Todos = 1,
        UmAno = 2,
        SeisMeses = 3,
        TresMeses = 4,
        UmMes = 5
    }
    /// <summary>
    /// Tipo da Pessoa que é o cliente ou fornecedor por exemplo 
    /// Pessoa Física ou Jurídica.
    /// </summary>
    public enum TipoPessoa
    {
        Juridica = 0,
        Fisica = 1,
        Todas = 2
    }
    /// <summary>
    /// Tipo das Coleções, Coleções de Endereco, Telefone, Email, Site, OSItens, OSDespAdic ou Todas.
    /// </summary>
    public enum TipoColecoes
    {
        Endereco,
        Telefone,
        Email,
        Site,
        Dividas,
        DividasVencidas,
        Acionamentos,
        Todos
    }
    /// <summary>
    /// Tipo de Situações dos Itens de Estoque.
    /// </summary>
    public enum TipoSituacaoItens
    {
        disponivel = 1,
        orcado = 2,
        confirmado = 3,
        locado = 4,
        manutencao = 5,
        pendente = 6,
        emdevolucao = 7
    }
    /// <summary>
    /// Tipos de Credenciais no Sistema, Logístico, Operador e Administrador
    /// </summary>
    public enum TipoCredencial
    {
        Acionador = 1,
        Encarregado = 2,
        Administrador = 3
    }
    /// <summary>
    /// Tipo de Permissões do Sistema, pois a permissão padrão segue uma ordem
    /// onde o Logístico somente tem permissão entrada e saída de itens locados.
    /// Logo o tipo Administrador somente o administrador tem permissão.
    /// e todos o próprio nome já diz, acesso para todos.
    /// </summary>
    public enum Permissao
    {
        Padrao = 1,
        Administrador = 2,
        Todos = 3
    }
    /// <summary>
    /// Tipo de Contas, por ex: A Pagar, A Receber ou Despesas Diversas.
    /// </summary>
    public enum TipoContas
    {
        aPagar = 1,
        aReceber = 2,
        DespesasDiversas = 3
    }
    /// <summary>
    /// Tipo de Entidades que posso armazenar na Tabela CTRL_Entidades.
    /// O Todos só é usado para questões de filtro.
    /// </summary>
    public enum TipoEntidades
    {
        Funcionarios,
        Devedores,
        Clientes,
        Fornecedores,
        Todos
    }
    /// <summary>
    /// Indica as opções de seleção de Dividas, marcar todas ou desmarcar todas.
    /// </summary>
    public enum TipoSelecaoDividas
    {
        MarcarTodas,
        DesmarcarTodas,
        MarcarUma,
        DesmarcarUma
    }
    /// <summary>
    /// Tipo de Ações que podem ocorrer com linkbuttons
    /// </summary>
    public enum TipoAcaoLinkButtons
    {
        Novo,
        Salvar,
        Excluir,
        Cancelar,
        AutoParcelar
    }

    /// <summary>
    /// Tipos de ações realizadas sobre a distribuição de fichas
    /// </summary>
    public enum TipoAcoesDistribuicaoFichas
    {
        DistribuicaoAutomatica,
        DistribuicaoManual,
        ZerarDistribuicaoAutomatica,
        ZerarDistribuicaoManual,
        Transferencia,
        TransferenciaDeFerias,
        Penalizacao,
        Rodizio
    }

    /// <summary>
    /// Tipo para filtro de dividas por quantidade
    /// </summary>
    public enum FiltroQuantidadeDeDividas
    {
        Todas,
        UmaDivida,
        DuasDividas,
        TresDividas,
        MaisQueTresDividas
    }
    /// <summary>
    /// Classe para identificar de onde um determinado evento é chamado.
    /// </summary>
    public class EventArgsSalvarEntidade : System.EventArgs
    { }

    /// <summary>
    /// Dto responsável pelo conteúdo de uma carta, como por exemplo título, dados do devedor e conteúdo.
    /// </summary>
    public class dtoCarta
    {
        readonly DtoCartaDadosDevedor _devedor;
        readonly List<DtoCartaLinha> _tituloSegundoAviso;
        readonly List<DtoCartaLinha> _conteudo;
        readonly List<DtoCartaLinha> _rodape;
        readonly bool _segundoAviso;

        public dtoCarta(bool pSegundoAviso, List<DtoCartaLinha> pTituloSegundoAviso, List<DtoCartaLinha> pConteúdo, List<DtoCartaLinha> pRodape)
        {
            _tituloSegundoAviso = pTituloSegundoAviso;
            _conteudo = pConteúdo;
            _rodape = pRodape;
            _segundoAviso = pSegundoAviso;
        }

        public dtoCarta(dtoCarta pCartaPadrao, DtoCartaDadosDevedor pDevedor)
        {
            _tituloSegundoAviso = pCartaPadrao.TituloSegundoAviso;
            var cartaLinha = pCartaPadrao.Conteudo.FirstOrDefault(x => x.Texto.Contains("[Protocolo]"));
            if (cartaLinha != null)
                cartaLinha.Texto = cartaLinha.Texto.Replace("[Protocolo]", pDevedor.Protocolo);
            _conteudo = pCartaPadrao.Conteudo;
            _rodape = pCartaPadrao.Rodape;
            _devedor = pDevedor;
            _segundoAviso = pCartaPadrao.SegundoAviso;

        }

        public bool SegundoAviso
        {
            get { return this._segundoAviso; }
        }

        public List<DtoCartaLinha> TituloSegundoAviso
        {
            get
            {
                if (_segundoAviso)
                    return _tituloSegundoAviso;
                else return new List<DtoCartaLinha>();
            }
        }

        public List<DtoCartaLinha> Conteudo
        {
            get { return _conteudo; }
        }

        public List<DtoCartaLinha> Rodape
        {
            get { return _rodape; }
        }

        public DtoCartaDadosDevedor DadosDevedor
        {
            get
            {
                StringBuilder mDadosDevedor = new StringBuilder();
                return _devedor;
            }
        }
    }

    public class DtoCartaDadosDevedor
    {
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro;
        public string CEP;
        public string Cidade;
        public string UF;
        public string Comentario;
        public string Protocolo;
        public string Email { get; set; }
    }

    public class DtoCartaLinha
    {
        public string Texto { get; set; }
        public TextFormat TextFormat { get; set; }
        public ParagraphAlignment ParagraphAlignment { get; set; } = ParagraphAlignment.Justify;
        public Unit FirstLineIndent { get; set; } = Unit.FromCentimeter(2);
    }
    public class DtoSms
    {
        int _codigoDevedor;
        string _nomeDevedor;
        string _mensagem;
        string _telefone;

        public DtoSms()
        { }

        public DtoSms(string pMensagem, string pNumeroCelular)
        {
            this.Mensagem = pMensagem;
            this.Telefone = pNumeroCelular;
        }

        public DtoSms(int pCodigoDevedor, string pNomeDevedor, string pMensagem, string pNumeroCelular)
        {
            this._codigoDevedor = pCodigoDevedor;
            this._nomeDevedor = pNomeDevedor;
            this.Mensagem = pMensagem;
            this.Telefone = pNumeroCelular;
        }

        public int CodigoDevedor
        {
            get { return _codigoDevedor; }
            set { _codigoDevedor = value; }
        }

        public string NomeDevedor
        {
            get { return _nomeDevedor; }
            set { _nomeDevedor = value; }
        }

        public string Mensagem
        {
            get { return _mensagem; }
            set
            {
                if (value != null && value.Length > 160) value = value.Substring(0, 160);
                _mensagem = value == null ? null : NBCobranca.Classes.NBFuncoes.RetiraAcentos(value);
            }
        }

        public string Telefone
        {
            get { return _telefone; }
            set { _telefone = value; }
        }
    }

    /// <summary>
    /// Classe que representa todos os dados do relatório de bordero
    /// </summary>
    public class DtoRelBordero
    {
        public List<DtoRegistroRelBordero> Registros { get; private set; }
        public SortedDictionary<int, DtoRegistroResumoRelBordero> ResumoRelBorderos { get; private set; }
        public string Carteira { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public double TotalValorRecebido { get; private set; }
        public double TotalValorNominal { get; private set; }
        public int NumeroDoBordero { get; set; }
        public string Titulo => "Bordero de Recebimentos";
        public string Periodo => $"Período de {DataInicial?.ToShortDateString()} até {DataFinal?.ToShortDateString()}";
        public string DataEmissao => $"Data do Bordero: {DateTime.Now.ToShortDateString()}";

        public void AddRegistro(DtoRegistroRelBordero registroRelBordero)
        {
            TotalValorNominal += registroRelBordero.ValorNominal;
            TotalValorRecebido += registroRelBordero.ValorRecebido;
            Registros.Add(registroRelBordero);
        }

        public DtoRelBordero()
        {
            Registros = new List<DtoRegistroRelBordero>();
            ResumoRelBorderos = new SortedDictionary<int, DtoRegistroResumoRelBordero>();
            TotalValorNominal = 0;
            TotalValorRecebido = 0;
        }


        /// <summary>
        /// Classe que representa um registro dos dados que serão mostrados no relatório de bordero
        /// </summary>
        public class DtoRegistroRelBordero
        {
            public string NomeDevedor { get; set; }
            public string TipoDivida { get; set; }
            public string Contrato { get; set; }
            public string NumDoc { get; set; }
            public DateTime DataVencimento { get; set; }
            public DateTime DataBaixa { get; set; }
            public string Carteira { get; set; }
            public int CodigoDevedor { get; set; }
            public double ValorRecebido { get; set; }
            public double ValorNominal { get; set; }
            public bool BaixaParcial { get; set; }
        }

        public class DtoRegistroResumoRelBordero
        {
            public int NumeroLinha { get; set; }
            public string Descricao { get; set; }
            public double Valor { get; set; }
        }

    }

    public class DtoTotaisPorAcionador
    {
        public int IdAcionador { get; set; }
        public string NomeAcionador { get; set; }
        public int TotalFichasRecebidas { get; set; }
        public int TotalFichasComPromessa { get; set; }
        public int TotalFichasLivresParaAcionarHoje { get; set; }
        public int TotalFichasAcionadas { get; set; }

    }

    public class DtoFichasPorAcionador
    {
        public int IdDevedor { get; set; }
        public string Carteira { get; set; }
        public string NomeDevedor { get; set; }
        public DateTime? DataUltimoAcionamento { get; set; }
        public string TipoDoAcionamento { get; set; }
        public DateTime LiberadaParaAcionarAPartirDe { get; set; }
    }

    public class DtoDetalhesDoAcionador
    {
        public string NomeAcionador { get; set; }
        public int FichasDistribuidas { get; set; }
        public int FichasLiberadasParaAcionar { get; set; }
        public decimal ValorTotalACobrar { get; set; }
        public List<DtoTotalPorTipoAcionamento> TotaisPorTipoAcionamentos { get; set; }
        public List<DtoFichasPorAcionador> FichasDoAcionador { get; set; }

        public DtoDetalhesDoAcionador()
        {
            TotaisPorTipoAcionamentos = new List<DtoTotalPorTipoAcionamento>();
            FichasDoAcionador = new List<DtoFichasPorAcionador>();
        }

        public class DtoTotalPorTipoAcionamento
        {
            public string TipoAcionamento { get; set; }
            public int TotalDeFichas { get; set; }
        }
    }
}

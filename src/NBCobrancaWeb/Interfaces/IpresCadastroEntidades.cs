using System;
using System.Collections.Generic;
using System.Text;
using NBCobranca.Classes;

namespace NBCobranca.Interfaces
{
    /// <summary>
    /// Interface Presenter do cadastro de Entidades.
    /// <para>
    /// Implementação do Padrão MVP - Model View Presenter
    /// </para>
    /// </summary>
    public interface IpresCadastroEntidades : IpresEntidade, IpresEndereco, IpresTelefone, IpresEmail
    {       
        bool IsPostBack { get;}
        string TituloCadastro { set;}
        string LabelNomeEntidade { set;}
        void AtualizaDataSourceColecoes(System.Collections.ICollection pDataSource, Tipos.TipoColecoes pColecao);
        void BloquearLimparCampos(Tipos.TipoColecoes pColecaoCamposParaBloquear, bool pBloquear);
        void BloquearLimparCampos(Tipos.TipoColecoes pColecaoCamposParaBloquear, bool pBloquear, bool pLimpar);
    }
}

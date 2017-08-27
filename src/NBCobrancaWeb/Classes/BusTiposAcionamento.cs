using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBCobranca.Entidades;
using Neobridge.NBDB;

namespace NBCobranca.Classes
{
    public class BusTiposAcionamento: BusBase
    {
        public BusTiposAcionamento(Sistema sistema, DBDirect dbDirect) : base(sistema, dbDirect)
        {
        }

        public List<entCOBR_TipoAcionamento> ListarTodos
        {
            get
            {
                var tipoAcionamento = new entCOBR_TipoAcionamento();
                var lista = new List<entCOBR_TipoAcionamento>();
                using (var dr = DbDirect.ExecuteDataReader(tipoAcionamento.SqlSelect(new Dictionary<string, object>())))
                    while (dr.Read())
                    {
                        var item = new entCOBR_TipoAcionamento();
                        item.Preencher(dr);
                        lista.Add(item);
                    }
                return lista;
            }
        }

    }
}

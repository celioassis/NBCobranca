using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NBCobranca.Entidades
{
    public class entCOBR_TipoAcionamento : entBase
    {
        private string _descricao;
        private int _diasReacionamento;
        private short _credencialExigida;

        public entCOBR_TipoAcionamento()
        {
            _NomeCamposChave.Add("Id");
            _NomeCamposTabela.AddRange(new string[]
            {"Id",
            "Descricao",
            "DiasReacionamento",
            "CredencialExigida"});
            Clear();

        }

        public override void Preencher(DataRow pDrCtrlEntidade)
        {
            throw new NotImplementedException();
        }

        public override void Preencher(IDataReader pDrCtrlEntidade)
        {
            Id = pDrCtrlEntidade.GetInt32(0);
            _descricao = pDrCtrlEntidade.GetString(1);
            _diasReacionamento = pDrCtrlEntidade.GetInt32(2);
            _credencialExigida = Convert.ToInt16(pDrCtrlEntidade.GetValue(3));
        }

        public override sealed void Clear()
        {
            Id = 0;
            _descricao = null;
            _diasReacionamento = 0;
            _credencialExigida = 0;
        }

        public override string Key => $"{Id}{_descricao}";

        public override int ID_BD
        {
            get { return Id; }
            set { Id = value; }
        }

        public int Id { get; set; }

        public string Descricao
        {
            get { return _descricao; }
            set
            {
                _descricao = value;
                ValidaAlteracao(ref _descricao, value, "Descricao");
            }
        }

        public int DiasReacionamento
        {
            get { return _diasReacionamento; }
            set
            {
                _diasReacionamento = value;
                ValidaAlteracao(ref _diasReacionamento, value, "DiasReacionamento");
            }
        }

        public short CredencialExigida
        {
            get { return _credencialExigida; }
            set
            {
                _credencialExigida = value;
                ValidaAlteracao(ref _credencialExigida, value, "CredencialExigida");
            }
        }
    }
}

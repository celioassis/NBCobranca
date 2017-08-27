using System.IO;
using System.Web;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using NBCobranca.Tipos;
using System;

namespace NBCobranca.Classes.Relatorios
{
    public class RelBorderos : Relatorios<DtoRelBordero>
    {
        private DtoRelBordero _dados;

        Table _table;

        public RelBorderos(HttpServerUtility server) : this(server.MapPath)
        { }

        public RelBorderos(Func<string, string> pathImages) : base(pathImages)
        {
            Documento.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(1);
            var section = Documento.LastSection;
            section.PageSetup.OddAndEvenPagesHeaderFooter = false;
            section.PageSetup.StartingNumber = 1;

            var paragraph = new Paragraph { Format = { Alignment = ParagraphAlignment.Right } };
            paragraph.AddText("Página: ");
            paragraph.AddPageField();

            // Add paragraph to footer for odd pages.
            section.Footers.Primary.Add(paragraph);
            // Add clone of paragraph to footer for odd pages. Cloning is necessary because an object must
            // not belong to more than one other object. If you forget cloning an exception is thrown.
            section.Footers.EvenPage.Add(paragraph.Clone());

        }

        public override MemoryStream Print(DtoRelBordero dados)
        {
            _dados = dados;

            ImprimeCabecalho();
            ImprimeTitulo();
            ImprimeDataEmissao();
            ImprimeNumeroBordero();
            ImprimeCliente();
            Documento.LastSection.AddParagraph();
            ImprimePeriodo();
            CriarCorpoDoRelatorio();
            ImprimeRegistros();
            ImprimeTotal();
            ImprimeResumo();

            return CriarStream();
        }

        private void ImprimeTitulo()
        {
            var paragrafo = Documento.LastSection.AddParagraph();
            paragrafo.Format.SpaceAfter = Unit.FromCentimeter(1);
            paragrafo.Format.Alignment = ParagraphAlignment.Center;
            paragrafo.Format.Font.Bold = true;
            paragrafo.AddText(_dados.Titulo);
        }

        private void ImprimeDataEmissao()
        {
            var paragrafo = Documento.LastSection.AddParagraph();
            paragrafo.Format.Alignment = ParagraphAlignment.Left;
            paragrafo.Format.Font.Bold = true;
            paragrafo.AddText(_dados.DataEmissao);
        }

        private void ImprimeCliente()
        {
            if (_dados.Carteira.Equals("Todas")) return;
            var paragrafo = Documento.LastSection.AddParagraph();
            paragrafo.Format.Alignment = ParagraphAlignment.Left;
            paragrafo.Format.Font.Bold = true;
            paragrafo.AddText($"Cliente: {_dados.Carteira}");

        }

        private void ImprimeNumeroBordero()
        {
            if (_dados.NumeroDoBordero.Equals(0)) return;
            var paragrafo = Documento.LastSection.AddParagraph();
            paragrafo.Format.Alignment = ParagraphAlignment.Left;
            paragrafo.Format.Font.Bold = true;
            paragrafo.AddText($"Bordero Número: {_dados.NumeroDoBordero.ToString("00000")}");

        }

        private void ImprimePeriodo()
        {
            if (_dados.DataInicial == null || _dados.DataFinal == null) return;
            var paragrafo = Documento.LastSection.AddParagraph();
            paragrafo.Format.Alignment = ParagraphAlignment.Left;
            paragrafo.Format.Font.Bold = true;
            paragrafo.AddText(_dados.Periodo);
        }

        private void ImprimeRegistros()
        {
            var alternado = false;

            foreach (var reg in _dados.Registros)
            {
                // Criando Linha de registro
                var row = _table.AddRow();
                row.HeightRule = RowHeightRule.Exactly;
                row.Height = Unit.FromCentimeter(0.75);
                row.Format.Font.Size = Unit.FromPoint(4);

                if (alternado)
                    row.Shading.Color = Gray;

                var indice = -1;
                if (_dados.Carteira.Equals("Todas"))
                    ConfiguraCell(row, reg.Carteira.ToUpper(), ++indice, false);
                ConfiguraCell(row, reg.NomeDevedor, ++indice, false);
                ConfiguraCell(row, reg.TipoDivida, ++indice, false);
                ConfiguraCell(row, reg.Contrato, ++indice, false);
                ConfiguraCell(row, reg.NumDoc, ++indice, false, ParagraphAlignment.Center);
                ConfiguraCell(row, reg.DataVencimento.ToShortDateString(), ++indice, false);
                ConfiguraCell(row, reg.DataBaixa.ToShortDateString(), ++indice, false);
                ConfiguraCell(row, reg.BaixaParcial ? "Sim" : "Não", ++indice, reg.BaixaParcial);
                ConfiguraCell(row, reg.ValorRecebido.ToString("N2"), ++indice, false, ParagraphAlignment.Right);

                alternado = !alternado;
            }

        }

        private void ImprimeTotal()
        {
            var offSet = _dados.Carteira.Equals("Todas") ? 0 : 1;
            var row = _table.AddRow();
            var cell = row.Cells[0];

            row.Shading.Color = GrayStrong;
            cell.MergeRight = 7 - offSet;
            ConfiguraCell(row, "Total", 0, true, ParagraphAlignment.Right);
            ConfiguraCell(row, _dados.TotalValorRecebido.ToString("N2"), 8 - offSet, true, ParagraphAlignment.Right);

        }

        private void ImprimeResumo()
        {
            if (_dados.ResumoRelBorderos.Count.Equals(0)) return;

            Documento.LastSection.AddPageBreak();

            ImprimeCabecalho();

            //Imprime titulo do resumo
            var paragrafo = Documento.LastSection.AddParagraph("Resumo de Repasse");
            paragrafo.Format.SpaceBefore = Unit.FromCentimeter(1);
            paragrafo.Format.SpaceAfter = Unit.FromCentimeter(1);
            paragrafo.Format.Alignment = ParagraphAlignment.Center;
            paragrafo.Format.Font.Bold = true;

            //Cria a Tabela de Resumo de Repasse
            var table = Documento.LastSection.AddTable();
            //table.Style = "Table";
            table.Borders.Style = BorderStyle.None;
            table.Borders.Width = 0;
            table.Borders.Left.Width = 0;
            table.Borders.Right.Width = 0;
            table.Rows.LeftIndent = 0;

            //Descrição
            var column = table.AddColumn("17.3cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            //Valor
            column = table.AddColumn("2.2cm");
            column.Format.Alignment = ParagraphAlignment.Right;


            var alternado = false;

            foreach (var reg in _dados.ResumoRelBorderos.Values)
            {
                // Criando Linha de registro
                var row = table.AddRow();
                if (alternado)
                    row.Shading.Color = Gray;

                ConfiguraCell(row, reg.Descricao, 0, false, ParagraphAlignment.Left, 10);
                ConfiguraCell(row, reg.Valor.ToString("N2"), 1, false, ParagraphAlignment.Right, 10);

                alternado = !alternado;
            }

        }

        private void CriarCorpoDoRelatorio()
        {
            // Create the item table
            _table = Documento.LastSection.AddTable();
            _table.Style = "Table";
            _table.Borders.Width = 0;
            _table.Borders.Left.Width = 0;
            _table.Borders.Right.Width = 0;
            _table.Rows.LeftIndent = 0;

            //Carteira
            Column column = _table.AddColumn("1,5cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            //Nome do Devedor
            column = _table.AddColumn("5 cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            //Tipo da Dívida
            column = _table.AddColumn("2.5cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            //Contrato
            column = _table.AddColumn("1.5cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            //NumDoc
            column = _table.AddColumn("1.5cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            //DataVencimento
            column = _table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            //DataPagamento
            column = _table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            //BaixaParcial
            column = _table.AddColumn("1.2cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            //ValorRecebido
            column = _table.AddColumn("2.5cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            if (!_dados.Carteira.Equals("Todas"))
            {
                _table.Columns.RemoveObjectAt(0);
                _table.Columns[0].Width = Unit.FromCentimeter(5.2);
            }

            // Criando o cabeçalho da tabela
            var row = _table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.HeightRule = RowHeightRule.Exactly;
            row.Height = Unit.FromCentimeter(0.75);
            row.Shading.Color = GrayStrong;

            var indice = -1;
            if (_dados.Carteira.Equals("Todas"))
                ConfiguraCell(row, "Carteira", ++indice);

            ConfiguraCell(row, "Nome do Devedor", ++indice);
            ConfiguraCell(row, "Tipo da Dívida", ++indice);
            ConfiguraCell(row, "Contrato", ++indice);
            ConfiguraCell(row, "Num.Doc", ++indice);
            ConfiguraCell(row, "Vencimento", ++indice);
            ConfiguraCell(row, "Pagamento", ++indice);
            ConfiguraCell(row, "Parcial", ++indice);
            ConfiguraCell(row, "Valor Recebido", ++indice, true, ParagraphAlignment.Right);

        }

        private static void ConfiguraCell(Row row, string texto, int indice, bool negrito = true, ParagraphAlignment alinhamentoParagrafo = ParagraphAlignment.Left, int tamanhoFonte = 8)
        {
            var cell = row.Cells[indice];
            cell.AddParagraph(texto);
            cell.Format.Font.Bold = negrito;
            cell.Format.Font.Size = tamanhoFonte;
            cell.Format.Alignment = alinhamentoParagrafo;
            cell.Format.Font.Name = "Arial";
            cell.VerticalAlignment = VerticalAlignment.Center;
        }

        readonly static Color Gray = new Color(242, 242, 242);

        readonly static Color GrayStrong = new Color(128, 128, 128);
    }
}

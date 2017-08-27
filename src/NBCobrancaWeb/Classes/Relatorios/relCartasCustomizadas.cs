using System.Collections.Generic;
using System.IO;
using System.Web;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using NBCobranca.Tipos;
using PdfSharp.Pdf;

namespace NBCobranca.Classes.Relatorios
{
    /// <summary>
    /// Relatório de Cartas Customizado
    /// </summary>
    public class RelCartasCustomizadas: Relatorios<List<dtoCarta>>
    {
        public RelCartasCustomizadas(HttpServerUtility server)
            :base(server.MapPath)
        {}

        public override MemoryStream Print(List<dtoCarta> cartas)
        {
            Documento.Info.Title = "Cartas de Cobrança";
            Documento.Info.Subject = "Relatório de Cartas de Cobrança";
            Documento.Info.Author = "Sistema de Cobrança Web - COEST";
            Documento.Sections.Clear();
            foreach (var carta in cartas)
            {
                Documento.AddSection();
                ImprimeCabecalho();
                ImprimirDadosDoDevedor(carta);
                ImprimirTituloSegundoAviso(carta);
                ImprimirConteudo(carta);
                ImprimirRodape(carta);
            }

            return CriarStream();

        }

        private void ImprimirDadosDoDevedor(dtoCarta carta)
        {
            var paragrafo = Documento.LastSection.AddParagraph();
            paragrafo.Format.LeftIndent = Unit.FromCentimeter(3);
            paragrafo.Format.SpaceBefore = Unit.FromCentimeter(0);
            paragrafo.Format.SpaceAfter = Unit.FromCentimeter(1);
            paragrafo.AddText("Ilmo Sr(a).\n");
            paragrafo.AddFormattedText($"{carta.DadosDevedor.Nome}\n", TextFormat.Bold);
            paragrafo.AddText($"{carta.DadosDevedor.Logradouro} - {carta.DadosDevedor.Complemento}\n");
            paragrafo.AddText($"{carta.DadosDevedor.Bairro}\n");
            paragrafo.AddText($"{carta.DadosDevedor.CEP} - {carta.DadosDevedor.Cidade} - {carta.DadosDevedor.UF} - {carta.DadosDevedor.Comentario}\n");

        }

        private void ImprimirTituloSegundoAviso(dtoCarta carta)
        {
            if (!carta.SegundoAviso) return;
            var paragrafo = Documento.LastSection.AddParagraph();
            paragrafo.Format.LeftIndent = Unit.FromCentimeter(3);
            paragrafo.Format.SpaceAfter = Unit.FromCentimeter(1);
            foreach (var linha in carta.TituloSegundoAviso)
                paragrafo.AddFormattedText(linha.Texto, linha.TextFormat);
        }

        private void ImprimirConteudo(dtoCarta carta)
        {
            var paragrafo = Documento.LastSection.AddParagraph();
            paragrafo.Format.Alignment = ParagraphAlignment.Justify;
            paragrafo.Format.FirstLineIndent = Unit.FromCentimeter(2);
            foreach (var linha in carta.Conteudo)
            {
                paragrafo.AddFormattedText(linha.Texto, linha.TextFormat);
                paragrafo.Format.Alignment = linha.ParagraphAlignment;
                paragrafo.Format.FirstLineIndent = linha.FirstLineIndent;
                if (!linha.Texto.Contains("\n")) continue;
                paragrafo = Documento.LastSection.AddParagraph();
                paragrafo.Format.Alignment = linha.ParagraphAlignment;
                paragrafo.Format.FirstLineIndent = linha.FirstLineIndent;
            }
        }

        private void ImprimirRodape(dtoCarta carta)
        {
            var paragrafo = Documento.LastSection.AddParagraph();            
            paragrafo.Format.SpaceAfter = Unit.FromCentimeter(1);
            paragrafo.Format.Alignment = ParagraphAlignment.Center;
            foreach (var linha in carta.Rodape)
                paragrafo.AddFormattedText(linha.Texto, linha.TextFormat);
           
            paragrafo = Documento.LastSection.Footers.Primary.AddParagraph();
            paragrafo.Format.Alignment = ParagraphAlignment.Center;
            paragrafo.Format.Font.Size = Unit.FromPoint(8);

            paragrafo.AddText("Nosso escritório atende em horário comercial de Segunda à Sexta-feira das 8:00 às 12:00  e das  14:00 às 18:00 Horas.\n");
            paragrafo.AddText("Email: acaocobrancas@yahoo.com.br");
        }

    }
}

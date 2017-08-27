using System.Collections.Generic;
using System.IO;
using System.Web;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using NBCobranca.Tipos;
using PdfSharp.Pdf;
using System;

namespace NBCobranca.Classes.Relatorios
{
    public abstract class Relatorios<T> where T : class
    {
        protected readonly Func<string, string> PathImages;
        protected Document Documento;

        protected Relatorios(Func<string, string> pathImages)
        {
            PathImages = pathImages;

            Documento = new Document
            {
                DefaultPageSetup =
                {
                    TopMargin = Unit.FromCentimeter(1),
                    RightMargin = Unit.FromCentimeter(1),
                    LeftMargin = Unit.FromCentimeter(2),
                    BottomMargin = Unit.FromCentimeter(1.5),
                    FooterDistance = Unit.FromCentimeter(1)
                }
            };

            var style = Documento.Styles["Normal"];
            style.Font.Name = "Times New Roman";
            style.Font.Size = Unit.FromPoint(13);

            Documento.AddSection();
        }

        public abstract MemoryStream Print(T dados);

        protected void ImprimeCabecalho()
        {
            var section = Documento.LastSection;
            var paragraph = section.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            section.AddImage(PathImages("../../imagens/cab_coest.png"));
        }

        protected MemoryStream CriarStream()
        {
            var renderer = new PdfDocumentRenderer(true, PdfFontEmbedding.Always)
            {
                Document = Documento
            };

            renderer.RenderDocument();


            var stream = new MemoryStream();
            renderer.PdfDocument.Save(stream, false);
            return stream;
        }

    }
}

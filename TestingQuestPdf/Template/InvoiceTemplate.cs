using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestingQuestPdf.Models;

namespace TestingQuestPdf.Template
{
    public class InvoiceTemplate : IDocument
    {
        public Invoice Invoice { get; }

        public InvoiceTemplate(Invoice invoice)
        {
            Invoice = invoice;
        }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                       .Page(page =>
                       {
                           page.Margin(50);
                           page.Header().Element(ComposeHeader);
                           page.Content().Element(ComposeContent);
                           page.Footer().Element(ComposeFooter);
                       });
        }

        #region Header
        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"Invoice #{Invoice.ReferenceCode}").Style(titleStyle);

                    column.Item().Text(text =>
                    {
                        text.Span("Issue date: ").SemiBold();
                        text.Span($"{Invoice.ReferenceCode}");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Due date: ").SemiBold();
                        text.Span($"{Invoice.ReferenceCode}");
                    });
                });

                row.ConstantItem(100).Height(50).Placeholder();
            });
        }
        #endregion

        #region Content
        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);

                column.Item().Element(ComposeTable);
            });
        }

        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();
                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Invoice");
                    header.Cell().Element(CellStyle).AlignRight().Text("Value");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (InvoiceDetails item in Invoice.InvoiceDetails)
                {
                    table.Cell().Element(CellStyle).Text(1.ToString());
                    table.Cell().Element(CellStyle).Text(item.Code);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Value}$");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }
        #endregion

        #region Compose Footer
        void ComposeFooter(IContainer container)
        {
            container.AlignCenter().Text(x =>
            {
                x.CurrentPageNumber();
                x.Span(" / ");
                x.TotalPages();
            });
        }
        #endregion
    }
}

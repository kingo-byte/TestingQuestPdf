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
                           page.Size(PageSizes.A4);
                           page.Header().Element(ComposeHeader);
                           page.Content().Element(ComposeContent);
                           page.Footer().Element(ComposeFooter);
                       });
        }

        #region Header
        void ComposeHeader(IContainer container)
        {
            TextStyle titleStyle = TextStyle
                .Default
                .FontSize(17)
                .SemiBold()
                .FontColor(Colors.Indigo.Darken1);

            container.Column(column =>
            {
                column
                    .Item()
                    .Row(row =>
                    {
                        row.ConstantItem(70)
                            .Height(70)
                            .Image("./Assets/sgbl-logo.png");

                        row.RelativeItem()
                            .AlignRight()
                            .Column(column =>
                            {
                                column.Item()
                                    .Text(DateTime.Now.ToString("dd/MM/yyyy"))
                                    .AlignRight()
                                    .FontSize(12);
                            });
                    });

                column
                .Item()
                .PaddingTop(10)
                .Row(row =>
                {
                    row.RelativeItem().Column(column =>
                    {
                        column
                            .Item()
                            .BorderTop(1)
                            .BorderColor(Colors.Red.Lighten1)
                            .BorderBottom(1)
                            .BorderColor(Colors.Red.Lighten1)
                            .Background(Colors.Grey.Lighten2)
                            .TranslateY(8)
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.DefaultTextStyle(TextStyle.Default.LineHeight(1));

                                text.Line("نسخة عن فاتورة الهاتف")
                                    .Style(titleStyle);

                                text.Line("عن شهر حزيران 2024")
                                    .Style(titleStyle);
                            });
                    });
                });

                column
                    .Item()
                    .PaddingTop(10)
                    .Row(row =>
                    {
                        row.RelativeItem()
                            .Column(column =>
                            {
                                column
                                    .Item()
                                    .AlignLeft()
                                    .TranslateX(100)
                                    .Text("العنوان: بيروت");
                            });

                        row.RelativeItem()
                            .Column(column =>
                            {
                                column
                                    .Item()
                                    .AlignRight()
                                    .Text("الاسم: محمد");
                            });
                    });


                column
                .Item()
                .PaddingTop(10)
                .Row(row =>
                {
                    row.RelativeItem()
                        .Column(column =>
                        {
                            column
                                .Item()
                                .AlignLeft()
                                .TranslateX(100)
                                .Text("العنوان: بيروت");
                        });

                    row.RelativeItem()
                        .Column(column =>
                        {
                            column
                                .Item()
                                .AlignRight()
                                .Text("الاسم: محمد");
                        });
                });
            });
        }

        #endregion

        #region Content
        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(20).Column(column =>
            {
                column.Item().Element(ComposeTable);
            });
        }

        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(5);
                    columns.RelativeColumn(5);
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("القيمة ل.ل").AlignRight();
                    header.Cell().Element(CellStyle).AlignRight().Text("الخدمة");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container
                            .DefaultTextStyle(x => x.SemiBold().FontSize(12)) // Make the text semi-bold and larger
                            .Border(1)
                            .BorderColor(Colors.Grey.Lighten1)
                            .Background(Colors.Grey.Lighten2)
                            .Padding(5);
                    }
                });


                foreach (InvoiceDetails item in Invoice.InvoiceDetails)
                {
                    table.Cell().Element(CellStyle).Text(item.Value.ToString()).AlignRight();
                    table.Cell().Element(CellStyle).Text(item.Code).AlignRight();

                    static IContainer CellStyle(IContainer container)
                    {
                        return container
                            .Border(1)
                            .BorderColor(Colors.Grey.Lighten1)
                            .Padding(5);
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

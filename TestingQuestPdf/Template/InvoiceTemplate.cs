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
            });
        }

        #endregion

        #region Content
        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(20).
            Column(column =>
            {
                column
                .Item()
                .PaddingTop(20)
                .Element(ComposeContentHeaderTable);

                column
                .Item()
                .PaddingTop(20)
                .Element(ComposeTable);
            });
        }

        void ComposeContentHeaderTable(IContainer container) 
        {
            container
            .Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(4);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(4);
                    columns.RelativeColumn(3);
                });

                table.Header(header =>
                {
                    //row 1
                    header.Cell().Element(CellValue).Text("07240964").AlignRight();
                    header.Cell().Element(CellTitle).AlignRight().Text("الرقم القديم");
                    header.Cell().AlignRight().Text("");
                    header.Cell().Element(CellValue).AlignRight().Text("07240964");
                    header.Cell().Element(CellTitle).AlignRight().Text("اشتراك رقم");


                    header.Cell().Text("");
                    header.Cell().AlignRight().Text("");
                    header.Cell().AlignRight().Text("");
                    header.Cell().AlignRight().Text("");
                    header.Cell().AlignRight().Text("");

                    //row 2
                    header.Cell().Element(CellValue).Text("01/20/2000").AlignRight();
                    header.Cell().Element(CellTitle).AlignRight().Text("تاريخ التأسيس");
                    header.Cell().AlignRight().Text("");
                    header.Cell().Element(CellValue).AlignRight().Text("محمد كمال عيتاني");
                    header.Cell().Element(CellTitle).AlignRight().Text("الاسم");

                    header.Cell().Text("");
                    header.Cell().AlignRight().Text("");
                    header.Cell().AlignRight().Text("");
                    header.Cell().AlignRight().Text("");
                    header.Cell().AlignRight().Text("");

                    //row 3
                    header.Cell().Element(CellValue).Text("07240964").AlignRight();
                    header.Cell().Element(CellTitle).AlignRight().Text("الرقم القديم");
                    header.Cell().AlignRight().Text("");
                    header.Cell().Element(CellValue).AlignRight().Text("07240964");
                    header.Cell().Element(CellTitle).AlignRight().Text("اشتراك رقم");

                    static IContainer CellTitle(IContainer container)
                    {
                        return container
                            .DefaultTextStyle(x => x.SemiBold().FontSize(12))
                            .Border(1)
                            .BorderColor(Colors.Grey.Lighten1)
                            .Background(Colors.Grey.Lighten2)
                            .Padding(5);
                    }

                    static IContainer CellValue(IContainer container)
                    {
                        return container
                            .DefaultTextStyle(x => x.SemiBold().FontSize(12))
                            .Border(1)
                            .BorderColor(Colors.Grey.Lighten1)
                            .Background(Colors.White)
                            .Padding(5);
                    }
                });
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
            container.Column(column =>
            {
                column.Item().Element(Line);

                column.Item().AlignRight().Text("ملاحظة: هذه الفاتورة وتفاصيلها تستند بالكامل إلى المعلومات الواردة من وزارة الاتصالات");

                column.Item().PaddingTop(4).AlignRight().Text(x =>
                {
                    x.Span("page ");
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });

            });

            static IContainer Line(IContainer container)
            {
                return container
                    .Height(1)
                    .Background(Colors.Grey.Lighten1)
                    .ExtendHorizontal();
            }
        }
        #endregion
    }
}

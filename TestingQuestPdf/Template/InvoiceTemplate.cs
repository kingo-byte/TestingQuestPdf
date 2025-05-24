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
                           page.Margin(20);
                           page.Size(PageSizes.A4.Landscape());
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
                .Bold()
                .FontColor(Colors.Black);

            TextStyle labelStyle = TextStyle
                .Default
                .FontSize(13)
                .Bold()
                .FontColor(Colors.Black);

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
                            .TranslateY(8)
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.DefaultTextStyle(TextStyle.Default.LineHeight(1));

                                text.Line("List of Port Daily Payments")
                                    .Style(titleStyle);
                            });
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
                            .AlignLeft()
                            .Text(text =>
                            {
                                text.DefaultTextStyle(TextStyle.Default.LineHeight(1));
                                text.Span("Operation Date: ")
                                .Style(labelStyle);
                                text.Span(DateTime.Now.ToString("dd/MM/yyyy"));
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
                .Element(ComposeTable);
            });
        }

        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Inputter");
                    header.Cell().Element(CellStyle).Text("Client Nbr");
                    header.Cell().Element(CellStyle).Text("Invoice Ref");
                    header.Cell().Element(CellStyle).Text("Client Name");
                    header.Cell().Element(CellStyle).Text("Total");
                    header.Cell().Element(CellStyle).Text("FT Ref");
                    header.Cell().Element(CellStyle).Text("Status");
                    header.Cell().Element(CellStyle).Text("Payment Date");
                    header.Cell().Element(CellStyle).Text("Channel");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container
                            .DefaultTextStyle(x => x.SemiBold().FontSize(9))
                            .Border(1)
                            .BorderColor(Colors.Grey.Lighten1)
                            .Padding(2);
                    }
                });


                foreach (InvoiceDetails item in Invoice.InvoiceDetails)
                {
                    table.Cell().Element(CellStyle).Text(item.Inputter.ToString());
                    table.Cell().Element(CellStyle).Text(item.ClientNbr);
                    table.Cell().Element(CellStyle).Text(item.InvoiceRef);
                    table.Cell().Element(CellStyle).Text(item.ClientName);
                    table.Cell().Element(CellStyle).Text(item.Total);
                    table.Cell().Element(CellStyle).Text(item.FtRef);
                    table.Cell().Element(CellStyle).Text(item.Status);
                    table.Cell().Element(CellStyle).Text(item.PaymentDate);
                    table.Cell().Element(CellStyle).Text(item.Channel);

                    static IContainer CellStyle(IContainer container)
                    {
                        return container
                            .DefaultTextStyle(x => x.SemiBold().FontSize(9))
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
                column.Item().PaddingTop(4).AlignRight().Text(x =>
                {
                    x.Span("page ");
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });

            });
        }
        #endregion
    }
}

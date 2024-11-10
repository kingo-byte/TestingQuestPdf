using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

QuestPDF.Settings.License = LicenseType.Community;

Document document = Document.Create(container =>
{
    container.Page(page =>
    {
        page.Size(PageSizes.A4);
        page.Margin(2, Unit.Centimetre);
        page.PageColor(Colors.White);
        page.DefaultTextStyle(x => x.FontSize(20));

        page.Header()
            .Text("SGBL")
            .SemiBold()
            .FontSize(30)
            .FontColor(Colors.Blue.Medium);

        page.Content()
            .PaddingVertical(1, Unit.Centimetre)
            .Column(col =>
            {
                col.Spacing(20);

                // Add Date and Image
                col.Item().Text(Placeholders.LongDate());
                col.Item().Image(Placeholders.Image(200, 100));

                // Add Two Tables Side by Side
                col.Item().Row(row =>
                {
                    row.RelativeItem().Table(table =>
                    {
                        // First Table
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        // Header for First Table
                        table.Header(header =>
                        {
                            header.Cell().Border(1).BorderColor(Colors.Grey.Medium).Text("Col 1A").SemiBold();
                            header.Cell().Border(1).BorderColor(Colors.Grey.Medium).Text("Col 2A").SemiBold();
                        });

                        // Data Rows for First Table
                        for (int i = 0; i < 3; i++)
                        {
                            table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).Text($"Row {i + 1}, 1A");
                            table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).Text($"Row {i + 1}, 2A");
                        }
                    });

                    row.RelativeItem().Table(table =>
                    {
                        // Second Table
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        // Header for Second Table
                        table.Header(header =>
                        {
                            header.Cell().Border(1).BorderColor(Colors.Grey.Medium).Text("Col 1B").SemiBold();
                            header.Cell().Border(1).BorderColor(Colors.Grey.Medium).Text("Col 2B").SemiBold();
                        });

                        // Data Rows for Second Table
                        for (int i = 0; i < 3; i++)
                        {
                            table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).Text($"Row {i + 1}, 1B");
                            table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).Text($"Row {i + 1}, 2B");
                        }
                    });
                });
            });

        page.Footer()
            .AlignCenter()
            .Text(x =>
            {
                x.Span("Page ");
                x.CurrentPageNumber();
            });
    });
});

document.GeneratePdf("two_tables_side_by_side.pdf");

document.ShowInCompanion();

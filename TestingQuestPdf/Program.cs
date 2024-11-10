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

                // Add Table with Borders
                col.Item().Table(table =>
                {
                    // Define the number of columns
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(); // Auto size column
                        columns.RelativeColumn(); // Auto size column
                        columns.RelativeColumn(); // Auto size column
                    });

                    // Add a header row with borders
                    table.Header(header =>
                    {
                        header.Cell().Border(1).BorderColor(Colors.Grey.Medium).Text("Column 1").SemiBold();
                        header.Cell().Border(1).BorderColor(Colors.Grey.Medium).Text("Column 2").SemiBold();
                        header.Cell().Border(1).BorderColor(Colors.Grey.Medium).Text("Column 3").SemiBold();
                    });

                    // Add data rows with borders
                    for (int i = 0; i < 5; i++)
                    {
                        table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).Text($"Row {i + 1}, Col 1");
                        table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).Text($"Row {i + 1}, Col 2");
                        table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).Text($"Row {i + 1}, Col 3");
                    }
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

document.GeneratePdf("hello_with_borders.pdf");

document.ShowInCompanion();

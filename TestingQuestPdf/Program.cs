using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using TestingQuestPdf.DataSource;
using TestingQuestPdf.Models;
using TestingQuestPdf.Template;

QuestPDF.Settings.License = LicenseType.Community;

InvoiceDataSource ds = new InvoiceDataSource();

InvoiceTemplate document = new InvoiceTemplate(ds.GetInvoice());

document.GeneratePdf("invoice.pdf");

document.ShowInCompanion();

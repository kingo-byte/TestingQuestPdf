using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingQuestPdf.Models
{
    public class Invoice
    {
        public string ReferenceCode { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string OldPhoneNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime Year { get; set; }
        public string Category { get; set; } = string.Empty;   

        public List<InvoiceDetails> InvoiceDetails { get; set; } = new List<InvoiceDetails>(); 
    }

    public class InvoiceDetails 
    {
        public string Inputter { get; set; } = string.Empty;
        public string ClientNbr { get; set; } = string.Empty;
        public string InvoiceRef { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string Total { get; set; } = string.Empty;
        public string FtRef { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string PaymentDate { get; set; } = string.Empty;
        public string Channel { get; set; } = string.Empty;
    }
}

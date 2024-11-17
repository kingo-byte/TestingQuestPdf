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
        public string Code { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}

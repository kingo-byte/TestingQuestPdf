﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingQuestPdf.Models;

namespace TestingQuestPdf.DataSource
{
    public class InvoiceDataSource
    {
        public Invoice GetInvoice()
        {
            List<InvoiceDetails> list = new List<InvoiceDetails>() {
            new InvoiceDetails { Code = "Fixed Sub", Value = "20,000" },
            new InvoiceDetails { Code = "Variant Sub", Value = "10,000" },
            new InvoiceDetails { Code = "Total", Value = "30,0000" }
            };

            return new Invoice
            {
                ReferenceCode = "INV-2021-001",
                PhoneNumber = "09123456789",
                OldPhoneNumber = "09123456789",
                Name = "John Doe",
                Year = DateTime.Now,
                Category = "Subscription",
                InvoiceDetails = list
            };
        }
    }
}
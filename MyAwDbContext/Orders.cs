using System;
using System.Collections.Generic;

namespace ExamTask.MyAwDbContext
{
    public partial class Orders
    {
        public string SalesOrderId { get; set; }
        public string RevisionNumber { get; set; }
        public string Status { get; set; }
        public string OnlineOrderFlag { get; set; }
        public string SalesOrderNumber { get; set; }
        public int? InvoiceOrderNumber { get; set; }
        public string AccountNumber { get; set; }
        public string CreditCardApprovalCode { get; set; }
        public string SubTotal { get; set; }
        public string TaxAmt { get; set; }
        public string Freight { get; set; }
        public string TotalDue { get; set; }
        public int Id { get; set; }
    }
}

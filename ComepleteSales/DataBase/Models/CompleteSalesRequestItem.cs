using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class CompleteSalesRequestItem
    {
        public long CompleteSalesRequestItemId { get; set; }
        public Guid ApiId { get; set; }
        public Guid ApiUserId { get; set; }
        public Guid CompleteSalesRequestId { get; set; }
        public Guid EbayOrderTransactionId { get; set; }
        public string TrackingNumber { get; set; }
        public string Courier { get; set; }
        public DateTime ShipDate { get; set; }
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}

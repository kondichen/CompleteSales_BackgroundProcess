using System;

namespace EbayAPIService.Models
{
    public class CallCompleteSalesRequest
    {
        public string ItemSite;
        public string ItemId;
        public string TransactionId;
        public string TrackingNumber;
        public string Courier;
        public DateTime ShipDate;
        public bool IsShipped;
    }
}

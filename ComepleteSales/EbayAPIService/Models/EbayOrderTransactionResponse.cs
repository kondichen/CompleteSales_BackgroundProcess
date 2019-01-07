using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Models
{
   public class EbayOrderTransactionResponse
    {
        public string buyerEbayId;
        public string buyerEmail;
        public string itemId;
        public string listingType;
        public int quantityPurchase;
        public string transactionId;
        public double transactionPrice;
        public string itemSku;
        public string variationSku;
        public DateTime? shippedTime;
        public string platform;
        public string transactionSiteId;
        public string shippingCarrierUsed;
        public string shipmentTrackingNumber;
    }
}

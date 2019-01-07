using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class CompleteCheckoutNotification
    {
        public long CompleteCheckoutNotificationId { get; set; }
        public string NotifictionEvtName { get; set; }
        public string EbayItemId { get; set; }
        public string EbayTransactionId { get; set; }
        public string EbayOrderId { get; set; }
        public string EbayReceipentId { get; set; }
        public sbyte AutoPay { get; set; }
        public string EiasToken { get; set; }
        public DateTime NotificationReceiveOn { get; set; }
        public string ItemSku { get; set; }
        public string VariationSku { get; set; }
        public string TransactionSite { get; set; }
    }
}

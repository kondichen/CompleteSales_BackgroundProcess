using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class QueuePullOrder
    {
        public int PullOrderQueueId { get; set; }
        public long ApiUserPlatformTokenId { get; set; }
        public string EBayItemId { get; set; }
        public string OrderId { get; set; }
        public string SiteCode { get; set; }
    }
}

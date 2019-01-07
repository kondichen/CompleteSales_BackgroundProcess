using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class LogPullOrder
    {
        public int PullOrderBgplogId { get; set; }
        public DateTime ProcessStartTime { get; set; }
        public DateTime ProcessEndTime { get; set; }
        public int ProcessStatus { get; set; }
        public string ProcessMessage { get; set; }
        public long ApiUserPlatformTokenId { get; set; }
    }
}

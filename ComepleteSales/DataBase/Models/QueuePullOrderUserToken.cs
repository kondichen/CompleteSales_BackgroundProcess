using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class QueuePullOrderUserToken
    {
        public int LoginUserTokenQueueId { get; set; }
        public long ApiUserPlatformTokenId { get; set; }
        public int NumberOfDays { get; set; }
    }
}

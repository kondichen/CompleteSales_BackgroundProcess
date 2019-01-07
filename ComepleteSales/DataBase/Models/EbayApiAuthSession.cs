using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class EbayApiAuthSession
    {
        public string SessionId { get; set; }
        public string ApiId { get; set; }
        public string UserId { get; set; }
        public string EbaySessionId { get; set; }
        public DateTime ExpireOn { get; set; }
        public string Platform { get; set; }
    }
}

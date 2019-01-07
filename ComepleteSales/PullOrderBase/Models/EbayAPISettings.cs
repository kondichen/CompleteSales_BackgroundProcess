using System;
using System.Collections.Generic;
using System.Text;

namespace PullOrderTransaction.Base.Models
{
    public class EbayAPISettings
    {
        public string SiteID { get; set; }
        public string AppID { get; set; }
        public string DevID { get; set; }
        public string AuthCert { get; set; }
        public string Version { get; set; }
        public string Token { get; set; }
        public string EndPoint { get; set; }
    }
}

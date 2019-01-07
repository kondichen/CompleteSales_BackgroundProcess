using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class CompleteSalesRequest
    {
        public Guid CompleteSalesRequestId { get; set; }
        public Guid ApiId { get; set; }
        public Guid ApiUserId { get; set; }
        public int NumberOfItems { get; set; }
        public string CallBackUrl { get; set; }
        public DateTime RequestOn { get; set; }
        public string RequestBy { get; set; }
    }
}

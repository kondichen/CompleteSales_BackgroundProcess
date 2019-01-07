using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class ApiCallback
    {
        public int ApiCallbackId { get; set; }
        public string ApiId { get; set; }
        public string CallbackUrl { get; set; }
        public string TopicArn { get; set; }
        public sbyte IsDeleted { get; set; }
    }
}

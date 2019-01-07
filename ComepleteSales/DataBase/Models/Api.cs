using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class Api
    {
        public string ApiId { get; set; }
        public string ApiKey { get; set; }
        public string AppName { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime ModifyOn { get; set; }
        public sbyte IsDeleted { get; set; }
    }
}

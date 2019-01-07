using System;
using System.Collections.Generic;

namespace DataBase.Models
{
    public partial class ApiUser
    {
        public Guid ApiUserId { get; set; }
        public Guid ApiId { get; set; }
        public string UserPrivateKey { get; set; }
        public string UserToken { get; set; }
        public string Username { get; set; }
        public string UserKey { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime ModifyOn { get; set; }
        public bool IsDeleted { get; set; }
        public string CallBackUrl { get; set; }
    }
}

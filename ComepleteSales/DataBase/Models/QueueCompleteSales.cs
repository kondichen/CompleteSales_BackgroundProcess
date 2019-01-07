using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("queue_pullorder_user_token")]
    public partial class QueueCompleteSales
    {
        public int QueueCompleteSalesId { get; set; }
        public Guid ApiUserId { get; set; }
        public Guid CompleteSalesRequestId { get; set; }
    }
}

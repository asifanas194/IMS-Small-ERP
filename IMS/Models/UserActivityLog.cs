using System;
using System.ComponentModel.DataAnnotations;

namespace IMS.Models
{
    public class UserActivityLog
    {
        [Key] // 👈 Add this line (important!)
        public int LogId { get; set; }

        public string? Username { get; set; }
        public string? Action { get; set; }
        public string? Controller { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}

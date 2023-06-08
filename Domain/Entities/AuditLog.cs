using System;

namespace Domain.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public string Actor { get; set; }

        public string Action { get; set; }
    }
}

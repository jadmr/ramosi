using System;

namespace Entities.Entities
{
    public class Audit : BaseEntity
    {
        public Guid EntityGuid { get; set; }

        public string ChangeType { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public DateTimeOffset Date { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
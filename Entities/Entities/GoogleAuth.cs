using System;

namespace Entities.Entities
{
    public class GoogleAuth : BaseEntity
    {
        public string Sub { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
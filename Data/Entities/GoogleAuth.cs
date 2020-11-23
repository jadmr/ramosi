using System;

namespace Data.Entities
{
    public class GoogleAuth
    {
        public Guid Guid { get; set; }

        public string Sub { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }   
}
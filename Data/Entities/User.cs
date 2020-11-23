using System;

namespace Data.Entities
{
    public class User
    {
        public Guid Guid { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
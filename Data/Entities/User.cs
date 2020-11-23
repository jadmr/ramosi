using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class User
    {
        public User()
        {
            Plants = new List<PlantAlias>();
            PlantCollection = new List<AquiredPlant>();
        }

        public Guid Guid { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public virtual GoogleAuth GoogleAuth { get; set; }

        public virtual ICollection<PlantAlias> Plants { get; set; }
        public virtual IList<AquiredPlant> PlantCollection { get; set; }
    }
}
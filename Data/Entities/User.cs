using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class User
    {
        public User()
        {
            Plants = new List<Plant>();
            PlantCollection = new List<PlantCollection>();
        }

        public Guid Guid { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public virtual GoogleAuth GoogleAuth { get; set; }

        public virtual ICollection<Plant> Plants { get; set; }
        public virtual IList<PlantCollection> PlantCollection { get; set; }
    }
}
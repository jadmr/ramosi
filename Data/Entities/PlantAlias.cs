using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities
{
    public class PlantAlias
    {
        public PlantAlias()
        {
            Owners = new List<User>();
            PlantCollection = new List<AquiredPlant>();
        }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public Guid PlantId { get; set; }
        public virtual Plant Plant { get; set; }

        public virtual ICollection<User> Owners { get; set; }
        public virtual IList<AquiredPlant> PlantCollection { get; set; }
    }
}

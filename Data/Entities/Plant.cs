using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Plant
    {
        public Plant()
        {
            Owners = new List<User>();
            PlantCollection = new List<AquiredPlant>();
        }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public Guid PlantCharacteristicId { get; set; }
        public virtual PlantCharacteristic PlantCharacteristic { get; set; }

        public virtual ICollection<User> Owners { get; set; }
        public virtual IList<AquiredPlant> PlantCollection { get; set; }
    }
}

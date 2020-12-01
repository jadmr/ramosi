using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Plant
    {
        public Plant()
        {
            PlantCollection = new List<PlantCollection>();
        }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public Guid PlantCharacteristicId { get; set; }
        public virtual PlantCharacteristic PlantCharacteristic { get; set; }

        public virtual IList<PlantCollection> PlantCollection { get; set; }
    }
}

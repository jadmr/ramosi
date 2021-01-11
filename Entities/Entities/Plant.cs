using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class Plant : BaseEntity
    {
        public Plant()
        {
            PlantCollection = new List<PlantCollection>();
        }

        public string Name { get; set; }

        public Guid PlantCharacteristicId { get; set; }
        public virtual PlantCharacteristic PlantCharacteristic { get; set; }

        public virtual IList<PlantCollection> PlantCollection { get; set; }
    }
}

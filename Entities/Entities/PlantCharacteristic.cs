using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class PlantCharacteristic : BaseEntity
    {
        public PlantCharacteristic()
        {
            Plants = new List<Plant>() { };
        }

        public string Notes { get; set; }

        public virtual IList<Plant> Plants { get; set; }
    }
}

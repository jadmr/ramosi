using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class PlantCharacteristic
    {
        public PlantCharacteristic()
        {
            Plants = new List<Plant>(){};
        }

        public Guid Guid { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<Plant> Plants { get; set; }
    }
}

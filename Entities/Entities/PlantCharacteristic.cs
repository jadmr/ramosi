using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class PlantCharacteristic
    {
        public PlantCharacteristic()
        {
            Plants = new List<Plant>() { };
        }

        public Guid Guid { get; set; }

        public string Notes { get; set; }

        public virtual IList<Plant> Plants { get; set; }
    }
}

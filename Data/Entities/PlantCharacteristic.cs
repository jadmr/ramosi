using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class PlantCharacteristic
    {
        public PlantCharacteristic()
        {
            Aliases = new List<PlantAlias>(){};
        }

        public Guid Guid { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<PlantAlias> Aliases { get; set; }
    }
}

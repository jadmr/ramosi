using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities
{
    public class Plant
    {
        public Plant()
        {
            Aliases = new List<PlantAlias>(){};
        }

        public Guid Guid { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<PlantAlias> Aliases { get; set; }
    }
}

using System;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities
{
    public class PlantAlias
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public Guid Guid PlantId { get; set; }
        public virtual Plant Plant { get; set; }
    }
}

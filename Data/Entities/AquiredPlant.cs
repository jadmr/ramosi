using System;

namespace Data.Entities
{
    public class AquiredPlant
    {
        public Guid Guid { get; set; }

        public string Nickname { get; set; }

        public string Location { get; set; }

        public string Notes { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid PlantAliasId { get; set; }
        public virtual PlantAlias PlantAlias { get; set; }
    }
}
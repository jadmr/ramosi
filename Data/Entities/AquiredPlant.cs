using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class AquiredPlant
    {
        public AquiredPlant()
        {
            Schedules = new List<WateringSchedule>();
        }

        public Guid Guid { get; set; }

        public string Nickname { get; set; }

        public string Location { get; set; }

        public string Notes { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid PlantAliasId { get; set; }
        public virtual PlantAlias PlantAlias { get; set; }

        public virtual IList<WateringSchedule> Schedules { get; set; }
    }
}
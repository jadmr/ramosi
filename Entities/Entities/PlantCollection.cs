using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public class PlantCollection : BaseEntity
    {
        public PlantCollection()
        {
            Schedules = new List<WateringSchedule>();
        }

        public string Nickname { get; set; }

        public string Location { get; set; }

        public string Notes { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid PlantId { get; set; }
        public virtual Plant Plant { get; set; }

        public virtual IList<WateringSchedule> Schedules { get; set; }
    }
}
using System;

namespace Entities.Entities
{
    public class WateringSchedule : BaseEntity
    {
        public string DayOfWeek { get; set; }

        public DateTime TimeOfDay { get; set; }

        public string Repeat { get; set; }

        public Guid PlantCollectionId { get; set; }
        public virtual PlantCollection PlantCollection { get; set; }
    }
}
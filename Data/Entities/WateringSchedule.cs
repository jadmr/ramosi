using System;

namespace Data.Entities
{
    public class WateringSchedule
    {
        public Guid Guid { get; set; }

        public string DayOfWeek { get; set; }

        public DateTime TimeOfDay { get; set; }

        public string Repeat { get; set; }

        public Guid AquiredPlantId { get; set; }
        public virtual AquiredPlant Plant { get; set; }
    }
}
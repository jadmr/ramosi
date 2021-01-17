using Data.Context;
using Data.Repos.Base;
using Entities.Entities;

namespace Data.Repos
{
    public class WateringScheduleRepository : BaseRepository<WateringSchedule>
    {
        public WateringScheduleRepository(RamosiContext context) : base(context)
        {
        }

        protected override void UpdateOriginalEntityFields(WateringSchedule original, WateringSchedule edited)
        {
            original.DayOfWeek = edited.DayOfWeek;
            original.TimeOfDay = edited.TimeOfDay;
            original.Repeat = edited.Repeat;

            original.PlantCollection = edited.PlantCollection;
        }
    }
}
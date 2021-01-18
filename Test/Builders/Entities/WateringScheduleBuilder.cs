using System;
using Entities.Entities;
using Entities.Enum;

namespace Test.Builders.Entities
{
    public class WateringScheduleBuilder
    {
        private string dayOfWeek = nameof(Weekday.Monday);
        private DateTime timeOfDay = new DateTime(default);
        private string repeat = nameof(ScheduleRepeat.Daily);
        private PlantCollection plantCollection = new PlantCollectionBuilder();

        public static implicit operator WateringSchedule(WateringScheduleBuilder builder)
        {
            return new WateringSchedule()
            {
                DayOfWeek = builder.dayOfWeek,
                TimeOfDay = builder.timeOfDay,
                Repeat = builder.repeat,
                PlantCollection = builder.plantCollection,
            };
        }

        public WateringScheduleBuilder WithDayOfWeek(string dayOfWeek)
        {
            this.dayOfWeek = dayOfWeek;
            return this;
        }

        public WateringScheduleBuilder WithTimeOfDay(DateTime timeOfDay)
        {
            this.timeOfDay = timeOfDay;
            return this;
        }

        public WateringScheduleBuilder WithRepeat(string repeat)
        {
            this.repeat = repeat;
            return this;
        }

        public WateringScheduleBuilder WithPlantCollection(PlantCollection plantCollection)
        {
            this.plantCollection = plantCollection;
            return this;
        }
    }
}
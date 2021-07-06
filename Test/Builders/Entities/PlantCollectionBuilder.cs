using System;
using System.Collections.Generic;
using Test.Builders.Entities;

namespace Entities.Entities
{
    public class PlantCollectionBuilder
    {
        private Guid guid = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
        private string nickname = "nickname";
        private string location = "location";
        private string notes = "notes";
        private User user = new UserBuilder();
        private Plant plant = new PlantBuilder();
        private IList<WateringSchedule> wateringSchedule = new List<WateringSchedule>() { };

        public static implicit operator PlantCollection(PlantCollectionBuilder builder)
        {
            return new PlantCollection()
            {
                Guid = builder.guid,
                Nickname = builder.nickname,
                Location = builder.location,
                Notes = builder.notes,
                User = builder.user,
                Plant = builder.plant,
                Schedules = builder.wateringSchedule,
            };
        }

        public PlantCollectionBuilder WithGuid(Guid guid)
        {
            this.guid = guid;
            return this;
        }

        public PlantCollectionBuilder WithNickname(string nickname)
        {
            this.nickname = nickname;
            return this;
        }

        public PlantCollectionBuilder WithLocation(string location)
        {
            this.location = location;
            return this;
        }

        public PlantCollectionBuilder WithNotes(string notes)
        {
            this.notes = notes;
            return this;
        }

        public PlantCollectionBuilder WithUser(User user)
        {
            this.user = user;
            return this;
        }

        public PlantCollectionBuilder WithPlant(Plant plant)
        {
            this.plant = plant;
            return this;
        }

        public PlantCollectionBuilder WithWateringSchedule(IList<WateringSchedule> wateringSchedule)
        {
            this.wateringSchedule = wateringSchedule;
            return this;
        }
    }
}
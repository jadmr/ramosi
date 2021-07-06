using System;
using System.Collections.Generic;
using Entities.Entities;

namespace Test.Builders.Entities
{
    public class PlantCharacteristicBuilder
    {
        private Guid guid = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
        private string notes = "notes";
        private IList<Plant> plants = new List<Plant> { };

        public static implicit operator PlantCharacteristic(PlantCharacteristicBuilder builder)
        {
            return new PlantCharacteristic()
            {
                Guid = builder.guid,
                Notes = builder.notes,
                Plants = builder.plants,
            };
        }

        public PlantCharacteristicBuilder WithGuid(Guid guid)
        {
            this.guid = guid;
            return this;
        }

        public PlantCharacteristicBuilder WithNotes(string notes)
        {
            this.notes = notes;
            return this;
        }

        public PlantCharacteristicBuilder WithPlants(IList<Plant> plants)
        {
            this.plants = plants;
            return this;
        }
    }
}
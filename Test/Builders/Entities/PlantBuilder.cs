using System;
using System.Collections.Generic;
using Entities.Entities;

namespace Test.Builders.Entities
{
    public class PlantBuilder
    {
        private Guid guid = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
        private string name = "plant";
        private PlantCharacteristic plantCharacteristic = new PlantCharacteristicBuilder();
        private IList<PlantCollection> plantCollection = new List<PlantCollection>() { };

        public static implicit operator Plant(PlantBuilder builder)
        {
            return new Plant()
            {
                Guid = builder.guid,
                Name = builder.name,
                PlantCharacteristic = builder.plantCharacteristic,
                PlantCollection = builder.plantCollection,
            };
        }

        public PlantBuilder WithGuid(Guid guid)
        {
            this.guid = guid;
            return this;
        }

        public PlantBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public PlantBuilder WithPlantCharacteristic(PlantCharacteristic plantCharacteristic)
        {
            this.plantCharacteristic = plantCharacteristic;
            return this;
        }

        public PlantBuilder WithPlantCollection(IList<PlantCollection> plantCollection)
        {
            this.plantCollection = plantCollection;
            return this;
        }
    }
}

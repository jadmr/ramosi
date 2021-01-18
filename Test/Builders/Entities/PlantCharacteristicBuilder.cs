using System.Collections.Generic;
using Entities.Entities;

namespace Test.Builders.Entities
{
    public class PlantCharacteristicBuilder
    {
        private string notes = "notes";
        private IList<Plant> plants = new List<Plant> { };

        public static implicit operator PlantCharacteristic(PlantCharacteristicBuilder builder)
        {
            return new PlantCharacteristic()
            {
                Notes = builder.notes,
                Plants = builder.plants,
            };
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
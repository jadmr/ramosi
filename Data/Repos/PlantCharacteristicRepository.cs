using Data.Context;
using Data.Repos.Base;
using Entities.Entities;

namespace Data.Repos
{
    public class PlantCharacteristicRepository : BaseRepository<PlantCharacteristic>
    {
        public PlantCharacteristicRepository(RamosiContext context) : base(context)
        {
        }

        protected override void UpdateOriginalEntityFields(PlantCharacteristic original, PlantCharacteristic edited)
        {
            original.Notes = edited.Notes;
            original.Plants = edited.Plants;
        }
    }
}
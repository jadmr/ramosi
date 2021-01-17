using Data.Context;
using Data.Repos.Base;
using Entities.Entities;

namespace Data.Repos
{
    public class PlantRepository : BaseRepository<Plant>
    {
        public PlantRepository(RamosiContext context) : base(context)
        {
        }

        protected override void UpdateOriginalEntityFields(Plant original, Plant edited)
        {
            original.Name = edited.Name;
            original.PlantCharacteristic = edited.PlantCharacteristic;
            original.PlantCollection = edited.PlantCollection;
        }
    }
}
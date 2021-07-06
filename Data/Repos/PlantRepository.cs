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
            // Plant characteristics are edited from the PlantCharacteristicRepo
            // Plants are added or removed from a collection through the PlantCollectionRepo
            original.Name = edited.Name;
        }
    }
}
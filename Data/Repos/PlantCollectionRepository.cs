using Data.Context;
using Data.Repos.Base;
using Entities.Entities;

namespace Data.Repos
{
    public class PlantCollectionRepository : BaseRepository<PlantCollection>
    {
        public PlantCollectionRepository(RamosiContext context) : base(context)
        {
        }

        protected override void UpdateOriginalEntityFields(PlantCollection original, PlantCollection edited)
        {
            original.Nickname = edited.Nickname;
            original.Location = edited.Location;
            original.Notes = edited.Notes;

            original.User = edited.User;
            original.Plant = edited.Plant;
            original.Schedules = edited.Schedules;
        }
    }
}
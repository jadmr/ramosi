using Data.Context;
using Data.Repos.Base;
using Entities.Entities;

namespace Data.Repos
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(RamosiContext context) : base(context)
        {
        }

        protected override void UpdateOriginalEntityFields(User original, User edited)
        {
            original.Nickname = edited.Nickname;
            original.Email = edited.Email;
            original.Phone = edited.Phone;

            original.GoogleAuth = edited.GoogleAuth;
            original.PlantCollection = edited.PlantCollection;
        }
    }
}
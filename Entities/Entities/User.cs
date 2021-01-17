using System.Collections.Generic;

namespace Entities.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            PlantCollection = new List<PlantCollection>();
        }

        public string Nickname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public virtual GoogleAuth GoogleAuth { get; set; }

        public virtual IList<PlantCollection> PlantCollection { get; set; }

        public virtual IList<Audit> Audits { get; set; }
    }
}
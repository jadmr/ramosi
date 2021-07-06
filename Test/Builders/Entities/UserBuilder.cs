using System.Collections.Generic;
using Entities.Entities;

namespace Test.Builders.Entities
{
    public class UserBuilder
    {
        private string nickname = "nickname";
        private string email = "test@email.com";
        private string phone = "(012) 345-6789";
        private GoogleAuth googleAuth = new GoogleAuthBuilder();
        private IList<PlantCollection> plantCollection = new List<PlantCollection>() { };

        public static implicit operator User(UserBuilder builder)
        {
            return new User()
            {
                Nickname = builder.nickname,
                Email = builder.email,
                Phone = builder.phone,
                GoogleAuth = builder.googleAuth,
                PlantCollection = builder.plantCollection,
            };
        }

        public UserBuilder WithNickname(string nickname)
        {
            this.nickname = nickname;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            this.email = email;
            return this;
        }

        public UserBuilder WithPhone(string phone)
        {
            this.phone = phone;
            return this;
        }

        public UserBuilder WithGoogleAuth(GoogleAuth googleAuth)
        {
            this.googleAuth = googleAuth;
            return this;
        }

        public UserBuilder WithPlantCollection(IList<PlantCollection> plantCollection)
        {
            this.plantCollection = plantCollection;
            return this;
        }
    }
}
using Entities.Entities;

namespace Test.Builders.Entities
{
    public class GoogleAuthBuilder
    {
        private string sub = "hash";
        private User user = new UserBuilder();

        public static implicit operator GoogleAuth(GoogleAuthBuilder builder)
        {
            return new GoogleAuth()
            {
                Sub = builder.sub,
                User = builder.user,
            };
        }

        public GoogleAuthBuilder WithSub(string sub)
        {
            this.sub = sub;
            return this;
        }

        public GoogleAuthBuilder WithUser(User user)
        {
            this.user = user;
            return this;
        }
    }
}
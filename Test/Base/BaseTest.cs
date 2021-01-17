using System;

namespace Test.Base
{
    public abstract class BaseTest
    {
        protected readonly Guid guidOne = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
        protected readonly Guid guidTwo = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2);
        protected readonly Guid guidThree = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3);
    }
}
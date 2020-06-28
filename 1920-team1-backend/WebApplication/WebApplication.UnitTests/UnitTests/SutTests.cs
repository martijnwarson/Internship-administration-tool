using System;
using NUnit.Framework;

namespace WebApplication.UnitTests.UnitTests
{
    [TestFixture]
    public abstract class SutTests<TSut>
        where TSut : class
    {
        protected TSut Sut { get; set; }
    }
}
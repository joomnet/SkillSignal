using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSignal.UnitTests
{
    using NUnit.Framework;

    using SkillSignal.Common;

    [TestFixture]
    public class ObjectValidatorTests
    {
        [Test]
        public void When_the_object_is_null_an_exception_is_thrown()
        {
            object myObj = null;
            Assert.Throws<ArgumentNullException>(() => ObjectValidator.IfNullThrowException(myObj, "myObj"));
        }
    }
}

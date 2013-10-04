using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSignal.UnitTests
{
    using NUnit.Framework;

    using SkillSignal.DependencyInjection;

    [TestFixture]
    public class WCFServiceConfigurationTests
    {
        [Test]
        public void When_Service_Address_Is_Null_Or_Empty_Should_Throw_Argument_Null_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new WCFServiceConfiguration(null, "http://localhost:1234/mex"));
            Assert.Throws<ArgumentNullException>(() => new WCFServiceConfiguration(string.Empty, "http://localhost:1234/mex"));
        }

        [Test]
        public void When_MEX_Address_Is_Null_Or_Empty_Should_Throw_Argument_Null_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new WCFServiceConfiguration("http://localhost:1234", null));
            Assert.Throws<ArgumentNullException>(() => new WCFServiceConfiguration("http://localhost:1234", string.Empty));
        }

        [Test]
        public void When_Service_Address_Is_Invalid_Should_Throw_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new WCFServiceConfiguration("http://localhost---:1234", "http://localhost:1234/mex"));            
        }
    }
}

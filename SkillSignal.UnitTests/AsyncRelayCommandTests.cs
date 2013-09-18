

namespace SkillSignal.UnitTests
{
    using NUnit.Framework;

    public class AsyncRelayCommandTests
    {
        [Test]
        public void CanExecute_ConditionIsFalse_FalseReturned()
        {
            var command = new AsyncRelayCommand(() => null, () => false);
            NUnit.Framework.Assert.IsFalse(command.CanExecute(null));
        }
    }
}

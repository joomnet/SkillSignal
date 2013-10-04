namespace SkillSignal.DependencyInjection
{
    using System;

    using SkillSignal.Common;

    public class WCFServiceConfiguration
    {
        public Uri MexAddress { get; private set; }

        public Uri ServiceAddress { get; private set; }

        public WCFServiceConfiguration(string serviceAddress, string mexAddress)
        {
            ObjectValidator.IfNullThrowException(serviceAddress, "serviceAddress");
            ObjectValidator.IfNullThrowException(mexAddress, "mexAddress");

            this.MexAddress = new Uri(mexAddress);
            this.ServiceAddress = new Uri(serviceAddress);
        }
    }
}
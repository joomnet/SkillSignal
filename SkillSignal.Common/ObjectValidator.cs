namespace SkillSignal.Common
{
    using System;

    public class ObjectValidator
    {
        public static void IfNullThrowException<TObject>(TObject thisObject, string parameterName)
        {
            if (thisObject == null)
            {
                throw new ArgumentNullException(string.Format("Parameter named {0} cannot be null", parameterName));
            }
        }
    }
}
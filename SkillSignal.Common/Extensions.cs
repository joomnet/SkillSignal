using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SkillSignal.Common
{
    public static class ExpressionExtensions
    {
        public static PropertyInfo GetProperty<T>(this Expression<Func<T>> expression)
        {
            var propertyExpression = expression.Body as MemberExpression;
            if (propertyExpression == null)
            {
                throw new NotSupportedException("Expression must be a member expression");
            }
            var propertyInfo = propertyExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new NotSupportedException("Member expression must be a property");
            }

            return propertyInfo;
        }
    }

    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T obj in enumerable)
                action(obj);
        }
    }
}

using System;
using System.Linq.Expressions;

namespace FilmLibrary.Core.Helpers
{
    public static class ObjectExtensionHelper
    {
        public static string GetPropertyName<T>(this object obj, Expression<Func<T>> expression)
        {
            MemberExpression memberExpression = (MemberExpression)expression.Body;
            return memberExpression.Member.Name;
        }
    }
}

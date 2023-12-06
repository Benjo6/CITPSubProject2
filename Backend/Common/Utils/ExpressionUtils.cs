using System.Linq.Expressions;

namespace Common.Utils;

/// <summary>
/// Helper class
/// </summary>
public static class ExpressionUtils
{
    public static Expression<Func<T, object>> GetPropertyExpression<T>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var conversion = Expression.Convert(property, typeof(object));
        return Expression.Lambda<Func<T, object>>(conversion, parameter);
    }
}
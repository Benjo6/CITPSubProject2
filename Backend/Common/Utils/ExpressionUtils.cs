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
    
    public static Expression<Func<T, bool>> GetFilterExpression<T>(FilterCondition condition)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, condition.PropertyName);
        var constant = Expression.Constant(condition.Value);
        
        BinaryExpression comparison = condition.Operator switch
        {
            OperatorEnum.Equals => Expression.Equal(property, constant),
            OperatorEnum.Less => Expression.LessThan(property, constant),
            OperatorEnum.LessOrEqual => Expression.LessThanOrEqual(property, constant),
            OperatorEnum.Greater => Expression.GreaterThan(property, constant),
            OperatorEnum.GreaterOrEqual => Expression.GreaterThanOrEqual(property, constant),
            _ => throw new NotSupportedException($"Operator {condition.Operator} is not supported"),
        };
        return Expression.Lambda<Func<T, bool>>(comparison, parameter);
    }

}
using System.Linq.Expressions;

namespace Common.Utils;

public static class FilterUtils
{
    public static IQueryable<T> ApplyFilter<T>(IQueryable<T> query, Filter filter)
    {
        foreach (var criteria in filter.FilterCriteria)
        {
            var propertyInfo = typeof(T).GetProperty(criteria.Key);
            if (propertyInfo != null)
            {
                Type propertyType = propertyInfo.PropertyType;
                var parameter = Expression.Parameter(typeof(T), "t");
                var property = Expression.Property(parameter, criteria.Key);
                Expression comparison;
                object actualValue;

                if (criteria.Value.StartsWith(">="))
                {
                    actualValue = Convert.ChangeType(criteria.Value.Substring(2), Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                    comparison = Expression.GreaterThanOrEqual(property, Expression.Constant(actualValue, propertyType));
                }
                else if (criteria.Value.StartsWith("<="))
                {
                    actualValue = Convert.ChangeType(criteria.Value.Substring(2), Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                    comparison = Expression.LessThanOrEqual(property, Expression.Constant(actualValue, propertyType));
                }
                else if (criteria.Value.StartsWith(">"))
                {
                    actualValue = Convert.ChangeType(criteria.Value.Substring(1), Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                    comparison = Expression.GreaterThan(property, Expression.Constant(actualValue, propertyType));
                }
                else if (criteria.Value.StartsWith("<"))
                {
                    actualValue = Convert.ChangeType(criteria.Value.Substring(1), Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                    comparison = Expression.LessThan(property, Expression.Constant(actualValue, propertyType));
                }
                else
                {
                    actualValue = Convert.ChangeType(criteria.Value, Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                    comparison = Expression.Equal(property, Expression.Constant(actualValue, propertyType));
                }

                var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
                query = query.Where(lambda);
            }
        }
        return query;
    }
}
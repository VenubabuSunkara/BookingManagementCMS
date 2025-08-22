using System.Linq.Expressions;
using System.Reflection;

namespace Repository;

public static class IQueryableExtensions
{
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string orderByProperty, string direction)
    {
        var command = direction.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";
        var type = typeof(T);
        var property = type.GetProperty(orderByProperty, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        var resultExpression = Expression.Call(typeof(Queryable), command,
            new Type[] { type, property.PropertyType },
            query.Expression, Expression.Quote(orderByExpression));

        return query.Provider.CreateQuery<T>(resultExpression);
    }
}


using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class DataTableRepository : IDataTableRepository
{
    public async Task<DatatableResponse<T>> GetDataAsync<T>(IQueryable<T> query, DatatableRequest request,string[] searchColumns) where T : class
    {
        int totalRecords = await query.CountAsync();

        // Search
        if (!string.IsNullOrEmpty(request.search?.value))
        {
            string searchValue = request.search.value.ToLower();
            var props = typeof(T).GetProperties()
     .Where(p => p.PropertyType == typeof(string) && searchColumns.Contains(p.Name))
     .ToArray();
            Expression<Func<T, bool>> predicate = null;

            foreach (var prop in props)
            {

                var param = Expression.Parameter(typeof(T), "x");


                var propExpr = Expression.Property(param, prop);
                var notNull = Expression.NotEqual(propExpr, Expression.Constant(null, typeof(string)));
                var toLowerCall = Expression.Call(propExpr, "ToLower", null);
                var containsCall = Expression.Call(toLowerCall, "Contains", null, Expression.Constant(searchValue));
                var lambda = Expression.Lambda<Func<T, bool>>(containsCall, param);

                predicate = predicate == null ? lambda : CombineOr(predicate, lambda);
            }

            if (predicate != null)
                query = query.Where(predicate);
        }

        int filteredRecords = query.Count();

        // Order
        if (request.order?.Any() == true)
        {
            var sortColumn = request.columns[request.order[0].column].data;
            var sortDir = request.order[0].dir;
            query = query.OrderByDynamic(sortColumn, sortDir);
        }

        // Paging
        var data = await Task.FromResult(query.Skip(request.start).Take(request.length).ToList());

        return new DatatableResponse<T>
        {
            draw = request.draw,
            recordsTotal = totalRecords,
            recordsFiltered = filteredRecords,
            data = data
        };
    }

    private static Expression<Func<T, bool>> CombineOr<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var parameter = Expression.Parameter(typeof(T));

        var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
        var left = leftVisitor.Visit(expr1.Body);

        var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
        var right = rightVisitor.Visit(expr2.Body);

        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);
    }

    private class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression node) =>
            node == _oldValue ? _newValue : base.Visit(node);
    }
}

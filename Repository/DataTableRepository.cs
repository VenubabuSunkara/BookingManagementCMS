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
    public async Task<DatatableResponse<T>> GetDataAsync<T>(
     IQueryable<T> query,
     DatatableRequest request,
     string[] searchColumns)
     where T : class
    {
        if (request == null) return new DatatableResponse<T>();

        // Total count (before filtering)
        int totalRecords = await query.CountAsync();

        // 🔍 SEARCH: on string props only matching searchColumns
        if (!string.IsNullOrWhiteSpace(request.search?.value))
        {
            string term = request.search.value.Trim().ToLower();
            Expression<Func<T, bool>> predicate = null;

            var props = typeof(T).GetProperties()
                .Where(p => p.PropertyType == typeof(string) && searchColumns.Contains(p.Name))
                .ToArray();

            foreach (var prop in props)
            {
                var param = Expression.Parameter(typeof(T), "x");
                var propExpr = Expression.Property(param, prop);

                // x.Prop != null
                var notNull = Expression.NotEqual(propExpr, Expression.Constant(null, typeof(string)));

                // x.Prop.ToLower().Contains(term)
                var toLower = Expression.Call(propExpr, nameof(string.ToLower), null);
                var contains = Expression.Call(toLower, nameof(string.Contains), null,
                    Expression.Constant(term));

                var body = Expression.AndAlso(notNull, contains);
                var lambda = Expression.Lambda<Func<T, bool>>(body, param);

                predicate = predicate == null ? lambda : CombineOr(predicate, lambda);
            }

            if (predicate != null)
                query = query.Where(predicate);
        }

        // Filtered count
        int filteredRecords = await query.CountAsync();

        // 📌 ORDERING: dynamic, only if valid col/dir
        if (request.order?.Any() == true && request.columns != null)
        {
            var sortCol = request.columns[request.order[0].column]?.data;
            var sortDir = request.order[0].dir;
            if (!string.IsNullOrEmpty(sortCol) && !string.IsNullOrEmpty(sortDir))
            {
                query = query.OrderByDynamic(sortCol, sortDir);
            }
        }

        // ⚡ PAGING: Skip & Take only once
        var data = await query
            .Skip(request.start)
            .Take(request.length)
            .ToListAsync();

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

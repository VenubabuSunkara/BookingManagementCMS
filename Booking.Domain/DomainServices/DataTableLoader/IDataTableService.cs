using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.DomainServices.DataTableLoader;

public interface IDataTableService
{
    Task<DataTableResponseEntity<T>> GetDataAsync<T>(IQueryable<T> query, DataTableRequestEntity request, string[] searchColumns) where T : class;
}

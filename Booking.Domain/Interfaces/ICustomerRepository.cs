using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerDTableEntity> GetAll(int Skip, int Take, string searchKey, CancellationToken token);
        Task<IEnumerable<CustomerEntity>> ExportAllAsync(CancellationToken token);
        Task<int> UnLockCustomer(int CustomerId, CancellationToken token);
        Task UpdatePassword(CustomerPassordEntity customerEntity, CancellationToken token);
        Task<int> DeActivateAccount(int CustomerId, CancellationToken token);
    }
}

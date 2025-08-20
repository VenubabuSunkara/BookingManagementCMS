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
        Task<CustomerDTableEntity> GetAll(int Skip, int Take, string searchKey = "");
        Task<IEnumerable<CustomerEntity>> ExportAllAsync();
        Task UnLockCustomer(int CustomerId);
        Task UpdatePassword(CustomerEntity customerEntity);
        Task UpdateCustomer(CustomerEntity customerEntity);
        Task DeActivateAccount(int CustomerId);
    }
}

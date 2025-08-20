using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class CustomerRepository(BookingCmsContext context, IMemoryCache cache) : ICustomerRepository
    {
        private readonly BookingCmsContext _context = context;
        private readonly IMemoryCache _cache = cache;
        public async Task DeActivateAccount(int CustomerId)
        {
            var customer = await _context.Customers.FindAsync(CustomerId);
            if (customer != null)
            {
                customer.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<CustomerEntity>> ExportAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDTableEntity> GetAll(int Skip, int Take, string searchKey = "")
        {
            int TotalCount = await _context.Customers.CountAsync();
            var Customers = _context.Customers.Select(x => new CustomerEntity()
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                IsActive = x.IsActive,
                Id = x.Id,
                PhoneNumber = x.PhoneNumber,
                IsLocked = x.IsLocked,
                UpdatedOn = x.UpdatedOn

            }).Skip(Skip).Take(Take);
            return new CustomerDTableEntity()
            {
                Total = TotalCount,
                Filtered = TotalCount,
                CustomerEntities = Customers
            };
        }

        public async Task UnLockCustomer(int CustomerId)
        {
            var customer = await _context.Customers.FindAsync(CustomerId);
            if (customer != null)
            {
                customer.IsLocked = false;
                await _context.SaveChangesAsync();
            }
        }

        public Task UpdateCustomer(CustomerEntity customerEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePassword(CustomerEntity customerEntity)
        {
            throw new NotImplementedException();
        }
    }
}

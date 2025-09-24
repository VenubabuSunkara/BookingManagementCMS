using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Booking.Infrastructure.Repositories
{
    public class CustomerRepository(BookingCmsContext context, IMemoryCache cache) : ICustomerRepository
    {
        private readonly BookingCmsContext _context = context;
        private readonly IMemoryCache _cache = cache;
        public async Task<int> DeActivateAccount(int CustomerId, CancellationToken token)
        {
            return await _context.Customers
                            .Where(x => x.Id == CustomerId)
                            .ExecuteUpdateAsync(setters => setters.SetProperty(d => d.IsActive, false)
                            , cancellationToken: token);
        }

        public Task<IEnumerable<CustomerEntity>> ExportAllAsync(CancellationToken token)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return Task.FromResult(_context.Customers.Select(x => new CustomerEntity()
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                IsActive = x.IsActive,
                Id = x.Id,
                PhoneNumber = x.PhoneNumber,
                IsLocked = x.IsLocked,
                UpdatedOn = x.UpdatedOn
            }).AsEnumerable());
        }

        public async Task<CustomerDTableEntity> GetAll(int Skip, int Take, string searchKey, CancellationToken token)
        {
            var q = _context.Customers.AsNoTracking();
            var total = await q.CountAsync(token);
            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                q = q.Where(d =>
                            d.FirstName.Contains(searchKey) ||
                            d.PhoneNumber.Contains(searchKey) ||
                            d.Email.Contains(searchKey) ||
                            d.LastName.Contains(searchKey));
            }
            // simple order by FullName default
            q = q.OrderByDescending(d => d.CreatedOn);
            var filtered = await q.CountAsync(token);
            var page = await q.Skip(Skip).Take(Take).ToListAsync(token);
            return new CustomerDTableEntity()
            {
                Total = total,
                Filtered = filtered,
                CustomerEntities = page.Select(x => new CustomerEntity()
                {
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    IsActive = x.IsActive,
                    Id = x.Id,
                    PhoneNumber = x.PhoneNumber,
                    IsLocked = x.IsLocked,
                    UpdatedOn = x.UpdatedOn
                })
            };
        }

        public async Task<int> UnLockCustomer(int CustomerId, CancellationToken token)
        {
            return await _context.Customers
                           .Where(x => x.Id == CustomerId)
                           .ExecuteUpdateAsync(setters => setters.SetProperty(d => d.IsLocked, true)
                           , cancellationToken: token);
        }
        public Task UpdatePassword(CustomerPassordEntity customerEntity, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}

using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<int> DeActivateAccount(int CustomerId, CancellationToken token)
        {
            return await _customerRepository.DeActivateAccount(CustomerId, token);
        }

        public async Task<IEnumerable<CustomerDto>> ExportAllAsync(CancellationToken token)
        {
            var customerData = await _customerRepository.ExportAllAsync(token);
            return customerData.Select(x => new CustomerDto()
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                IsActive = x.IsActive,
                Id = x.Id,
                PhoneNumber = x.PhoneNumber,
                IsLocked = x.IsLocked,
                UpdatedOn = x.UpdatedOn
            });
        }

        public async Task<CustomerDTableDto> GetAll(int Skip, int Take, string searchKey, CancellationToken token)
        {
            var customerData = await _customerRepository.GetAll(Skip, Take, searchKey, token);
            return new CustomerDTableDto()
            {
                Total = customerData.Total,
                Filtered = customerData.Filtered,
                CustomerDto = customerData.CustomerEntities.Select(x => new CustomerDto()
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
            return await _customerRepository.UnLockCustomer(CustomerId, token);
        }

        public async Task UpdatePassword(CustomerPassordDto customerEntity, CancellationToken token)
        {
            await _customerRepository.UpdatePassword(new Domain.Entities.CustomerPassordEntity()
            {
                CustomerId = customerEntity.CustomerId,
                NewPassword = customerEntity.NewPassword,
            }, token);
        }
    }
}

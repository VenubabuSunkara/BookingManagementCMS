using Booking.Application.DTOs;

namespace Booking.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDTableDto> GetAll(int Skip, int Take, string searchKey, CancellationToken token);
        Task<IEnumerable<CustomerDto>> ExportAllAsync(CancellationToken token);
        Task<int> UnLockCustomer(int CustomerId, CancellationToken token);
        Task UpdatePassword(CustomerPassordDto customerEntity, CancellationToken token);
        Task<int> DeActivateAccount(int CustomerId, CancellationToken token);
    }
}

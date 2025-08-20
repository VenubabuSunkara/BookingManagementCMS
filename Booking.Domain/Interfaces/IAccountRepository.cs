using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> Login(string username, string password);
        Task<UserEntity> Register(UserEntity userEntity);
        Task<UserEntity> GetUserDetails(UserEntity user);
    }
}

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
        Task<UserEntity> Login(LoginEntity loginEntity);
        Task Register(UserEntity userEntity);
        Task<UserEntity> GetUserDetails(UserEntity user);
    }
}

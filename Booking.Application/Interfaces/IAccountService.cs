using Booking.Application.DTOs;
using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces
{
    public interface IAccountService
    {
        Task<UserDto?> Login(LoginEntity loginEntity);
        Task Register(UserEntity userEntity);
        Task<UserEntity> GetUserDetails(UserEntity user);
    }
}

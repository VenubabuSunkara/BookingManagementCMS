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
        Task<UserDto?> Register(UserEntity userEntity);
        Task LogOut(string UserId);
        Task<bool> ConfirmEmailAsync(string userId, string regToken, CancellationToken cancellation);
        Task<bool> ResetPassword(ForgotPasswordDto model, CancellationToken cancellation);
        Task<bool> ChangePassword(ChangePasswordDto model, CancellationToken cancellation);
        Task<ForgotPasswordDto> ForgotPassword(ForgotPasswordDto model, CancellationToken cancellation);
        Task<UserDto?> GetUserById(UserEntity user);
        Task<UserTableDto> GetUsers(string SearchValue, int Take, int Skip, string roleName, CancellationToken token);
    }
}

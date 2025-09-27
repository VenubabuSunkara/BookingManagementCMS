using Booking.Domain.Entities;

namespace Booking.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<UserEntity> Login(LoginEntity loginEntity);
        Task<UserEntity> Register(UserEntity userEntity);
        Task<UserEntity?> GetUserById(UserEntity user);
        Task<ForgotPasswordEntity> ForgotPassword(ForgotPasswordEntity model, CancellationToken cancellation);
        Task<bool> ResetPassword(ForgotPasswordEntity model, CancellationToken cancellation);
        Task<bool> ChangePassword(ChangePasswordEntity model, CancellationToken cancellation);
        Task<bool> ConfirmEmailAsync(string userId, string regToken, CancellationToken cancellation);
        Task LogOut(string UserId);
        Task<UserTableEntity> GetUsers(string SearchValue, int Take, int Skip, string roleName, CancellationToken token);
    }
}

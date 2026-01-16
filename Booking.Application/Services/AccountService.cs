using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;

namespace Booking.Application.Services
{
    public class AccountService(IAccountRepository accountRepository) : IAccountService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        public async Task<bool> ChangePassword(ChangePasswordDto model, CancellationToken cancellation)
        {
            return await _accountRepository.ChangePassword(new ChangePasswordEntity()
            {
                Email = model.Email,
                Password = model.Password,
                NewPassword = model.NewPassword,
                Token = model.Token,
                UserId = model.UserId,
                UserName = model.UserName,
            }, cancellation);
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string regToken, CancellationToken cancellation)
        {
            return await _accountRepository.ConfirmEmailAsync(userId, regToken, cancellation);
        }

        public async Task<ForgotPasswordDto> ForgotPassword(ForgotPasswordDto model, CancellationToken cancellation)
        {
            var res = await _accountRepository.ForgotPassword(new ForgotPasswordEntity()
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                Token = model.Token,
                UserId = model.UserId,
            }, cancellation);
            return new ForgotPasswordDto()
            {
                Email = res.Email,
                Password = res.Password,
                Token = res.Token,
                UserId = res.UserId,
                UserName = res.UserName
            };
        }

        public async Task<UserDto?> GetUserById(UserEntity user)
        {
            var userDto = await _accountRepository.GetUserById(user);
            if (userDto == null) return null;
            return new UserDto()
            {
                Id = userDto.Id,
                Email = userDto.Email,
                Username = userDto.Username,
                Contact = userDto.Contact,
            };
        }

        public async Task<UserTableDto> GetUsers(string SearchValue, int Take, int Skip, string roleName, CancellationToken token)
        {
            var users = await _accountRepository.GetUsers(SearchValue, Take, Skip, roleName, token);
            return new UserTableDto()
            {
                TotalRecords = users.TotalRecords,
                FilterRecords = users.FilterRecords,
                UsersDto = users.UserEntities
                .Select(x => new UserDto()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    ProfilePhoto = x.ProfilePhoto,
                    Contact = x.Contact,
                    FullName = x.FullName,
                    Username = x.Username,
                    Id = x.Id,
                    IsActive = x.IsActive
                })
            };
        }

        public async Task<UserDto?> Login(LoginEntity loginEntity)
        {
            var user = await _accountRepository.Login(loginEntity);
            if (user == null) return null;
            return new UserDto()
            {
                Address = user.Address,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Id = user.Id,
                RoleId = user.RoleId,
                Contact = user.Contact,
                Roles = user.Roles,
                ErrorMessges = user.ErrorMsaages
            };
        }

        public async Task LogOut(string UserId)
        {
            await _accountRepository.LogOut(UserId);
        }

        public async Task<UserDto?> Register(UserDto userDto)
        {
            var userEntity = await _accountRepository.Register(new UserEntity()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                RegistrationToken = userDto.RegistrationToken,
                Password = userDto.Password,
                Contact = userDto.Contact,
                RoleId = userDto.RoleId,
                ProfilePhoto = userDto.ProfilePhoto,
                Roles = userDto.Roles,
                TenantId = userDto.TenantId,
                Username = userDto.Username,
                CreatedBy = userDto.CreatedBy,
                UpdatedBy = userDto.UpdatedBy
            });
            return new UserDto()
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email,
                RegistrationToken = userEntity.RegistrationToken,
                Contact = userEntity.Contact,
                RoleId = userEntity.RoleId,
                ProfilePhoto = userEntity.ProfilePhoto,
                Roles = userEntity.Roles,
                TenantId = userEntity.TenantId,
                Username = userEntity.Username
            };
        }

        public async Task<bool> ResetPassword(ForgotPasswordDto model, CancellationToken cancellation)
        {
            return await _accountRepository.ResetPassword(new ForgotPasswordEntity()
            {
                Email = model.Email,
                Password = model.Password,
                Token = model.Token,
                UserName = model.UserName,
                UserId = model.UserId
            }, cancellation);
        }
    }
}

using Amazon.Runtime.Internal.UserAgent;
using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class AccountService(IAccountRepository accountRepository) : IAccountService
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        public Task<UserEntity> GetUserDetails(UserEntity user)
        {
            throw new NotImplementedException();
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
                Roles = user.Roles
            };
        }

        public async Task Register(UserEntity userEntity)
        {
            await accountRepository.Register(userEntity);
        }
    }
}

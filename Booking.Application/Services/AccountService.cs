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

        public async Task<bool> Login(string username, string password)
        {
            return await _accountRepository.Login(username, password);
        }

        public async Task<UserEntity> Register(UserEntity userEntity)
        {
            return await accountRepository.Register(userEntity);
        }
    }
}

using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class AccountRepository(BookingCmsContext context, IPasswordHasher<CompanyUser> passwordHasher) : IAccountRepository
    {
        private readonly BookingCmsContext _context = context;
        private readonly IPasswordHasher<CompanyUser> _passwordHasher = passwordHasher;

        public Task<UserEntity> GetUserDetails(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Login(string username, string password)
        {
            CompanyUser user = new CompanyUser { UserName = username };
            var dbUser = await _context.CompanyUsers.Where(x =>
            (x.UserName == user.UserName
            || x.Email == user.UserName
            || x.Contact == user.UserName)).FirstOrDefaultAsync();
            if (dbUser == null || dbUser.PasswordHashText == null) return false;
            //string passwordHash = dbUser.PasswordHashText;


            var result = _passwordHasher.VerifyHashedPassword(user, dbUser.PasswordHashText, password);

            return result != PasswordVerificationResult.Failed;
        }

        public async Task<UserEntity> Register(UserEntity userEntity)
        {
            CompanyUser user = new()
            {
                UserName = userEntity.Username,
                Email = userEntity.Email,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Address = userEntity.Address,
                TenantId = userEntity.TenantId,
                Contact = userEntity.Contact,
                IsActive = userEntity.IsActive,
            };
            string password = _passwordHasher.HashPassword(user, userEntity.Password);
            user.PasswordHashText = password;
            _context.CompanyUsers.Add(user);
            await _context.SaveChangesAsync();
            userEntity.Id = user.Id;
            return userEntity;
        }
    }
}

using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories
{
    public class AccountRepository(BookingCmsContext context,
        IPasswordHasher<CompanyUser> passwordHasher, UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager
        ) : IAccountRepository
    {
        private readonly BookingCmsContext _context = context;
        private readonly IPasswordHasher<CompanyUser> _passwordHasher = passwordHasher;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;

        public Task<UserEntity> GetUserDetails(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Login(string username, string password)
        {
            var results = await _signInManager.PasswordSignInAsync(username, password, true, lockoutOnFailure: false);

            CompanyUser user = new() { UserName = username };
            var dbUser = await _context.CompanyUsers.Where(x =>
            (x.UserName == user.UserName
            || x.Email == user.UserName
            || x.Contact == user.UserName)).FirstOrDefaultAsync();
            if (dbUser == null || dbUser.PasswordHashText == null) return false;
            var result = _passwordHasher.VerifyHashedPassword(user, dbUser.PasswordHashText, password);

            // _usermanager.GetUserAsync()

            return result != PasswordVerificationResult.Failed;
        }

        public async Task<UserEntity> Register(UserEntity userEntity)
        {
            var user = new IdentityUser()
            {
                UserName = userEntity.Username,
                Email = userEntity.Email,
                PhoneNumber = userEntity.Contact
            };
            var userResults = await _userManager.CreateAsync(user, userEntity.Password);
            if (userResults.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            var IUser = await _userManager.FindByNameAsync(user.UserName);
            var cmsuser = _context.CompanyUsers.AddAsync(new CompanyUser()
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                IsActive = userEntity.IsActive,
                Address = userEntity.Address,
                
            });

            //string password = _passwordHasher.HashPassword(user, userEntity.Password);
            //user.PasswordHashText = password;
            //_context.CompanyUsers.Add(user);
            //await _context.SaveChangesAsync();
            return new UserEntity()
            {
                Username = user.UserName,
                Id = user.Id,
                Email = user.Email
            };
        }
    }
}

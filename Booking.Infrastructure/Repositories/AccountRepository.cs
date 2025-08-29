using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using System.Text.Json;

namespace Booking.Infrastructure.Repositories
{
    public class AccountRepository(BookingCmsContext context,
        IPasswordHasher<CompanyUser> passwordHasher, UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager
        ) : IAccountRepository
    {
        private readonly BookingCmsContext _context = context;
        private readonly IPasswordHasher<CompanyUser> _passwordHasher = passwordHasher;
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public Task<UserEntity> GetUserDetails(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity?> Login(LoginEntity loginEntity)
        {
            var user = new IdentityUser()
            {
                UserName = loginEntity.Email,
                PasswordHash = loginEntity.Password
            };
            var result = await _signInManager.PasswordSignInAsync(user.UserName, loginEntity.Password, loginEntity.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded) { return null; }

            var userinfo = await _userManager.FindByEmailAsync(loginEntity.Email);
            if (userinfo == null) return null;
            var userinrole = await _userManager.IsInRoleAsync(userinfo, "Admin");
            if (!userinrole) return null;
            var userEntity = await _context.CompanyUsers.Where(x => x.UserId == userinfo.Id).FirstOrDefaultAsync();
            var roles = await _userManager.GetRolesAsync(user);
            /*Add Claims*/
            var finalUserdata = new UserEntity()
            {
                Username = user.UserName,
                Id = user.Id,
                Email = user.Email,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Contact = user.PhoneNumber,
                Address = userEntity.Address,
                Roles = [.. roles]
            };
            string userdata = JsonSerializer.Serialize(finalUserdata);
            await _userManager.AddClaimAsync(userinfo, new Claim(ClaimTypes.UserData, userdata));

            return finalUserdata;
        }

        public async Task Register(UserEntity userEntity)
        {
            if (!await roleManager.RoleExistsAsync(userEntity.RoleId))
            {
                await roleManager.CreateAsync(new IdentityRole(userEntity.RoleId));
            }
            var user = new IdentityUser()
            {
                UserName = userEntity.Username,
                Email = userEntity.Email,
                PhoneNumber = userEntity.Contact,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            await _userManager.CreateAsync(user, userEntity.Password);
            //if (userResults.Succeeded)
            //{
            //    await _signInManager.SignInAsync(user, isPersistent: false);
            //}
            var IUser = await _userManager.FindByNameAsync(user.UserName);
            await _context.CompanyUsers.AddAsync(new CompanyUser()
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                IsActive = userEntity.IsActive,
                Address = userEntity.Address,
                UserId = IUser?.Id,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                TenantId = userEntity.TenantId,
            });
            await _context.SaveChangesAsync();
            var results = await _userManager.AddToRoleAsync(user, userEntity.RoleId);
            if (results.Succeeded)
            {
             
            }
            //var roles = await _userManager.GetRolesAsync(user);
            //return new UserEntity()
            //{
            //    Username = user.UserName,
            //    Id = user.Id,
            //    Email = user.Email,
            //    FirstName = userEntity.FirstName,
            //    LastName = userEntity.LastName,
            //    Contact = user.PhoneNumber,
            //    Address = userEntity.Address,
            //    Roles = [.. roles]
            //};
        }
    }
}

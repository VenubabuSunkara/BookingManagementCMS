using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Booking.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<bool> ChangePassword(ChangePasswordEntity model, CancellationToken cancellation)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return true;
            }
            return false;
        }
        public async Task<bool> ConfirmEmailAsync(string userId, string regToken, CancellationToken cancellation)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, regToken);
            return result.Succeeded;
        }
        public async Task<ForgotPasswordEntity> ForgotPassword(ForgotPasswordEntity model, CancellationToken cancellation)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            model.Token = token;
            return model;
        }
        public async Task<UserEntity?> GetUserById(UserEntity user)
        {
            if (!string.IsNullOrEmpty(user.Id))
            {
                var identityuser = await _userManager.FindByIdAsync(user.Id);
                return new UserEntity()
                {
                    Id = identityuser?.Id ?? string.Empty,
                    Email = identityuser?.Email ?? string.Empty,
                    Username = identityuser?.UserName ?? string.Empty
                };
            }
            return null;

        }
        public async Task<UserTableEntity> GetUsers(string SearchValue, int Take, int Skip, string roleName, CancellationToken token)
        {
            var q = _context.AspNetUsers
                .Include(x => x.CompanyUser)
                .Include(x => x.Roles)
                .AsNoTracking();
            var total = await q.CountAsync(token);
            if (!string.IsNullOrWhiteSpace(SearchValue))
            {
                q = q.Where(d => d.UserName.Contains(SearchValue)
                || d.PhoneNumber.Contains(SearchValue)
                || d.Email.Contains(SearchValue));
            }
            q = q.OrderByDescending(d => d.CompanyUser.CreatedOn);
            var filtered = await q.CountAsync(token);
            var page = await q.Skip(Skip).Take(Take).ToListAsync(token);

            return new UserTableEntity()
            {
                TotalRecords = total,
                FilterRecords = filtered,
                UserEntities = [.. page.Select(x => new UserEntity()
                {
                    FirstName = x.CompanyUser?.FirstName ?? string.Empty,
                    LastName = x.CompanyUser?.LastName ?? string.Empty,
                    Address = x.CompanyUser?.Address,
                    Contact = x.PhoneNumber?? string.Empty,
                    Email = x.Email?? string.Empty,
                    Id = x.Id,
                    Username = x.UserName?? string.Empty,
                    IsActive = x.CompanyUser?.IsActive,
                    RoleId = string.Join(",", x.Roles.Select(r => r.Name))
                })]
            };
        }
        /// <summary>
        /// Login the user and return user details with claims
        /// Get the user and assign role 
        /// </summary>
        /// <param name="loginEntity"></param>
        /// <returns></returns>
        public async Task<UserEntity?> Login(LoginEntity loginEntity)
        {
            var user = new IdentityUser()
            {
                UserName = loginEntity.Email,
                PasswordHash = loginEntity.Password
            };

            var result = await _signInManager.PasswordSignInAsync(user.UserName, loginEntity.Password, loginEntity.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return new UserEntity()
                {
                    ErrorMsaages = [ResorceHelper.LoginUerNameAndPasswordNotMatch]
                };
            }

            var userinfo = await _userManager.FindByEmailAsync(loginEntity.Email);
            if (userinfo == null)
            {
                return new UserEntity()
                {
                    ErrorMsaages = [ResorceHelper.UserEmailNotFound]
                };
            }

            var userEntity = await _context.CompanyUsers.Where(x => x.UserId == userinfo.Id).FirstOrDefaultAsync();
            if (userEntity == null)
            {
                return new UserEntity()
                {
                    ErrorMsaages = [ResorceHelper.UserNotFound]
                };
            }
            var roles = await _userManager.GetRolesAsync(user);
            /*Add Claims*/
            var finalUserdata = new UserEntity()
            {
                Username = user.UserName,
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName ?? string.Empty,
                Contact = user.PhoneNumber ?? string.Empty,
                Address = userEntity.Address,
                Roles = [.. roles]
            };
            string userdata = JsonSerializer.Serialize(finalUserdata);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.UserData, userdata));
            return finalUserdata;
        }
        public async Task LogOut(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            await _signInManager.SignOutAsync();
            await _userManager.UpdateSecurityStampAsync(user);
        }
        public async Task<UserEntity> Register(UserEntity userEntity)
        {
            // ✅ Use cache or preload roles in memory if frequently reused
            if (!await _roleManager.RoleExistsAsync(userEntity.RoleId))
            {
                await _roleManager.CreateAsync(new IdentityRole(userEntity.RoleId));
            }

            var user = new IdentityUser
            {
                UserName = userEntity.Username,
                Email = userEntity.Email,
                PhoneNumber = userEntity.Contact,
                EmailConfirmed = true,  // Let email confirmation happen via token
                PhoneNumberConfirmed = true
            };

            var userResults = await _userManager.CreateAsync(user, userEntity.Password);

            if (!userResults.Succeeded)
            {
                // Optionally aggregate errors and throw/log here
                throw new InvalidOperationException(
                    $"User creation failed: {string.Join(", ", userResults.Errors.Select(e => e.Description))}");
            }

            // ✅ No need to re-fetch user from DB
            userEntity.Id = user.Id;

            // Generate token only if needed
            userEntity.RegistrationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Use a transaction for consistency
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var companyUser = new CompanyUser
                {
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    IsActive = userEntity.IsActive,
                    Address = userEntity.Address,
                    UserId = user.Id,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow,
                    TenantId = userEntity.TenantId,
                    CreatedBy = userEntity.CreatedBy,
                    UpdatedBy = userEntity.UpdatedBy
                };
                await _context.CompanyUsers.AddAsync(companyUser);
                await _context.SaveChangesAsync();
                await _userManager.AddToRoleAsync(user, userEntity.RoleId);
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

            return userEntity;
        }
        public async Task<bool> ResetPassword(ForgotPasswordEntity model, CancellationToken cancellation)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            return result.Succeeded;
        }
    }
}

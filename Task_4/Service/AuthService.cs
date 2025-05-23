using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Task_4.DTO;
using Task_4.Interface;
using Task_4.Models;

namespace Task_4.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User?> GetLoggedInUserAsync()
        {
            if (_httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated != true)
            {
                return null;
            }

            // Get the user name
            string? userName = _httpContextAccessor.HttpContext.User.Identity?.Name;

            if (userName == null)
            {
                return null;
            }

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return null;
            }
      
            return user;
        }

        public async Task<(bool Success, string ErrorMessage)> RegisterAsync(signupDTO dto)
        {
            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                Status = UserStatus.ACTIVE,
                RegisteredAt = DateTime.UtcNow
            };

            try
            {
                var result = await _userManager.CreateAsync(user, dto.Password);

                if (result.Succeeded)
                {
                    
                    return (true, null);
                }
                    

                // Combine Identity errors
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return (false, errors);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx)
                {
                    // 2601 and 2627 are SQL Server error numbers for unique constraint violation
                    if (sqlEx.Number == 2601 || sqlEx.Number == 2627)
                    {
                        return (false, "This email address is already registered.");
                    }
                }

                
                throw;
            }
        }

        public async Task<LoginResult> LoginAsync(signinDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return LoginResult.UserNotFound;

            if (user.Status == UserStatus.BLOCKED)
                return LoginResult.UserBlocked;

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                user.LastSeen = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);
                return LoginResult.Success;
            }

            return LoginResult.WrongPassword;
        }


        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}

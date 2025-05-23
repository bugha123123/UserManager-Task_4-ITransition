using System.Threading.Tasks;
using Task_4.DTO;
using Task_4.Models;

namespace Task_4.Interface
{
    public interface IAuthService
    {
        Task<(bool Success, string ErrorMessage)> RegisterAsync(signupDTO dto);
        Task<LoginResult> LoginAsync(signinDTO dto);
        Task LogoutAsync();

        Task<User?> GetLoggedInUserAsync();
    }
}

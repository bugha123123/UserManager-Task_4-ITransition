using Task_4.Models;

namespace Task_4.Interface
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers(); 
    }
}

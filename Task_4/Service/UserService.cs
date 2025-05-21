using Microsoft.EntityFrameworkCore;
using Task_4.Interface;
using Task_4.Models;

namespace Task_4.Service
{
    public class UserService : IUserService
    {
        private readonly DbContextion _dbcontext;
public UserService(DbContextion dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<User>> GetAllUsers(string? query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                return await _dbcontext.Users.Where(x => x.Email.Contains(query)|| x.UserName.Contains(query)).ToListAsync();
            }

            return await _dbcontext.Users.ToListAsync();
        }
    }
}

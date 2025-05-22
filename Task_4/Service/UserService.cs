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

        public async Task ActivateUser(string[] SelectedUserIds)
        {
            if (SelectedUserIds == null || SelectedUserIds.Length == 0)
                return;

            var users = _dbcontext.Users.Where(x => SelectedUserIds.Contains(x.Id));

            foreach (var user in users)
            {
                user.Status = UserStatus.ACTIVE;
            }

            await _dbcontext.SaveChangesAsync();

        }

        public async Task BlockUser(string[] SelectedUserIds)
        {
            if (SelectedUserIds == null || SelectedUserIds.Length == 0)
                return;

            var users = _dbcontext.Users.Where(x => SelectedUserIds.Contains(x.Id));

            foreach (var user in users)
            {
                if (user.Status != UserStatus.BLOCKED)
                {
                    user.Status = UserStatus.BLOCKED;
                }
                
            }

            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteUser(string[] SelectedUserIds)
        {
            if (SelectedUserIds == null || SelectedUserIds.Length == 0)
                return;

            var users = _dbcontext.Users.Where(u => SelectedUserIds.Contains(u.Id));
            _dbcontext.Users.RemoveRange(users);
            await _dbcontext.SaveChangesAsync();
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

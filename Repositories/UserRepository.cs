using AzureOcrFlutterAPI.Data;
using AzureOcrFlutterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureOcrFlutterAPI.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByName(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name.Equals(name));
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                if (user.Name != null)
                {
                    existingUser.Name = user.Name;
                }
                if (user.Password != null)
                {
                    existingUser.Password = user.Password;
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}

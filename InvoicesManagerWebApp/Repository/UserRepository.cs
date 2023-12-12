using InvoicesManagerWebApp.Data;
using InvoicesManagerWebApp.Interface;
using InvoicesManagerWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoicesManagerWebApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context= context;
        }
        public async Task<User> GetUserById(string userId)
            =>  await _context.Users.FindAsync(userId);

        public Task Delete(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}

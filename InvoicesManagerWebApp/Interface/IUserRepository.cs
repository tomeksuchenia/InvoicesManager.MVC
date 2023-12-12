using InvoicesManagerWebApp.Models;

namespace InvoicesManagerWebApp.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserById(string userId);
        Task Delete(int userId);
        Task Update(User user);
    }
}

using InvoicesManagerWebApp.Models;

namespace InvoicesManagerWebApp.Interface
{
    public interface IUserService
    {
        Task Update(User user);
    }
}

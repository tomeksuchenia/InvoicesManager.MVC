using InvoicesManagerWebApp.Interface;
using InvoicesManagerWebApp.Models;

namespace InvoicesManagerWebApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task Update(User user)
        {
            var userDb = await _userRepository.GetUserById(user.Id);

            userDb.CompanyName = user.CompanyName;
            userDb.Address = user.Address;
            userDb.TaxId = user.TaxId;
            userDb.TelephoneNumber = user.TelephoneNumber;

            await _userRepository.Update(userDb);
        }
    }
}

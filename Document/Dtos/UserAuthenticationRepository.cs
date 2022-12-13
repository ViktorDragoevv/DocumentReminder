using Document.Models;
using Microsoft.AspNetCore.Identity;

namespace Document.Dtos
{
    internal sealed class UserAuthenticationRepository
    {
        private readonly UserManager<User> _userManager;
        public UserAuthenticationRepository(
        UserManager<User> userManager)
        {
            _userManager = userManager;
        }


    }
}
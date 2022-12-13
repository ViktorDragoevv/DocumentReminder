using Document.Models;
using Microsoft.AspNetCore.Identity;

namespace Document.Dtos
{
    public interface IUserAuthenticationRepository
    {
        Task<IdentityResult> RegisterUserAsync(User userForRegistration);
    }
}

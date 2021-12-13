using EC2_1601226.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EC2_1601226.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);

        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);

        Task SignOutAsync();
    }
}
using MicroondasMVC_Benner.Models.API;

namespace MicroondasMVC_Benner.Repository.Auth
{
    public interface IAuthInterface
    {
        public Task<UserAuthModel> CadastrarUser(UserAuthModel user);
        public Task<string> Login(UserAuthModel user);
    }
}

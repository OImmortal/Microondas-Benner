using MicroondasMVC_Benner.Models.API;
using MicroondasMVC_Benner.Repository.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Cryptography;
using System.Text;

namespace MicroondasMVC_Benner.Repository.Auth
{
    public class AuthService : IAuthInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<UserAuthModel> CadastrarUser(UserAuthModel user)
        {
            try
            {
                string username = user.User!;
                string hashedPassword = HashPasswordWithSHA1(user.Senha!);

                UserAuthModel newUser = new UserAuthModel
                {
                    User = username,
                    Senha = hashedPassword
                };

                await _context.UserAuth.AddAsync(newUser);

                await _context.SaveChangesAsync();

                return newUser;

            } catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<string> Login(UserAuthModel user)
        {
            try
            {
                var hashDaSenhaDigitada = HashPasswordWithSHA1(user.Senha!);
                UserAuthModel existUser = await _context.UserAuth.FirstOrDefaultAsync(u => u.User == user.User && u.Senha == hashDaSenhaDigitada);

                if (existUser != null)
                {
                    var secretKey = _configuration["Jwt:Key"];
                    string token = TokenService.GenerateToken(existUser, secretKey!);

                    return token;
                }

                return "";
            } catch(Exception Ex)
            {
               throw Ex;
            }
  
        }

        private string HashPasswordWithSHA1(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);

                
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}

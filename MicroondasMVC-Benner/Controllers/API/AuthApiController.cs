using MicroondasMVC_Benner.Models.API;
using MicroondasMVC_Benner.Repository.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroondasMVC_Benner.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {

        private readonly IAuthInterface _authInterface;
        public AuthApiController(IAuthInterface authInterface)
        {
            _authInterface = authInterface;
        }

        [HttpPost("CadastrarUser")]
        public async Task<IActionResult> CadastrarNovoUser(UserAuthModel model)
        {
            ResponseModel<UserAuthModel> response = new ResponseModel<UserAuthModel>();
            try
            {
                UserAuthModel user = await _authInterface.CadastrarUser(model);
                if (user != null)
                {
                    response.SetSucesso(user, 200);
                    return StatusCode(response.Codigo, response);
                }

                response.SetErro("Erro ao cadastrar usuário", 400);
                return StatusCode(response.Codigo, response);
            }
            catch (Exception ex)
            {
                response.SetErro(ex.Message, 500);
                return BadRequest(response);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserAuthModel model)
        {
            ResponseModel<string> response = new ResponseModel<string>();
            try
            {
                string token = await _authInterface.Login(model);
                if (!string.IsNullOrEmpty(token))
                {
                    response.SetSucesso(token, 200);
                    return StatusCode(response.Codigo, response);
                }
                response.SetErro("Usuário ou senha inválidos", 401);
                return StatusCode(response.Codigo, response);
            }
            catch (Exception ex)
            {
                response.SetErro(ex.Message, 500);
                return BadRequest(response);
            }
        }

    }
}

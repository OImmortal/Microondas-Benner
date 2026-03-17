using MicroondasMVC_Benner.Models.API;
using MicroondasMVC_Benner.Repository.Auth;
using Microsoft.AspNetCore.Mvc;

namespace MicroondasMVC_Benner.Controllers.Web
{
    public class UserCadastroController : Controller
    {
        private readonly IAuthInterface _authInterface;

        // Injeta o serviço de autenticação pra cadastrar/logar sem acoplar na controller.
        public UserCadastroController(IAuthInterface authInterface)
        {
            _authInterface = authInterface;
        }

        [HttpPost]
        // Recebe o formulário e tenta cadastrar o usuário.
        public async Task<IActionResult> CadastrarNovoUser(UserAuthModel model)
        {
            try
            {
               await _authInterface.CadastrarUser(model);
            } catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(model);
            }
            return RedirectToAction("Index", "MicroOndas");
        }

        [HttpGet]
        // Abre a tela de cadastro de usuário.
        public IActionResult CadastrarNovoUser()
        {
            return View();
        }
    }
}

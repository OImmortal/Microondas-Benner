using MicroondasMVC_Benner.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace MicroondasMVC_Benner.Controllers
{
    public class MicroOndasController : Controller
    {
        public static MicroOndasModel microOndas = new MicroOndasModel();

        [HttpPost]
        public IActionResult Iniciar(int contador, int potencia)
        {
            microOndas.Iniciar(contador, potencia);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            
            
            return View(microOndas);
        }
    }
}

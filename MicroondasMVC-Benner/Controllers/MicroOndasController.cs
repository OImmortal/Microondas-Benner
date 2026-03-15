using MicroondasMVC_Benner.Models.Microondas;
using MicroondasMVC_Benner.Models.PreAquecimento;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using static System.Reflection.Metadata.BlobBuilder;

namespace MicroondasMVC_Benner.Controllers
{
    public class MicroOndasController : Controller
    {
        public static MicroOndasModel microOndas = new MicroOndasModel();

        [HttpPost]
        public IActionResult IniciarPreAquecimento(int idPreAquecimento)
        {
            microOndas.IniciarPreAquecimento(idPreAquecimento);
            return RedirectToAction("Index");
        }



        [HttpPost]
        public IActionResult Iniciar(int contador, int potencia)
        {
            if (contador == 0) contador = 30;
            if (potencia == 0) potencia = 10;

            microOndas.Iniciar(contador, potencia);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult PausarECancelar(int contador)
        {
            microOndas.PausarECancelar(contador);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Cancelar()
        {
            microOndas.Cancelar();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            
            
            return View(microOndas);
        }
    }
}

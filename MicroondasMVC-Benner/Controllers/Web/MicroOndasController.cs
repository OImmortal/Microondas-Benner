using MicroondasMVC_Benner.Models.Microondas;
using MicroondasMVC_Benner.Models.PreAquecimento;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using static System.Reflection.Metadata.BlobBuilder;

namespace MicroondasMVC_Benner.Controllers.Web
{
    public class MicroOndasController : Controller
    {
        public static MicroOndasModel microOndas = new MicroOndasModel();

        [HttpPost]
        // Começa um pré-aquecimento já cadastrado.
        public IActionResult IniciarPreAquecimento(int idPreAquecimento)
        {
            microOndas.IniciarPreAquecimento(idPreAquecimento);
            return RedirectToAction("Index");
        }



        [HttpPost]
        // Inicia o micro-ondas com tempo/potência (e aplica defaults quando vem zerado).
        public IActionResult Iniciar(int contador, int potencia)
        {
            if (contador == 0) contador = 30;
            if (potencia == 0) potencia = 10;

            microOndas.Iniciar(contador, potencia);
            return RedirectToAction("Index");
        }

        [HttpPost]
        // Alterna entre pausar/cancelar conforme o estado atual do micro-ondas.
        public IActionResult PausarECancelar(int contador)
        {
            microOndas.PausarECancelar(contador);
            return RedirectToAction("Index");
        }


        [HttpPost]
        // Cancela tudo e reseta o micro-ondas.
        public IActionResult Cancelar()
        {
            microOndas.Cancelar();
            return RedirectToAction("Index");
        }

        [HttpPost]
        // Atalho pra tela de cadastro de um novo pré-aquecimento.
        public IActionResult CriarNovoItem()
        {
            return RedirectToAction("Cadastro", "PreAquecimento");
        }


        // Renderiza a tela principal com o estado atual do micro-ondas.
        public IActionResult Index()
        {
            
            
            return View(microOndas);
        }
    }
}

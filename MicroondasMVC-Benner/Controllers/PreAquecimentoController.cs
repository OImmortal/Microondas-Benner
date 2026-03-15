using MicroondasMVC_Benner.Models.Microondas;
using MicroondasMVC_Benner.Models.PreAquecimento;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.IO;

namespace MicroondasMVC_Benner.Controllers
{
    public class PreAquecimentoController : Controller
    {

        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(PreAquecimentoModel preAquecimento)
        {
            var microondas = MicroOndasController.microOndas;

            preAquecimento.id = microondas.preAquecimentos.Count + 1;

            if (microondas.ContainsCaracter(preAquecimento.CaractereAquecimento!))
            {
                ViewBag.Mensagem = "Este caractere já está em uso!";
                return View(preAquecimento); // Recomendo passar o model de volta para não limpar os campos
            }

            if (preAquecimento.Tempo <= 0 || preAquecimento.Potencia <= 0)
            {
                ViewBag.Mensagem = "Tempo e potência devem ser maiores que zero!";
                return View(preAquecimento);
            }

            if (preAquecimento.Potencia > 10)
            {
                ViewBag.Mensagem = "A potência deve ser entre 1 e 10!";
                return View(preAquecimento);
            }

            if (string.IsNullOrWhiteSpace(preAquecimento.Nome) || string.IsNullOrWhiteSpace(preAquecimento.Alimento))
            {
                ViewBag.Mensagem = "Nome e alimento são obrigatórios!";
                return View(preAquecimento);
            }

            preAquecimento.NovoItem = true;

            string caminhoJson = Path.Combine("Config", "PreAquecimentoItens.Json");

            string conteudo = System.IO.File.ReadAllText(caminhoJson);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Use as options no Deserialize
            List<PreAquecimentoModel> aquecimentos = [];

            if (conteudo != "")
            {
                aquecimentos = JsonSerializer.Deserialize<List<PreAquecimentoModel>>(conteudo, options)!;
            }

            aquecimentos.Add(preAquecimento);

            string jsonString = JsonSerializer.Serialize(aquecimentos);

            System.IO.File.WriteAllText(caminhoJson, jsonString);

            microondas.preAquecimentos.Add(preAquecimento);
            return RedirectToAction("Index", "MicroOndas");

        }
    }
}

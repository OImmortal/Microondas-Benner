using MicroondasMVC_Benner.Models.PreAquecimento;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Packaging;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MicroondasMVC_Benner.Models.Microondas
{
    public class MicroOndasModel
    {
        [Required(ErrorMessage = "O campo 'Potência' e 'Contador' é obrigatório.")]
        public Display? display { get; set; } = new Display();
        public List<Blocos> blocos { get; set; } = [];
        public bool Ligado { get; set; } = false;
        public bool Pausado { get; set; } = false;
        public List<PreAquecimentoModel> preAquecimentos { get; set; } = [];
        public bool PreAquecimentoActive { get; set; }
        public string? ResultTXT { get; set; }

        private const int LIMITE_CONTADOR = 120;
        private string tempCaracterAquecimento = "";

        public MicroOndasModel()
        {
            this.preAquecimentos = new List<PreAquecimentoModel>()
            {
                new PreAquecimentoModel(1, "Pipoca de Microondas", "Pipoca", 180, 7, "*", "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento."),
                new PreAquecimentoModel(2, "Leite", "Leite", 300, 5, "@", "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras."),
                new PreAquecimentoModel(3, "Carne em pedaço ou fatias de Microondas", "Carne", 840, 4, "#", "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."),
                new PreAquecimentoModel(4, "Frango (qualquer corte)", "Frango", 480, 7, "$", "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."),
                new PreAquecimentoModel(5, "Feijão congelado", "Feijão", 480, 9, "%", "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas."),
            };

            List<PreAquecimentoModel> preAquecimentosAdicionados = new List<PreAquecimentoModel>();

            string caminhoJson = Path.Combine("Config", "PreAquecimentoItens.Json");

            if(File.Exists(caminhoJson))
            {
                string conteudo = File.ReadAllText(caminhoJson);

                if (conteudo == "")
                {
                    return;
                }


                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                List<PreAquecimentoModel> aquecimentos = JsonSerializer.Deserialize<List<PreAquecimentoModel>>(conteudo, options)!;
                if (aquecimentos != null)
                {
                    foreach (PreAquecimentoModel preAquecimento in aquecimentos)
                    {
                        preAquecimentos.Add(preAquecimento);
                    }
                }
            }
        }

        public void IniciarPreAquecimento(int idPreAquecimento)
        {
            var preAquecimentoSelecionado = preAquecimentos.FirstOrDefault(p => p.id == idPreAquecimento);
            if (preAquecimentoSelecionado != null)
            {
                this.tempCaracterAquecimento = preAquecimentoSelecionado.CaractereAquecimento!;
                Iniciar(preAquecimentoSelecionado.Tempo, preAquecimentoSelecionado.Potencia);
            }

            PreAquecimentoActive = true;
        }

        public void Iniciar(int contador, int potencia)
        {

            if (display != null)
            {

                if (Ligado && !Pausado && display.contador < LIMITE_CONTADOR)
                {
                    if (display!.contador + contador > LIMITE_CONTADOR)
                    {
                        display.contador = LIMITE_CONTADOR;
                    }
                    else
                    {
                        display.contador += contador;
                    }
                    blocos = gerarBloco(tempCaracterAquecimento);
                    return;
                }

                if (Ligado && Pausado)
                {
                    blocos = gerarBloco(tempCaracterAquecimento);
                    Pausado = false;
                    return;
                }

                Ligado = true;
                display.contador = contador;
                display.potencia = potencia;

                blocos = gerarBloco(tempCaracterAquecimento);

            }
        }

        public void PausarECancelar(int contador)
        {
            if (Ligado && !Pausado)
            {
                if (display != null)
                {
                    display.contador = contador;
                    blocos = gerarBloco(tempCaracterAquecimento);
                }

                Pausado = true;
                return;
            }

            if ((Ligado && Pausado) || (!Ligado && !Pausado))
            {
                Cancelar();
            }
        }

        public void Cancelar()
        {
            Ligado = false;
            Pausado = false;
            PreAquecimentoActive = false;
            tempCaracterAquecimento = ".";
            if (display != null)
            {
                display.contador = 0;
                display.potencia = 0;
            }
            blocos.Clear();
        }

        public string ObterTempoFormatado()
        {
            if (display != null)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(display!.contador);
                return timeSpan.ToString(@"mm\:ss");
            }

            return "00:00";
        }

        public bool ContainsCaracter(string caracter)
        {
            bool contains = preAquecimentos.Any(p => p.CaractereAquecimento == caracter) || caracter == ".";
            return contains;
        }

        private List<Blocos> gerarBloco(string caracter = "")
        {
            List<Blocos> blocosGerados = new List<Blocos>();
            for (int i = 0; i < display!.contador; i++)
            {
                Blocos bloco = new Blocos(display!.potencia, caracter);
                blocosGerados.Add(bloco);
            }

            return blocosGerados;
        }

    }
}

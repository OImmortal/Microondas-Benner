using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace MicroondasMVC_Benner.Models
{
    public class MicroOndasModel
    {
        [Required(ErrorMessage = "O campo 'Potência' e 'Contador' é obrigatório.")]
        public Display? display { get; set; } = new Display();
        public List<Blocos> blocos { get; set; } = [];
        public bool Ligado { get; set; } = false;
        public bool Pausado { get; set; } = false;
        public string? ResultTXT { get; set; }

        private const int LIMITE_CONTADOR = 120;

        public async void Iniciar(int contador, int potencia)
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
                    blocos = gerarBloco();
                    return;
                }

                if (Ligado && Pausado && display.contador <= LIMITE_CONTADOR)
                {
                    blocos = gerarBloco();
                    Pausado = false;
                    return;
                }

                Ligado = true;
                display.contador = contador;
                display.potencia = potencia;

                blocos = gerarBloco();

            }
        }

        public void PausarECancelar(int contador)
        {
            if (Ligado && !Pausado)
            {
                if (display != null)
                {
                    display.contador = contador;
                    blocos = gerarBloco();
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

        private List<Blocos> gerarBloco()
        {
            List<Blocos> blocosGerados = new List<Blocos>();
            for (int i = 0; i < display!.contador; i++)
            {
                Blocos bloco = new Blocos(display!.potencia);
                blocosGerados.Add(bloco);
            }

            return blocosGerados;
        }

    }
}

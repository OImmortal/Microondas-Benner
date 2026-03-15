using System.ComponentModel.DataAnnotations;

namespace MicroondasMVC_Benner.Models
{
    public class MicroOndasModel
    {
        [Required(ErrorMessage = "O campo 'Potência' e 'Contador' é obrigatório.")]
        public Display? display { get; set; } = new Display();
        public bool Ligado { get; set; } = false;

        public void Iniciar(int contador, int potencia)
        {
            Ligado = true;
            if (display != null)
            {
                if (Ligado)
                {
                    display.contador = contador;
                }
            }
        }

        public string ObterTempoFormatado()
        {
            if (display != null)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(30);
                return timeSpan.ToString(@"mm\:ss");
            }

            return "00:00";
        }
    }
}

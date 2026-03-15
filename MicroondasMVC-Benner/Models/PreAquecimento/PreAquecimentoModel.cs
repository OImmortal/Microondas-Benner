using MicroondasMVC_Benner.Models.Microondas;
using System.ComponentModel.DataAnnotations;

namespace MicroondasMVC_Benner.Models.PreAquecimento
{
    public class PreAquecimentoModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string? Nome { get; set; }
        [Required]
        public string? Alimento { get; set; }
        [Required]
        public int Tempo { get; set; }
        [Required]
        public int Potencia { get; set; }
        [Required]
        public string? CaractereAquecimento { get; set; }
        [Required]
        public string? InstrucoesComplementares { get; set; }

        public PreAquecimentoModel(int id, string nome, string alimento, int tempo, int potencia, string CaractereAquecimento, string desc)
        {
            this.id = id;
            this.Nome = nome;
            this.Alimento = alimento;
            this.Tempo = tempo;
            this.Potencia = potencia;
            this.CaractereAquecimento = CaractereAquecimento;
            this.InstrucoesComplementares = desc;
        }

        public string ObterTempoFormatado()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(Tempo);
            return timeSpan.ToString(@"mm\:ss");
        }

    }
}

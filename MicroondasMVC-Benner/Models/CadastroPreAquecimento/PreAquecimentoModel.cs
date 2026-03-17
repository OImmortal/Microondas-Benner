using MicroondasMVC_Benner.Models.Microondas;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MicroondasMVC_Benner.Models.PreAquecimento
{
    public class PreAquecimentoModel
    {

        [JsonIgnore]
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
        public string? InstrucoesComplementares { get; set; }
        [JsonIgnore]
        public bool NovoItem { get; set; } = false;

        public PreAquecimentoModel()
        {
            
        }

        // Cria um preset já com id (normalmente os itens base/seed).
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

        // Cria um preset sem id (id costuma ser preenchido na hora de salvar).
        public PreAquecimentoModel(string nome, string alimento, int tempo, int potencia, string CaractereAquecimento, string desc)
        {
            this.Nome = nome;
            this.Alimento = alimento;
            this.Tempo = tempo;
            this.Potencia = potencia;
            this.CaractereAquecimento = CaractereAquecimento;
            this.InstrucoesComplementares = desc;
        }

        // Só formata o tempo do preset pra mostrar na tela.
        public string ObterTempoFormatado()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(Tempo);
            return timeSpan.ToString(@"mm\:ss");
        }

    }
}

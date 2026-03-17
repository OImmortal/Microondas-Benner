namespace MicroondasMVC_Benner.Models.Microondas
{
    public class Blocos
    {
        public string Caracter = ".";

        public string BlocoCompleto { get; set; }

        // Monta um “bloco” de aquecimento baseado na potência e no caractere escolhido.
        public Blocos(int pontencia, string caracter = "")
        {
            BlocoCompleto = GerarBloco(pontencia, caracter);
        }

        // Gera a string repetindo o caractere o número de vezes da potência.
        private string GerarBloco(int potencia, string caracter = "")
        {
            this.Caracter = caracter;

            string bloco = "";
            for (int i = 0; i < potencia; i++)
            {
                bloco += Caracter;
            }

            return bloco;
        }
    }
}
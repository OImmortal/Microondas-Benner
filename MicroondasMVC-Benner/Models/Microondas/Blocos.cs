namespace MicroondasMVC_Benner.Models.Microondas
{
    public class Blocos
    {
        public string Caracter = ".";

        public string BlocoCompleto { get; set; }

        public Blocos(int pontencia, string caracter = "")
        {
            BlocoCompleto = GerarBloco(pontencia, caracter);
        }

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
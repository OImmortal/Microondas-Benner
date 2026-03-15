namespace MicroondasMVC_Benner.Models
{
    public class Blocos
    {
        private const string Caracter = ".";

        public string BlocoCompleto { get; set; }

        public Blocos(int pontencia)
        {
            BlocoCompleto = GerarBloco(pontencia);
        }

        private string GerarBloco(int potencia)
        {
            string bloco = "";
            for (int i = 0; i < potencia; i++)
            {
                bloco += ".";
            }

            return bloco;
        }
    }
}
using MicroondasMVC_Benner.Models.Microondas;
using MicroondasMVC_Benner.Models.PreAquecimento;

namespace MicroondasMVC_Benner.Repository.MicroOndas
{
    public interface IMicroOndasInterface
    {
        public MicroOndasModel? IniciarOuAdicionar(int? Tempo, int? Potencia);
        public MicroOndasModel? PausarOuCancelar(int Tempo);
        public MicroOndasModel? IniciarPreAquecimento(int IdPreAquecimento);
        public PreAquecimentoModel? CadastrarPreAquecimento(PreAquecimentoModel model);
    }
}

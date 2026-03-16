namespace MicroondasMVC_Benner.Repository.MicroOndas
{
    public interface IMicroOndasInterface
    {
        public Task IniciarOuAdicionar();
        public Task PausarOuCancelar();
        public Task IniciarPreAquecimento();
        public Task CadastrarPreAquecimento();
    }
}

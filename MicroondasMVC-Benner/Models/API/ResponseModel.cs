namespace MicroondasMVC_Benner.Models.API
{
    public class ResponseModel<T>
    {
        public int Codigo { get; set; }
        public dynamic? Erro { get; set; }
        public T? Data { get; set; }
        public bool Sucesso { get; set; }

        // Preenche o response como sucesso, com payload e status code.
        public void SetSucesso(T Data, int Codigo)
        {
            this.Data = Data;
            this.Codigo = Codigo;
            this.Sucesso = true;
        }

        // Preenche o response como erro, com mensagem/objeto e status code.
        public void SetErro(dynamic erro, int Codigo)
        {
            this.Erro = erro;
            this.Codigo = Codigo;
            this.Sucesso = false;
        }

    }
}

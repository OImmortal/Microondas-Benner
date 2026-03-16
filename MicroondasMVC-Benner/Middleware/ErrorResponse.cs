using Humanizer;
using System;

namespace MicroondasMVC_Benner.Middleware
{
    public class ErrorResponse
    {
        public string? Exception { get; set; }
        public string? InnerException { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public string? Outros { get; set; }

        public string toString()
        {
            string contudo = $"--- ERRO REGISTRADO EM: {DateTime.Now} ---\n" +
                             $"Exception: {Exception}\n" +
                             $"Mensagem: {Message}\n" +
                             $"Inner Exception: {InnerException ?? "Nenhuma"}\n" +
                             $"StackTrace: {StackTrace}\n" +
                             $"Outros: {Outros}\n\n";
            return contudo;
        }
    }
}

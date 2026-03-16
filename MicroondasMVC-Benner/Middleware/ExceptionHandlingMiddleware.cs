using MicroondasMVC_Benner.Models.API;
using System.Net;
using System.Text.Json;

namespace MicroondasMVC_Benner.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Items.TryGetValue("ExceptionToLog", out var exceptionObj) && exceptionObj is Exception handledException)
                {
                    await SalvarLogNoArquivoAsync(handledException);
                }
            }
            catch (Exception ex)
            {
                await SalvarLogNoArquivoAsync(ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task SalvarLogNoArquivoAsync(Exception exception)
        {
            string pastaLogs = "Logs";
            if (!Directory.Exists(pastaLogs))
            {
                Directory.CreateDirectory(pastaLogs);
            }

            string pathLog = Path.Combine(pastaLogs, $"Log_{DateTime.Now.ToString("dd-MM-yyyy")}.txt");

            ErrorResponse response = new ErrorResponse();
            response.Exception = exception.GetType().Name;
            response.InnerException = exception.InnerException?.Message ?? "Nenhuma";
            response.StackTrace = exception.StackTrace;
            response.Message = exception.Message;

            if (exception is RegraMicroOndasException)
                response.Outros = "Regra de negócio violada";
            else
                response.Outros = "Ocorreu um erro interno inesperado no servidor.";

            string conteudoLog = response.toString();
            await File.AppendAllTextAsync(pathLog, conteudoLog);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new ErrorResponse
            {
                Message = exception.Message,
                Exception = exception.GetType().Name
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}

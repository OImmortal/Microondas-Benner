namespace MicroondasMVC_Benner.Models.API
{
    public class RegraMicroOndasException : Exception
    {
        public RegraMicroOndasException()
        {
            
        }
        public RegraMicroOndasException(string? message) : base(message)
        {
        }

        public RegraMicroOndasException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

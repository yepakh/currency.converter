namespace Currency.Converter.Common
{
    public class ErrorModel
    {
        public ErrorModel(string message, string exception)
        {
            Message = message;
            Exception = exception;
        }

        public string Message { get; set; }
        public string Exception { get; set; }
    }
}

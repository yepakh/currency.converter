namespace Currency.Converter.Common
{
    public class RequestModel
    {
        public string StringToConvert { get; set; }

        public RequestModel(string stringToConvert)
        {
            StringToConvert = stringToConvert;
        }
    }
}
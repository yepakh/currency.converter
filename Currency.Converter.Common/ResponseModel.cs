namespace Currency.Converter.Common
{
    public class ResponseModel
    {
        public string ConversionResult { get; set; }

        public ResponseModel(string conversionResult)
        {
            ConversionResult = conversionResult;
        }
    }
}

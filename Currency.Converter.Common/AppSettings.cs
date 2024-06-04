namespace Currency.Converter.Common
{
    public class AppSettings
    {
        public const string ServerBaseAddress = "http://localhost:8080/";
        public const string ConvertEndpointSubPath = "Convert";
        public const string FullConvertEndpointAddress = ServerBaseAddress + ConvertEndpointSubPath + "/";
    }
}

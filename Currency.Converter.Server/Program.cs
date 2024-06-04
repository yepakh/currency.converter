using Currency.Converter.Common;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;

namespace Currency.Converter.Server
{
    internal class Program
    {
        private static readonly HttpListener _httpListenter = new HttpListener();

        static void Main(string[] args)
        {
            _httpListenter.Prefixes.Add(AppSettings.FullConvertEndpointAddress); // Listen on 51111
            _httpListenter.Start();

            Console.WriteLine($"Started listening to {AppSettings.FullConvertEndpointAddress}");
            Task.Run(() => StartHandlingRequests());
            Console.ReadLine();
        }

        private static Task StartHandlingRequests()
        {
            while(true)
            {
                var context = _httpListenter.GetContext();

                var jsonBody = new StreamReader(context.Request.InputStream).ReadToEnd();
                Console.WriteLine($"Received conversion request. Content: {jsonBody}");
                HandleRequest(context, jsonBody);
            }
        }

        private static void HandleRequest(HttpListenerContext context, string jsonBody)
        {
            string responseString = String.Empty;
            try
            {
                var request = JsonSerializer.Deserialize<RequestModel>(jsonBody);
                var result = new ResponseModel(BL.Converter.Convert(request.StringToConvert));
                context.Response.StatusCode = 200;
                responseString = JsonSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                responseString = JsonSerializer.Serialize(new ErrorModel("Something went wrong.", ex.Message));
                context.Response.StatusCode = 500;
            }
            finally
            {
                byte[] responseBytes = Encoding.UTF8.GetBytes(responseString);
                context.Response.KeepAlive = false;
                context.Response.ContentLength64 = responseBytes.Length;

                var output = context.Response.OutputStream;
                output.Write(responseBytes, 0, responseBytes.Length);
                context.Response.Close();
            }
        }
    }
}
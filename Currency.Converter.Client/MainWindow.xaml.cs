using Currency.Converter.Common;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Media;

namespace Currency.Converter.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient httpClient;

        public MainWindow()
        {
            InitializeComponent();
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri(AppSettings.ServerBaseAddress),
            };
            httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var text = Input.Text;
            var request = new RequestModel(text);
            StringContent jsonContent = new(JsonSerializer.Serialize(request));

            HttpResponseMessage response = httpClient.PostAsync(AppSettings.ConvertEndpointSubPath, jsonContent).GetAwaiter().GetResult();
            var jsonResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            string displayData;
            Brush displayColor;
            if (response.IsSuccessStatusCode)
            {
                var responseModel = JsonSerializer.Deserialize<ResponseModel>(jsonResponse);
                displayData = responseModel.ConversionResult;
                displayColor = Brushes.Black;
            }
            else
            {
                var responseModel = JsonSerializer.Deserialize<ErrorModel>(jsonResponse);
                displayData = "Exception: " + responseModel.Exception;
                displayColor = Brushes.MediumVioletRed;
            }

            ResultBlock.Text = displayData;
            ResultBlock.Foreground = displayColor;
        }

        private void Input_GotFocus(object sender, RoutedEventArgs e)
        {
            LabelPlaceholder.Foreground = Brushes.White;
        }

        private void Input_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Input.Text.Length == 0)
            {
                LabelPlaceholder.Foreground = Brushes.Black;
            }
        }
    }
}

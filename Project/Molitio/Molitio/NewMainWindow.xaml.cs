using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Molitio
{
    /// <summary>
    /// Interaction logic for NewMainWindow.xaml
    /// </summary>
    public partial class NewMainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly DispatcherTimer timer;

        public NewMainWindow()
        {
            InitializeComponent();
            GetJokesDataFromAPI();
            // Initialize the timer
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(10)
            };
            timer.Tick += Timer_Tick;

            // Start the timer
            timer.Start();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            // Call the API and update UI with a new joke
            GetJokesDataFromAPI();
        }

        private async void GetJokesDataFromAPI()
        {
            string apiUrl = "https://v2.jokeapi.dev/joke/Any?blacklistFlags=nsfw,religious,racist,sexist,explicit&format=txt&type=single";

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string joke = await response.Content.ReadAsStringAsync();
                    // Update your UI with the retrieved joke
                    lbJokes.Content = joke;
                }
                else
                {
                    // Handle error
                    MessageBox.Show("Error fetching joke from the API", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request exception
                MessageBox.Show($"HTTP request error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Molitio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Tempatkan logika validasi login di sini.
            // Misalnya, Anda dapat memeriksa apakah username dan password cocok dengan data yang sah.

            if (IsValidLogin(username, password))
            {
                MessageBox.Show("Login berhasil!");
                // Di sini Anda dapat membuka jendela baru atau melakukan tindakan lain sesuai kebutuhan.
            }
            else
            {
                MessageBox.Show("Login gagal. Periksa kembali username dan password.");
            }
        }

        private bool IsValidLogin(string username, string password)
        {
            // Anda dapat mengganti ini dengan logika validasi yang sesuai dengan kebutuhan Anda.
            // Contoh sederhana: username dan password harus "admin" dan "password".
            return username == "admin" && password == "password";
        }
    }
}

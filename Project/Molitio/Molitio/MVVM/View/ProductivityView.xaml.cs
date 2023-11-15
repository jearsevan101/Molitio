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

namespace Molitio.MVVM.View
{
    /// <summary>
    /// Interaction logic for ProductivityView.xaml
    /// </summary>
    public partial class ProductivityView : UserControl
    {
        private SoundController soundController;
        public ProductivityView()
        {
            InitializeComponent();
            soundController = new SoundController(mediaElement);
        }

        private void btStop_Click(object sender, RoutedEventArgs e)
        {
            soundController.Stop();
        }

        private void btFirePlace_Click(object sender, RoutedEventArgs e)
        {
            soundController.Play("https://cs1100320016d775b19.blob.core.windows.net/junpro/Fireplace.wav?sv=2023-01-03&st=2023-11-15T18%3A19%3A46Z&se=2024-11-16T18%3A19%3A00Z&sr=b&sp=r&sig=vemBgEjssTKhUcYjiwP6ut9hTpFkEMXa5euC1PTu5u0%3D");
        }

        private void btForrest_Click(object sender, RoutedEventArgs e)
        {
            soundController.Play("https://cs1100320016d775b19.blob.core.windows.net/junpro/Forrest.wav?sv=2023-01-03&st=2023-11-15T18%3A20%3A20Z&se=2024-11-16T18%3A20%3A00Z&sr=b&sp=r&sig=1JDdSuLPNn9vRGv6hFMQR%2F7k2ujiIT1klUVVfO4oVRQ%3D");
        }

        private void btOcean_Click(object sender, RoutedEventArgs e)
        {
            soundController.Play("https://cs1100320016d775b19.blob.core.windows.net/junpro/Ocean.wav?sv=2023-01-03&st=2023-11-15T17%3A49%3A11Z&se=2024-11-16T17%3A49%3A00Z&sr=b&sp=r&sig=ROeSgiH%2B%2BQ3N32bWttGAweTCFNY7xEb0XuuYq4i2yos%3D");
        }

        private void btLofi_Click(object sender, RoutedEventArgs e)
        {
            soundController.Play("https://cs1100320016d775b19.blob.core.windows.net/junpro/Lofi.wav?sv=2023-01-03&st=2023-11-15T18%3A20%3A44Z&se=2024-11-16T18%3A20%3A00Z&sr=b&sp=r&sig=%2FfEKmydPYrexASuKEj152JHYHowzstvTe10TDY3ZpVc%3D");
        }
    }
}

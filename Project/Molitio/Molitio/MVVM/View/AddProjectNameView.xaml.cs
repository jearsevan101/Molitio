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
using System.Windows.Shapes;

namespace Molitio.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddProjectNameView.xaml
    /// </summary>
    public partial class AddProjectNameView : Window
    {
        public AddProjectNameView()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

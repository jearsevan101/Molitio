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
    /// Interaction logic for IndividualTaskView.xaml
    /// </summary>
    public partial class IndividualTaskView : Window
    {
        public IndividualTaskView()
        {
            InitializeComponent();
        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void switchProject_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}

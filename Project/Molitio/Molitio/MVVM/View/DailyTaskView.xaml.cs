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
    /// Interaction logic for DailyTaskView.xaml
    /// </summary>
    public partial class DailyTaskView : Window
    {
        public DailyTaskView()
        {
            InitializeComponent();
        }

        private void btnAddDailyTask_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}

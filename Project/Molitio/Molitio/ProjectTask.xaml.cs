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

namespace Molitio
{
    /// <summary>
    /// Interaction logic for ProjectTask.xaml
    /// </summary>
    public partial class ProjectTask : Window
    {
        public ProjectTask()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSaveProject_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void switchIndividual_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btnAddToDoList_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}

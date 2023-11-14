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

namespace Molitio.View.UserControls
{
    /// <summary>
    /// Interaction logic for DailyTaskUserControl.xaml
    /// </summary>
    public partial class DailyTaskUserControl : UserControl
    {
        private ConnectionToDB connectionToDB;
        private AddDailyTask addDailyTask;
        public int Id{get; set;}
        public bool isDone { get; set; }
        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Time.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(string), typeof(DailyTaskUserControl), new PropertyMetadata(string.Empty));


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }


        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(DailyTaskUserControl), new PropertyMetadata(string.Empty));
        public DailyTaskUserControl()
        {
            InitializeComponent();
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            connectionToDB= new ConnectionToDB();
            addDailyTask = new AddDailyTask();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int taskId = Id;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this task?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // User clicked "Yes," so delete the task
                connectionToDB.DeleteDailyTask(taskId);
            }
            else
            {
                // User clicked "No" or closed the MessageBox, display a different message
                MessageBox.Show("Deletion canceled.", "Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            int taskId = Id;
            string oldTaskName = Title;
            string oldTaskTime = Time;
            bool oldIsDone = isDone;
            addDailyTask.UpdateData(taskId, oldTaskName, oldTaskTime, oldIsDone);
        }
    }
}

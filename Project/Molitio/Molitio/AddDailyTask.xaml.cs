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
    /// Interaction logic for AddDailyTask.xaml
    /// </summary>
    public partial class AddDailyTask : Window
    {
        private ConnectionToDB connectionToDB;
        private bool isNew = true;
        private int idTask;
        private bool isDone;
        public AddDailyTask()
        {
            InitializeComponent();
            connectionToDB = new ConnectionToDB();
        }
        public void UpdateData(int taskId, string oldTaskName, string oldTaskTime, bool oldIsDone)
        {
            idTask= taskId;
            isDone = oldIsDone;
            tbTaskName.Text = oldTaskName;
            TimeSpan timeSpan = TimeSpan.Parse(oldTaskTime);
            tbHour.Text = timeSpan.Hours.ToString();
            tbMinute.Text = timeSpan.Minutes.ToString();
            isNew = false;
            ShowDialog();
        }
        private void btnAddDailyTask_Click(object sender, RoutedEventArgs e)
        {
            if (isNew)
            {
                addNewData();
            }
            else
            {
                editOldData();
            }
        }
        private void editOldData()
        {
            string taskName = tbTaskName.Text;
            if (int.TryParse(tbHour.Text, out int hours) && int.TryParse(tbMinute.Text, out int minutes))
            {
                // Ensure hours and minutes are within valid ranges (0-23 for hours and 0-59 for minutes)
                if (hours >= 0 && hours <= 23 && minutes >= 0 && minutes <= 59)
                {
                    // Format the time string as "HH:mm:ss"
                    string taskTime = $"{hours:D2}:{minutes:D2}:00";
                    connectionToDB.UpdateDailyTask(idTask, taskName, taskTime, isDone);
                    isNew = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid hours or minutes. Please enter valid values.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid input for hours or minutes. Please enter numeric values.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void addNewData()
        {
            string taskName = tbTaskName.Text;
            if (int.TryParse(tbHour.Text, out int hours) && int.TryParse(tbMinute.Text, out int minutes))
            {
                // Ensure hours and minutes are within valid ranges (0-23 for hours and 0-59 for minutes)
                if (hours >= 0 && hours <= 23 && minutes >= 0 && minutes <= 59)
                {
                    // Format the time string as "HH:mm:ss"
                    string taskTime = $"{hours:D2}:{minutes:D2}:00";
                    connectionToDB.AddTask(taskName, taskTime);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid hours or minutes. Please enter valid values.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid input for hours or minutes. Please enter numeric values.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void tbTaskName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tbTaskName.Text = "";
        }

        private void tbMinute_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tbMinute.Text = "";
        }

        private void tbHour_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tbHour.Text = "";
        }
    }
}

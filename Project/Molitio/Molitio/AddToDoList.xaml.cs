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
    /// Interaction logic for AddToDoList.xaml
    /// </summary>
    public partial class AddToDoList : Window
    {
        private ConnectionToDB connectionToDB;
        private bool isNew = true;
        private int idTask;
        private bool isDone;
        public AddToDoList()
        {
            InitializeComponent();
            connectionToDB = new ConnectionToDB();
        }
        public void UpdateData(int taskId, string oldTaskName, string oldTaskDesc, DateTime oldTaskTime, bool oldIsDone)
        {
            idTask = taskId;
            isDone = oldIsDone;
            tbDescription.Text = oldTaskName;
            tbTaskName.Text = oldTaskName;
            dlTask.SelectedDate = oldTaskTime;
            isNew = false;
            ShowDialog();
        }
        private void updateOldData()
        {
            try
            {
                string taskName = tbTaskName.Text;
                string description = tbDescription.Text;

                // Check if a date is selected in the DatePicker
                if (dlTask.SelectedDate.HasValue)
                {
                    DateTime taskDate = dlTask.SelectedDate.Value;

                    // Call the AddTask method from ConnectionToDB
                    connectionToDB.UpdateToDoList(idTask, taskName, description, taskDate, isDone);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select a date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void addNewData()
        {
            try
            {
                string taskName = tbTaskName.Text;
                string description = tbDescription.Text;

                // Check if a date is selected in the DatePicker
                if (dlTask.SelectedDate.HasValue)
                {
                    DateTime taskDate = dlTask.SelectedDate.Value;

                    // Call the AddTask method from ConnectionToDB
                    connectionToDB.AddTask(taskName, description, taskDate);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select a date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnAddTask_Click(object sender, RoutedEventArgs e)
        {
            if (isNew)
            {
                addNewData();
            }
            else
            {
                updateOldData();
            }
        }
        private void tbTaskName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tbTaskName.Text = "";
        }

        private void tbDescription_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tbDescription.Text = "";
        }
    }
}

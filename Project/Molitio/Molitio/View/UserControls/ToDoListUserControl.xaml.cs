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
    /// Interaction logic for ToDoListUserControl.xaml
    /// </summary>
    public partial class ToDoListUserControl : UserControl
    {
        private ConnectionToDB connectionToDB;
        private AddToDoList addToDoList;
        public int Id{get; set;}
        public bool isDone{get; set;}
        public string TaskName
        {
            get { return (string)GetValue(TaskNameProperty); }
            set { SetValue(TaskNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskNameProperty =
            DependencyProperty.Register("TaskName", typeof(string), typeof(ToDoListUserControl), new PropertyMetadata(string.Empty));



        public string DateTask
        {
            get { return (string)GetValue(DateTaskProperty); }
            set { SetValue(DateTaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateTaskProperty =
            DependencyProperty.Register("DateTask", typeof(string), typeof(ToDoListUserControl), new PropertyMetadata(string.Empty));


        public string TaskDescription
        {
            get { return (string)GetValue(TaskDescriptionProperty); }
            set { SetValue(TaskDescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskDescription.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskDescriptionProperty =
            DependencyProperty.Register("TaskDescription", typeof(string), typeof(ToDoListUserControl), new PropertyMetadata(string.Empty));



        public ToDoListUserControl()
        {
            InitializeComponent();
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            connectionToDB = new ConnectionToDB();
            addToDoList = new AddToDoList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int taskId = Id; // retrieve the task ID associated with the button (you need to store it when populating the UI)

            // Ask the user for confirmation
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this ToDoList task?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // User clicked "Yes," so delete the ToDoList task
                connectionToDB.DeleteToDoListTask(taskId);
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
            string oldTaskName = TaskName;
            string oldTaskTime = DateTask;
            string oldDescription = TaskDescription;
            DateTime dateTime = DateTime.Parse(oldTaskTime);
            bool oldIsDone = isDone;
            addToDoList.UpdateData(taskId, oldTaskName,oldDescription, dateTime, oldIsDone);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            int taskId = Id;
            bool IsDone = true;
            MessageBoxResult result = MessageBox.Show("Are you sure you have finished this todolist?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                connectionToDB.UpdateIsDoneStatus(taskId, IsDone, true);
            }
            else
            {
                MessageBox.Show("todolist task canceled to be done.", "Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

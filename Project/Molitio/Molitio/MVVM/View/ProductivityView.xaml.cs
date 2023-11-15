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
using System.Windows.Threading;
using Npgsql;
using System.Data;
using System.Data.SqlClient;
using Molitio.View.UserControls;
using System.Data.Common;
using System.Diagnostics;

namespace Molitio.MVVM.View
{
    /// <summary>
    /// Interaction logic for ProductivityView.xaml
    /// </summary>
    public partial class ProductivityView : UserControl
    {
        private int initialTimeInSeconds = 5; // Initial time in seconds
        private int currentTimeInSeconds;
        private int pomodoroTimeInSeconds = 5;
        private int pomodoroShortBreakInSeconds = 3;
        private int pomodoroLongBreakInSeconds = 9;
        private int pomodoroBreakInSecond;
        private bool isRest;
        private bool isDefault;
        private DispatcherTimer timer;
        private NpgsqlConnection conn;
        private ConnectionToDB connectionToDB;
        //string connstring = "Host=localhost; Port=5432; Username=postgres; Password=postgresql1Evan; Database=MolitioDatabase";
        string connstring = "Host=rain.db.elephantsql.com; Port=5432; Username=vsuvdxqk; Password=N0RNrStZzPjdawPEwqNnA5NtGhhZO-Da; Database=vsuvdxqk;";

        /*string connstring = "Host=localhost; Port=5432; Username=postgres; Password=psql; Database=junpro";*/
        public ProductivityView()
        {
            InitializeComponent();
            InitializeTimer();
            connectionToDB = new ConnectionToDB();
            connectionToDB.DataUpdated += ConnectionToDB_DataUpdated;
            PopulateDailyTasksFromDatabase();
            PopulateToDoListFromDatabase();
        }

        private void ConnectionToDB_DataUpdated(object? sender, EventArgs e)
        {
            PopulateDailyTasksFromDatabase();
            PopulateToDoListFromDatabase();
        }
        private void PopulateDailyTasksFromDatabase()
        {
            try
            {
                List<DailyTask> tasks = connectionToDB.GetDailyTasks();
                int row = 0;
                foreach (var task in tasks)
                {
                    // Update UI elements (e.g., labels) with task.Time and task.Title
                    // ...
                    if (task.isDone == false)
                    {
                        if (gridDailyTasks.Children[row] is DailyTaskUserControl dailyTaskUserControl)
                        {
                            dailyTaskUserControl.Time = task.Time;
                            dailyTaskUserControl.Title = task.Title;
                            dailyTaskUserControl.Id = task.Id;
                            dailyTaskUserControl.Visibility = Visibility.Visible;
                        }
                        row++;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, show error message)
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void PopulateToDoListFromDatabase()
        {
            try
            {
                List<ToDoList> toDos = connectionToDB.GetToDoList();
                int row = 0;
                foreach (var todo in toDos)
                {
                    // Update UI elements (e.g., labels) with task.Time and task.Title
                    // ...
                    if (todo.isDone == false)
                    {
                        if (gridToDoList.Children[row] is ToDoListUserControl toDoListUserControl)
                        {
                            toDoListUserControl.TaskName = todo.Title;
                            toDoListUserControl.DateTask = todo.Time;
                            toDoListUserControl.Id = todo.Id;
                            toDoListUserControl.TaskDescription = todo.Description;
                            toDoListUserControl.Visibility = Visibility.Visible;
                        }
                        row++;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log, show error message)
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void pomodoroBreak()
        {
            currentTimeInSeconds = pomodoroBreakInSecond;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            //TimerTitle.Content = "Rest well...";
            UpdateTimerDisplay();
            isRest = false;
            timer.Start();
            isDefault = false;
        }
        private void pomodoroDefault()
        {
            currentTimeInSeconds = pomodoroTimeInSeconds;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            //TimerTitle.Content = "Pomodoro starting...";
            UpdateTimerDisplay();
            isRest = true;
            timer.Start();
            isDefault = true;
        }

        private void timerDone(string text, bool type)
        {
            timer.Stop();
            Timerbg.Fill = new SolidColorBrush(Colors.LightPink);
            MessageBoxResult result = MessageBox.Show(text, "Timer Alert", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Timerbg.Fill = new SolidColorBrush(Colors.LightBlue);
                if (type == true)
                {
                    pomodoroBreak();
                }
                else if (type == false)
                {
                    pomodoroDefault();
                    timer.Start();
                }
            }
            else
            {
                Timerbg.Fill = new SolidColorBrush(Colors.LightBlue);
                //TimerTitle.Content = "Pomodoro Timer";
            }
        }
        private void InitializeTimer()
        {
            currentTimeInSeconds = initialTimeInSeconds;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            UpdateTimerDisplay();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (currentTimeInSeconds > 0)
            {
                currentTimeInSeconds--;
                UpdateTimerDisplay();
            }
            else
            {
                if (isDefault == true)
                {
                    timerDone("Start resting?", true);
                }
                else if (isDefault == false)
                {
                    timerDone("Restart pomodoro?", false);
                }
            }
        }
        private void UpdateTimerDisplay()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTimeInSeconds);
            TimerLabel.Content = timeSpan.ToString(@"hh\:mm\:ss");
        }

        private void OnButtonStart(object sender, RoutedEventArgs e)
        {
            if (!timer.IsEnabled)
            {
                pomodoroDefault();
            }
        }

        private void OnButtonStop(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
            }
        }

        private void ShortBreak_Checked(object sender, RoutedEventArgs e)
        {
            pomodoroBreakInSecond = pomodoroShortBreakInSeconds;
        }

        private void LongBreak_Checked(object sender, RoutedEventArgs e)
        {
            pomodoroBreakInSecond = pomodoroLongBreakInSeconds;
        }

        private void btnAddToDoList_Click(object sender, RoutedEventArgs e)
        {
            AddToDoList addToDoList = new AddToDoList();
            addToDoList.ShowDialog();
        }

        private void btnAddNote_Click(object sender, RoutedEventArgs e)
        {
            try
            {   
                using (conn = new NpgsqlConnection(connstring))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("select * from notes_insert(:_noteTitle,:_noteDesc,:_noteDate::date)", conn))
                    {
                        cmd.Parameters.AddWithValue("_noteTitle", tbNoteTitle.Text);
                        cmd.Parameters.AddWithValue("_noteDesc", tbNoteDesc.Text);
                        cmd.Parameters.AddWithValue("_noteDate", SqlDbType.Date);
                        cmd.Parameters["_noteDate"].Value = DateTime.Now.Date;

                        if ((int)cmd.ExecuteScalar() == 1)
                        {
                            MessageBox.Show("Data Has Been Successfully Inputed", "Well Done!", MessageBoxButton.OK, MessageBoxImage.Information);
                            conn.Close();
                            tbNoteTitle.Text = tbNoteDesc.Text = null;
                        }
                    }   
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Insert FAIL", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddDailyTask_Click(object sender, RoutedEventArgs e)
        {
            AddDailyTask addDailyTask= new AddDailyTask();
            addDailyTask.ShowDialog();
        }
    }
}

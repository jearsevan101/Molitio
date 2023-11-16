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
    /// 

    
    public partial class ProductivityView : UserControl
    {
        private SoundController soundController;
        private int initialTimeInSeconds = 1500; // Initial time in seconds
        private int currentTimeInSeconds;
        private int pomodoroTimeInSeconds = 1500;
        private int pomodoroShortBreakInSeconds = 300;
        private int pomodoroLongBreakInSeconds = 900;
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
            soundController = new SoundController(mediaElement);
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
                for (int i = row; i < tasks.Count; i++)
                {
                    if (gridDailyTasks.Children[i] is DailyTaskUserControl dailyTaskUserControl)
                    {
                        dailyTaskUserControl.Visibility = Visibility.Hidden;
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
                for (int i = row; i < toDos.Count; i++)
                {
                    if (gridToDoList.Children[i] is ToDoListUserControl toDoListUserControl)
                    {
                        toDoListUserControl.Visibility = Visibility.Hidden;
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

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            PopulateDailyTasksFromDatabase();
            PopulateToDoListFromDatabase();
        }

        private void tbTitleClicked(object sender, MouseButtonEventArgs e)
        {
            tbNoteTitle.Text = "";
        }

        private void tbBodyClicked(object sender, MouseButtonEventArgs e)
        {
            tbNoteDesc.Text = "";
        }
    }
}

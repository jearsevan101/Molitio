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
using static System.Net.Mime.MediaTypeNames;

namespace Molitio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void pomodoroBreak()
        {
            currentTimeInSeconds = pomodoroBreakInSecond;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            TimerTitle.Content = "Rest well...";
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
            TimerTitle.Content = "Pomodoro starting...";
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
                TimerTitle.Content = "Pomodoro Timer";
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
                if (isDefault==true)
                {
                    timerDone("Start resting?", true);
                }
                else if (isDefault==false)
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


        private void TimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void RadioButton1_Checked(object sender, RoutedEventArgs e)
        {
            pomodoroBreakInSecond = pomodoroShortBreakInSeconds;
        }

        private void RadioButton2_Checked(object sender, RoutedEventArgs e)
        {
            pomodoroBreakInSecond = pomodoroLongBreakInSeconds;
        }
    }
}

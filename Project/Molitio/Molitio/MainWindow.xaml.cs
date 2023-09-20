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

namespace Molitio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int initialTimeInSeconds = 60; // Initial time in seconds
        private int currentTimeInSeconds;
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
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
                timer.Stop();
                Timerbg.Fill = new SolidColorBrush(Colors.LightPink);
                MessageBox.Show("Time's up!");
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
                timer.Start();
            }
        }

        private void OnButtonStop(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
            }
        }

        private void SetTimeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!timer.IsEnabled)
            {
                if (int.TryParse(TimeTextBox.Text, out int newTime))
                {
                    initialTimeInSeconds = newTime;
                    currentTimeInSeconds = initialTimeInSeconds;
                    UpdateTimerDisplay();
                }
                else
                {
                    MessageBox.Show("Invalid input. Please enter a valid integer.");
                }
            }
            else
            {
                MessageBox.Show("Timer is running. Stop it before setting a new time.");
            }
        }
    }
}

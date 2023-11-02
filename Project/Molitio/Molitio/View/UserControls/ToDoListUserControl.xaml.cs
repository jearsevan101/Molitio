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
        }
    }
}

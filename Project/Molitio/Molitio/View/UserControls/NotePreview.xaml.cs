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
    /// Interaction logic for NotePreview.xaml
    /// </summary>
    public partial class NotePreview : UserControl
    {
        public static readonly DependencyProperty NoteTitleProperty =
            DependencyProperty.Register("NoteTitle", typeof(string), typeof(NotePreview));

        public static readonly DependencyProperty NoteDescProperty =
            DependencyProperty.Register("NoteDate", typeof(string), typeof(NotePreview));

        public string NoteTitle
        {
            get { return (string)GetValue(NoteTitleProperty); }
            set { SetValue(NoteTitleProperty, value); }
        }

        public string NoteDesc
        {
            get { return (string)GetValue(NoteDescProperty); }
            set { SetValue(NoteDescProperty, value); }
        }

        public NotePreview()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}

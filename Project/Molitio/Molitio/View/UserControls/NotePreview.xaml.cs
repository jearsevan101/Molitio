using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private ConnectionToDB connectionToDB;
        private AddNote addNote;
        public int NotesID { get; set; }
        public static readonly DependencyProperty NoteTitleProperty =
            DependencyProperty.Register("NoteTitle", typeof(string), typeof(NotePreview));

        public static readonly DependencyProperty NoteDescProperty =
            DependencyProperty.Register("NoteDesc", typeof(string), typeof(NotePreview));

        public static readonly DependencyProperty NoteDateProperty =
            DependencyProperty.Register("NoteDate", typeof(string), typeof(NotePreview));
        public string NoteDate
        {
            get { return (string)GetValue(NoteDateProperty); }
            set { SetValue(NoteDateProperty, value); }
        }
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
            connectionToDB = new ConnectionToDB();
            DataContext = this;
            addNote = new AddNote();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int Id = NotesID;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this notes?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // User clicked "Yes," so delete the ToDoList task
                connectionToDB.DeleteNote(Id);
            }
            else
            {
                // User clicked "No" or closed the MessageBox, display a different message
                MessageBox.Show("Deletion canceled.", "Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            int notesID = NotesID;
            string oldNotesTitle = NoteTitle;
            string oldNotesDesc = NoteDesc;
            addNote.UpdateData(notesID, oldNotesTitle, oldNotesDesc);
        }
    }
}

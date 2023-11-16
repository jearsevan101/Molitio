using Molitio.View.UserControls;
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
using Molitio.View.UserControls;


namespace Molitio.MVVM.View
{
    /// <summary>
    /// Interaction logic for NotebookView.xaml
    /// </summary>
    public partial class NotebookView : UserControl
    {
        private ConnectionToDB connectionToDB;
        public NotebookView()
        {
            InitializeComponent();

            connectionToDB = new ConnectionToDB();

            updateData();
        }
        private void updateData()
        {
            // Initialize your collection with data from the database
            List<NoteItem> NoteItems = connectionToDB.GetNoteItemsFromDatabase();

            // Set the DataContext to the collection
            DataContext = this;

            int notesInRow = 0;

            // Create and display NotePreview instances
            foreach (var noteItem in NoteItems)
            {
                var notePreview = new NotePreview
                {
                    NoteTitle = noteItem.NoteTitle,
                    NotesID = noteItem.NotesID,
                    NoteDesc = noteItem.NoteDesc // Format as needed
                };

                YourWrapPanel.Children.Add(notePreview);

                // Increment notesInRow
                notesInRow++;

                // Check if we've reached three notes in a row
                if (notesInRow == 3)
                {
                    // Add a line break by adding an empty separator
                    YourWrapPanel.Children.Add(new Separator());

                    // Reset the notesInRow counter
                    notesInRow = 0;
                }
            }
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

            // Clear existing UI elements
            YourWrapPanel.Children.Clear();

            updateData();
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

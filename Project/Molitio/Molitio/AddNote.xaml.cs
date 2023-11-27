using Molitio.View.UserControls;
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
using System.Windows.Shapes;

namespace Molitio
{
    /// <summary>
    /// Interaction logic for AddNote.xaml
    /// </summary>
    public partial class AddNote : Window
    {
        private int notesID;
        private ConnectionToDB connectionToDB;
        public AddNote()
        {
            InitializeComponent();
            connectionToDB = new ConnectionToDB();
        }
        public void UpdateData(int notesId, string oldTitleName, string oldNoteDescription)
        {
            notesID = notesId;
            tbTitleName.Text = oldTitleName;
            tbDescription.Text = oldNoteDescription;
            ShowDialog();
        }
        private void btnEditNotes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string noteTitle = tbTitleName.Text;
                string description = tbDescription.Text;

                // Call the AddTask method from ConnectionToDB
                connectionToDB.UpdateNote(notesID, noteTitle, description);
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tbTitleName_TextChanged(object sender, MouseButtonEventArgs e)
        {
            tbTitleName.Text = "";
        }

        private void tbDescription_TextChanged(object sender, MouseButtonEventArgs e)
        {
            tbDescription.Text = "";
        }
    }
}

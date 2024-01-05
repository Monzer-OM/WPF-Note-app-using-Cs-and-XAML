using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace FirstAppWPF
{
    public partial class NotesWindow : Window
    {
        public NotesWindow()
        {
            InitializeComponent();
        }

        public void DisplayNotes(string filePath)
        {
            if (File.Exists(filePath))
            {
                List<string> notes = new List<string>(File.ReadAllLines(filePath));
                notesListBox.ItemsSource = notes;
            }
            else
            {
                MessageBox.Show("No notes found!");
            }
        }

        private void BackToMainWindowButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)this.Owner; // Get the owner (MainWindow)
            this.Close(); // Close the NotesWindow
            mainWindow.Show(); // Show the MainWindow
        }
    }
}

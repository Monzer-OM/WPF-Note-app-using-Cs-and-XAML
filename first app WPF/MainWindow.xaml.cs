using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace FirstAppWPF
{
    public partial class MainWindow : Window
    {
        private const string NotesFilePath = "notes.txt";

        public MainWindow()
        {
            InitializeComponent();
            // Hook up the PreviewKeyDown event handler for MainWindow
            this.PreviewKeyDown += MainWindow_PreviewKeyDown;
        }

        private void SaveNote()
        {
            string note = $"{DateTime.Now} - {noteTextBox.Text}"; // Include date and time with the note
            if (!string.IsNullOrWhiteSpace(noteTextBox.Text))
            {
                File.AppendAllText(NotesFilePath, note + Environment.NewLine);
                noteTextBox.Clear();
                MessageBox.Show("Note saved successfully!");
            }
            else
            {
                MessageBox.Show("Please write something to save.....");
            }
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Check if F1 key is pressed
            if (e.Key == Key.F1)
            {
                ShowSavedNotes();
                e.Handled = true; // Mark the event as handled to prevent further processing
            }

            // save
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S) 
            {
                SaveNote();
                e.Handled = true;
            }

            // clear
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.D)
            {
                noteTextBox.Clear();
                e.Handled = true;
            }
        }

        private void ShowSavedNotes()
        {
            if (File.Exists(NotesFilePath))
            {
                NotesWindow notesWindow = new NotesWindow();
                notesWindow.Owner = this; // Set MainWindow as the owner
                notesWindow.DisplayNotes(NotesFilePath);
                notesWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("No notes found!");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveNote(); // Call SaveNote method when Save Button is clicked
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            noteTextBox.Clear();
        }

        private void ShowNotesButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSavedNotes();
        }
    }
}

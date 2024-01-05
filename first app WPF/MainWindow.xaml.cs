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
            this.PreviewKeyDown += MainWindow_PreviewKeyDown;
        }

        private void SaveNote()
        {
            string note = noteTextBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(note))
            {
                string formattedNote = $"{DateTime.Now.ToString("MM/dd/yyyy HH:mm")} - {note}"; // Include date and time with the note

                File.AppendAllText(NotesFilePath, formattedNote + Environment.NewLine);
                noteTextBox.Clear();
                MessageBox.Show("Note saved successfully!!!!!!");
            }
            else
            {
                MessageBox.Show("Please write something to save.");
            }
        }

        private void ShowSaved()
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

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                SaveNote();
                e.Handled = true;
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.D)
            {
                noteTextBox.Clear();
            }
            else if (e.Key == Key.F1)
            {
                ShowSaved();
                e.Handled = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveNote();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            noteTextBox.Clear();
        }

        private void ShowNotesButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSaved();
        }
    }
}

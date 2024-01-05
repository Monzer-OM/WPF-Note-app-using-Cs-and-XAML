using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System;
using System.Windows.Controls;




namespace FirstAppWPF
{
    public partial class NotesWindow : Window
    {
        public NotesWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += savedWindow_previewKeyDown;
            notesListBox.MouseDoubleClick += NotesListBox_MouseDoubleClick;

        }

        private void savedWindow_previewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.N)
            {
                this.Close();
                e.Handled = true; // Mark the event as handled to prevent further processinggg..
            }

        }



        public void DisplayNotes(string filePath)
        {
            if (File.Exists(filePath))
            {
                List<string> notes = new List<string>();
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string note = line.Trim();
                    if (note.Length > 50)
                    {
                        //note = note.Substring(0, 30); // Take only the first 50 characters
                        note = $"{note.Substring(0, 50)}{"........"}";

                    }
                    notes.Add(note);
                }

                notesListBox.ItemsSource = notes;
            }
            else
            {
                MessageBox.Show("No notes found!");
            }
        }



        private void WriteNewNoteButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the NotesWindow
            Console.WriteLine("Hello World!");
        }

        private void NotesListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox != null && listBox.SelectedItem != null)
            {
                string selectedNote = listBox.SelectedItem.ToString();

                // Implement code to open the selected note here (e.g., open in a new window or display in a text box)
                MessageBox.Show(selectedNote);
            }
        }
    }
}

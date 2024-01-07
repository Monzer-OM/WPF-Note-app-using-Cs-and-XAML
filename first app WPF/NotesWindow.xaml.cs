using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FirstAppWPF
{
    public partial class NotesWindow : Window
    {
        public NotesWindow()
        {
            InitializeComponent();
            notesListBox.MouseDoubleClick += NotesListBox_MouseDoubleClick;
            this.PreviewKeyDown += notePage_PreviewKeyDown;
        }

        private void notePage_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Ctrl + N
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.N)
            {
                this.Close();
            }
        }
        private void WriteNewNoteButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the NotesWindow
        }

        private List<string> fullNotes; // Declare a list to store full notes

        public void DisplayNotes(string filePath)
        {
            if (File.Exists(filePath))
            {
                List<string> truncatedNotes = new List<string>(); // Store truncated notes for display
                fullNotes = new List<string>(); // Initialize the list for full notes

                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string note = line.Trim();
                    string truncatedNote;
                    if (note.Length > 50)
                    {
                        truncatedNote = $"{note.Substring(0, 50)}{"......"}"; // Take only the first 50 characters for display
                    }
                    else
                    {
                        truncatedNote = note; // Keep the original note if it's 50 characters or less
                    }
                    truncatedNotes.Add(truncatedNote); // Add truncated note to display list
                    fullNotes.Add(note); // Add full note to separate list
                }

                notesListBox.ItemsSource = truncatedNotes;
            }
            else
            {
                MessageBox.Show("No notes found!");
            }
        }

        private void NotesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox != null && listBox.SelectedItem != null)
            {
                int selectedIndex = listBox.SelectedIndex;
                if (selectedIndex >= 0 && selectedIndex < fullNotes.Count)
                {
                    string fullNote = fullNotes[selectedIndex];
                    MessageBox.Show($"Full Note: {fullNote}");
                }
            }
        }

        private void notesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

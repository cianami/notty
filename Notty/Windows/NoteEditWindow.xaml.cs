using Notty.Data;
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
using System.Windows.Shapes;

namespace Notty.Windows
{
    /// <summary>
    /// Логика взаимодействия для NoteEditWindow.xaml
    /// </summary>
    public partial class NoteEditWindow : Window
    {
        public DialogAction dialogAction { get; private set; }
        public Note Note { get; private set; }
        public NoteEditWindow():this(new Note("",""))
        {
            DeleteButton.IsEnabled = false;
        }

        public NoteEditWindow(Note note)
        {
            InitializeComponent();
            Note = note;
            TitleTextBox.Text= Note.Title;
            ContentTextBox.Text= Note.Content;
        }

        private void SaveNote(object sender, RoutedEventArgs e)
        {
            Note.Title=TitleTextBox.Text;
            Note.Content = ContentTextBox.Text;
            dialogAction = DialogAction.Save;
            DialogResult = true;
        }

        private void DeleteNote(object sender, RoutedEventArgs e)
        {
            dialogAction = DialogAction.Delete;
            DialogResult = true;
        }
        public enum DialogAction { Save,Delete }
    }
}

using Notty.Data;
using Notty.Windows;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Notty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Cards.ItemsSource = NoteDB.GetNoteList();
        }

        private void AddNote(object sender, RoutedEventArgs e)
        {
            NoteEditWindow noteEdit = new NoteEditWindow();
            noteEdit.Show();
        }

        private void SelectedCard(object sender, RoutedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            Note selectedNote = (Note)listBox.SelectedItem;
            NoteEditWindow noteEdit = new NoteEditWindow(selectedNote);
            noteEdit.ShowDialog();
        }
    }
}

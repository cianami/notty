using Notty.Data;
using Notty.Windows;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using System.Windows.Resources;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Notty
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NoteDB dataBase;
        public MainWindow()
        {
            InitializeComponent();
            dataBase = new NoteDB("NoteDB.sqlite");
            Cards.ItemsSource = dataBase.GetAll();
        }

        private void AddNote(object sender, RoutedEventArgs e)
        {
            NoteEditWindow noteEdit = new NoteEditWindow();
            noteEdit.ShowDialog();
            if (noteEdit.DialogResult == true && noteEdit.dialogAction == NoteEditWindow.DialogAction.Save)
            {
                dataBase.Add(noteEdit.Note);
                Cards.ItemsSource = dataBase.GetAll();
            }
        }
        private void SelectedCard(object sender, RoutedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            if (listBox.SelectedItem == null) return;
            Note selectedNote = (Note)listBox.SelectedItem;
            listBox.SelectedItem = null;
            NoteEditWindow noteEdit = new NoteEditWindow(selectedNote);
            noteEdit.ShowDialog();
            if (noteEdit.DialogResult == true && noteEdit.dialogAction == NoteEditWindow.DialogAction.Save)
            {
                dataBase.Update(noteEdit.Note);
                Cards.ItemsSource = dataBase.GetAll();
            }
            else if (noteEdit.DialogResult == true && noteEdit.dialogAction == NoteEditWindow.DialogAction.Delete)
            {
                dataBase.Delete(noteEdit.Note);
                Cards.ItemsSource = dataBase.GetAll();
            }
        }
    }
}

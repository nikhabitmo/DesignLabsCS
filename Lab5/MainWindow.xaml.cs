using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Lab3;
using Lab3.DataStorage;
using Lab3.Task;

namespace Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataStorage _dataStorage = new DataStorage();
        private List<TaskItem> _taskItems;
        
        
        public MainWindow()
        {
            InitializeComponent();
            _taskItems = _dataStorage.LoadFromSQLite();
            Console.WriteLine(_taskItems);
            taskListView.ItemsSource = _taskItems; // Basicly loading from sqlite
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            AddTaskDialog addTaskDialog = new AddTaskDialog();
            if (addTaskDialog.ShowDialog() == true)
            {
                _taskItems.Add(addTaskDialog.Task);
            }
        }
    }

}
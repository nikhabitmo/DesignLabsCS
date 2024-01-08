using Lab3.Task;
using System;
using System.Windows;

namespace Lab5
{
    public partial class AddTaskDialog : Window
    {
        public TaskItem Task { get; private set; }

        public AddTaskDialog()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Task = new TaskItem
            {
                Title = titleTextBox.Text,
                Description = descriptionTextBox.Text,
                Deadline = deadlineDatePicker.SelectedDate ?? DateTime.Now,
            };

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
using System;
using System.Windows;

namespace demo
{
    public partial class AddTaskDialog : Window
    {
        private DatabaseHelper dbHelper;
        private string username;
        public string TaskTitle { get; private set; }
        public string TaskDescription { get; private set; }
        public DateTime? ReminderDate { get; private set; }

        public AddTaskDialog(string user)
        {
            InitializeComponent();
            username = user;
            dbHelper = new DatabaseHelper();
        }

        private void ReminderChecked(object sender, RoutedEventArgs e)
        {
            reminderDate.Visibility = Visibility.Visible;
        }

        private void ReminderUnchecked(object sender, RoutedEventArgs e)
        {
            reminderDate.Visibility = Visibility.Collapsed;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleBox.Text))
            {
                MessageBox.Show("Please enter a task title.", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TaskTitle = titleBox.Text.Trim();
            TaskDescription = descBox.Text.Trim();
            ReminderDate = reminderCheck.IsChecked == true ? reminderDate.SelectedDate : null;

            if (dbHelper.AddTask(TaskTitle, TaskDescription, ReminderDate, username))
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Failed to add task. Please check your database connection.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
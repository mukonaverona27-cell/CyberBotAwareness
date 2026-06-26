using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace demo
{
    public partial class TaskManagerWindow : Window
    {
        private DatabaseHelper dbHelper;
        private string username;

        public TaskManagerWindow(string user)
        {
            InitializeComponent();
            username = user;
            dbHelper = new DatabaseHelper();
            LoadTasks();
            ActivityLogger.Log("Opened Task Manager", username);
        }

        private void LoadTasks()
        {
            try
            {
                DataTable dt = dbHelper.GetTasks(username);
                List<TaskModel> tasks = new List<TaskModel>();

                foreach (DataRow row in dt.Rows)
                {
                    tasks.Add(new TaskModel
                    {
                        TaskID = Convert.ToInt32(row["TaskID"]),
                        Title = row["Title"].ToString(),
                        Description = row["Description"].ToString(),
                        ReminderDate = row["ReminderDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["ReminderDate"]),
                        IsCompleted = Convert.ToBoolean(row["IsCompleted"])
                    });
                }

                taskListView.ItemsSource = tasks;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddTask(object sender, RoutedEventArgs e)
        {
            var dialog = new AddTaskDialog(username);
            if (dialog.ShowDialog() == true)
            {
                LoadTasks();
                ActivityLogger.Log($"Added task: {dialog.TaskTitle}", username);
            }
        }

        private void CompleteTask(object sender, RoutedEventArgs e)
        {
            if (taskListView.SelectedItem is TaskModel task)
            {
                if (dbHelper.UpdateTaskStatus(task.TaskID, true))
                {
                    LoadTasks();
                    ActivityLogger.Log($"Completed task: {task.Title}", username);
                }
                else
                {
                    MessageBox.Show("Failed to update task status.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a task to complete.", "No Selection",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteTask(object sender, RoutedEventArgs e)
        {
            if (taskListView.SelectedItem is TaskModel task)
            {
                var result = MessageBox.Show($"Delete task '{task.Title}'?", "Confirm Delete",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (dbHelper.DeleteTask(task.TaskID))
                    {
                        LoadTasks();
                        ActivityLogger.Log($"Deleted task: {task.Title}", username);
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete task.", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a task to delete.", "No Selection",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RefreshTasks(object sender, RoutedEventArgs e)
        {
            LoadTasks();
            ActivityLogger.Log("Refreshed task list", username);
        }

        private void TaskDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (taskListView.SelectedItem is TaskModel task)
            {
                MessageBox.Show(
                    $"Title: {task.Title}\n" +
                    $"Description: {task.Description}\n" +
                    $"Status: {(task.IsCompleted ? "Completed" : "Pending")}\n" +
                    $"Reminder: {(task.ReminderDate.HasValue ? task.ReminderDate.Value.ToShortDateString() : "None")}",
                    "Task Details",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }
}

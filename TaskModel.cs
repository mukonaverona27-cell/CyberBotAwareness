using System;

namespace demo
{
    public class TaskModel
    {
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }

        public string DisplayText
        {
            get
            {
                string status = IsCompleted ? "✓ Completed" : "○ Pending";
                string reminder = ReminderDate.HasValue ? $" (Reminder: {ReminderDate.Value.ToShortDateString()})" : "";
                return $"{Title} - {status}{reminder}";
            }
        }
    }
}
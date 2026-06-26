using System;
using System.Collections.Generic;
using System.Linq;

namespace demo
{
    public static class ActivityLogger
    {
        private static List<string> activities = new List<string>();
        private static int maxDisplay = 10;

        public static void Log(string action, string username = "")
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string userInfo = string.IsNullOrEmpty(username) ? "" : $" [{username}]";
            activities.Insert(0, $"[{timestamp}]{userInfo} {action}");
        }

        public static List<string> GetRecentActivities(int count = 10)
        {
            return activities.Take(count).ToList();
        }

        public static List<string> GetAllActivities()
        {
            return activities;
        }

        public static void Clear()
        {
            activities.Clear();
        }

        public static int Count => activities.Count;
    }
}
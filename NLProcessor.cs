using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace demo
{
    public class NLProcessor
    {
        private static Dictionary<string, string> intentKeywords = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Task intents
            { "add task", "add_task" },
            { "create task", "add_task" },
            { "new task", "add_task" },
            { "remind me", "add_reminder" },
            { "set reminder", "add_reminder" },
            { "remind", "add_reminder" },
            { "task to", "add_task" },
            
            // Quiz intents
            { "quiz", "start_quiz" },
            { "play quiz", "start_quiz" },
            { "test me", "start_quiz" },
            { "knowledge check", "start_quiz" },
            { "take quiz", "start_quiz" },
            
            // Log intents
            { "activity log", "show_log" },
            { "what have you done", "show_log" },
            { "show log", "show_log" },
            { "history", "show_log" },
            { "show activity", "show_log" },
            
            // Help
            { "help", "show_help" },
            { "what can you do", "show_help" },
            { "commands", "show_help" }
        };

        public static string DetectIntent(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return "unknown";

            string input = userInput.ToLower().Trim();

            foreach (var keyword in intentKeywords)
            {
                if (input.Contains(keyword.Key))
                {
                    return keyword.Value;
                }
            }

            // Check for cybersecurity topics
            if (input.Contains("password") || input.Contains("passphrase"))
                return "password_tip";
            if (input.Contains("phish") || input.Contains("scam") || input.Contains("fraud"))
                return "phishing_tip";
            if (input.Contains("privacy") || input.Contains("data") || input.Contains("personal"))
                return "privacy_tip";
            if (input.Contains("thank") || input.Contains("thanks"))
                return "thanks";

            return "unknown";
        }

        public static string ExtractTaskTitle(string userInput)
        {
            string[] phrases = { "remind me to", "add task", "create task", "task to", "add a task" };
            foreach (var phrase in phrases)
            {
                int index = userInput.ToLower().IndexOf(phrase);
                if (index >= 0)
                {
                    string task = userInput.Substring(index + phrase.Length).Trim();
                    if (!string.IsNullOrWhiteSpace(task))
                        return task;
                }
            }
            return userInput;
        }

        public static DateTime? ExtractReminderDate(string userInput)
        {
            userInput = userInput.ToLower();

            if (userInput.Contains("tomorrow"))
                return DateTime.Now.AddDays(1);

            Regex regex = new Regex(@"in (\d+) days?");
            Match match = regex.Match(userInput);
            if (match.Success)
            {
                int days = int.Parse(match.Groups[1].Value);
                return DateTime.Now.AddDays(days);
            }

            Regex dateRegex = new Regex(@"on (\d{1,2}[/-]\d{1,2}[/-]\d{2,4})");
            match = dateRegex.Match(userInput);
            if (match.Success)
            {
                if (DateTime.TryParse(match.Groups[1].Value, out DateTime date))
                    return date;
            }

            return null;
        }
    }
}
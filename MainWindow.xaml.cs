using demo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace demo
{//start of namespace

    public partial class MainWindow : Window
    {//start of class

        //creating an instance for the class Array
        ArrayList reply = new ArrayList();
        ArrayList ignore = new ArrayList();
        user_name check_name = new user_name();

        // variables
        string username = string.Empty;
        string pre_question = string.Empty;
        int counting = 0;

        public MainWindow()
        {
            InitializeComponent();

            new respond(reply, ignore) { };

            //creating an instance for the class voice_greeting 
            //with an object name greet
            voice_greeting greet = new voice_greeting();

            //call the voice method
            greet.greet();

            // Log application start
            ActivityLogger.Log("Application started");
        }

        //proceed event handler
        private void proceed(object sender, RoutedEventArgs e)
        {
            //Hide home page grid and set Username grid visible
            home_grid.Visibility = Visibility.Hidden;
            username_grid.Visibility = Visibility.Visible;
        }

        //submit name event handler
        private void submit_name(object sender, RoutedEventArgs e)
        {
            //check the user name from memory recall
            username = check_name.submit_name(usernames_input, chats);

            //Hide username page grid and set chats grid visible
            username_grid.Visibility = Visibility.Hidden;
            chat_grid.Visibility = Visibility.Visible;

            // Log user login
            ActivityLogger.Log($"User logged in: {username}", username);
        }

        //send event handler
        private void send(object sender, RoutedEventArgs e)
        {
            // Get the question from the design and sanitize it
            string rawQuestion = question.Text.ToString().Trim();

            if (string.IsNullOrWhiteSpace(rawQuestion))
            {
                error_method("ChatBot", "Please enter a question or command.");
                return;
            }

            // Log user message
            ActivityLogger.Log($"User asked: {rawQuestion}", username);

            // Remove special characters and clean the question
            string questions = RemoveSpecialCharacters(rawQuestion);

            // Show what the user typed 
            error_method(username, rawQuestion);

            // AI chat and auto_show_interest
            auto_show_interest();
            ai_check(questions);
        }

        //start of ai_chat method
        private void ai_check(string questions)
        {
            // Check if user entered anything meaningful
            if (string.IsNullOrWhiteSpace(questions))
            {
                error_method("ChatBot", "Please enter a valid question.");
                question.Clear();
                return;
            }

            // Check for NLP intents first
            string intent = NLProcessor.DetectIntent(questions);

            switch (intent)
            {
                case "add_task":
                case "add_reminder":
                    HandleAddTask(questions);
                    return;

                case "start_quiz":
                    HandleStartQuiz();
                    return;

                case "show_log":
                    HandleShowLog();
                    return;

                case "show_help":
                    HandleShowHelp();
                    return;

                case "password_tip":
                    error_method("ChatBot", "🔐 **Password Safety Tip:**\n• Use strong passwords with at least 12 characters\n• Include numbers, symbols, and uppercase letters\n• Don't reuse passwords across accounts\n• Consider using a password manager");
                    ActivityLogger.Log("Gave password safety tip", username);
                    question.Clear();
                    return;

                case "phishing_tip":
                    error_method("ChatBot", "🎣 **Phishing Prevention Tip:**\n• Never click suspicious links in emails\n• Check sender email addresses carefully\n• Don't share personal info via email\n• Report phishing attempts to your IT department");
                    ActivityLogger.Log("Gave phishing prevention tip", username);
                    question.Clear();
                    return;

                case "privacy_tip":
                    error_method("ChatBot", "🛡️ **Privacy Protection Tip:**\n• Review your privacy settings regularly\n• Be careful what you share on social media\n• Use two-factor authentication when available\n• Update your passwords regularly");
                    ActivityLogger.Log("Gave privacy protection tip", username);
                    question.Clear();
                    return;

                case "thanks":
                    error_method("ChatBot", "You're welcome! 😊 I'm here to help you stay safe online. Feel free to ask me anything about cybersecurity!");
                    question.Clear();
                    return;
            }

            // If no NLP intent matched, continue with existing keyword matching
            // Variables for processing
            string[] words = questions.ToLower().Split(new char[] { ' ', ',', '.', '?', '!', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
            bool found = false;
            string message = string.Empty;
            Random indexer = new Random();
            List<string> per_word = new List<string>();
            List<string> answers_found = new List<string>();

            // Process each word
            foreach (string word in words)
            {
                // Skip very short words or ignored words
                if (word.Length < 3 || ignore.Contains(word.ToLower()))
                    continue;

                per_word.Clear();

                //start of interests
                if (word.Contains("interested"))
                {
                    string store_interests = string.Empty;
                    bool found_interest = false;

                    HashSet<string> currentInterests = new HashSet<string>();

                    foreach (string interest in words)
                    {
                        // CLEAN INPUT
                        string clean = interest.ToLower().Trim();
                        clean = Regex.Replace(clean, @"[^a-zA-Z0-9\s]", "");

                        // FILTER NOISE WORDS
                        if (!ignore.Contains(clean) && clean != "interested" && clean != "and" && clean != "in" && clean.Length >= 3)
                        {
                            found_interest = true;
                            currentInterests.Add(clean);
                        }
                    }

                    // prepare interests
                    store_interests = string.Join(", ", currentInterests);

                    if (found_interest && !string.IsNullOrWhiteSpace(store_interests))
                    {
                        string filename = "interested_topic.txt";
                        bool userFound = false;

                        if (File.Exists(filename))
                        {
                            string[] lines = File.ReadAllLines(filename);

                            for (int i = 0; i < lines.Length; i++)
                            {
                                if (lines[i].StartsWith(username))
                                {
                                    userFound = true;

                                    //get all the interests
                                    string existing = lines[i].Replace(username + " interested in:", "").ToLower();

                                    HashSet<string> existingSet = new HashSet<string>(existing.Split(',').Select(x => x.Trim()).Where(x => x != ""));

                                    // remove duplicates
                                    foreach (string item in currentInterests)
                                    {
                                        existingSet.Add(item);
                                    }

                                    string finalList = string.Join(", ", existingSet);

                                    lines[i] = username + " interested in: " + finalList;
                                    File.WriteAllLines(filename, lines);

                                    message += "great, i added " + store_interests + " to your interests and ";
                                    ActivityLogger.Log($"Added interests: {store_interests}", username);
                                    break;
                                }
                            }
                        }

                        if (!userFound)
                        {
                            File.AppendAllText(
                                filename,
                                username + " interested in: " + store_interests + "\n"
                            );

                            message += "great, i will remember that you are interested in " + store_interests + " and ";
                            ActivityLogger.Log($"Added interests: {store_interests}", username);
                        }
                    }
                    else
                    {
                        message += "Please specify what you're interested in (e.g., 'I am interested in cybersecurity')";
                    }
                }
                //end of interests

                // Search for matching answers
                bool wordFound = false;
                foreach (string answer in reply)
                {
                    if (answer.ToLower().Contains(word))
                    {
                        wordFound = true;
                        per_word.Add(answer);
                    }
                }

                if (wordFound && per_word.Count > 0)
                {
                    found = true;
                    int indexing = indexer.Next(0, per_word.Count);
                    answers_found.Add(per_word[indexing]);
                }
            }

            // Show responses or error message
            if (found && answers_found.Count > 0)
            {
                // Remove duplicate answers
                answers_found = answers_found.Distinct().ToList();

                foreach (string per_answer in answers_found)
                {
                    message += per_answer + "\n";
                }

                error_method("ChatBot", message.TrimEnd('\n'));
                ActivityLogger.Log($"Chatbot response: {message.TrimEnd('\n').Substring(0, Math.Min(50, message.Length))}...", username);

                chats.ScrollIntoView(chats.Items[chats.Items.Count - 1]);
            }
            else
            {
                // when nothing is found
                string[] fallbackMessages = {
                    "I'm sorry, I don't understand that. Could you rephrase your question?",
                    "I didn't quite get that. Try asking about cyber security topics.",
                    "Hmm, I'm not sure how to respond to that. Can you ask something else?",
                    "I couldn't find an answer for that. Please ask about programming, security, or technology.",
                    "My apologies, I don't have information on that topic yet."
                };

                Random random = new Random();
                string fallbackMessage = fallbackMessages[random.Next(fallbackMessages.Length)];
                error_method("ChatBot", fallbackMessage);
                ActivityLogger.Log($"Unknown query: {questions.Substring(0, Math.Min(30, questions.Length))}...", username);
            }

            // Clear the input box
            question.Clear();
        }
        //end of ai_chat method

        // Helper Methods for NLP Handlers
        private void HandleAddTask(string questions)
        {
            // Extract task title from user input
            string taskTitle = NLProcessor.ExtractTaskTitle(questions);

            if (string.IsNullOrWhiteSpace(taskTitle))
            {
                error_method("ChatBot", "What task would you like to add? Please specify the task details.");
                return;
            }

            // Check for reminder date
            DateTime? reminderDate = NLProcessor.ExtractReminderDate(questions);

            // Add to database
            DatabaseHelper db = new DatabaseHelper();
            if (db.AddTask(taskTitle, "", reminderDate, username))
            {
                string reminderMsg = reminderDate.HasValue ?
                    $" with reminder on {reminderDate.Value.ToShortDateString()}" : "";
                error_method("ChatBot", $"✅ Task added successfully! '{taskTitle}'{reminderMsg}");
                ActivityLogger.Log($"Added task: '{taskTitle}'{reminderMsg}", username);
            }
            else
            {
                error_method("ChatBot", "❌ Failed to add task. Please check your database connection.");
            }

            question.Clear();
        }

        private void HandleStartQuiz()
        {
            try
            {
                QuizManagerWindow quizWindow = new QuizManagerWindow(username);
                quizWindow.ShowDialog();
                ActivityLogger.Log("Started quiz", username);
            }
            catch (Exception ex)
            {
                error_method("ChatBot", $"Error starting quiz: {ex.Message}");
            }
        }

        private void HandleShowLog()
        {
            var activities = ActivityLogger.GetRecentActivities();
            if (activities.Count == 0)
            {
                error_method("ChatBot", "No activities logged yet. Start using the chatbot to log activities!");
                return;
            }

            string logMessage = "📊 **Recent Activities:**\n";
            for (int i = 0; i < activities.Count; i++)
            {
                logMessage += $"{i + 1}. {activities[i]}\n";
            }
            error_method("ChatBot", logMessage);
        }

        private void HandleShowHelp()
        {
            string helpMessage =
                "🤖 **Commands I understand:**\n\n" +
                "📋 **Task Commands:**\n" +
                "• 'add task: [task name]'\n" +
                "• 'remind me to [task] tomorrow'\n" +
                "• 'remind me in [X] days'\n\n" +
                "🎯 **Quiz Commands:**\n" +
                "• 'quiz' or 'start quiz'\n\n" +
                "📊 **Log Commands:**\n" +
                "• 'activity log' or 'show log'\n\n" +
                "💡 **Cybersecurity Topics:**\n" +
                "• Ask about passwords, phishing, or privacy\n" +
                "• I can give you safety tips!";

            error_method("ChatBot", helpMessage);
        }

        // Button Event Handlers for GUI Features
        private void OpenTaskManager(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(username))
            {
                error_method("ChatBot", "Please enter your username first!");
                return;
            }

            try
            {
                TaskManagerWindow taskWindow = new TaskManagerWindow(username);
                taskWindow.ShowDialog();
                ActivityLogger.Log("Opened Task Manager", username);
            }
            catch (Exception ex)
            {
                error_method("ChatBot", $"Error opening Task Manager: {ex.Message}");
            }
        }

        private void OpenQuizManager(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(username))
            {
                error_method("ChatBot", "Please enter your username first!");
                return;
            }

            try
            {
                QuizManagerWindow quizWindow = new QuizManagerWindow(username);
                quizWindow.ShowDialog();
                ActivityLogger.Log("Opened Quiz Manager", username);
            }
            catch (Exception ex)
            {
                error_method("ChatBot", $"Error opening Quiz: {ex.Message}");
            }
        }

        private void ShowActivityLog(object sender, RoutedEventArgs e)
        {
            var activities = ActivityLogger.GetRecentActivities();
            if (activities.Count == 0)
            {
                error_method("ChatBot", "No activities logged yet. Start using the chatbot to log activities!");
                return;
            }

            string logMessage = "📊 Recent Activities:\n";
            for (int i = 0; i < activities.Count; i++)
            {
                logMessage += $"{i + 1}. {activities[i]}\n";
            }

            error_method("ChatBot", logMessage);
        }

        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            string helpMessage =
                "🤖 **Cybersecurity Awareness Chatbot Help**\n\n" +
                "📋 **Tasks** - Manage your cybersecurity tasks with reminders\n" +
                "🎯 **Quiz** - Test your cybersecurity knowledge\n" +
                "📊 **Activity Log** - View your recent activities\n\n" +
                "💡 **You can also type commands like:**\n" +
                "• 'add task: Review privacy settings'\n" +
                "• 'quiz' - Start the quiz\n" +
                "• 'activity log' - Show recent activities\n" +
                "• 'remind me to update password tomorrow'\n\n" +
                "🔐 **Topics I can help with:**\n" +
                "• Password safety\n" +
                "• Phishing scams\n" +
                "• Privacy protection\n" +
                "• Safe browsing\n" +
                "• Two-factor authentication";

            error_method("ChatBot", helpMessage);
        }

        //method to remove special characters
        private string RemoveSpecialCharacters(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            StringBuilder sanitized = new StringBuilder();

            foreach (char c in input)
            {
                // Keep letters, numbers, spaces, and basic punctuation
                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '\'' || c == '-')
                {
                    sanitized.Append(c);
                }
                else
                {
                    // Replace other special characters with space
                    sanitized.Append(' ');
                }
            }

            // Clean up extra spaces and trim
            string result = sanitized.ToString();
            result = System.Text.RegularExpressions.Regex.Replace(result, @"\s+", " ").Trim();

            return result;
        }
        //end of method to remove special characters

        //method count to show interests randomly
        private void auto_show_interest()
        {
            //check if three times
            if (counting == 3)
            {
                //read the user's interests from file
                string filename = "interested_topic.txt";

                if (File.Exists(filename))
                {
                    string[] lines = File.ReadAllLines(filename);

                    //find the user's line
                    foreach (string line in lines)
                    {
                        if (line.StartsWith(username))
                        {
                            //get the interests part
                            int colonIndex = line.IndexOf("interested in:");
                            if (colonIndex >= 0)
                            {
                                string interests = line.Substring(colonIndex + 14).Trim();

                                //show reminder of interests
                                error_method("ChatBot", "Just a reminder, you are interested in " + interests + " and ");
                                ai_check(interests);
                                break;
                            }
                        }
                    }
                }

                //reset counting
                counting = 0;
            }
            else
            {
                //incrementing
                counting += 1;
            }
        }
        //end of count interest method

        // Updated error method with better formatting
        private void error_method(string name, string message)
        {
            // Create a border for chats
            Border messageBorder = new Border
            {
                Margin = new Thickness(0, 2, 0, 2),
                Padding = new Thickness(5, 3, 5, 3),
                CornerRadius = new CornerRadius(5)
            };

            // Set different background for user vs bot
            if (name.ToLower().Contains("chatbot") || name.ToLower().Contains("chat"))
            {// Light blue
                messageBorder.Background = new SolidColorBrush(Color.FromRgb(240, 248, 255));
                messageBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(173, 216, 230));
            }
            else
            {    // Light gray
                messageBorder.Background = new SolidColorBrush(Color.FromRgb(245, 245, 245));
                messageBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(211, 211, 211));
            }
            messageBorder.BorderThickness = new Thickness(1);

            TextBlock messageText = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(2)
            };

            // Set color based on sender
            Brush nameColor = (name.ToLower().Contains("chatbot") || name.ToLower().Contains("chat")) ?
                              Brushes.DarkBlue : Brushes.DarkGreen;

            Brush messageColor = Brushes.Black;

            messageText.Inlines.Add(new Run
            {
                Text = name + ": ",
                Foreground = nameColor,
                FontWeight = FontWeights.Bold
            });

            messageText.Inlines.Add(new Run
            {
                Text = message,
                Foreground = messageColor
            });

            messageBorder.Child = messageText;
            chats.Items.Add(messageBorder);

            chats.ScrollIntoView(chats.Items[chats.Items.Count - 1]);
        }//end of error method

    }//end of class
}//end of namespace
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace demo
{
    public partial class QuizManagerWindow : Window
    {
        private List<Question> questions;
        private int currentIndex = 0;
        private int score = 0;
        private bool quizStarted = false;
        private string username;

        public QuizManagerWindow(string user)
        {
            InitializeComponent();
            username = user;
            questions = QuestionBank.GetQuestions();
            progressText.Text = $"Question 0 of {questions.Count}";
            ActivityLogger.Log("Opened Quiz Manager", username);
        }

        private void StartQuiz(object sender, RoutedEventArgs e)
        {
            if (questions.Count == 0)
            {
                MessageBox.Show("No questions available!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            quizStarted = true;
            currentIndex = 0;
            score = 0;
            summaryPanel.Visibility = Visibility.Collapsed;
            startBtn.IsEnabled = false;
            nextBtn.IsEnabled = true;
            ShowQuestion();
            ActivityLogger.Log($"Started quiz - {questions.Count} questions", username);
        }

        private void ShowQuestion()
        {
            if (currentIndex >= questions.Count)
            {
                ShowSummary();
                return;
            }

            var q = questions[currentIndex];
            questionText.Text = q.Text;
            optionsList.ItemsSource = q.Options;
            feedbackText.Text = "";
            progressText.Text = $"Question {currentIndex + 1} of {questions.Count}";
            scoreText.Text = $"{score}/{currentIndex}";

            // Clear selection
            if (optionsList.ItemContainerGenerator != null)
            {
                foreach (var item in optionsList.Items)
                {
                    var container = optionsList.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                    if (container != null)
                    {
                        var radio = container.ContentTemplate.FindName("Options", container) as RadioButton;
                        if (radio != null) radio.IsChecked = false;
                    }
                }
            }
        }

        private void NextQuestion(object sender, RoutedEventArgs e)
        {
            // Get selected answer
            int selectedIndex = -1;
            for (int i = 0; i < optionsList.Items.Count; i++)
            {
                var container = optionsList.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter;
                if (container != null)
                {
                    var radio = container.ContentTemplate.FindName("Options", container) as RadioButton;
                    if (radio != null && radio.IsChecked == true)
                    {
                        selectedIndex = i;
                        break;
                    }
                }
            }

            if (selectedIndex == -1)
            {
                MessageBox.Show("Please select an answer!", "No Selection",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var q = questions[currentIndex];
            if (selectedIndex == q.CorrectAnswerIndex)
            {
                score++;
                feedbackText.Text = $"✅ Correct! {q.Explanation}";
                feedbackText.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                string correctAnswer = q.Options[q.CorrectAnswerIndex];
                feedbackText.Text = $"❌ Incorrect. The correct answer was: {correctAnswer}\n{q.Explanation}";
                feedbackText.Foreground = System.Windows.Media.Brushes.Red;
            }

            currentIndex++;

            if (currentIndex >= questions.Count)
            {
                nextBtn.IsEnabled = false;
                ShowSummary();
            }
            else
            {
                ShowQuestion();
            }
        }

        private void ShowSummary()
        {
            int total = questions.Count;
            double percentage = (double)score / total * 100;

            string feedback;
            if (percentage >= 80) feedback = "🌟 Excellent! You're a cybersecurity pro!";
            else if (percentage >= 60) feedback = "👍 Good job! Keep learning to improve!";
            else if (percentage >= 40) feedback = "📚 Not bad! Review the topics you missed.";
            else feedback = "💪 Keep practicing! Cybersecurity is important to learn.";

            summaryText.Text = $"Quiz Complete!\n\nScore: {score}/{total} ({percentage:F1}%)\n\n{feedback}";
            summaryPanel.Visibility = Visibility.Visible;
            progressText.Text = $"Completed! Score: {score}/{total}";
            scoreText.Text = $"{score}/{total}";
            questionText.Text = "Quiz finished!";
            optionsList.ItemsSource = null;

            ActivityLogger.Log($"Completed quiz - Score: {score}/{total} ({percentage:F1}%)", username);
        }

        private void ResetQuiz(object sender, RoutedEventArgs e)
        {
            currentIndex = 0;
            score = 0;
            quizStarted = false;
            summaryPanel.Visibility = Visibility.Collapsed;
            startBtn.IsEnabled = true;
            nextBtn.IsEnabled = false;
            questionText.Text = "Click 'Start Quiz' to begin!";
            optionsList.ItemsSource = null;
            feedbackText.Text = "";
            progressText.Text = $"Question 0 of {questions.Count}";
            scoreText.Text = "0/0";
            ActivityLogger.Log("Reset quiz", username);
        }

        private void CloseQuiz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
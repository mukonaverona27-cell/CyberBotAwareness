using System;
using System.Collections.Generic;

namespace demo
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; }
        public bool IsTrueFalse { get; set; } = false;
    }

    public static class QuestionBank
    {
        private static List<Question> questions;

        static QuestionBank()
        {
            InitializeQuestions();
        }

        private static void InitializeQuestions()
        {
            questions = new List<Question>
            {
                new Question
                {
                    Text = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it" },
                    CorrectAnswerIndex = 2,
                    Explanation = "Reporting phishing emails helps prevent scams and protects others."
                },
                new Question
                {
                    Text = "Which password is the strongest?",
                    Options = new List<string> { "password123", "MyDogBuster2024", "P@ssw0rd!", "D0g$@reC00l#2024" },
                    CorrectAnswerIndex = 3,
                    Explanation = "Strong passwords use a mix of uppercase, lowercase, numbers, and special characters."
                },
                new Question
                {
                    Text = "True or False: It's safe to use the same password for multiple accounts.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Using the same password across accounts puts all your accounts at risk if one gets compromised.",
                    IsTrueFalse = true
                },
                new Question
                {
                    Text = "What is phishing?",
                    Options = new List<string> { "A type of fish", "A cybersecurity attack where attackers impersonate legitimate organizations", "A programming language", "A social media platform" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Phishing attacks trick people into revealing sensitive information by pretending to be legitimate."
                },
                new Question
                {
                    Text = "True or False: Two-Factor Authentication (2FA) adds an extra layer of security.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswerIndex = 0,
                    Explanation = "2FA requires an additional verification step, making accounts more secure.",
                    IsTrueFalse = true
                },
                new Question
                {
                    Text = "What should you do before clicking a link in an email?",
                    Options = new List<string> { "Click it immediately", "Hover over the link to see the actual URL", "Forward the email to friends", "Reply to the sender" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Hovering over links reveals the actual destination, helping identify suspicious URLs."
                },
                new Question
                {
                    Text = "True or False: Public Wi-Fi is always safe to use for banking.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Public Wi-Fi networks can be insecure. Use a VPN or avoid sensitive transactions on public networks.",
                    IsTrueFalse = true
                },
                new Question
                {
                    Text = "What is social engineering in cybersecurity?",
                    Options = new List<string> { "Building social media profiles", "Manipulating people into revealing confidential information", "Engineering social networks", "Creating social apps" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Social engineering exploits human psychology rather than technical vulnerabilities."
                },
                new Question
                {
                    Text = "True or False: Ransomware encrypts your files and demands payment.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswerIndex = 0,
                    Explanation = "Ransomware is malware that encrypts files and demands payment for decryption."
                },
                new Question
                {
                    Text = "What is the best practice for creating a password?",
                    Options = new List<string> { "Use your birthday", "Use a common word", "Use a passphrase with numbers and symbols", "Use your pet's name" },
                    CorrectAnswerIndex = 2,
                    Explanation = "Passphrases with numbers and symbols are harder to crack."
                },
                new Question
                {
                    Text = "True or False: It's okay to share your password with friends or family.",
                    Options = new List<string> { "True", "False" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Passwords should be kept private and never shared with anyone.",
                    IsTrueFalse = true
                },
                new Question
                {
                    Text = "What should you do if you suspect your account has been hacked?",
                    Options = new List<string> { "Ignore it", "Change your password immediately", "Wait and see", "Tell your friends" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Immediately change your password and enable 2FA if available."
                }
            };
        }

        public static List<Question> GetQuestions()
        {
            return questions;
        }

        public static int TotalQuestions => questions.Count;
    }
}
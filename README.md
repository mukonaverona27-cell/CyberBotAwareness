# CyberBotAwareness
Cyber Security Awareness ChatBot
🛡️ Cybersecurity Awareness Chatbot

Module: Programming 2A (PROG6221/w)

Institution: The Independent Institute of Education (IIE)

Assessment: Portfolio of Evidence (POE) — 300 Marks


📋 Table of Contents


Project Overview
Features
Technologies Used
Prerequisites
Getting Started
Project Structure
Part 1 — Console Chatbot
Part 2 — GUI Chatbot
Part 3 / POE — Advanced Features
Database Setup
GitHub & CI/CD
Video Presentations
References



📖 Project Overview

The Cybersecurity Awareness Chatbot is a virtual assistant designed to educate South African citizens on identifying and mitigating common cyber threats. Built in response to the rise in cyberattacks targeting individuals, businesses, and government institutions in South Africa (Pieterse, 2021), this chatbot simulates real-life cybersecurity scenarios and guides users through topics such as:


Phishing email identification
Safe password practices
Recognising suspicious links
Privacy and data protection
Social engineering awareness


The project is developed across three parts, progressively evolving from a command-line application into a fully featured GUI-based cybersecurity education tool.


✨ Features

Part 1 — Console Application


🔊 Voice Greeting — WAV audio welcome message on launch
🖼️ ASCII Art Logo — Cybersecurity-themed header display
👤 Personalised Interaction — Greets user by name
💬 Basic Response System — Answers questions on passwords, phishing, and safe browsing
✅ Input Validation — Handles empty or unrecognised inputs gracefully
🎨 Enhanced Console UI — Colour-coded text, borders, and typing effect


Part 2 — GUI Application (WPF / WinForms)


🖥️ Graphical User Interface — All Part 1 features translated into a GUI
🔑 Keyword Recognition — Detects cybersecurity topics (e.g., "password", "scam", "privacy")
🎲 Random Responses — Varied, non-repetitive answers for common topics
🔄 Conversation Flow — Handles follow-up questions ("tell me more", "explain further")
🧠 Memory & Recall — Remembers user details and personalises later responses
😊 Sentiment Detection — Adapts tone based on user mood (worried, curious, frustrated)
🛡️ Error Handling — Graceful fallback for unknown inputs


Part 3 / POE — Advanced Features


📝 Task Assistant — Add, view, complete, and delete cybersecurity tasks
🗄️ MySQL Database Integration — Persistent task and reminder storage
🎮 Cybersecurity Quiz Mini-Game — 10+ questions with immediate feedback and scoring
🤖 NLP Simulation — Flexible keyword detection for natural user input
📋 Activity Log — Tracks and displays the last 5–10 chatbot actions
🔗 Full Integration — All features from Parts 1, 2, and 3 work cohesively



🛠️ Technologies Used

TechnologyPurposeC# (.NET)Core programming languageWPF / WinFormsGUI framework (Part 2 & 3)System.MediaWAV audio playback (voice greeting)MySQLTask and reminder database (Part 3)GitHub ActionsContinuous Integration (CI) workflowXAMLUI layout and styling


✅ Prerequisites

Before running this project, ensure you have the following installed:


Visual Studio 2022 (Community or higher)

Workloads: .NET desktop development



.NET Framework 4.7.2+ or .NET 6+
MySQL Server (for Part 3 database features)
MySQL Connector/NET NuGet package



🚀 Getting Started

1. Clone the Repository

bashgit clone https://github.com/<your-username>/CybersecurityAwarenessBot.git
cd CybersecurityAwarenessBot

2. Open in Visual Studio


Open CybersecurityAwarenessBot.sln in Visual Studio.


3. Restore NuGet Packages

Tools → NuGet Package Manager → Restore NuGet Packages

Or via terminal:

bashdotnet restore

4. Configure the Database (Part 3 only)

See the Database Setup section below.

5. Build and Run


Press F5 or click Start in Visual Studio.
Alternatively, from the terminal:


bashdotnet run


⚠️ Note: Marks are only awarded for working, running software. Ensure the project compiles without errors before submission.




📁 Project Structure

CybersecurityAwarenessBot/
│
├── Part1_Console/                  # Part 1 — Console Application
│   ├── Program.cs                  # Entry point
│   ├── ChatBot.cs                  # Core chatbot logic
│   ├── ResponseHandler.cs          # Response system and input validation
│   ├── ConsoleUI.cs                # ASCII art, colours, and formatting
│   ├── VoiceGreeting.cs            # WAV audio playback
│   └── Assets/
│       └── greeting.wav            # Voice greeting audio file
│
├── Part2_GUI/                      # Part 2 — GUI Application
│   ├── MainWindow.xaml             # GUI layout
│   ├── MainWindow.xaml.cs          # GUI code-behind
│   ├── ChatBot.cs                  # Extended chatbot logic
│   ├── KeywordHandler.cs           # Keyword recognition
│   ├── SentimentDetector.cs        # Sentiment detection
│   ├── MemoryManager.cs            # User memory and recall
│   └── Assets/
│       └── greeting.wav
│
├── Part3_POE/                      # Part 3 / POE — Full Application
│   ├── MainWindow.xaml
│   ├── MainWindow.xaml.cs
│   ├── TaskAssistant.cs            # Task management feature
│   ├── DatabaseHelper.cs           # MySQL database integration
│   ├── QuizGame.cs                 # Cybersecurity mini-game
│   ├── NLPSimulator.cs             # NLP keyword detection
│   ├── ActivityLog.cs              # Activity log feature
│   └── Assets/
│       └── greeting.wav
│
├── .github/
│   └── workflows/
│       └── ci.yml                  # GitHub Actions CI workflow
│
└── README.md                       # This file


Part 1 — Console Chatbot

Running Part 1


Set Part1_Console as the Startup Project in Visual Studio.
Press F5 to run.
The application will:

Play the WAV voice greeting
Display the ASCII art logo
Ask for your name
Begin the cybersecurity conversation





Sample Interaction

============================================
   🛡️  CYBERSECURITY AWARENESS BOT 🛡️
============================================

[Voice greeting plays]

Hello! What is your name?
> Alice

Welcome, Alice! I'm your Cybersecurity Awareness Assistant.
How can I help you stay safe online today?

You can ask me about:
  • Password Safety
  • Phishing Emails
  • Safe Browsing
  • Privacy Tips

> Tell me about phishing

Bot: Phishing is when attackers disguise themselves as trusted
     organisations to steal your information. Always verify the
     sender's email address before clicking any links.


Part 2 — GUI Chatbot

Running Part 2


Set Part2_GUI as the Startup Project.
Press F5 to launch the GUI window.
The voice greeting plays automatically on launch.
Type your message in the input field and press Send or Enter.


Supported Keywords

KeywordTopicpasswordPassword safety tipsphishingPhishing email awarenessscamOnline scam preventionprivacyData privacy guidancemalwareMalware protection2fa / two-factorTwo-factor authenticationbrowsingSafe browsing practices

Sentiment Detection Examples

User InputDetected SentimentBot Response Style"I'm worried about scams"WorriedReassuring and supportive"I'm curious about phishing"CuriousInformative and engaging"This is so frustrating!"FrustratedPatient and encouraging


Part 3 / POE — Advanced Features

Task Assistant

Users can manage cybersecurity tasks through natural language commands:

User: Add task - Enable two-factor authentication
Bot:  Task added! Description: "Enable two-factor authentication 
      to add an extra layer of security to your accounts."
      Would you like to set a reminder?

User: Yes, remind me in 5 days
Bot:  Got it! I'll remind you on [date].

Task Commands:


add task [description] — Add a new task
view tasks / show my tasks — List all tasks
complete task [title] — Mark a task as done
delete task [title] — Remove a task


Cybersecurity Quiz

Start the quiz with:

User: Start quiz


10+ multiple-choice and true/false questions
Covers phishing, password safety, safe browsing, and social engineering
Immediate feedback after each answer
Final score and performance feedback at the end


Activity Log

View recent chatbot actions:

User: Show activity log

Bot: Here's a summary of recent actions:
  1. Task added: 'Enable two-factor authentication' (Reminder: 5 days)
  2. Quiz started — 8/10 questions answered correctly
  3. Reminder set: 'Review privacy settings' on [date]
  4. Keyword detected: 'phishing' — tip provided
  5. Task marked complete: 'Change email password'


🗄️ Database Setup

Part 3 requires a MySQL database for task and reminder storage.

1. Create the Database

sqlCREATE DATABASE cybersecurity_bot;
USE cybersecurity_bot;

CREATE TABLE tasks (
    id INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    reminder_date DATE,
    is_completed BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

2. Update the Connection String

In DatabaseHelper.cs, update the connection string with your MySQL credentials:

csharpprivate string connectionString = 
    "Server=localhost;Database=cybersecurity_bot;User ID=root;Password=your_password;";

3. Verify Connection

Run the application and attempt to add a task. If the task persists after restarting the app, the database connection is working correctly.


🔧 GitHub & CI/CD

Continuous Integration

This project uses GitHub Actions for automated CI on every push.

The workflow file is located at:

.github/workflows/ci.yml

CI checks include:


Project builds successfully
No syntax errors detected
Code formatting verified


CI Workflow Status


📸 (Insert screenshot of green CI check from GitHub Actions here)



Commit History

This repository maintains a minimum of 6 meaningful commits per part, for example:

✅ Initial commit: Set up project structure and main files
✅ Added voice greeting and ASCII art display
✅ Implemented basic response system and input validation
✅ Added keyword recognition and random responses
✅ Implemented sentiment detection and memory features
✅ Added GUI interface with WPF/WinForms
✅ Integrated MySQL database for task storage
✅ Added cybersecurity quiz mini-game
✅ Implemented NLP simulation and activity log
✅ Final POE: Integrated all parts and polished UI

Releases & Tags

TagDescriptionv1.0Part 1 — Console chatbot completev2.0Part 2 — GUI with keyword recognition and sentiment detectionv3.0Part 3 / POE — Full feature set with database and mini-game


🎥 Video Presentations

All presentations are unlisted YouTube videos with voice-over explanations.

PartYouTube LinkDurationPart 1(Insert YouTube link here)~5 minPart 2(Insert YouTube link here)~5 minPart 3 / POE(Insert YouTube link here)8–10 min


⚠️ All videos include the presenter's own voice. No AI-generated voices are used.




📚 References

Pieterse, H. 2021. The Cyber Threat Landscape in South Africa: A 10-Year Review. The African Journal of Information and Communication, 28(28). doi: https://doi.org/10.23962/10539/32213. [Online]. Available at: https://www.scielo.org.za/scielo.php?pid=S2077-72132021000200003&script=sci_arttext [Accessed 16 February 2026].


👤 Author

Student Name: (Your Full Name)

Student Number: (Your Student Number)

Module: Programming 2A — PROG6221/w

Institution: The Independent Institute of Education (IIE)

Year: 2026



using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace demo
{
	public class DatabaseHelper
	{
		private string connectionString = "Server=localhost;Database=CybersecurityBot;Uid=root;Pwd=;";

		public bool TestConnection()
		{
			try
			{
				using (var conn = new MySqlConnection(connectionString))
				{
					conn.Open();
					return true;
				}
			}
			catch
			{
				return false;
			}
		}

		public bool AddTask(string title, string description, DateTime? reminderDate, string username)
		{
			try
			{
				using (var conn = new MySqlConnection(connectionString))
				{
					conn.Open();
					string query = @"INSERT INTO Tasks (Title, Description, ReminderDate, Username) 
                                    VALUES (@Title, @Description, @ReminderDate, @Username)";
					using (var cmd = new MySqlCommand(query, conn))
					{
						cmd.Parameters.AddWithValue("@Title", title);
						cmd.Parameters.AddWithValue("@Description", description ?? "");
						cmd.Parameters.AddWithValue("@ReminderDate", reminderDate ?? (object)DBNull.Value);
						cmd.Parameters.AddWithValue("@Username", username);
						return cmd.ExecuteNonQuery() > 0;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error adding task: {ex.Message}");
				return false;
			}
		}

		public DataTable GetTasks(string username)
		{
			try
			{
				using (var conn = new MySqlConnection(connectionString))
				{
					conn.Open();
					string query = "SELECT TaskID, Title, Description, ReminderDate, IsCompleted FROM Tasks WHERE Username = @Username ORDER BY CreatedAt DESC";
					using (var cmd = new MySqlCommand(query, conn))
					{
						cmd.Parameters.AddWithValue("@Username", username);
						using (var adapter = new MySqlDataAdapter(cmd))
						{
							DataTable dt = new DataTable();
							adapter.Fill(dt);
							return dt;
						}
					}
				}
			}
			catch
			{
				return new DataTable();
			}
		}

		public bool UpdateTaskStatus(int taskId, bool isCompleted)
		{
			try
			{
				using (var conn = new MySqlConnection(connectionString))
				{
					conn.Open();
					string query = "UPDATE Tasks SET IsCompleted = @IsCompleted WHERE TaskID = @TaskID";
					using (var cmd = new MySqlCommand(query, conn))
					{
						cmd.Parameters.AddWithValue("@IsCompleted", isCompleted);
						cmd.Parameters.AddWithValue("@TaskID", taskId);
						return cmd.ExecuteNonQuery() > 0;
					}
				}
			}
			catch
			{
				return false;
			}
		}

		public bool DeleteTask(int taskId)
		{
			try
			{
				using (var conn = new MySqlConnection(connectionString))
				{
					conn.Open();
					string query = "DELETE FROM Tasks WHERE TaskID = @TaskID";
					using (var cmd = new MySqlCommand(query, conn))
					{
						cmd.Parameters.AddWithValue("@TaskID", taskId);
						return cmd.ExecuteNonQuery() > 0;
					}
				}
			}
			catch
			{
				return false;
			}
		}
	}
}
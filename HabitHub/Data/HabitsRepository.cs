using HabitHub.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Reflection.PortableExecutable;

namespace HabitHub.Data;

public static class HabitsRepository
{
	/// <summary>
	/// Adds a new record to the 'habits' table.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="habit"></param>
	public static void AddHabit(SqliteConnection connection, HabitModel habit)
	{
		connection.Open();
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			$@"INSERT INTO habits (habit_name)
                       VALUES('{habit.HabitName}');";
		tableCmd.ExecuteNonQuery();
	}

	/// <summary>
	/// Retrieves all data from the 'habits' table.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="habits"></param>
	public static void GetHabits(SqliteConnection connection, List<HabitModel> habits)
	{
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@"SELECT * FROM habits;";
		tableCmd.CommandType = System.Data.CommandType.Text;
		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			habits.Add(new HabitModel
			{
				Id = int.Parse(reader["id"].ToString()),
				HabitName = reader["habit_name"].ToString()
			});
		}
		reader.Close();
	}

	/// <summary>
	/// Retrieves all habit names form the 'habits' table.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="habitNames"></param>
	public static void GetHabitNames(SqliteConnection connection, List<string> habitNames)
	{
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@"SELECT habit_name FROM habits;";
		tableCmd.CommandType = System.Data.CommandType.Text;
		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			habitNames.Add(Convert.ToString(reader["habit_name"]));
		}
	}

	/// <summary>
	/// Deletes a record from the 'habits' table based on the habit name given.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="habitName"></param>
	public static void DeleteHabit(SqliteConnection connection, string habitName)
	{
		connection.Open();
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			$@"DELETE FROM habits
			   WHERE habit_name LIKE %{habitName}%;";
		tableCmd.ExecuteNonQuery();
	}

	/// <summary>
	/// Adds a habit record to the 'habit_records' table.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="habitToRecord"></param>
	/// <param name="habitRecord"></param>
	public static void AddHabitRecord(SqliteConnection connection, string habitToRecord, HabitRecordModel habitRecord)
	{
		connection.Open();
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@$"INSERT INTO habit_records(habits_id, amount, unit, date)
			   VALUES((SELECT id
					   FROM habits
					   WHERE habit_name = '{habitToRecord}'),
					   '{habitRecord.Amount}', '{habitRecord.Unit}', '{habitRecord.Date}');";
		tableCmd.ExecuteNonQuery();
	}

	/// <summary>
	/// Retrieves all data from the 'habit_records' table.
	/// </summary>
	/// <param name="connection"></param>
	/// <param name="habitRecords"></param>
	public static void GetHabitRecords(SqliteConnection connection, List<HabitRecordModel> habitRecords)
	{
		var tableCmd = connection.CreateCommand();
		tableCmd.CommandText =
			@"SELECT * FROM habit_records;";
		tableCmd.CommandType = System.Data.CommandType.Text;
		SqliteDataReader reader = tableCmd.ExecuteReader();
		while (reader.Read())
		{
			habitRecords.Add(new HabitRecordModel
			{
				Id = int.Parse(reader["id"].ToString()),
				HabitsId = int.Parse(reader["habits_id"].ToString()),
				Amount = float.Parse(reader["amount"].ToString()),
				Unit = reader["unit"].ToString(),
				Date = DateTime.Parse(reader["date"].ToString())
			});
		}
		reader.Close();
	}
}

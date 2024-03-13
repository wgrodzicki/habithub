using HabitHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace HabitHub.Pages
{
    public class ViewHabitsModel : PageModel
	{
		[BindProperty] public List<HabitModel> Habits { get; set; }
		[BindProperty] public List<HabitRecordModel> HabitRecords { get; set; }

		private readonly IConfiguration _configuration;

		public ViewHabitsModel(IConfiguration configuration)
		{
			_configuration = configuration;
			Habits = new List<HabitModel>();
			HabitRecords = new List<HabitRecordModel>();
		}

		public IActionResult OnGet()
        {
			using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
			{
				connection.Open();
				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText =
					@"SELECT * FROM habits;";
				tableCmd.CommandType = System.Data.CommandType.Text;
				SqliteDataReader reader = tableCmd.ExecuteReader();
				while (reader.Read())
				{
					Habits.Add(new HabitModel
					{
						Id = int.Parse(reader["id"].ToString()),
						HabitName = reader["habit_name"].ToString()
					});
				}
				reader.Close();

				tableCmd.CommandText =
					@"SELECT * FROM habit_records;";
				tableCmd.CommandType = System.Data.CommandType.Text;
				reader = tableCmd.ExecuteReader();
				while (reader.Read())
				{
					HabitRecords.Add(new HabitRecordModel
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
			return Page();
		}

		public string GetHabitName(HabitRecordModel habitRecord)
		{
			var habit = Habits.FirstOrDefault(x => x.Id == habitRecord.HabitsId);
			if (habit != null)
				return habit.HabitName.ToString();
			else
				return "";
		}
    }
}

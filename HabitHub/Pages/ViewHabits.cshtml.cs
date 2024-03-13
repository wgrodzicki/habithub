using HabitHub.Data;
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

				HabitsRepository.GetHabits(connection, Habits);
				HabitsRepository.GetHabitRecords(connection, HabitRecords);
			}
			return Page();
		}

		/// <summary>
		/// Returns the name of the habit corresponding to the record given.
		/// </summary>
		/// <param name="habitRecord"></param>
		/// <returns></returns>
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

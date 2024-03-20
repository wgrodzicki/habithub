using HabitHub.Data;
using HabitHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace HabitHub.Pages
{
    public class ViewHabitsModel : PageModel
	{
		[BindProperty] public List<HabitModel> Habits { get; set; }
		[BindProperty] public List<HabitRecordModel> HabitRecords { get; set; }
		[BindProperty] public int RecordToEdit { get; set; }
		[BindProperty] public string HabitToUpdate { get; set; }
		[BindProperty] public HabitRecordModel RecordToUpdate { get; set; }
		[BindProperty] public int RecordToDelete { get; set; }
		public List<string> SavedHabits { get; set; }
		public int RecordsTableRowCounter { get; set; } = 0;

		private readonly IConfiguration _configuration;

		public ViewHabitsModel(IConfiguration configuration)
		{
			_configuration = configuration;
			Habits = new List<HabitModel>();
			HabitRecords = new List<HabitRecordModel>();
			SavedHabits = new List<string>();
		}

		public IActionResult OnGet()
        {
			using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
			{
				connection.Open();
				HabitsRepository.GetAllHabits(connection, Habits);
				HabitsRepository.GetAllHabitRecords(connection, HabitRecords);
				HabitsRepository.GetAllHabitNames(connection, SavedHabits);
			}
			return Page();
		}

		public IActionResult OnPostEdit()
		{
			using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
			{
				connection.Open();
				int habitIndex = HabitsRepository.GetHabitIndex(connection, HabitToUpdate);

				// Make sure habit exists
				if (habitIndex != -1)
					HabitsRepository.UpdateRecord(connection, habitIndex, RecordToUpdate);
			}
			return OnGet();
		}

		public IActionResult OnPostDelete()
		{
			using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
			{
				connection.Open();
				HabitsRepository.DeleteRecord(connection, RecordToDelete);
			}
			return OnGet();
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

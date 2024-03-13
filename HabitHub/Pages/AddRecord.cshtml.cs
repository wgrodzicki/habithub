using HabitHub.Data;
using HabitHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace HabitHub.Pages
{
    public class AddRecordModel : PageModel
    {
		[BindProperty] public HabitRecordModel HabitRecord { get; set; }
		[BindProperty] public string HabitToSave { get; set; }
		public List<string> SavedHabits { get; set; }


		private readonly IConfiguration _configuration;

        public AddRecordModel(IConfiguration configuration)
        {
            _configuration = configuration;
            SavedHabits = new List<string>();
        }

		public IActionResult OnGet()
		{
			using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
			{
				connection.Open();

                HabitsRepository.GetHabitNames(connection, SavedHabits);
			}
			return Page();
		}

        public IActionResult OnPost()
        {
			if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    @$"INSERT INTO habit_records(habits_id, amount, unit, date)
					   VALUES((SELECT id
							   FROM habits
							   WHERE habit_name = '{HabitToSave}'),
							   '{HabitRecord.Amount}', '{HabitRecord.Unit}', '{HabitRecord.Date}');";
                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }
	}
}

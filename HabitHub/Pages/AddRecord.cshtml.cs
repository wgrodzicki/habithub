using HabitHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace HabitHub.Pages
{
    public class AddRecordModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public AddRecordModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public HabitRecordModel HabitRecord { get; set; }

		public IActionResult OnGet()
		{
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
                    @$"INSERT INTO habit_records(amount, unit, date)
                      VALUES('{HabitRecord.Amount}', '{HabitRecord.Unit}', '{HabitRecord.Date}')";
                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }
	}
}

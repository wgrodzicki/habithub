using HabitHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace HabitHub.Pages
{
    public class AddHabitModel : PageModel
    {
        [BindProperty]
        public HabitModel Habit { get; set; }

        private readonly IConfiguration _configuration;

        public AddHabitModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
                    $@"INSERT INTO habits (habit_name)
                       VALUES('{Habit.HabitName}')";
                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }
    }
}

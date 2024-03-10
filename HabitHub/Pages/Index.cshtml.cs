using HabitHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace HabitHub.Pages
{
    public class IndexModel : PageModel
    {
        public HabitModel Habit { get; set; }

        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {
            // 
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    @$"SELECT habit_name FROM habits";
                tableCmd.CommandType = System.Data.CommandType.Text;
                SqliteDataReader reader = tableCmd.ExecuteReader();
                while (reader.Read())
                {
                    string? habitName = Convert.ToString(reader["habit_name"]);
                }
            }
        }
    }
}

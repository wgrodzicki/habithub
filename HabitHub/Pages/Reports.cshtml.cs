using HabitHub.Data;
using HabitHub.Helpers;
using HabitHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System.Net.Mime;

namespace HabitHub.Pages
{
    public class ReportsModel : PageModel
    {
        private const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        private readonly IConfiguration _configuration;

        public ReportsModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPostGenerateReport()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(DateTime.Now.ToShortDateString());

                List<HabitModel> habits = new List<HabitModel>();
                List<HabitRecordModel> habitRecords = new List<HabitRecordModel>();

                using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
                {
                    connection.Open();
                    HabitsRepository.GetAllHabits(connection, habits);
                    HabitsRepository.GetAllHabitRecords(connection, habitRecords);
                }

                worksheet = ReportHandler.PopulateReport(worksheet, habits, habitRecords);

                var excelData = package.GetAsByteArray();
                string fileName = $"habithub-report.xlsx";

                return File(excelData, ContentType, fileName);
            }
        }
    }
}

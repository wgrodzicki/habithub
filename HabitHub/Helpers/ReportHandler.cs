using HabitHub.Data;
using HabitHub.Models;
using OfficeOpenXml;

namespace HabitHub.Helpers;

public static class ReportHandler
{
    public static ExcelWorksheet PopulateReport(ExcelWorksheet worksheet, List<HabitModel> habits, List<HabitRecordModel> habitRecords)
    {
        worksheet.Cells["A1:D1"].Style.Font.Size = 14.0f;
        worksheet.Cells["A1:D1"].Style.Font.Bold = true;
        worksheet.Cells["A1"].Value = "Habit";
        worksheet.Cells["B1"].Value = "Amount";
        worksheet.Cells["C1"].Value = "Unit";
        worksheet.Cells["D1"].Value = "Date";



        return worksheet;
    }
}


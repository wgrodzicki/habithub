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

        // Habit names
        List<string> habitNames = new List<string>();
        foreach (HabitRecordModel habitRecord in habitRecords)
        {
            var habit = habits.Find(x => x.Id == habitRecord.HabitsId);

            if (habit != null)
                habitNames.Add(habit.HabitName);
        }
        worksheet.Cells[2, 1, habitRecords.Count + 1, 1].FillList(habitNames);

        // Amounts
        var amounts = habitRecords.Select(x => x.Amount).ToList();
        worksheet.Cells[2, 2,  habitRecords.Count + 1, 2].FillList(amounts);

        // Units
        var units = habitRecords.Select(x => x.Unit).ToList();
        worksheet.Cells[2, 3, habitRecords.Count + 1, 3].FillList(units);
        
        // Dates
        var dates = habitRecords.Select(x => x.Date.ToString()).ToList();
        worksheet.Cells[2, 4, habitRecords.Count + 1, 4].FillList(dates);

        return worksheet;
    }
}


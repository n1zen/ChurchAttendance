using ChurchAttendanceApp.Data;
using ChurchAttendanceApp.Models;
using ClosedXML.Excel;
using System.Text;

namespace ChurchAttendanceApp.Services
{
    public class ExportService
    {
        private readonly ChurchDbContext _context;

        public ExportService(ChurchDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]> ExportMembersCSV(List<Member> members, List<string> columns)
        {
            return GenerateCSV(members, columns);
        }

        public async Task<byte[]> ExportMembersExcel(List<Member> members, List<string> columns)
        {
            return GenerateExcel(members, columns, "Members");
        }

        public async Task<byte[]> ExportMemberDetailsExcel(List<Member> member, List<AttendanceRecord> records, List<string> memberColumns, List<string> recordColumns, string memberId)
        {
            return GenerateDetailsExcel(member, records, memberColumns, recordColumns, memberId);
        }

        private byte[] GenerateCSV<T>(List<T> data, List<string> columns)
        {
            var csv = new StringBuilder();
            csv.AppendLine(string.Join(",", columns));

            foreach(var row in data)
            {
                var values = columns.Select(col =>
                {
                    var value = typeof(T).GetProperty(col)?.GetValue(row)?.ToString() ?? "";
                    return value.Contains(",") ? $"\"{value}\"" : value; // Handle commas in values
                });
                csv.AppendLine(string.Join(",", values));
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }

        private byte[] GenerateExcel<T>(List<T> data, List<string> columns, string sheetName)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(sheetName);

            // Headers
            for (int i = 0; i < columns.Count; i++)
                worksheet.Cell(1, i + 1).Value = columns[i];

            // Rows
            for (int rowIdx = 0; rowIdx < data.Count; rowIdx++)
            {
                for (int colIdx = 0; colIdx < columns.Count; colIdx++)
                {
                    var value = typeof(T).GetProperty(columns[colIdx])?.GetValue(data[rowIdx])?.ToString() ?? "";
                    worksheet.Cell(rowIdx + 2, colIdx + 1).Value = value;
                }
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        private byte[] GenerateDetailsExcel<TMember, TRecord>(List<TMember> memberData, List<TRecord> recordsData, List<string> memberColumns, List<string> recordsColumns, string memberId)
        {
            using var workbook = new XLWorkbook();
            var memberWorksheet = workbook.Worksheets.Add(memberId);
            var attendanceRecordsWorksheet = workbook.Worksheets.Add("Attendance Records");

            // Member Headers
            for (int i = 0; i < memberColumns.Count; i++)
                memberWorksheet.Cell(1, i + 1).Value = memberColumns[i];

            // Attendance Records Headers
            for (int i = 0; i < recordsColumns.Count; i++)
                attendanceRecordsWorksheet.Cell(1, i + 1).Value = recordsColumns[i];

            // Member Rows
            for (int rowIdx = 0; rowIdx < memberData.Count; rowIdx++)
            {
                for (int colIdx = 0; colIdx < memberColumns.Count; colIdx++)
                {
                    var value = typeof(TMember).GetProperty(memberColumns[colIdx])?.GetValue(memberData[rowIdx])?.ToString() ?? "";
                    memberWorksheet.Cell(rowIdx + 2, colIdx + 1).Value = value;
                }
            }

            // Attendance Records Rows
            for (int rowIdx = 0; rowIdx < recordsData.Count; rowIdx++)
            {
                for (int colIdx = 0; colIdx < recordsColumns.Count; colIdx++)
                {
                    var value = typeof(TRecord).GetProperty(recordsColumns[colIdx])?.GetValue(recordsData[rowIdx])?.ToString() ?? "";
                    attendanceRecordsWorksheet.Cell(rowIdx + 2, colIdx + 1).Value = value;
                }
            }

            memberWorksheet.Columns().AdjustToContents();
            attendanceRecordsWorksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }

}

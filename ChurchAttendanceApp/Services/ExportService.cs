using System.Text;
using ChurchAttendanceApp.Data;
using ChurchAttendanceApp.Models;

namespace ChurchAttendanceApp.Services
{
    public class ExportService
    {
        private readonly ChurchDbContext _context;

        public ExportService(ChurchDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]> ExportMembers(List<Member> members, List<string> columns)
        {
            return GenerateCSV(members, columns);
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
    }

}

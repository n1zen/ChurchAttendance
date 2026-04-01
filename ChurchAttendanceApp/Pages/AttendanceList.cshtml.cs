using ChurchAttendanceApp.Models;
using ChurchAttendanceApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChurchAttendanceApp.Pages
{
    [Authorize(Roles = "Admin")]
    public class AttendanceListModel : PageModel
    {
        private readonly AttendanceService _attendanceService;
        private readonly ExportService _exportService;

        [BindProperty(SupportsGet = true)]
        public DateOnly CurrentDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        public AttendanceListModel(AttendanceService attendanceService, ExportService exportService)
        {
            _attendanceService = attendanceService;
            _exportService = exportService;
        }

        public List<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();

        public List<Member> Members { get; set; } = new List<Member>();


        public void OnGet()
        {
            AttendanceRecords = _attendanceService.GetByDate(CurrentDate);
            var members = AttendanceRecords.Select(a => a.Member!).ToList();
            Members = members;
        }

        public async Task<IActionResult> OnPostExportCSV()
        {
            AttendanceRecords = _attendanceService.GetByDate(CurrentDate);
            var members = AttendanceRecords.Select(a => a.Member!).ToList();

            var columns = new List<string> { "Id", "MemberId", "Name", "Gender", "Birthday", "DateBaptized", "ChurchOfOrigin", "Address", "Email", "Phone", "MembershipStatus", "AttendanceDate", "DateRegistered" };
            var file = await _exportService.ExportMembersCSV(members, columns);
            return File(file, "text/csv", $"attendance-{CurrentDate}.csv");
        }

        public async Task<IActionResult> OnPostExportExcel()
        {
            AttendanceRecords = _attendanceService.GetByDate(CurrentDate);
            var members = AttendanceRecords.Select(a => a.Member!).ToList();

            var columns = new List<string> { "Id", "MemberId", "Name", "Gender", "Birthday", "DateBaptized", "ChurchOfOrigin", "Address", "Email", "Phone", "MembershipStatus", "AttendanceDate", "DateRegistered" };

            var file = await _exportService.ExportMemberExcel(members, columns);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"attendance-{CurrentDate}.xlsx");
        }
    }
}

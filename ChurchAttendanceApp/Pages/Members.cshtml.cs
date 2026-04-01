using ChurchAttendanceApp.Models;
using ChurchAttendanceApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChurchAttendanceApp.Pages
{
    [Authorize(Roles = "Admin")]
    public class MembersModel : PageModel
    {
        private readonly MemberService _memberService;
        private readonly ExportService _exportService;
        public List<Member> Members { get; set; } = [];

        public DateOnly dateToday = DateOnly.FromDateTime(DateTime.Today);

        public MembersModel(MemberService memberService, ExportService exportService)
        {
            _memberService = memberService;
            _exportService = exportService;
        }
        public void OnGet()
        {
            Members = _memberService.GetAll();
        }

        public async Task<IActionResult> OnPostExportCSV()
        {
            var members = _memberService.GetAll();

            var columns = new List<string> { "Id", "MemberId", "Name", "Gender", "Birthday", "DateBaptized", "ChurchOfOrigin", "Address", "Email", "Phone", "MembershipStatus", "AttendanceDate", "DateRegistered" };

            var file =  await _exportService.ExportMembersCSV(members, columns);
            return File(file, "text/csv", "members.csv");
        }

        public async Task<IActionResult> OnPostExportExcel()
        {
            var members = _memberService.GetAll();

            var columns = new List<string> { "Id", "MemberId", "Name", "Gender", "Birthday", "DateBaptized", "ChurchOfOrigin", "Address", "Email", "Phone", "MembershipStatus", "AttendanceDate", "DateRegistered" };

            var file = await _exportService.ExportMembersExcel(members, columns);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "members.xlsx");
        }

        public bool CheckAttendance(Member member)
        {
            if (member.AttendanceDate == dateToday)
                return true;

            return false;
        }
    }
}

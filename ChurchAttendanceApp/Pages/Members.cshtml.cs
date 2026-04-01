using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChurchAttendanceApp.Services;
using ChurchAttendanceApp.Models;
using Microsoft.AspNetCore.Authorization;

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

        public async Task<IActionResult> OnPostExport()
        {
            var members = _memberService.GetAll();

            var columns = new List<string> { "Id", "MemberId", "Name", "Gender", "Birthday", "DateBaptized", "ChurchOfOrigin", "Address", "Email", "Phone", "MembershipStatus", "AttendanceDate", "DateRegistered" };

            var file =  await _exportService.ExportMembers(members, columns);
            return File(file, "text/csv", $"members.csv");
        }
        public bool CheckAttendance(Member member)
        {
            if (member.AttendanceDate == dateToday)
                return true;

            return false;
        }
    }
}

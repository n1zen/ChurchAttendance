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
        private readonly MemberService _memberService;

        [BindProperty(SupportsGet = true)]
        public DateOnly CurrentDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        public AttendanceListModel(AttendanceService attendanceService, MemberService memberService)
        {
            _attendanceService = attendanceService;
            _memberService = memberService;
        }

        public List<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();

        public List<Member> Members { get; set; } = new List<Member>();


        public void OnGet()
        {
            AttendanceRecords = _attendanceService.GetByDate(CurrentDate);
            Members = _memberService.GetMembersByDate(CurrentDate);
        }
    }
}

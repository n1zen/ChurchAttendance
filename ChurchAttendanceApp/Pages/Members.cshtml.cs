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
        public List<Member> Members { get; set; } = [];

        public DateOnly dateToday = DateOnly.FromDateTime(DateTime.Today);

        public MembersModel(MemberService memberService)
        {
            _memberService = memberService;
        }
        public void OnGet()
        {
            Members = _memberService.GetAll();
        }

        public bool CheckAttendance(Member member)
        {
            if (member.AttendanceDate == dateToday)
                return true;

            return false;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChurchAttendanceApp.Services;
using ChurchAttendanceApp.Models;

namespace ChurchAttendanceApp.Pages
{
    public class MembersModel : PageModel
    {
        private readonly MemberService _memberService;

        public List<Member> Members { get; set; } = [];

        public MembersModel(MemberService memberService)
        {
            _memberService = memberService;
        }
        public void OnGet()
        {
            Members = _memberService.GetAll();
        }
    }
}

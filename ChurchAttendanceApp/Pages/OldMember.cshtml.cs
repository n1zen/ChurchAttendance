using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChurchAttendanceApp.Models;
using ChurchAttendanceApp.Services;

namespace ChurchAttendanceApp.Pages
{
    public class OldMemberModel : PageModel
    {
        private readonly MemberService _memberService;
        private readonly AttendanceService _attendanceService;
        
        public OldMemberModel(MemberService memberService, AttendanceService attendanceService)
        {
            _memberService = memberService;
            _attendanceService = attendanceService;
        }

        public List<Member> Members { get; set; } = [];

        [BindProperty]
        public string SelectedMemberId { get; set; } = string.Empty;

        public void OnGet()
        {
            Members = _memberService.GetAll();
        }

        public IActionResult OnPost()
        {
            // refresh members list
            Members = _memberService.GetAll();

            // check if input is valid
            if (!ModelState.IsValid || string.IsNullOrEmpty(SelectedMemberId))
                return Page();

            // find the member
            var member = _memberService.GetByMemberId(SelectedMemberId);

            // check if member exists
            if (member == null)
                return Page();
            

            // check if member already attended
            var today = DateOnly.FromDateTime(DateTime.Today);
            var alreadyAttended = _attendanceService.ExistsForToday(member.Id, today);

            if (alreadyAttended)
            {
                ModelState.AddModelError(string.Empty, "You have already logged your attendance.");
                return Page();
            }

            // add the record
            var attendanceRecord = new AttendanceRecord
            {
                MemberId = member.Id
            };
            
            _attendanceService.Add(attendanceRecord);
            return RedirectToPage("/Index");
        }
    }
}

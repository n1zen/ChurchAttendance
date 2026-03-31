using ChurchAttendanceApp.Services;
using ChurchAttendanceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace ChurchAttendanceApp.Pages
{
    [Authorize(Roles = "Admin")]
    public class MemberDetailModel : PageModel
    {
        private readonly MemberService _memberService;
        private readonly AttendanceService _attendanceService;

        [BindProperty]
        public Member? Member { get; set; }

        [BindProperty]
        public List<AttendanceRecord> AttendanceRecords { get; set; } = [];

        public MemberDetailModel(MemberService memberService, AttendanceService attendanceService)
        {
            _memberService = memberService;
            _attendanceService = attendanceService;
        }

        public IActionResult OnGet(int id)
        {
            Member = _memberService.Get(id);

            if (Member == null)
                return NotFound();

            AttendanceRecords = _attendanceService.GetByMember(Member.Id);

            Console.WriteLine(Member);

            return Page();
        }

        public IActionResult OnPost()
        {

            ModelState.Remove("Member.DateRegistered");
            ModelState.Remove("Member.AttendanceDate");
            ModelState.Remove("Member.AttendanceRecords");

            if (!ModelState.IsValid)
            {
                if (Member != null)
                    Member = _memberService.Get(Member.Id) ?? Member;
                return Page();
            }

            if (Member == null)
                return NotFound();

            try
            {
                var db = _memberService.Get(Member.Id);
                if (db == null) return NotFound();

                db.Name = Member.Name;
                db.MemberId = Member.MemberId;
                db.Gender = Member.Gender;
                db.MembershipStatus = Member.MembershipStatus;
                db.Birthday = Member.Birthday;
                db.DateBaptized = Member.DateBaptized;
                db.Address = Member.Address;
                db.Email = Member.Email;
                db.Phone = Member.Phone;
                // ... copy only editable props, do NOT overwrite MemberId unless present
                _memberService.Update(db);
                TempData["SuccessMessage"] = $"{Member.Name} has been updated successfully.";
                return RedirectToPage("/Members");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("Member.MemberId", ex.Message);
                // reload backing data if needed
                Member = _memberService.Get(Member.Id) ?? Member;
                return Page();
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            var member = _memberService.Get(id);
            if (member == null) return NotFound();

            _memberService.Delete(id);
            TempData["SuccessMessage"] = $"{member.Name} has been deleted successfully.";
            return RedirectToPage("/Members");
        }
        public bool CheckAttendance(Member member)
        {
            if (member.AttendanceDate == DateOnly.FromDateTime(DateTime.Today)) 
                return true;

            return false;
        }
    }
}

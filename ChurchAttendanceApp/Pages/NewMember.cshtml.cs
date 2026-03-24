using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChurchAttendanceApp.Services;
using ChurchAttendanceApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ChurchAttendanceApp.Pages
{
    public class NewMemberModel : PageModel
    {
        private readonly MemberService _memberService;

        [BindProperty]
        public NewMemberInputModel Input { get; set; } = new();

        [BindProperty]
        public DateOnly DateNow { get; set; }= DateOnly.FromDateTime(DateTime.Now);

        public NewMemberModel(MemberService memberService)
        {
            _memberService = memberService;
        }
        public void OnGet() { }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
                return Page();

            var suffix = new Random().Next(1000, 9999);
            
            var member = new Member
            {
                MemberId = "SDA-" + Input.Name.Trim().Replace(" ", "") + "-" + suffix,
                Name = Input.Name,
                Gender = Input.Gender,
                Birthday = Input.Birthday,
                DateBaptized = Input.DateBaptized,
                Address = Input.Address,
                Email = Input.Email,
                Phone = Input.Phone,
                MembershipStatus = "Visitor"
            };
            
            _memberService.Add(member);
            TempData["SuccessMessage"] = $"{member.Name} has been registered successfully.";

            return RedirectToPage("/Index");
        }
    }
}

public class NewMemberInputModel
{
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Gender { get; set; } = string.Empty;

    [Required]
    public DateOnly Birthday { get; set; }

    public DateOnly? DateBaptized { get; set; }

    public string? Address { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [RegularExpression(@"^09\d{9}$", ErrorMessage = "Enter a valid PH number (09xxxxxxxxx).")]
    public string? Phone { get; set; }
}
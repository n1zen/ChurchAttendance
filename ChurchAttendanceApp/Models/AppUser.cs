using Microsoft.AspNetCore.Identity;

namespace ChurchAttendanceApp.Models
{
    public class AppUser : IdentityUser
    {
        public int? MemberId { get; set; }
        public Member? Member { get; set; }
    }
}

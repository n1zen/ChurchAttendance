namespace ChurchAttendanceApp.Models;

public class Member
{
    public int Id { get; set; } // Unique primary ID
    public required string MemberId { get; set; } // Custom ID "SDA-Rhoy"
    public required string Name { get; set; }    
    public required string Gender { get; set; }
    public DateOnly? Birthday { get; set; }
    public DateOnly? DateBaptized { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public required string MembershipStatus { get; set; }
    public DateOnly AttendanceDate { get; set; }
    public DateOnly DateRegistered { get; set; }


    public List<AttendanceRecord> AttendanceRecords { get; set; } = new();
}
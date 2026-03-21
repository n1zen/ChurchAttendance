using System.Text.Json.Serialization;

namespace ChurchAttendanceApp.Models;

public class AttendanceRecord
{
    public int Id { get; set; }
    public int MemberId { get; set; }

    [JsonIgnore]
    public Member? Member { get; set; }

    public DateOnly AttendanceDate { get; set; }
}
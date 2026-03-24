using ChurchAttendanceApp.Models;
using ChurchAttendanceApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ChurchAttendanceApp.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AttendanceController : ControllerBase
{
    private readonly MemberService _memberService;
    private readonly AttendanceService _attendanceService;
    public AttendanceController(MemberService memberService, AttendanceService attendanceService)
    {
        _memberService = memberService;
        _attendanceService = attendanceService;
    }

    // Get all attendance records
    [HttpGet]
    public ActionResult<List<AttendanceRecord>> GetAll() =>
        _attendanceService.GetAll();

    // Get attendance by id
    [HttpGet("{id}")]
    public ActionResult<AttendanceRecord> Get(int id)
    {
        var attendanceRecord = _attendanceService.Get(id);

        if (attendanceRecord == null)
            return NotFound();

        return attendanceRecord;
    }

    // Get all attendance records by member
    [HttpGet("member/{memberId}")]
    public ActionResult<List<AttendanceRecord>> GetByMember(int memberId)
    {
        var member = _memberService.Get(memberId);

        if (member == null)
            return NotFound();

        return _attendanceService.GetByMember(memberId);
    }

    [Authorize(Roles = "Admin")]
    // Create a new attendance record
    [HttpPost]
    public IActionResult Create(AttendanceRecord attendanceRecord)
    {
        var member = _memberService.Get(attendanceRecord.MemberId);

        if (member == null)
            return NotFound($"Member with id {attendanceRecord.MemberId} not found.");
        
        if (member.AttendanceDate == attendanceRecord.AttendanceDate)
            return Conflict($"{member.Name} is already attending.");

        _attendanceService.Add(attendanceRecord);
        return CreatedAtAction(nameof(Get), new { id = attendanceRecord.Id }, attendanceRecord);
    }

    [Authorize(Roles = "Admin")]
    // Update an attendance record
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] AttendanceRecord attendanceRecord)
    {
        if (id != attendanceRecord.Id)
            return BadRequest();

        var existing = _attendanceService.Get(id);
        if (existing == null)
            return NotFound();

        var member = _memberService.Get(attendanceRecord.MemberId);
        if (member == null)
            return NotFound($"Member with id {attendanceRecord.MemberId} not found.");
        
        if (member.AttendanceDate == attendanceRecord.AttendanceDate)
            return Conflict($"{member.Name} is already attending.");

        _attendanceService.Update(attendanceRecord);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    // Delete an attendance record
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var attendanceRecord = _attendanceService.Get(id);

        if (attendanceRecord == null)
            return NotFound();

        _attendanceService.Delete(id);

        return NoContent();
    }
}
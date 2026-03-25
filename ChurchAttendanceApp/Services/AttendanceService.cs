using ChurchAttendanceApp.Models;
using ChurchAttendanceApp.Data;
using Microsoft.EntityFrameworkCore;

public class AttendanceService
{
    private readonly ChurchDbContext _context;

    public AttendanceService(ChurchDbContext context)
    {
        _context = context;
    }

    public List<AttendanceRecord> GetAll() =>
        _context.AttendanceRecords.ToList();
    
    public AttendanceRecord? Get(int id) =>
        _context.AttendanceRecords.Find(id);

    public List<AttendanceRecord> GetByMember(int memberId) =>
        _context.AttendanceRecords
            .OrderByDescending(a => a.AttendanceDate)
            .Where(a => a.MemberId == memberId)
            .ToList();

    public List<AttendanceRecord> GetByDate(DateOnly date) =>
        _context.AttendanceRecords
            .Include(a => a.Member)
            .OrderByDescending(a => a.Id)
            .Where(a => a.AttendanceDate == date)
            .ToList();

    public void Add(AttendanceRecord attendanceRecord)
    {
        attendanceRecord.AttendanceDate = DateOnly.FromDateTime(DateTime.Today);
        _context.AttendanceRecords.Add(attendanceRecord);

        var member = _context.Members.Find(attendanceRecord.MemberId);

        if (member != null)
            member.AttendanceDate = DateOnly.FromDateTime(DateTime.Today);

        _context.SaveChanges();
    }

    public void Update(AttendanceRecord attendanceRecord)
    {
        _context.AttendanceRecords.Update(attendanceRecord);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var attendanceRecord = _context.AttendanceRecords.Find(id);
        if (attendanceRecord != null)
        {
            _context.AttendanceRecords.Remove(attendanceRecord);
            _context.SaveChanges();
        }
    }


    // for checking if member is already attending
    public bool ExistsForToday(int memberId, DateOnly date)
    {
        return _context.AttendanceRecords.Any(a => a.MemberId == memberId && a.AttendanceDate == date);
    }
}
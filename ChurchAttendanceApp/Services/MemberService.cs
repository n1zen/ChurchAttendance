using ChurchAttendanceApp.Models;
using ChurchAttendanceApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ChurchAttendanceApp.Services;

public class MemberService
{
    private readonly ChurchDbContext _context;

    public MemberService(ChurchDbContext context)
    {
        _context = context;
    }

    public List<Member> GetAll() =>
        _context.Members
            .Include(m => m.AttendanceRecords) 
            .ToList();
    
    public Member? Get(int id) =>
        _context.Members
            .Include(m => m.AttendanceRecords)
            .FirstOrDefault(m => m.Id == id);
    
    public void Add(Member member)
    {
        try
        {
            member.DateRegistered = DateOnly.FromDateTime(DateTime.Today);
            member.AttendanceDate = DateOnly.FromDateTime(DateTime.Today);
            _context.Members.Add(member);
            _context.SaveChanges();

            var attendanceRecord = new AttendanceRecord
            {
                MemberId = member.Id,
                AttendanceDate = DateOnly.FromDateTime(DateTime.Today)
            };
            _context.AttendanceRecords.Add(attendanceRecord);
            _context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("A member already exists with the same Name and/or MemberId");
        }
    }

    public void Update(Member member)
    {
        var existing = _context.Members.Find(member.Id);
        if (existing == null) return;

        try
        {
            existing.MemberId = member.MemberId;
            existing.Name = member.Name;
            existing.Gender = member.Gender;
            existing.Birthday = member.Birthday;
            existing.DateBaptized = member.DateBaptized;
            existing.Address = member.Address;
            existing.Email = member.Email;
            existing.Phone = member.Phone;
            existing.MembershipStatus = member.MembershipStatus;
            existing.AttendanceDate = member.AttendanceDate;
            existing.DateRegistered = member.DateRegistered;

            _context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("A member already exists with the same Name and/or MemberId");
        }

    }

    public void Delete(int id)
    {
        var member = _context.Members.Find(id);
        if (member != null)
        {
            _context.Members.Remove(member);
            _context.SaveChanges();
        }
    }
}
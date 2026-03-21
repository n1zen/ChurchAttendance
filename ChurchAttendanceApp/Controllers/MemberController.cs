using ChurchAttendanceApp.Models;
using ChurchAttendanceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChurchAttendanceApp.Controllers;

[ApiController]
[Route("[controller]")]
public class MemberController : ControllerBase
{
    private readonly MemberService _memberService;
    public MemberController(MemberService memberService)
    {
        _memberService = memberService;
    }

    // Get all members
    [HttpGet]
    public ActionResult<List<Member>> GetAll() =>
        _memberService.GetAll();
    
    // Get member by id
    [HttpGet("{id}")]
    public ActionResult<Member> Get(int id)
    {
        var member = _memberService.Get(id);

        if (member == null)
            return NotFound();
        
        return member;
    }

    // Create a new member
    [HttpPost]
    public IActionResult Create(Member member)
    {
        try
        {
            _memberService.Add(member);
            return CreatedAtAction(nameof(Get), new { id = member.Id }, member);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    // Update a member
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Member member)
    {
        if (id != member.Id)
            return BadRequest();
        
        var existingMember = _memberService.Get(id);
        if (existingMember == null)
            return NotFound();
        
        // _memberService.Update(member);

        // return NoContent();
        try
        {
            _memberService.Update(member);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    // Delete a member
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var member = _memberService.Get(id);

        if (member == null)
            return NotFound();

        _memberService.Delete(id);

        return NoContent();
    }
}

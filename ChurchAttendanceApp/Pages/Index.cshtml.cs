using Microsoft.AspNetCore.Mvc.RazorPages;
using ChurchAttendanceApp.Services;
using ChurchAttendanceApp.Models;

namespace ChurchAttendanceApp.Pages;

public class IndexModel : PageModel
{
    private readonly MemberService _memberService;
    private readonly BibleVerseService _bibleverseService;

    public int MemberCount { get; set; }
    public BibleVerse? DailyVerse { get; set; }

    public IndexModel(MemberService memberService, BibleVerseService bibleVerseService)
    {
        _memberService = memberService;
        _bibleverseService = bibleVerseService;
    }

    public async Task OnGetAsync()
    {
        MemberCount = _memberService.GetAll().Count();
        DailyVerse = await _bibleverseService.GetRandomVerseAsync("NKJV");
    }
}
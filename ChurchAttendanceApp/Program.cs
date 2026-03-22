using ChurchAttendanceApp.Data;
using ChurchAttendanceApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChurchDbContext>(options =>
    options.UseSqlite("Data Source=church.db"));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<BibleVerseService>();
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<AttendanceService>();

builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// localhost:5090/dashboard -> redirect -> localhost:5090/home
app.UseHttpsRedirection();

app.UseStaticFiles();

// localhost:5090/home (home page) -> localhost:5090/members (members page)
app.UseRouting();

// security 
// authentication login and registration
app.UseAuthentication();
// authorization permissions rules
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
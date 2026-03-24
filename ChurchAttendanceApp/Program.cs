using ChurchAttendanceApp.Data;
using ChurchAttendanceApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChurchDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));  

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


// use for Azure App Service
// Apply pending migrations and create the database if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ChurchDbContext>();
    db.Database.Migrate();
}

// disable on Azure and local dev
// uncomment when deploying to master (Render deployment)
// app.Urls.Add("http://0.0.0.0:8080");

app.Run();

using Microsoft.EntityFrameworkCore;
using MedbaseApi;
using MedbaseApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<DataContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultApiConnection"));
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//Questions
var questions = app.MapGroup("/questions");
questions.MapGet("/", async (DataContext context) => await context.Questions.ToListAsync());
questions.MapGet("/{topic}/{number}", async (DataContext context, int topic, int number) => await context.Questions.Where(x => x.TopicId == topic).OrderBy(p => Guid.NewGuid()).Take(number).ToListAsync());
questions.MapGet("/select/{id}", async (DataContext context, int id) => await context.Questions.FindAsync(id));
questions.MapGet("/{topic}", async (DataContext context, int topic) => await context.Questions.Where(x => x.TopicId == topic).OrderBy(p => p.Id).ToListAsync());

//Articles

app.MapGet("/articles", async (DataContext context) => await context.Articles.ToListAsync());
app.MapGet("/articles/{id}", async (DataContext context, int id) => await context.Articles.FindAsync(id));
app.MapGet("/articles/select/{number}", async (DataContext context, int number) => await context.Articles.OrderByDescending(p => p.Id).Take(number).ToListAsync());

//Subs
var subs = app.MapGroup("/subs");

subs.MapPost("/{sub}", async (DataContext context, Subscription sub) =>
{
    context.Subscriptions.Add(sub);
    await context.SaveChangesAsync();
});

subs.MapGet("/{userid}", async (DataContext context, string userid) => await context.Subscriptions.FindAsync(userid));

//Topics & Courses
app.MapGet("/topics", async (DataContext context) => await context.Topics.ToListAsync());
app.MapGet("/topics/{courseref}", async (DataContext context, string courseref) => await context.Topics.Where(x => x.CourseRef == courseref).OrderBy(p => p.Id).ToListAsync());

app.MapGet("/courses",  (DataContext context) => 
{
	try
	{
		return Results.Ok(context.Courses.ToListAsync());
    }
	catch (Exception ex)
	{
		return Results.Ok(new { ErrorMessage = ex.Message });
	}
});

app.Run();

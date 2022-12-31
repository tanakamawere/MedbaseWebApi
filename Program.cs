using Microsoft.EntityFrameworkCore;
using MedbaseApi;
using MedbaseApi.Models;

var builder = WebApplication.CreateBuilder(args);

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
//Get functions
var questions = app.MapGroup("/questions");
questions.MapGet("/", async (DataContext context) => await context.Questions.ToListAsync());
questions.MapGet("/select/{id}", async (DataContext context, int id) => await context.Questions.FindAsync(id));
questions.MapGet("/{topic}", async (DataContext context, int topic) => await context.Questions.Where(x => x.TopicId == topic).OrderBy(p => p.Id).ToListAsync());
questions.MapGet("/{topic}/{numResults}/{page}", async (DataContext context, int topic, double numResults,int page) => 
{
    var pageResults = numResults;
    var pageCount = Math.Ceiling(context.Questions.Where(x => x.TopicId == topic).Count() / pageResults);

    var products = await context.Questions
        .Where(x => x.TopicId == topic)
        .Skip((page - 1) * (int)pageResults)
        .Take((int)pageResults)
        .ToListAsync();

    var response = new QuestionPaged
    { 
        Questions= products,
        CurrentPage = page,
        Pages = (int)pageCount
    };

    return response;
});
 questions.MapPost("/{question}", async (DataContext context, Question question) => 
 {
     if (question == null) return;
     await context.Questions.AddAsync(question);
     await context.SaveChangesAsync();
 });
// questions.MapDelete("/", async (DataContext context, Question question) => 
// {
//     context.Questions.Remove(question);
//     await context.SaveChangesAsync();
// });
// questions.MapPut("/", async (DataContext context, Question question) => 
// {
//     if (question == null) return;
//     Question? dataQuestion = context.Questions.FirstOrDefault(x => x.Id == question.Id);
//     dataQuestion.QuestionMain = question.QuestionMain;
//     dataQuestion.ChildA = question.ChildA;
//     dataQuestion.ChildB = question.ChildB;
//     dataQuestion.ChildC = question.ChildC;
//     dataQuestion.ChildD = question.ChildD;
//     dataQuestion.ChildE = question.ChildE;
//     dataQuestion.AnswerA = question.AnswerA;
//     dataQuestion.AnswerB = question.AnswerB;
//     dataQuestion.AnswerC = question.AnswerC;
//     dataQuestion.AnswerD = question.AnswerD;
//     dataQuestion.AnswerE = question.AnswerE;
//     dataQuestion.TopicId = question.TopicId;

//     await context.Questions.AddAsync(dataQuestion);
//     await context.SaveChangesAsync();
// });



//Articles

app.MapGet("/articles", async (DataContext context) => await context.Articles.ToListAsync());
app.MapGet("/articles/{id}", async (DataContext context, int id) => await context.Articles.FindAsync(id));
app.MapGet("/articles/select/{number}", async (DataContext context, int number) => await context.Articles.OrderByDescending(p => p.Id).Take(number).ToListAsync());
app.MapPost("/articles/{article}", async (DataContext context, Article article) =>
{
    await context.Articles.AddAsync(article);
    await context.SaveChangesAsync();
});

//Subs
var subs = app.MapGroup("/subs");

subs.MapPost("/{sub}", async (DataContext context, Subscription sub) =>
{
    context.Subscriptions.Add(sub);
    await context.SaveChangesAsync();
});

subs.MapGet("/{userid}", async (DataContext context, string userid) => await context.Subscriptions.FindAsync(userid));

//Topics
app.MapGet("/topics", async (DataContext context) => await context.Topics.ToListAsync());
app.MapGet("topics/select/{id}", async (DataContext context, long id) => await context.Topics.FindAsync(id));
app.MapGet("/topics/{courseref}", async (DataContext context, string courseref) => await context.Topics.Where(x => x.CourseRef == courseref).OrderBy(p => p.Id).ToListAsync());
app.MapPost("topics/{topic}", async (DataContext context, Topic topic) => 
{
    await context.Topics.AddAsync(topic);
    await context.SaveChangesAsync();
});

//Courses
app.MapGet("/courses",  async (DataContext context) => await context.Courses.ToListAsync());
app.MapGet("/courses/{id}", async (DataContext context, int id) => await context.Courses.FindAsync(id));
app.MapPost("/courses/{course}", async (DataContext context, Course course) => 
{
    await context.Courses.AddAsync(course);
    await context.SaveChangesAsync();
});
app.MapDelete("/course/{id}", async (DataContext context, int id) => 
{
    Course course = await context.Courses.FindAsync(id);
    foreach (Topic item in context.Topics.ToList())
            {
                if (item.CourseRef.Equals(course.CourseRef))
                {
                    context.Topics.Remove(item);
                    context.Questions.RemoveRange(context.Questions.Where(x => x.TopicId == item.TopicRef).ToList());
                }
            }
            context.Remove(course);
            await context.SaveChangesAsync();
});

app.Run();

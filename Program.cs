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
questions.MapGet("/{topic}", async (DataContext context, int topic) => await context.Questions.Where(x => x.TopicRef == topic).OrderBy(p => p.Id).ToListAsync());
questions.MapGet("/{topic}/{numResults}/{page}", async (DataContext context, int topic, double numResults, int page) =>
{
    var pageResults = numResults;
    var pageCount = Math.Ceiling(context.Questions.Where(x => x.TopicRef == topic).Count() / pageResults);

    var products = await context.Questions
        .Where(x => x.TopicRef == topic)
        .Skip((page - 1) * (int)pageResults)
        .Take((int)pageResults)
        .ToListAsync();

    var response = new QuestionPaged
    {
        Questions = products,
        CurrentPage = page,
        Pages = (int)pageCount
    };

    return response;
});
//Search function for questions
questions.MapGet("/{topic}/{numResults}/{page}/{keyword}", async (DataContext context, int topic, double numResults, int page, string keyword) =>
{
    var pageResults = numResults;
    var pageCount = Math.Ceiling(context.Questions.Where(x => x.TopicRef == topic).Count() / pageResults);

    var products = await context.Questions
        .Where(x => x.TopicRef == topic)
        .Where(x => x.QuestionMain.Contains(keyword))
        .Skip((page - 1) * (int)pageResults)
        .Take((int)pageResults)
        .ToListAsync();

    var response = new QuestionPaged
    {
        Questions = products,
        CurrentPage = page,
        Pages = (int)pageCount
    };

    return response;
});


questions.MapPost("/{question}", async (DataContext context, Question question) => 
 {
     if (question == null) return;
     try
     {
        await context.Questions.AddAsync(question);
        await context.SaveChangesAsync();
     }
     catch(Exception ex)
     {
        Console.WriteLine(ex.InnerException.Message);
     }
 });
questions.MapDelete("/{questionId}", async (DataContext context, int questionId) => 
{
    Question question = await context.Questions.FindAsync(questionId);

    context.Questions.Remove(question);
    await context.SaveChangesAsync();
});
questions.MapPut("/{id}", async (DataContext context, Question inputQuestion, int id) => 
{
    Question question = await context.Questions.FindAsync(id);

    if (question == null) return Results.NotFound();

    question.QuestionMain = inputQuestion.QuestionMain;
    question.ChildA =  inputQuestion.ChildA;
    question.ChildB =  inputQuestion.ChildB;
    question.ChildC =  inputQuestion.ChildC;
    question.ChildD =  inputQuestion.ChildD;
    question.ChildE =  inputQuestion.ChildE;
    question.AnswerA = inputQuestion.AnswerA;
    question.AnswerB = inputQuestion.AnswerB;
    question.AnswerC = inputQuestion.AnswerC;
    question.AnswerD = inputQuestion.AnswerD;
    question.AnswerE = inputQuestion.AnswerE;
    question.ExplanationA = inputQuestion.ExplanationA;
    question.ExplanationB = inputQuestion.ExplanationB;
    question.ExplanationC = inputQuestion.ExplanationC;
    question.ExplanationD = inputQuestion.ExplanationD;
    question.ExplanationE = inputQuestion.ExplanationE;
    question.TopicRef = inputQuestion.TopicRef;

    await context.SaveChangesAsync();

    return Results.NoContent();
});
questions.MapGet("/quiz/{topic}/{number}", (DataContext context, int topic, int number) => 
{
    return context.Questions.Where(d => d.TopicRef == topic)
                    .OrderBy(x => Guid.NewGuid())
                    .Take(number);
});

//Articles
app.MapGet("/articles", async (DataContext context) => await context.Articles.ToListAsync());
app.MapGet("/articles/{id}", async (DataContext context, int id) => await context.Articles.FindAsync(id));
app.MapGet("/articles/select/{number}", async (DataContext context, int number) => await context.Articles.OrderByDescending(p => p.Id).Take(number).ToListAsync());
app.MapPost("/articles/{article}", async (DataContext context, Article article) =>
{
    await context.Articles.AddAsync(article);
    await context.SaveChangesAsync();
});
app.MapPut("/articles/{id}", async (DataContext context, int id, Article inputArticle) => 
{
    Article article = await context.Articles.FindAsync(id);
    if(article is null) return Results.NotFound();

    article.Title = inputArticle.Title;
    article.Body = inputArticle.Body;
    article.Summary = inputArticle.Summary;
    article.Writer = inputArticle.Writer;
    article.DatePosted = inputArticle.DatePosted;
    article.ImageURL = inputArticle.ImageURL;

    await context.SaveChangesAsync();

    return Results.NoContent();
});
app.MapDelete("/articles/{id}", async (DataContext context, int id) => 
{
    context.Articles.Remove(await context.Articles.FindAsync(id));
    await context.SaveChangesAsync();
});
//Subs
var subs = app.MapGroup("/subscriptions");
subs.MapGet("/", async (DataContext context) => await context.Subscriptions.ToListAsync());
subs.MapPost("/{sub}", async (DataContext context, Subscription sub) =>
{
    context.Subscriptions.Add(sub);
    await context.SaveChangesAsync();
});
subs.MapGet("/{email}", async (DataContext context, string email) =>
{
    Subscription subscription = new();
    try
    {
       subscription = await context.Subscriptions.Where(x => x.Email == email).FirstAsync();
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
        
    return subscription;
});
subs.MapPut("/{id}", async (DataContext context, int id, Subscription inputSubscription) => 
{
    Subscription subscription = await context.Subscriptions.FindAsync(id);
    if(subscription is null) return Results.NotFound();

    subscription.Phone = inputSubscription.Phone;
    subscription.Email = inputSubscription.Email;
    subscription.StartDate = inputSubscription.StartDate;
    subscription.EndDate = inputSubscription.EndDate;
    subscription.Amount = inputSubscription.Amount;

    await context.SaveChangesAsync();

    return Results.NoContent();
});

//Topics
app.MapGet("/topics", async (DataContext context) => await context.Topics.ToListAsync());
app.MapGet("topics/select/{id}", async (DataContext context, long id) => 
{
    Topic topic = await context.Topics.Where(x => x.TopicRef == id).FirstAsync();
    return topic;
});
app.MapGet("/topics/{courseref}", async (DataContext context, string courseref) => await context.Topics.Where(x => x.CourseRef == courseref).OrderBy(p => p.Id).ToListAsync());
app.MapPost("/topics/{topic}", async (DataContext context, Topic topic) => 
{
    await context.Topics.AddAsync(topic);
    await context.SaveChangesAsync();
});
app.MapPut("/topics/{id}", async (DataContext context, int id, Topic inputTopic) => 
{
    Topic topic = await context.Topics.FindAsync((long)id);
    if(topic is null) return Results.NotFound();

    topic.CourseRef = inputTopic.CourseRef;
    topic.TopicRef = inputTopic.TopicRef;
    topic.Name = inputTopic.Name;

    await context.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/topics/{id}", async (DataContext context, int id) => 
{
    context.Topics.Remove(await context.Topics.FindAsync((long)id));
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
                    context.Questions.RemoveRange(context.Questions.Where(x => x.TopicRef == item.TopicRef).ToList());
                }
            }
            context.Remove(course);
            await context.SaveChangesAsync();
});
app.MapPut("/courses/{id}", async (DataContext context, int id, Course inputCourse) =>
{
    Course course = await context.Courses.FindAsync(id);
    if(course == null) return Results.NotFound();

    course.CourseRef = inputCourse.CourseRef;
    course.Description = inputCourse.Description;
    course.ImageURL = inputCourse.ImageURL;
    course.Title = inputCourse.Title;

    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();

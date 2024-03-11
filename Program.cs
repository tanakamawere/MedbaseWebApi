using Microsoft.EntityFrameworkCore;
using MedbaseApi;
using MedbaseLibrary.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.OpenApi.Models;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

//Swagger Config for Putting JWT Token in requests
var securityScheme = new OpenApiSecurityScheme()
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JSON Web Token based security",
};

var securityReq = new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        { 
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        }, Array.Empty<string>()
    }
};
var contact = new OpenApiContact()
{
    Name = "FirstName LastName",
    Email = "user@example.com",
    Url = new Uri("http://www.example.com")
};

var license = new OpenApiLicense()
{
    Name = "My License",
    Url = new Uri("http://www.example.com")
};
var info = new OpenApiInfo()
{
    Version = "v1",
    Title = "Medbase Minimal API",
    Description = "Minimal API for Medbase",
    TermsOfService = new Uri("http://www.example.com"),
    Contact = contact,
    License = license
};



builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<DataContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultApiConnection"));
});
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultApiConnection"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(o => 
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", info);
    o.AddSecurityDefinition("Bearer", securityScheme);
    o.AddSecurityRequirement(securityReq);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//Questions 
//Get functions
var questions = app.MapGroup("/questions");
questions.MapGet("/", [Authorize] async (DataContext context) => await context.Questions.ToListAsync());

questions.MapGet("/select/{id}", async Task<Results<Ok<Question>, NotFound>> (DataContext context, int id) =>
    await context.Questions.FindAsync(id)
    is Question question
       ? TypedResults.Ok(question)
       : TypedResults.NotFound());

questions.MapGet("/{topic}", async (DataContext context, int topic) => 
    await context.Questions.Where(x => x.TopicRef == topic).OrderBy(p => p.Id).ToListAsync());

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


questions.MapPost("/", async Task<Results<Ok<string>, NotFound>> (DataContext context, Question question) => 
{
    if (question == null) return TypedResults.Ok("Something wrong happened");

     try
     {
        await context.Questions.AddAsync(question);
        await context.SaveChangesAsync();
        return TypedResults.Ok($"{question.Id} was successful");
     }
     catch(Exception ex)
     {
        Console.WriteLine(ex.InnerException.Message);
        return TypedResults.Ok($"{ex.InnerException.Message}");
     }

 });
questions.MapDelete("/{questionId}", async (DataContext context, int questionId) => 
{
    Question question = await context.Questions.FindAsync(questionId);

    context.Questions.Remove(question);
    await context.SaveChangesAsync();
});
questions.MapPut("/{id}", async Task<Results<Ok<string>, BadRequest<string>>> (DataContext context, Question inputQuestion, int id) => 
{
    try
    {
        Question question = await context.Questions.FindAsync(id);

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

        return TypedResults.Ok($"Question edited successfully");
    }
    catch (System.Exception ex)
    {
        return TypedResults.BadRequest($"Error message: {ex}");
    }
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
app.MapPost("/articles/", async Task<Results<Ok, BadRequest>> (DataContext context, Article article) =>
{
    await context.Articles.AddAsync(article);
    
        if (await context.SaveChangesAsync() > 0)
        {
            return TypedResults.Ok();
        }
        else
        {
            return TypedResults.BadRequest();
        }
});
app.MapPut("/articles/{id}", async Task<Results<Ok, BadRequest>> (DataContext context, int id, Article inputArticle) => 
{
    Article article = await context.Articles.FindAsync(id);
    if(article is null) return TypedResults.BadRequest();

    article.Title = inputArticle.Title;
    article.Body = inputArticle.Body;
    article.Summary = inputArticle.Summary;
    article.Writer = inputArticle.Writer;
    article.DatePosted = inputArticle.DatePosted;
    article.ImageURL = inputArticle.ImageURL;
        if (await context.SaveChangesAsync() > 0)
        {
            return TypedResults.Ok();
        }
        else
        {
            return TypedResults.BadRequest();
        }
});
app.MapDelete("/articles/{id}", async Task<Results<Ok, BadRequest>> (DataContext context, int id) => 
{
    context.Articles.Remove(await context.Articles.FindAsync(id));
    if (await context.SaveChangesAsync() > 0)
        {
            return TypedResults.Ok();
        }
        else
        {
            return TypedResults.BadRequest();
        }
});

//Topics
app.MapGet("/topics", async (DataContext context) => await context.Topics.ToListAsync());
app.MapGet("topics/select/{id}", async (DataContext context, long id) => 
{
    Topic topic = await context.Topics.Where(x => x.TopicRef == id).FirstAsync();
    return topic;
});
app.MapGet("/topics/{courseref}", async (DataContext context, string courseref) => await context.Topics.Where(x => x.CourseRef == courseref).OrderBy(p => p.Id).ToListAsync());
app.MapPost("/topics/", async Task<Results<Ok<string>, BadRequest<string>>> (DataContext context, Topic topic) => 
{
    try
    {
        await context.Topics.AddAsync(topic);

        if (await context.SaveChangesAsync() > 0)
        {
            return TypedResults.Ok("Article edited successfully");
        }
        else
        {
            return TypedResults.BadRequest("Something went wrong");
        }
    }
    catch (System.Exception ex)
    {
        return TypedResults.BadRequest($"Error message: {ex}");
    }
});
app.MapPut("/topics/{id}", async Task<Results<Ok, BadRequest>>  (DataContext context, int id, Topic inputTopic) => 
{
    try
    {
        Topic topic = await context.Topics.FindAsync((long)id);

        topic.CourseRef = inputTopic.CourseRef;
        topic.TopicRef = inputTopic.TopicRef;
        topic.Name = inputTopic.Name;

        
        if (await context.SaveChangesAsync() > 0)
        {
            return TypedResults.Ok();
        }
        else
        {
            return TypedResults.BadRequest();
        }
    }
    catch (System.Exception ex)
    {
        return TypedResults.BadRequest();
    }
});
app.MapDelete("/topics/{id}", async (DataContext context, int id) => 
{
    context.Topics.Remove(await context.Topics.FindAsync((long)id));
    await context.SaveChangesAsync();
});

//Courses
app.MapGet("/courses",  async (DataContext context) => await context.Courses.ToListAsync());
app.MapGet("/courses/{id}", async (DataContext context, int id) => await context.Courses.FindAsync(id));
app.MapPost("/courses/", async Task<Results<Ok<string>, BadRequest<string>>>  (DataContext context, Course course) => 
{    
    try
    {
        await context.Courses.AddAsync(course);

        if (await context.SaveChangesAsync() > 0)
        {
            return TypedResults.Ok("Article edited successfully");
        }
        else
        {
            return TypedResults.BadRequest("Something went wrong");
        }
    }
    catch (System.Exception ex)
    {
        return TypedResults.BadRequest($"Error message: {ex}");
    }
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
app.MapPut("/courses/{id}", async Task<Results<Ok<string>, BadRequest<string>>>  (DataContext context, int id, Course inputCourse) =>
{
    try
    {
        Course course = await context.Courses.FindAsync(id);

        course.CourseRef = inputCourse.CourseRef;
        course.Description = inputCourse.Description;
        course.ImageURL = inputCourse.ImageURL;
        course.Title = inputCourse.Title;

        if (await context.SaveChangesAsync() > 0)
        {
            return TypedResults.Ok("Course edited successfully");
        }
        else
        {
            return TypedResults.BadRequest("Something went wrong");
        }
    }
    catch (System.Exception ex)
    {
        return TypedResults.BadRequest($"Error message: {ex}");
    }
});

//Gets articles and courses for Maui App 
app.MapGet("/dashboard/getall", async (DataContext context) =>
{
    CourseArticlesDto courseArticlesDto = new();
    courseArticlesDto.Articles = await context.Articles.OrderByDescending(p => p.Id).Take(3).ToListAsync();
    courseArticlesDto.Courses = await context.Courses.ToListAsync();

    return courseArticlesDto;
});

//Answer Corrections Api Calls
app.MapPost("/corrections/", async Task<Results<Ok, BadRequest<string>>> (DataContext context, Corrections correction) =>
{
    try
    {
        await context.Corrections.AddAsync(correction);
        
        if (await context.SaveChangesAsync() > 0)
        {
            return TypedResults.Ok();
        }
        else
        {
            return TypedResults.BadRequest("Something went wrong");
        }
    }
    catch (System.Exception ex)
    {
        return TypedResults.BadRequest($"Error message: {ex}");
    }
});

app.MapGet("/corrections/", async (DataContext context) => await context.Corrections.ToListAsync());
app.MapDelete("/corrections/deleteone/{id}", async Task<Results<Ok, BadRequest>> (DataContext context, int id) => 
{
    Corrections corr = await context.Corrections.FindAsync(id);
    context.Corrections.Remove(corr);   
    
        if (await context.SaveChangesAsync() > 0)
        {
            return TypedResults.Ok();
        }
        else
        {
            return TypedResults.BadRequest();
        }
});
app.MapDelete("/corrections/clearall/", async Task<Results<Ok, BadRequest>> (DataContext context) => 
{
    context.Corrections.RemoveRange(context.Corrections);
    
        if (await context.SaveChangesAsync() > 0)
        {
            return TypedResults.Ok();
        }
        else
        {
            return TypedResults.BadRequest();
        }
});

app.MapPost("/corrections/mergeone/{id}", async Task<Results<Ok, BadRequest>> (DataContext context, int id) =>
{               
    Corrections item = await context.Corrections.FindAsync(id);
    if (item is not null)
    {
        Question question = new();
        switch (item.QuestionChild.ToUpper())
        {
            case "A":
                question = new()
                {
                    Id = item.QuestionId,
                    AnswerA = item.SuggestedAnswer,
                    ExplanationA = item.SuggestedExplanation
                };
        
                context.Questions.Attach(question);
                context.Entry(question).Property(x => x.AnswerA).IsModified = true;
                
                if (!string.IsNullOrEmpty(item.SuggestedExplanation))
                    context.Entry(question).Property(x => x.ExplanationA).IsModified = true;
                break;
            case "B":
                question = new()
                {
                    Id = item.QuestionId,
                    AnswerB = item.SuggestedAnswer,
                    ExplanationB = item.SuggestedExplanation
                };
                context.Questions.Attach(question);
                context.Entry(question).Property(x => x.AnswerB).IsModified = true;
                
                if (!string.IsNullOrEmpty(item.SuggestedExplanation))
                    context.Entry(question).Property(x => x.ExplanationB).IsModified = true;
                break;
            case "C":
                question = new()
                {
                    Id = item.QuestionId,
                    AnswerC = item.SuggestedAnswer,
                    ExplanationC = item.SuggestedExplanation
                };
                context.Questions.Attach(question);
                context.Entry(question).Property(x => x.AnswerC).IsModified = true;
                
                if (!string.IsNullOrEmpty(item.SuggestedExplanation))
                    context.Entry(question).Property(x => x.ExplanationC).IsModified = true;
                break;
            case "D":
                question = new()
                {
                    Id = item.QuestionId,
                    AnswerD = item.SuggestedAnswer,
                    ExplanationD = item.SuggestedExplanation
                };
                context.Questions.Attach(question);
                context.Entry(question).Property(x => x.AnswerD).IsModified = true;
                
                if (!string.IsNullOrEmpty(item.SuggestedExplanation))
                    context.Entry(question).Property(x => x.ExplanationD).IsModified = true;
                break;
            case "E":
                question = new()
                {
                    Id = item.QuestionId,
                    AnswerE = item.SuggestedAnswer,
                    ExplanationE = item.SuggestedExplanation
                };
                context.Questions.Attach(question);
                context.Entry(question).Property(x => x.AnswerE).IsModified = true;
                
                if (!string.IsNullOrEmpty(item.SuggestedExplanation))
                    context.Entry(question).Property(x => x.ExplanationE).IsModified = true;
                break;
            default:
                break;
        }
        await context.SaveChangesAsync();

        context.Corrections.Remove(item);
        if (await context.SaveChangesAsync() > 0)
        {
            return TypedResults.Ok();
        }
        else
        {
            return TypedResults.BadRequest();
        }
    }   
    else 
    {
        return TypedResults.BadRequest();
    }             
});

app.MapPost("/corrections/mergeall", async Task<Results<Ok, BadRequest>> (DataContext context) => 
{
    List<Corrections> listOfCurrentCorrections = await context.Corrections.ToListAsync();
    if (listOfCurrentCorrections.Count != 0)
    {
        try
        {
            foreach (Corrections item in listOfCurrentCorrections)
            {
                Question question = new();
                switch (item.QuestionChild.ToUpper())
                {
                    case "A":
                        question = new()
                        {
                            Id = item.QuestionId,
                            AnswerA = item.SuggestedAnswer,
                            ExplanationA = item.SuggestedExplanation
                        };
                
                        context.Questions.Attach(question);
                        context.Entry(question).Property(x => x.AnswerA).IsModified = true;
                        
                        if (!string.IsNullOrEmpty(item.SuggestedExplanation))
                            context.Entry(question).Property(x => x.ExplanationA).IsModified = true;
                        break;
                    case "B":
                        question = new()
                        {
                            Id = item.QuestionId,
                            AnswerB = item.SuggestedAnswer,
                            ExplanationB = item.SuggestedExplanation
                        };
                        context.Questions.Attach(question);
                        context.Entry(question).Property(x => x.AnswerB).IsModified = true;
                        
                        if (!string.IsNullOrEmpty(item.SuggestedExplanation))
                            context.Entry(question).Property(x => x.ExplanationB).IsModified = true;
                        break;
                    case "C":
                        question = new()
                        {
                            Id = item.QuestionId,
                            AnswerC = item.SuggestedAnswer,
                            ExplanationC = item.SuggestedExplanation
                        };
                        context.Questions.Attach(question);
                        context.Entry(question).Property(x => x.AnswerC).IsModified = true;
                        
                        if (!string.IsNullOrEmpty(item.SuggestedExplanation))
                            context.Entry(question).Property(x => x.ExplanationC).IsModified = true;
                        break;
                    case "D":
                        question = new()
                        {
                            Id = item.QuestionId,
                            AnswerD = item.SuggestedAnswer,
                            ExplanationD = item.SuggestedExplanation
                        };
                        context.Questions.Attach(question);
                        context.Entry(question).Property(x => x.AnswerD).IsModified = true;
                        
                        if (!string.IsNullOrEmpty(item.SuggestedExplanation))
                            context.Entry(question).Property(x => x.ExplanationD).IsModified = true;
                        break;
                    case "E":
                        question = new()
                        {
                            Id = item.QuestionId,
                            AnswerE = item.SuggestedAnswer,
                            ExplanationE = item.SuggestedExplanation
                        };
                        context.Questions.Attach(question);
                        context.Entry(question).Property(x => x.AnswerE).IsModified = true;
                        
                        if (!string.IsNullOrEmpty(item.SuggestedExplanation))
                            context.Entry(question).Property(x => x.ExplanationE).IsModified = true;
                        break;
                    default:
                        break;
                }
                await context.SaveChangesAsync();

                context.Corrections.Remove(item);
                await context.SaveChangesAsync();
            }
            return TypedResults.Ok();
        }
        catch (System.Exception)
        {
            return TypedResults.Ok();
        }
    }
    else
    {
        return TypedResults.Ok();
    }
});

//Notes calls
//Notes call to get all notes in a list
app.MapGet("/notes/getall", async (DataContext context) =>
{ 
    List<Note> notes = await context.Notes.ToListAsync();
    List<NoteDto> noteDtos = new();

    foreach (var item in notes)
    {
        Topic topic = await context.Topics.Where(x => x.TopicRef == item.TopicReference).FirstOrDefaultAsync();
        NoteDto noteDto = new()
        {
            Id = item.Id,
            TopicReferenceName = topic.Name,
            DateUpdated = item.DateUpdated,
            Text = item.Text
        };
        noteDtos.Add(noteDto);
    }
    return noteDtos;
});

app.MapGet("/notes/get/{topicReference}", async(DataContext context, int topicReference) =>
{
    try
    {
        Note note = await context.Notes.Where(x => x.TopicReference == topicReference).FirstOrDefaultAsync();
        NoteDto noteDto = new()
        {
            Id = note.Id,
            TopicReferenceName = (await context.Topics.Where(x => x.TopicRef == topicReference).FirstOrDefaultAsync()).Name,
            DateUpdated = note.DateUpdated,
            Text = note.Text
        };
        return noteDto;
    }
    catch (System.Exception)
    {
        return null;
    }
});

app.MapGet("/notes/get_with_reference/{topicReference}", async(DataContext context, int topicReference) =>
{
    try
    {
        return await context.Notes.Where(x => x.TopicReference == topicReference).FirstOrDefaultAsync();
    }
    catch (System.Exception)
    {
        return null;
    }
});

app.MapPost("/notes/post/", async Task<Results<Ok, BadRequest>>(DataContext context, Note note) =>
{
    try
    {
        await context.Notes.AddAsync(note);
        await context.SaveChangesAsync();
        return TypedResults.Ok();
    }
    catch (System.Exception)
    {
        return TypedResults.BadRequest();
    }
});
app.MapPut("/notes/put/", async Task<Results<Ok, BadRequest>> (DataContext context, Note note) =>
{
    try
    {
        Note noteToEdit = await context.Notes.FindAsync(note.Id);
        noteToEdit.DateUpdated = note.DateUpdated;
        noteToEdit.Text = note.Text;
        await context.SaveChangesAsync();
        return TypedResults.Ok();
    }
    catch (System.Exception)
    {
        return TypedResults.BadRequest();
    }
});
app.MapDelete("/notes/delete/{id}", async Task<Results<Ok, BadRequest>> (DataContext context, int id) =>
{
    try
    {
        Note noteToDelete = await context.Notes.FindAsync(id);
        context.Notes.Remove(noteToDelete);
        await context.SaveChangesAsync();
        return TypedResults.Ok();
    }
    catch (System.Exception)
    {
        return TypedResults.BadRequest();
    }
});

//Course Topics calls
app.MapGet("/notes/coursetopics/get/{courseReference}", async (DataContext context, string courseReference) =>
{
    CourseTopicsDto courseTopicsDto = new()
    {
        Course = courseReference,
        Topics = await context.Topics.Where(x => x.CourseRef == courseReference).ToListAsync()
    };
    return courseTopicsDto;
});
//call to get all courses and their topics
app.MapGet("/notes/coursetopics/getall", async (DataContext context) =>
{
    List<CourseTopicsDto> courseTopicsDtos = new();
    List<Course> courses = await context.Courses.ToListAsync();
    foreach (Course course in courses)
    {
        CourseTopicsDto courseTopicsDto = new();
        courseTopicsDto.Course = course.Title;
        courseTopicsDto.CourseImage = course.ImageURL;
        courseTopicsDto.Topics = await context.Topics.Where(x => x.CourseRef == course.CourseRef).ToListAsync();
        courseTopicsDtos.Add(courseTopicsDto);
    }
    return courseTopicsDtos;
});

//AUTH
app.MapPost("/security/getToken",
[AllowAnonymous] (User user) =>
{

    if (user.UserName == "user1" && user.Password == "password1")
    {
        var issuer = builder.Configuration["Jwt:Issuer"];
        var audience = builder.Configuration["Jwt:Audience"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(issuer: issuer,audience: audience,signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        var stringToken = tokenHandler.WriteToken(token);

        return Results.Ok(stringToken);
    }
    else
    {
        return Results.Unauthorized();
    }
});


app.Run();

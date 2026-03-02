using CyberQuiz.BLL.Services;

var builder = WebApplication.CreateBuilder(args);


//Lägg till cors policy.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUI", policy =>
    {
        policy.WithOrigins("https://localhost:7047")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});



// Add services to the container.
builder.Services.AddControllers();
// När någon frågar efter IQuizService får de en QuizService tillbaka.
builder.Services.AddScoped<IQuizService, QuizService>();
// När någon frågar efter IUserResultService får de en UserResultService tillbaka.
builder.Services.AddScoped<IUserResultService, UserResultService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowUI");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

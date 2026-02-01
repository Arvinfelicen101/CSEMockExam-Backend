using Backend.Context;
using Backend.Middlewares;
using Backend.Models;
using Backend.Repository.Auth;
using Backend.Repository.Importer;
using Backend.Repository.Question;
using Backend.Repository.SubCategory;
using Backend.Repository.UserManagement;
using Backend.Services.Authentication;
using Backend.Services.Importer;
using Backend.Services.Question;
using Backend.Services.Question.QuestionValidator;
using Backend.Services.SubCategory;
using Backend.Services.UserManagement;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using Backend.Repository.ChoicesManagement;
using Backend.Repository.ParagraphManagement;
using Backend.Repository.YearPeriodManagement;
using Backend.Services.ChoicesManagement;
using Backend.Services.ParagraphManagement;
using Backend.Services.YearPeriodManagement;
using Backend.Repository.ExamRepository;
using Backend.Services.ExamService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();

//DI Repositories
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUserManagementRepository, UserManagementRepository>();
builder.Services.AddScoped<IImporterRepository, ImporterRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IChoicesRepository, ChoicesRepository>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<IYearPeriodRepository, YearPeriodManagement>();
builder.Services.AddScoped<IParagraphManagementRepository, ParagraphManagementRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();

//DI Services
builder.Services.AddScoped<IQuestionValidator, QuestionValidator>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IUserManagementServices, UserManagementServices>();
builder.Services.AddScoped<IImporterService, ImporterService>();
builder.Services.AddScoped<IQuestionService,  QuestionService>();
builder.Services.AddScoped<IChoiceService, ChoiceService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
builder.Services.AddScoped<IYearPeriodService, YearPeriodService>();
builder.Services.AddScoped<IParagraphManagementService, ParagraphManagementService>();
builder.Services.AddScoped<IExamService, ExamService>();

//DI Middleware
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// database config
var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "CSEMockExam";
var user = Environment.GetEnvironmentVariable("DB_USER") ?? "postgres";
var pass = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "postgres";

var connectionString = $"Host={host};Port={port};Database={dbName};Username={user};Password={pass}";

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register Identity
builder.Services.AddIdentity<Users, IdentityRole>()
    .AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders();

var jwtConfig = builder.Configuration.GetSection("JwtConfig");
var key = jwtConfig["Key"] ?? throw new InvalidOperationException("JWT Key is missing");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError("JWT AUTH FAILED: {Message}", context.Exception.Message);
            return Task.CompletedTask;
        }
    };

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig["Issuer"],
        ValidAudience = jwtConfig["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtConfig["Key"]!)),
        ClockSkew = TimeSpan.Zero
    };
});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {

//        options.Events = new JwtBearerEvents
//        {
//            OnAuthenticationFailed = context =>
//            {
//                Console.WriteLine("JWT AUTH FAILED: " + context.Exception.Message);
//                return Task.CompletedTask;
//            }
//        };

//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,

//            ValidIssuer = jwtConfig["Issuer"],
//            ValidAudience = jwtConfig["Audience"],

//            IssuerSigningKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes(jwtConfig["Key"]!)),

//            ClockSkew = TimeSpan.Zero
//        };
//    });


builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.UseExceptionHandler();
}
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapOpenApi();

app.Run();

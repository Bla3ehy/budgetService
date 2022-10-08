using BudgetService;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddSingleton<IBudgetRepo, BudgetRepo>();
builder.Services.AddScoped<BudgetService.BudgetService>();
app.MapGet("/", () => "Hello World!");


app.Run();
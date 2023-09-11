using PandaTech.TestCase1;
using PandaTech.TestCase1.WebApi;

Logger.Initialize();

var builder = WebApplication.CreateBuilder(args);
builder.Services.Initialize();

var app = builder.Build();
app.Initialize();

app.Run();
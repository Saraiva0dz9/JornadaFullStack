using Final.Api.Data;
using Final.Api.Data.Services;
using Final.Core.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Conection to database
builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// SERVICES
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

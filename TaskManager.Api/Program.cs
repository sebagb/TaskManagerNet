using TaskManager.Application;
using TaskManager.Application.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["Database:ConnectionString"];
builder.Services.AddApplication();
builder.Services.AddDatabase(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
dbInitializer.Initialize();

app.Run();
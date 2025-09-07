
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Application;
using TaskManager.Application.Models;
using TaskManager.Console;

var builder = Host.CreateApplicationBuilder(args);

var conf = builder.Configuration["Database:ConnectionString"];
builder.Services.AddApplication();
builder.Services.AddDatabase(conf);

builder.Services.AddTransient<Client>();

using IHost host = builder.Build();

var client = host.Services.GetRequiredService<Client>();
var response = client.Service.CreateProject(new Project
{
    Deadline = 2026,
    Status = "Pending",
    Tasks = "Start, Continue, Finish",
    Title = "My fist project"
});
var created = response ? "Yes" : "No";
Console.WriteLine($"Was it created? {created}");
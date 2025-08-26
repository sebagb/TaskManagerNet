
using Microsoft.Extensions.Configuration;
using TaskManager.Application;
using TaskManager.Application.Models;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

var connection = config["Database:ConnectionString"];
ApplicationServiceCollectionExtension.AddApplication(connection!);
ApplicationServiceCollectionExtension.Initializer!.Initialize();

ApplicationServiceCollectionExtension.Service!.CreateProject(new Project()
{
    Id = 4444,
    Deadline = 2026,
    Status = "Pending",
    Tasks = "Start, Continue, Finish",
    Title = "My fist project"
});
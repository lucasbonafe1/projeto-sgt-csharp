using Microsoft.OpenApi.Models;
using SGT.Application.Interfaces;
using SGT.Application.Services;
using SGT.Domain.Repositories;
using SGT.Infrastructure.Data;
using SGT.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

DbConfig.Initialize(configuration);

//presentation
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SGT.API", Version = "v1" });
    c.EnableAnnotations();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

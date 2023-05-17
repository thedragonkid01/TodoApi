using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoListApi.Models;
using TodoListApi.Repositories;
using TodoListApi.Repositories.IRepositories;
using TodoListApi.Services;
using TodoListApi.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TodoDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("TodoDB")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITodoTagRepository, TodoTagRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

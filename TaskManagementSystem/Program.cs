using TaskManagementSystem.Services;
using TaskManagementSystem.Data;

using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Validators;
using TaskManagementSystem.DTOs;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// register tables
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IValidator<UserRequestDto>, UserCreateValidator>();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IValidator<ProjectRequestDto>, ProjectCreateValidator>();

builder.Services.AddScoped<ITaskItemService, TaskItemService>();
builder.Services.AddScoped<IValidator<TaskItemRequestDto>, TaskItemCreateValidator>();

builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IValidator<CommentRequestDto>, CommentCreateValidator>();

//sqlate connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


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

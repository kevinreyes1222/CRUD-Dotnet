using CRUD.DTOs;
using CRUD.Models;
using CRUD.Services;
using CRUD.Validator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inyections
builder.Services.AddDbContext<LibraryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryConnection")));

builder.Services.AddScoped<IBookService, BookService>();

//Validators

builder.Services.AddScoped<IValidator<BookInsertDto>, BookInsertValidator>();
builder.Services.AddScoped<IValidator<BookUpdateDto>, BookUpdateValidator>();

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

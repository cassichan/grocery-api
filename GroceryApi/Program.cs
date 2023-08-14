using Microsoft.EntityFrameworkCore;
using GroceryApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Add Db context service to the Dependency Injection container in order to register it and
// provide the service to controllers
//Specify that the db context will use in memory db
builder.Services.AddDbContext<GroceryContext>(opt => opt.UseInMemoryDatabase("GroceryList"));


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
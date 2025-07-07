using HR;
using Microsoft.EntityFrameworkCore;

// builder - prepares the project
// app - starts the project

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setup the connection options to the HR database
builder.Services.AddDbContext<HrDbContext>
(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("HrContext"))
);

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

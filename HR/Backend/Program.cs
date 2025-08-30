using HR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// builder - prepares the project
// app - starts the project

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Activate JWT Authentication
// Informing our program that there will be incoming tokens
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // This secret key is used to authenticate the incoming token is the same as the
        // predefined token in the system.
        // Converts into an array of ASCII representation of the key string.
        var key = Encoding.UTF8.GetBytes("9z862Dl7slyWeLB8hmAzHbuoETkaWCE3gwiYbRPGZFY=");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, // The source where the token is issued
            ValidateAudience = false, // The users who can use this token
            ValidateIssuerSigningKey = true, // Make sure that the incoming token is using my secret key
            // Symmetric Security => One key to encrypt and decrypt tokens
            // Asymmetric Security => One key to encrypt and one key to decrypt tokens (more secure)
            IssuerSigningKey = new SymmetricSecurityKey(key), // Generate the token using our key
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

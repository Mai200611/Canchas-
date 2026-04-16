using System;
using Cancha.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=DefaultConnection"));

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
if (builder.Environment.IsDevelopment())
{
    // fallback to LocalDB for local development (avoid exposing remote host-name issues)
    conn = builder.Configuration.GetConnectionString("LocalDb") ?? "Server=(localdb)\\MSSQLLocalDB;Database=Cancha;Trusted_Connection=True;";
}

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(conn, sqlOptions => 
        sqlOptions.EnableRetryOnFailure(maxRetryCount:5, maxRetryDelay:TimeSpan.FromSeconds(30), errorNumbersToAdd:null)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

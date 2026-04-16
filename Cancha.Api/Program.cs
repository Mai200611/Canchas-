using System;
using Cancha.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

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

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();


using System;
using Cancha.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(x =>
{
    x.Password.RequireDigit = false;
    x.Password.RequiredLength = 6;
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

// Registro del SeedDb
builder.Services.AddTransient<SeedDb>();


builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=DefaultConnection"));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Este es el registro del SeedDb
builder.Services.AddTransient<SeedDb>();


builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=DefaultConnection"));

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
if (builder.Environment.IsDevelopment())
{
    conn = builder.Configuration.GetConnectionString("LocalDb") ?? "Server=(localdb)\\MSSQLLocalDB;Database=Cancha;Trusted_Connection=True;";
}

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(conn, sqlOptions =>
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null)));

var app = builder.Build();


// Ejecucion del SeedDb
var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<SeedDb>();
    await seeder.SeedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();


app.MapControllers();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();

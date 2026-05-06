using Cancha.Web;
using Cancha.Web.Repositories;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSweetAlert2();

var apiUrl = builder.Configuration["ApiUrl"] ?? "https://localhost:7246/";

builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri(apiUrl) 
});

builder.Services.AddScoped<IRepository, Repository>();

await builder.Build().RunAsync();



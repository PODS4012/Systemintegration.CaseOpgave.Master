using System.Globalization;
using Systemintegration.CaseOpgave.Shared.DataTransferObjects;
using Systemintegration.CaseOpgave.UI.Extensions;
using Systemintegration.CaseOpgave.UI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureService(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<SecretSettings>(builder.Configuration.GetSection("SecretSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

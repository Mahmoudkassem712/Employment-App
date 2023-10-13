using DataLayer.Data;
using EmploymentApp.BackGroundService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'EmploymentAppContextConnection' not found.");
     builder.Services.AddDbContext<EmploymentSystemDbContext>(options =>
       options.UseSqlServer(connectionString));
builder.Services.AddTransient<EmploymentSystemDbContext>();


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHostedService<BackGroundWorkerService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.MapControllers();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

using Foodie.DataModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add your service registrations
builder.Services.AddScoped<DataAccessModel>();
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(o =>
    o.IdleTimeout = TimeSpan.FromMinutes(3600)
);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();



app.UseSession();

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
    pattern: "{controller=User}/{action=Index}");

app.MapControllerRoute(
    name: "Admin",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Category",
    pattern: "{controller=Admin}/{action=Category}");

app.Run();

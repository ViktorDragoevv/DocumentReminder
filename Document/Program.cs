using Document.Data;
using Document.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Document.Controllers;

var builder = WebApplication.CreateBuilder(args);
var policyName = "_myAllowSpecificOrigins"; // cors
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

/*
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1",
        policy =>
        {
            policy.WithOrigins("https://localhost:7174",
                                "https://localhost:7174");
        });

    options.AddPolicy("AnotherPolicy",
        policy =>
        {
            policy.WithOrigins("https://localhost:7174")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
*/









//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
                      builder =>
                      {
                          builder
                            .WithOrigins("https://localhost:44466") // specifying the allowed origin
                            .WithMethods("GET", "DELETE", "PUT") // defining the allowed HTTP method
                            .AllowAnyHeader(); // allowing any header to be sent
                      });
});
builder.Services.AddControllers(); // cors


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();

app.UseCors(policyName); // added for cors

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html");

app.MapCategoryEndpoints();

app.Run();

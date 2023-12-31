using IdentityServer.AuthServer;
using IdentityServer.AuthServer.Models;
using IdentityServer.AuthServer.Repository;
using IdentityServer.AuthServer.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICustomerUserRepository,CustomUserRepository>();

builder.Services.AddDbContext<CustomDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb"));
});


builder.Services.AddIdentityServer().
    AddInMemoryApiResources(Config.GetApiResources()).
    AddInMemoryApiScopes(Config.GetApiScopes())
    .AddInMemoryClients(Config.GetClients())
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddDeveloperSigningCredential()
    .AddProfileService<CustomProfileService>();

builder.Services.AddControllersWithViews();

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
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

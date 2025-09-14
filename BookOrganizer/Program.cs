using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookOrganizer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using BookOrganizer.Features;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookOrganizerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookOrganizerContext") ?? throw new InvalidOperationException("Connection string 'BookOrganizerContext' not found.")));


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BookOrganizerContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpClient("BookOrganizerApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<BookOrganizerContext>();
    dbContext.Database.Migrate();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roleManager.RoleExistsAsync(Roles.Admin))
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
    }
    if (!await roleManager.RoleExistsAsync(Roles.User))
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.User));
    }
}


app.UseHttpsRedirection();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

RegisterUser.MapEndpoint(app);

app.Run();

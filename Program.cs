using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebApplication4.Data;
using WebApplication4.Areas.Identity.Data;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ClaimContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services
builder.Services.AddDbContext<DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity with the custom user class
builder.Services.AddDefaultIdentity<WebApplication4User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    
    .AddEntityFrameworkStores<DbContext>();

// Add services for controllers with views
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // AddING this to enable authentication
app.UseAuthorization();//ensuring its enabled

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();  // Map Razor Pages

app.Run();
void CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<WebApplication4User>>();
    string[] roleNames = { "Lecturer", "Manager" };

    foreach (var roleName in roleNames)
    {
        var roleExist = roleManager.RoleExistsAsync(roleName).Result;
        if (!roleExist)
        {
            // Create the roles and seed them to the database
            roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
        }
    }

    // Optionally, create a default user and assign them to a role
    var powerUser = new WebApplication4User
    {
        UserName = "admin@admin.com",
        Email = "admin@admin.com"
    };

    string userPassword = "Admin@123";
    var user = userManager.FindByEmailAsync("admin@admin.com").Result;

    if (user == null)
    {
        var createPowerUser = userManager.CreateAsync(powerUser, userPassword).Result;
        if (createPowerUser.Succeeded)
        {
            // Assign Admin role to the user
            userManager.AddToRoleAsync(powerUser, "Manager").Wait();
        }
    }
}

// Call the role creation function
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    CreateRoles(services);
}


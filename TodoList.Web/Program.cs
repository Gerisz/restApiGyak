using ELTE.TodoList.Persistence.Services;
using ELTE.TodoList.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TodoListDbContext>(options =>
{
    IConfigurationRoot configuration = builder.Configuration;

    // Use MSSQL database: need Microsoft.EntityFrameworkCore.SqlServer package for this
    options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));

    // Alternatively use Sqlite database: need Microsoft.EntityFrameworkCore.Sqlite package for this
    //options.UseSqlite(configuration.GetConnectionString("SqliteConnection"));

    // Use lazy loading (don't forget the virtual keyword on the navigational properties also)
    options.UseLazyLoadingProxies();
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<TodoListDbContext>();

builder.Services.AddTransient<ITodoListService, TodoListService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Get an instance of the database context and run DbInitializer
// Creating a new service scope is required, because TodoListDbContext is registered as a scoped service
using (var serviceScope = app.Services.CreateScope())
using (var context = serviceScope.ServiceProvider.GetRequiredService<TodoListDbContext>())
{
    string imageSource = app.Configuration.GetValue<string>("ImageSource");
    DbInitializer.Initialize(context, imageSource);
}

app.Run();
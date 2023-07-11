using ELTE.TodoList.Persistence;
using ELTE.TodoList.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

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
.AddEntityFrameworkStores<TodoListDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddTransient<ITodoListService, TodoListService>();

// Add AutoMapper to container. Pass the assembly of the profiles.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Alternative: it will automatically get the assembly of the given type.
//builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

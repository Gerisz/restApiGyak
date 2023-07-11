using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ELTE.TodoList.Persistence
{
    public class TodoListDbContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<List> Lists { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;

        public TodoListDbContext(DbContextOptions options): base(options)
        {

        }
    }
}

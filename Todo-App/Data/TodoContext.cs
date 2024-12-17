using TodoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        { }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}

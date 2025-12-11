using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;
using Todo_List.Models;

namespace Todo_List.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

    }
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "todo.db");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}

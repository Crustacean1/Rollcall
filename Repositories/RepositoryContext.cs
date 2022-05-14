using Microsoft.EntityFrameworkCore;
using Rollcall.Models;

namespace Rollcall.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.UseCollation("utf8_general_ci");
            builder.Entity<Child>().HasKey(c => c.Id);
            builder.Entity<Group>().HasKey(g => g.Id);
            builder.Entity<MealSchema>().HasKey(s => s.Name);
            builder.Entity<ChildMeal>().HasKey(a => new { a.ChildId, a.MealName, a.Date });
            builder.Entity<GroupMask>().HasKey(a => new { a.GroupId, a.MealName, a.Date });
            builder.Entity<DefaultMeal>().HasKey(a => new { a.ChildId, a.MealName });

            builder.Entity<MealSchema>().HasData(new MealSchema[]{
                new MealSchema{Name = "breakfast"},
                new MealSchema{Name = "dinner"},
                new MealSchema{Name = "desert"}
            });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DbSet<ChildMeal> ChildAttendance { get; set; }
        public DbSet<GroupMask> GroupAttendance { get; set; }
        public DbSet<DefaultMeal> DefaultAttendance { get; set; }
        public DbSet<MealSchema> MealSchemas { get; set; }
    }
}
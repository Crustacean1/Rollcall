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
            builder.Entity<Child>().HasKey(c => c.Id);
            builder.Entity<Group>().HasKey(g => g.Id);
            builder.Entity<MealSchema>().HasKey(s => s.Name);
            builder.Entity<ChildAttendance>().HasKey(a => new { a.ChildId, a.MealName, a.Date });
            builder.Entity<GroupAttendance>().HasKey(a => new { a.GroupId, a.MealName, a.Date });
            builder.Entity<DefaultAttendance>().HasKey(a => new { a.ChildId, a.MealName });

            builder.Entity<MealSchema>().HasData(new MealSchema[]{
                new MealSchema{Name = "breakfast"},
                new MealSchema{Name = "dinner"},
                new MealSchema{Name = "desert"}
            });

            builder.Entity<Group>().HasData(new Group[]{
                new Group{
                    Name = "AEII",
                    Id = 1
                }
            });

            builder.Entity<Child>().HasData(new Child[]{
                new Child{
                    Name = "Kamil",
                    Surname = "Kowalski",
                    GroupId = 1,
                    Id = 1
                }
            });

            builder.Entity<DefaultAttendance>().HasData(
                new DefaultAttendance[]{
                    new DefaultAttendance{ChildId = 1,MealName = "breakfast",Attendance=true},
                    new DefaultAttendance{ChildId = 1,MealName = "dinner",Attendance=true},
                    new DefaultAttendance{ChildId = 1,MealName = "desert",Attendance=false}
                });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DbSet<ChildAttendance> ChildAttendance { get; set; }
        public DbSet<GroupAttendance> GroupAttendance { get; set; }
        public DbSet<DefaultAttendance> DefaultAttendance { get; set; }
        public DbSet<MealSchema> MealSchemas { get; set; }
    }
}
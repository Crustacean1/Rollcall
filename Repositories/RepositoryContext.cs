using Microsoft.EntityFrameworkCore;
using Rollcall.Models;

namespace Rollcall.Repositories{
    public class RepositoryContext : DbContext{
        public RepositoryContext(DbContextOptions options) : base(options){

        }
        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<Mask>().HasKey((mask) => new {mask.GroupId,mask.Date});

            builder.Entity<Attendance>().HasKey((att) => new {att.ChildId,att.Date});

            
            builder.Entity<MealSchema>().HasData(new MealSchema[]{
                new MealSchema{Name = "breakfast", Id = 1},
                new MealSchema{Name = "dinner", Id = 2},
                new MealSchema{Name = "desert", Id = 3}
            });
        }
        public DbSet<User> Users{get;set;}
        public DbSet<Child> Children{get;set;}
        public DbSet<Group> Groups{get;set;}
        public DbSet<Attendance> Attendance{get;set;}
        public DbSet<Mask> Masks{get;set;}
        public DbSet<MealSchema> MealSchemas{get;set;}
    }
}
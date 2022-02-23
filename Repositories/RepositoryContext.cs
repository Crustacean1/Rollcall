using Microsoft.EntityFrameworkCore;
using Rollcall.Models;

namespace Rollcall.Repositories{
    public class RepositoryContext : DbContext{
        public RepositoryContext(DbContextOptions options) : base(options){

        }
        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<Attendance>().HasKey((day) => new {day.ChildId,day.Date});
        }
        public DbSet<User> Users{get;set;}
        public DbSet<Child> Children{get;set;}
        public DbSet<Group> Groups{get;set;}
        public DbSet<Attendance> Attendance{get;set;}
        public DbSet<Mask> Masks{get;set;}
        public DbSet<MealSchema> MealSchemas{get;set;}
    }
}
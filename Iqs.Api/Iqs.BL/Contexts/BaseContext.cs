using Iqs.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iqs.DAL.Contexts
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options) : base(options) {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<UserGrade> UserGrades { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.EfCore.PostgreSql
{
    public class ExampleDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = "Host=localhost;Port=5432;Database=postgres;Username=admin;Password=123456;";
            optionsBuilder.UseNpgsql(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }

    [Table("student")]
    public class Student
    {
        [Column("id")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        [Column("age")]
        public int Age { get; set; }
    }
}

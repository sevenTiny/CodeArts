using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFCoreMySql.Entity
{
    public partial class SevenTinyTestContext : DbContext
    {
        public SevenTinyTestContext()
        {
        }

        public SevenTinyTestContext(DbContextOptions<SevenTinyTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OperateTest> OperateTest { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("这里写连接字符串");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperateTest>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Key2 })
                    .HasName("PRIMARY");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                //entity.Property(e => e.Key2).HasColumnType("int(11)");

                //entity.Property(e => e.DateNullKey).HasColumnType("date");

                //entity.Property(e => e.DateTimeNullKey)
                //    .HasColumnType("datetime");

                //entity.Property(e => e.IntKey).HasColumnType("int(11)");

                //entity.Property(e => e.IntNullKey).HasColumnType("int(11)");

                //entity.Property(e => e.StringKey).HasColumnType("varchar(500)");
            });

            //modelBuilder.Entity<Student>(entity =>
            //{
            //    entity.Property(e => e.Id).HasColumnType("int(11)");

            //    entity.Property(e => e.Age).HasColumnType("int(11)");

            //    entity.Property(e => e.Name).HasColumnType("varchar(255)");

            //    entity.Property(e => e.SchoolTime)
            //        .HasColumnType("datetime");
            //});
        }
    }
}

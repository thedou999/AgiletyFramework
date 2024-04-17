using DatabaseTest.model.entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTest.model
{
    public class TempDbContext:DbContext
    {
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<TeacherEntity> Teachers { get; set; }
        public DbSet<StudentTeacherMap> StudentTeacherMaps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=PC-20240312PQQX\\SQLEXPRESS;Initial Catalog=Temp;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region
            //modelBuilder.Entity<StudentEntity>()
            //    .HasMany(s => s.Teachers)
            //    .WithMany(t => t.Students)
            //    .UsingEntity("StudentTeacherMap",
            //      t => t.HasOne(typeof(TeacherEntity)).WithMany().HasForeignKey("TeacherId").HasPrincipalKey(nameof(TeacherEntity.Id)),
            //      s => s.HasOne(typeof(StudentEntity)).WithMany().HasForeignKey("StudentId").HasPrincipalKey(nameof(StudentEntity.Id)),
            //    m => m.HasKey("StudentId", "TeacherId")
            //    );
            #endregion
            #region
            modelBuilder.Entity<StudentEntity>()
                .HasMany(s => s.Teachers)
                .WithMany(t => t.Students)
                .UsingEntity<StudentTeacherMap>(
                    t => t.HasOne(t => t.Teacher).WithMany(m => m.StudentTeacherMaps).HasForeignKey(m => m.TeacherId),
                    s => s.HasOne(s => s.Student).WithMany(m => m.StudentTeacherMaps).HasForeignKey(m => m.StudentId)
                );
            #endregion

            modelBuilder.Entity<TeacherEntity>()
                .HasMany(t => t.Students)
                .WithMany(s => s.Teachers);
        }

    }
}

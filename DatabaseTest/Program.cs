using AgiletyFramework.DBModels.DbContexts;
using AgiletyFramework.DBModels.Entities;
using DatabaseTest.model;
using DatabaseTest.model.entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace DatabaseTest
{
    public class DatabaseTest
    {
        static async Task Main(string[] args)
        {
            //await InitData();
            //await AddStudentToTeacher();
            await ReadData();
            //await DeleteData();
        }

        static async Task InitData()
        {
            using(TempDbContext dbContext = new TempDbContext())
            {
                var s1 = new StudentEntity() { Name = "s1" };
                var s2 = new StudentEntity() { Name = "s2" };

                var t1 = new TeacherEntity() { Name = "t1" };
                var t2 = new TeacherEntity() { Name = "t2" };

                dbContext.Students.Add(s1);
                dbContext.Students.Add(s2);

                dbContext.Teachers.Add(t1);
                dbContext.Teachers.Add(t2);

                try
                {
                    int result = dbContext.SaveChanges();
                    if (result <= 0) throw new Exception("添加失败");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        static async Task AddStudentToTeacher()
        {
            using (TempDbContext dbContext = new TempDbContext())
            {
                var students = dbContext.Students.Where(o => true).ToList();
                var t1 = dbContext.Teachers.Where(o => o.Name.Equals("t1")).FirstOrDefault();
                var t2 = dbContext.Teachers.Where(o => o.Name.Equals("t2")).FirstOrDefault();

                //foreach (var student in students)
                //{
                //    t1.Students.Add(student);
                //}
                //t2.Students.Add(dbContext.Students.FirstOrDefault());

                #region
                foreach (var student in students)
                {
                    var newMap1 = new StudentTeacherMap() { Student = student, Teacher = t1 };
                    newMap1 = dbContext.StudentTeacherMaps.Add(newMap1).Entity;
                    t1.StudentTeacherMaps.Add(newMap1);
                }
                var newMap2 = new StudentTeacherMap() { Student = students.FirstOrDefault(), Teacher = t2 };
                newMap2 = dbContext.StudentTeacherMaps.Add(newMap2).Entity;
                t2.StudentTeacherMaps.Add(newMap2);
                #endregion

                try
                {
                    int result = dbContext.SaveChanges();
                    if (result <= 0) throw new Exception("添加失败");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        static async Task ReadData()
        {
            using (TempDbContext dbContext = new TempDbContext())
            {
                //var students = dbContext.Students.Include(s => s.Teachers).Include(s => s.StudentTeacherMaps).ToList();
                //var students = dbContext.Students.Include(s => s.Teachers).ToList();

                //foreach (var student in students)
                //{
                //    foreach (var teacher in student.Teachers)
                //    {
                //        await Console.Out.WriteLineAsync(teacher.Name);
                //    }
                //}

                var student = dbContext.Students.Where(s => s.Name.Equals("adfsafd")).FirstOrDefault();
                await Console.Out.WriteLineAsync(student.ToString());

            }
        }

        static async Task DeleteData()
        {
            using (TempDbContext dbContext = new TempDbContext())
            {
                //var students = dbContext.Students.Where(s => true);
                //dbContext.Students.RemoveRange(students);

                //var teachers = dbContext.Teachers.Where(t => true);
                //dbContext.Teachers.RemoveRange(teachers);

                var maps = dbContext.StudentTeacherMaps.Where(t => true);
                dbContext.StudentTeacherMaps.RemoveRange(maps);

                try
                {
                    int result = dbContext.SaveChanges();
                    if (result < 0) throw new Exception("添加失败");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTest.model.entity
{
    public class StudentTeacherMap
    {
        public int StudentId { get; set; } 
        public StudentEntity Student { get; set; }

        public int TeacherId { get; set; }
        public TeacherEntity Teacher { get; set; }
    }
}

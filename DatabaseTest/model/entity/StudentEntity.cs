using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTest.model.entity
{
    public class StudentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TeacherEntity> Teachers { get; set; } = [];
        public ICollection<StudentTeacherMap> StudentTeacherMaps { get; set; } = [];
    }
}

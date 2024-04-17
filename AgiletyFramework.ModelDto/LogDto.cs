using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDto
{
    public class LogDto
    {
        public DateTime Date { get; set; }
        public string? Thread { get; set; }
        public string? Level { get; set; }
        public string? Logger { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime ModifyTime { get; set; }
        public int Status { get; set; }
    }
}

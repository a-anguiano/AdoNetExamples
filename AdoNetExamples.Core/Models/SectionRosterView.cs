using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetExamples.Core.Models
{
    public class SectionRosterView
    {
        public int StudentID { get; set; }
        public string StudentFirst { get; set; } //FirstName
        public string StudentLast { get; set; }
        public decimal CurrentGrade { get; set; }
    }
}

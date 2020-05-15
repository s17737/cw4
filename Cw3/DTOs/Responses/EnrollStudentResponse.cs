using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DTOs.Responses
{
    public class EnrollStudentResponse
    {
        public int IdEnrollment { get; set; }
        public string Semester { get; set; }
        public int IdStudy { get; set; }
        public string StartDate { get; set; }
    }
}

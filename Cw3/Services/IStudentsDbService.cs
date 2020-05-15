using Cw3.DTOs.Requests;
using Cw3.DTOs.Responses;
using Cw3.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Services
{
    public interface IStudentsDbService
    {
        IEnumerable<Student> GetStudent();
        IEnumerable<Student> GetStudent(string id);
        IEnumerable<EnrollStudentResponse> EnrollStudent(EnrollStudentRequest model);
        //object EnrollStudent(EnrollStudentRequest enrollStudentRequest, object model);
       
        //IEnumerable<StudiesResponse> GetStudies(StudiesRequest modelStudies);
    }
    
}

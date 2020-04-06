using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using Cw3.Models;
using Cw3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        //private const string ConString = "Data Source=db-mssql;Initial Catalog=s17737;Integrated Security=True";

        private IStudentsDal _dbService;

        public StudentsController(IStudentsDal dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudent()
        {
            var list = new List<Student>();

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17737;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT s.FirstName, s.LastName, s.BirthDate, st.Name, e.Semester " +
                    "FROM Student s " +
                    "JOIN Enrollment e on e.IdEnrollment = s.IdEnrollment " +
                    "JOIN Studies st on st.IdStudy = e.IdStudy;";

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.Name = dr["Name"].ToString();
                    st.Semester = dr["Semester"].ToString();
                    list.Add(st);
                }

            }
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17737;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT s.FirstName, s.LastName, s.BirthDate, st.Name, e.Semester " +
                    "FROM Student s " +
                    "JOIN Enrollment e on e.IdEnrollment = s.IdEnrollment " +
                    "JOIN Studies st on st.IdStudy = e.IdStudy WHERE s.IndexNumber = @id;";

                com.Parameters.AddWithValue("id", id);

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.Name = dr["Name"].ToString();
                    st.Semester = dr["Semester"].ToString();
                    //...
                    return Ok(st);
                }
            }
            return NotFound("Nie znaleziono studenta");
        }
    }
}

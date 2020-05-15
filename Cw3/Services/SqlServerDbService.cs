using Cw3.DTOs.Requests;
using Cw3.DTOs.Responses;
using Cw3.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Services
{
    public class SqlServerDbService : IStudentsDbService
    {
        //public IEnumerable<Student> GetStudents(EnrollStudentRequest model)
        public IEnumerable<EnrollStudentResponse> EnrollStudent(EnrollStudentRequest model)
        {
            Dictionary<string, string> studiesList = new Dictionary<string, string>();

            var IdEnrollment = new List<string>();
            var id = new List<string>();

            //Następnie sprawdzamy czy istnieją studia w
            //tabeli Studies zgodne z wartością przesłaną przez klienta

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17737;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT Name from Studies;";

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    studiesList.Add(dr["Name"].ToString(), dr["Name"].ToString());
                }
                studiesList.TryGetValue(model.Name, out string value2);

                try
                {
                    if (!value2.Equals(null))
                    {
                        //niech zwraca response - nie, response na koncu
                        //return Ok(model.Name);
                    }
                }
                catch (Exception e)
                {

                    //return BadRequest();
                }
                con.Close();
            }

            //Następnie odnajdujemy najnowszy wpis w tabeli Enrollments
            //zgodny ze studiami studenta i wartością Semester = 1(student zapisuje się na pierwszy
            //).

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17737;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT e.IdEnrollment FROM enrollment e " +
                    "JOIN studies s ON s.IdStudy = e.IdStudy " +
                    "where e.semester = 1 " +
                    "and s.Name = '" + model.Name + "'";
                con.Open();

                var dr2 = com.ExecuteReader();
                while (dr2.Read())
                {
                    IdEnrollment.Add(dr2["IdEnrollment"].ToString());
                    Console.WriteLine("dodanie do listy: " + IdEnrollment);
                }
                con.Close();
            }

            //Jeśli tak wpis nie istnieje to dodajemy go do bazy danych(StartDate ustawiamy na aktualną datę).

            /*using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17737;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select cast(count(IdEnrollment) as varchar(10)) from Enrollment";
                con.Open();

                var dr3 = com.ExecuteReader();

                //Jeśli tak wpis nie istnieje to dodajemy go do bazy danych
                //if (IdEnrollment.Count == 0)
                //{

                while (dr3.Read())
                    {
                        id.Add(dr3["IdsEnrollment"].ToString());
                    //int x = Int32.Parse(str);
                    Console.WriteLine("zliczone IdEnrollment: " + id);
                    }
                con.Close();

            //Na końcu dodajemy wpis w tabeli Students. Pamiętamy o
            //tym, aby sprawdzić czy indeks podany przez studenta jest unikalny. W przeciwnym
            //wypadku zgłaszamy błąd.


                //string resultId = 1 + id;
                //Console.WriteLine("resultId: "+resultId);

                //com.CommandText = "Insert into enrollment values()";
                //}
            }
            */

            //W ciele
            //żądania zwracamy przypisany do studenta obiekt Enrollment reprezentujący semestr
            //na który został wpisany.

            var list = new List<EnrollStudentResponse>();
            list.Add(new EnrollStudentResponse
            {
                IdEnrollment = 1,
                Semester = "1",
                IdStudy = 1,
                StartDate = "test"
            });

            return list;
        }

        /*public object EnrollStudent(EnrollStudentRequest enrollStudentRequest, object model)
        {
            throw new NotImplementedException();
        }*/

        public IEnumerable<Student> GetStudent()
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

                return list;

            }
        }

        public IEnumerable<Student> GetStudent(string id)
        {
            var list = new List<Student>();

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
                    list.Add(st);
                }
                return list;
            }

        }

        
        //public IEnumerable<StudiesResponse> GetStudies(StudiesRequest modelStudies)
        //{
            /*
           //Powinniśmy upewnić się, że w tabeli Enrollment istnieje wpis o podanej wartości Studies i Semester.
           //W przeciwnym razie zwracamy błąd 404 Not Found.
           Dictionary<string, string> studiesList = new Dictionary<string, string>();
           Dictionary<string, string> semesterList = new Dictionary<string, string>();

           using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17737;Integrated Security=True"))
           using (var com = new SqlCommand())
           {
               com.Connection = con;
               com.CommandText = "SELECT Name from Studies;";

               con.Open();
               var dr = com.ExecuteReader();
               while (dr.Read())
               {
                   studiesList.Add(dr["Name"].ToString(), dr["Name"].ToString());
               }
               studiesList.TryGetValue(modelStudies.Name, out string value2);

               try
               {
                   if (!value2.Equals(null))
                   {
                       //niech zwraca response - nie, response na koncu
                       //return Ok(model.Name);
                   }
               }
               catch (Exception e)
               {
                   return BadRequest();
               }
               con.Close();
           }
           */

           // -----------

            /*var list = new List<StudiesResponse>();
            list.Add(new StudiesResponse
            {
                Name = "IT",
                Semester = "1"
            });

            return list;

        }*/


    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tut4.Models;

namespace tut4.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {


        [HttpGet]
        public IActionResult GetStudent(string orderBy)
        {
            var listOfStudents = new List<Student>();
            using (var connection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True"))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"select s.FirstName, s.LastName, s.BirthDate, st.studyName, en.Semester
                                            from Students s,Studies st,Enrollment en
											where s.idEnrollment = en.idEnrollment AND
											st.idStudy = en.idStudy;";
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var st = new Student
                        {
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            DateOfBirth = DateTime.Parse(reader["BirthDate"].ToString()),
                            Studies = reader["Studies"].ToString(),
                            Semester = int.Parse(reader["Semester"].ToString())
                        };
                        listOfStudents.Add(st);
                    }
                }
            }

            return Ok(listOfStudents);
        }

        [HttpGet("{id}")]
        public IActionResult GetSemester(string id)
        {
            Enrollment enrollment = null;


            using (var connection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True"))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT * FROM Enrollment en, Students s
                                            Where s.idEnrollment = en.idEnrollment
                                            AND s.indexNumber=@id;";
                    command.Parameters.AddWithValue("id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        enrollment = new Enrollment
                        {
                            IdEnrollment = int.Parse(reader["IdEnrollment"].ToString()),
                            IdStudy = int.Parse(reader["IdStudy"].ToString()),
                            Semester = int.Parse(reader["Semester"].ToString()),
                            StartDate = DateTime.Parse(reader["StartDate"].ToString()),
                        };
                    }

                    return Ok(enrollment);

                }

            }

        }
    }
}

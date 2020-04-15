using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tut4.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using tut4.Services;
using tut4.DTOs;
using System.Data.SqlClient;

namespace tut4.Controllers
{
    [Route("api/enrollment")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentServiceDb _service;


        public EnrollmentsController(IStudentServiceDb service)
        {
            _service = service;
        }
        [HttpPost("PromStud")]
        public IActionResult PromoteStudents()
        {

            _service.PromoteStudents(1, "IT");

            return Ok();
        }

        [HttpPost(Name = "EnrollStudents")]
        public IActionResult EnrollStudent(EnrollRequest request)
        {
            _service.EnrollStudent(request);

            var response = new EnrollResponse();

            using (SqlConnection con = new SqlConnection("Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=2019SBD;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                var transac = con.BeginTransaction();
                com.CommandText = "select studyName Studies where studyName=@par1";
                com.Parameters.AddWithValue("par1", request.Studies);
                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    transac.Rollback();
                    Console.WriteLine("Error");
                }
                return Ok();
            }
        }
      

    }
}
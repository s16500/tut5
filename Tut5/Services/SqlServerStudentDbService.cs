using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tut4.DTOs.Request;

namespace tut4.Services
{
    public class SqlServerStudentDbService
    {
        public void EnrollStudent(EnrollRequest request)
        {
            
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
                    Console.WriteLine("404 Error");
                }
                com.CommandText = "insert into students (idStudent,firstName,lastName,indexNumber) values (@par1,@par2,@par3,@par4,@par5,par6)";

                com.Parameters.AddWithValue("par1", request.idStudent);
                com.Parameters.Add("par2", SqlDbType.VarChar, 50).Value = $"s{new Random().Next(1, 20000)}";
                com.Parameters.AddWithValue("par3", request.firstName);
                com.Parameters.AddWithValue("par4", request.lastName);
                com.Parameters.AddWithValue("par5", request.idEnrollment);
                com.Parameters.AddWithValue("par6", request.BirthDate);

                con.Open();
                int affectedRows = com.ExecuteNonQuery();
                transac.Commit();
            }
            
        }
    }
}

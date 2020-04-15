using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tut4.DTOs.Request;

namespace tut4.Services
{
    public interface IStudentServiceDb
    {
        void EnrollStudent(EnrollRequest req);
        void PromoteStudents(int semester, string studies);
    }
}
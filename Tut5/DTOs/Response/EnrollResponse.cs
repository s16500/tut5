using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tut4.DTOs
{
    public class EnrollResponse
    {
        [Required]
        public string idEnrollment { get; set; }
        [Required]
        public string Semester { get; set; }
        [Required]
        public string idStudy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tut4.DTOs.Request
{
    public class EnrollRequest
    {
        [Required]
        public string idStudent { get; set; }
        [Required]
        public string indexNumber { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string idEnrollment { get; set; }
        [Required]
        public string Studies { get; set; }

    }
}

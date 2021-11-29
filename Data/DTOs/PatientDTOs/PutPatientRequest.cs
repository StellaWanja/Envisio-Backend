using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Envisio.Data.DTOs.PatientDTOs
{
    public class PutPatientRequest
    {
        [Required]
        public string FirstName {get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MaritalStatus { get; set; }
        [Required]
        public string DOB {get; set; }
        [Required]
        public double Height {get; set; }
        [Required]
        public double Weight {get; set; }
    }
}

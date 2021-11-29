using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Envisio.Data.DTOs.PatientDTOs
{
    public class GetPatientResponse
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FirstName {get; set; }
        public string LastName { get; set; }
        public string MaritalStatus { get; set; }
        public string DOB {get; set; }
        public double Height {get; set; }
        public double Weight {get; set; }
        public string FamilyMedicalHistory {get; set; }

    }
}

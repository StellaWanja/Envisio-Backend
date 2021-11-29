using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Envisio.Models
{
    public class Patient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FirstName {get; set; }
        public string LastName { get; set; }
        public string MaritalStatus { get; set; }
        public string DOB {get; set; }
        public double Height {get; set; }
        public double Weight {get; set; }
        public string FamilyMedicalHistory {get; set; }
        public List<TestResult> TestResults {get; set;}
    }
}

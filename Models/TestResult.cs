using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Envisio.Models
{
    public class TestResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string Date {get; set; }
        public string Test { get; set; }
        public string Result { get; set; }

        public TestResult()
        {
            this.Date = DateTime.Now.ToShortDateString();
            this.Test = "Breast Cancer";
        }
    }
}

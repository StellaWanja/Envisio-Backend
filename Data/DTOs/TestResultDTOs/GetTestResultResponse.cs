using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Envisio.Data.DTOs.TestResultDTOs
{
    public class GetTestResultResponse
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string Date {get; set; }
        public string Test { get; set; }
        public string Result { get; set; }

        public GetTestResultResponse()
        {
            this.Date = DateTime.Now.ToShortDateString();
            this.Test = "Breast Cancer";
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Envisio.Data.DTOs.TestResultDTOs
{
    public class AddTestResultResponse
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string Result { get; set; }
    }
}

using Envisio.Models;
using Envisio.Data;
using Envisio.Data.DTOs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Envisio.Data.DTOs.PatientDTOs;
using Microsoft.EntityFrameworkCore;

namespace Envisio.BusinessLogic
{
    public interface ITestResultService
    {
        Task<TestResult> CreateTestResult(string patientId, string result);
        Task<List<TestResult>> GetAllTestResultsBelongingToAPatient(string patientId);
        Task<TestResult> GetFromApi();
    }
}
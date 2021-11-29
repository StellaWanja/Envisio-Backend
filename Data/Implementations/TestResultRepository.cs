using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Envisio.Models;

namespace Envisio.Data
{
    public class TestResultRepository : ITestResultRepository
    {
        private readonly AppDbContext _context;

        public TestResultRepository(AppDbContext context)
        {
            _context = context;
        }

        //add test result to database
        public async Task<TestResult> AddTestResult(TestResult testResult)
        {
            try
            {
                await _context.AddAsync(testResult);
                var result = await _context.SaveChangesAsync();

                return testResult;
            }
            catch (Exception e)
            {             
                throw new Exception(e.Message);
            }       
        }

        //get result per patient
        public async Task<List<TestResult>> GetAllTestResultsPerPatient(string patientId)
        {
            return await _context.TestResults.Where(testResult => testResult.PatientId == patientId).ToListAsync();
        }

    }
}
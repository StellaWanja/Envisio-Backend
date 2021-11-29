using Envisio.Models;
using Envisio.Data;
using Envisio.Data.DTOs;
using Envisio.Data.DTOs.PatientDTOs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace Envisio.BusinessLogic
{
    public class TestResultService : ITestResultService
    {
        //actions to collect data from datastore
        private readonly ITestResultRepository _dataStore;
        private HttpClient _httpClient;


        public TestResultService(ITestResultRepository dataStore, HttpClient httpClient)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        //create test result method
        public async Task<TestResult> CreateTestResult(string patientId, string result)
        {
            TestResult testResult = new TestResult
            {
                PatientId = patientId,
                Result = result
            };
            var resultOfTest = await _dataStore.AddTestResult(testResult);            
            if(resultOfTest != null)
            {
                return testResult;
            }
            throw new TimeoutException("Unable to create test result instance at this time"); 
        }

        //get all patients per user
        public async Task <List<TestResult>> GetAllTestResultsBelongingToAPatient(string patientId)
        {
            return await _dataStore.GetAllTestResultsPerPatient(patientId);
        }

        public async Task<TestResult> GetFromApi()
        {
            string APIURL = "http://envisio-001-site1.itempurl.com/api/v1/PredictionTest";
            
            var response = await _httpClient.GetAsync(APIURL);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var createdRes = JsonSerializer.Deserialize<TestResult>(content);
            return createdRes;
        }
    }
}
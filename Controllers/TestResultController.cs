using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Http;
using Microsoft.AspNetCore.Mvc;
using Envisio.BusinessLogic;
using Envisio.Data;
using Envisio.Models;
using Envisio.Data.DTOs.TestResultDTOs;
using Envisio.Data.DTOs.Mappings;
using System.Text.Json;
using System.Net.Http;

namespace envisio_backendv2
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] //authorize data annotation to be applied in all methods

    public class TestResultController : ControllerBase
    {
        private readonly ITestResultService _testResultService;
        private HttpClient _httpClient;

        public TestResultController(ITestResultService testResultService, HttpClient httpClient)
        {
            _testResultService = testResultService ?? throw new ArgumentNullException(nameof(testResultService));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        //get all results for a patient
        [HttpGet("all-results")]
        public async Task<ActionResult<List<GetTestResultResponse>>> TestResult([FromQuery] string patientId)
        {
            try
            {
                var result = await _testResultService.GetAllTestResultsBelongingToAPatient(patientId);
                var results = TestResultMappings.GetResultsForAPatient(result);
                return results;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // //create new test result
        [HttpPost("test-result")]
        public async Task<IActionResult> AddTestResult(AddTestResultRequest addTestResult)
        {
            try
            {
                var testResult = await _testResultService.CreateTestResult(addTestResult.PatientId, addTestResult.Result);
                //pass through mapping
                var result = TestResultMappings.AddTestResultResponse(testResult);
                //create new test result
                return Created("", result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
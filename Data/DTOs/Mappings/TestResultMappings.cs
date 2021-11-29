using Envisio.Models;
using Envisio.Data.DTOs.TestResultDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Envisio.Data.DTOs.Mappings
{
    // structure of the response 
    public class TestResultMappings
    {
        public static AddTestResultResponse AddTestResultResponse(TestResult testResult)
        {
            return new AddTestResultResponse
            {
                Id = testResult.Id,
                PatientId = testResult.PatientId,
                Result = testResult.Result
            };
        }

        public static GetTestResultResponse GetTestResultResponse(TestResult testResult)
        {
            return new GetTestResultResponse
            {
                Id = testResult.Id,
                PatientId = testResult.PatientId,
                Result = testResult.Result
            };
        }

        public static List<GetTestResultResponse> GetResultsForAPatient(List<TestResult> testResults)
        {
            var testsVM = new List<GetTestResultResponse>();
            foreach (var result in testResults)
            {
                testsVM.Add(
                    new GetTestResultResponse
                    {
                        Id = result.Id,
                        PatientId = result.PatientId,
                        Date = result.Date,
                        Test = result.Test,
                        Result = result.Result
                    }
                );
            }
            return testsVM;
        }
    }
}
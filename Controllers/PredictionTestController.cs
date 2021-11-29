using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Envisio.BusinessLogic;
using Envisio.Data;
using Envisio.Models;
using Envisio.Data.DTOs.PredictionTestDTOs;
using Envisio.Data.DTOs.Mappings;
using System.Text.Json;
using System.Text;
using System.Net; 
using Newtonsoft.Json;

using Newtonsoft.Json.Linq;
namespace envisio_backendv2
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] //authorize data annotation to be applied in all methods

    public class PredictionTestController : ControllerBase
    {
        private HttpClient _httpClient;
        private readonly ITestResultService _testResultService;

        public PredictionTestController(ITestResultService testResultService, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _testResultService = testResultService ?? throw new ArgumentNullException(nameof(testResultService));
        }

        [HttpPost]
        public async Task<string> AddToApi(AddPredictionTestRequest addPredictionTest)
        {
            try
            {
                var predictionTest = new PredictionTest
                {
                    RadiusMean = addPredictionTest.RadiusMean,
                    TextureMean = addPredictionTest.TextureMean,
                    PerimeterMean = addPredictionTest.PerimeterMean,
                    AreaMean = addPredictionTest.AreaMean,
                    CompactnessMean = addPredictionTest.CompactnessMean,
                    ConcavityMean = addPredictionTest.ConcavityMean,
                    PerimeterSe = addPredictionTest.PerimeterSe, 
                    AreaSe = addPredictionTest.AreaSe,
                    RadiusWorst = addPredictionTest.RadiusWorst,
                    TextureWorst = addPredictionTest.TextureWorst,
                    PerimeterWorst = addPredictionTest.PerimeterWorst,
                    AreaWorst = addPredictionTest.AreaWorst,
                    CompactnessWorst = addPredictionTest.CompactnessWorst,
                    ConcavityWorst = addPredictionTest.ConcavityWorst
                };

                var content = new StringContent(JsonConvert.SerializeObject(predictionTest), Encoding.UTF8, "application/json"); 
                var response = await _httpClient.PostAsync("https://acesbc.herokuapp.com/", content);
                var body = await response.Content.ReadAsStringAsync();

                if(body != null)
                {
                    var userObj = JObject.Parse(body);
                    var userResults = Convert.ToInt32(userObj["results"]["results"]); //return 1 or 0
                    if(userResults == 1)
                    {
                        return "Benign";
                    }
                    if(userResults == 0)
                    {
                        return "Malignant";
                    }
                }
                return $"Status Code: {response.StatusCode}";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
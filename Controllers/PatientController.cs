using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Envisio.BusinessLogic;
using Envisio.Data;
using Envisio.Models;
using Envisio.Data.DTOs.PatientDTOs;
using Envisio.Data.DTOs.Mappings;

namespace envisio_backendv2
{
    [ApiController]
    [Route("api/v1/")]
    [Authorize] //authorize data annotation to be applied in all methods

    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        }

        //get patient
        [HttpGet]
        [Route("[controller]")]
        public async Task<ActionResult<GetPatientResponse>> GetPatient([FromQuery] string patientId)
        {
            try
            {
                var patient = await _patientService.DisplayPatient(patientId);
                var result = PatientMappings.GetPatientResponse(patient);
                return Ok(result);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Resource does not exist");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //get all patients for a user
        [HttpGet("all-patients")]
        public async Task<ActionResult<List<GetPatientResponse>>> Profile([FromQuery] string userId)
        {
            try
            {
                var result = await _patientService.GetAllPatientsBelongingToAUser(userId);
                var patients = PatientMappings.GetPatientsForAUser(result);
                return patients;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //create new patient
        [HttpPost("register-patient")]
        public async Task<IActionResult> AddPatient(AddPatientRequest addPatient)
        {
            try
            {
                var patient = await _patientService.CreatePatient(addPatient.FirstName, addPatient.LastName, addPatient.MaritalStatus, addPatient.DOB, addPatient.Height, addPatient.Weight, addPatient.FamilyMedicalHistory,addPatient.UserId);
                //pass through mapping
                var result = PatientMappings.AddPatientResponse(patient);
                //create new patient
                return CreatedAtAction(nameof(GetPatient), new { patientId = patient.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //update selected patient fields
        [HttpPatch("update-patient")]
        public async Task<IActionResult> UpdatePatientUsingPatch(PatchPatientRequest patchRequest, [FromQuery] string patientId, string userId)
        {
            try
            {
                var result = await _patientService.UpdatePatientUsingPatch(patchRequest, patientId, userId);
                if (result)
                {
                    //return no content - has been updated
                    return NoContent();
                }

                return NotFound("Resource not found");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //delete patient
        [HttpDelete]
        public async Task<ActionResult<Patient>> DeletePatient([FromQuery] string patientId)
        {
            try
            {
                //find user id
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //remove store
                var result = await _patientService.RemovePatient(patientId, userId);
                if (result)
                {
                    //return no content - has been deleted
                    return NoContent();
                }

                return NotFound("Resource not found");
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
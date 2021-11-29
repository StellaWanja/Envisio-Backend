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
    public interface IPatientService
    {
        Task<Patient> CreatePatient(string firstName, string lastName, string maritalStatus, string dOB, double height, double weight, string familyMedicalHistory, string userId);
        Task<Patient> DisplayPatient(string patientId);
        Task<List<Patient>> GetAllPatients();
        Task<List<Patient>> GetAllPatientsBelongingToAUser(string userId);
        Task<bool> UpdatePatientUsingPatch(PatchPatientRequest patientDTO, string patientId, string userId);
        Task<bool> RemovePatient(string patientId, string userId);
    }
}
using Envisio.Models;
using Envisio.Data;
using Envisio.Data.DTOs;
using Envisio.Data.DTOs.PatientDTOs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Envisio.BusinessLogic
{
    public class PatientService : IPatientService
    {
        //actions to collect data from store
        private readonly IPatientRepository _dataStore;

        public PatientService(IPatientRepository dataStore)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
        }

        //create patient method
        public async Task<Patient> CreatePatient(string firstName, string lastName, string maritalStatus, string dOB, double height, double weight, string familyMedicalHistory, string userId)
        {
            Patient patient = new Patient
            {
                FirstName = firstName,
                LastName = lastName,
                MaritalStatus = maritalStatus,
                DOB = dOB,
                Height = height,
                Weight = weight,
                FamilyMedicalHistory = familyMedicalHistory, 
                UserId = userId
            };
            var result = await _dataStore.AddPatient(patient);            
            if(result != null)
            {
                return patient;
            }
            throw new TimeoutException("Unable to create patient instance at this time"); 
        }

        // Get specific patient
        public async Task<Patient> DisplayPatient(string patientId)
        {
            return await _dataStore.GetPatient(patientId);
        }

        //get all patients
        public async Task<List<Patient>> GetAllPatients()
        {
            return await _dataStore.GetAllPatients();
        }

        //get all patients per user
        public async Task<List<Patient>> GetAllPatientsBelongingToAUser(string userId)
        {
            return await _dataStore.GetAllPatientsPerUser(userId);
        }

        //update using patch
        public async Task<bool> UpdatePatientUsingPatch(PatchPatientRequest patientDTO, string patientId, string userId)
        {
            try
            {
                //get the patient details
                var patient = await DisplayPatient(patientId);
                //if store is null, return false
                if (patient == null)
                {
                    return false;
                }
                //if user id doesnt match, throw exception
                if (patient.UserId != userId)
                {
                    throw new UnauthorizedAccessException("Forbidden");
                }
                //update only certain details
                //if eg name = new name, then set it to the new name, else set it to previous one
                patient.FirstName = patientDTO.FirstName ?? patient.FirstName;
                patient.LastName = patientDTO.LastName ?? patient.LastName;
                patient.MaritalStatus = patientDTO.MaritalStatus ?? patient.MaritalStatus;
                patient.DOB = patientDTO.DOB ?? patient.DOB;
                patient.FamilyMedicalHistory = patientDTO.FamilyMedicalHistory ?? patient.FamilyMedicalHistory;
                patient.Height = patientDTO.Height != 0 ? patientDTO.Height : patient.Height;
                patient.Weight = patientDTO.Weight != 0 ? patientDTO.Weight : patient.Weight;

                return await _dataStore.UpdatePatient(patient);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //remove patient method
        public async Task<bool> RemovePatient(string patientId, string userId)
        {
            //get the patient
            var result = await DisplayPatient(patientId);
            //only the user can delete the patient
            if (result.UserId == userId)
            {
                return await _dataStore.DeletePatient(patientId);
            }
            //else throw exception
            throw new TimeoutException("Unable to create patient instance at this time"); 
        }
    }
}
using Envisio.Models;
using Envisio.Data.DTOs.PatientDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Envisio.Data.DTOs.Mappings
{
    // structure of the response 
    public class PatientMappings
    {
        public static AddPatientResponse AddPatientResponse(Patient patient)
        {
            return new AddPatientResponse
            {
                Id = patient.Id,
                UserId = patient.UserId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                MaritalStatus = patient.MaritalStatus,
                DOB = patient.DOB,
                Height = patient.Height,
                Weight = patient.Weight,
                FamilyMedicalHistory = patient.FamilyMedicalHistory
            };
        }

        public static GetPatientResponse GetPatientResponse(Patient patient)
        {
            return new GetPatientResponse
            {
                Id = patient.Id,
                UserId = patient.UserId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                MaritalStatus = patient.MaritalStatus,
                DOB = patient.DOB,
                Height = patient.Height,
                Weight = patient.Weight,
                FamilyMedicalHistory = patient.FamilyMedicalHistory
            };
        }

        public static List<GetPatientResponse> GetPatientsForAUser(List<Patient> patients)
        {
            var patientsUserVM = new List<GetPatientResponse>();
            foreach (var patient in patients)
            {
                patientsUserVM.Add(
                    new GetPatientResponse
                    {
                        Id = patient.Id,
                        UserId = patient.UserId,
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        MaritalStatus = patient.MaritalStatus,
                        DOB = patient.DOB,
                        Height = patient.Height,
                        Weight = patient.Weight,
                        FamilyMedicalHistory = patient.FamilyMedicalHistory
                    }
                );
            }
            return patientsUserVM;
        }

    }
}
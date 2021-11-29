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
    public interface IPatientRepository
    {
        //add patient
        Task<Patient> AddPatient(Patient patient);
        //display of patient
        Task<Patient> GetPatient(string patientId);
        //get all patients
        Task<List<Patient>> GetAllPatients();
        //get all patients of a specific doctor
        Task<List<Patient>> GetAllPatientsPerUser(string userId);
        //update spatient
        Task<bool> UpdatePatient(Patient patient);
        //delete patient
        Task<bool> DeletePatient(string patientId);
    }
}
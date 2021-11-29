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
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        //add patient to database
        public async Task<Patient> AddPatient(Patient patient)
        {
            try
            {
                await _context.AddAsync(patient);
                var result = await _context.SaveChangesAsync();

                return patient;
            }
            catch (Exception e)
            {             
                throw new Exception(e.Message);
            }       
        }

        //get details of a patient
        public async  Task<Patient> GetPatient(string patientId)
        {
            try
            {
                Patient patient = await _context.Patients.FirstOrDefaultAsync(patient => patient.Id == patientId);
                if (patient == null)
                {
                    throw new ArgumentNullException("Resource does not exist");
                }
                return patient;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //get all patients
        public async Task<List<Patient>> GetAllPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        //get per user
        public async Task<List<Patient>> GetAllPatientsPerUser(string userId)
        {
            return await _context.Patients.Where(patient => patient.UserId == userId).ToListAsync();
        }

        //update patient
        public async Task<bool> UpdatePatient(Patient patient)
        {
            var findPatient = await _context.Patients.FirstOrDefaultAsync(patient => patient.Id == patient.Id);
            if (findPatient == null)
            {
                return false;
            }
            _context.Patients.Update(patient);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        //delete patient
        public async Task<bool> DeletePatient(string patientId)
        {
            Patient patient = await _context.Patients.FirstOrDefaultAsync(patient => patient.Id == patientId);
            if (patient == null)
            {
                return false;
            }
            _context.Remove(patient);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
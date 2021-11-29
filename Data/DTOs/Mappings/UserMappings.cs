using Envisio.Models;
using Envisio.Data.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Envisio.Data.DTOs.Mappings
{

    // structure of the response 
    public class UserMappings
    {
        public static UserResponseDTO GetUserResponse(AppUser user)
        {
            return new UserResponseDTO
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                HospitalName = user.HospitalName,
                Id = user.Id
            };
        }

        public static AppUser GetUser(RegistrationRequest request)
        {
            return new AppUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                HospitalName = request.HospitalName,
                UserName = string.IsNullOrWhiteSpace(request.UserName) ? request.Email : request.UserName,
            };
        }
    }
}
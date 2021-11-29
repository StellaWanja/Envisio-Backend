using Envisio.Data.DTOs;
using Envisio.Data.DTOs.Mappings;
using Envisio.Data.DTOs.UserDTOs;
using Envisio.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Envisio.BusinessLogic
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;

        public Authentication(UserManager<AppUser> userManager, ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        //handle user login
        public async Task<UserResponseDTO> Login(UserRequest userRequest)
        {
            AppUser user = await _userManager.FindByEmailAsync(userRequest.Email);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, userRequest.Password))
                {
                    var response =  UserMappings.GetUserResponse(user);
                    response.Token = await _tokenGenerator.GenerateToken(user);

                    return response;
                }

                throw new AccessViolationException("Invalid credentials. Please try again");
            }
            throw new AccessViolationException("Invalid credentials. Please try again");
        }

        //handle user registration
        public async Task<UserResponseDTO> Register(RegistrationRequest registrationRequest)
        {
            AppUser user = UserMappings.GetUser(registrationRequest);
            AppUser userEmail = await _userManager.FindByEmailAsync(registrationRequest.Email);

            IdentityResult result = await _userManager.CreateAsync(user, registrationRequest.Password);
            if (result.Succeeded)
            {
                return UserMappings.GetUserResponse(user);
            }

            string errors = string.Empty;

            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }

            throw new MissingFieldException(errors);
        }

        public async Task<string> ForgotPasswordAsync(ForgotPassword forgotPassword)
        {
            AppUser user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if(user == null)
            {
                return "No user associated with the email provided";
            }
            return "Proceed to reset password";
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordRequest resetPassword)
        {
            AppUser user = await _userManager.FindByEmailAsync(resetPassword.Email);

            if(user == null)
            {
                return "No user associated with the email provided";
            }

            if(resetPassword.NewPassword != resetPassword.ConfirmNewPassword)
            {
                return "Passwords do not match!";
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, resetPassword.NewPassword);

            if(result.Succeeded)
            {
                return "Password has been reset successfully!";
            }
            return "Something went wrong. Please try again!";
        }
    }
}

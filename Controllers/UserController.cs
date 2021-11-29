using Envisio.BusinessLogic;
using Envisio.Data.DTOs;
using Envisio.Data;
using Envisio.Data.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;

namespace envisio_backendv2
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] //authorize data annotation to be applied in all methods
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _context;

        public UserController(IUserService userService, AppDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        //get the user
        [HttpGet("get-user")]
        public async Task<IActionResult> GetUser(string userId)
        {
            try
            {                
                return Ok(await _userService.GetUser(userId)); 
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        
        //update user details (not entire user details)
        // [HttpPatch]
        // public async Task<IActionResult> Update(ResetPasswordRequest resetPasswordRequest)
        // {
        //     try
        //     {
        //         var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

        //         var result = await _userService.ResetPassword(userId, resetPasswordRequest);
        //         return NoContent(); //204
        //     }
        //     catch (MissingMemberException mmex)
        //     {
        //         return BadRequest(mmex.Message);
        //     }
        //     catch (ArgumentException argex)
        //     {
        //         return BadRequest(argex.Message);
        //     }
        //     catch (Exception)
        //     {
        //         return StatusCode(500);
        //     }
        // }

        //delete the user
        //must be both regular and admin to handle delete
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> Delete(string userId)
        {
            try
            {
                await _userService.DeleteUser(userId);
                return NoContent(); //404
            }
            catch (MissingMemberException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
